using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Factory.Models;
using System.Web.Security;
using PagedList;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Factory.Report;

namespace Factory.Controllers
{
    public class HomeController : Controller
    {
        DateTime minValue = DateTime.Parse("01,01,1970");
        FactoryContext db = new FactoryContext();
        const string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=FactoryDB;Integrated Security=True;MultipleActiveResultSets=true";

        [Authorize]
        public ActionResult Index(string searchString, int? page, int? lastChange, string sqlExecuteMessage)
        {
            if (sqlExecuteMessage != string.Empty)
            {
                ViewBag.ErrorMessage = sqlExecuteMessage;
            }
            if (lastChange == 0)
            {
                ViewBag.ErrorMessage = "Прошло слишком мало времени для перехода на новый этап производства";
            }
            const int PAGE_SIZE = 5;
            int pageNumber = page ?? 1;
            var routings = db.Routings.ToList();

            //            select * from Routings r
            //join Details d on d.Id = r.DetailId
            //join Processes p on p.Id = r.ProcessId
            //join Teams t on t.id = r.TeamId

            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.searchString = searchString;
                routings = routings.Where(r => r.Detail.Name.Contains(searchString)).ToList();
            }
            return View(routings.ToList().ToPagedList(pageNumber, PAGE_SIZE));
        }

        [Authorize]
        public ActionResult ContinueTheProcess(int? routingId, int? page)
        {
            if (routingId == null)
            {
                return RedirectToAction("Index");
            }
            var route = db.Routings.FirstOrDefault(r => r.Id == routingId);

            //            select* from Routings r
            //join Details d on d.Id = r.DetailId
            //join Processes p on p.Id = r.ProcessId
            //join Teams t on t.id = r.TeamId
            //where r.Id = routingId
            var nextStep = db.Statuses.Where(s => s.ProcessStep == (route.Status.ProcessStep + 1)).FirstOrDefault();
            var lastChange = DateTime.Now - route.StartProduction;
            if (nextStep != null && lastChange.Days >= 1)
            {
                db.Entry(route).State = EntityState.Modified;
                route.StatusId = nextStep.Id;
                if (route.StartProduction == minValue)
                {
                    route.StartProduction = DateTime.Now;
                }
                else
                {
                    route.PutOnStorage = DateTime.Now;
                }
                db.SaveChanges();
                //                update Routings
                //set StatusId = route.Status.ProcessStep + 1
                //where id = routingId
            }
            return RedirectToAction("Index", new { lastChange = lastChange.Days, page = page });
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateRouting()
        {
            ViewBag.detail = new SelectList(db.Details, "Id", "Name");
            //select * from Details
            ViewBag.process = new SelectList(db.Processes, "Id", "Name");
            //Select * from Processes
            ViewBag.team = new SelectList(db.Teams, "Id", "Number");
            //Select * from teams
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateRouting(Routing route)
        {
            route.PutOnStorage = minValue;
            route.StartProduction = minValue;
            route.StatusId = 1;
            var routings = db.Routings.Where(r => r.TeamId == route.TeamId && (r.StatusId == 1 || r.StatusId == 2)).ToList();
            TimeSpan operatioTime = new TimeSpan();
            try
            {
                operatioTime = DateTime.Now - db.Processes.Find(route.ProcessId).Equipment.Release;
            }
            catch (Exception e)
            {

            }
            var testvar = db.Routings.Where(r => r.TeamId == route.TeamId);
            if (routings.Count != 0)
            {
                ViewBag.ErrorMessage = "у этой бригады уже есть активная партия деталей";
                ViewBag.detail = new SelectList(db.Details, "Id", "Name");
                ViewBag.process = new SelectList(db.Processes, "Id", "Name");
                ViewBag.team = new SelectList(db.Teams, "Id", "Number");
                return View();
            }
            if (operatioTime.TotalDays >= 730)
            {
                ViewBag.ErrorMessage = "оборудование используемое процессом старше двух лет, обновите его";
                ViewBag.detail = new SelectList(db.Details, "Id", "Name");
                ViewBag.process = new SelectList(db.Processes, "Id", "Name");
                ViewBag.team = new SelectList(db.Teams, "Id", "Number");
                return View();
            }
            //            insert into Routings(DetailId, ProcessId, TeamId, PutOnStorage, Notes, StatusId, StartProduction)
            //values(route.DetailId, route.ProcessId, route.TeamId, route.PutOnStorage, route.Notes, route.StatusId, route.StartProduction)
            db.Routings.Add(route);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult CrossQuery()
        {
            string sqlText = "SELECT ds.Name, \n";
            var details = db.Details;
            string sqlHelperTextString = "";
            List<string> detailNames = new List<string>();
            foreach (var detail in details)
            {
                sqlHelperTextString += string.Format("Count (CASE d.Name WHEN '{0}' THEN TeamId ELSE null END) AS [{0}], \n", detail.Name);
                detailNames.Add(detail.Name);
            }
            ViewBag.detailNames = detailNames;
            sqlHelperTextString = sqlHelperTextString.Remove(sqlHelperTextString.Length - 3);
            sqlText += sqlHelperTextString;
            sqlText += " FROM Routings r\n" +
            " join Details d on d.id = r.DetailId\n" +
            " join DetailStatus ds on ds.Id = r.StatusId\n" +
            " GROUP BY ds.Name";
            ViewBag.sqlText = sqlText;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<QueryHelper> list = new List<QueryHelper>();
            while (reader.Read())
            {
                List<int> detailCount = new List<int>();
                for (int i = 1; i <= detailNames.Count; i++)
                {
                    detailCount.Add(reader.GetInt32(i));
                }
                list.Add(new QueryHelper()
                {
                    Status = reader.GetString(0),
                    MotorCount = detailCount
                });
            }
            connection.Close();
            return View(list);
        }

        [Authorize]
        public ActionResult AddColumn()
        {
            string sqlText = "alter table Routings add TestColumn int";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlText, connection);
            connection.Open();
            command.ExecuteNonQuery();
            return RedirectToAction("Index", new { sqlExecuteMessage = "Колонка добавлена, sql текст: " + sqlText });
        }

        [Authorize]
        public ActionResult DropColumn()
        {
            string sqlText = "alter table Routings drop column TestColumn";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlText, connection);
            connection.Open();
            command.ExecuteNonQuery();
            return RedirectToAction("Index", new { sqlExecuteMessage = "Колонка удалена, sql текст: " + sqlText });
        }

        [Authorize]
        public ActionResult Report()
        {
            ReportDataSet ds = new ReportDataSet();
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(1500);

            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT Routings.PutOnStorage, Routings.Notes, Routings.StartProduction, " +
                "Details.Name, Details.Cost, Teams.Number, Processes.Name AS Expr1, DetailStatus.Name AS Expr2"
                + " FROM Routings INNER JOIN"
                + " Teams ON Routings.TeamId = Teams.Id INNER JOIN"
                + " Details ON Routings.DetailId = Details.Id INNER JOIN"
                + " Processes ON Routings.ProcessId = Processes.Id INNER JOIN"
                + " DetailStatus ON Routings.StatusId = DetailStatus.Id", conx);

            adp.Fill(ds);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Report\Report2.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDataSet", ds.Tables[1]));


            ViewBag.ReportViewer = reportViewer;

            return View();
        }
    }
}