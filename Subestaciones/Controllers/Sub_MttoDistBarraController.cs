using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class Sub_MttoDistBarraController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_MttoDistBarra
        public ActionResult Index()
        {
            return View(db.Sub_MttoDistBarra.ToList());
        }

        // GET: Sub_MttoDistBarra/Details/5
        public ActionResult Details(string subcodigo, DateTime fecha, string barra, string valor)
        {
            if ((subcodigo == null) && (fecha == null) && (barra == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMttoSubDistribucion mtto = db.SubMttoSubDistribucion.Find(subcodigo, fecha);

            var ListaBarra = new SubMttoSubDistRepositorio(db);
            var mttoBarra = ListaBarra.MttoBarra(subcodigo, fecha, barra).FirstOrDefault();
            //Sub_MttoDistBarra mttoBarra = db.Sub_MttoDistBarra.Find(subcodigo, fecha, barra);
            if (mttoBarra == null)
            {
                return HttpNotFound();
            }
            ViewBag.codSub = subcodigo;
            ViewBag.fechaMtto = fecha;
            ViewBag.valor = valor;
            return View(mttoBarra);
        }

        // GET: Sub_MttoDistBarra/Create
        public ActionResult Create(string sub, DateTime fecha, string valor)
        {
            ViewBag.codSub = sub;
            ViewBag.fechaMtto = fecha;
            ViewBag.valor = valor;

            var repoMtto = new SubMttoSubDistRepositorio(db);

            ViewBag.estBarra = repoMtto.BM();
            ViewBag.conex= repoMtto.BM();
            ViewBag.estPuentes = repoMtto.BM();

            return View();
        }

        // POST: Sub_MttoDistBarra/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoSub,Fecha,CodigoBarra,EstadoBarra,Conexiones,EstadoPuentes")] Sub_MttoDistBarra sub_MttoDistBarra)
        {
            if (ModelState.IsValid)
            {
                if (await ValidarExisteMtto(sub_MttoDistBarra.CodigoSub, sub_MttoDistBarra.Fecha, sub_MttoDistBarra.CodigoBarra))
                {
                    SubMttoSubDistribucion dmtto = db.SubMttoSubDistribucion.Find(sub_MttoDistBarra.CodigoSub, sub_MttoDistBarra.Fecha);

                    db.Sub_MttoDistBarra.Add(sub_MttoDistBarra);
                    db.SaveChanges(); if (Request.Form["submitButton"].ToString() == "Editar")
                    {
                        return RedirectToAction("Edit", "SubMttoSubDistribucions", new { codigo = dmtto.CodigoSub, fecha = dmtto.Fecha });

                    }
                    return RedirectToAction("ComponentesMtto", "SubMttoSubDistribucions", new { codigo = dmtto.CodigoSub, fecha = dmtto.Fecha });
                }
                else
                {
                    ModelState.AddModelError("barra", "Ya se le realizó un mantenimiento a la barra seleccionada en la fecha seleccionada.");

                }
            }
            ViewBag.codSub = sub_MttoDistBarra.CodigoSub;
            ViewBag.fechaMtto = sub_MttoDistBarra.Fecha;
            ViewBag.valor = Request.Form["submitButton"].ToString();

            var repoMtto = new SubMttoSubDistRepositorio(db);

            ViewBag.estBarra = repoMtto.BM();
            ViewBag.conex = repoMtto.BM();
            ViewBag.estPuentes = repoMtto.BM();
            return View(sub_MttoDistBarra);
        }

        private async Task<bool> ValidarExisteMtto(string Codigo, DateTime fecha, string barra)
        {
            Sub_MttoDistBarra mttoBarra = db.Sub_MttoDistBarra.Find(Codigo, fecha, barra);
            if (mttoBarra == null)
            {
                return true;
            }
            return false;

            //return !listaMttos.Select(c => new { c.CodigoSub, c.Fecha }).Any(c => c.CodigoSub == Codigo && c.Fecha == fecha);
        }


        // GET: Sub_MttoDistBarra/Edit/5
        public ActionResult Edit(string subcodigo, DateTime fecha, string barra, string valor)
        {
            if ((subcodigo == null) && (fecha == null) && (barra == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMttoSubDistribucion mtto = db.SubMttoSubDistribucion.Find(subcodigo, fecha);

            var ListaBarra = new SubMttoSubDistRepositorio(db);
            var mttoBarra = ListaBarra.MttoBarra(subcodigo, fecha, barra).FirstOrDefault();
            //Sub_MttoDistBarra mttoBarra = db.Sub_MttoDistBarra.Find(subcodigo, fecha, barra);
            if (mttoBarra == null)
            {
                return HttpNotFound();
            }
            ViewBag.codSub = subcodigo;
            ViewBag.fechaMtto = fecha;
            ViewBag.valor = valor;

            var repoMtto = new SubMttoSubDistRepositorio(db);

            ViewBag.estBarra = repoMtto.BM();
            ViewBag.conex = repoMtto.BM();
            ViewBag.estPuentes = repoMtto.BM();

            return View(mttoBarra);
        }

        // POST: Sub_MttoDistBarra/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoSub,Fecha,CodigoBarra,EstadoBarra,Conexiones,EstadoPuentes")] Sub_MttoDistBarra sub_MttoDistBarra)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(sub_MttoDistBarra).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.Form["submitButton"].ToString() == "Editar")
                {
                    return RedirectToAction("Edit", "SubMttoSubDistribucions", new { codigo = sub_MttoDistBarra.CodigoSub, fecha = sub_MttoDistBarra.Fecha });

                }
                return RedirectToAction("ComponentesMtto", "SubMttoSubDistribucions", new { codigo = sub_MttoDistBarra.CodigoSub, fecha = sub_MttoDistBarra.Fecha });

            }
            ViewBag.codSub = sub_MttoDistBarra.CodigoSub;
            ViewBag.fechaMtto = sub_MttoDistBarra.Fecha;
            ViewBag.valor = Request.Form["submitButton"].ToString();
            var repoMtto = new SubMttoSubDistRepositorio(db);

            ViewBag.estBarra = repoMtto.BM();
            ViewBag.conex = repoMtto.BM();
            ViewBag.estPuentes = repoMtto.BM();

            return View(sub_MttoDistBarra);
        }

        // GET: Sub_MttoDistBarra/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_MttoDistBarra sub_MttoDistBarra = db.Sub_MttoDistBarra.Find(id);
            if (sub_MttoDistBarra == null)
            {
                return HttpNotFound();
            }
            return View(sub_MttoDistBarra);
        }

        // POST: Sub_MttoDistBarra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sub_MttoDistBarra sub_MttoDistBarra = db.Sub_MttoDistBarra.Find(id);
            db.Sub_MttoDistBarra.Remove(sub_MttoDistBarra);
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

        public ActionResult EliminaMttoBarra(string cod, string idBarra, DateTime? fechaM)
        {
            try
            {
                Repositorio br = new Repositorio(db);

                Sub_MttoDistBarra mttoBarra = db.Sub_MttoDistBarra.Find(cod, fechaM, idBarra);
                //int accion = br.GetNumAccion("M", "SIA", mttoDesc.NumAccion ?? 0);
                db.Sub_MttoDistBarra.Remove(mttoBarra);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult _VPBarra(string sub)
        {

            var ListaBarra = new SubMttoSubDistRepositorio(db);
            var barras = ListaBarra.FindBarraEnSub(sub);

            ViewBag.listadoBarra = barras;
            ViewBag.cantB = barras.Count();

            return PartialView();
        }

      

        public JsonResult DatoBarra(string sub, string codBarra)
        {

            var ListaBarras = new BarraRepositorio(db);
            var barra = ListaBarras.FindAsync(sub, codBarra);

            return Json(barra, JsonRequestBehavior.AllowGet);
        }
    }
}
