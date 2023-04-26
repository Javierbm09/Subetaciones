using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;

namespace Subestaciones.Controllers
{
    public class NomencladorTensionController : Controller
    {
        private DBContext db = new DBContext();

        // GET: NomencladorTension
        public ActionResult Index()
        {
            return View(db.NomencladorTension.ToList());
        }

        // GET: NomencladorTension/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NomencladorTension nomencladorTension = db.NomencladorTension.Find(id);
            if (nomencladorTension == null)
            {
                return HttpNotFound();
            }
            return View(nomencladorTension);
        }

        // GET: NomencladorTension/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NomencladorTension/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTension,tension")] NomencladorTension nomencladorTension)
        {
            if (ModelState.IsValid)
            {
                db.NomencladorTension.Add(nomencladorTension);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nomencladorTension);
        }

        // GET: NomencladorTension/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NomencladorTension nomencladorTension = db.NomencladorTension.Find(id);
            if (nomencladorTension == null)
            {
                return HttpNotFound();
            }
            return View(nomencladorTension);
        }

        // POST: NomencladorTension/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTension,tension")] NomencladorTension nomencladorTension)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nomencladorTension).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nomencladorTension);
        }

        // GET: NomencladorTension/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NomencladorTension nomencladorTension = db.NomencladorTension.Find(id);
            if (nomencladorTension == null)
            {
                return HttpNotFound();
            }
            return View(nomencladorTension);
        }

        // POST: NomencladorTension/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            NomencladorTension nomencladorTension = db.NomencladorTension.Find(id);
            db.NomencladorTension.Remove(nomencladorTension);
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
