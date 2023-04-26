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
    public class Emplazamiento_SigereController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Emplazamiento_Sigere
        public ActionResult Index(string sub)
        {
            return View(db.Emplazamiento_Sigere.ToList().Where(c => c.CentroTransformacion == sub));
        }

        // GET: Emplazamiento_Sigere/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emplazamiento_Sigere emplazamiento_Sigere = db.Emplazamiento_Sigere.Find(id);
            if (emplazamiento_Sigere == null)
            {
                return HttpNotFound();
            }
            return View(emplazamiento_Sigere);
        }

        // GET: Emplazamiento_Sigere/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emplazamiento_Sigere/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre,Tipo,Provincia,Fuente_Energia,Id_EAdministrativa,NumAccion,Calle,Numero,Entrecalle1,Entrecalle2,BarrioPueblo,Sucursal,CentroTransformacion,Id_EAdireccion,coddireccion,tipo_fuelCTE,planta_pico,centg")] Emplazamiento_Sigere emplazamiento_Sigere)
        {
            if (ModelState.IsValid)
            {
                db.Emplazamiento_Sigere.Add(emplazamiento_Sigere);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emplazamiento_Sigere);
        }

        // GET: Emplazamiento_Sigere/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emplazamiento_Sigere emplazamiento_Sigere = db.Emplazamiento_Sigere.Find(id);
            if (emplazamiento_Sigere == null)
            {
                return HttpNotFound();
            }
            return View(emplazamiento_Sigere);
        }

        // POST: Emplazamiento_Sigere/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre,Tipo,Provincia,Fuente_Energia,Id_EAdministrativa,NumAccion,Calle,Numero,Entrecalle1,Entrecalle2,BarrioPueblo,Sucursal,CentroTransformacion,Id_EAdireccion,coddireccion,tipo_fuelCTE,planta_pico,centg")] Emplazamiento_Sigere emplazamiento_Sigere)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emplazamiento_Sigere).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emplazamiento_Sigere);
        }

        // GET: Emplazamiento_Sigere/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emplazamiento_Sigere emplazamiento_Sigere = db.Emplazamiento_Sigere.Find(id);
            if (emplazamiento_Sigere == null)
            {
                return HttpNotFound();
            }
            return View(emplazamiento_Sigere);
        }

        // POST: Emplazamiento_Sigere/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Emplazamiento_Sigere emplazamiento_Sigere = db.Emplazamiento_Sigere.Find(id);
            db.Emplazamiento_Sigere.Remove(emplazamiento_Sigere);
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
