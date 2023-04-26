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
using Subestaciones.Models.Clases;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class TransformadoresPotenciaController : Controller
    {
        private DBContext db = new DBContext();

        // GET: TransformadoresPotencia
        public ActionResult Index()
        {
            return View(db.Bloque.ToList());//aqui poner el listado de los TF de todas las subestaciones
        }

        // GET: TransformadoresPotencia/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bloque bloque = db.Bloque.Find(id);
            if (bloque == null)
            {
                return HttpNotFound();
            }
            return View(bloque);
        }

        // GET: TransformadoresPotencia/Create mover transformadores subtransmision
        public ActionResult Create(string mover)
        {
            var repo = new MoverTransfRepo(db);
            Repositorio repoA = new Repositorio(db);

            ViewBag.ErrorEA = 0;
            //ViewBag.TrasnfEnSub = new SelectList(repo.ObtenerTransEnSubestacion().Select(c => new { Value = c.Id_Transformador, Text = c.Nombre }), "Value", "Text");
            ViewBag.TrasnfEnSub = repo.ObtenerTransEnSubestacionDistribucion();
            //ViewBag.TrasnfEnAlm = new SelectList(repo.ObtenerTransEnAlmacen().Select(c => new { Value = c.Id_Transformador, Text = c.Nombre }), "Value", "Text");
            ViewBag.TrasnfEnAlm = repo.ObtenerTransDistribucionEnAlmacen();
            ViewBag.ListaSub = repo.subestacionesDistribucion();
            ViewBag.Almacenes = repo.ListaAlmacenes();
            ViewBag.move = mover;
            return View();
        }

        // POST: TransformadoresPotencia/Create  mover transformadores subtransmision
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MoverTransf mover)
        {
            var repo = new MoverTransfRepo(db);
            Repositorio repoA = new Repositorio(db);
            if (ModelState.IsValid)
            {
                if (mover != null)
                {
                    var destino = "";
                    if (Request.Form["submitButton"].ToString() == "ParaAlmacen")
                    {
                        destino = mover.codAlmacen;
                    }
                    else
                          if (Request.Form["submitButton"].ToString() == "ParaSubestacion")
                    {
                        destino = mover.codigo;
                    }

                        var transformador = db.TransformadoresSubtransmision.Find(mover.idEA, mover.idTransformador);
                        if (transformador != null)
                        {
                            transformador.Id_EAdministrativa = mover.idEA;
                            transformador.Codigo = destino;
                            repoA.GetNumAccion("M", "TTS", transformador.NumAccion);
                            db.Entry(transformador).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Create", new { mover = "Si" });
                        }
                }
            }
            ViewBag.TrasnfEnSub = repo.ObtenerTransEnSubestacionDistribucion();
            ViewBag.TrasnfEnAlm = repo.ObtenerTransDistribucionEnAlmacen();
            ViewBag.Almacenes = repo.ListaAlmacenes();
            ViewBag.ListaSub = repo.subestacionesDistribucion();
            ViewBag.move = "Si";
            return View();
        }

        // GET: TransformadoresPotencia/Edit/5 mover transformadores de transmision
        public ActionResult Edit(string mover)
        {
            var repo = new MoverTransfRepo(db);
            Repositorio repoA = new Repositorio(db);

            ViewBag.ErrorEA = 0;
            //ViewBag.TrasnfEnSub = new SelectList(repo.ObtenerTransEnSubestacion().Select(c => new { Value = c.Id_Transformador, Text = c.Nombre }), "Value", "Text");
            ViewBag.TrasnfEnSub = repo.ObtenerTransEnSubestacionTransmision();
            //ViewBag.TrasnfEnAlm = new SelectList(repo.ObtenerTransEnAlmacen().Select(c => new { Value = c.Id_Transformador, Text = c.Nombre }), "Value", "Text");
            ViewBag.TrasnfEnAlm = repo.ObtenerTransTransmisionEnAlmacen();
            ViewBag.ListaSub = repo.subestacionesTransmision();
            ViewBag.Almacenes = repo.ListaAlmacenes();
            ViewBag.move = mover;
            return View();
        }

        // POST: TransformadoresPotencia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MoverTransf mover)
        {
            var repo = new MoverTransfRepo(db);
            Repositorio repoA = new Repositorio(db);
            if (ModelState.IsValid)
            {
                if (mover != null)
                {
                    var destino = "";
                    if (Request.Form["submitButton"].ToString() == "ParaAlmacen")
                    {
                        destino = mover.codAlmacen;
                    }
                    else
                          if (Request.Form["submitButton"].ToString() == "ParaSubestacion")
                    {
                        destino = mover.codigo;
                    }
                    var transformador = db.TransformadoresTransmision.Find(mover.idEA, mover.idTransformador);
                        if (transformador != null)
                        {
                            transformador.Codigo = mover.codigo;
                            transformador.NumAccion = repoA.GetNumAccion("M", "TTS", transformador.NumAccion);
                            db.Entry(transformador).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Edit", new { mover = "Si" });
                        }
                }
            }
            ViewBag.TrasnfEnSub = repo.ObtenerTransEnSubestacionTransmision();
            ViewBag.TrasnfEnAlm = repo.ObtenerTransTransmisionEnAlmacen();
            ViewBag.Almacenes = repo.ListaAlmacenes();
            ViewBag.ListaSub = repo.subestacionesTransmision();
            ViewBag.move = "Si";
            return View();
        }

        // GET: TransformadoresPotencia/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bloque bloque = db.Bloque.Find(id);
            if (bloque == null)
            {
                return HttpNotFound();
            }
            return View(bloque);
        }

        // POST: TransformadoresPotencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Bloque bloque = db.Bloque.Find(id);
            db.Bloque.Remove(bloque);
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
    }
}
