using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class Sub_InspAspectosSubTransController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_InspAspectosSubTrans
        //public async Task<ActionResult> Index()
        //{
        //    var ListaInsps = new Sub_InspAspectosSubTransRepositorio(db);
        //    return View(await ListaInsps.ObtenerSub_InspAspectosSubTrans());
        //}

       

        // GET: Sub_InspAspectosSubTrans/Edit/5
        public ActionResult Edit( string nombInsp, string codigo, DateTime fechaIns, int aspecto, string NombreAspecto)
        {
            Repositorio repo = new Repositorio(db);
            var repoInsp = new Sub_InspAspectosSubTransRepositorio(db);
            Sub_InspAspectosSubTrans dinsp = db.Sub_InspAspectosSubTrans.Find(nombInsp, codigo, fechaIns, aspecto);

            if (dinsp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (dinsp == null)
            {
                return HttpNotFound();
            }

            ViewBag.nombreAspecto = NombreAspecto;
            ViewBag.fechaIns = fechaIns;
            ViewBag.nombreInsp = nombInsp;

            ViewBag.cant = 0;
            return View(dinsp);
        }

        // POST: Sub_InspAspectosSubTrans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NombreCelaje,CodigoSub,fecha,Aspecto,Defecto,Observaciones")] Sub_InspAspectosSubTrans sub_InspAspectosSubTrans)
        {
            Repositorio repo = new Repositorio(db);
            var repoInsp = new Sub_InspAspectosSubTransRepositorio(db);

            if (ModelState.IsValid)
            {
                Sub_InspAspectosSubTrans sub_Insp = db.Sub_InspAspectosSubTrans.SingleOrDefault(c => c.NombreCelaje.Equals(sub_InspAspectosSubTrans.NombreCelaje) && c.CodigoSub.Equals(sub_InspAspectosSubTrans.CodigoSub) && c.fecha.Equals(sub_InspAspectosSubTrans.fecha) && c.Aspecto.Equals(sub_InspAspectosSubTrans.Aspecto));

                sub_Insp.Aspecto = sub_InspAspectosSubTrans.Aspecto;
                sub_Insp.Defecto = sub_InspAspectosSubTrans.Defecto;
                sub_Insp.Observaciones = sub_InspAspectosSubTrans.Observaciones;


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            


            return View(sub_InspAspectosSubTrans);
        }

        // GET: Sub_InspAspectosSubTrans/Delete/5
       

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
