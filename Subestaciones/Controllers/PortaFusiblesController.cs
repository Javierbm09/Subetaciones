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
    public class PortaFusiblesController : Controller
    {
        private DBContext db = new DBContext();

        // GET: PortaFusibles
        public ActionResult Index()
        {
            return View(db.PortaFusibles.ToList());
        }

        // GET: PortaFusibles/Details/5
        public ActionResult Details(short ea, int id)
        {
            PortaFusibles portaFusibles = db.PortaFusibles.Find(ea, id);
            if (portaFusibles == null)
            {
                return HttpNotFound();
            }

            var TipoFuse = (
                  from sb in db.Inst_TipoFusible
                  select new SelectListItem { Value = sb.Id_TipoFusible.ToString(), Text = sb.DescripcionTipoFusible.ToString() }
              ).ToList();

            var TensionFuse = (
                from sb in db.Inst_TensionFusible
                select new SelectListItem { Value = sb.Id_TensionFusible.ToString(), Text = sb.DescripcionTensionFusible.ToString() }
            ).ToList();

            var CapacidadFuse = (
              from sb in db.Inst_CapacidadFusible
              select new SelectListItem { Value = sb.Id_CapacidadFusible.ToString(), Text = sb.DescripcionCapacidadFusible.ToString() }
          ).ToList();

            var tipoPortaFusible = (
             from TF in db.Inst_TipoPortaFusible
             select new SelectListItem { Value = TF.Id_TipoPortaFusible.ToString(), Text = TF.DescripcionTipoPortaFusible.ToString() }
         ).ToList();



            ViewBag.tipoPortafusibleLista = new SelectList(tipoPortaFusible, "Value", "Text", portaFusibles.TipoFuse);
            ViewBag.tipoFusible = new SelectList(TipoFuse, "Value", "Text", portaFusibles.Id_Fusible);
            ViewBag.tension = new SelectList(TensionFuse, "Value", "Text", portaFusibles.Id_VoltajeN);
            ViewBag.capacidad = new SelectList(CapacidadFuse, "Value", "Text", portaFusibles.CapacidadFusible);

            return View(portaFusibles);
        }

        // GET: PortaFusibles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PortaFusibles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_EAdministrativa,Id_Portafusible,NumAccion,CodigoPortafusible,Codigo,TipoFuse,TEquipoProt,CE,Estado,Fase,Ubicacion,EstadoOperativo,Marca,Id_VoltajeN,CorrienteNominal,FusibleAjuste,CorrienteCortoCto,Id_Fabricante,Id_Fusible,TipoFusible,Id_FabricanteFusible,CapacidadFusible,id_EAdministrativa_Prov")] PortaFusibles portaFusibles)
        {
            if (ModelState.IsValid)
            {
                db.PortaFusibles.Add(portaFusibles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(portaFusibles);
        }

        // GET: PortaFusibles/Edit/5
        public ActionResult Edit(short ea, int id)
        {
          
            PortaFusibles portaFusibles = db.PortaFusibles.Find(ea, id);
            if (portaFusibles == null)
            {
                return HttpNotFound();
            }

            var TipoFuse = (
                  from sb in db.Inst_TipoFusible
                  select new SelectListItem { Value = sb.Id_TipoFusible.ToString(), Text = sb.DescripcionTipoFusible.ToString() }
              ).ToList();

            var TensionFuse = (
                from sb in db.Inst_TensionFusible
                select new SelectListItem { Value = sb.Id_TensionFusible.ToString(), Text = sb.DescripcionTensionFusible.ToString() }
            ).ToList();

            var CapacidadFuse = (
              from sb in db.Inst_CapacidadFusible
              select new SelectListItem { Value = sb.Id_CapacidadFusible.ToString(), Text = sb.DescripcionCapacidadFusible.ToString() }
          ).ToList();

            var tipoPortaFusible = (
             from TF in db.Inst_TipoPortaFusible
             select new SelectListItem { Value = TF.Id_TipoPortaFusible.ToString(), Text = TF.DescripcionTipoPortaFusible.ToString() }
         ).ToList();



            ViewBag.tipoPortafusibleLista = new SelectList(tipoPortaFusible, "Value", "Text", portaFusibles.TipoFuse);
            ViewBag.tipoFusible = new SelectList(TipoFuse, "Value", "Text", portaFusibles.Id_Fusible);
            ViewBag.tension = new SelectList(TensionFuse, "Value", "Text", portaFusibles.Id_VoltajeN);
            ViewBag.capacidad = new SelectList(CapacidadFuse, "Value", "Text", portaFusibles.CapacidadFusible);

            return View(portaFusibles);
        }

        // POST: PortaFusibles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_EAdministrativa,Id_Portafusible,NumAccion,CodigoPortafusible,Codigo,TipoFuse,TEquipoProt,CE,Estado,Fase,Ubicacion,EstadoOperativo,Marca,Id_VoltajeN,CorrienteNominal,FusibleAjuste,CorrienteCortoCto,Id_Fabricante,Id_Fusible,TipoFusible,Id_FabricanteFusible,CapacidadFusible,id_EAdministrativa_Prov")] PortaFusibles portaFusibles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(portaFusibles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "InstalacionDesconectivos");

            }

            var TipoFuse = (
                 from sb in db.Inst_TipoFusible
                 select new SelectListItem { Value = sb.Id_TipoFusible.ToString(), Text = sb.DescripcionTipoFusible.ToString() }
             ).ToList();

            var TensionFuse = (
                from sb in db.Inst_TensionFusible
                select new SelectListItem { Value = sb.Id_TensionFusible.ToString(), Text = sb.DescripcionTensionFusible.ToString() }
            ).ToList();

            var CapacidadFuse = (
              from sb in db.Inst_CapacidadFusible
              select new SelectListItem { Value = sb.Id_CapacidadFusible.ToString(), Text = sb.DescripcionCapacidadFusible.ToString() }
          ).ToList();

            var tipoPortaFusible = (
             from TF in db.Inst_TipoPortaFusible
             select new SelectListItem { Value = TF.Id_TipoPortaFusible.ToString(), Text = TF.DescripcionTipoPortaFusible.ToString() }
         ).ToList();



            ViewBag.tipoPortafusibleLista = new SelectList(tipoPortaFusible, "Value", "Text", portaFusibles.TipoFuse);
            ViewBag.tipoFusible = new SelectList(TipoFuse, "Value", "Text", portaFusibles.Id_Fusible);
            ViewBag.tension = new SelectList(TensionFuse, "Value", "Text", portaFusibles.Id_VoltajeN);
            ViewBag.capacidad = new SelectList(CapacidadFuse, "Value", "Text", portaFusibles.CapacidadFusible);

            return View(portaFusibles);
        }

        [HttpPost]
        public ActionResult eliminar(short ea, int id, string codigo, int idEAProv)
        {//cuando se cambia el tipo de portafusible
            //elimino el fusible
            PortaFusibles portaFusibles = db.PortaFusibles.Find(ea, id);
            db.PortaFusibles.Remove(portaFusibles);

            Repositorio repo = new Repositorio(db);

            //agrego el breaker
            var id_breaker = db.Database.SqlQuery<int>(@"declare @numBreaker int
                                    Select @numBreaker = Max(Id_Breaker) + 1
                                    From Breakers
                                    Where id_EAdministrativa = {0}
                                    if @numBreaker is null
                                    set @numBreaker = 1
                                    Select @numBreaker as idBreaker", ea).First();
            Breakers b = new Breakers
            {
                id_EAdministrativa = ea,
                NumAccion = repo.GetNumAccion("I", "GDD", 0),
                CodigoBreaker = codigo,
                Id_Breaker = id_breaker,
                id_EAdministrativa_Prov = idEAProv
            };
            db.Breakers.Add(b);
            db.SaveChanges();
            //return RedirectToAction("Edit", "Breakers", new { ea = b.id_EAdministrativa, id = b.Id_Breaker });
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
