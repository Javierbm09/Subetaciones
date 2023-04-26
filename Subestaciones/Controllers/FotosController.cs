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
    public class FotosController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Fotos
        public ActionResult Index()
        {
            return View(db.Fotos.ToList());
        }

        
        public ActionResult IndexSub(string cod)
        {
            ViewBag.instalacion = cod;
            return View(db.Fotos.ToList().Where(c => c.Instalacion == cod));
        }


        // GET: Fotos/Details/5
        public ActionResult Details(short? id_ea, int numA)
        {
            if (id_ea == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos fotos = db.Fotos.Find(id_ea, numA);
            if (fotos == null)
            {
                return HttpNotFound();
            }
            return View(fotos);
        }

        // GET: Fotos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fotos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_EAdministrativa,NumAccion,Instalacion,Version,Nombre,Foto,tipo")] Fotos fotos)
        {
            if (ModelState.IsValid)
            {
                db.Fotos.Add(fotos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fotos);
        }

        // GET: Fotos/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos fotos = db.Fotos.Find(id);
            if (fotos == null)
            {
                return HttpNotFound();
            }
            return View(fotos);
        }

        // POST: Fotos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_EAdministrativa,NumAccion,Instalacion,Version,Nombre,Foto,tipo")] Fotos fotos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fotos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fotos);
        }

        // GET: Fotos/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fotos fotos = db.Fotos.Find(id);
            if (fotos == null)
            {
                return HttpNotFound();
            }
            return View(fotos);
        }

        // POST: Fotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Fotos fotos = db.Fotos.Find(id);
            db.Fotos.Remove(fotos);
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
