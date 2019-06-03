using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Factory.Models;
using System.Data.Entity;

namespace Factory.Controllers
{
    public class DetailController : Controller
    {
        FactoryContext db = new FactoryContext();

        [Authorize]
        public ActionResult GeneralInfo(bool? deleteError)
        {
            ViewBag.deleteError = deleteError;
            var details = db.Details;
            //select * from details
            return View(details);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Detail detail)
        {
            if(detail == null)
            {
                return HttpNotFound();
            }
            db.Details.Add(detail);
            db.SaveChangesAsync();
            //insert into details(Name, Size, Amount, Cost, Notes)
            //values(detail.Name, detail.Size, detail.Amount, detail.Cost, detail.Notes)
            return RedirectToAction("GeneralInfo");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            var detail = db.Details.FirstOrDefault(d => d.Id == id);
            if(detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Detail detail)
        {
            if(ModelState.IsValid)
            {
                db.Entry(detail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GeneralInfo");
            }
            return View(detail);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int? id)
        {
            bool deleteError = false;
            if (id == null)
            {
                return HttpNotFound();
            }
            try
            {
                Detail process = db.Details.Find(id);
                db.Details.Remove(process);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                deleteError = true;
            }
            return RedirectToAction("GeneralInfo", new { deleteError = deleteError });
        }
    }
}