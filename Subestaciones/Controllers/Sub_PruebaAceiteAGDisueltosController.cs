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
    public class Sub_PruebaAceiteAGDisueltosController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_PruebaAceiteAGDisueltos
        public async Task<ActionResult> Index()
        {
            var ListaAQR = new AnalisisGDRepositorio(db);
            return View(await ListaAQR.ObtenerListadoAnalisisGD());
        }

        // GET: Sub_PruebaAceiteAGDisueltos/Details/5
        public ActionResult Details(int t, int EA, DateTime fecha)
        {
            if ((t == null) && (EA == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var repoPrueba = new AnalisisGDRepositorio(db);
            var prueba = repoPrueba.ObtenerPrueba(t, EA, fecha).FirstOrDefault();
            //Sub_PruebaAceiteAQReducido sub_PruebaAceiteAQReducido = db.Sub_PruebaAceiteAQReducido.Find(t, EA, fecha);
            if (prueba == null)
            {
                ViewBag.mensaje = "No existen datos de la prueba de aceite - análisis gases disueltos.";
                return View("~/Views/Shared/Error.cshtml");
            }
            return View(prueba);
        }

        // GET: Sub_PruebaAceiteAGDisueltos/Create
        public async Task<ActionResult> Create()
        {
            var repo = new Repositorio(db);

            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            return View();
        }

        // POST: Sub_PruebaAceiteAGDisueltos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_Transf,Id_EAdminTransf,Fecha,CodigoSub,Id_EAdministrativa,NumAccion,RealizadoenLaboraorio,NumeroControl,FechaSeleccion,FechaRecepcion,FechaInicio,FechaTerminacion,MuestreoSegProc,MuestreadoPor,EjecutadoPor,RevisadoPor,ResulSegunNormas,Observaciones,ResNro,EmitidoPor,CargoEmitidoPor,MetodoEnsayo,NormaMuestreo,H2Hidrogeno,CH4Metano,C2H6Etano,C2H4Etileno,C2H2Acetileno,COMonoxidoCarbono,TempAceite,CO2DioxidoCarbono, f, fSelec, fIni, fRec, fTer")] Sub_PruebaAceiteAGDisueltos sub_PruebaAceiteAGDisueltos)
        {
            Repositorio repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();
            if (ModelState.IsValid)
            {

                if (await ValidarExistePrueba(sub_PruebaAceiteAGDisueltos.Id_Transf, sub_PruebaAceiteAGDisueltos.CodigoSub, formatoFecha(sub_PruebaAceiteAGDisueltos.f)))
                {
                    sub_PruebaAceiteAGDisueltos.Fecha = formatoFecha(sub_PruebaAceiteAGDisueltos.f);
                    sub_PruebaAceiteAGDisueltos.FechaInicio = formatoFecha(sub_PruebaAceiteAGDisueltos.fIni);
                    sub_PruebaAceiteAGDisueltos.FechaRecepcion = formatoFecha(sub_PruebaAceiteAGDisueltos.fRec);
                    sub_PruebaAceiteAGDisueltos.FechaSeleccion = formatoFecha(sub_PruebaAceiteAGDisueltos.fSelec);
                    sub_PruebaAceiteAGDisueltos.FechaTerminacion = formatoFecha(sub_PruebaAceiteAGDisueltos.fTer);
                    sub_PruebaAceiteAGDisueltos.Id_EAdministrativa = (short)Id_Eadministrativa;
                    sub_PruebaAceiteAGDisueltos.NumAccion = repo.GetNumAccion("I", "SPA", 0);
                    db.Sub_PruebaAceiteAGDisueltos.Add(sub_PruebaAceiteAGDisueltos);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CodigoSub", "Ya existe una prueba al transformador en la fecha.");

                }

            }
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");

            return View(sub_PruebaAceiteAGDisueltos);
        }

        private async Task<bool> ValidarExistePrueba(int t, string Codigo, DateTime fecha)
        {
            var listaMttos = await new AnalisisGDRepositorio(db).ObtenerListadoAnalisisGD();

            return !listaMttos.Select(c => new { c.Id_Transf, c.Fecha, c.CodigoSub }).Any(c => c.Id_Transf == t && c.Fecha == fecha && c.CodigoSub == Codigo);
        }

        private DateTime formatoFecha(string fecha)
        {
            var fechaArray = fecha.Split('-');
            DateTime newFecha = new DateTime(Convert.ToInt32(fechaArray[0]), Convert.ToInt32(fechaArray[1]), Convert.ToInt32(fechaArray[2]));
            return newFecha;
        }
        // GET: Sub_PruebaAceiteAGDisueltos/Edit/5
        public ActionResult Edit(int t, int EA, DateTime fecha)
        {
            if ((t == null) && (EA == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var repoPrueba = new AnalisisGDRepositorio(db);
            var prueba = repoPrueba.ObtenerPrueba(t, EA, fecha).FirstOrDefault();
            //Sub_PruebaAceiteAQReducido sub_PruebaAceiteAQReducido = db.Sub_PruebaAceiteAQReducido.Find(t, EA, fecha);
            if (prueba == null)
            {
                ViewBag.mensaje = "No existen datos de la prueba de aceite - análisis gases disueltos.";
                return View("~/Views/Shared/Error.cshtml");
            }
            return View(prueba);
        }

        // POST: Sub_PruebaAceiteAGDisueltos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Transf,Id_EAdminTransf,Fecha,CodigoSub,Id_EAdministrativa,NumAccion,RealizadoenLaboraorio,NumeroControl,FechaSeleccion,FechaRecepcion,FechaInicio,FechaTerminacion,MuestreoSegProc,MuestreadoPor,EjecutadoPor,RevisadoPor,ResulSegunNormas,Observaciones,ResNro,EmitidoPor,CargoEmitidoPor,MetodoEnsayo,NormaMuestreo,H2Hidrogeno,CH4Metano,C2H6Etano,C2H4Etileno,C2H2Acetileno,COMonoxidoCarbono,TempAceite,CO2DioxidoCarbonof,f, fSelec, fIni, fRec, fTer")] Sub_PruebaAceiteAGDisueltos sub_PruebaAceiteAGDisueltos)
        {
            Repositorio repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();
            if (ModelState.IsValid)
            {
                sub_PruebaAceiteAGDisueltos.Fecha = formatoFecha(sub_PruebaAceiteAGDisueltos.f);
                //sub_PruebaAceiteAGDisueltos.FechaInicio = formatoFecha(sub_PruebaAceiteAGDisueltos.fIni);
                //sub_PruebaAceiteAGDisueltos.FechaRecepcion = formatoFecha(sub_PruebaAceiteAGDisueltos.fRec);
                //sub_PruebaAceiteAGDisueltos.FechaSeleccion = formatoFecha(sub_PruebaAceiteAGDisueltos.fSelec);
                //sub_PruebaAceiteAGDisueltos.FechaTerminacion = formatoFecha(sub_PruebaAceiteAGDisueltos.fTer);
                sub_PruebaAceiteAGDisueltos.Id_EAdministrativa = (short)Id_Eadministrativa;
                sub_PruebaAceiteAGDisueltos.NumAccion = repo.GetNumAccion("M", "SPA", 0);
                db.Entry(sub_PruebaAceiteAGDisueltos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var repoPrueba = new AnalisisGDRepositorio(db);

            var prueba = repoPrueba.ObtenerPrueba(sub_PruebaAceiteAGDisueltos.Id_Transf, sub_PruebaAceiteAGDisueltos.Id_EAdminTransf, formatoFecha(sub_PruebaAceiteAGDisueltos.f)).FirstOrDefault();
            return View(prueba);
        }

        // GET: Sub_PruebaAceiteAGDisueltos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_PruebaAceiteAGDisueltos sub_PruebaAceiteAGDisueltos = db.Sub_PruebaAceiteAGDisueltos.Find(id);
            if (sub_PruebaAceiteAGDisueltos == null)
            {
                return HttpNotFound();
            }
            return View(sub_PruebaAceiteAGDisueltos);
        }

        [TienePermiso(38)]// verifico que tenga permiso de crear y eliminar mtto
        public ActionResult Eliminar(int transf, int ea, DateTime fecha)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_PruebaAceiteAGDisueltos AGD = db.Sub_PruebaAceiteAGDisueltos.Find(transf, ea, fecha);
                db.Sub_PruebaAceiteAGDisueltos.Remove(AGD);
                int accion = br.GetNumAccion("B", "SPA", AGD.NumAccion);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        // POST: Sub_PruebaAceiteAGDisueltos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sub_PruebaAceiteAGDisueltos sub_PruebaAceiteAGDisueltos = db.Sub_PruebaAceiteAGDisueltos.Find(id);
            db.Sub_PruebaAceiteAGDisueltos.Remove(sub_PruebaAceiteAGDisueltos);
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

        public JsonResult ObtenerPruebas(int t, int EA, string cod)
        {
            var repoPrueba = new AnalisisGDRepositorio(db);
            return Json(repoPrueba.ObtenerPrueba(t, EA, cod), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> ListadoAGD()
        {
            var ListaPruebas = new AnalisisGDRepositorio(db);
            return PartialView("_VPPruebasAGD", await ListaPruebas.ObtenerListadoAnalisisGD());
        }
    }
}
