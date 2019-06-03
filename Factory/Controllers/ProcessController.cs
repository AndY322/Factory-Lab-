using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Factory.Models;
using System.Data.Entity;
using System.Web.UI;

namespace Factory.Controllers
{
    public class ProcessController : Controller
    {

        FactoryContext db = new FactoryContext();

        [Authorize]
        public ActionResult GeneralInfo(bool? deleteError)
        {
            ViewBag.deleteError = deleteError;
            var processes = db.Processes;
            return View(processes);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.equipment = new SelectList(db.Equipment, "Id", "Name");
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Process process)
        {
            if(process == null)
            {
                return HttpNotFound();
            }
            db.Processes.Add(process);
            db.SaveChangesAsync();
            return RedirectToAction("GeneralInfo");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? id)
        {
            ViewBag.equipment = new SelectList(db.Equipment, "Id", "Name");
            if (id == null)
            {
                return HttpNotFound();
            }
            var process = db.Processes.FirstOrDefault(d => d.Id == id);
            if (process == null)
            {
                return HttpNotFound();
            }
            return View(process);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Process process)
        {
            if (ModelState.IsValid)
            {
                db.Entry(process).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GeneralInfo");
            }
            return View(process);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int? id)
        {
            bool deleteError = false ;
            if (id == null)
            {
                return HttpNotFound();
            }
            try
            {
                var process = new Process { Id = (int)id };
                db.Entry(process).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch(Exception e)
            {
                deleteError = true;
            }
            return RedirectToAction("GeneralInfo", new { deleteError = deleteError });
        }
    }
}