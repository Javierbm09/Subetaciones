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
    public class ES_TP_DevanadoController : Controller
    {
        private DBContext db = new DBContext();

        // GET: ES_TP_Devanado
        public ActionResult Index()
        {
            return View(db.ES_TP_Devanado.ToList());
        }

        // GET: ES_TP_Devanado/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ES_TP_Devanado eS_TP_Devanado = db.ES_TP_Devanado.Find(id);
            if (eS_TP_Devanado == null)
            {
                return HttpNotFound();
            }
            return View(eS_TP_Devanado);
        }

        // GET: ES_TP_Devanado/Create
        public ActionResult Create(string NroSerie, short Cant)
        {
            ViewBag.Cant = Cant;
            ViewBag.NroSerie = NroSerie;
            var repoTC = new TCRepositorio(db);
            ViewBag.tension = repoTC.Tension("");
            return View();
        }

        // POST: ES_TP_Devanado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string NroSerie0 = null, string NroSerie1 = null, string NroSerie2 = null,
            string tension0 = null, string tension1 = null, string tension2 = null,
            short Nro_Dev0 = 0, short Nro_Dev1 = 0, short Nro_Dev2 = 0, 
            double Capacidad0 = 0, double Capacidad1 = 0, double Capacidad2 = 0, 
            string Clase_Precision0 = null, string Clase_Precision1 = null, string Clase_Precision2 = null,
            string Designacion0 = null, string Designacion1 = null, string Designacion2 = null)
        {
            if (ModelState.IsValid)
            {
                var eS_TP_Devanado1 = new ES_TP_Devanado();
                eS_TP_Devanado1.Nro_Dev = Nro_Dev0;
                eS_TP_Devanado1.Nro_TP = NroSerie0;
                eS_TP_Devanado1.Tension = tension0;
                eS_TP_Devanado1.Capacidad = Capacidad0;
                eS_TP_Devanado1.ClasePresicion = Clase_Precision0;
                eS_TP_Devanado1.Designacion = Designacion0;

                db.ES_TP_Devanado.Add(eS_TP_Devanado1);
                db.SaveChanges();

                if (NroSerie1 != null && Nro_Dev1 != 0)
                {
                    var eS_TP_Devanado2 = new ES_TP_Devanado();
                    eS_TP_Devanado2.Nro_Dev = Nro_Dev1;
                    eS_TP_Devanado2.Nro_TP = NroSerie1;
                    eS_TP_Devanado2.Tension = tension1;
                    eS_TP_Devanado2.Capacidad = Capacidad1;
                    eS_TP_Devanado2.ClasePresicion = Clase_Precision1;
                    eS_TP_Devanado2.Designacion = Designacion1;
                    db.ES_TP_Devanado.Add(eS_TP_Devanado2);
                    db.SaveChanges();
                }

                if (NroSerie2 != null && Nro_Dev2 != 0)
                {
                    var eS_TP_Devanado3 = new ES_TP_Devanado();
                    eS_TP_Devanado3.Nro_Dev = Nro_Dev2;
                    eS_TP_Devanado3.Nro_TP = NroSerie2;
                    eS_TP_Devanado3.Tension = tension2;
                    eS_TP_Devanado3.Capacidad = Capacidad2;
                    eS_TP_Devanado3.ClasePresicion = Clase_Precision2;
                    eS_TP_Devanado3.Designacion = Designacion2;
                    db.ES_TP_Devanado.Add(eS_TP_Devanado3);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "ES_TransformadorPotencial");
            }

            return View();
        }

        //para crear un solo devanado cuando es desde el editar TP
        public ActionResult CrearDevanado(string NroSerie, short Cant)
        {
            ViewBag.Cant = Cant;
            ViewBag.NroSerie = NroSerie;
            var repoTC = new TCRepositorio(db);
            ViewBag.tension = repoTC.Tension("");
            var p = new ES_TP_Devanado();
            p.Nro_Dev = Cant;
            p.Nro_TP = NroSerie;

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearDevanado([Bind(Include = "Nro_Dev,Nro_TP, Tension,Capacidad,Clase_Precision,Designacion")] ES_TP_Devanado eS_TP_Devanado)
        {
            if (ModelState.IsValid)
            {
                db.ES_TP_Devanado.Add(eS_TP_Devanado);
                var tp = db.ES_TransformadorCorriente.Find(eS_TP_Devanado.Nro_TP);
                if (tp != null)
                {
                    tp.Cant_Devanado = eS_TP_Devanado.Nro_Dev;
                    db.Entry(tp).State = EntityState.Modified;
                }
                db.SaveChanges();
                //db.Database.ExecuteSqlCommandAsync("UPDATE ES_TransformadorCorriente SET Cant_Devanado = @Nro_Dev WHERE Nro_Serie = @Nro_TC",
                //                new SqlParameter("@Nro_TC", eS_TC_Devanado.Nro_TC),
                //                new SqlParameter("@Nro_Dev", eS_TC_Devanado.Nro_Dev));

                return RedirectToAction("Edit", "ES_TransformadorPotencial", new { Nro_Serie = eS_TP_Devanado.Nro_TP });
            }

            return View(eS_TP_Devanado);
        }

        // GET: ES_TP_Devanado/Edit/5
        public ActionResult Edit(string Nro_TP, short Nro_Dev)
        {
           
            if (Nro_TP == null && Nro_Dev == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ES_TP_Devanado eS_TP_Devanado = db.ES_TP_Devanado.Find(Nro_Dev, Nro_TP);
            if (eS_TP_Devanado == null)
            {
                return HttpNotFound();
            }
            var repoTC = new TCRepositorio(db);
            ViewBag.tension = repoTC.Tension(eS_TP_Devanado.Tension);
            return View(eS_TP_Devanado);
        }

        // POST: ES_TP_Devanado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nro_Dev,Nro_TP,id_Voltaje_Secundario,Designacion,ClasePresicion,Capacidad,Tension")] ES_TP_Devanado eS_TP_Devanado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eS_TP_Devanado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "ES_TransformadorPotencial", new { Nro_Serie = eS_TP_Devanado.Nro_TP });
            }
            return View(eS_TP_Devanado);
        }

        [HttpPost]
        public JsonResult Eliminar(string TP, int dev)
        {
            var cant = dev - 1;
            try
            {
                ES_TP_Devanado devanado = db.ES_TP_Devanado.Find(dev, TP);
                db.ES_TP_Devanado.Remove(devanado);
                var tp = db.ES_TransformadorPotencial.Find(TP);
                if (tp != null)
                {
                    tp.Cant_Devanado = (short)cant;
                    db.Entry(tp).State = EntityState.Modified;
                }
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: ES_TP_Devanado/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ES_TP_Devanado eS_TP_Devanado = db.ES_TP_Devanado.Find(id);
            if (eS_TP_Devanado == null)
            {
                return HttpNotFound();
            }
            return View(eS_TP_Devanado);
        }

        // POST: ES_TP_Devanado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            ES_TP_Devanado eS_TP_Devanado = db.ES_TP_Devanado.Find(id);
            db.ES_TP_Devanado.Remove(eS_TP_Devanado);
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
