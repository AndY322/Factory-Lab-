using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Factory.Models;
using System.Data.Entity;

namespace Factory.Controllers
{
    public class EquipmentController : Controller
    {
        FactoryContext db = new FactoryContext();

        [Authorize]
        public ActionResult GeneralInfo()
        {
            var equipment = db.Equipment;
            return View(equipment);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Equipment equipment)
        {
            if(equipment == null)
            {
                return HttpNotFound();
            }
            db.Equipment.Add(equipment);
            db.SaveChanges();
            return RedirectToAction("GeneralInfo");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var equipment = db.Equipment.FirstOrDefault(d => d.Id == id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GeneralInfo");
            }
            return View(equipment);
        }
    }
}