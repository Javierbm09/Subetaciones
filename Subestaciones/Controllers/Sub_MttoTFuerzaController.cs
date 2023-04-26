using Subestaciones.Models;
using Subestaciones.Models.Clases;
using Subestaciones.Models.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Subestaciones.Controllers
{
    public class Sub_MttoTFuerzaController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_MttoTFuerza
        public async Task<ActionResult> Index()
        {
            var ListaMttos = new Sub_MttoTFuerzaRepositorio(db);
            return View(await ListaMttos.ObtenerMttos());
        }

        public ActionResult Create()
        {
            var repo = new Repositorio(db);
            var repoMtto = new Sub_MttoTFuerzaRepositorio(db);

            //listados general
            ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().OrderBy(c => c.Codigo).ThenBy(c => c.NombreSubestacion).Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();

            //listado transformadores de fuerza
            ViewBag.TFuerza = new SelectList(db.TransformadoresTransmision.Where(c => c.Codigo == "").Select(c => new { Value = c.Id_Transformador, Text = c.Nombre }), "Value", "Text");

            return View();
        }

        // POST: Sub_MttoTFuerza/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sub_MttoTFuerzaViewModel dMttoTFVM)
        {
            var repo = new Repositorio(db);
            var repoMtto = new Sub_MttoTFuerzaRepositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();
            dMttoTFVM.Sub_MTF.Id_EAdministrativa = (short)Id_Eadministrativa;

            if (ModelState.IsValid)
            {

                if (ValidarExisteMttoTFuerza(dMttoTFVM.Sub_MTF.Id_TFuerza, dMttoTFVM.Sub_MTF.Id_EAdministrativaTFuerza, dMttoTFVM.Sub_MTF.Subestacion))
                {
                    ModelState.AddModelError("Sub_MTF.MensajeExistenciaMtto", "Ya existe un mantenimiento para este transformador de fuerza.");
                }
                else
                {

                    dMttoTFVM.Sub_MTF.Num_Accion = repo.GetNumAccion("I", "SMF", 0);

                    db.Sub_MttoTFuerza.Add(dMttoTFVM.Sub_MTF);
                    db.SaveChanges();
                    return RedirectToAction("ComponentesMtto", new
                    {
                        IdTFuerza = dMttoTFVM.Sub_MTF.Id_TFuerza,
                        IdEAdministrativaTFuerza = dMttoTFVM.Sub_MTF.Id_EAdministrativaTFuerza,
                        sub = dMttoTFVM.Sub_MTF.Subestacion,
                        tensionPrimaria = dMttoTFVM.DatosCTF.VoltajeP,
                        tensionSecundaria = dMttoTFVM.DatosCTF.VoltajeS,
                        tensionTerciaria = dMttoTFVM.DatosCTF.VoltajeT,
                        nroPosicion = dMttoTFVM.DatosCTF.NroPosiciones,
                    });
                }
            }

            //listados general
            ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().OrderBy(c => c.Codigo).ThenBy(c => c.NombreSubestacion).Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();

            //listado transformadores de fuerza
            ViewBag.TFuerza = new SelectList(db.TransformadoresTransmision.Where(c => c.Codigo == dMttoTFVM.Sub_MTF.Subestacion).Select(c => new { Value = c.Id_Transformador, Text = c.Nombre }), "Value", "Text");

            if (dMttoTFVM.Sub_MTF.Mantenido == null)
            {
                dMttoTFVM.Sub_MTF.Mantenido = false;
            }

            return View(dMttoTFVM);

        }


        public ActionResult ComponentesMtto(int IdTFuerza, int IdEAdministrativaTFuerza, string sub, double tensionPrimaria = 0, double tensionSecundaria = 0, double tensionTerciaria = 0, short nroPosicion = (short)0)
        {
            var repo = new Repositorio(db);
            var repoMtto = new Sub_MttoTFuerzaRepositorio(db);

            Sub_MttoTFuerzaViewModel dMttoTFVM = new Sub_MttoTFuerzaViewModel();

            Sub_MttoTFuerza dmtto = db.Sub_MttoTFuerza.Where(c => c.Id_TFuerza == IdTFuerza && c.Id_EAdministrativaTFuerza == IdEAdministrativaTFuerza && c.Subestacion == sub).First();

            if (dmtto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (tensionPrimaria != 0 && tensionSecundaria != 0 && tensionTerciaria != 0)
            {
                ViewBag.LugarMedicion = 4;
            }
            else
            {
                ViewBag.LugarMedicion = 3;
            }


            dMttoTFVM.Sub_MTF = dmtto;

            //ViewBag.NroVasos = dcbbvm.CantidadVasos;
            ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().OrderBy(c => c.Codigo).ThenBy(c => c.NombreSubestacion).Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();
            ViewBag.instAislEnrrollado = repoMtto.instrumentoUtilizadoAislamientoEnrrollado();
            ViewBag.instTDEnrrollado = repoMtto.instrumentoUtilizadoTDEnrrollado();

            ViewBag.Sub = dmtto.Subestacion;
            ViewBag.IdMttoTF = dmtto.Id_MttoTFuerza;
            ViewBag.IdTF = dmtto.Id_TFuerza;
            ViewBag.IdEATF = dmtto.Id_EAdministrativaTFuerza;

            ViewBag.tensionPrimaria = tensionPrimaria;
            ViewBag.tensionSecundaria = tensionSecundaria;
            ViewBag.tensionTerciaria = tensionTerciaria;
            
            ViewBag.nroDerivacion = nroPosicion;

            if (dMttoTFVM.Sub_MTF.Mantenido == null)
            {
                dMttoTFVM.Sub_MTF.Mantenido = false;
            }

            //ViewBag.Instrumento = new SelectList(repoMtto.tipoInstrumentos(dcbbvm.id_MttoBatEstacionarias, dcbbvm.Id_Bateria, (short)dcbbvm.id_EAdministrativa).ToList().Select(c => new { Value = c.Id_InstrumentoMedicion, Text = c.Instrumento + " - " + c.ModeloTipo }), "Value", "Text");

            return View(dMttoTFVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComponentesExtras(Sub_MttoTFuerzaViewModel dMttoTFVM)
        {

            dMttoTFVM.Sub_MTFA.Id_MttoTFuerza = dMttoTFVM.Sub_MTF.Id_MttoTFuerza;

            if (ModelState.IsValid)
            {
                //Mantenimiento auxiliar se arrastra por varios tap
                if (dMttoTFVM.Sub_MTFA != null)
                {
                    db.Sub_MttoTFuerzaAuxiliares.Add(dMttoTFVM.Sub_MTFA);
                }

                // Tap de enrrollado
                if (dMttoTFVM.Sub_MTFAE != null)
                {
                    foreach (var item in dMttoTFVM.Sub_MTFAE)
                    {
                        db.Sub_MttoTFuerzaAislamEnrollado.Add(item);
                    }
                }

                if (dMttoTFVM.Sub_MTFTDE != null)
                {
                    db.Sub_MttoTFuerzaTanDeltaEnrollado.Add(dMttoTFVM.Sub_MTFTDE);
                }

                // Tap de bushings
                if (dMttoTFVM.Sub_MTFPB != null)
                {
                    db.Sub_MttoTFuerzaPresionBushing.Add(dMttoTFVM.Sub_MTFPB);
                }

                if (dMttoTFVM.Sub_MTFAB != null)
                {
                    foreach (var item in dMttoTFVM.Sub_MTFAB)
                    {
                        db.Sub_MttoTFuerzaAislamBushings.Add(item);
                    }
                }

                if (dMttoTFVM.Sub_MTFTDB != null)
                {
                    db.Sub_MttoTFuerzaTanDeltaBushings.Add(dMttoTFVM.Sub_MTFTDB);
                }

                if (dMttoTFVM.Sub_MTFRT != null)
                {
                    foreach (var item in dMttoTFVM.Sub_MTFRT)
                    {
                        db.Sub_MttoTFuerzaRelacTransf.Add(item);
                    }
                }
                
                if (dMttoTFVM.Sub_MTFRO != null)
                {
                    foreach (var item in dMttoTFVM.Sub_MTFRO)
                    {
                        db.Sub_MttoTFuerzaResistOhm.Add(item);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("ComponentesMtto", new
            {
                IdTFuerza = dMttoTFVM.Sub_MTF.Id_TFuerza,
                IdEAdministrativaTFuerza = dMttoTFVM.Sub_MTF.Id_EAdministrativaTFuerza,
                sub = dMttoTFVM.Sub_MTF.Subestacion,
                tensionPrimaria = dMttoTFVM.VoltajeP,
                tensionSecundaria = dMttoTFVM.VoltajeS,
                tensionTerciaria = dMttoTFVM.VoltajeT,
            });

        }


        private bool ValidarExisteMttoTFuerza(int idTFuerza, int idEATFuerza, string sub)
        {
            var parametroidTFuerza = new SqlParameter("@idTFuerza", idTFuerza);
            var parametroidEATFuerza = new SqlParameter("@idEATFuerza", idEATFuerza);
            var parametrosub = new SqlParameter("@sub", sub);

            var cant = db.Database.SqlQuery<Sub_MttoTFuerza>(@"SELECT  *
                                                   FROM    Sub_MttoTFuerza
                                                   WHERE   Id_TFuerza = @idTFuerza
                                                   AND Id_EAdministrativaTFuerza = @idEATFuerza
                                                   AND Subestacion = @sub", parametroidTFuerza, parametroidEATFuerza, parametrosub).ToList();
            return !(cant.Count == 0);

        }

        //public ActionResult Details(int id_MBE, int id_Bateria, short redEA, string cod)
        //{
        //    if ((id_MBE == 0) && (id_Bateria == 0) && (cod == null))
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var repo = new Repositorio(db);
        //    var repoMtto = new SubMttoBateriasEstacionariasRepositorio(db);
        //    Sub_MttoBateriasEstacionariasViewModel dMttoBEVM = new Sub_MttoBateriasEstacionariasViewModel();
        //    DatosChapaBancoBateriaViewModel dcbbvm = repoMtto.mttoComponentes(id_Bateria, redEA, cod).First();


        //    dMttoBEVM.Sub_MBE = db.Sub_MttoBateriasEstacionarias.Where(c => c.id_Bateria == id_Bateria && c.EA_RedCD == redEA && c.subestacion == cod).First();
        //    dMttoBEVM.DatosCBBVM = dcbbvm;
        //    dMttoBEVM.Sub_MBEV = db.Sub_MttoBateriasEstacionarias_Vasos.Where(c => c.id_MttoBatEstacionarias == id_MBE && c.id_Bateria == id_Bateria && c.EA_RedCD == redEA && c.subestacion == cod).ToList();


        //    ViewBag.NroVasos = dcbbvm.CantidadVasos;
        //    ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
        //    ViewBag.TiposMtto = repoMtto.tipoMtto();
        //    ViewBag.personal = repo.RealizadoPor();
        //    ViewBag.nivel = repoMtto.nivel();
        //    ViewBag.Observaciones = dMttoBEVM.Sub_MBE.Observaciones;
        //    ViewBag.IdMttoBE = dcbbvm.id_MttoBatEstacionarias;
        //    ViewBag.IdBateria = dcbbvm.Id_Bateria;
        //    ViewBag.RedEA = dcbbvm.id_EAdministrativa;

        //    if (dMttoBEVM.Sub_MBE.Mantenido == null)
        //    {
        //        dMttoBEVM.Sub_MBE.Mantenido = false;
        //    }

        //    ViewBag.Instrumento = new SelectList(repoMtto.tipoInstrumentos(dcbbvm.id_MttoBatEstacionarias, dcbbvm.Id_Bateria, (short)dcbbvm.id_EAdministrativa).ToList().Select(c => new { Value = c.Id_InstrumentoMedicion, Text = c.Instrumento + " - " + c.ModeloTipo }), "Value", "Text");

        //    return View(dMttoBEVM);
        //}




        [HttpPost]
        public JsonResult InsertarTDBushings(int idMTF, string sec = "", string esq = "", double cap = 0, double tanD = 0)
        {
            if (idMTF != 0)
            {
                new Sub_MttoTFuerzaRepositorio(db).InsertarTDBushings(idMTF, sec, esq, cap, tanD);
                return Json("true");
            }
            throw new ArgumentNullException();
        }

        [HttpPost]
        public JsonResult ActualizarTDBushings(Sub_MttoTFuerzaTanDeltaBushings datosFilaTDBushing, int idMTF)
        {
            datosFilaTDBushing.Id_MttoTFuerza = idMTF;
            if (idMTF != 0 && datosFilaTDBushing.Id_TanDeltaBushing != 0)
            {
                new Sub_MttoTFuerzaRepositorio(db).ActualizarTDBushings(datosFilaTDBushing.Id_TanDeltaBushing,
                    datosFilaTDBushing.Id_MttoTFuerza, datosFilaTDBushing.Seccion, datosFilaTDBushing.Esquema,
                    (double)datosFilaTDBushing.Capacitancia, (double)datosFilaTDBushing.TanDelta);
                return Json("true");
            }
            throw new ArgumentNullException();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaTDBushings(int idMTF)
        {
            var listaTDBushings = await new Sub_MttoTFuerzaRepositorio(db).listarTDBushings(idMTF);
            return Json(listaTDBushings, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EliminarTDBushings(int idTDB)
        {
            if (idTDB != 0)
            {
                try
                {
                    Sub_MttoTFuerzaTanDeltaBushings subMTFTDB = db.Sub_MttoTFuerzaTanDeltaBushings.Where(c => c.Id_TanDeltaBushing == idTDB).First();


                    if (subMTFTDB != null)
                    {
                        try
                        {
                            db.Entry(subMTFTDB).State = EntityState.Deleted;
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw (e);
                        }
                    }
                    else
                    {
                        throw new HttpException((int)HttpStatusCode.NotFound, "Lo sentimos no se puede eliminar.");
                    }

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
        
        [HttpPost]
        public JsonResult InsertarCorrienteExit(int idMTF, int tap = 0, double a = 0, double b = 0, double c = 0, double porcDesv = 0)
        {
            if (idMTF != 0)
            {
                new Sub_MttoTFuerzaRepositorio(db).InsertarCorrienteExit(idMTF, tap, a, b, c, porcDesv);
                return Json("true");
            }
            throw new ArgumentNullException();
        }

        //[HttpPost]
        //public JsonResult ActualizarCorrienteExit(Sub_MttoTFuerzaCorrienteExit datosFilaCorrienteExit, int idMTF)
        //{
        //    datosFilaCorrienteExit.Id_MttoTFuerza = idMTF;
        //    if (idMTF != 0 && datosFilaCorrienteExit.Id_CorrienteExit != 0)
        //    {
        //        new Sub_MttoTFuerzaRepositorio(db).ActualizarCorrienteExit(datosFilaCorrienteExit.Id_CorrienteExit,
        //            datosFilaCorrienteExit.Id_MttoTFuerza,
        //            datosFilaCorrienteExit.A_0,
        //            datosFilaCorrienteExit.B_0,
        //            datosFilaCorrienteExit.C_0, 
        //            (double)datosFilaCorrienteExit.PorcientoDesviacion);
        //        return Json("true");
        //    }
        //    throw new ArgumentNullException();
        //}

        [HttpGet]
        public async Task<JsonResult> ObtenerListaCorrienteExit(int idMTF)
        {
            var listaCorrienteExit = await new Sub_MttoTFuerzaRepositorio(db).ListarCorrienteExit(idMTF);
            return Json(listaCorrienteExit, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EliminarCorrienteExit(int idCE)
        {
            if (idCE != 0)
            {
                try
                {
                    Sub_MttoTFuerzaCorrienteExit subMTFCE = db.Sub_MttoTFuerzaCorrienteExit.Where(c => c.Id_CorrienteExit == idCE).First();


                    if (subMTFCE != null)
                    {
                        try
                        {
                            db.Entry(subMTFCE).State = EntityState.Deleted;
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw (e);
                        }
                    }
                    else
                    {
                        throw new HttpException((int)HttpStatusCode.NotFound, "Lo sentimos no se puede eliminar.");
                    }

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

        [HttpPost]
        public JsonResult InsertarTDEnrrollado(int idMTF, string sec = "", string esq = "", double cap = 0, double tanD = 0)
        {
            if (idMTF != 0)
            {
                new Sub_MttoTFuerzaRepositorio(db).InsertarTDEnrrollado(idMTF, sec, esq, cap, tanD);
                return Json("true");
            }
            throw new ArgumentNullException();
        }

        [HttpPost]
        public JsonResult ActualizarTDEnrrollado(Sub_MttoTFuerzaTanDeltaEnrollado datosFilaTDEnrrollado, int idMTF)
        {
            datosFilaTDEnrrollado.Id_MttoTFuerza = idMTF;
            if (idMTF != 0 && datosFilaTDEnrrollado.Id_TanDeltaEnrollados != 0)
            {
                new Sub_MttoTFuerzaRepositorio(db).ActualizarTDEnrrollado(datosFilaTDEnrrollado.Id_TanDeltaEnrollados,
                    datosFilaTDEnrrollado.Id_MttoTFuerza, datosFilaTDEnrrollado.Seccion, datosFilaTDEnrrollado.Esquema,
                    (double)datosFilaTDEnrrollado.Capacitancia, (double)datosFilaTDEnrrollado.TangenteDelta);
                return Json("true");
            }
            throw new ArgumentNullException();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaTDEnrrollado(int idMTF)
        {
            var listaTDEnrrollado = await new Sub_MttoTFuerzaRepositorio(db).listarTDEnrrollado(idMTF);
            return Json(listaTDEnrrollado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EliminarTDEnrrollado(int idTDE)
        {
            if (idTDE != 0)
            {
                try
                {
                    Sub_MttoTFuerzaTanDeltaEnrollado subMTFTDE = db.Sub_MttoTFuerzaTanDeltaEnrollado.Where(c => c.Id_TanDeltaEnrollados == idTDE).First();


                    if (subMTFTDE != null)
                    {
                        try
                        {
                            db.Entry(subMTFTDE).State = EntityState.Deleted;
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw (e);
                        }
                    }
                    else
                    {
                        throw new HttpException((int)HttpStatusCode.NotFound, "Lo sentimos no se puede eliminar.");
                    }

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

        //public ActionResult Edit(int id_MBE, int id_Bateria, short redEA, string cod)
        //{

        //    if ((id_MBE == 0) && (id_Bateria == 0) && (cod == null))
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var repo = new Repositorio(db);
        //    var repoMtto = new SubMttoBateriasEstacionariasRepositorio(db);
        //    Sub_MttoBateriasEstacionariasViewModel dMttoBEVM = new Sub_MttoBateriasEstacionariasViewModel();
        //    DatosChapaBancoBateriaViewModel dcbbvm = repoMtto.mttoComponentes(id_Bateria, redEA, cod).First();


        //    dMttoBEVM.Sub_MBE = db.Sub_MttoBateriasEstacionarias.Where(c => c.id_Bateria == id_Bateria && c.EA_RedCD == redEA && c.subestacion == cod).First();
        //    dMttoBEVM.DatosCBBVM = dcbbvm;
        //    dMttoBEVM.Sub_MBEV = db.Sub_MttoBateriasEstacionarias_Vasos.Where(c => c.id_MttoBatEstacionarias == id_MBE && c.id_Bateria == id_Bateria && c.EA_RedCD == redEA && c.subestacion == cod).ToList();


        //    ViewBag.NroVasos = dcbbvm.CantidadVasos;
        //    ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
        //    ViewBag.TiposMtto = repoMtto.tipoMtto();
        //    ViewBag.personal = repo.RealizadoPor();
        //    ViewBag.TipoB = new SelectList(repoMtto.listaBaterias(cod).Select(c => new { Value = c.Id_Bateria, Text = c.TipoBateria }), "Value", "Text");
        //    ViewBag.nivel = repoMtto.nivel();
        //    ViewBag.Observaciones = dMttoBEVM.Sub_MBE.Observaciones;
        //    ViewBag.IdMttoBE = dcbbvm.id_MttoBatEstacionarias;
        //    ViewBag.IdBateria = dcbbvm.Id_Bateria;
        //    ViewBag.RedEA = dcbbvm.id_EAdministrativa;

        //    if (dMttoBEVM.Sub_MBE.Mantenido == null)
        //    {
        //        dMttoBEVM.Sub_MBE.Mantenido = false;
        //    }

        //    ViewBag.Instrumento = new SelectList(repoMtto.tipoInstrumentos(dcbbvm.id_MttoBatEstacionarias, dcbbvm.Id_Bateria, (short)dcbbvm.id_EAdministrativa).ToList().Select(c => new { Value = c.Id_InstrumentoMedicion, Text = c.Instrumento + " - " + c.ModeloTipo }), "Value", "Text");

        //    return View(dMttoBEVM);
        //}

        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Sub_MttoBateriasEstacionariasViewModel dMttoBEVM)
        //{
        //    var repo = new Repositorio(db);
        //    var repoMtto = new SubMttoBateriasEstacionariasRepositorio(db);

        //    if (ModelState.IsValid)
        //    {
        //        dMttoBEVM.Sub_MBE.numAccion = repo.GetNumAccion("I", "RMB", 0);
        //        db.Entry(dMttoBEVM.Sub_MBE).State = EntityState.Modified;

        //        var nroVaso = 1;
        //        var i = 0;

        //        if (dMttoBEVM.Sub_MBEV != null)
        //        {
        //            foreach (var item in dMttoBEVM.Sub_MBEV)
        //            {
        //                item.nroVaso = (short)nroVaso++;
        //                db.Entry(dMttoBEVM.Sub_MBEV[i]).State = EntityState.Modified;
        //                i++;
        //            }
        //        }

        //        db.SaveChanges();

        //        return RedirectToAction("Index");

        //    }

        //    DatosChapaBancoBateriaViewModel dcbbvm = repoMtto.mttoComponentes(dMttoBEVM.Sub_MBE.id_Bateria, dMttoBEVM.Sub_MBE.EA_RedCD, dMttoBEVM.Sub_MBE.subestacion).First();

        //    dMttoBEVM.Sub_MBE = db.Sub_MttoBateriasEstacionarias.Where(c => c.id_Bateria == dMttoBEVM.Sub_MBE.id_Bateria && c.EA_RedCD == dMttoBEVM.Sub_MBE.EA_RedCD && c.subestacion == dMttoBEVM.Sub_MBE.subestacion).First();
        //    dMttoBEVM.DatosCBBVM = dcbbvm;
        //    dMttoBEVM.Sub_MBEV = db.Sub_MttoBateriasEstacionarias_Vasos.Where(c => c.id_MttoBatEstacionarias == dMttoBEVM.Sub_MBEI.id_MttoBatEstacionarias && c.id_Bateria == dMttoBEVM.Sub_MBE.id_Bateria && c.EA_RedCD == dMttoBEVM.Sub_MBE.EA_RedCD && c.subestacion == dMttoBEVM.Sub_MBE.subestacion).ToList();


        //    ViewBag.NroVasos = dcbbvm.CantidadVasos;
        //    ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
        //    ViewBag.TiposMtto = repoMtto.tipoMtto();
        //    ViewBag.personal = repo.RealizadoPor();
        //    ViewBag.TipoB = new SelectList(repoMtto.listaBaterias(dMttoBEVM.Sub_MBE.subestacion).Select(c => new { Value = c.Id_Bateria, Text = c.TipoBateria }), "Value", "Text");
        //    ViewBag.nivel = repoMtto.nivel();
        //    ViewBag.Observaciones = dMttoBEVM.Sub_MBE.Observaciones;
        //    ViewBag.IdMttoBE = dcbbvm.id_MttoBatEstacionarias;
        //    ViewBag.IdBateria = dcbbvm.Id_Bateria;
        //    ViewBag.RedEA = dcbbvm.id_EAdministrativa;
        //    ViewBag.Instrumento = new SelectList(repoMtto.tipoInstrumentos(dcbbvm.id_MttoBatEstacionarias, dcbbvm.Id_Bateria, (short)dcbbvm.id_EAdministrativa).ToList().Select(c => new { Value = c.Id_InstrumentoMedicion, Text = c.Instrumento + " - " + c.ModeloTipo }), "Value", "Text");

        //    if (dMttoBEVM.Sub_MBE.Mantenido == null)
        //    {
        //        dMttoBEVM.Sub_MBE.Mantenido = false;
        //    }

        //    return View(dMttoBEVM);
        //}

        public ActionResult Eliminar(int idMTF, int idTF, int idEATF, short idEA, string codSub)
        {
            try
            {
                //List<Sub_MttoBateriaEstac_Instrumentos> subMBEI = db.Sub_MttoBateriaEstac_Instrumentos.Where(c => c.id_MttoBatEstacionarias == idMBE && c.id_Bateria == idB && c.EA_RedCD == redEA).ToList();
                //if (subMBEI != null)
                //{
                //    db.Sub_MttoBateriaEstac_Instrumentos.RemoveRange(subMBEI);
                //}

                //List<Sub_MttoBateriasEstacionarias_Vasos> subMBEV = db.Sub_MttoBateriasEstacionarias_Vasos.Where(c => c.id_MttoBatEstacionarias == idMBE && c.id_Bateria == idB && c.EA_RedCD == redEA && c.subestacion == codSub).ToList();
                //if (subMBEI != null)
                //{
                //    db.Sub_MttoBateriasEstacionarias_Vasos.RemoveRange(subMBEV);
                //}

                //Sub_MttoBateriasEstacionarias subMBE = db.Sub_MttoBateriasEstacionarias.Where(c => c.id_Bateria == idB && c.EA_RedCD == redEA && c.subestacion == codSub).First();
                //db.Sub_MttoBateriasEstacionarias.Remove(subMBE);

                //db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ListadoMttoTFuerza()
        {
            var ListaMtto = new Sub_MttoTFuerzaRepositorio(db);
            return PartialView("_VPMttoTFuerza", await ListaMtto.ObtenerMttos());
        }



        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        public ActionResult Cargar_transformador_fuerza(string codsub)
        {
            var repoMtto = new Sub_MttoTFuerzaRepositorio(db);
            ViewBag.TFuerza = new SelectList(repoMtto.listaTFuerza(codsub).Select(c => new { Value = c.Id_Transformador, Text = c.Nombre }), "Value", "Text");

            return PartialView("_VPTFuerza");
        }

        public JsonResult Cargar_datos_TFuerza(string codSub, int idT)
        {
            var repoMtto = new Sub_MttoTFuerzaRepositorio(db);
            return Json(repoMtto.datosParaCadaTFuerza(codSub, idT), JsonRequestBehavior.AllowGet);
        }
    }
}