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
    public class SubTermometrosTransfTransmisionsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: SubTermometrosTransfTransmisions
        public ActionResult Index()
        {
            return View(db.SubTermometrosTransfTransmision.ToList());
        }

        // GET: SubTermometrosTransfTransmisions/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubTermometrosTransfTransmision subTermometrosTransfTransmision = db.SubTermometrosTransfTransmision.Find(id);
            if (subTermometrosTransfTransmision == null)
            {
                return HttpNotFound();
            }
            return View(subTermometrosTransfTransmision);
        }

        // GET: SubTermometrosTransfTransmisions/Create
        public ActionResult Create(int EA, int id_t, short? id, int NumAccion, string ubicado)
        {
            SubTermometrosTransfTransmision termo = new SubTermometrosTransfTransmision { Id_EAdministrativa = EA, Id_Transformador = id_t, NumAccion = NumAccion };
            return View(termo);
        }

        // POST: SubTermometrosTransfTransmisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_EAdministrativa,Id_Transformador,Id_Termometro,NumAccion,Numero,Tipo,Rango")] SubTermometrosTransfTransmision subTermometrosTransfTransmision, string ubicado)
        {
            if (ModelState.IsValid)
            {
                db.SubTermometrosTransfTransmision.Add(subTermometrosTransfTransmision);
                db.SaveChanges();
                return RedirectToAction("Edit", "TransformadoresTransmisions", new { EA = subTermometrosTransfTransmision.Id_EAdministrativa, id_transformador = subTermometrosTransfTransmision.Id_Transformador, ubicado = ubicado });
            }

            return View(subTermometrosTransfTransmision);
        }

        // GET: SubTermometrosTransfTransmisions/Edit/5
        public ActionResult Edit(int EA, int id_t, short? id, int NumAccion, string ubicado)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubTermometrosTransfTransmision subTermometrosTransfTransmision = db.SubTermometrosTransfTransmision.Find(EA, id_t, id, NumAccion);
            if (subTermometrosTransfTransmision == null)
            {
                return HttpNotFound();
            }
            ViewBag.ubicado = ubicado;
            return View(subTermometrosTransfTransmision);
        }

        // POST: SubTermometrosTransfTransmisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_EAdministrativa,Id_Transformador,Id_Termometro,NumAccion,Numero,Tipo,Rango")] SubTermometrosTransfTransmision subTermometrosTransfTransmision, string ubicado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subTermometrosTransfTransmision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "TransformadoresTransmisions", new { EA = subTermometrosTransfTransmision.Id_EAdministrativa, id_transformador = subTermometrosTransfTransmision.Id_Transformador, ubicado = ubicado });

            }
            return View(subTermometrosTransfTransmision);
        }

        // GET: SubTermometrosTransfTransmisions/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubTermometrosTransfTransmision subTermometrosTransfTransmision = db.SubTermometrosTransfTransmision.Find(id);
            if (subTermometrosTransfTransmision == null)
            {
                return HttpNotFound();
            }
            return View(subTermometrosTransfTransmision);
        }

        // POST: SubTermometrosTransfTransmisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            SubTermometrosTransfTransmision subTermometrosTransfTransmision = db.SubTermometrosTransfTransmision.Find(id);
            db.SubTermometrosTransfTransmision.Remove(subTermometrosTransfTransmision);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Eliminar(int EA, int id_t, short? id, int NumAccion)
        {

            try
            {
                SubTermometrosTransfTransmision termo = db.SubTermometrosTransfTransmision.Find(EA, id_t, id, NumAccion);
                db.SubTermometrosTransfTransmision.Remove(termo);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

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
