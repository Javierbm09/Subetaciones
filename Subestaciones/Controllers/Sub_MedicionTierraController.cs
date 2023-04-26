using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
    public class Sub_MedicionTierraController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_MedicionTierra
        public async Task<ActionResult> Index()
        {
            var ListaMediciones = new MedicionesRepo(db);
            return View(await ListaMediciones.ObtenerListadoMediciones());
        }

        // GET: Sub_MedicionTierra/Details/5
        public ActionResult Details(string sub, DateTime fecha)
        {
            Subestaciones.Models.Clases.MedicionesTierraViewModel mediciones = new MedicionesTierraViewModel();


            mediciones.Mediciones = db.Sub_MedicionTierra.Find(sub, fecha);

            if (mediciones.Mediciones == null)
            {
                ViewBag.mensaje = "No existen datos de mediciones de tierra.";
                return View("~/Views/Shared/Error.cshtml");
            }
            mediciones.CaidaPotencial = db.Sub_MedicionTierra_CaidaPotencial.Where(s => s.Subestacion == sub && s.Fecha == fecha).ToList();
            mediciones.ContinuidadMalla = db.Sub_MedicionTierra_ContinuidadMalla.Where(s => s.Subestacion == sub && s.Fecha == fecha).ToList();

            var repo = new Repositorio(db);
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            return View(mediciones);
        }

        // GET: Sub_MedicionTierra/Create
        public ActionResult Create()
        {
            var repo = new Repositorio(db);

            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            var repoMediciones = new MedicionesRepo(db);

            ViewBag.malla = repoMediciones.tipoMalla();
            ViewBag.estadoSuelo = repoMediciones.estadoSuelo();
            ViewBag.tipoSuelo = repoMediciones.tiposSuelos();
            ViewBag.corriente = repoMediciones.SN();
            //ViewBag.malla = repoMediciones.SN();
            ViewBag.certf = repoMediciones.SN();
            ViewBag.PtosCount = 0;
            ViewBag.CaidaCount = 0;

            return View();
        }

        // POST: Sub_MedicionTierra/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MedicionesTierraViewModel medicionesTierra)
        {
            Repositorio repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();
            if (ModelState.IsValid)
            {

                if (await ValidarExisteMedicion(medicionesTierra.Mediciones.Subestacion, formatoFecha(medicionesTierra.Mediciones.f)))
                {
                    medicionesTierra.Mediciones.Fecha = formatoFecha(medicionesTierra.Mediciones.f);
                    HttpPostedFileBase file = Request.Files["ImageData"];
                    if (file != null)
                        medicionesTierra.Mediciones.ImagenPuntos = ConvertToBytes(file);

                    medicionesTierra.Mediciones.Id_EAdministrativa = (int)Id_Eadministrativa;
                    medicionesTierra.Mediciones.NumAccion = repo.GetNumAccion("I", "SPA", 0);
                    db.Sub_MedicionTierra.Add(medicionesTierra.Mediciones);

                    if (medicionesTierra.CaidaPotencial != null)
                    {
                        
                        foreach (var item in medicionesTierra.CaidaPotencial)
                        {
                            item.Id_EAdministrativa = medicionesTierra.Mediciones.Id_EAdministrativa;
                            item.NumAccion = medicionesTierra.Mediciones.NumAccion;
                            item.Fecha = medicionesTierra.Mediciones.Fecha;

                            db.Sub_MedicionTierra_CaidaPotencial.Add(item);
                        }
                    }

                    if (medicionesTierra.ContinuidadMalla != null)
                    {

                        foreach (var item in medicionesTierra.ContinuidadMalla)
                        {
                            item.Id_EAdministrativa = medicionesTierra.Mediciones.Id_EAdministrativa;
                            item.NumAccion = medicionesTierra.Mediciones.NumAccion;
                            item.Fecha = medicionesTierra.Mediciones.Fecha;

                            db.Sub_MedicionTierra_ContinuidadMalla.Add(item);
                        }
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Subestacion", "Ya se realizó una medición a la subestación en la fecha.");

                }

            }
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            var repoMediciones = new MedicionesRepo(db);

            ViewBag.malla = repoMediciones.tipoMalla();
            ViewBag.estadoSuelo = repoMediciones.estadoSuelo();
            ViewBag.tipoSuelo = repoMediciones.tiposSuelos();
            ViewBag.corriente = repoMediciones.SN();
            //ViewBag.malla = repoMediciones.SN();
            ViewBag.certf = repoMediciones.SN();
            ViewBag.PtosCount = 0;
            ViewBag.CaidaCount = 0;
            return View(medicionesTierra);
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        private async Task<bool> ValidarExisteMedicion(string sub, DateTime fecha)
        {
            var listaMediciones = await new MedicionesRepo(db).ObtenerListadoMediciones();

            return !listaMediciones.Select(c => new { c.Subestacion, c.Fecha }).Any(c => c.Subestacion == sub && c.Fecha == fecha );
        }

        private DateTime formatoFecha(string fecha)
        {
            var fechaArray = fecha.Split('-');
            DateTime newFecha = new DateTime(Convert.ToInt32(fechaArray[0]), Convert.ToInt32(fechaArray[1]), Convert.ToInt32(fechaArray[2]));
            return newFecha;
        }



        // GET: Sub_MedicionTierra/Edit/5
        public ActionResult Edit(string sub, DateTime fecha)
        {
                Subestaciones.Models.Clases.MedicionesTierraViewModel mediciones = new MedicionesTierraViewModel();


                mediciones.Mediciones = db.Sub_MedicionTierra.Find(sub, fecha);

                if (mediciones.Mediciones == null)
                {
                    ViewBag.mensaje = "No existen datos de mediciones de tierra.";
                    return View("~/Views/Shared/Error.cshtml");
                }
                mediciones.CaidaPotencial = db.Sub_MedicionTierra_CaidaPotencial.Where(s => s.Subestacion == sub && s.Fecha == fecha).ToList();
                mediciones.ContinuidadMalla = db.Sub_MedicionTierra_ContinuidadMalla.Where(s => s.Subestacion == sub && s.Fecha == fecha).ToList();

                var repo = new Repositorio(db);
                ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            var repoMediciones = new MedicionesRepo(db);

            ViewBag.malla = repoMediciones.tipoMalla();
            ViewBag.estadoSuelo = repoMediciones.estadoSuelo();
            ViewBag.tipoSuelo = repoMediciones.tiposSuelos();
            ViewBag.corriente = repoMediciones.SN();
            ViewBag.certf = repoMediciones.SN();

            ViewBag.PtosCount = mediciones.ContinuidadMalla.Count();
            ViewBag.CaidaCount = mediciones.CaidaPotencial.Count();
            return View(mediciones);
            }
       

        // POST: Sub_MedicionTierra/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MedicionesTierraViewModel medicionesTierra)
        {
            Repositorio repo = new Repositorio(db);

            if (ModelState.IsValid)
            {

                repo.GetNumAccion("M", "SAP", medicionesTierra.Mediciones.NumAccion);
                HttpPostedFileBase file = Request.Files["ImageData"];
                if (file != null)
                    medicionesTierra.Mediciones.ImagenPuntos = ConvertToBytes(file);

                db.Entry(medicionesTierra.Mediciones).State = EntityState.Modified;

                if (medicionesTierra.CaidaPotencial != null)
                {
                    foreach (Sub_MedicionTierra_CaidaPotencial item in medicionesTierra.CaidaPotencial)
                    {
                        if (db.Sub_MedicionTierra_CaidaPotencial.Any(b => b.Fecha == item.Fecha && b.Subestacion == item.Subestacion && b.NumAccion == item.NumAccion))
                        {
                             db.Entry(item).State = EntityState.Modified;
                        }
                        else

                        {
                            item.Id_EAdministrativa = medicionesTierra.Mediciones.Id_EAdministrativa;
                            item.NumAccion = medicionesTierra.Mediciones.NumAccion;
                            item.Fecha = medicionesTierra.Mediciones.Fecha;

                            db.Sub_MedicionTierra_CaidaPotencial.Add(item);
                        }
                    }
                    }

                if (medicionesTierra.ContinuidadMalla != null)
                {
                    foreach (Sub_MedicionTierra_ContinuidadMalla item in medicionesTierra.ContinuidadMalla)
                    {
                        if (db.Sub_MedicionTierra_ContinuidadMalla.Any(b => b.Fecha == item.Fecha && b.Subestacion == item.Subestacion && b.NumAccion == item.NumAccion))
                        {

                              db.Entry(item).State = EntityState.Modified;
                        }
                        else

                        {
                        item.Id_EAdministrativa = medicionesTierra.Mediciones.Id_EAdministrativa;
                        item.NumAccion = medicionesTierra.Mediciones.NumAccion;
                        item.Fecha = medicionesTierra.Mediciones.Fecha;

                        db.Sub_MedicionTierra_ContinuidadMalla.Add(item);
                        }
                    }

                }
                    db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            var repoMediciones = new MedicionesRepo(db);

            ViewBag.malla = repoMediciones.tipoMalla();
            ViewBag.estadoSuelo = repoMediciones.estadoSuelo();
            ViewBag.tipoSuelo = repoMediciones.tiposSuelos();
            ViewBag.corriente = repoMediciones.SN();
            ViewBag.certf = repoMediciones.SN();

            ViewBag.PtosCount = medicionesTierra.ContinuidadMalla.Count();
            return View(medicionesTierra);
        }

        // GET: Sub_MedicionTierra/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_MedicionTierra sub_MedicionTierra = db.Sub_MedicionTierra.Find(id);
            if (sub_MedicionTierra == null)
            {
                return HttpNotFound();
            }
            return View(sub_MedicionTierra);
        }

        // POST: Sub_MedicionTierra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sub_MedicionTierra sub_MedicionTierra = db.Sub_MedicionTierra.Find(id);
            db.Sub_MedicionTierra.Remove(sub_MedicionTierra);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Eliminar(string sub, DateTime fecha)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_MedicionTierra med = db.Sub_MedicionTierra.Find(sub, fecha);
                int accion = br.GetNumAccion("B", "SUP", med.NumAccion ?? 0);
                db.Sub_MedicionTierra.Remove(med);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<ActionResult> ListadoMediciones()
        {
            var ListaMed = new MedicionesRepo(db);
            return PartialView("_VPMediciones",await ListaMed.ObtenerListadoMediciones());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult ObtenerCaidaPotencial( string cod, DateTime f)
        {
            var repoMed = new MedicionesRepo(db);
            
            var listaMediciones = repoMed.ObtenerCaidaPotencial(cod, f);

            return Json(repoMed.ObtenerCaidaPotencial(cod, f), JsonRequestBehavior.AllowGet);
        }
    }
}
