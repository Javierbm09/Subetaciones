using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
    public class Sub_CertificacionController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_Certificacion
        public async  Task<ActionResult> Index()
        {
            var ListaCert = new CertificacionSubDistRepo(db);
            return View(await ListaCert.ObtenerCertificaciones());
        }

        // GET: Sub_Certificacion/Details/5
        public ActionResult Details(int EA, int numAccion)
        {
            CertificacionSubDistViewModel certf = new CertificacionSubDistViewModel();


            certf.DCertificaciones = db.Sub_Certificacion.Find(EA, numAccion);

            if (certf.DCertificaciones == null)
            {
                ViewBag.mensaje = "No existen datos de certificación.";
                return View("~/Views/Shared/Error.cshtml");
            }
            certf.Comision = db.Sub_CertificacionComision.Where(s => s.Id_EACertificacion == EA && s.NumAccionCertificacion == numAccion).ToList();
            certf.Detalles = db.Sub_CertificacionDetalles.Where(d => d.Id_EACertificacion == EA && d.NumAccionCertificacion == numAccion).ToList();
            //lista de aspectos
            ViewBag.Aspectos = db.Sub_CertificacionAspectos.ToList();

            //lista de subaspectos
            ViewBag.SubAspectos = db.Sub_CertificacionSubAspectos.ToList();
            return View(certf);
        }

        // GET: Sub_Certificacion/Create
        [TienePermiso(39)]
        public ActionResult Create()
        {

            CertificacionSubDistViewModel DCertf = new CertificacionSubDistViewModel();
            var repo = new Repositorio(db);
            var repoCertf = new CertificacionSubDistRepo(db);
            ViewBag.ComisionCount = 0;
            ViewBag.AspectosCount = db.Sub_CertificacionAspectos.Count();
            ViewBag.SUbAspectosCount = db.Sub_CertificacionSubAspectos.Count();


            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();
            //lista de aspectos
            ViewBag.Aspectos = db.Sub_CertificacionAspectos.ToList();

            //lista de subaspectos
            ViewBag.SubAspectos = db.Sub_CertificacionSubAspectos.ToList();

            //lista detalles
            DCertf.Detalles = db.Sub_CertificacionDetalles.ToList();


            ViewBag.cumplimiento = repoCertf.cumplimiento();
            ViewBag.responsable = repoCertf.responsable();

            return View(DCertf);
        }

        // POST: Sub_Certificacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CertificacionSubDistViewModel DCertf)
        {
            Repositorio repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();

            var Aspectos = db.Sub_CertificacionAspectos.ToList();

            //lista de subaspectos
            var SubAspectos = db.Sub_CertificacionSubAspectos.ToList();

            if (ModelState.IsValid)
            {
                   //datos de la certificacion
                DCertf.DCertificaciones.Id_EAdministrativa = (int)Id_Eadministrativa;
                DCertf.DCertificaciones.NumAccion = repo.GetNumAccion("I", "SCD", 0);
                db.Sub_Certificacion.Add(DCertf.DCertificaciones);

                //datos de la comision
                if (DCertf.Comision != null)
                {
                    foreach (var item in DCertf.Comision)
                    {
                        item.Id_EACertificacion = DCertf.DCertificaciones.Id_EAdministrativa;
                        item.NumAccionCertificacion = DCertf.DCertificaciones.NumAccion;
                        db.Sub_CertificacionComision.Add(item);
                    }
                }

               
                            if (DCertf.Detalles != null)
                            {
                                foreach (var item in DCertf.Detalles)
                                {

                                    item.Id_CertificacionDetalles = DCertf.DCertificaciones.Id_EAdministrativa;
                                    item.Id_EACertificacion = DCertf.DCertificaciones.Id_EAdministrativa;
                                    item.NumAccionCertificacion = DCertf.DCertificaciones.NumAccion;
                                    db.Sub_CertificacionDetalles.Add(item);
                                }
                            }
                

                db.SaveChanges();
                return RedirectToAction("Index");
            }
          

            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();
            return View(DCertf);
        }

        // GET: Sub_Certificacion/Edit/5
        public ActionResult Edit(int EA, int numAccion)
        {
            CertificacionSubDistViewModel DCertf = new CertificacionSubDistViewModel();
            var repoCertf = new CertificacionSubDistRepo(db);

            DCertf.DCertificaciones = db.Sub_Certificacion.Find(EA, numAccion);
            if (DCertf.DCertificaciones == null)
            {
                ViewBag.mensaje = "No existen datos de certificación.";
                return View("~/Views/Shared/Error.cshtml");
            }

            DCertf.Comision = db.Sub_CertificacionComision.Where(s => s.Id_EACertificacion == EA && s.NumAccionCertificacion == numAccion).ToList();
            DCertf.Detalles = db.Sub_CertificacionDetalles.Where(d => d.Id_EACertificacion == EA && d.NumAccionCertificacion == numAccion).ToList();
            ViewBag.ComisionCount = DCertf.Comision.Count;

            //lista de aspectos
            ViewBag.Aspectos = db.Sub_CertificacionAspectos.ToList();

            //lista de subaspectos
            ViewBag.SubAspectos = db.Sub_CertificacionSubAspectos.ToList();

            ViewBag.cumplimiento = repoCertf.cumplimiento();
            ViewBag.responsable = repoCertf.responsable();

            return View(DCertf);

        }

        // POST: Sub_Certificacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CertificacionSubDistViewModel DCertf)
        {
            Repositorio repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();
            if (ModelState.IsValid)
            {
                //datos de la certificacion
                //DCertf.DCertificaciones.Id_EAdministrativa = (int)Id_Eadministrativa;
                 repo.GetNumAccion("M", "SCD", DCertf.DCertificaciones.NumAccion);
                db.Entry(DCertf.DCertificaciones).State = EntityState.Modified;

                //datos de la comision
                if (DCertf.Comision != null)
                {
                    foreach (Sub_CertificacionComision item in DCertf.Comision)
                    {
                            var existecomision = db.Sub_CertificacionComision.Any(sc => sc.Id_CertificacionComision == item.Id_CertificacionComision);
                        if (existecomision)
                        {//si existe lo edito

                            db.Entry(item).State = EntityState.Modified;
                        }
                        else
                        {//si no lo inserto
                            item.Id_EACertificacion = DCertf.DCertificaciones.Id_EAdministrativa;
                            item.NumAccionCertificacion = DCertf.DCertificaciones.NumAccion;
                            db.Sub_CertificacionComision.Add(item);
                        }

                    }
                }

                //datos de los detalles
                if (DCertf.Detalles != null)
                {
                    foreach (Sub_CertificacionDetalles d in DCertf.Detalles)
                    {
                        db.Entry(d).State = EntityState.Modified;

                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var repoCertf = new CertificacionSubDistRepo(db);


            //lista de aspectos
            ViewBag.Aspectos = db.Sub_CertificacionAspectos.ToList();

            //lista de subaspectos
            ViewBag.SubAspectos = db.Sub_CertificacionSubAspectos.ToList();

            //lista detalles
            DCertf.Detalles = db.Sub_CertificacionDetalles.ToList();


            ViewBag.cumplimiento = repoCertf.cumplimiento();
            ViewBag.responsable = repoCertf.responsable();

            return View(DCertf);
        }

        [TienePermiso(39)]// verifico que tenga permiso de crear y eliminar mtto
        public ActionResult Eliminar(int idEA, int numAcc)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_Certificacion c = db.Sub_Certificacion.Find(idEA, numAcc);
                db.Sub_Certificacion.Remove(c);
                int accion = br.GetNumAccion("B", "SCD", c.NumAccion);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }



        // GET: Sub_Certificacion/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Certificacion sub_Certificacion = db.Sub_Certificacion.Find(id);
            if (sub_Certificacion == null)
            {
                return HttpNotFound();
            }
            return View(sub_Certificacion);
        }

        // POST: Sub_Certificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Sub_Certificacion sub_Certificacion = db.Sub_Certificacion.Find(id);
            db.Sub_Certificacion.Remove(sub_Certificacion);
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

        #region Vista Parciales
        [HttpPost]
        public async Task<ActionResult> ListadoCertificaciones()
        {
            var ListaCert = new CertificacionSubDistRepo(db);
            return PartialView("_VPCertificaciones", await ListaCert.ObtenerCertificaciones());
        }

        public ActionResult UEB(string codSub)
        {
            var parametrocodigo = new SqlParameter("@codigo", codSub);

            string UEB = db.Database.SqlQuery<string>(@"SELECT ea1.Nombre nombreobe
            FROM EstructurasAdministrativas ea
            INNER JOIN EstructurasAdministrativas ea1
            ON ea.Subordinada = ea1.Id_EAdministrativa
            inner join Subestaciones s on s.Sucursal = ea.Id_EAdministrativa
            WHERE s.Codigo=@codigo", parametrocodigo).First();

            return Json(UEB, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult idUEB(string codSub)
        {
            var parametrocodigo = new SqlParameter("@codigo", codSub);

            int idUEB = db.Database.SqlQuery<int>(@"SELECT ea1.Id_EAdministrativa
            FROM EstructurasAdministrativas ea
            INNER JOIN EstructurasAdministrativas ea1
            ON ea.Subordinada = ea1.Id_EAdministrativa
            inner join Subestaciones s on s.Sucursal = ea.Id_EAdministrativa
            WHERE s.Codigo=@codigo", parametrocodigo).First();

            return Json(idUEB, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarComision(int id)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_CertificacionComision c = db.Sub_CertificacionComision.Find(id);
                db.Sub_CertificacionComision.Remove(c);
                int accion = br.GetNumAccion("B", "SCD", c.NumAccionCertificacion);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion
    }
}
