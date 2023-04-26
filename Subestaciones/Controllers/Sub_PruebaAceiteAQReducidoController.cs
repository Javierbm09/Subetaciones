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
    public class Sub_PruebaAceiteAQReducidoController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_PruebaAceiteAQReducido
        public async Task<ActionResult> Index()
        {
            var ListaAQR = new AnalisisQRRepositorio(db);
            return View(await ListaAQR.ObtenerListadoAnalisisQR());
        }

        // GET: Sub_PruebaAceiteAQReducido/Details/5
        public ActionResult Details(int t, int EA, DateTime fecha)
        {
            if ((t == null) && (EA == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var repoPrueba = new AnalisisQRRepositorio(db);
            var prueba = repoPrueba.ObtenerPrueba(t, EA, fecha).FirstOrDefault();
            //Sub_PruebaAceiteAQReducido sub_PruebaAceiteAQReducido = db.Sub_PruebaAceiteAQReducido.Find(t, EA, fecha);
            if (prueba == null)
            {
                ViewBag.mensaje = "No existen datos de la prueba de aceite - análisis químico reducido.";
                return View("~/Views/Shared/Error.cshtml");
            }
            return View(prueba);
        }

        // GET: Sub_PruebaAceiteAQReducido/Create
        public ActionResult Create()
        {
            var repo = new Repositorio(db);

            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            return View();
        }

        // POST: Sub_PruebaAceiteAQReducido/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_Transf,Id_EAdminTransf,Fecha,CodigoSub,Id_EAdministrativa,NumAccion,RealizadoenLaboraorio,NumeroControl,FechaSeleccion,FechaRecepcion,FechaInicio,FechaTerminacion,MuestreoSegProc,MuestreoSegProcPor,Clasificacion,EjecutadoPor,RevisadoPor,TempMuestra,Densidada20GC,NrodeNeutralizacion,AguaporKFisher,HumedadenPapel,TensionInterfacial,PuntoInflamacion,RigidezDielectrica,SedimentoyCienoPrecip,Viscosidada40GC,FactorDisipacionTAmb,FactorDisipaciona70GC,FactorDisipacion90GC,AspectoFisico,ResulSegunNormas,Observaciones,ResNro,EmitidoPor,CargoEmitidoPor,f,fechaIni,fechaRec,fechaSelec,fechaTer")] Sub_PruebaAceiteAQReducido sub_PruebaAceiteAQReducido)
        {
            Repositorio repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();
            if (ModelState.IsValid)
            {

                if (await ValidarExistePrueba(sub_PruebaAceiteAQReducido.Id_Transf, sub_PruebaAceiteAQReducido.CodigoSub, formatoFecha(sub_PruebaAceiteAQReducido.f)))
                {
                    sub_PruebaAceiteAQReducido.Fecha = formatoFecha(sub_PruebaAceiteAQReducido.f);
                    sub_PruebaAceiteAQReducido.FechaInicio = formatoFecha(sub_PruebaAceiteAQReducido.fechaIni);
                    sub_PruebaAceiteAQReducido.FechaRecepcion = formatoFecha(sub_PruebaAceiteAQReducido.fechaRec);
                    sub_PruebaAceiteAQReducido.FechaSeleccion = formatoFecha(sub_PruebaAceiteAQReducido.fechaSelec);
                    sub_PruebaAceiteAQReducido.FechaTerminacion = formatoFecha(sub_PruebaAceiteAQReducido.fechaTer);
                    sub_PruebaAceiteAQReducido.Id_EAdministrativa = (short)Id_Eadministrativa;
                    sub_PruebaAceiteAQReducido.NumAccion = repo.GetNumAccion("I", "SPA", 0);
                    db.Sub_PruebaAceiteAQReducido.Add(sub_PruebaAceiteAQReducido);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CodigoSub", "Ya existe una prueba al transformador en la fecha.");

                }

            }
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");

            return View(sub_PruebaAceiteAQReducido);
        }

        private async Task<bool> ValidarExistePrueba(int t, string Codigo, DateTime fecha)
        {
            var listaMttos = await new AnalisisQRRepositorio(db).ObtenerListadoAnalisisQR();

            return !listaMttos.Select(c => new { c.Id_Transf, c.Fecha, c.CodigoSub }).Any(c => c.Id_Transf == t && c.Fecha == fecha && c.CodigoSub == Codigo);
        }

        private DateTime formatoFecha(string fecha)
        {
            var fechaArray = fecha.Split('-');
            DateTime newFecha = new DateTime(Convert.ToInt32(fechaArray[0]), Convert.ToInt32(fechaArray[1]), Convert.ToInt32(fechaArray[2]));
            return newFecha;
        }

        // GET: Sub_PruebaAceiteAQReducido/Edit/5
        public ActionResult Edit(int t, int EA, DateTime fecha)
        {
            if ((t == null) && (EA == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var repoPrueba = new AnalisisQRRepositorio(db);
            var prueba = repoPrueba.ObtenerPrueba(t, EA, fecha).FirstOrDefault();
            //Sub_PruebaAceiteAQReducido sub_PruebaAceiteAQReducido = db.Sub_PruebaAceiteAQReducido.Find(t, EA, fecha);
            if (prueba == null)
            {
                ViewBag.mensaje = "No existen datos de la prueba de aceite - análisis químico reducido.";
                return View("~/Views/Shared/Error.cshtml");
            }
            return View(prueba);
        }

        // POST: Sub_PruebaAceiteAQReducido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Transf,Id_EAdminTransf,Fecha,CodigoSub,Id_EAdministrativa,NumAccion,RealizadoenLaboraorio,NumeroControl,FechaSeleccion,FechaRecepcion,FechaInicio,FechaTerminacion,MuestreoSegProc,MuestreoSegProcPor,Clasificacion,EjecutadoPor,RevisadoPor,TempMuestra,Densidada20GC,NrodeNeutralizacion,AguaporKFisher,HumedadenPapel,TensionInterfacial,PuntoInflamacion,RigidezDielectrica,SedimentoyCienoPrecip,Viscosidada40GC,FactorDisipacionTAmb,FactorDisipaciona70GC,FactorDisipacion90GC,AspectoFisico,ResulSegunNormas,Observaciones,ResNro,EmitidoPor,CargoEmitidoPor,f,fechaTer,fechaSelec,fechaRec,fechaIni")] Sub_PruebaAceiteAQReducido sub_PruebaAceiteAQReducido)
        {
            Repositorio repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();
            if (ModelState.IsValid)
            {
                sub_PruebaAceiteAQReducido.Fecha = formatoFecha(sub_PruebaAceiteAQReducido.f);
                sub_PruebaAceiteAQReducido.Id_EAdministrativa = (short)Id_Eadministrativa;
                sub_PruebaAceiteAQReducido.NumAccion = repo.GetNumAccion("M", "SPA", 0);
                db.Entry(sub_PruebaAceiteAQReducido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //return View(sub_PruebaAceiteAQReducido);
            var repoPrueba = new AnalisisQRRepositorio(db);

            var prueba = repoPrueba.ObtenerPrueba(sub_PruebaAceiteAQReducido.Id_Transf, sub_PruebaAceiteAQReducido.Id_EAdminTransf, formatoFecha(sub_PruebaAceiteAQReducido.f)).FirstOrDefault();
            return View(prueba);
        }

        // GET: Sub_PruebaAceiteAQReducido/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_PruebaAceiteAQReducido sub_PruebaAceiteAQReducido = db.Sub_PruebaAceiteAQReducido.Find(id);
            if (sub_PruebaAceiteAQReducido == null)
            {
                return HttpNotFound();
            }
            return View(sub_PruebaAceiteAQReducido);
        }

        // POST: Sub_PruebaAceiteAQReducido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sub_PruebaAceiteAQReducido sub_PruebaAceiteAQReducido = db.Sub_PruebaAceiteAQReducido.Find(id);
            db.Sub_PruebaAceiteAQReducido.Remove(sub_PruebaAceiteAQReducido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [TienePermiso(38)]// verifico que tenga permiso de crear y eliminar mtto
        public ActionResult Eliminar(int transf, int ea, DateTime fecha)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_PruebaAceiteAQReducido AQR = db.Sub_PruebaAceiteAQReducido.Find(transf, ea, fecha);
                db.Sub_PruebaAceiteAQReducido.Remove(AQR);
                int accion = br.GetNumAccion("B", "SPA", AQR.NumAccion);
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

        #region Vista Parciales
        [HttpPost]
        public async Task<ActionResult> ListadoAQR()
        {
            var ListaMtto = new AnalisisQRRepositorio(db);
            return PartialView("_VPPruebasAQR", await ListaMtto.ObtenerListadoAnalisisQR());
        }

        public ActionResult CargarTransf(string codsub)
        {
            var ListaTransf = new AnalisisQRRepositorio(db);
            ViewBag.listaTransf = ListaTransf.ObtenerListadoTransf(codsub);
            return PartialView("_VPListaTransf");
        }
        #endregion

        public JsonResult ObtenerPruebas(int t, int EA, string cod)
        {
            var repoPrueba = new AnalisisQRRepositorio(db);
            return Json(repoPrueba.ObtenerPrueba(t, EA, cod), JsonRequestBehavior.AllowGet);
        }
    }
}
