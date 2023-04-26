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
    public class Inst_Nomenclador_DesconectivosController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Inst_Nomenclador_Desconectivos
        public ActionResult Index()
        {
            return View(db.Inst_Nomenclador_Desconectivos.ToList());
        }

        // GET: Inst_Nomenclador_Desconectivos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_Desconectivos inst_Nomenclador_Desconectivos = db.Inst_Nomenclador_Desconectivos.Find(id);
            if (inst_Nomenclador_Desconectivos == null)
            {
                return HttpNotFound();
            }
            return View(inst_Nomenclador_Desconectivos);
        }

        // GET: Inst_Nomenclador_Desconectivos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inst_Nomenclador_Desconectivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Nomenclador,Descripcion,Id_Administrativa,NumAccion,id_EAdministrativa_Prov,Id_ModeloDesconectivo,Id_Fabricante,Id_Tension,Id_Corriente,Id_Cortocircuito,Id_ApertCable,Id_Bil,Id_SecuenciaOperacion,Id_MedioExtinsion,Id_Aislamiento,Id_PresionGas,PesoGas,PesoInterruptor,PesoGabinete,PesoTotal,Tanque")] Inst_Nomenclador_Desconectivos inst_Nomenclador_Desconectivos)
        {
            if (ModelState.IsValid)
            {
                db.Inst_Nomenclador_Desconectivos.Add(inst_Nomenclador_Desconectivos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inst_Nomenclador_Desconectivos);
        }

        // GET: Inst_Nomenclador_Desconectivos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_Desconectivos inst_Nomenclador_Desconectivos = db.Inst_Nomenclador_Desconectivos.Find(id);
            if (inst_Nomenclador_Desconectivos == null)
            {
                return HttpNotFound();
            }
            return View(inst_Nomenclador_Desconectivos);
        }

        // POST: Inst_Nomenclador_Desconectivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Nomenclador,Descripcion,Id_Administrativa,NumAccion,id_EAdministrativa_Prov,Id_ModeloDesconectivo,Id_Fabricante,Id_Tension,Id_Corriente,Id_Cortocircuito,Id_ApertCable,Id_Bil,Id_SecuenciaOperacion,Id_MedioExtinsion,Id_Aislamiento,Id_PresionGas,PesoGas,PesoInterruptor,PesoGabinete,PesoTotal,Tanque")] Inst_Nomenclador_Desconectivos inst_Nomenclador_Desconectivos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inst_Nomenclador_Desconectivos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inst_Nomenclador_Desconectivos);
        }

        // GET: Inst_Nomenclador_Desconectivos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_Desconectivos inst_Nomenclador_Desconectivos = db.Inst_Nomenclador_Desconectivos.Find(id);
            if (inst_Nomenclador_Desconectivos == null)
            {
                return HttpNotFound();
            }
            return View(inst_Nomenclador_Desconectivos);
        }

        // POST: Inst_Nomenclador_Desconectivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inst_Nomenclador_Desconectivos inst_Nomenclador_Desconectivos = db.Inst_Nomenclador_Desconectivos.Find(id);
            db.Inst_Nomenclador_Desconectivos.Remove(inst_Nomenclador_Desconectivos);
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
