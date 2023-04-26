using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class BreakersController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Breakers
        public ActionResult Index()
        {
            return View(db.Breakers.ToList());
        }

        // GET: Breakers/Details/5
        public ActionResult Details(short ea, int id)
        {
            Breakers breakers = db.Breakers.Find(ea, id);
            if (breakers == null)
            {
                return HttpNotFound();
            }

            var fabricante = (
                  from sb in db.Fabricantes
                  select new SelectListItem { Value = sb.Id_Fabricante.ToString(), Text = sb.Nombre.ToString() }
              ).ToList();

            var TensionFuse = (
                from sb in db.Inst_TensionFusible
                select new SelectListItem { Value = sb.Id_TensionFusible.ToString(), Text = sb.DescripcionTensionFusible.ToString() }
            ).ToList();

            var CapacidadFuse = (
              from sb in db.Inst_CapacidadFusible
              select new SelectListItem { Value = sb.Id_CapacidadFusible.ToString(), Text = sb.DescripcionCapacidadFusible.ToString() }
          ).ToList();

            ViewBag.fab = new SelectList(fabricante, "Value", "Text", breakers.id_fabricante);
            ViewBag.tension = new SelectList(TensionFuse, "Value", "Text", breakers.id_VoltajeN);
            ViewBag.capacidad = new SelectList(CapacidadFuse, "Value", "Text", breakers.Capacidad_breaker);

            return View(breakers);
        }

        // GET: Breakers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Breakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_EAdministrativa,Id_Breaker,NumAccion,CodigoBreaker,Codigo,id_fabricante,id_VoltajeN,Capacidad_breaker,modelo,id_EAdministrativa_Prov")] Breakers breakers)
        {
            if (ModelState.IsValid)
            {
                db.Breakers.Add(breakers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(breakers);
        }

        // GET: Breakers/Edit/5
        public ActionResult Edit(short ea, int id)
        {
            
            Breakers breakers = db.Breakers.Find(ea, id);
            if (breakers == null)
            {
                return HttpNotFound();
            }
           
            var fabricante = (
                  from sb in db.Fabricantes
                  select new SelectListItem { Value = sb.Id_Fabricante.ToString(), Text = sb.Nombre.ToString() }
              ).ToList();

            var TensionFuse = (
                from sb in db.Inst_TensionFusible
                select new SelectListItem { Value = sb.Id_TensionFusible.ToString(), Text = sb.DescripcionTensionFusible.ToString() }
            ).ToList();

            var CapacidadFuse = (
              from sb in db.Inst_CapacidadFusible
              select new SelectListItem { Value = sb.Id_CapacidadFusible.ToString(), Text = sb.DescripcionCapacidadFusible.ToString() }
          ).ToList();

            ViewBag.fab = new SelectList(fabricante, "Value", "Text", breakers.id_fabricante);
            ViewBag.tension = new SelectList(TensionFuse, "Value", "Text", breakers.id_VoltajeN);
            ViewBag.capacidad = new SelectList(CapacidadFuse, "Value", "Text", breakers.Capacidad_breaker);

            return View(breakers);
        }

        // POST: Breakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_EAdministrativa,Id_Breaker,NumAccion,CodigoBreaker,Codigo,id_fabricante,id_VoltajeN,Capacidad_breaker,modelo,id_EAdministrativa_Prov")] Breakers breakers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(breakers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "InstalacionDesconectivos");
            }

           
            var fabricante = (
                  from sb in db.Fabricantes
                  select new SelectListItem { Value = sb.Id_Fabricante.ToString(), Text = sb.Nombre.ToString() }
              ).ToList();

            var TensionFuse = (
                from sb in db.Inst_TensionFusible
                select new SelectListItem { Value = sb.Id_TensionFusible.ToString(), Text = sb.DescripcionTensionFusible.ToString() }
            ).ToList();

            var CapacidadFuse = (
              from sb in db.Inst_CapacidadFusible
              select new SelectListItem { Value = sb.Id_CapacidadFusible.ToString(), Text = sb.DescripcionCapacidadFusible.ToString() }
          ).ToList();

            ViewBag.fab = new SelectList(fabricante, "Value", "Text", breakers.id_fabricante);
            ViewBag.tension = new SelectList(TensionFuse, "Value", "Text", breakers.id_VoltajeN);
            ViewBag.capacidad = new SelectList(CapacidadFuse, "Value", "Text", breakers.Capacidad_breaker);
            return View(breakers);
        }

        [HttpPost]
        public ActionResult eliminarB(short ea, int id, string codigo, int idEAProv)
        {//cuando se cambia el tipo de portafusible
            //elimino el breaker
            Breakers b = db.Breakers.Find(ea, id);
            db.Breakers.Remove(b);

            Repositorio repo = new Repositorio(db);

            //agrego el fuse
            var id_fuse = db.Database.SqlQuery<int>(@"declare @numFuse int
                                    Select @numFuse = Max(Id_Portafusible) + 1
                                    From PortaFusibles
                                    Where Id_EAdministrativa = {0}
                                    if @numFuse is null
                                    set @numFuse = 1
                                    Select @numFuse as idFuse", ea).First();
            PortaFusibles p = new PortaFusibles
            {
                Id_EAdministrativa = ea,
                NumAccion = repo.GetNumAccion("I", "GDD", 0),
                CodigoPortafusible = codigo,
                Id_Portafusible = id_fuse,
                id_EAdministrativa_Prov = idEAProv
            };
            db.PortaFusibles.Add(p);

            db.SaveChanges();
            //return RedirectToAction("Edit", "PortaFusibles", new { ea = p.Id_EAdministrativa, id = p.Id_Portafusible });
            return RedirectToAction("Index", "InstalacionDesconectivos");

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
