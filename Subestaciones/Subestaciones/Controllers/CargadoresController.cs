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
    public class CargadoresController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_NomCargadores
        public ActionResult Index()
        {
            return View(db.Cargadores.ToList());
        }

        // GET: Sub_NomCargadores/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargadores sub_NomCargadores = db.Cargadores.Find(id);
            if (sub_NomCargadores == null)
            {
                return HttpNotFound();
            }

            return View(sub_NomCargadores);
        }

        // GET: Sub_NomCargadores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sub_NomCargadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCargador,TipoCargador,VoltajeCorrienteDirecta,Corriente,VoltajeCA")] Cargadores sub_NomCargadores)
        {
            if (ModelState.IsValid)
            {
                db.Cargadores.Add(sub_NomCargadores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sub_NomCargadores);
        }

        // GET: Sub_NomCargadores/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargadores sub_NomCargadores = db.Cargadores.Find(id);
            if (sub_NomCargadores == null)
            {
                return HttpNotFound();
            }
            return View(sub_NomCargadores);
        }

        // POST: Sub_NomCargadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCargador,TipoCargador,VoltajeCorrienteDirecta,Corriente,VoltajeCA")] Cargadores sub_NomCargadores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_NomCargadores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sub_NomCargadores);
        }

        // GET: Sub_NomCargadores/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargadores sub_NomCargadores = db.Cargadores.Find(id);
            if (sub_NomCargadores == null)
            {
                return HttpNotFound();
            }
            return View(sub_NomCargadores);
        }

        // POST: Sub_NomCargadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Cargadores sub_NomCargadores = db.Cargadores.Find(id);
            db.Cargadores.Remove(sub_NomCargadores);
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
