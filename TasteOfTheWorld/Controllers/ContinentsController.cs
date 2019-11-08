using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TasteOfTheWorld.Models;

namespace TasteOfTheWorld.Controllers
{
    public class ContinentsController : Controller
    {
        private CapstoneProjectEntities db = new CapstoneProjectEntities();

        // GET: Continents
        public ActionResult Index()
        {
            return View(db.Continents.ToList());
        }

        // GET: Continents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Continent continent = db.Continents.Find(id);
            if (continent == null)
            {
                return HttpNotFound();
            }
            return View(continent);
        }

        // GET: Continents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Continents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Continent_Id,Continent_Name")] Continent continent)
        {
            if (ModelState.IsValid)
            {
                db.Continents.Add(continent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(continent);
        }

        // GET: Continents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Continent continent = db.Continents.Find(id);
            if (continent == null)
            {
                return HttpNotFound();
            }
            return View(continent);
        }

        // POST: Continents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Continent_Id,Continent_Name")] Continent continent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(continent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(continent);
        }

        // GET: Continents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Continent continent = db.Continents.Find(id);
            if (continent == null)
            {
                return HttpNotFound();
            }
            return View(continent);
        }

        // POST: Continents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Continent continent = db.Continents.Find(id);
            db.Continents.Remove(continent);
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
