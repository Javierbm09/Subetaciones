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
    public class Sub_MttoBateriasEstacionariasController : Controller
    {
        private DBContext db = new DBContext();

        public async Task<ActionResult> Index()
        {
            var ListaMttos = new SubMttoBateriasEstacionariasRepositorio(db);
            return View(await ListaMttos.ObtenerMttos());
        }

        public ActionResult Details(int id_MBE, int id_Bateria, short redEA, string cod)
        {
            if ((id_MBE == 0) && (id_Bateria == 0) && (cod == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var repo = new Repositorio(db);
            var repoMtto = new SubMttoBateriasEstacionariasRepositorio(db);
            Sub_MttoBateriasEstacionariasViewModel dMttoBEVM = new Sub_MttoBateriasEstacionariasViewModel();
            DatosChapaBancoBateriaViewModel dcbbvm = repoMtto.mttoComponentes(id_Bateria, redEA, cod).First();


            dMttoBEVM.Sub_MBE = db.Sub_MttoBateriasEstacionarias.Where(c => c.id_Bateria == id_Bateria && c.EA_RedCD == redEA && c.subestacion == cod).First();
            dMttoBEVM.DatosCBBVM = dcbbvm;
            dMttoBEVM.Sub_MBEV = db.Sub_MttoBateriasEstacionarias_Vasos.Where(c => c.id_MttoBatEstacionarias == id_MBE && c.id_Bateria == id_Bateria && c.EA_RedCD == redEA && c.subestacion == cod).ToList();


            ViewBag.NroVasos = dcbbvm.CantidadVasos;
            ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();
            ViewBag.nivel = repoMtto.nivel();
            ViewBag.Observaciones = dMttoBEVM.Sub_MBE.Observaciones;
            ViewBag.IdMttoBE = dcbbvm.id_MttoBatEstacionarias;
            ViewBag.IdBateria = dcbbvm.Id_Bateria;
            ViewBag.RedEA = dcbbvm.id_EAdministrativa;

            if (dMttoBEVM.Sub_MBE.Mantenido == null)
            {
                dMttoBEVM.Sub_MBE.Mantenido = false;
            }

            ViewBag.Instrumento = new SelectList(repoMtto.tipoInstrumentos(dcbbvm.id_MttoBatEstacionarias, dcbbvm.Id_Bateria, (short)dcbbvm.id_EAdministrativa).ToList().Select(c => new { Value = c.Id_InstrumentoMedicion, Text = c.Instrumento + " - " + c.ModeloTipo }), "Value", "Text");

            return View(dMttoBEVM); 
        }

        public ActionResult Create()
        {
            var repo = new Repositorio(db);
            var repoMtto = new SubMttoBateriasEstacionariasRepositorio(db);

            //listados general
            ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();

            //listado banco de baterias
            ViewBag.TiposBaterias = new SelectList(repoMtto.listaBaterias("").Select(c => new { Value = c.Id_Bateria, Text = c.TipoBateria }), "Value", "Text");

            //listado nivel de electrolito
            ViewBag.nivel = repoMtto.nivel();

            return View();
        }

        // POST: Sub_MttoDistTransf/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sub_MttoBateriasEstacionariasViewModel dMttoBEVM)
        {
            var repo = new Repositorio(db);
            var repoMtto = new SubMttoBateriasEstacionariasRepositorio(db);

            if (ModelState.IsValid)
            {

                if (ValidarExisteMttoBatEst(dMttoBEVM.Sub_MBE.id_Bateria, dMttoBEVM.Sub_MBE.EA_RedCD, dMttoBEVM.Sub_MBE.subestacion))
                {
                    ModelState.AddModelError("Sub_MBE.MensajeExistenciaMtto", "Ya existe un mantenimiento a esta batería estacionaría.");
                }
                else
                {
                    dMttoBEVM.Sub_MBE.numAccion = repo.GetNumAccion("I", "RMB", 0);

                    db.Sub_MttoBateriasEstacionarias.Add(dMttoBEVM.Sub_MBE);
                    db.SaveChanges();
                    return RedirectToAction("ComponentesMtto", new
                    {
                        id_bateria = dMttoBEVM.Sub_MBE.id_Bateria,
                        redEA = dMttoBEVM.Sub_MBE.EA_RedCD,
                        sub = dMttoBEVM.Sub_MBE.subestacion,
                    });
                }

            }

            //listados general
            ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();

            //listado banco de baterias
            ViewBag.TiposBaterias = new SelectList(repoMtto.listaBaterias("").Select(c => new { Value = c.Id_Bateria, Text = c.TipoBateria }), "Value", "Text");

            //listado nivel de electrolito
            ViewBag.nivel = repoMtto.nivel();

            if (dMttoBEVM.Sub_MBE.Mantenido == null)
            {
                dMttoBEVM.Sub_MBE.Mantenido = false;
            }

            return View(dMttoBEVM);

        }

        public ActionResult ComponentesMtto(int id_bateria, short redEA, string sub)
        {
            var repo = new Repositorio(db);
            var repoMtto = new SubMttoBateriasEstacionariasRepositorio(db);

            Sub_MttoBateriasEstacionariasViewModel dMttoBEVM = new Sub_MttoBateriasEstacionariasViewModel();

            Sub_MttoBateriasEstacionarias dmtto = db.Sub_MttoBateriasEstacionarias.Where(c => c.id_Bateria == id_bateria && c.EA_RedCD == redEA && c.subestacion == sub).First();

            DatosChapaBancoBateriaViewModel dcbbvm = repoMtto.mttoComponentes(id_bateria, redEA, sub).First();

            if (dmtto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (dcbbvm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dMttoBEVM.Sub_MBE = dmtto;

            ViewBag.NroVasos = dcbbvm.CantidadVasos;
            ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();

            ViewBag.Sub = dmtto.subestacion;
            ViewBag.IdMttoBE = dcbbvm.id_MttoBatEstacionarias;
            ViewBag.IdBateria = dcbbvm.Id_Bateria;
            ViewBag.RedEA = dcbbvm.id_EAdministrativa;

            if (dMttoBEVM.Sub_MBE.Mantenido == null)
            {
                dMttoBEVM.Sub_MBE.Mantenido = false;
            }

            ViewBag.Instrumento = new SelectList(repoMtto.tipoInstrumentos(dcbbvm.id_MttoBatEstacionarias, dcbbvm.Id_Bateria, (short)dcbbvm.id_EAdministrativa).ToList().Select(c => new { Value = c.Id_InstrumentoMedicion, Text = c.Instrumento + " - " + c.ModeloTipo }), "Value", "Text");

            return View(dMttoBEVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComponentesExtras(Sub_MttoBateriasEstacionariasViewModel dMttoBEVM)
        {

            if (ModelState.IsValid)
            {
                var nroVaso = 1;

                if (dMttoBEVM.Sub_MBEV != null)
                {
                    foreach (var item in dMttoBEVM.Sub_MBEV)
                    {
                        item.nroVaso = (short)nroVaso++;
                        db.Sub_MttoBateriasEstacionarias_Vasos.Add(item);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("ComponentesMtto", new
            {
                id_bateria = dMttoBEVM.Sub_MBE.id_Bateria,
                redEA = dMttoBEVM.Sub_MBE.EA_RedCD,
                sub = dMttoBEVM.Sub_MBE.subestacion,
            });

        }

        [HttpPost]
        public async Task<ActionResult> InsertarInstrumento(int idIM, int idMBE, int idB, short redEA)
        {
            if ((idIM != 0) && (idMBE != 0) && (idB != 0))
            {
                await new SubMttoBateriasEstacionariasRepositorio(db).insertarInstrumento(idIM, idMBE, idB, redEA);
                return Json("true");
            }
            throw new ArgumentNullException();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaInstrumentos(int idMBE, int idBateria, short redEA)
        {
            var instrumentos = await new SubMttoBateriasEstacionariasRepositorio(db).listarInstrumentos(idMBE, idBateria, redEA);
            return Json(instrumentos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EliminarInstrumento(string instrumento, int idMBE, int idBateria, short redEA)
        {
            if (instrumento != null)
            {
                try
                {
                    Sub_MttoBateriaEstac_Instrumentos instr = new SubMttoBateriasEstacionariasRepositorio(db).EliminarInstrumento(instrumento, idMBE, idBateria, redEA).First();

                    if (instr != null)
                    {
                        try
                        {
                            //db.Sub_MttoBateriaEstac_Instrumentos.Remove(instr); 
                            db.Entry(instr).State = EntityState.Deleted;
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            throw (e);
                        }
                    }
                    else
                    {
                        throw new HttpException((int)HttpStatusCode.NotFound, "Lo sentimos no se puede eliminar, el circuito no existe.");
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

        public ActionResult Edit(int id_MBE, int id_Bateria, short redEA, string cod)
        {

            if ((id_MBE == 0) && (id_Bateria == 0) && (cod == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var repo = new Repositorio(db);
            var repoMtto = new SubMttoBateriasEstacionariasRepositorio(db);
            Sub_MttoBateriasEstacionariasViewModel dMttoBEVM = new Sub_MttoBateriasEstacionariasViewModel();
            DatosChapaBancoBateriaViewModel dcbbvm = repoMtto.mttoComponentes(id_Bateria, redEA, cod).First();


            dMttoBEVM.Sub_MBE = db.Sub_MttoBateriasEstacionarias.Where(c => c.id_Bateria == id_Bateria && c.EA_RedCD == redEA && c.subestacion == cod).First();
            dMttoBEVM.DatosCBBVM = dcbbvm;
            dMttoBEVM.Sub_MBEV = db.Sub_MttoBateriasEstacionarias_Vasos.Where(c => c.id_MttoBatEstacionarias == id_MBE && c.id_Bateria == id_Bateria && c.EA_RedCD == redEA && c.subestacion == cod).ToList();


            ViewBag.NroVasos = dcbbvm.CantidadVasos;
            ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();
            ViewBag.TipoB = new SelectList(repoMtto.listaBaterias(cod).Select(c => new { Value = c.Id_Bateria, Text = c.TipoBateria }), "Value", "Text");
            ViewBag.nivel = repoMtto.nivel();
            ViewBag.Observaciones = dMttoBEVM.Sub_MBE.Observaciones;
            ViewBag.IdMttoBE = dcbbvm.id_MttoBatEstacionarias;
            ViewBag.IdBateria = dcbbvm.Id_Bateria;
            ViewBag.RedEA = dcbbvm.id_EAdministrativa;

            if (dMttoBEVM.Sub_MBE.Mantenido == null)
            {
                dMttoBEVM.Sub_MBE.Mantenido = false;
            }

            ViewBag.Instrumento = new SelectList(repoMtto.tipoInstrumentos(dcbbvm.id_MttoBatEstacionarias, dcbbvm.Id_Bateria, (short)dcbbvm.id_EAdministrativa).ToList().Select(c => new { Value = c.Id_InstrumentoMedicion, Text = c.Instrumento + " - " + c.ModeloTipo }), "Value", "Text");

            return View(dMttoBEVM);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sub_MttoBateriasEstacionariasViewModel dMttoBEVM)
        {
            var repo = new Repositorio(db);
            var repoMtto = new SubMttoBateriasEstacionariasRepositorio(db);

            if (ModelState.IsValid)
            {
                dMttoBEVM.Sub_MBE.numAccion = repo.GetNumAccion("I", "RMB", 0);
                db.Entry(dMttoBEVM.Sub_MBE).State = EntityState.Modified;

                var nroVaso = 1;
                var i = 0;

                if (dMttoBEVM.Sub_MBEV != null)
                {
                    foreach (var item in dMttoBEVM.Sub_MBEV)
                    {
                        item.nroVaso = (short)nroVaso++;
                        db.Entry(dMttoBEVM.Sub_MBEV[i]).State = EntityState.Modified;
                        i++;
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Index");

            }

            DatosChapaBancoBateriaViewModel dcbbvm = repoMtto.mttoComponentes(dMttoBEVM.Sub_MBE.id_Bateria, dMttoBEVM.Sub_MBE.EA_RedCD, dMttoBEVM.Sub_MBE.subestacion).First();

            dMttoBEVM.Sub_MBE = db.Sub_MttoBateriasEstacionarias.Where(c => c.id_Bateria == dMttoBEVM.Sub_MBE.id_Bateria && c.EA_RedCD == dMttoBEVM.Sub_MBE.EA_RedCD && c.subestacion == dMttoBEVM.Sub_MBE.subestacion).First();
            dMttoBEVM.DatosCBBVM = dcbbvm;
            dMttoBEVM.Sub_MBEV = db.Sub_MttoBateriasEstacionarias_Vasos.Where(c => c.id_MttoBatEstacionarias == dMttoBEVM.Sub_MBEI.id_MttoBatEstacionarias && c.id_Bateria == dMttoBEVM.Sub_MBE.id_Bateria && c.EA_RedCD == dMttoBEVM.Sub_MBE.EA_RedCD && c.subestacion == dMttoBEVM.Sub_MBE.subestacion).ToList();


            ViewBag.NroVasos = dcbbvm.CantidadVasos;
            ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();
            ViewBag.TipoB = new SelectList(repoMtto.listaBaterias(dMttoBEVM.Sub_MBE.subestacion).Select(c => new { Value = c.Id_Bateria, Text = c.TipoBateria }), "Value", "Text");
            ViewBag.nivel = repoMtto.nivel();
            ViewBag.Observaciones = dMttoBEVM.Sub_MBE.Observaciones;
            ViewBag.IdMttoBE = dcbbvm.id_MttoBatEstacionarias;
            ViewBag.IdBateria = dcbbvm.Id_Bateria;
            ViewBag.RedEA = dcbbvm.id_EAdministrativa;
            ViewBag.Instrumento = new SelectList(repoMtto.tipoInstrumentos(dcbbvm.id_MttoBatEstacionarias, dcbbvm.Id_Bateria, (short)dcbbvm.id_EAdministrativa).ToList().Select(c => new { Value = c.Id_InstrumentoMedicion, Text = c.Instrumento + " - " + c.ModeloTipo }), "Value", "Text");

            if (dMttoBEVM.Sub_MBE.Mantenido == null)
            {
                dMttoBEVM.Sub_MBE.Mantenido = false;
            }

            return View(dMttoBEVM);
        }

        public ActionResult Eliminar(int idMBE, int idB, int redEA, string codSub)
        {
            try
            {
                List<Sub_MttoBateriaEstac_Instrumentos> subMBEI = db.Sub_MttoBateriaEstac_Instrumentos.Where(c => c.id_MttoBatEstacionarias == idMBE && c.id_Bateria == idB && c.EA_RedCD == redEA).ToList();
                if (subMBEI != null)
                {
                    db.Sub_MttoBateriaEstac_Instrumentos.RemoveRange(subMBEI);
                }

                List<Sub_MttoBateriasEstacionarias_Vasos> subMBEV = db.Sub_MttoBateriasEstacionarias_Vasos.Where(c => c.id_MttoBatEstacionarias == idMBE && c.id_Bateria == idB && c.EA_RedCD == redEA && c.subestacion == codSub).ToList();
                if (subMBEI != null)
                {
                    db.Sub_MttoBateriasEstacionarias_Vasos.RemoveRange(subMBEV);
                }

                Sub_MttoBateriasEstacionarias subMBE = db.Sub_MttoBateriasEstacionarias.Where(c => c.id_Bateria == idB && c.EA_RedCD == redEA && c.subestacion == codSub).First();
                db.Sub_MttoBateriasEstacionarias.Remove(subMBE);

                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ListadoMttos()
        {
            var ListaMtto = new SubMttoBateriasEstacionariasRepositorio(db);
            return PartialView("_VPMttoBateriasEstacionarias", await ListaMtto.ObtenerMttos());
        }

        private bool ValidarExisteMttoBatEst(int id_Bateria, int EA_RedCD, string sub)
        {
            var parametroid_Bateria = new SqlParameter("@id_Bateria", id_Bateria);
            var parametroEA_RedCD = new SqlParameter("@EA_RedCD", EA_RedCD);
            var parametrosub = new SqlParameter("@sub", sub);

            var cant = db.Database.SqlQuery<Sub_MttoBateriasEstacionarias>(@"SELECT  *
                                                   FROM    Sub_MttoBateriasEstacionarias
                                                   WHERE   id_Bateria = @id_Bateria
                                                   AND EA_RedCD = @EA_RedCD
                                                   AND subestacion = @sub", parametroid_Bateria, parametroEA_RedCD, parametrosub).ToList();
            return !(cant.Count == 0);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Cargar_bancos_Bateria(string codsub)
        {
            var repoMtto = new SubMttoBateriasEstacionariasRepositorio(db);
            ViewBag.TiposBaterias = new SelectList(repoMtto.listaBaterias(codsub).Select(c => new { Value = c.Id_Bateria, Text = c.TipoBateria }), "Value", "Text");

            return PartialView("_VPBancoBaterias");
        }

        public JsonResult Cargar_datos_Bateria(int id_tipoBateria, string codSub)
        {
            var repoMtto = new SubMttoBateriasEstacionariasRepositorio(db);
            return Json(repoMtto.redCorrienteDirecta(id_tipoBateria, codSub), JsonRequestBehavior.AllowGet);
        }

    }
}
