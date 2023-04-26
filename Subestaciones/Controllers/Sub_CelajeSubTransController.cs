using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;
using Subestaciones.Models.Repositorio;
using Subestaciones.Models.Clases;
using System.Web.Routing;

namespace Subestaciones.Controllers
{
    public class Sub_CelajeSubTransController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_Celaje
        public async Task<ActionResult> Index()
        {
            var ListaInsps = new Sub_CelajeInspSubTransRepositorio(db);
            return View(await ListaInsps.ObtenerSub_CelajeSubTrans());
        }

        // GET: Sub_Celaje/Details/5
        public async Task<ActionResult> Details(string nombInsp, string codigo, DateTime fechaIns)
        {
            Repositorio repo = new Repositorio(db);
            var repoInsp = new Sub_CelajeInspSubTransRepositorio(db);
            var repoInspAspect = new Sub_InspAspectosSubTransRepositorio(db);
            Sub_CelajeViewModel sub_CelajeViewModel = new Sub_CelajeViewModel();

            sub_CelajeViewModel.sub_Celaje = db.Sub_Celaje.Find(nombInsp, codigo, fechaIns); ;

            if (sub_CelajeViewModel.sub_Celaje == null)
            {
                ViewBag.mensaje = "No existen datos del Sub Celaje";
                return View("~/Views/Shared/Error.cshtml");
            }
            sub_CelajeViewModel.sub_Celaje = db.Sub_Celaje.Where(s => s.NombreCelaje == nombInsp && s.CodigoSub == codigo && s.fecha == fechaIns).FirstOrDefault();
            sub_CelajeViewModel.sub_InspAspectosSubTransViewModel = await repoInspAspect.ObtenerSub_InspAspectos(codigo, fechaIns);
            sub_CelajeViewModel.personal = db.Personal.Where(s => s.Id_Persona == sub_CelajeViewModel.sub_Celaje.RealizadoPor).FirstOrDefault();


            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");

            ViewBag.personal = repo.RealizadoPor();
            ViewBag.tipo = repoInsp.tipoInsp();
            ViewBag.cant = 0;
            return View(sub_CelajeViewModel);
        }

        // GET: Sub_Celaje/Create
        public ActionResult Create()
        {
            var repo = new Repositorio(db);
            var repoInsp = new Sub_CelajeInspSubTransRepositorio(db);

            ViewBag.ListaSub = new SelectList(repo.subT().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();
            ViewBag.tipo = repoInsp.tipoInsp();
            return View();
        }

        // POST: Sub_Celaje/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Sub_Celaje sub_Celaje)
        {
            Repositorio repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();
            var repoInsp = new Sub_CelajeInspSubTransRepositorio(db);

            if (await ValidarExisteIns(sub_Celaje.NombreCelaje, sub_Celaje.CodigoSub, sub_Celaje.fecha))
            {
                ModelState.AddModelError("NombreCelaje", "Ya existe un sub celaje de subestación de transmición.");
                ModelState.AddModelError("CodigoSub", "Ya existe un codigo el de subestación de transmición.");
                ModelState.AddModelError("fecha", "Ya existe una fecha de subestación de transmición.");
            }

            if (ModelState.IsValid)
            {
                sub_Celaje.id_EAdministrativa = (Int16)Id_Eadministrativa; // (int)Id_Eadministrativa;
                sub_Celaje.NumAccion = repo.GetNumAccion("I", "SIE", 0);
                db.Sub_Celaje.Add(sub_Celaje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();
            ViewBag.tipo = repoInsp.tipoInsp();
            return View(sub_Celaje);
        }

        // GET: Sub_Celaje/Edit/5
        public async Task <ActionResult> Edit(string nombInsp, string codigo, DateTime fechaIns)
        {
            Repositorio repo = new Repositorio(db);
            var repoInsp = new Sub_CelajeInspSubTransRepositorio(db);
            var repoInspAspect = new Sub_InspAspectosSubTransRepositorio(db);
            Sub_CelajeViewModel sub_CelajeViewModel = new Sub_CelajeViewModel();

            sub_CelajeViewModel.sub_Celaje = db.Sub_Celaje.Find(nombInsp, codigo, fechaIns); ;

            if (sub_CelajeViewModel.sub_Celaje == null)
            {
                ViewBag.mensaje = "No existen datos del Sub Celaje";
                return View("~/Views/Shared/Error.cshtml");
            }
            sub_CelajeViewModel.sub_Celaje = db.Sub_Celaje.Where(s => s.NombreCelaje == nombInsp && s.CodigoSub == codigo && s.fecha == fechaIns).FirstOrDefault();
            sub_CelajeViewModel.sub_InspAspectosSubTransViewModel = await repoInspAspect.ObtenerSub_InspAspectos(codigo, fechaIns);
            sub_CelajeViewModel.personal = db.Personal.Where(s => s.Id_Persona == sub_CelajeViewModel.sub_Celaje.RealizadoPor).FirstOrDefault();


            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");

            ViewBag.fechaInsp = fechaIns;

            ViewBag.personal = repo.RealizadoPor();
            ViewBag.tipo = repoInsp.tipoInsp();
            ViewBag.cant = 0;
            return View(sub_CelajeViewModel);
        }

        // POST: Sub_Celaje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Sub_CelajeViewModel sub_CelajeViewModel)
        {
            Repositorio repo = new Repositorio(db);
            var repoInsp = new Sub_CelajeInspSubTransRepositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();

            if (ModelState.IsValid)
            {
                Sub_Celaje cl = db.Sub_Celaje.SingleOrDefault(c => c.NombreCelaje.Equals(sub_CelajeViewModel.sub_Celaje.NombreCelaje) && c.CodigoSub.Equals(sub_CelajeViewModel.sub_Celaje.CodigoSub) && c.fecha.Equals(sub_CelajeViewModel.sub_Celaje.fecha));
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();

            ViewBag.tipo = repoInsp.tipoInsp();

            return View(sub_CelajeViewModel);
        }

        // GET: Sub_InspAspectosSubTrans/Edit/5
        public async  Task<ActionResult> EditAspecto(string nombInsp, string codigo, DateTime fechaIns, int aspecto, string NombreAspecto)
        {
            Repositorio repo = new Repositorio(db);
            var repoInsp = new Sub_InspAspectosSubTransRepositorio(db);
            Sub_InspAspectosSubTransViewModel dinsp = await repoInsp.ObtenerSub_InspAspectosSubTrans(codigo, fechaIns, aspecto);

            if (dinsp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (dinsp == null)
            {
                return HttpNotFound();
            }

            ViewBag.nombreAspecto = NombreAspecto;
            ViewBag.fechaIns = fechaIns;
            ViewBag.nombreInsp = nombInsp;
            ViewBag.defecto = repoInsp.defectos();

            ViewBag.cant = 0;
            return View(dinsp);
        }

        // POST: Sub_InspAspectosSubTrans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAspecto( Sub_InspAspectosSubTransViewModel sub_InspAspectosSubTransViewModel)
        {
            Repositorio repo = new Repositorio(db);
            var repoInsp = new Sub_InspAspectosSubTransRepositorio(db);

            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();

            if (ModelState.IsValid)
            {
                Sub_InspAspectosSubTrans sub_Insp = db.Sub_InspAspectosSubTrans.SingleOrDefault(c => c.NombreCelaje.Equals(sub_InspAspectosSubTransViewModel.NombreCelaje) && c.CodigoSub.Equals(sub_InspAspectosSubTransViewModel.CodigoSub) && c.fecha.Equals(sub_InspAspectosSubTransViewModel.fecha) && c.Aspecto.Equals(sub_InspAspectosSubTransViewModel.Aspecto));

                
                sub_Insp.Defecto = sub_InspAspectosSubTransViewModel.Defecto;
                sub_Insp.Observaciones = sub_InspAspectosSubTransViewModel.Observaciones;

                db.SaveChanges();
                RouteValueDictionary routeValueDicttionary = new RouteValueDictionary() ;
                routeValueDicttionary.Add("nombInsp", sub_InspAspectosSubTransViewModel.NombreCelaje);
                routeValueDicttionary.Add("codigo", sub_InspAspectosSubTransViewModel.CodigoSub);
                routeValueDicttionary.Add("fechaIns", sub_InspAspectosSubTransViewModel.fecha);

                return RedirectToAction(nameof(Edit), routeValueDicttionary);
            }

            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");



            return View(sub_InspAspectosSubTransViewModel);
        }

        // GET: Sub_Celaje/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Celaje sub_Celaje = await db.Sub_Celaje.FindAsync(id);
            if (sub_Celaje == null)
            {
                return HttpNotFound();
            }
            return View(sub_Celaje);
        }

        [TienePermiso(34)]// verifico que tenga permiso de crear y eliminar SubCelajeSubTrans
        public ActionResult Eliminar(string nombI, string codigo, DateTime fecha)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_Celaje t = db.Sub_Celaje.Find(nombI, codigo, fecha);
                db.Sub_Celaje.Remove(t);
                int accion = br.GetNumAccion("B", "SIE", t.NumAccion);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        // POST: Sub_Celaje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Sub_Celaje sub_Celaje = await db.Sub_Celaje.FindAsync(id);
            db.Sub_Celaje.Remove(sub_Celaje);
            await db.SaveChangesAsync();
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

        private async Task<bool> ValidarExisteIns(string nomb, string Codigo, DateTime fecha)
        {
            var listaInsps = await new Sub_CelajeInspSubTransRepositorio(db).ObtenerSub_CelajeSubTrans();

            return listaInsps.Select(c => new { c.NombreCelaje, c.CodigoSub, c.fecha }).Any(c => c.NombreCelaje == nomb && c.CodigoSub == Codigo && c.fecha == fecha);
        }

        #region Vista Parciales
        [HttpPost]
        public async Task<ActionResult> ListadoInsp()
        {
            var ListaSub_CelajeInspSubTrans = new Sub_CelajeInspSubTransRepositorio(db);
            return PartialView("_VPSub_Celaje", await ListaSub_CelajeInspSubTrans.ObtenerSub_CelajeSubTrans());
        }
        #endregion

    }
}
