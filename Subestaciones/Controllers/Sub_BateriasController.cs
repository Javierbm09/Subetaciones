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
    public class Sub_BateriasController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_Baterias
        public ActionResult Index()
        {
            return View(db.Sub_Baterias.ToList());
        }

        // GET: Sub_Baterias/Details/5
        public async Task<ActionResult> Details(int? id, int Id_ServicioCD)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaBat = new BateriasRepositorio(db);
            
            var sub_Baterias = await ListaBat.FindAsync(id, Id_ServicioCD);
            if (sub_Baterias == null)
            {
                return HttpNotFound();
            }
            return View(sub_Baterias);
        }

        // GET: Sub_Baterias/Create
        public ActionResult Create(int id_servicioRedCD)
        {
            var Repositorio = new BateriasRepositorio(db);
            Sub_Baterias bateria = new Sub_Baterias { Id_ServicioCDBat = id_servicioRedCD };
            ViewBag.mes = Repositorio.mes();
            ViewBag.TiposBaterias = new SelectList(Repositorio.tipoBaterias().Select(c => new { Value = c.IdBateria, Text = c.TipoBateria + ", " + c.CapacidadBateria + ", " + c.TensionBateria }), "Value", "Text");
            ViewBag.Fab = new SelectList(Repositorio.fab().Select(c => new { value = c.Id_Fabricante, Text = c.Nombre + ", " + c.Pais }), "Value", "Text");

            return View(bateria);
        }

        // POST: Sub_Baterias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Bateria,Tipo,id_EAdministrativa,NumAccion,CantidadVasos,Voltaje_Vasos,Densisdad,Id_ServicioCDBat,PesoCadaVaso,VoltajeFlotacion,CantElectVasos,CantElect,CantVasosPilotos,MesFab,AnnoFab,Fabricante,VoltajeEcualizanteCargaBat,TiempoCargaBat,PeriodicidadCargaBat,FechaInstalado")] Sub_Baterias sub_Baterias)
        {
            Repositorio br = new Repositorio(db);
            var Id_Eadministrativa = br.GetId_EAdministrativaUsuario();//esta EA ya esta bien

            if (ModelState.IsValid)
            {
                sub_Baterias.id_EAdministrativa = (int)Id_Eadministrativa;
                sub_Baterias.NumAccion = br.GetNumAccion("I", "SRC", 0);
                db.Entry(sub_Baterias).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Edit", "Sub_RedCorrienteDirecta", new { Id_ServicioCD = sub_Baterias.Id_ServicioCDBat });
            }
            var Repositorio = new BateriasRepositorio(db);

            ViewBag.mes = Repositorio.mes();

            return View(sub_Baterias);
        }

        // GET: Sub_Baterias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Repositorio = new BateriasRepositorio(db);
            Sub_Baterias sub_Baterias = db.Sub_Baterias.Find(id);
            ViewBag.TiposBaterias = new SelectList(Repositorio.tipoBaterias().Select(c => new { Value = c.IdBateria, Text = c.TipoBateria + ", " + c.CapacidadBateria + ", " + c.TensionBateria }), "Value", "Text");
            ViewBag.Fab = new SelectList(Repositorio.fab().Select(c => new { value = c.Id_Fabricante, Text = c.Nombre + ", " + c.Pais }), "Value", "Text");
            ViewBag.mes = Repositorio.mes();

            if (sub_Baterias == null)
            {
                return HttpNotFound();
            }
            return View(sub_Baterias);
        }

        // POST: Sub_Baterias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Bateria,Tipo,id_EAdministrativa,NumAccion,CantidadVasos,Voltaje_Vasos,Densisdad,Id_ServicioCDBat,PesoCadaVaso,VoltajeFlotacion,CantElectVasos,CantElect,CantVasosPilotos,MesFab,AnnoFab,Fabricante,VoltajeEcualizanteCargaBat,TiempoCargaBat,PeriodicidadCargaBat,FechaInstalado")] Sub_Baterias sub_Baterias)
        {
            Repositorio br = new Repositorio(db);
            var Id_Eadministrativa = br.GetId_EAdministrativaUsuario();//esta EA ya esta bien

            if (ModelState.IsValid)
            {
                sub_Baterias.id_EAdministrativa = (int)Id_Eadministrativa;
                sub_Baterias.NumAccion = br.GetNumAccion("M", "SRC", 0);
                db.Entry(sub_Baterias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "Sub_RedCorrienteDirecta", new { Id_ServicioCD = sub_Baterias.Id_ServicioCDBat });

            }
            return View(sub_Baterias);
        }

        // GET: Sub_Baterias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Baterias sub_Baterias = db.Sub_Baterias.Find(id);
            if (sub_Baterias == null)
            {
                return HttpNotFound();
            }
            return View(sub_Baterias);
        }

        // POST: Sub_Baterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sub_Baterias sub_Baterias = db.Sub_Baterias.Find(id);
            db.Sub_Baterias.Remove(sub_Baterias);
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

        public JsonResult Cargar_datos_Bateria(int id_tipoBateria)
        {
            var Repositorio = new BateriasRepositorio(db);
            return Json(Repositorio.tipoBaterias(id_tipoBateria), JsonRequestBehavior.AllowGet);
        }
    }
}
