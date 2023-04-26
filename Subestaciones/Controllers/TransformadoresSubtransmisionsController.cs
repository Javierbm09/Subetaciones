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
using Subestaciones.Models.Clases;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class TransformadoresSubtransmisionsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: TransformadoresSubtransmisions
        public ActionResult Index(string ubicado, string inserta)
        {

            var ListaTransfSub = new TransfSubtRepositorio(db);
            if (ubicado == "TS") { ViewBag.En = "subestación"; } else { ViewBag.En = "almacén"; }
            ViewBag.ubicado = ubicado;
            ViewBag.Inserto = inserta;

            return View(ListaTransfSub.ObtenerListadoTransformador(ubicado));
        }


        // GET: TransformadoresSubtransmisions/Details/5
        public ActionResult Details(short EA, int id_transformador, string ubicado)
        {
            if (id_transformador == 0 && EA == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaTransformadores = new TransfSubtRepositorio(db);
            var Transformador = ListaTransformadores.EditarTransformador(EA, id_transformador, ubicado);

            if (id_transformador == 0 && EA == 0)
            {
                return HttpNotFound();
            }
            ViewBag.ubicado = ubicado;
                if (ubicado == "TS")
            {
                ViewBag.En = "subestación";
            }
            if (ubicado == "TSA")
            {
                ViewBag.En = "almacén";
            }
            return View(Transformador);
        }

        // GET: TransformadoresSubtransmisions/Create
        public ActionResult Create(string ubicado)
        {
            var repo = new Repositorio(db);
            var TP = new TransfSubtRepositorio(db);
            if (ubicado == "TS")
            {
                ViewBag.En = "subestación";
                ViewBag.ListaSub = new SelectList(repo.subD().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            }
            if (ubicado == "TSA")
            {
                ViewBag.En = "almacén";
                ViewBag.ListaSub = new SelectList(repo.almacenes().Select(c => new { Value = c.Dir_Calle, Text = c.Nombre }), "Value", "Text");
            }

            ViewBag.ubicado = ubicado;
            ViewBag.ErrorEA = 0;
            //ViewBag.ListaBloques = new SelectList(TP.Bloques().Select(c => new { Value = c.Id_bloque, Text = c.tipobloque + " - " + c.EsquemaPorBaja + "-" + c.Sector + "-" + c.VoltajeTerciario + "-" + c.  }), "Value", "Text");

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
            ViewBag.numF = TP.numFase();
            //new SelectList(TP.EstadoO().Select(c => new { Value = c., Text = c.NombreFase }), "Value", "Text");
            return View();
        }

        // POST: TransformadoresSubtransmisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_EAdministrativa,Id_Transformador,NumAccion,Codigo,Numemp,Id_Capacidad,SimboloTaps,Id_VoltajePrim,Fase,NoSerie,NecesidadEmitida,CE,PosicionBanco,EstadoOperativo,TapEncontrado,TapDejado,NumFase,UltAccionVer,EstadoHermeticidad,EstadoPinturaTanque,EstadoPinturaRotulos,AcidezAceite,NivelAceite,ColoracionAceite,PerteneceA,Id_Voltaje_Secun,Id_Bloque,TabRegulable,TabPrimarioSecundario,CantidadRegulacion,CuentaOperaciones,TabFecha,AnnoFabricacion,idFabricante,VoltajeSecundario,PorcientoImpedancia,GrupoConexion,PesoTotal,CapacidadVentilador,Nombre,CorrienteAlta,FrecuenciaN,TipoEnfriamiento,PerdidasVacio,PerdidasBajoCarga,NivelRuido,MaxTemperatura,VoltajeImpulso,PesoAceite,PesoNucleo,NivelRadioInterf,CorrienteBaja,Tipo,CantVentiladores,CantRadiadores,Observaciones,PesoTansporte,TipoRegVoltaje,NroPosiciones,TipoCajaMando,TuboExplosor,ValvulaSobrePresion,VoltajeTerciario,PosicionTrabajo,NumeroInventario,FechaDeInstalado")] TransformadoresSubtransmision transformadoresSubtransmision, string ubicado)
        {
            //var usuario = System.Web.HttpContext.Current.User?.Identity?.Name ?? null;
            //string nombre_usuario = System.Web.HttpContext.Current.User.Identity.Name;
            //var usuario_logueado = db.Personal.FirstOrDefault(c => c.Nombre == nombre_usuario);
            Repositorio br = new Repositorio(db);
            var Id_Eadministrativa = br.GetId_EAdministrativaUsuario(); //esta EA ya esta bien

            if (ModelState.IsValid && Id_Eadministrativa != null)
            {
                transformadoresSubtransmision.Id_EAdministrativa = (int)Id_Eadministrativa;
                var id_T = db.Database.SqlQuery<int>(@"declare @Numtrans int
                Select @Numtrans = Max(Id_Transformador) + 1
                From dbo.TransformadoresSubtransmision
                Where id_EAdministrativa = {0}
                if @Numtrans is null
                set @Numtrans = 1
                Select @Numtrans as idPararrayo", Id_Eadministrativa);
                transformadoresSubtransmision.NumAccion = br.GetNumAccion("I", "STS", 0);
                transformadoresSubtransmision.Id_Transformador = id_T.ToList().First();
                db.TransformadoresSubtransmision.Add(transformadoresSubtransmision);
                db.SaveChanges();
                return RedirectToAction("Index", new { ubicado = ubicado,  inserta = "Si" });
            }
            ViewBag.ErrorEA = 0;

            var TP = new TransfSubtRepositorio(db);
            if (ubicado == "TS")
            {
                ViewBag.En = "subestación";
                ViewBag.ListaSub = new SelectList(br.subD().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            }
            if (ubicado == "TSA")
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
            ViewBag.numF = TP.numFase();

            return View(transformadoresSubtransmision);
        }

        // GET: TransformadoresSubtransmisions/Edit/5
        public ActionResult Edit(short EA, int id_transformador, string ubicado)
        {
            //if (id_transformador == 0||EA==0)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var TP = new TransfSubtRepositorio(db);
            TransformadoresSubtransmision transformadoresSubtransmision = db.TransformadoresSubtransmision.Find(EA, id_transformador);
            Bloque bloquetransf = db.Bloque.Find(transformadoresSubtransmision.Codigo, transformadoresSubtransmision.Id_Bloque);

            if (bloquetransf!=null) {
                transformadoresSubtransmision.Bloque = bloquetransf.tipobloque;
                transformadoresSubtransmision.tipoSalidaBloque = bloquetransf.TipoSalida;

                EsquemasBaja esquema = db.EsquemasBaja.Find(bloquetransf.EsquemaPorBaja);
                if (esquema != null)
                {
                    transformadoresSubtransmision.esquemaBloque = esquema.EsquemaPorBaja;
                }

                SalidaExclusivaSub cliente = db.SalidaExclusivaSub.Find(bloquetransf.Codigo, (short)bloquetransf.Id_bloque);
                if (cliente != null)
                {
                    transformadoresSubtransmision.clienteBloque = cliente.Cliente;

                    Sectores sector = db.Sectores.Find(cliente.Sector); ;
                    if (sector != null)
                    {
                        transformadoresSubtransmision.sectorClienteBloque = sector.Sector;
                    }
                }

                VoltajesSistemas tensionSalida = db.VoltajesSistemas.Find(bloquetransf.VoltajeSecundario);
                if (tensionSalida != null)
                {
                    transformadoresSubtransmision.tensionSalidaBloque = tensionSalida.Voltaje;
                }

                VoltajesSistemas tensionTer = db.VoltajesSistemas.Find(bloquetransf.VoltajeTerciario);
                if (tensionTer != null)
                {
                    transformadoresSubtransmision.tensionTerciarioBloque = tensionTer.Voltaje;
                }
            }

            ViewBag.ErrorEA = 0;
            ViewBag.ubicado = ubicado;
            Repositorio br = new Repositorio(db);

            if (ubicado == "TS")
            {
                ViewBag.En = "subestación";
                ViewBag.ListaSub = new SelectList(br.subD().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text", transformadoresSubtransmision.Codigo);
            }
            if (ubicado == "TSA")
            {
                ViewBag.En = "almacén";
                ViewBag.ListaSub = new SelectList(br.almacenes().Select(c => new { Value = c.Dir_Calle, Text = c.Nombre }), "Value", "Text", transformadoresSubtransmision.Codigo);
            }
            ViewBag.fab = new SelectList(TP.fab().Select(c => new { Value = c.Id_Fabricante, Text = c.Nombre + ", " + c.Pais }), "Value", "Text", transformadoresSubtransmision.idFabricante);
            ViewBag.GrupoConex = new SelectList(TP.GrupoConexion().Select(c => new { Value = c.tipo, Text = c.tipo }), "Value", "Text", transformadoresSubtransmision.GrupoConexion);
            ViewBag.Enfriam = new SelectList(TP.Enfriamiento().Select(c => new { Value = c.Codigo, Text = c.TipoEnfriamiento }), "Value", "Text", transformadoresSubtransmision.TipoEnfriamiento);
            ViewBag.capacidad = new SelectList(br.capacidades().Select(c => new { Value = c.Id_Capacidad, Text = c.Capacidad }), "Value", "Text", transformadoresSubtransmision.Id_Capacidad);
            ViewBag.voltPrim = new SelectList(TP.voltajePrimario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text", transformadoresSubtransmision.Id_VoltajePrim);
            ViewBag.voltSec = new SelectList(TP.voltajeSecundario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text", transformadoresSubtransmision.VoltajeSecundario);
            ViewBag.voltTerc = new SelectList(TP.voltajeTerciario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text", transformadoresSubtransmision.VoltajeTerciario);
            ViewBag.EO = TP.EstadoO();
            ViewBag.RegV = TP.RegVolt();
            ViewBag.TuboExp = TP.TuboE();
            ViewBag.valvulasobre = TP.Valvula();
            ViewBag.numF = TP.numFase();
            return View(transformadoresSubtransmision);
        }

        // POST: TransformadoresSubtransmisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TransformadoresSubtransmision transformadoresSubtransmision, string ubicado)
        {
            Repositorio br = new Repositorio(db);
            var Id_Eadministrativa = br.GetId_EAdministrativaUsuario();

            if (ModelState.IsValid && Id_Eadministrativa != null)
            {
                transformadoresSubtransmision.NumAccion = br.GetNumAccion("M", "STS", 0);
                db.Entry(transformadoresSubtransmision).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.Form["submitButton"].ToString() == "TS")
                {

                    return RedirectToAction("Index", new { ubicado = ubicado });
                }
                if (Request.Form["submitButton"].ToString() == "Termo")
                {

                    ViewBag.transf = transformadoresSubtransmision.Id_Transformador;
                    return RedirectToAction("Create", "SubTermometrosTransfSubTransmisions", new { EA = transformadoresSubtransmision.Id_EAdministrativa, id_t = transformadoresSubtransmision.Id_Transformador, NumAccion = transformadoresSubtransmision.NumAccion, ubicado = ubicado });
                }

            }
            ViewBag.ErrorEA = 0;
            ViewBag.ubicado = ubicado;
            var TP = new TransfSubtRepositorio(db);
            if (ubicado == "TS")
            {
                ViewBag.En = "subestación";
                ViewBag.ListaSub = new SelectList(br.subD().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text", transformadoresSubtransmision.Codigo);
            }
            if (ubicado == "TSA")
            {
                ViewBag.En = "almacén";
                ViewBag.ListaSub = new SelectList(br.almacenes().Select(c => new { Value = c.Dir_Calle, Text = c.Nombre }), "Value", "Text", transformadoresSubtransmision.Codigo);
            }
            ViewBag.fab = new SelectList(TP.fab().Select(c => new { Value = c.Id_Fabricante, Text = c.Nombre + ", " + c.Pais }), "Value", "Text", transformadoresSubtransmision.idFabricante);
            ViewBag.GrupoConex = new SelectList(TP.GrupoConexion().Select(c => new { Value = c.tipo, Text = c.tipo }), "Value", "Text", transformadoresSubtransmision.GrupoConexion);
            ViewBag.Enfriam = new SelectList(TP.Enfriamiento().Select(c => new { Value = c.Codigo, Text = c.TipoEnfriamiento }), "Value", "Text", transformadoresSubtransmision.TipoEnfriamiento);
            ViewBag.capacidad = new SelectList(br.capacidades().Select(c => new { Value = c.Id_Capacidad, Text = c.Capacidad }), "Value", "Text", transformadoresSubtransmision.Id_Capacidad);
            ViewBag.voltPrim = new SelectList(TP.voltajePrimario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text", transformadoresSubtransmision.Id_VoltajePrim);
            ViewBag.voltSec = new SelectList(TP.voltajeSecundario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text", transformadoresSubtransmision.Id_Voltaje_Secun);
            ViewBag.voltTerc = new SelectList(TP.voltajeTerciario().Select(c => new { Value = c.Id_VoltajeSistema, Text = c.Voltaje.ToString() + " Kv" }), "Value", "Text", transformadoresSubtransmision.VoltajeTerciario);
            ViewBag.EO = TP.EstadoO();
            ViewBag.RegV = TP.RegVolt();
            ViewBag.TuboExp = TP.TuboE();
            ViewBag.valvulasobre = TP.Valvula();
            ViewBag.numF = TP.numFase();
            return View(transformadoresSubtransmision);
        }

        // GET: TransformadoresSubtransmisions/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransformadoresSubtransmision transformadoresSubtransmision = db.TransformadoresSubtransmision.Find(id);
            if (transformadoresSubtransmision == null)
            {
                return HttpNotFound();
            }
            return View(transformadoresSubtransmision);
        }

        public JsonResult Eliminar(short EA, int id_transformador)
        {

            try
            {
                TransformadoresSubtransmision transformador = db.TransformadoresSubtransmision.Find(EA, id_transformador);
                db.TransformadoresSubtransmision.Remove(transformador);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            //}
        }

        [HttpPost]
        public ActionResult ListadoTransformadores(string ubicado)
        {
            var ListaTransf = new TransfSubtRepositorio(db);
            //return View(await ListaIM.ObtenerListadoIM());
            return PartialView("_VPTablaTransfSubt", ListaTransf.ObtenerListadoTransformador(ubicado));
        }

        // POST: TransformadoresSubtransmisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            TransformadoresSubtransmision transformadoresSubtransmision = db.TransformadoresSubtransmision.Find(id);
            db.TransformadoresSubtransmision.Remove(transformadoresSubtransmision);
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

        public ActionResult CargarBloque(string codsub)
        {
            var repo = new TransfSubtRepositorio(db);
            var ListaBloque = repo.Bloques(codsub);
            ViewBag.bloques = ListaBloque;
            ViewBag.cantidad = ListaBloque.Count();
            return PartialView("_VPBloque");
        }

        [HttpGet]
        public JsonResult ObtenerListaBloques(string cod)
        {
            var bloques = new TransfSubtRepositorio(db).Bloques(cod);
            return Json(bloques, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ObtenerListaTermometros(int idTransf)
        {
            var termometros = new TransfSubtRepositorio(db).ObtenerListaTermometros(idTransf);
            return Json(termometros, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertarTermometro(int EA, int numA, int transf, string termometro, string tipo, double rango)
        {
            if ((EA != 0) && (numA != 0) && (transf != 0))
            {
                new TransfSubtRepositorio(db).InsertarTermometro(EA, numA, transf, termometro, tipo, rango);
                return Json("true");
            }
            throw new ArgumentNullException();
        }

        [HttpPost]
        public ActionResult ActualizarTermometro(int EA, int numA, int transf, int id_termo, string termometro, string tipo, double editRango)
        {
            if (termometro != null)
            {
                try
                {
                    new TransfSubtRepositorio(db).ActualizarTermo(EA, numA, transf, id_termo, termometro, tipo, editRango);
                    return Json("true");
                }
                catch (Exception e)
                {
                    throw e;

                    throw new HttpException((int)HttpStatusCode.NotFound, "Ocurrió un error al editar el termómetro.");
                }
            }
            throw new ArgumentNullException();
        }

        [HttpPost]
        public ActionResult EliminarTermometro(int EA, int id_t, short? id, int NumAccion)
        {
            if (id_t != 0)
            {
                try
                {
                    new TransfSubtRepositorio(db).EliminarTermometro(EA, id_t, id, NumAccion);
                    return Json("true");
                }
                catch (Exception e)
                {
                    throw e;
                    throw new HttpException(404, "this is a 404 error");
                }
            }
            throw new ArgumentNullException();
        }

        #region Vistas Parciales (VP) AJAX
        [AllowAnonymous]
        public ActionResult cargarTablaTermometro(int? idTransf, string ubicadoT)
        {
            var listado = db.SubTermometrosTransfSubTransmision.Where(x => x.Id_Transformador == idTransf).ToList();

            ViewBag.termo = listado;
            ViewBag.cantidad = listado.Count();
            ViewBag.ubicado = ubicadoT;
            return PartialView("_VPTermometroSub");
        }
        #endregion

    }
}
