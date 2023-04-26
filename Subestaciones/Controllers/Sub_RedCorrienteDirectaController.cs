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
    public class Sub_RedCorrienteDirectaController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_RedCorrienteDirecta
        public async Task<ActionResult> Index(string inserta)
        {
            var ListaRedesCD = new RedCDRepositorio(db);
            ViewBag.Inserto = inserta;
            return View(await ListaRedesCD.ObtenerListadoRedesCD());
        }

        // GET: Sub_RedCorrienteDirecta/Details/5
        public async Task<ActionResult> Details(int Id_ServicioCD)
        {
            if (Id_ServicioCD == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaRedesCD = new RedCDRepositorio(db);
            var redes = await ListaRedesCD.FindAsync(Id_ServicioCD);
            if (Id_ServicioCD == 0)
            {
                return HttpNotFound();
            }
            return View(redes);

        }

        // GET: Sub_RedCorrienteDirecta/Create
        [TienePermiso(Servicio: 33)]
        public async Task<ActionResult> Create()
        {
            var repo = new Repositorio(db);
            var repored = new RedCDRepositorio(db);
            ViewBag.Subestacion = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.voltaje = new SelectList(await repo.voltajeRedCD(), "idTension", "tension ");
            ViewBag.usoRedCD = new SelectList(repored.UsoRed(), "Value", "Text");
            return View();
        }
        private async Task<bool> ValidarExisteSubestacion(string Codigo)
        {
            var listaRedes = await new RedCDRepositorio(db).ObtenerListadoRedesCD();

            return !listaRedes.Select(c => new { c.Codigo }).Any(c => c.Codigo == Codigo);
        }

        [HttpPost]
        public async Task<ActionResult> ValidarSiExiste(string Codigo)
        {
            try
            {
                return Json(await ValidarExisteSubestacion(Codigo));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        // POST: Sub_RedCorrienteDirecta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_ServicioCD,NombreServicioCD,Codigo,id_EAdministrativa,NumAccion,idVoltaje,UsoRed,ControlAislamBarras,Observaciones")] Sub_RedCorrienteDirecta sub_RedCorrienteDirecta)
        {
            var br = new Repositorio(db);
            var Id_Eadministrativa = br.GetId_EAdministrativaUsuario(); //esta EA ya esta bien
            if (ModelState.IsValid)
            {
                if (await ValidarExisteSubestacion(sub_RedCorrienteDirecta.Codigo))
                {
                    sub_RedCorrienteDirecta.id_EAdministrativa = (int)Id_Eadministrativa;
                    sub_RedCorrienteDirecta.NumAccion = br.GetNumAccion("I", "SRC", 0);
                    db.Sub_RedCorrienteDirecta.Add(sub_RedCorrienteDirecta);
                    db.SaveChanges();
                    return RedirectToAction("Edit", new { sub_RedCorrienteDirecta.Id_ServicioCD, inserta = "Si" });
                }
                else
                {
                    ModelState.AddModelError("NombreServicioCD", "Ya existe un Servicio de Red de Corriente Directa en la subestación.");
                }
            }
            var repo = new Repositorio(db);
            var repored = new RedCDRepositorio(db);
            ViewBag.Subestacion = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.voltaje = new SelectList(await repo.voltajeRedCD(), "idTension", "tension ");
            ViewBag.usoRedCD = new SelectList(repored.UsoRed(), "Value", "Text");

            return View(sub_RedCorrienteDirecta);
        }

        // GET: Sub_RedCorrienteDirecta/Edit/5
        [TienePermiso(Servicio: 11)]
        public async Task<ActionResult> Edit(int Id_ServicioCD, string inserta)
        {
            if (Id_ServicioCD == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_RedCorrienteDirecta sub_RedCorrienteDirecta = db.Sub_RedCorrienteDirecta.Find(Id_ServicioCD);
            var repo = new Repositorio(db);
            var redCD = new RedCDRepositorio(db);
            ViewBag.Inserto = inserta;
            ViewBag.Subestacion = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text", sub_RedCorrienteDirecta.Codigo);
            ViewBag.voltaje = new SelectList(await repo.voltajeRedCD(), "idTension", "tension ", sub_RedCorrienteDirecta.idVoltaje);
            ViewBag.usoRedCD = new SelectList(redCD.UsoRed(), "Value", "Text", sub_RedCorrienteDirecta.UsoRed);

            return View(sub_RedCorrienteDirecta);
        }

        // POST: Sub_RedCorrienteDirecta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_ServicioCD,NombreServicioCD,Codigo,id_EAdministrativa,NumAccion,idVoltaje,UsoRed,ControlAislamBarras,Observaciones")] Sub_RedCorrienteDirecta sub_RedCorrienteDirecta)
        {
            var redCD = new Repositorio(db);
            var Id_Eadministrativa = redCD.GetId_EAdministrativaUsuario(); //esta EA ya esta bien
            if (ModelState.IsValid)
            {
                sub_RedCorrienteDirecta.id_EAdministrativa = (int)Id_Eadministrativa;
                sub_RedCorrienteDirecta.NumAccion = redCD.GetNumAccion("M", "SRC", sub_RedCorrienteDirecta.NumAccion ?? 0);
                db.Entry(sub_RedCorrienteDirecta).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.Form["submitButton"].ToString() == "redCD")
                {
                    return RedirectToAction("Index", new { inserta = "Si" });
                }
                if (Request.Form["submitButton"].ToString() == "cargador")
                {
                    ViewBag.idRedCD = sub_RedCorrienteDirecta.Id_ServicioCD;
                    return RedirectToAction("Create", "Sub_Cargador", new { id_servicioRedCD = sub_RedCorrienteDirecta.Id_ServicioCD, cod = sub_RedCorrienteDirecta.Codigo });
                }
                if (Request.Form["submitButton"].ToString() == "bateria")
                {
                    ViewBag.idRedCD = sub_RedCorrienteDirecta.Id_ServicioCD;
                    return RedirectToAction("Create", "Sub_Baterias", new { id_servicioRedCD = sub_RedCorrienteDirecta.Id_ServicioCD });
                }

            }
            return View(sub_RedCorrienteDirecta);
            //if (ModelState.IsValid)
            //{
            //    db.Entry(sub_RedCorrienteDirecta).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(sub_RedCorrienteDirecta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearCargador( Sub_RedCorrienteDirecta sub_RedCorrienteDirecta)
        {
            var redCD = new Repositorio(db);
            var Id_Eadministrativa = redCD.GetId_EAdministrativaUsuario(); //esta EA ya esta bien
            if (ModelState.IsValid)
            {
                sub_RedCorrienteDirecta.id_EAdministrativa =(int) Id_Eadministrativa;
                sub_RedCorrienteDirecta.NumAccion = redCD.GetNumAccion("M", "SRC", sub_RedCorrienteDirecta.NumAccion ?? 0);
                db.Entry(sub_RedCorrienteDirecta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create", "Sub_Cargador", new { Id_ServicioCDCarg = sub_RedCorrienteDirecta.Id_ServicioCD,  cod = sub_RedCorrienteDirecta.Codigo });
            }
            return PartialView("_VPRedCD");

        }


        // GET: Sub_RedCorrienteDirecta/Delete/5
        [TienePermiso(Servicio: 33)]
        public async Task<ActionResult> Delete(int Id_ServicioCD)
        {
            if (Id_ServicioCD == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaRedesCD = new RedCDRepositorio(db);
            var redes = await ListaRedesCD.FindAsync(Id_ServicioCD);

            if (Id_ServicioCD == 0)
            {
                return HttpNotFound();
            }
            return View(redes);

        }

        // POST: Sub_RedCorrienteDirecta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? Id_ServicioCD)
        {
            Repositorio br = new Repositorio(db);
            Sub_RedCorrienteDirecta elimina_red = db.Sub_RedCorrienteDirecta.FindAsync(Id_ServicioCD).Result;
            int accion = br.GetNumAccion("B", "SRD", elimina_red.NumAccion ?? 0);
            db.Sub_RedCorrienteDirecta.Remove(elimina_red);
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

        #region Vistas Parciales (VP) AJAX
        [AllowAnonymous]
        public async Task<ActionResult> cargarTablaCargadores(int IdredCD, string codsub)
        {
            BancoCargadoresRepositorio repositorio = new BancoCargadoresRepositorio(db);
            var listado = await repositorio.ObtenerCargadores(IdredCD);

            ViewBag.cargadores = listado;
            ViewBag.cantidad = listado.Count();
            ViewBag.subestacion = codsub;

            return PartialView("_VPTablaCargadores");
        }

        public async Task<ActionResult> cargarTablaBaterias(int IdredCD)
        {
            BateriasRepositorio repositorio = new BateriasRepositorio(db);
            var listado = await repositorio.ObtenerBaterias(IdredCD);

            ViewBag.baterias = listado;
            ViewBag.cantidad = listado.Count();

            return PartialView("_VPTablaBaterias");
        }


        public ActionResult Eliminar(int? Id_ServicioCD)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_RedCorrienteDirecta redCD = db.Sub_RedCorrienteDirecta.Find(Id_ServicioCD);
                db.Sub_RedCorrienteDirecta.Remove(redCD);
                int accion = br.GetNumAccion("B", "SRD", redCD.NumAccion ?? 0);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult EliminarCargador(int Id_Cargador)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_Cargador C = db.Sub_Cargador.Find(Id_Cargador);
                db.Sub_Cargador.Remove(C);
                int accion = br.GetNumAccion("B", "SRC", C.NumAccion );
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult EliminarBateria(int Id_Bateria)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_Baterias B = db.Sub_Baterias.Find(Id_Bateria);
                db.Sub_Baterias.Remove(B);
                int accion = br.GetNumAccion("B", "SRB", B.NumAccion);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<ActionResult> ListadoRedesCD()
        {
            var ListaRedesCD = new RedCDRepositorio(db);
            return PartialView("_VPTablaRedCD", await ListaRedesCD.ObtenerListadoRedesCD());
        }
        #endregion

    }
}
