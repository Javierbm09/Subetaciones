using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;

namespace Subestaciones.Controllers
{
    public class ES_TC_DevanadoController : Controller
    {
        private DBContext db = new DBContext();

        // GET: ES_TC_Devanado
        public ActionResult Index()
        {
            return View(db.ES_TC_Devanado.ToList());
        }

        // GET: ES_TC_Devanado/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ES_TC_Devanado eS_TC_Devanado = db.ES_TC_Devanado.Find(id);
            if (eS_TC_Devanado == null)
            {
                return HttpNotFound();
            }
            return View(eS_TC_Devanado);
        }

        // GET: ES_TC_Devanado/Create
        public ActionResult Create(string NroSerie, short Cant)
        {
            //var Devanados = new List<ES_TC_Devanado>();
            //for (short i = 1; i <= Cant; i++)
            //{
            //    var Devanado = new ES_TC_Devanado { Nro_TC = NroSerie, Nro_Dev = i };
            //    Devanados.Add(Devanado);
            //}

            ViewBag.Cant = Cant;
            ViewBag.NroSerie = NroSerie;


            return View();
        }

        // POST: ES_TC_Devanado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //var nameTV = "NroSerie" + @i;
        //var Nro_Dev = "Nro_Dev" + @i;
        //var Capacidad = "Capacidad" + @i;

        //var Clase_Precision = "Clase_Precision" + @i;
        //var Designacion = "Designacion" + @i;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string NroSerie0 = null , string NroSerie1 = null, string NroSerie2 = null, short Nro_Dev0=0, short Nro_Dev1 = 0, short Nro_Dev2 = 0, double Capacidad0 =0
            , double Capacidad1 = 0, double Capacidad2 = 0, string Clase_Precision0 = null, string Clase_Precision1 = null, string Clase_Precision2 = null,
            string Designacion0 = null, string Designacion1 = null, string Designacion2 = null)
        {
            if (ModelState.IsValid)
            {               
                var eS_TC_Devanado1 = new ES_TC_Devanado ();
                eS_TC_Devanado1.Nro_Dev = Nro_Dev0;
                eS_TC_Devanado1.Nro_TC = NroSerie0;
                eS_TC_Devanado1.Capacidad = Capacidad0;
                eS_TC_Devanado1.Clase_Precision = Clase_Precision0;
                eS_TC_Devanado1.Designacion = Designacion0;

                db.ES_TC_Devanado.Add(eS_TC_Devanado1);                
                db.SaveChanges();

                if (NroSerie1 != null && Nro_Dev1 != 0)
                {
                    var eS_TC_Devanado2 = new ES_TC_Devanado();
                    eS_TC_Devanado2.Nro_Dev = Nro_Dev1;
                    eS_TC_Devanado2.Nro_TC = NroSerie1;
                    eS_TC_Devanado2.Capacidad = Capacidad1;
                    eS_TC_Devanado2.Clase_Precision = Clase_Precision1;
                    eS_TC_Devanado2.Designacion = Designacion1;
                    db.ES_TC_Devanado.Add(eS_TC_Devanado2);
                    db.SaveChanges();
                }

                if (NroSerie2 != null && Nro_Dev2 != 0)
                {
                    var eS_TC_Devanado3 = new ES_TC_Devanado();
                    eS_TC_Devanado3.Nro_Dev = Nro_Dev2;
                    eS_TC_Devanado3.Nro_TC = NroSerie2;
                    eS_TC_Devanado3.Capacidad = Capacidad2;
                    eS_TC_Devanado3.Clase_Precision = Clase_Precision2;
                    eS_TC_Devanado3.Designacion = Designacion2;
                    db.ES_TC_Devanado.Add(eS_TC_Devanado3);
                    db.SaveChanges();
                }

                //db.ES_TC_Devanado.Add(eS_TC_Devanado[0]);
                //db.SaveChanges();
                return RedirectToAction("Index","ES_TransformadorCorriente");
            }

            return View();
        }
        //para crear un solo devanado cuando es desde el editar TC
        public ActionResult CrearDevanado(string NroSerie, short Cant)
        {           
            ViewBag.Cant = Cant;
            ViewBag.NroSerie = NroSerie;

            var p = new ES_TC_Devanado();
            p.Nro_Dev = Cant;
            p.Nro_TC = NroSerie;

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearDevanado([Bind(Include = "Nro_Dev,Nro_TC,Capacidad,Clase_Precision,Designacion")] ES_TC_Devanado eS_TC_Devanado)
        {
            if (ModelState.IsValid)
            {
                db.ES_TC_Devanado.Add(eS_TC_Devanado);
                var tc = db.ES_TransformadorCorriente.Find(eS_TC_Devanado.Nro_TC);
                if (tc != null)
                {
                    tc.Cant_Devanado = eS_TC_Devanado.Nro_Dev;
                    db.Entry(tc).State = EntityState.Modified;
                }
                db.SaveChanges();
                //db.Database.ExecuteSqlCommandAsync("UPDATE ES_TransformadorCorriente SET Cant_Devanado = @Nro_Dev WHERE Nro_Serie = @Nro_TC",
                //                new SqlParameter("@Nro_TC", eS_TC_Devanado.Nro_TC),
                //                new SqlParameter("@Nro_Dev", eS_TC_Devanado.Nro_Dev));

                return RedirectToAction("Edit", "ES_TransformadorCorriente", new { Nro_Serie = eS_TC_Devanado.Nro_TC });
            }

            return View(eS_TC_Devanado);
        }

        [HttpPost]
        public ActionResult Insertar(List<ES_TC_Devanado> eS_TC_Devanado)
        {
            if (eS_TC_Devanado != null)
            {
               // db.ES_TC_Devanado.Add(eS_TC_Devanado1);
               // db.SaveChanges();
            }
            return View("Index","ES_TransformadorCorriente");
        }

        // GET: ES_TC_Devanado/Edit/5
        public ActionResult Edit(string Nro_TC, short Nro_Dev)
        {
            if (Nro_TC == null && Nro_Dev == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ES_TC_Devanado eS_TC_Devanado = db.ES_TC_Devanado.Find(Nro_Dev, Nro_TC);
            if (eS_TC_Devanado == null)
            {
                return HttpNotFound();
            }
            return View(eS_TC_Devanado);
        }

        // POST: ES_TC_Devanado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nro_Dev,Nro_TC,Capacidad,Clase_Precision,Designacion")] ES_TC_Devanado eS_TC_Devanado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eS_TC_Devanado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "ES_TransformadorCorriente", new { Nro_Serie = eS_TC_Devanado.Nro_TC });
            }
            return View(eS_TC_Devanado);
        }

        [HttpPost]
        public JsonResult Eliminar( string TC,  int dev)
        {
            var cant = dev - 1;
            try
            {
                ES_TC_Devanado devanado = db.ES_TC_Devanado.Find(dev, TC);
                db.ES_TC_Devanado.Remove(devanado);
                var tc = db.ES_TransformadorCorriente.Find(TC);
                if (tc != null)
                {
                    tc.Cant_Devanado = (short)cant;
                    db.Entry(tc).State = EntityState.Modified;
                }
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: ES_TC_Devanado/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ES_TC_Devanado eS_TC_Devanado = db.ES_TC_Devanado.Find(id);
            if (eS_TC_Devanado == null)
            {
                return HttpNotFound();
            }
            return View(eS_TC_Devanado);
        }

        // POST: ES_TC_Devanado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            ES_TC_Devanado eS_TC_Devanado = db.ES_TC_Devanado.Find(id);
            db.ES_TC_Devanado.Remove(eS_TC_Devanado);
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
