using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;
using System.Threading.Tasks;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class Sub_RedCorrienteAlternaController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_RedCorrienteAlterna
        public ActionResult Index(string inserta)
        {
            ViewBag.Inserto = inserta;
            return View(db.Sub_RedCorrienteAlterna.ToList());
        }

        // GET: Sub_RedCorrienteAlterna/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_RedCorrienteAlterna sub_RedCorrienteAlterna = db.Sub_RedCorrienteAlterna.Find(id);
            if (sub_RedCorrienteAlterna == null)
            {
                return HttpNotFound();
            }
            var repositorio = new Repositorio(db);
            ViewBag.Codigo = new SelectList(repositorio.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + "-" + c.NombreSubestacion }), "Value", "Text", sub_RedCorrienteAlterna.Codigo);
            return View(sub_RedCorrienteAlterna);
        }

        // GET: Sub_RedCorrienteAlterna/Create
        [TienePermiso(Servicio: 32)]//Servicio: crear red corriente alterna
        public ActionResult Create()
        {
            var repositorio = new Repositorio(db);
            ViewBag.Codigo = new SelectList(repositorio.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + "-" + c.NombreSubestacion }), "Value", "Text");
            //ViewBag.Transformadores = await repositorio.ObtenerListadoBancosTransformadores();

            return View();
        }

        // POST: Sub_RedCorrienteAlterna/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [TienePermiso(Servicio: 32)]//Servicio: crear red corriente alterna
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_RedCA,NombreServicioCA,Codigo,NumAccion,Id_EAdministrativa")] Sub_RedCorrienteAlterna sub_RedCorrienteAlterna)
        {
            var usuario = System.Web.HttpContext.Current.User?.Identity?.Name ?? null;
            string nombre_usuario = System.Web.HttpContext.Current.User.Identity.Name;
            var usuario_logueado = db.Personal.FirstOrDefault(c => c.Nombre == nombre_usuario);
            int? EAdmin = usuario_logueado.id_EAdministrativa;

            Repositorio repositorio = new Repositorio(db);

            if (ModelState.IsValid)
            {
                if (ValidarExisteRed(sub_RedCorrienteAlterna.Codigo))
                {
                    sub_RedCorrienteAlterna.NumAccion = repositorio.GetNumAccion("I", "RCA", 0);
                    sub_RedCorrienteAlterna.Id_EAdministrativa = (short)EAdmin;

                    db.Sub_RedCorrienteAlterna.Add(sub_RedCorrienteAlterna);
                    db.SaveChanges();


                    return RedirectToAction("Edit", "sub_RedCorrienteAlterna", new { id = sub_RedCorrienteAlterna.Id_RedCA, inserta = "Si" });
                }
                else
                {
                    ModelState.AddModelError("NombreServicioCA", "Ya existe un Servicio de Red de Corriente Alterna en la subestación.");
                }
            }

            ViewBag.Codigo = new SelectList(repositorio.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + "-" + c.NombreSubestacion }), "Value", "Text");
            return View(sub_RedCorrienteAlterna);
        }

        private bool ValidarExisteRed(string Codigo)
        {

            var listaRedes = db.Sub_RedCorrienteAlterna.Where(a => a.Codigo == Codigo).FirstOrDefault();
            return listaRedes == null ? true : false;
        }

        [TienePermiso(Servicio: 16)]
        public async Task<ActionResult> Edit(int id, string inserta)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repositorio repositorio = new Repositorio(db);
            Sub_RedCorrienteAlterna sub_RedCorrienteAlterna = db.Sub_RedCorrienteAlterna.Find(id);
            ViewBag.Inserto = inserta;
            ViewBag.Codigo = new SelectList(repositorio.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + "-" + c.NombreSubestacion }), "Value", "Text", sub_RedCorrienteAlterna.Codigo);
            return View(sub_RedCorrienteAlterna);
        }

        // POST: Sub_RedCorrienteAlterna/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [TienePermiso(Servicio: 16)]//Servicio: editar red corriente alterna
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_RedCA,NombreServicioCA,Codigo,NumAccion,Id_EAdministrativa")] Sub_RedCorrienteAlterna sub_RedCorrienteAlterna)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_RedCorrienteAlterna).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { inserta = "Si" });
            }
            return View(sub_RedCorrienteAlterna);
        }

        // GET: Sub_RedCorrienteAlterna/Delete/5
        [TienePermiso(Servicio: 32)]//Servicio: Eliminar red corriente alterna
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_RedCorrienteAlterna sub_RedCorrienteAlterna = db.Sub_RedCorrienteAlterna.Find(id);
            if (sub_RedCorrienteAlterna == null)
            {
                return HttpNotFound();
            }
            return View(sub_RedCorrienteAlterna);
        }

        // POST: Sub_RedCorrienteAlterna/Delete/5
        [TienePermiso(Servicio: 32)]//Servicio: Eliminar red corriente alterna
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Sub_RedCorrienteAlterna sub_RedCorrienteAlterna = db.Sub_RedCorrienteAlterna.Find(id);
            db.Sub_RedCorrienteAlterna.Remove(sub_RedCorrienteAlterna);
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

        public JsonResult Eliminar(short id)
        {

            try
            {
                Sub_RedCorrienteAlterna redCA = db.Sub_RedCorrienteAlterna.Find(id);
                db.Sub_RedCorrienteAlterna.Remove(redCA);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            //}
        }


        #region Vistas Parciales (VP) AJAX
        [AllowAnonymous]
        public async Task<ActionResult> CargarTablaBancosTransformadores(string codigoSubestacion)
        {
            RedCorrienteAlternaRepositorio repositorio = new RedCorrienteAlternaRepositorio(db);
            return Json(await repositorio.ObtenerListadoBancosTransformadores(codigoSubestacion), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> CargarTablaTransformadores(string banco)
        {
            RedCorrienteAlternaRepositorio repositorio = new RedCorrienteAlternaRepositorio(db);
            return Json(await repositorio.ObtenerListadoTransformadores(banco), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ListadoRedes()
        {
            return PartialView("_VPRedCA", db.Sub_RedCorrienteAlterna.ToList());
        }

        public async Task<ActionResult> CargarTablaBreakers(string codigoSubestacion, int? idRedCA)
        {
            RedCorrienteAlternaRepositorio repositorio = new RedCorrienteAlternaRepositorio(db);
            return Json(await repositorio.ObtenerListaBreakers(codigoSubestacion, idRedCA), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
