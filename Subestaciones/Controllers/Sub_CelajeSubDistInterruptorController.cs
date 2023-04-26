using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;
using Subestaciones.Models.Clases;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class Sub_CelajeSubDistInterruptorController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_CelajeSubDistInterruptor
        public ActionResult Index()
        {
            return View(db.Sub_CelajeSubDistInterruptor.ToList());
        }

        // GET: Sub_CelajeSubDistInterruptor/Details/5
        public ActionResult Details(string nomb, string Codigo, DateTime fecha, string interp, string valor)
        {
            if ((nomb == null) && (Codigo == null) && (fecha == null) && (interp == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Sub_CelajeSubDistInterruptor sub_CelajeSubDistInterruptor = db.Sub_CelajeSubDistInterruptor.Find(interp, nomb, Codigo, fecha);

            var ListaInt = new InspSubDistRepo(db);

            var inspTF = ListaInt.FindInspInterr( nomb, interp, Codigo, fecha).FirstOrDefault();


            if (inspTF == null)
            {
                return HttpNotFound();
            }
        
            ViewBag.nomb = nomb;
            ViewBag.codSub = Codigo;
            ViewBag.fechaInsp = fecha;
            ViewBag.valor = valor;

            return View(inspTF);
         }

        // GET: Sub_CelajeSubDistInterruptor/Create
        public ActionResult Create(string nombreCelaje, string sub, DateTime? fechaIns, string valor)
        {

            var repoInsp = new InspSubDistRepo(db);

            ViewBag.nomb = nombreCelaje;
            ViewBag.codSub = sub;
            ViewBag.fechaInsp = fechaIns;
            ViewBag.valor = valor;

            ViewBag.salideros = repoInsp.SN();
            ViewBag.aceite = repoInsp.nivelAceite();
            ViewBag.pintura = repoInsp.estados();
            ViewBag.bat = repoInsp.estadoOtrosInt();
            ViewBag.gab = repoInsp.estadoOtrosInt();
            return View();
        }

        // POST: Sub_CelajeSubDistInterruptor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoInterruptor,nombreCelaje,codigoSub,fecha,Salidero,NAceite,Pintura,cuentaOP,estadoBateria,candadoGabinete,tipoInterruptor,id_EAdministrativa")] Sub_CelajeSubDistInterruptor sub_CelajeSubDistInterruptor)
        {
            if (ModelState.IsValid)
            {
                if (ValidarExisteInsp(sub_CelajeSubDistInterruptor.codigoInterruptor,sub_CelajeSubDistInterruptor.nombreCelaje,sub_CelajeSubDistInterruptor.codigoSub, sub_CelajeSubDistInterruptor.fecha))
                {

                    db.Sub_CelajeSubDistInterruptor.Add(sub_CelajeSubDistInterruptor);
                    db.SaveChanges();
                    if (Request.Form["submitButton"].ToString() == "Editar")
                    {
                        return RedirectToAction("Edit", "Sub_CelajeSubDistribucion", new { nombInsp = sub_CelajeSubDistInterruptor.nombreCelaje, codigo = sub_CelajeSubDistInterruptor.codigoSub, fechaIns = sub_CelajeSubDistInterruptor.fecha });

                    }
                    return RedirectToAction("ComponentesInsp", "Sub_CelajeSubDistribucion", new {nombInsp = sub_CelajeSubDistInterruptor.nombreCelaje, codigo = sub_CelajeSubDistInterruptor.codigoSub, fecha = sub_CelajeSubDistInterruptor.fecha });

                }
                else
                {
                    ModelState.AddModelError("Codigo", "Ya se le realizó un mantenimiento al interruptor seleccionado en la fecha seleccionada.");

                }

                
            }

            var repoInsp = new InspSubDistRepo(db);

            ViewBag.nomb = sub_CelajeSubDistInterruptor.nombreCelaje;
            ViewBag.codSub = sub_CelajeSubDistInterruptor.codigoSub;
            ViewBag.fechaInsp = sub_CelajeSubDistInterruptor.fecha;
            ViewBag.valor = Request.Form["submitButton"].ToString();

            ViewBag.salideros = repoInsp.SN();
            ViewBag.aceite = repoInsp.nivelAceite();
            ViewBag.pintura = repoInsp.estados();
            ViewBag.bat = repoInsp.estadoOtrosInt();
            ViewBag.gab = repoInsp.estadoOtrosInt();

            return View(sub_CelajeSubDistInterruptor);
        }

            private bool ValidarExisteInsp(string codInt, string nomb, string Codigo, DateTime fecha)
            {
                Sub_CelajeSubDistInterruptor mttoDesc = db.Sub_CelajeSubDistInterruptor.Find(codInt,nomb, Codigo, fecha);
                if (mttoDesc == null)
                {
                    return true;
                }
                return false;

                //return !listaMttos.Select(c => new { c.CodigoSub, c.Fecha }).Any(c => c.CodigoSub == Codigo && c.Fecha == fecha);
            }


            // GET: Sub_CelajeSubDistInterruptor/Edit/5
            public ActionResult Edit(string nomb, string Codigo, DateTime fecha, string interp, string valor)
        {
            if ((nomb == null) && (Codigo == null) && (fecha == null) && (interp == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaInt = new InspSubDistRepo(db);

            var inspTF = ListaInt.FindInspInterr(nomb, interp, Codigo, fecha).FirstOrDefault();
            if (inspTF == null)
            {
                return HttpNotFound();
            }

            ViewBag.nomb = nomb;
            ViewBag.codSub = Codigo;
            ViewBag.fechaInsp = fecha;
            ViewBag.valor = valor;

            var repoInsp = new InspSubDistRepo(db);

            ViewBag.salideros = repoInsp.SN();
            ViewBag.aceite = repoInsp.nivelAceite();
            ViewBag.pintura = repoInsp.estados();
            ViewBag.bat = repoInsp.estadoOtrosInt();
            ViewBag.gab = repoInsp.estadoOtrosInt();

            return View(inspTF);
        }

        // POST: Sub_CelajeSubDistInterruptor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoInterruptor,nombreCelaje,codigoSub,fecha,Salidero,NAceite,Pintura,cuentaOP,estadoBateria,candadoGabinete,tipoInterruptor,id_EAdministrativa")] Sub_CelajeSubDistInterruptor sub_CelajeSubDistInterruptor)
        {
            
            if (ModelState.IsValid)
            {
                
                db.Entry(sub_CelajeSubDistInterruptor).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.Form["submitButton"].ToString() == "Editar")
                {
                    return RedirectToAction("Edit", "Sub_CelajeSubDistribucion", new { nombInsp = sub_CelajeSubDistInterruptor.nombreCelaje, codigo = sub_CelajeSubDistInterruptor.codigoSub, fechaIns = sub_CelajeSubDistInterruptor.fecha });

                }
                return RedirectToAction("ComponentesInsp", "Sub_CelajeSubDistribucion", new { nombInsp = sub_CelajeSubDistInterruptor.nombreCelaje, codigo = sub_CelajeSubDistInterruptor.codigoSub, fecha = sub_CelajeSubDistInterruptor.fecha });

            }

            ViewBag.nomb = sub_CelajeSubDistInterruptor.nombreCelaje;
            ViewBag.codSub = sub_CelajeSubDistInterruptor.codigoSub;
            ViewBag.fechaInsp = sub_CelajeSubDistInterruptor.fecha;
            ViewBag.valor = Request.Form["submitButton"].ToString();

            var repoInsp = new InspSubDistRepo(db);

            ViewBag.salideros = repoInsp.SN();
            ViewBag.aceite = repoInsp.nivelAceite();
            ViewBag.pintura = repoInsp.estados();
            ViewBag.bat = repoInsp.estadoOtrosInt();
            ViewBag.gab = repoInsp.estadoOtrosInt();

            return View(sub_CelajeSubDistInterruptor);
        }

        // GET: Sub_CelajeSubDistInterruptor/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_CelajeSubDistInterruptor sub_CelajeSubDistInterruptor = db.Sub_CelajeSubDistInterruptor.Find(id);
            if (sub_CelajeSubDistInterruptor == null)
            {
                return HttpNotFound();
            }
            return View(sub_CelajeSubDistInterruptor);
        }

        // POST: Sub_CelajeSubDistInterruptor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sub_CelajeSubDistInterruptor sub_CelajeSubDistInterruptor = db.Sub_CelajeSubDistInterruptor.Find(id);
            db.Sub_CelajeSubDistInterruptor.Remove(sub_CelajeSubDistInterruptor);
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

        public ActionResult _VPInter(string sub)
        {

            var ListaDesc = new InspSubDistRepo(db);
            var desconectivos = ListaDesc.FindInterruptorSub(sub);

            ViewBag.listadoDesc = desconectivos;
            ViewBag.cantD = desconectivos.Count();

            return PartialView();
        }

        public JsonResult DatoDesc(string codDesc)
        {

            var ListaDesconectivos = new DesconectivoSubestacionesRepositorio(db);
            var desc = ListaDesconectivos.FindDesc(codDesc);

            return Json(desc, JsonRequestBehavior.AllowGet);
        }

    }
}
