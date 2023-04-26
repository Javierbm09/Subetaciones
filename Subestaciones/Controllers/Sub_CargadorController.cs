using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using Subestaciones.Models.Repositorio;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;
using System.Threading.Tasks;

namespace Subestaciones.Controllers
{
    public class Sub_CargadorController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_Cargador
        public async Task<ActionResult> Index(int Id_ServicioCD)
        {
            var ListaBancoCargadores = new BancoCargadoresRepositorio(db);
            ViewBag.ids = Id_ServicioCD;
            return View(await ListaBancoCargadores.ObtenerCargadores(Id_ServicioCD));
        }

        // GET: Sub_Cargador/Details/5
        public async Task<ActionResult> Details(int id, int Id_ServicioCD, string cod)
        {
            if (id == 0 || Id_ServicioCD == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaCargadores = new BancoCargadoresRepositorio(db);
            ViewBag.ids = Id_ServicioCD;
            ViewBag.sub = cod;

            var cargador = await ListaCargadores.FindAsync(id, Id_ServicioCD);
            if (id == 0 || Id_ServicioCD == 0)
            {
                return HttpNotFound();
            }
           
            return View(cargador);
        }

        // GET: Sub_Cargador/Create
        public ActionResult Create(int id_servicioRedCD, string cod)
        {            
            var Repositorio = new BancoCargadoresRepositorio(db);
            Sub_Cargador cargador = new Sub_Cargador { Id_ServicioCDCarg = id_servicioRedCD};
            ViewBag.red = cod;
            ViewBag.EO = Repositorio.EstadoO();
            ViewBag.mes = Repositorio.mes();
            ViewBag.TiposCargadores = new SelectList(Repositorio.tipoCargadores().Select(c => new { Value = c.IdCargador, Text = c.TipoCargador }), "Value", "Text");
            ViewBag.Fab = new SelectList(Repositorio.fab().Select(c => new { value = c.Id_Fabricante, Text = c.Nombre + ", " + c.Pais}), "Value", "Text");
            ViewBag.CA = new SelectList(Repositorio.redCA(cod).Select(c => new { value = c.Id_RedCA, Text = c.NombreServicioCA }), "Value", "Text");
            //ViewBag.idRed = id_servicioRedCD;
            return View(cargador);
        }

        // POST: Sub_Cargador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Cargador,id_EAdministrativa,NumAccion,tipo,Id_ServicioCDCarg,EstOp,MesFab,AnnoFab,Fabricante,Modelo,NroSerie,VoltajeMaxCD,VoltajeMinCD,Id_RedCA,FechaInstalado")] Sub_Cargador sub_Cargador, string cod)
        {
            Repositorio br = new Repositorio(db);
            var Id_Eadministrativa = br.GetId_EAdministrativaUsuario();//esta EA ya esta bien

            if (ModelState.IsValid)
            {
                sub_Cargador.id_EAdministrativa = (int)Id_Eadministrativa;
                sub_Cargador.NumAccion = br.GetNumAccion("I", "SRC", 0);
                 db.Sub_Cargador.Add(sub_Cargador);
                db.SaveChanges();
                return RedirectToAction("Edit", "Sub_RedCorrienteDirecta", new { Id_ServicioCD = sub_Cargador.Id_ServicioCDCarg });
            }
            var Repositorio = new BancoCargadoresRepositorio(db);
            ViewBag.EO = Repositorio.EstadoO();
            ViewBag.mes = Repositorio.mes();
            ViewBag.TiposCargadores = new SelectList(Repositorio.tipoCargadores().Select(c => new { Value = c.IdCargador, Text = c.TipoCargador }), "Value", "Text");
            ViewBag.Fab = new SelectList(Repositorio.fab().Select(c => new { value = c.Id_Fabricante, Text = c.Nombre + ", " + c.Pais }), "Value", "Text");
            ViewBag.CA = new SelectList(Repositorio.redCA(cod).Select(c => new { value = c.Id_RedCA, Text = c.NombreServicioCA }), "Value", "Text");
            return View(sub_Cargador);
        }

        // GET: Sub_Cargador/Edit/5
        public ActionResult Edit(int id,int Id_ServicioCD, string cod)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Repositorio = new BancoCargadoresRepositorio(db);
            Sub_Cargador cargador = db.Sub_Cargador.Find(id);
            ViewBag.EO = Repositorio.EstadoO();
            ViewBag.TiposCargadores = new SelectList(Repositorio.tipoCargadores().Select(c => new { Value = c.IdCargador, Text = c.TipoCargador }), "Value", "Text");
            ViewBag.Fab = new SelectList(Repositorio.fab().Select(c => new { value = c.Id_Fabricante, Text = c.Nombre + ", " + c.Pais }), "Value", "Text");
            ViewBag.CA = new SelectList(Repositorio.redCA(cod).Select(c => new { value = c.Id_RedCA, Text = c.NombreServicioCA }), "Value", "Text");
            ViewBag.mes = Repositorio.mes();

            return View(cargador);
        }

        // POST: Sub_Cargador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Cargador,id_EAdministrativa,NumAccion,tipo,Id_ServicioCDCarg,EstOp,MesFab,AnnoFab,Fabricante,Modelo,NroSerie,VoltajeMaxCD,VoltajeMinCD,Id_RedCA,FechaInstalado")] Sub_Cargador sub_Cargador, string cod)
        {
            Repositorio rep = new Repositorio(db);
            var Id_Eadministrativa = rep.GetId_EAdministrativaUsuario();//esta EA ya esta bien
            if (ModelState.IsValid)
            {
                sub_Cargador.id_EAdministrativa = (int)Id_Eadministrativa;
                sub_Cargador.NumAccion = rep.GetNumAccion("M", "SRC", 0);
                db.Entry(sub_Cargador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Sub_RedCorrienteDirecta", new { Id_ServicioCD = sub_Cargador.Id_ServicioCDCarg });
            }
            var Repositorio = new BancoCargadoresRepositorio(db);
            ViewBag.EO = Repositorio.EstadoO();
            ViewBag.mes = Repositorio.mes();
            ViewBag.TiposCargadores = new SelectList(Repositorio.tipoCargadores().Select(c => new { Value = c.IdCargador, Text = c.TipoCargador }), "Value", "Text");
            ViewBag.Fab = new SelectList(Repositorio.fab().Select(c => new { value = c.Id_Fabricante, Text = c.Nombre + ", " + c.Pais }), "Value", "Text");
            ViewBag.CA = new SelectList(Repositorio.redCA(cod).Select(c => new { value = c.Id_RedCA, Text = c.NombreServicioCA }), "Value", "Text");
            return View(sub_Cargador);
        }

        // GET: Sub_Cargador/Delete/5
        public async Task<ActionResult> Delete(int id, int red)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaCargadores = new BancoCargadoresRepositorio(db);
            var cargador = await ListaCargadores.FindAsync(id, red);
            return View(cargador);
        }

        // POST: Sub_Cargador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Repositorio br = new Repositorio(db);
            Sub_Cargador elimina_cargador = db.Sub_Cargador.FindAsync(id).Result;
            int accion = br.GetNumAccion("B", "SRC",  0);
            db.Sub_Cargador.Remove(elimina_cargador);
            db.SaveChanges();
            return RedirectToAction("Edit", "Sub_RedCorrienteDirecta", new { Id_ServicioCD = elimina_cargador.Id_ServicioCDCarg });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult Cargar_datos_Cargador(int id_cargador)
        {
            var cargador = db.Sub_NomCargadores.Find(id_cargador);

            return Json(cargador);
        }

        #region Vistas Parciales (VP) AJAX
        [AllowAnonymous]
        public ActionResult CargarTipoCargador(string tipoequipo, string codsub)
        {
           // CodigoEquipos(tipoequipo, codsub);
            return PartialView("_VPInstalaciones");
        }
        #endregion
    }
}
