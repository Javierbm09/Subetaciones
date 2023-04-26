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
    public class Inst_Nomenclador_InterruptorAireController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Inst_Nomenclador_InterruptorAire
        public ActionResult Index()
        {
            var inst_Nomenclador_InterruptorAire = db.Inst_Nomenclador_InterruptorAire.Include(i => i.Inst_Nomenclador_InterruptorAire_Mando).Include(i => i.Inst_Nomenclador_InterruptorAire_Operacion).Include(i => i.Inst_Nomenclador_InterruptorAire_Tension);
            return View(inst_Nomenclador_InterruptorAire.ToList());
        }

        // GET: Inst_Nomenclador_InterruptorAire/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_InterruptorAire inst_Nomenclador_InterruptorAire = db.Inst_Nomenclador_InterruptorAire.Find(id);
            if (inst_Nomenclador_InterruptorAire == null)
            {
                return HttpNotFound();
            }
            var fabricante = (
             from sb in db.Fabricantes
             select new SelectListItem { Value = sb.Id_Fabricante.ToString(), Text = sb.Nombre.ToString() }
         ).ToList();
            ViewBag.fab = new SelectList(fabricante, "Value", "Text", inst_Nomenclador_InterruptorAire.Id_Fabricante);
            ViewBag.Id_Mando = new SelectList(db.Inst_Nomenclador_InterruptorAire_Mando, "Id_Mando", "DescripcionMando", inst_Nomenclador_InterruptorAire.Id_Mando);
            ViewBag.Id_Operacion = new SelectList(db.Inst_Nomenclador_InterruptorAire_Operacion, "Id_Operacion", "DescripcionOperacion", inst_Nomenclador_InterruptorAire.Id_Operacion);
            ViewBag.Id_Tension = new SelectList(db.Inst_Nomenclador_InterruptorAire_Tension, "Id_Tension", "DescripcionTension", inst_Nomenclador_InterruptorAire.Id_Tension);

            return View(inst_Nomenclador_InterruptorAire);
        }

        // GET: Inst_Nomenclador_InterruptorAire/Create
        public ActionResult Create()
        {
            ViewBag.Id_Mando = new SelectList(db.Inst_Nomenclador_InterruptorAire_Mando, "Id_Mando", "DescripcionMando");
            ViewBag.Id_Operacion = new SelectList(db.Inst_Nomenclador_InterruptorAire_Operacion, "Id_Operacion", "DescripcionOperacion");
            ViewBag.Id_Tension = new SelectList(db.Inst_Nomenclador_InterruptorAire_Tension, "Id_Tension", "DescripcionTension");
            return View();
        }

        // POST: Inst_Nomenclador_InterruptorAire/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_InterruptorAire,Codigo,Id_Fabricante,Id_Tension,Id_Operacion,Id_Mando,Id_Administrativa,NumAccion,id_EAdministrativa_Prov")] Inst_Nomenclador_InterruptorAire inst_Nomenclador_InterruptorAire)
        {
            if (ModelState.IsValid)
            {
                db.Inst_Nomenclador_InterruptorAire.Add(inst_Nomenclador_InterruptorAire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Mando = new SelectList(db.Inst_Nomenclador_InterruptorAire_Mando, "Id_Mando", "DescripcionMando", inst_Nomenclador_InterruptorAire.Id_Mando);
            ViewBag.Id_Operacion = new SelectList(db.Inst_Nomenclador_InterruptorAire_Operacion, "Id_Operacion", "DescripcionOperacion", inst_Nomenclador_InterruptorAire.Id_Operacion);
            ViewBag.Id_Tension = new SelectList(db.Inst_Nomenclador_InterruptorAire_Tension, "Id_Tension", "DescripcionTension", inst_Nomenclador_InterruptorAire.Id_Tension);
            return View(inst_Nomenclador_InterruptorAire);
        }

        // GET: Inst_Nomenclador_InterruptorAire/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_InterruptorAire inst_Nomenclador_InterruptorAire = db.Inst_Nomenclador_InterruptorAire.Find(id);
            if (inst_Nomenclador_InterruptorAire == null)
            {
                return HttpNotFound();
            }
            var fabricante = (
             from sb in db.Fabricantes
             select new SelectListItem { Value = sb.Id_Fabricante.ToString(), Text = sb.Nombre.ToString() }
         ).ToList();
            ViewBag.fab = new SelectList(fabricante, "Value", "Text", inst_Nomenclador_InterruptorAire.Id_Fabricante);
            ViewBag.Id_Mando = new SelectList(db.Inst_Nomenclador_InterruptorAire_Mando, "Id_Mando", "DescripcionMando", inst_Nomenclador_InterruptorAire.Id_Mando);
            ViewBag.Id_Operacion = new SelectList(db.Inst_Nomenclador_InterruptorAire_Operacion, "Id_Operacion", "DescripcionOperacion", inst_Nomenclador_InterruptorAire.Id_Operacion);
            ViewBag.Id_Tension = new SelectList(db.Inst_Nomenclador_InterruptorAire_Tension, "Id_Tension", "DescripcionTension", inst_Nomenclador_InterruptorAire.Id_Tension);

            return View(inst_Nomenclador_InterruptorAire);
        }

        // POST: Inst_Nomenclador_InterruptorAire/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_InterruptorAire,Codigo,Id_Fabricante,Id_Tension,Id_Operacion,Id_Mando,Id_Administrativa,NumAccion,id_EAdministrativa_Prov")] Inst_Nomenclador_InterruptorAire inst_Nomenclador_InterruptorAire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inst_Nomenclador_InterruptorAire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "InstalacionDesconectivos", new { inserta = "Si" });
            }
            var fabricante = (
             from sb in db.Fabricantes
             select new SelectListItem { Value = sb.Id_Fabricante.ToString(), Text = sb.Nombre.ToString() }
         ).ToList();
            ViewBag.fab = new SelectList(fabricante, "Value", "Text", inst_Nomenclador_InterruptorAire.Id_Fabricante);
            ViewBag.Id_Mando = new SelectList(db.Inst_Nomenclador_InterruptorAire_Mando, "Id_Mando", "DescripcionMando", inst_Nomenclador_InterruptorAire.Id_Mando);
            ViewBag.Id_Operacion = new SelectList(db.Inst_Nomenclador_InterruptorAire_Operacion, "Id_Operacion", "DescripcionOperacion", inst_Nomenclador_InterruptorAire.Id_Operacion);
            ViewBag.Id_Tension = new SelectList(db.Inst_Nomenclador_InterruptorAire_Tension, "Id_Tension", "DescripcionTension", inst_Nomenclador_InterruptorAire.Id_Tension);
            return View(inst_Nomenclador_InterruptorAire);
        }

        // GET: Inst_Nomenclador_InterruptorAire/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_InterruptorAire inst_Nomenclador_InterruptorAire = db.Inst_Nomenclador_InterruptorAire.Find(id);
            if (inst_Nomenclador_InterruptorAire == null)
            {
                return HttpNotFound();
            }
            return View(inst_Nomenclador_InterruptorAire);
        }

        // POST: Inst_Nomenclador_InterruptorAire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inst_Nomenclador_InterruptorAire inst_Nomenclador_InterruptorAire = db.Inst_Nomenclador_InterruptorAire.Find(id);
            db.Inst_Nomenclador_InterruptorAire.Remove(inst_Nomenclador_InterruptorAire);
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
