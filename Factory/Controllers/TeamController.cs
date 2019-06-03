using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Factory.Models;
using System.Data.Entity;

namespace Factory.Controllers
{
    public class TeamController : Controller
    {
        FactoryContext db = new FactoryContext();

        [Authorize]
        public ActionResult GeneralInfo()
        {
            var team = db.Teams;
            return View(team);
        }
        
        [Authorize]
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            var team = db.Teams.Where(t => t.Id == id).Include(t => t.Employees).FirstOrDefault();
            if(team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.specialization = new SelectList(db.Specializations, "Id", "Name");
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Team team)
        {
            if(team == null)
            {
                return HttpNotFound();
            }
            db.Teams.Add(team);
            db.SaveChangesAsync();
            return RedirectToAction("GeneralInfo");
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if(id == null || db.Teams.FirstOrDefault(t => t.Id == id) == null)
            {
                return HttpNotFound();
            }
            db.Teams.Remove(db.Teams.FirstOrDefault(t => t.Id == id));
            db.SaveChangesAsync();
            return RedirectToAction("GeneralInfo");
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateEmployee(int? Id)
        {
            ViewBag.position = new SelectList(db.JobPositions, "Id", "Name");
            if (Id == null && db.Teams.First(t => t.Id == Id) == null)
            {
                return HttpNotFound();
            }
            //ViewBag.team = new SelectList(db.Teams, "Id", "Number");
            ViewBag.teamId = Id;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
            if(employee == null)
            {
                return HttpNotFound();
            }
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("GeneralInfo");
        }
    }
}