using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;
using Subestaciones.Models.Clases;

namespace Subestaciones.Controllers
{
    

    public class GruposGsController : Controller
    {
        private DBContext db = new DBContext();

        #region CRUD

        // GET: GruposGs
        public ActionResult Index()
        {
            //Inicializar();
            

            var Grupos = (from subD in db.Subestaciones
                           join Grupo in db.GruposGs on subD.Codigo equals Grupo.Instalacion_Transformadora
                           join TipoGrupo in db.TipoGrupos_Sigere on Grupo.Tipo equals TipoGrupo.id_tipo
                           join EA in db.EstructurasAdministrativas on Grupo.Id_EAdministrativa equals (short)EA.Id_EAdministrativa
                           join Fab in db.Fabricantes on TipoGrupo.Fabricante equals Fab.Id_Fabricante
                          where Grupo.TipoGeneracion == "E"
                          select new Clase_GruposElectrogenos
                           {
                               Subestacion = subD.Codigo + "-" + subD.NombreSubestacion,
                               Cod = Grupo.Codigo,
                               Cod_Emergencia = Grupo.Codigo_Emergencia,
                               FactorPot = TipoGrupo.factor_Potencia,
                               Fabr = Fab.Nombre,
                               Pot = TipoGrupo.Potencia,
                               Velocidad = TipoGrupo.velocidad,
                               Tension = TipoGrupo.Voltaje,
                               FactorCarga = TipoGrupo.Factor_Carga
                           }
             ).Union
                (from subT in db.SubestacionesTransmision
                 join Grupo in db.GruposGs on subT.Codigo equals Grupo.Instalacion_Transformadora
                 join TipoGrupo in db.TipoGrupos_Sigere on Grupo.Tipo equals TipoGrupo.id_tipo
                 join EA in db.EstructurasAdministrativas on Grupo.Id_EAdministrativa equals (short)EA.Id_EAdministrativa
                 join Fab in db.Fabricantes on TipoGrupo.Fabricante equals Fab.Id_Fabricante
                 where Grupo.TipoGeneracion == "E"
                 select new Clase_GruposElectrogenos
                 {
                     Subestacion = subT.Codigo + "-" + subT.NombreSubestacion,
                     Cod = Grupo.Codigo,
                     Cod_Emergencia = Grupo.Codigo_Emergencia,
                     FactorPot = TipoGrupo.factor_Potencia,
                     Fabr = Fab.Nombre,
                     Pot = TipoGrupo.Potencia,
                     Velocidad = TipoGrupo.velocidad,
                     Tension = TipoGrupo.Voltaje,
                     FactorCarga = TipoGrupo.Factor_Carga
                 }).ToList();
            //ViewBag.Grupos = Grupos;

            ViewBag.Grupos = Grupos;

            return View();
        }

        // GET: GruposGs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruposG gruposG = db.GruposGs.Find(id);
            if (gruposG == null)
            {
                return HttpNotFound();
            }
            return View(gruposG);
        }

        // GET: GruposGs/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: GruposGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Emplazamiento,Bateria,Id_Grupo,FechaIni,Tipo,Number,Instalacion_Transformadora,Calle,Numero,Entrecalle1,Entrecalle2,Sucursal,coddireccion,Eq_Sincronizacion,NormalmenteSinc,NoSerie,Desconectivo,Id_Seccion,Id_EAdireccion,BarrioPueblo,Id_EAdministrativa,TipoGeneracion,Codigo_Emergencia,NumAccion,EstadoOperativo,Generación,centg,batg,unigg")] GruposG gruposG)
        {
            if (ModelState.IsValid)
            {
                db.GruposGs.Add(gruposG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gruposG);
        }

        // GET: GruposGs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruposG gruposG = db.GruposGs.Find(id);
            if (gruposG == null)
            {
                return HttpNotFound();
            }
            return View(gruposG);
        }

        // POST: GruposGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Emplazamiento,Bateria,Id_Grupo,FechaIni,Tipo,Number,Instalacion_Transformadora,Calle,Numero,Entrecalle1,Entrecalle2,Sucursal,coddireccion,Eq_Sincronizacion,NormalmenteSinc,NoSerie,Desconectivo,Id_Seccion,Id_EAdireccion,BarrioPueblo,Id_EAdministrativa,TipoGeneracion,Codigo_Emergencia,NumAccion,EstadoOperativo,Generación,centg,batg,unigg")] GruposG gruposG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gruposG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gruposG);
        }

        // GET: GruposGs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruposG gruposG = db.GruposGs.Find(id);
            if (gruposG == null)
            {
                return HttpNotFound();
            }
            return View(gruposG);
        }

        // POST: GruposGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GruposG gruposG = db.GruposGs.Find(id);
            db.GruposGs.Remove(gruposG);
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

        #endregion

        public void Inicializar()
        {
            var Subestacion =
    (from subD in db.Subestaciones
     select subD.Codigo + "-" + subD.NombreSubestacion)
    .Union
        (from subT in db.SubestacionesTransmision
         select subT.Codigo + "-" + subT.NombreSubestacion)
;
            //var Subestacion = db.Subestaciones
            //    .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
            //    .Union(db.SubestacionesTransmision
            //        .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }))
            //     );

            //    var reles = (
            //        from rel in db.Relevadores
            //        join pl in db.Plantillas on rel.id_Plantilla equals pl.id_Plantilla
            //        join fa in db.Fabricantes on pl.Id_Fabricante equals fa.Id_Fabricante
            //        select new SelectListItem
            //        {
            //            Value = rel.Nro_Serie,
            //            Text = rel.Nro_Serie + " - " + pl.Modelo + " - " + fa.Nombre
            //        }

            //    );

            //    ViewBag.Subestacion = new SelectList(instalaciones, "Value", "Text");
            //    ViewBag.Interruptores = new SelectList(db.Desconectivos.ToList(), "Codigo", "Codigo");
            //    ViewBag.Relevadores = new SelectList(reles, "Value", "Text");
            //    ViewBag.RelevadorFunc = new SelectList(db.Relevadores.ToList(), "Nro_Serie", "Nro_Serie");
            //    ViewBag.TC = new SelectList(db.TransformadoresCorriente.ToList(), "Nro_Serie", "Nro_Serie");
            //    ViewBag.TP = new SelectList(db.TransformadoresPotencial.ToList(), "Nro_Serie", "Nro_Serie");
            //
        }

        }
}
