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
using System.Data.SqlClient;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class Sub_NomInstrumentoMedicionController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_NomInstrumentoMedicion
        public async Task<ActionResult> Index()
        {
            var ListaIM = new IMRepositorio(db);
            return View(await ListaIM.ObtenerListadoIM());
        }

        // GET: Sub_NomInstrumentoMedicion/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaIM = new IMRepositorio(db);
            var InstMediciones = await ListaIM.FindAsync(id);
            ViewBag.mediciones = new SelectList(await ListaIM.TipoMediciones(), "Id_TipoMedicion", "NombreTipoMedicion ");
            ViewBag.estado = new SelectList(ListaIM.Estados(), "Value", "Text");
            if (id == 0)
            {
                return HttpNotFound();
            }
            return View(InstMediciones);
        }

        // GET: Sub_NomInstrumentoMedicion/Create
        public async Task<ActionResult> Create()
        {
            var repoIM = new IMRepositorio(db);
            ViewBag.mediciones = new SelectList(await repoIM.TipoMediciones(), "Id_TipoMedicion", "NombreTipoMedicion ");
            ViewBag.estado = new SelectList(repoIM.Estados(), "Value", "Text");
            return View();
        }

        // POST: Sub_NomInstrumentoMedicion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_InstrumentoMedicion,Id_TipoMedicion,Instrumento,ModeloTipo,Serie,Fabricante,AnoFabricacion,Pais,FechaVerificado,FechaVencimiento,Estado,BrigadaResponsable,Observaciones")] Sub_NomInstrumentoMedicion sub_NomInstrumentoMedicion)
        {
            if (ModelState.IsValid)
            {
                db.Sub_NomInstrumentoMedicion.Add(sub_NomInstrumentoMedicion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var repoIM = new IMRepositorio(db);
            ViewBag.mediciones = new SelectList(await repoIM.TipoMediciones(), "Id_TipoMedicion", "NombreTipoMedicion ");
            ViewBag.estado = new SelectList(repoIM.Estados(), "Value", "Text");
            return View(sub_NomInstrumentoMedicion);
        }

        // GET: Sub_NomInstrumentoMedicion/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_NomInstrumentoMedicion IM =  db.Sub_NomInstrumentoMedicion.Find(id);
            var repoIM = new IMRepositorio(db);
            ViewBag.tipo = new SelectList(repoIM.Estados(), "Value", "Text", IM.Estado);

            ViewBag.mediciones = new SelectList(await repoIM.TipoMediciones(), "Id_TipoMedicion", "NombreTipoMedicion ",IM.Id_TipoMedicion);
            return View(IM);
        }

        // POST: Sub_NomInstrumentoMedicion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_InstrumentoMedicion,Id_TipoMedicion,Instrumento,ModeloTipo,Serie,Fabricante,AnoFabricacion,Pais,FechaVerificado,FechaVencimiento,Estado,BrigadaResponsable,Observaciones")] Sub_NomInstrumentoMedicion sub_NomInstrumentoMedicion)
        {
            var usuario = System.Web.HttpContext.Current.User?.Identity?.Name ?? null;
            string nombre_usuario = System.Web.HttpContext.Current.User.Identity.Name;
            var usuario_logueado = db.Personal.FirstOrDefault(c => c.Nombre == nombre_usuario);
            int? EAdmin = usuario_logueado.id_EAdministrativa;
            Repositorio brep = new Repositorio(db);
            if (ModelState.IsValid)
            {
                db.Entry(sub_NomInstrumentoMedicion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //var repoIM = new IMRepositorio(db);
            //ViewBag.estado = new SelectList(repoIM.Estados(), "Value", "Text", sub_NomInstrumentoMedicion.Estado);
            //ViewBag.mediciones = new SelectList(await repoIM.TipoMediciones(), "Id_TipoMedicion", "NombreTipoMedicion ", sub_NomInstrumentoMedicion.Id_TipoMedicion);
            return View(sub_NomInstrumentoMedicion);
        }

        // GET: Sub_NomInstrumentoMedicion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_NomInstrumentoMedicion sub_NomInstrumentoMedicion = db.Sub_NomInstrumentoMedicion.Find(id);
            if (sub_NomInstrumentoMedicion == null)
            {
                return HttpNotFound();
            }
            return View(sub_NomInstrumentoMedicion);
        }

        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            //if (ValidarSiVinculado(id))
            //{
            //    return Json(false, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{aquí se deja eliminar aunque el IM este asociado a un mantenimiento...
                try
                {
                    Sub_NomInstrumentoMedicion IM = db.Sub_NomInstrumentoMedicion.Find(id);
                    db.Sub_NomInstrumentoMedicion.Remove(IM);
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            //}
        }

        // POST: Sub_NomInstrumentoMedicion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sub_NomInstrumentoMedicion sub_NomInstrumentoMedicion = db.Sub_NomInstrumentoMedicion.Find(id);
            db.Sub_NomInstrumentoMedicion.Remove(sub_NomInstrumentoMedicion);
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

        public bool ValidarSiVinculado(int idIM)
        {
            var Instrumento = db.Sub_NomInstrumentoMedicion.Where(a => a.Id_InstrumentoMedicion == idIM).FirstOrDefault();

            return Instrumento != null ? true : false;
        }

        [HttpPost]
        public async Task<ActionResult> ListadoIM()
        {
            var ListaIM = new IMRepositorio(db);
            //return View(await ListaIM.ObtenerListadoIM());
            return PartialView("_VPTablaIM",await ListaIM.ObtenerListadoIM());
        }


    }
}
