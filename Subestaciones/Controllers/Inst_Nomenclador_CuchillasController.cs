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
    public class Inst_Nomenclador_CuchillasController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Inst_Nomenclador_Cuchillas
        public ActionResult Index()
        {
            var inst_Nomenclador_Cuchillas = db.Inst_Nomenclador_Cuchillas.Include(i => i.Inst_Nomenclador_Cuchillas_Mando).Include(i => i.Inst_Nomenclador_Cuchillas_Operacion).Include(i => i.Inst_Nomenclador_Cuchillas_Tension);
            return View(inst_Nomenclador_Cuchillas.ToList());
        }

        // GET: Inst_Nomenclador_Cuchillas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_Cuchillas inst_Nomenclador_Cuchillas = db.Inst_Nomenclador_Cuchillas.Find(id);
            if (inst_Nomenclador_Cuchillas == null)
            {
                return HttpNotFound();
            }
            var fabricante = (
              from sb in db.Fabricantes
              select new SelectListItem { Value = sb.Id_Fabricante.ToString(), Text = sb.Nombre.ToString() }
          ).ToList();
            ViewBag.fab = new SelectList(fabricante, "Value", "Text", inst_Nomenclador_Cuchillas.Id_Fabricante);

            ViewBag.mando = new SelectList(db.Inst_Nomenclador_Cuchillas_Mando, "Id_Mando", "DescripcionMandoCuchillas", inst_Nomenclador_Cuchillas.Id_Mando);
            ViewBag.op = new SelectList(db.Inst_Nomenclador_Cuchillas_Operacion, "Id_Operacion", "DescripcionOperacionCuchillas", inst_Nomenclador_Cuchillas.Id_Operacion);
            ViewBag.IdT = new SelectList(db.Inst_Nomenclador_Cuchillas_Tension, "Id_Tension", "DescripcionTensionCuchillas", inst_Nomenclador_Cuchillas.Id_Tension);
            return View(inst_Nomenclador_Cuchillas);
        }

        // GET: Inst_Nomenclador_Cuchillas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Mando = new SelectList(db.Inst_Nomenclador_Cuchillas_Mando, "Id_Mando", "DescripcionMandoCuchillas");
            ViewBag.Id_Operacion = new SelectList(db.Inst_Nomenclador_Cuchillas_Operacion, "Id_Operacion", "DescripcionOperacionCuchillas");
            ViewBag.Id_Tension = new SelectList(db.Inst_Nomenclador_Cuchillas_Tension, "Id_Tension", "DescripcionTensionCuchillas");
            return View();
        }

        // POST: Inst_Nomenclador_Cuchillas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Cuchilla,Codigo,Id_Fabricante,Id_Tension,Id_Operacion,Id_Mando,Id_Administrativa,NumAccion,id_EAdministrativa_Prov")] Inst_Nomenclador_Cuchillas inst_Nomenclador_Cuchillas)
        {
            if (ModelState.IsValid)
            {
                db.Inst_Nomenclador_Cuchillas.Add(inst_Nomenclador_Cuchillas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Mando = new SelectList(db.Inst_Nomenclador_Cuchillas_Mando, "Id_Mando", "DescripcionMandoCuchillas", inst_Nomenclador_Cuchillas.Id_Mando);
            ViewBag.Id_Operacion = new SelectList(db.Inst_Nomenclador_Cuchillas_Operacion, "Id_Operacion", "DescripcionOperacionCuchillas", inst_Nomenclador_Cuchillas.Id_Operacion);
            ViewBag.Id_Tension = new SelectList(db.Inst_Nomenclador_Cuchillas_Tension, "Id_Tension", "DescripcionTensionCuchillas", inst_Nomenclador_Cuchillas.Id_Tension);
            return View(inst_Nomenclador_Cuchillas);
        }

        // GET: Inst_Nomenclador_Cuchillas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_Cuchillas inst_Nomenclador_Cuchillas = db.Inst_Nomenclador_Cuchillas.Find(id);
            if (inst_Nomenclador_Cuchillas == null)
            {
                return HttpNotFound();
            }
            var fabricante = (
              from sb in db.Fabricantes
              select new SelectListItem { Value = sb.Id_Fabricante.ToString(), Text = sb.Nombre.ToString() }
          ).ToList();
            ViewBag.fab = new SelectList(fabricante, "Value", "Text", inst_Nomenclador_Cuchillas.Id_Fabricante);

            ViewBag.mando = new SelectList(db.Inst_Nomenclador_Cuchillas_Mando, "Id_Mando", "DescripcionMandoCuchillas", inst_Nomenclador_Cuchillas.Id_Mando);
            ViewBag.op = new SelectList(db.Inst_Nomenclador_Cuchillas_Operacion, "Id_Operacion", "DescripcionOperacionCuchillas", inst_Nomenclador_Cuchillas.Id_Operacion);
            ViewBag.IdT = new SelectList(db.Inst_Nomenclador_Cuchillas_Tension, "Id_Tension", "DescripcionTensionCuchillas", inst_Nomenclador_Cuchillas.Id_Tension);
            return View(inst_Nomenclador_Cuchillas);
        }

        // POST: Inst_Nomenclador_Cuchillas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Cuchilla,Codigo,Id_Fabricante,Id_Tension,Id_Operacion,Id_Mando,Id_Administrativa,NumAccion,id_EAdministrativa_Prov")] Inst_Nomenclador_Cuchillas inst_Nomenclador_Cuchillas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inst_Nomenclador_Cuchillas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "InstalacionDesconectivos", new { inserta = "si" });
            }
            var fabricante = (
                 from sb in db.Fabricantes
                 select new SelectListItem { Value = sb.Id_Fabricante.ToString(), Text = sb.Nombre.ToString() }
             ).ToList();
            ViewBag.fab = new SelectList(fabricante, "Value", "Text", inst_Nomenclador_Cuchillas.Id_Fabricante);

            ViewBag.mando = new SelectList(db.Inst_Nomenclador_Cuchillas_Mando, "Id_Mando", "DescripcionMandoCuchillas", inst_Nomenclador_Cuchillas.Id_Mando);
            ViewBag.op = new SelectList(db.Inst_Nomenclador_Cuchillas_Operacion, "Id_Operacion", "DescripcionOperacionCuchillas", inst_Nomenclador_Cuchillas.Id_Operacion);
            ViewBag.IdT = new SelectList(db.Inst_Nomenclador_Cuchillas_Tension, "Id_Tension", "DescripcionTensionCuchillas", inst_Nomenclador_Cuchillas.Id_Tension);
            return View(inst_Nomenclador_Cuchillas);
        }

        // GET: Inst_Nomenclador_Cuchillas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_Cuchillas inst_Nomenclador_Cuchillas = db.Inst_Nomenclador_Cuchillas.Find(id);
            if (inst_Nomenclador_Cuchillas == null)
            {
                return HttpNotFound();
            }
            return View(inst_Nomenclador_Cuchillas);
        }

        // POST: Inst_Nomenclador_Cuchillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inst_Nomenclador_Cuchillas inst_Nomenclador_Cuchillas = db.Inst_Nomenclador_Cuchillas.Find(id);
            db.Inst_Nomenclador_Cuchillas.Remove(inst_Nomenclador_Cuchillas);
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
