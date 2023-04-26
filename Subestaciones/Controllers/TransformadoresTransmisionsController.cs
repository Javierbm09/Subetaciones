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
    public class TransformadoresTransmisionsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: TransformadoresTransmisions
        public ActionResult Index(string ubicado, string inserta)
        {
            var ListaTransfTrans = new TransfTransRepositorio(db);
            ViewBag.ubicado = ubicado;
            ViewBag.Inserto = inserta;

            if (ubicado == "TT") { ViewBag.En = "subestación"; } else { ViewBag.En = "almacén"; }
            return View(ListaTransfTrans.ObtenerListadoTransformador(ubicado));
        }

        // GET: TransformadoresTransmisions/Details/5
        public ActionResult Details(short EA, int id_transformador, string ubicado)
        {
            if (id_transformador == 0 && EA == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaTransformadores = new TransfTransRepositorio(db);
            var Transformador = ListaTransformadores.EditarTransformador(EA, id_transformador, ubicado);
            //TransformadoresTransmision Transformador = db.TransformadoresTransmision.Find(EA, id_transformador);

            if (id_transformador == 0 && EA == 0)
            {
                return HttpNotFound();
            }
            var repo = new Repositorio(db);
            var TP = new TransfSubtRepositorio(db);
            if (ubicado == "TT")
            {
                ViewBag.En = "subestación";
                ViewBag.ListaSub = new SelectList(repo.subT().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            }
            if (ubicado == "TTA")
            {
                ViewBag.En = "almacén";
                ViewBag.ListaSub = new SelectList(repo.almacenes().Select(c => new { Value = c.Dir_Calle, Text = c.Nombre }), "Value", "Text");
            }


            ViewBag.ubicado = ubicado;
            ViewBag.ErrorEA = 0;
            ViewBag.fab = new SelectList(TP.fab().Select(c => new { Value = c.Id_Fabricante, Text = c.Nombre + ", " + c.Pais }), "Value", "Text");
            ViewBag.GrupoConex = new SelectList(TP.GrupoConexion().Select(c => new { Value = c.tipo, Text = c.tipo }), "Value", "Text");
            ViewBag.Enfriam = new SelectList(TP.Enfriamiento().Select(c => new { Value = c.Codigo, Text = c.TipoEnfriamiento }), "Value", "Text");
            ViewBag.capacidad = new SelectList(repo.capacidades().Select(c => new { Value = c.Id_Capacidad, Text = c.Capacidad }), "Value", "Text");
            ViewBag.voltPrim = new SelectList(TP.voltajePrimario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text");
            ViewBag.voltSec = new SelectList(TP.voltajeSecundario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text");
            ViewBag.voltTerc = new SelectList(TP.voltajeTerciario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text");
            ViewBag.EO = TP.EstadoO();
            ViewBag.RegV = TP.RegVolt();
            ViewBag.TuboExp = TP.TuboE();
            ViewBag.valvulasobre = TP.Valvula();
            ViewBag.term = TP.Termosifones();
            return View(Transformador);
        }

        // GET: TransformadoresTransmisions/Create
        public ActionResult Create(string ubicado)
        {
            var repo = new Repositorio(db);
            var TP = new TransfSubtRepositorio(db);
            if (ubicado == "TT")
            {
                ViewBag.En = "subestación";
                ViewBag.ListaSub = new SelectList(repo.subT().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            }
            if (ubicado == "TTA")
            {
                ViewBag.En = "almacén";
                ViewBag.ListaSub = new SelectList(repo.almacenes().Select(c => new { Value = c.Dir_Calle, Text = c.Nombre }), "Value", "Text");
            }


            ViewBag.ubicado = ubicado;
            ViewBag.ErrorEA = 0;
            ViewBag.fab = new SelectList(TP.fab().Select(c => new { Value = c.Id_Fabricante, Text = c.Nombre + ", " + c.Pais }), "Value", "Text");
            ViewBag.GrupoConex = new SelectList(TP.GrupoConexion().Select(c => new { Value = c.tipo, Text = c.tipo }), "Value", "Text");
            ViewBag.Enfriam = new SelectList(TP.Enfriamiento().Select(c => new { Value = c.Codigo, Text = c.TipoEnfriamiento }), "Value", "Text");
            ViewBag.capacidad = new SelectList(repo.capacidades().Select(c => new { Value = c.Id_Capacidad, Text = c.Capacidad }), "Value", "Text");
            ViewBag.voltPrim = new SelectList(TP.voltajePrimario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text");
            ViewBag.voltSec = new SelectList(TP.voltajeSecundario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text");
            ViewBag.voltTerc = new SelectList(TP.voltajeTerciario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text");
            ViewBag.EO = TP.EstadoO();
            ViewBag.RegV = TP.RegVolt();
            ViewBag.TuboExp = TP.TuboE();
            ViewBag.valvulasobre = TP.Valvula();
            ViewBag.term = TP.Termosifones();
            ViewBag.numF = TP.numFase();

            return View();
        }

        // POST: TransformadoresTransmisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_EAdministrativa,Id_Transformador,NumAccion,Codigo,Numemp,Id_Capacidad,SimboloTaps,Id_VoltajePrim,Fase,NoSerie,NecesidadEmitida,CE,PosicionBanco,EstadoOperativo,TapEncontrado,TapDejado,NumFase,UltAccionVer,EstadoHermeticidad,EstadoPinturaTanque,EstadoPinturaRotulos,AcidezAceite,NivelAceite,ColoracionAceite,PerteneceA,Id_Voltaje_Secun,Id_Bloque,TabRegulable,TabPrimarioSecundario,CantidadRegulacion,CuentaOperaciones,TabFecha,AnnoFabricacion,idFabricante,VoltajeSecundario,PorcientoImpedancia,GrupoConexion,PesoTotal,CapacidadVentilador,Nombre,CorrienteAlta,FrecuenciaN,TipoEnfriamiento,PerdidasVacio,PerdidasBajoCarga,NivelRuido,MaxTemperatura,NivelRadioInterf,VoltajeImpulso,PesoAceite,PesoNucleo,CorrienteBaja,Tipo,CantVentiladores,CantRadiadores,Observaciones,PesoTansporte,TipoRegVoltaje,NroPosiciones,PosicionTrabajo,TipoCajaMando,TuboExplosor,ValvulaSobrePresion,VoltajeTerciario,NumeroInventario,CorrienteTerciaria,PorcientoZccPS,PorcientoZccST,PorcientoZccPT,RegVoltajeSecNroPos,RegVoltajeSecPosTrab,TieneTermosifones,CantTermosifones,BushingPrimFaseATipo,BushingPrimFaseBTipo,BushingPrimFaseCTipo,BushingPrimFaseNeutroTipo,BushingSecFasesTipo,BushingSecNeutroTipo,BushingTercFasesTipo,BushingPrimFaseASerie,BushingPrimFaseBSerie,BushingPrimFaseCSerie,BushingPrimFaseAUn,BushingPrimFaseBUn,BushingPrimFaseCUn,BushingPrimFaseAIn,BushingPrimFaseBIn,BushingPrimFaseCIn,BushingSecFasesUn,BushingSecFasesIn,BushingTercFasesUn,BushingTercFasesIn,FechaDeInstalado")] TransformadoresTransmision transformadoresTransmision, string ubicado)
        {
            Repositorio br = new Repositorio(db);
            var Id_Eadministrativa = br.GetId_EAdministrativaUsuario();

            if (ModelState.IsValid && Id_Eadministrativa != null)
            {
                transformadoresTransmision.Id_EAdministrativa = (int)Id_Eadministrativa;
                var id_T = db.Database.SqlQuery<int>(@"declare @Numtrans int
                Select @Numtrans = Max(Id_Transformador) + 1
                From dbo.TransformadoresTransmision
                Where id_EAdministrativa = {0}
                if @Numtrans is null
                set @Numtrans = 1
                Select @Numtrans as idPararrayo", Id_Eadministrativa);
                transformadoresTransmision.NumAccion = br.GetNumAccion("I", "STT", 0);
                transformadoresTransmision.Id_Transformador = id_T.ToList().First();
                db.TransformadoresTransmision.Add(transformadoresTransmision);
                db.SaveChanges();
                return RedirectToAction("Index", new { ubicado = ubicado, inserta = "Si" });

            }

            var TP = new TransfSubtRepositorio(db);
            ViewBag.ErrorEA = 0;
            if (ubicado == "TT")
            {
                ViewBag.En = "subestación";
                ViewBag.ListaSub = new SelectList(br.subT().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            }
            if (ubicado == "TTA")
            {
                ViewBag.En = "almacén";
                ViewBag.ListaSub = new SelectList(br.almacenes().Select(c => new { Value = c.Dir_Calle, Text = c.Nombre }), "Value", "Text");
            }
            ViewBag.fab = new SelectList(TP.fab().Select(c => new { Value = c.Id_Fabricante, Text = c.Nombre + ", " + c.Pais }), "Value", "Text");
            ViewBag.GrupoConex = new SelectList(TP.GrupoConexion().Select(c => new { Value = c.tipo, Text = c.tipo }), "Value", "Text");
            ViewBag.Enfriam = new SelectList(TP.Enfriamiento().Select(c => new { Value = c.Codigo, Text = c.TipoEnfriamiento }), "Value", "Text");
            ViewBag.capacidad = new SelectList(br.capacidades().Select(c => new { Value = c.Id_Capacidad, Text = c.Capacidad }), "Value", "Text");
            ViewBag.voltPrim = new SelectList(TP.voltajePrimario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text");
            ViewBag.voltSec = new SelectList(TP.voltajeSecundario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text");
            ViewBag.voltTerc = new SelectList(TP.voltajeTerciario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text");
            ViewBag.EO = TP.EstadoO();
            ViewBag.RegV = TP.RegVolt();
            ViewBag.TuboExp = TP.TuboE();
            ViewBag.valvulasobre = TP.Valvula();
            ViewBag.term = TP.Termosifones();
            ViewBag.numF = TP.numFase();

            return View(transformadoresTransmision);
        }

        // GET: TransformadoresTransmisions/Edit/5
        public ActionResult Edit(short EA, int id_transformador, string ubicado)
        {
            var ListaTransformadores = new TransfTransRepositorio(db);

            //var Transformador = ListaTransformadores.EditarTransformador(EA, id_transformador, ubicado);

            TransformadoresTransmision Transformador = db.TransformadoresTransmision.Find(EA, id_transformador);

            Bloque bloquetransf = db.Bloque.Find(Transformador.Codigo, Transformador.Id_Bloque);

            if (bloquetransf != null)
            {
                Transformador.Bloque = bloquetransf.tipobloque;
                Transformador.tipoSalidaBloque = bloquetransf.TipoSalida;

                EsquemasBaja esquema = db.EsquemasBaja.Find(bloquetransf.EsquemaPorBaja);
                if (esquema != null)
                {
                    Transformador.esquemaBloque = esquema.EsquemaPorBaja;
                }

                SalidaExclusivaSub cliente = db.SalidaExclusivaSub.Find(bloquetransf.Codigo,(short)bloquetransf.Id_bloque);
                if (cliente != null)
                {
                    Transformador.clienteBloque = cliente.Cliente;
                    Sectores sector = db.Sectores.Find(cliente.Sector);
                    if (sector != null)
                    {
                        Transformador.sectorClienteBloque = sector.Sector;
                    }
                }

                VoltajesSistemas tensionSalida = db.VoltajesSistemas.Find(bloquetransf.VoltajeSecundario);
                if (tensionSalida != null)
                {
                    Transformador.tensionSalidaBloque = tensionSalida.Voltaje;
                }

                VoltajesSistemas tensionTer = db.VoltajesSistemas.Find(bloquetransf.VoltajeTerciario);
                if (tensionTer != null)
                {
                    Transformador.tensionTerciarioBloque = tensionTer.Voltaje;
                }
            }


            var TP = new TransfSubtRepositorio(db);
            Repositorio br = new Repositorio(db);
            ViewBag.ubicado = ubicado;

            if (ubicado == "TT")
            {
                ViewBag.En = "subestación";
                ViewBag.ListaSub = new SelectList(br.subT().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            }
            if (ubicado == "TTA")
            {
                ViewBag.En = "almacén";
                ViewBag.ListaSub = new SelectList(br.almacenes().Select(c => new { Value = c.Dir_Calle, Text = c.Nombre }), "Value", "Text");
            }

            ViewBag.fab = new SelectList(TP.fab().Select(c => new { Value = c.Id_Fabricante, Text = c.Nombre + ", " + c.Pais }), "Value", "Text", Transformador.idFabricante);
            ViewBag.GrupoConex = new SelectList(TP.GrupoConexion().Select(c => new { Value = c.tipo, Text = c.tipo }), "Value", "Text", Transformador.GrupoConexion);
            ViewBag.Enfriam = new SelectList(TP.Enfriamiento().Select(c => new { Value = c.Codigo, Text = c.TipoEnfriamiento }), "Value", "Text", Transformador.TipoEnfriamiento);
            ViewBag.capacidad = new SelectList(br.capacidades().Select(c => new { Value = c.Id_Capacidad, Text = c.Capacidad }), "Value", "Text", Transformador.Id_Capacidad);
            ViewBag.voltPrim = new SelectList(TP.voltajePrimario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text", Transformador.Id_VoltajePrim);
            ViewBag.voltSec = new SelectList(TP.voltajeSecundario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text", Transformador.Id_Voltaje_Secun);
            ViewBag.voltTerc = new SelectList(TP.voltajeTerciario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text", Transformador.VoltajeTerciario);
            ViewBag.EO = TP.EstadoO();
            ViewBag.RegV = TP.RegVolt();
            ViewBag.TuboExp = TP.TuboE();
            ViewBag.valvulasobre = TP.Valvula();
            ViewBag.term = TP.Termosifones();
            ViewBag.ErrorEA = 0;
            ViewBag.numF = TP.numFase();

            return View(Transformador);
        }

        // POST: TransformadoresTransmisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_EAdministrativa,Id_Transformador,NumAccion,Codigo,Numemp,Id_Capacidad,SimboloTaps,Id_VoltajePrim,Fase,NoSerie,NecesidadEmitida,CE,PosicionBanco,EstadoOperativo,TapEncontrado,TapDejado,NumFase,UltAccionVer,EstadoHermeticidad,EstadoPinturaTanque,EstadoPinturaRotulos,AcidezAceite,NivelAceite,ColoracionAceite,PerteneceA,Id_Voltaje_Secun,Id_Bloque,TabRegulable,TabPrimarioSecundario,CantidadRegulacion,CuentaOperaciones,TabFecha,AnnoFabricacion,idFabricante,VoltajeSecundario,PorcientoImpedancia,GrupoConexion,PesoTotal,CapacidadVentilador,Nombre,CorrienteAlta,FrecuenciaN,TipoEnfriamiento,PerdidasVacio,PerdidasBajoCarga,NivelRuido,MaxTemperatura,NivelRadioInterf,VoltajeImpulso,PesoAceite,PesoNucleo,CorrienteBaja,Tipo,CantVentiladores,CantRadiadores,Observaciones,PesoTansporte,TipoRegVoltaje,NroPosiciones,PosicionTrabajo,TipoCajaMando,TuboExplosor,ValvulaSobrePresion,VoltajeTerciario,NumeroInventario,CorrienteTerciaria,PorcientoZccPS,PorcientoZccST,PorcientoZccPT,RegVoltajeSecNroPos,RegVoltajeSecPosTrab,TieneTermosifones,CantTermosifones,BushingPrimFaseATipo,BushingPrimFaseBTipo,BushingPrimFaseCTipo,BushingPrimFaseNeutroTipo,BushingSecFasesTipo,BushingSecNeutroTipo,BushingTercFasesTipo,BushingPrimFaseASerie,BushingPrimFaseBSerie,BushingPrimFaseCSerie,BushingPrimFaseAUn,BushingPrimFaseBUn,BushingPrimFaseCUn,BushingPrimFaseAIn,BushingPrimFaseBIn,BushingPrimFaseCIn,BushingSecFasesUn,BushingSecFasesIn,BushingTercFasesUn,BushingTercFasesIn,FechaDeInstalado")] TransformadoresTransmision transformadoresTransmision, string ubicado)
        {
            Repositorio br = new Repositorio(db);
            var Id_Eadministrativa = br.GetId_EAdministrativaUsuario();

            if (ModelState.IsValid && Id_Eadministrativa != null)
            {

                transformadoresTransmision.NumAccion = br.GetNumAccion("M", "STT", 0);
                db.Entry(transformadoresTransmision).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.Form["submitButton"].ToString() == "TT")
                {

                    return RedirectToAction("Index", new { ubicado = ubicado });
                }
                if (Request.Form["submitButton"].ToString() == "Termo")
                {
                    ViewBag.transf = transformadoresTransmision.Id_Transformador;
                    return RedirectToAction("Create", "SubTermometrosTransfTransmisions", new { EA = transformadoresTransmision.Id_EAdministrativa, id_t = transformadoresTransmision.Id_Transformador, NumAccion = transformadoresTransmision.NumAccion, ubicado = ubicado });
                }
            }
            ViewBag.ErrorEA = 0;
            var TP = new TransfSubtRepositorio(db);
            if (ubicado == "TT") { ViewBag.En = "subestación";
                ViewBag.ListaSub = new SelectList(br.subT().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text"); }
            if (ubicado == "TTA") { ViewBag.En = "almacén";
                ViewBag.ListaSub = new SelectList(br.almacenes().Select(c => new { Value = c.Dir_Calle, Text = c.Nombre }), "Value", "Text"); }
            ViewBag.fab = new SelectList(TP.fab().Select(c => new { Value = c.Id_Fabricante, Text = c.Nombre + ", " + c.Pais }), "Value", "Text", transformadoresTransmision.idFabricante);
            ViewBag.GrupoConex = new SelectList(TP.GrupoConexion().Select(c => new { Value = c.tipo, Text = c.tipo }), "Value", "Text", transformadoresTransmision.GrupoConexion);
            ViewBag.Enfriam = new SelectList(TP.Enfriamiento().Select(c => new { Value = c.Codigo, Text = c.TipoEnfriamiento }), "Value", "Text", transformadoresTransmision.TipoEnfriamiento);
            ViewBag.capacidad = new SelectList(br.capacidades().Select(c => new { Value = c.Id_Capacidad, Text = c.Capacidad }), "Value", "Text", transformadoresTransmision.Id_Capacidad);
            ViewBag.voltPrim = new SelectList(TP.voltajePrimario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text", transformadoresTransmision.Id_VoltajePrim);
            ViewBag.voltSec = new SelectList(TP.voltajeSecundario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text", transformadoresTransmision.Id_Voltaje_Secun);
            ViewBag.voltTerc = new SelectList(TP.voltajeTerciario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text", transformadoresTransmision.VoltajeTerciario);
            ViewBag.EO = TP.EstadoO();
            ViewBag.RegV = TP.RegVolt();
            ViewBag.TuboExp = TP.TuboE();
            ViewBag.valvulasobre = TP.Valvula();
            ViewBag.term = TP.Termosifones();
            ViewBag.numF = TP.numFase();

            return View(transformadoresTransmision);
        }

        // GET: TransformadoresTransmisions/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransformadoresTransmision transformadoresTransmision = db.TransformadoresTransmision.Find(id);
            if (transformadoresTransmision == null)
            {
                return HttpNotFound();
            }
            return View(transformadoresTransmision);
        }


        // POST: TransformadoresTransmisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            TransformadoresTransmision transformadoresTransmision = db.TransformadoresTransmision.Find(id);
            db.TransformadoresTransmision.Remove(transformadoresTransmision);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult Eliminar(short EA, int id_transformador)
        {
            try
            {
                TransformadoresTransmision transformador = db.TransformadoresTransmision.Find(EA, id_transformador);
                db.TransformadoresTransmision.Remove(transformador);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ListadoTransformadores(string ubicado)
        {
            var ListaTransf = new TransfTransRepositorio(db);
            //return View(await ListaIM.ObtenerListadoIM());
            return PartialView("_VPTablaTransfTrans", ListaTransf.ObtenerListadoTransformador(ubicado));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CargarBloque(string codsub)
        {
            var repo = new TransfTransRepositorio(db);
            var ListaBloque = repo.Bloques(codsub);
            ViewBag.bloques = ListaBloque;
            ViewBag.cantidad = ListaBloque.Count();
            return PartialView("_VPBloque");
        }

        #region Vistas Parciales (VP) AJAX
        [AllowAnonymous]
        public ActionResult cargarTablaTermometro(int? idTransf, string ubicadoT)
        {
            var listado = db.SubTermometrosTransfTransmision.Where(x => x.Id_Transformador == idTransf).ToList();

            ViewBag.termo = listado;
            ViewBag.cantidad = listado.Count();
            ViewBag.ubicado = ubicadoT;
            return PartialView("_VPTermometroTrans");
        }
        #endregion
    }
}
