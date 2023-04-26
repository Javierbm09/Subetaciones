using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class NomencladorBateriasController : Controller
    {
        private DBContext db = new DBContext();

        // GET: NomencladorBaterias
        public ActionResult Index()
        {
            return View(db.NomencladorBaterias.ToList());
        }

        // GET: NomencladorBaterias/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NomencladorBaterias nomencladorBaterias = db.NomencladorBaterias.Find(id);
            if (nomencladorBaterias == null)
            {
                return HttpNotFound();
            }
            return View(nomencladorBaterias);
        }

        // GET: NomencladorBaterias/Create
        public ActionResult Create()
        {
            var clase = new Repositorio(db);

            ViewBag.Tipo = new SelectList(clase.ClaseBateria(), "Value", "Text");
            return View();
        }

        // POST: NomencladorBaterias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBateria,TipoBateria,CapacidadBateria,TensionBateria,ClaseBateria")] NomencladorBaterias nomencladorBaterias)
        {
            if (ModelState.IsValid)
            {
                db.NomencladorBaterias.Add(nomencladorBaterias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nomencladorBaterias);
        }

        // GET: NomencladorBaterias/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NomencladorBaterias nomencladorBaterias = db.NomencladorBaterias.Find(id);
            if (nomencladorBaterias == null)
            {
                return HttpNotFound();
            }
            var clase = new Repositorio(db);

            ViewBag.Tipo = new SelectList(clase.ClaseBateria(), "Value", "Text", nomencladorBaterias.ClaseBateria);


            return View(nomencladorBaterias);
        }

        // POST: NomencladorBaterias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBateria,TipoBateria,CapacidadBateria,TensionBateria,ClaseBateria")] NomencladorBaterias nomencladorBaterias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nomencladorBaterias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nomencladorBaterias);
        }

        // GET: NomencladorBaterias/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NomencladorBaterias nomencladorBaterias = db.NomencladorBaterias.Find(id);
            if (nomencladorBaterias == null)
            {
                return HttpNotFound();
            }
            return View(nomencladorBaterias);
        }

        [HttpPost]
        public JsonResult Eliminar(short id)
        {
            if (ValidarSiVinculadoABateria(id))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    NomencladorBaterias nomencladorBaterias = db.NomencladorBaterias.Find(id);
                    db.NomencladorBaterias.Remove(nomencladorBaterias);
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
        }


        // POST: NomencladorBaterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            NomencladorBaterias nomencladorBaterias = db.NomencladorBaterias.Find(id);
            db.NomencladorBaterias.Remove(nomencladorBaterias);
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

        public bool ValidarSiVinculadoABateria(int idBateria)
        {
            var bateria = db.Sub_Baterias.Where(a => a.Tipo == idBateria).FirstOrDefault();

            return bateria != null ? true : false;
        }

    [HttpPost]
    public ActionResult ListadoBaterias()
    {

        return PartialView("_VPTablaBateria", db.NomencladorBaterias.ToList());
    }
}

}
