using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ResidualCenter.Models;

namespace ResidualCenter.Controllers
{
    public class ResidueTypesController : Controller
    {
        private ResidualCenterContext db = new ResidualCenterContext();

        // GET: ResidueTypes
        public ActionResult Index()
        {
            return View(db.ResidueTypes.ToList());
        }

        // GET: ResidueTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResidueType residueType = db.ResidueTypes.Find(id);
            if (residueType == null)
            {
                return HttpNotFound();
            }
            return View(residueType);
        }

        // GET: ResidueTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResidueTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Unit")] ResidueType residueType)
        {
            if (ModelState.IsValid)
            {
                db.ResidueTypes.Add(residueType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(residueType);
        }

        // GET: ResidueTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResidueType residueType = db.ResidueTypes.Find(id);
            if (residueType == null)
            {
                return HttpNotFound();
            }
            return View(residueType);
        }

        // POST: ResidueTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Unit")] ResidueType residueType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(residueType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(residueType);
        }

        // GET: ResidueTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResidueType residueType = db.ResidueTypes.Find(id);
            if (residueType == null)
            {
                return HttpNotFound();
            }
            return View(residueType);
        }

        // POST: ResidueTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResidueType residueType = db.ResidueTypes.Find(id);
            db.ResidueTypes.Remove(residueType);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
