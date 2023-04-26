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
    public class Inst_Nomenclador_PuenteController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Inst_Nomenclador_Puente
        public ActionResult Index()
        {
            return View(db.Inst_Nomenclador_Puente.ToList());
        }

        // GET: Inst_Nomenclador_Puente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_Puente puente = db.Inst_Nomenclador_Puente.Find(id);
            if (puente == null)
            {
                return HttpNotFound();
            }
            var TipoPuentes = (
                   from sb in db.Inst_Nomenclador_Puente_Tipo
                   select new SelectListItem { Value = sb.Id_Tipo.ToString(), Text = sb.DescripcionTipo.ToString() }
               ).ToList();

            var ModeloPuentes = (
                from sb in db.Inst_Nomenclador_Puente_Modelo
                select new SelectListItem { Value = sb.Id_Modelo.ToString(), Text = sb.Descripcion_Modelo.ToString() }
            ).ToList();
            ViewBag.tipo = new SelectList(TipoPuentes, "Value", "Text", puente.Id_Tipo);
            ViewBag.modelo = new SelectList(ModeloPuentes, "Value", "Text", puente.Id_Modelo);
            return View(puente);
        }

        // GET: Inst_Nomenclador_Puente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inst_Nomenclador_Puente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Puente,Codigo,Id_Modelo,Id_Tipo,Bimetalica,Id_Administrativa,NumAccion,id_EAdministrativa_Prov")] Inst_Nomenclador_Puente inst_Nomenclador_Puente)
        {
            if (ModelState.IsValid)
            {
                db.Inst_Nomenclador_Puente.Add(inst_Nomenclador_Puente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inst_Nomenclador_Puente);
        }

        // GET: Inst_Nomenclador_Puente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_Puente puente = db.Inst_Nomenclador_Puente.Find(id);
            if (puente == null)
            {
                return HttpNotFound();
            }
            var TipoPuentes = (
                   from sb in db.Inst_Nomenclador_Puente_Tipo
                   select new SelectListItem { Value = sb.Id_Tipo.ToString(), Text = sb.DescripcionTipo.ToString() }
               ).ToList();

            var ModeloPuentes = (
                from sb in db.Inst_Nomenclador_Puente_Modelo
                select new SelectListItem { Value = sb.Id_Modelo.ToString(), Text = sb.Descripcion_Modelo.ToString() }
            ).ToList();
            ViewBag.tipo = new SelectList(TipoPuentes, "Value", "Text", puente.Id_Tipo);
            ViewBag.modelo = new SelectList(ModeloPuentes, "Value", "Text", puente.Id_Modelo);
            return View(puente);
        }

        // POST: Inst_Nomenclador_Puente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Puente,Codigo,Id_Modelo,Id_Tipo,Bimetalica,Id_Administrativa,NumAccion,id_EAdministrativa_Prov")]Inst_Nomenclador_Puente inst_Nomenclador_Puente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inst_Nomenclador_Puente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "InstalacionDesconectivos", new { inserta = "si" });
            }
            return View(inst_Nomenclador_Puente);
        }

        // GET: Inst_Nomenclador_Puente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_Puente inst_Nomenclador_Puente = db.Inst_Nomenclador_Puente.Find(id);
            if (inst_Nomenclador_Puente == null)
            {
                return HttpNotFound();
            }
            return View(inst_Nomenclador_Puente);
        }

        // POST: Inst_Nomenclador_Puente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inst_Nomenclador_Puente inst_Nomenclador_Puente = db.Inst_Nomenclador_Puente.Find(id);
            db.Inst_Nomenclador_Puente.Remove(inst_Nomenclador_Puente);
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
