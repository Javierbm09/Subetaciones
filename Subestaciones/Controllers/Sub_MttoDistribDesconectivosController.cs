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
    public class Sub_MttoDistribDesconectivosController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_MttoDistribDesconectivos
        public ActionResult Index()
        {
            return View(db.Sub_MttoDistribDesconectivos.ToList());
        }

        // GET: Sub_MttoDistribDesconectivos/Details/5
        public ActionResult Details(string subcodigo, DateTime fecha, string codDes, string valor)
        {
            if ((subcodigo == null) && (fecha == null) && (codDes == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMttoSubDistribucion mtto = db.SubMttoSubDistribucion.Find(subcodigo, fecha);
            string tipo = db.InstalacionDesconectivos.Where((c => c.Codigo == codDes && c.id_EAdministrativa_Prov == mtto.Id_EAdministrativa)).Select(c => c.TipoSeccionalizador).FirstOrDefault();

            //InstalacionDesconectivos desc = db.InstalacionDesconectivos.Find(codDes, mtto.Id_EAdministrativa);
            Sub_MttoDistribDesconectivos sub_MttoDistribDesconectivos = db.Sub_MttoDistribDesconectivos.Find(subcodigo, fecha, codDes);
            if (sub_MttoDistribDesconectivos == null)
            {
                return HttpNotFound();
            }
            ViewBag.tipoDesc = tipo;
            ViewBag.codSub = subcodigo;
            ViewBag.fechaMtto = fecha;
            ViewBag.valor = valor;
            return View(sub_MttoDistribDesconectivos);
        }

        // GET: Sub_MttoDistribDesconectivos/Create
        public ActionResult Create(string sub, DateTime fecha, int ea, string valor)
        {
            ViewBag.codSub = sub;
            ViewBag.fechaMtto = fecha;
            ViewBag.eaMtto = ea;
            ViewBag.valor = valor;

            var repoMtto = new SubMttoSubDistRepositorio(db);

            //Interruptor/recerrador
            ViewBag.limpTanque = repoMtto.BM();
            ViewBag.limpGab = repoMtto.BM();
            ViewBag.pintura = repoMtto.BM();
            ViewBag.aterrTanque = repoMtto.aterramientos();
            ViewBag.aterrGab = repoMtto.aterramientos();
            ViewBag.presion = repoMtto.nivel();
            ViewBag.pruebas = repoMtto.pruebasRealizadas();
            ViewBag.rotulos = repoMtto.BM();
            //cuchilla
            ViewBag.limpAisl = repoMtto.BM();
            ViewBag.limpCont = repoMtto.BM();
            ViewBag.pinturaCuchilla = repoMtto.BM();
            ViewBag.aterr = repoMtto.aterramientos();
            ViewBag.mttoMando = repoMtto.pruebasRealizadas();
            //portafusible
            ViewBag.verifCap = repoMtto.verif();
            return View();
        }

        // POST: Sub_MttoDistribDesconectivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoSub,Fecha,CodigoDesc,EstLimpiezaTanque,EstLimpiezaGabinete,EstPintura,EstAterramTanque,EstAterramGabinete,PresionSF6,NroOperaciones,PorcDesgasteFaseA,PorcDesgasteFaseB,PorcDesgasteFaseC,PruebasFuncionales,EstRotulos,ResistContactoFaseA,ResistContactoFaseB,ResistContactoFaseC,ResistAislamFaseAET,ResistAislamFaseBET,ResistAislamFaseCET,ResistAislamFaseAST,ResistAislamFaseBST,ResistAislamFaseCST,ResistAislamFaseAEST,ResistAislamFaseBEST,ResistAislamFaseCEST,Ovservaciones,LimpiezaAislamiento,LimpiezaContactos,Pintura,MttoMando,Aterramiento,VerificacionCapacidad")] Sub_MttoDistribDesconectivos dmttosDesc)
        {
            if (ModelState.IsValid)
            {
                if (await ValidarExisteMtto(dmttosDesc.CodigoSub, dmttosDesc.Fecha, dmttosDesc.CodigoDesc))
                {
                    SubMttoSubDistribucion dmtto = db.SubMttoSubDistribucion.Find(dmttosDesc.CodigoSub, dmttosDesc.Fecha);

                    db.Sub_MttoDistribDesconectivos.Add(dmttosDesc);
                    db.SaveChanges();
                    if (Request.Form["submitButton"].ToString() == "Editar")
                    {
                        return RedirectToAction("Edit", "SubMttoSubDistribucions", new { codigo = dmttosDesc.CodigoSub, fecha = dmttosDesc.Fecha });

                    }
                    return RedirectToAction("ComponentesMtto", "SubMttoSubDistribucions", new { codigo = dmtto.CodigoSub, fecha = dmtto.Fecha });
                }
                else
                {
                    ModelState.AddModelError("Codigo", "Ya se le realizó un mantenimiento al desconectivo seleccionado en la fecha seleccionada.");

                }
            }
            ViewBag.valor = Request.Form["submitButton"].ToString();

            var repoMtto = new SubMttoSubDistRepositorio(db);
            ViewBag.codSub = dmttosDesc.CodigoSub;
            ViewBag.fechaMtto = dmttosDesc.Fecha;
            ViewBag.eaMtto = dmttosDesc.CodigoDesc;
            //Interruptor/recerrador
            ViewBag.limpTanque = repoMtto.BM();
            ViewBag.limpGab = repoMtto.BM();
            ViewBag.pintura = repoMtto.BM();
            ViewBag.aterrTanque = repoMtto.aterramientos();
            ViewBag.aterrGab = repoMtto.aterramientos();
            ViewBag.presion = repoMtto.nivel();
            ViewBag.pruebas = repoMtto.pruebasRealizadas();
            ViewBag.rotulos = repoMtto.BM();
            //cuchilla
            ViewBag.limpAisl = repoMtto.BM();
            ViewBag.limpCont = repoMtto.BM();
            ViewBag.pinturaCuchilla = repoMtto.BM();
            ViewBag.aterr = repoMtto.aterramientos();
            ViewBag.mttoMando = repoMtto.pruebasRealizadas();
            //portafusible
            ViewBag.verifCap = repoMtto.verif();
            return View(dmttosDesc);
        }



        // GET: Sub_MttoDistribDesconectivos/Edit/5
        public ActionResult Edit(string subcodigo, DateTime fecha, string codDes, string valor)
        {
            if ((subcodigo == null) && (fecha == null) && (codDes == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMttoSubDistribucion mtto = db.SubMttoSubDistribucion.Find(subcodigo, fecha);
            string tipo = db.InstalacionDesconectivos.Where((c => c.Codigo == codDes && c.id_EAdministrativa_Prov == mtto.Id_EAdministrativa)).Select(c => c.TipoSeccionalizador).FirstOrDefault();

            //InstalacionDesconectivos desc = db.InstalacionDesconectivos.Find(codDes, mtto.Id_EAdministrativa);
            Sub_MttoDistribDesconectivos sub_MttoDistribDesconectivos = db.Sub_MttoDistribDesconectivos.Find(subcodigo, fecha, codDes);
            if (sub_MttoDistribDesconectivos == null)
            {
                return HttpNotFound();
            }
            ViewBag.tipoDesc = tipo;
            ViewBag.codSub = subcodigo;
            ViewBag.fechaMtto = fecha;
            ViewBag.valor = valor;


            var repoMtto = new SubMttoSubDistRepositorio(db);

            //Interruptor/recerrador
            ViewBag.limpTanque = repoMtto.BM();
            ViewBag.limpGab = repoMtto.BM();
            ViewBag.pintura = repoMtto.BM();
            ViewBag.aterrTanque = repoMtto.aterramientos();
            ViewBag.aterrGab = repoMtto.aterramientos();
            ViewBag.presion = repoMtto.nivel();
            ViewBag.pruebas = repoMtto.pruebasRealizadas();
            ViewBag.rotulos = repoMtto.BM();
            //cuchilla
            ViewBag.limpAisl = repoMtto.BM();
            ViewBag.limpCont = repoMtto.BM();
            ViewBag.pinturaCuchilla = repoMtto.BM();
            ViewBag.aterr = repoMtto.aterramientos();
            ViewBag.mttoMando = repoMtto.pruebasRealizadas();
            //portafusible
            ViewBag.verifCap = repoMtto.verif();
            return View(sub_MttoDistribDesconectivos);
        }

        // POST: Sub_MttoDistribDesconectivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoSub,Fecha,CodigoDesc,EstLimpiezaTanque,EstLimpiezaGabinete,EstPintura,EstAterramTanque,EstAterramGabinete,PresionSF6,NroOperaciones,PorcDesgasteFaseA,PorcDesgasteFaseB,PorcDesgasteFaseC,PruebasFuncionales,EstRotulos,ResistContactoFaseA,ResistContactoFaseB,ResistContactoFaseC,ResistAislamFaseAET,ResistAislamFaseBET,ResistAislamFaseCET,ResistAislamFaseAST,ResistAislamFaseBST,ResistAislamFaseCST,ResistAislamFaseAEST,ResistAislamFaseBEST,ResistAislamFaseCEST,Ovservaciones,LimpiezaAislamiento,LimpiezaContactos,Pintura,MttoMando,Aterramiento,VerificacionCapacidad")] Sub_MttoDistribDesconectivos dmttosDesc)
        {
            if (ModelState.IsValid)
            {
                SubMttoSubDistribucion dmtto = db.SubMttoSubDistribucion.Find(dmttosDesc.CodigoSub, dmttosDesc.Fecha);

                db.Entry(dmttosDesc).State = EntityState.Modified;
                if (Request.Form["submitButton"].ToString() == "Editar")
                {
                    return RedirectToAction("Edit", "SubMttoSubDistribucions", new { codigo = dmttosDesc.CodigoSub, fecha = dmttosDesc.Fecha });

                }
                db.SaveChanges();
                return RedirectToAction("ComponentesMtto", "SubMttoSubDistribucions", new { codigo = dmtto.CodigoSub, fecha = dmtto.Fecha });
            }
            ViewBag.valor = Request.Form["submitButton"].ToString();

            var repoMtto = new SubMttoSubDistRepositorio(db);
            ViewBag.codSub = dmttosDesc.CodigoSub;
            ViewBag.fechaMtto = dmttosDesc.Fecha;
            ViewBag.eaMtto = dmttosDesc.CodigoDesc;
            //Interruptor/recerrador
            ViewBag.limpTanque = repoMtto.BM();
            ViewBag.limpGab = repoMtto.BM();
            ViewBag.pintura = repoMtto.BM();
            ViewBag.aterrTanque = repoMtto.aterramientos();
            ViewBag.aterrGab = repoMtto.aterramientos();
            ViewBag.presion = repoMtto.nivel();
            ViewBag.pruebas = repoMtto.pruebasRealizadas();
            ViewBag.rotulos = repoMtto.BM();
            //cuchilla
            ViewBag.limpAisl = repoMtto.BM();
            ViewBag.limpCont = repoMtto.BM();
            ViewBag.pinturaCuchilla = repoMtto.BM();
            ViewBag.aterr = repoMtto.aterramientos();
            ViewBag.mttoMando = repoMtto.pruebasRealizadas();
            //portafusible
            ViewBag.verifCap = repoMtto.verif();
            return View(dmttosDesc);
        }

        private async Task<bool> ValidarExisteMtto(string Codigo, DateTime fecha, string desc)
        {
            Sub_MttoDistribDesconectivos mttoDesc = db.Sub_MttoDistribDesconectivos.Find(Codigo, fecha, desc);
            if (mttoDesc == null)
            {
                return true;
            }
            return false;

            //return !listaMttos.Select(c => new { c.CodigoSub, c.Fecha }).Any(c => c.CodigoSub == Codigo && c.Fecha == fecha);
        }

        // GET: Sub_MttoDistribDesconectivos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_MttoDistribDesconectivos sub_MttoDistribDesconectivos = db.Sub_MttoDistribDesconectivos.Find(id);
            if (sub_MttoDistribDesconectivos == null)
            {
                return HttpNotFound();
            }
            return View(sub_MttoDistribDesconectivos);
        }

        // POST: Sub_MttoDistribDesconectivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sub_MttoDistribDesconectivos sub_MttoDistribDesconectivos = db.Sub_MttoDistribDesconectivos.Find(id);
            db.Sub_MttoDistribDesconectivos.Remove(sub_MttoDistribDesconectivos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EliminaMttoDesc(string cod, string codDesc, DateTime fechaM)
        {
            try
            {
                Repositorio br = new Repositorio(db);

                Sub_MttoDistribDesconectivos mttoDesc = db.Sub_MttoDistribDesconectivos.Find(cod, fechaM, codDesc);
                //int accion = br.GetNumAccion("M", "SIA", mttoDesc.NumAccion ?? 0);
                db.Sub_MttoDistribDesconectivos.Remove(mttoDesc);
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

        public ActionResult _VPDesc(string sub)
        {

            var ListaDesc = new SubMttoSubDistRepositorio(db);
            var desconectivos = ListaDesc.FindDescEnSub(sub);

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
