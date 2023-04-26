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
using Subestaciones.Models.Clases;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class Sub_MttoDistTransfController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_MttoDistTransf
        public ActionResult Index()
        {
            return View(db.Sub_MttoDistTransf.ToList());
        }

        // GET: Sub_MttoDistTransf/Details/5
        public ActionResult Details(string codigo, DateTime? fecha, int idt, int ea, string valor)
        {
            if ((codigo == null) && (fecha == null) && (idt == 0) && (ea == 0))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MttoSubDistViewModel Dmtto = new MttoSubDistViewModel();

            Dmtto.transf = db.Sub_MttoDistTransf.Find(codigo, fecha, idt, ea);
            Dmtto.relacT = db.Sub_MttoDistRelacTransformacion.Where(s => s.CodigoSub == codigo && s.Fecha == fecha && s.Id_Transformador == idt && s.Id_EATransformador == ea).ToList();
            Dmtto.resistO = db.Sub_MttoDistResistOhmica.Where(s => s.CodigoSub == codigo && s.Fecha == fecha && s.Id_Transformador == idt && s.Id_EATransformador == ea).ToList();
            if (Dmtto.transf == null)
            {
                return HttpNotFound();
            }
            ViewBag.valor = valor;
            return View(Dmtto);
        }

        // GET: Sub_MttoDistTransf/Create
        public ActionResult Create(string sub, DateTime? fecha, int? ea, string valor)
        {
            var repoMtto = new SubMttoSubDistRepositorio(db);
            MttoSubDistViewModel dmttos = new MttoSubDistViewModel();

            ViewBag.codSub = sub;
            ViewBag.fechaMtto = fecha;
            ViewBag.eaMtto = ea;
            ViewBag.valor = valor;

            ViewBag.estadoTanqueExp = repoMtto.estadoTanque();
            ViewBag.term = repoMtto.estadoTanque();
            ViewBag.silica = repoMtto.estadoTanque();
            ViewBag.indicador = repoMtto.estados();
            ViewBag.rele = repoMtto.estados();
            ViewBag.bushingAlta = repoMtto.estados();
            ViewBag.bushingBaja = repoMtto.estados();
            ViewBag.pintura = repoMtto.estados();
            ViewBag.estadosVent = repoMtto.estados();
            ViewBag.estadoVal = repoMtto.estados();
            ViewBag.Conex = repoMtto.estados();
            ViewBag.aterrTanque = repoMtto.aterramientos();
            ViewBag.nivelAceite = repoMtto.nivel();
            ViewBag.tubo = repoMtto.estadoTanque();
            ViewBag.radiadores = repoMtto.estados();
            ViewBag.valSobreP = repoMtto.estadoTanque();
            ViewBag.aterrNeutro = repoMtto.aterramientos();
            ViewBag.salideros = repoMtto.salideros();
            ViewBag.normas = repoMtto.tiposPruebas();
            return View(dmttos);
        }

        // POST: Sub_MttoDistTransf/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MttoSubDistViewModel dmttoTransf)
        {

            if (ModelState.IsValid)
            {
                if (ValidarExisteMttoTransformador(dmttoTransf.transf.CodigoSub, dmttoTransf.transf.Fecha,
                    dmttoTransf.transf.Id_EATransformador, dmttoTransf.transf.Id_Transformador))
                {
                    ModelState.AddModelError("dmttoTransf.transf.Fecha", "Al transformador actual ya se le realizó su mantenimiento");
                }
                else
                {
                    db.Sub_MttoDistTransf.Add(dmttoTransf.transf);

                    var nroTapRO = 1;
                    var nroTapRT = 1;

                    if (dmttoTransf.resistO != null)
                    {

                        foreach (var item in dmttoTransf.resistO)
                        {

                            item.NroTap = (short)nroTapRO++;
                            db.Sub_MttoDistResistOhmica.Add(item);

                        }
                    }
                    if (dmttoTransf.relacT != null)
                    {
                        foreach (var item in dmttoTransf.relacT)
                        {

                            item.NroTap = (short)nroTapRT++;
                            db.Sub_MttoDistRelacTransformacion.Add(item);

                        }
                    }

                    db.SaveChanges();
                    if (Request.Form["submitButton"].ToString() == "Editar")
                    {
                        return RedirectToAction("Edit", "SubMttoSubDistribucions", new { codigo = dmttoTransf.transf.CodigoSub, fecha = dmttoTransf.transf.Fecha });

                    }
                    SubMttoSubDistribucion dmtto = db.SubMttoSubDistribucion.Find(dmttoTransf.transf.CodigoSub, dmttoTransf.transf.Fecha);

                    return RedirectToAction("ComponentesMtto", "SubMttoSubDistribucions", new { codigo = dmttoTransf.transf.CodigoSub, fecha = dmttoTransf.transf.Fecha });
                }
            }

            ViewBag.valor = Request.Form["submitButton"].ToString();

            var repoMtto = new SubMttoSubDistRepositorio(db);
            ViewBag.codSub = dmttoTransf.transf.CodigoSub;
            ViewBag.fechaMtto = dmttoTransf.transf.Fecha;
            ViewBag.eaMtto = dmttoTransf.transf.Id_EATransformador;
            ViewBag.estadoTanqueExp = repoMtto.estadoTanque();
            ViewBag.term = repoMtto.estadoTanque();
            ViewBag.silica = repoMtto.estadoTanque();
            ViewBag.indicador = repoMtto.estados();
            ViewBag.rele = repoMtto.estados();
            ViewBag.bushingAlta = repoMtto.estados();
            ViewBag.bushingBaja = repoMtto.estados();
            ViewBag.pintura = repoMtto.estados();
            ViewBag.estadosVent = repoMtto.estados();
            ViewBag.estadoVal = repoMtto.estados();
            ViewBag.Conex = repoMtto.estados();
            ViewBag.aterrTanque = repoMtto.aterramientos();
            ViewBag.nivelAceite = repoMtto.nivel();
            ViewBag.tubo = repoMtto.estadoTanque();
            ViewBag.radiadores = repoMtto.estados();
            ViewBag.valSobreP = repoMtto.estadoTanque();
            ViewBag.aterrNeutro = repoMtto.aterramientos();
            ViewBag.salideros = repoMtto.salideros();
            ViewBag.normas = repoMtto.tiposPruebas();
            return View(dmttoTransf);

        }

        // GET: Sub_MttoDistTransf/Edit/5
        public ActionResult Edit(string subcodigo, DateTime fecha, int idt, int ea, string valor)
        {
            MttoSubDistViewModel dmttoTransf = new MttoSubDistViewModel();

            if ((subcodigo == null) && (fecha == null) && (idt == 0) && (ea == 0))

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.codSub = subcodigo;
            ViewBag.fechaMtto = fecha;
            ViewBag.eaMtto = ea;
            ViewBag.valor = valor;
            ViewBag.idT = idt;

            Sub_MttoDistTransf sub_MttoDistTransf = db.Sub_MttoDistTransf.Find(subcodigo, fecha, idt, (short)ea);

            dmttoTransf.resistO = db.Sub_MttoDistResistOhmica.Where(c => c.CodigoSub == subcodigo &&
            c.Fecha == fecha && c.Id_EATransformador == (short)ea && c.Id_Transformador == idt).ToList();
            
            dmttoTransf.relacT = db.Sub_MttoDistRelacTransformacion.Where(c => c.CodigoSub == subcodigo &&
            c.Fecha == fecha && c.Id_EATransformador == (short)ea && c.Id_Transformador == idt).ToList();

            ViewBag.resistOCount = dmttoTransf.resistO.Count;
            ViewBag.relacTCount = dmttoTransf.relacT.Count;

            if (sub_MttoDistTransf == null)
            {
                return HttpNotFound();
            }

            dmttoTransf.transf = sub_MttoDistTransf;

            ViewBag.valor = valor;

            var repoMtto = new SubMttoSubDistRepositorio(db);
            ViewBag.codSub = subcodigo;
            ViewBag.fechaMtto = fecha;
            ViewBag.eaMtto = ea;
            ViewBag.estadoTanqueExp = repoMtto.estadoTanque();
            ViewBag.term = repoMtto.estadoTanque();
            ViewBag.silica = repoMtto.estadoTanque();
            ViewBag.indicador = repoMtto.estados();
            ViewBag.rele = repoMtto.estados();
            ViewBag.bushingAlta = repoMtto.estados();
            ViewBag.bushingBaja = repoMtto.estados();
            ViewBag.pintura = repoMtto.estados();
            ViewBag.estadosVent = repoMtto.estados();
            ViewBag.estadoVal = repoMtto.estados();
            ViewBag.Conex = repoMtto.estados();
            ViewBag.aterrTanque = repoMtto.aterramientos();
            ViewBag.nivelAceite = repoMtto.nivel();
            ViewBag.tubo = repoMtto.estadoTanque();
            ViewBag.radiadores = repoMtto.estados();
            ViewBag.valSobreP = repoMtto.estadoTanque();
            ViewBag.aterrNeutro = repoMtto.aterramientos();
            ViewBag.salideros = repoMtto.salideros();
            ViewBag.normas = repoMtto.tiposPruebas();

            return View(dmttoTransf);
        }

        // POST: Sub_MttoDistTransf/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MttoSubDistViewModel dmttoTransf)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dmttoTransf.transf).State = EntityState.Modified;

                var i = 0;
                var y = 0;
                var nroTapRO = 1;
                var nroTapRT = 1;

                if (dmttoTransf.resistO != null)
                {

                    foreach (var item in dmttoTransf.resistO)
                    {
                        item.NroTap = (short)nroTapRO++;
                        db.Entry(dmttoTransf.resistO[i]).State = EntityState.Modified;
                        i++;
                    }
                }
                if (dmttoTransf.relacT != null)
                {
                    foreach (var item in dmttoTransf.relacT)
                    {
                        item.NroTap = (short)nroTapRT++;
                        db.Entry(dmttoTransf.relacT[y]).State = EntityState.Modified;
                        y++;
                    }
                }

                db.SaveChanges();

                if (Request.Form["submitButton"].ToString() == "Editar")
                {
                    return RedirectToAction("Edit", "SubMttoSubDistribucions", new { codigo = dmttoTransf.transf.CodigoSub, fecha = dmttoTransf.transf.Fecha });

                }
                return RedirectToAction("ComponentesMtto", "SubMttoSubDistribucions", new { codigo = dmttoTransf.transf.CodigoSub, fecha = dmttoTransf.transf.Fecha });

            }
            //aqui faltan las tablas!!

            ViewBag.valor = Request.Form["submitButton"].ToString();

            var repoMtto = new SubMttoSubDistRepositorio(db);
            ViewBag.codSub = dmttoTransf.transf.CodigoSub;
            ViewBag.fechaMtto = dmttoTransf.transf.Fecha;
            ViewBag.eaMtto = dmttoTransf.transf.Id_EATransformador;
            ViewBag.estadoTanqueExp = repoMtto.estadoTanque();
            ViewBag.term = repoMtto.estadoTanque();
            ViewBag.silica = repoMtto.estadoTanque();
            ViewBag.indicador = repoMtto.estados();
            ViewBag.rele = repoMtto.estados();
            ViewBag.bushingAlta = repoMtto.estados();
            ViewBag.bushingBaja = repoMtto.estados();
            ViewBag.pintura = repoMtto.estados();
            ViewBag.estadosVent = repoMtto.estados();
            ViewBag.estadoVal = repoMtto.estados();
            ViewBag.Conex = repoMtto.estados();
            ViewBag.aterrTanque = repoMtto.aterramientos();
            ViewBag.nivelAceite = repoMtto.nivel();
            ViewBag.tubo = repoMtto.estadoTanque();
            ViewBag.radiadores = repoMtto.estados();
            ViewBag.valSobreP = repoMtto.estadoTanque();
            ViewBag.aterrNeutro = repoMtto.aterramientos();
            ViewBag.salideros = repoMtto.salideros();
            ViewBag.normas = repoMtto.tiposPruebas();
            return View(dmttoTransf);
        }

        // GET: Sub_MttoDistTransf/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_MttoDistTransf sub_MttoDistTransf = db.Sub_MttoDistTransf.Find(id);
            if (sub_MttoDistTransf == null)
            {
                return HttpNotFound();
            }
            return View(sub_MttoDistTransf);
        }

        // POST: Sub_MttoDistTransf/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sub_MttoDistTransf sub_MttoDistTransf = db.Sub_MttoDistTransf.Find(id);
            db.Sub_MttoDistTransf.Remove(sub_MttoDistTransf);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool ValidarExisteMttoTransformador(string Codigo, DateTime fecha, int EAT, int idT)
        {

            var parametrocodS = new SqlParameter("@sub", Codigo);
            var parametroDia = new SqlParameter("@dia", fecha.Day);
            var parametroMes = new SqlParameter("@mes", fecha.Month);
            var parametroAnno = new SqlParameter("@anho", fecha.Year);
            var parametroEAT = new SqlParameter("@EAT", EAT);
            var parametroIdT = new SqlParameter("@idT", idT);

            var cant = db.Database.SqlQuery<Sub_MttoDistTransf>(@"SELECT  *
                                                   FROM    Sub_MttoDistTransf
                                                   WHERE   CodigoSub =@sub
                                                   AND DAY(Fecha) = @dia
                                                   AND MONTH(Fecha) =@mes
                                                   AND YEAR(Fecha) =@anho
                                                   AND Id_EATransformador = @EAT
                                                   AND Id_Transformador = @idT", parametrocodS, parametroDia, parametroMes, parametroAnno, parametroEAT, parametroIdT).ToList();
            return !(cant.Count == 0);

        }

        public ActionResult EliminaMttoTransf(string cod, int? idTransf, DateTime? fechaM, int? EA)
        {
            try
            {
                Repositorio br = new Repositorio(db);

                Sub_MttoDistTransf mttoTransf = db.Sub_MttoDistTransf.Find(cod, fechaM, idTransf, EA);
                db.Sub_MttoDistTransf.Remove(mttoTransf);

                var ListaTransformadores = new TransfSubtRepositorio(db);
                var Transformador = ListaTransformadores.Find((int)EA, (int)idTransf, "TS");
                var numPos = Transformador.NroPosiciones;

                for (int i = 1; i <= numPos; i++)
                {
                    Sub_MttoDistRelacTransformacion mttoRelTrans = db.Sub_MttoDistRelacTransformacion.Find(cod, fechaM, idTransf, EA, i);
                    db.Sub_MttoDistRelacTransformacion.Remove(mttoRelTrans);
                    Sub_MttoDistResistOhmica mttoResOhm = db.Sub_MttoDistResistOhmica.Find(cod, fechaM, idTransf, EA, i);
                    db.Sub_MttoDistResistOhmica.Remove(mttoResOhm);
                }

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

        public ActionResult _VPTransf(string sub)
        {

            var lista = db.TransformadoresSubtransmision.Where(x => x.Codigo == sub).ToList();

            ViewBag.listaTransf = lista;
            ViewBag.cant = lista.Count();

            return PartialView();
        }

        public ActionResult _VPTransfEdit(string sub)
        {

            var lista = db.TransformadoresSubtransmision.Where(x => x.Codigo == sub).ToList();

            ViewBag.listaTransf = lista;
            ViewBag.cant = lista.Count();

            return PartialView();
        }

        public JsonResult DatoTransf(string sub, int ea, int t)
        {
            var lista = db.TransformadoresSubtransmision.Where(x => x.Codigo == sub).ToList();

            ViewBag.listaTransf = lista;
            ViewBag.cant = lista.Count();
            //SubMttoSubDistRepositorio repo = new SubMttoSubDistRepositorio(db);
            //var transf = repo.FindTransf(sub, ea, t);

            var ListaTransformadores = new TransfSubtRepositorio(db);
            var Transformador = ListaTransformadores.Find(ea, t, "TS");
            ViewBag.nroPos = Transformador.NroPosiciones;

            return Json(Transformador, JsonRequestBehavior.AllowGet);
        }
    }
}
