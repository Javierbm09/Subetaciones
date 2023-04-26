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
    public class SubTermometrosTransfSubTransmisionsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: SubTermometrosTransfSubTransmisions
        public ActionResult Index()
        {
            return View(db.SubTermometrosTransfSubTransmision.ToList());
        }

        // GET: SubTermometrosTransfSubTransmisions/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubTermometrosTransfSubTransmision subTermometrosTransfSubTransmision = db.SubTermometrosTransfSubTransmision.Find(id);
            if (subTermometrosTransfSubTransmision == null)
            {
                return HttpNotFound();
            }
            return View(subTermometrosTransfSubTransmision);
        }

        // GET: SubTermometrosTransfSubTransmisions/Create
        public ActionResult Create(int EA, int id_t, short? id, int NumAccion, string ubicado)
        {
            ViewBag.ubicado = ubicado;
            SubTermometrosTransfSubTransmision termo = new SubTermometrosTransfSubTransmision { Id_EAdministrativa = EA,Id_Transformador = id_t, NumAccion = NumAccion };
            return View(termo);
        }

        // POST: SubTermometrosTransfSubTransmisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_EAdministrativa,Id_Transformador,NumAccion,Numero,Tipo,Rango")] SubTermometrosTransfSubTransmision subTermometrosTransfSubTransmision, string ubicado)
        {
            if (ModelState.IsValid)
            {
               // ViewBag.ubicado = ubicado;
                db.SubTermometrosTransfSubTransmision.Add(subTermometrosTransfSubTransmision);
                db.SaveChanges();
                return RedirectToAction("Edit", "TransformadoresSubtransmisions", new { EA = subTermometrosTransfSubTransmision.Id_EAdministrativa, id_transformador = subTermometrosTransfSubTransmision.Id_Transformador, ubicado = ubicado });

            }

            return View(subTermometrosTransfSubTransmision);
        }

        // GET: SubTermometrosTransfSubTransmisions/Edit/5
        public ActionResult Edit( int EA, int id_t,short? id, int NumAccion, string ubicado)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubTermometrosTransfSubTransmision subTermometrosTransfSubTransmision = db.SubTermometrosTransfSubTransmision.Find(EA,id_t, id, NumAccion);
            if (subTermometrosTransfSubTransmision == null)
            {
                return HttpNotFound();
            }
            ViewBag.ubicado = ubicado;
            return View(subTermometrosTransfSubTransmision);
        }

        // POST: SubTermometrosTransfSubTransmisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_EAdministrativa,Id_Transformador,Id_Termometro,NumAccion,Numero,Tipo,Rango")] SubTermometrosTransfSubTransmision subTermometrosTransfSubTransmision, string ubicado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subTermometrosTransfSubTransmision).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "TransformadoresSubtransmisions", new { EA = subTermometrosTransfSubTransmision.Id_EAdministrativa, id_transformador = subTermometrosTransfSubTransmision.Id_Transformador, ubicado = ubicado });

            }
            return View(subTermometrosTransfSubTransmision);
        }

        // GET: SubTermometrosTransfSubTransmisions/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubTermometrosTransfSubTransmision subTermometrosTransfSubTransmision = db.SubTermometrosTransfSubTransmision.Find(id);
            if (subTermometrosTransfSubTransmision == null)
            {
                return HttpNotFound();
            }
            return View(subTermometrosTransfSubTransmision);
        }

        // POST: SubTermometrosTransfSubTransmisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            SubTermometrosTransfSubTransmision subTermometrosTransfSubTransmision = db.SubTermometrosTransfSubTransmision.Find(id);
            db.SubTermometrosTransfSubTransmision.Remove(subTermometrosTransfSubTransmision);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Eliminar(int EA, int id_t, short? id, int NumAccion)
        {
           
                try
                {
                    SubTermometrosTransfSubTransmision termo = db.SubTermometrosTransfSubTransmision.Find(EA, id_t, id, NumAccion);
                    db.SubTermometrosTransfSubTransmision.Remove(termo);
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
