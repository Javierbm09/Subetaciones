using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models.Clases;
using Subestaciones.Models;
using Subestaciones.Models.Repositorio;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Subestaciones.Controllers
{
    
    public class Sub_BarraController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_Barra
        public async Task<ActionResult> Index(string inserta)
        {
            ViewBag.Inserto = inserta;

            var ListaBarras = new BarraRepositorio(db);
            return View(await ListaBarras.ObtenerListadoBarras());
        }

        // GET: Sub_Barra/Details/5
        public async Task<ActionResult> Details(string Codigosub, string Codigobarra)
        {            
            if (Codigosub == null || Codigobarra == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaBarras = new BarraRepositorio(db);
            var sub_Barra=await ListaBarras.FindAsync(Codigosub, Codigobarra);
            if (Codigosub == null || Codigobarra == null)
            {
                return HttpNotFound();
            }
            return View(sub_Barra);
        }

        // GET: Sub_Barra/Create
        [TienePermiso(Servicio: 29)]//Servicio: crear barra
        public async Task<ActionResult> Create()
        {
            var Inicializa = new BarraRepositorio(db);
            var repo = new Repositorio(db);
            //var subs = repo.subs()
            //    .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }).ToList();
            var conds = Inicializa.conductores()
                .Select(c => new SelectListItem { Value = c.codigo, Text = c.codigo + " - " + c.TCond + " - " + c.material + " - " + c.calibre + " - " + c.recubrimiento }).ToList();
            var tension = await Inicializa.voltaje();
                
                var te = tension.Select(c => new SelectListItem { Value = c.Id_VoltajeSistema.ToString(), Text = c.Voltaje.ToString() });
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.Conductor = Inicializa.conductores();/*new SelectList(conds, "Value", "Text");*/
            ViewBag.ID_Voltaje = new SelectList(te, "Value", "Text");
            return View();
        }


        // POST: Sub_Barra/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [TienePermiso(Servicio: 29)]//Servicio: crear barra
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Subestacion,codigo,Conductor,ID_Voltaje,corriente,CantidadCond,equipo1,equipo2,Id_EAdministrativa,NumAccion")] Sub_Barra sub_Barra)
        {
            var repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario(); //esta EA ya esta bien
            if (ModelState.IsValid)
            {
                if (await ValidarCodigo(sub_Barra.SubAnterior, sub_Barra.CodAnterior, sub_Barra.Subestacion, sub_Barra.codigo))
                {
                    sub_Barra.Id_EAdministrativa = Id_Eadministrativa;
                    sub_Barra.NumAccion = repo.GetNumAccion("I", "SUB", 0);
                    db.Sub_Barra.Add(sub_Barra);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { inserta = "si" });
                }
                else
                {
                    ModelState.AddModelError("Subestacion", "Ya existe la barra en la subestación.");
                }
            }
            var Inicializa = new BarraRepositorio(db);
            var subs2 = repo.subs()
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }).ToList();
            var tension = await Inicializa.voltaje();

            var te = tension.Select(c => new SelectListItem { Value = c.Id_VoltajeSistema.ToString(), Text = c.Voltaje.ToString() });
            ViewBag.Subestacion = new SelectList(subs2, "Value", "Text");
            ViewBag.Conductor = Inicializa.conductores();
            ViewBag.ID_Voltaje = new SelectList(te, "Value", "Text");
            return View(sub_Barra);
        }

        // GET: Sub_Barra/Edit/5
        [TienePermiso(Servicio: 11)]//Servicio: editar barra
        public async Task<ActionResult> Edit(string Codigosub, string Codigobarra)
        {
            if (Codigosub == null || Codigobarra == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sub_Barra b = await db.Sub_Barra.FindAsync(Codigosub, Codigobarra);

            var Inicializa = new BarraRepositorio(db);
            var repo = new Repositorio(db);
            var subs = repo.subs()
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }).ToList();
            //var conds = Inicializa.conductores()
            //    .Select(c => new SelectListItem { Value = c.codigo, Text = c.codigo + " - " + c.TCond + " - " + c.material + " - " + c.calibre + " - " + c.recubrimiento }).ToList();
            var tension = await Inicializa.voltaje();

            var te = tension.Select(c => new SelectListItem { Value = c.Id_VoltajeSistema.ToString(), Text = c.Voltaje.ToString() });
            ViewBag.ListadoSubestacion = new SelectList(subs, "Value", "Text", b.Subestacion);
            ViewBag.Cond = Inicializa.conductores();
            ViewBag.ID_Voltaje = new SelectList(te, "Value", "Text",b.ID_Voltaje);
            return View(b);
          
        }

        private async Task<bool> ValidarCodigo(string SubAnterior, string CodAnterior, string Subestacion, string codigo)
        {
            var listaBarras = await new BarraRepositorio(db).ObtenerListadoBarras();
            
            return !listaBarras.Select(c => new { c.sub, c.barra }).Where(c => c.sub == Subestacion && c.barra == codigo).Any(c => c.sub != SubAnterior || c.barra != CodAnterior);
        }

        [HttpPost]
        public async Task<ActionResult> ValidarCodigos(string SubAnterior, string CodAnterior, string Subestacion, string codigo)
        {
            try
            {        
                return Json(await ValidarCodigo(SubAnterior, CodAnterior, Subestacion, codigo));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        // POST: Sub_Barra/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [TienePermiso(Servicio: 11)]//Servicio: editar barra
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Subestacion,codigo,Conductor,ID_Voltaje,corriente,CantidadCond,equipo1,equipo2,SubAnterior,CodAnterior ")] Sub_Barra sub_Barra)
        {
            var repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario(); //esta EA ya esta bien

            Repositorio brep = new Repositorio(db);
            if (ModelState.IsValid)
            {
                //var subAnterior = Request.Form["SubAnterior"].ToString();
                //var codAnterior = Request.Form["CodAnterior"].ToString();                

                if ((sub_Barra.SubAnterior != sub_Barra.Subestacion) || (sub_Barra.CodAnterior != sub_Barra.codigo))
                {
                    if (await ValidarCodigo(sub_Barra.SubAnterior, sub_Barra.CodAnterior, sub_Barra.Subestacion, sub_Barra.codigo))
                    {
                        await db.Database.ExecuteSqlCommandAsync("UPDATE Sub_Barra SET Subestacion = @Codigosub, codigo = @Codigobarra WHERE Subestacion = @subAnterior and codigo = @codAnterior", new SqlParameter("@Codigosub", sub_Barra.Subestacion), new SqlParameter("@Codigobarra", sub_Barra.codigo), new SqlParameter("@subAnterior", sub_Barra.SubAnterior), new SqlParameter("@codAnterior", sub_Barra.CodAnterior));

                        sub_Barra.Id_EAdministrativa = Id_Eadministrativa;
                        sub_Barra.NumAccion = brep.GetNumAccion("M", "SUB", sub_Barra.NumAccion ?? 0);
                        db.Entry(sub_Barra).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", new { inserta = "si" });
                    }
                    else
                        ModelState.AddModelError("Subestacion", "Ya existe la barra en la subestación.");
                }
                else if ((sub_Barra.SubAnterior == sub_Barra.Subestacion) && (sub_Barra.CodAnterior == sub_Barra.codigo)) {
                    sub_Barra.Id_EAdministrativa = Id_Eadministrativa;
                    sub_Barra.NumAccion = brep.GetNumAccion("M", "SUB", sub_Barra.NumAccion ?? 0);
                    db.Entry(sub_Barra).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            Sub_Barra b = await db.Sub_Barra.FindAsync(sub_Barra.Subestacion, sub_Barra.codigo);

            var Inicializa = new BarraRepositorio(db);
            var subs = repo.subs()
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }).ToList();
            var conds = Inicializa.conductores()
                .Select(c => new SelectListItem { Value = c.codigo, Text = c.codigo + " - " + c.TCond + " - " + c.material + " - " + c.calibre + " - " + c.recubrimiento }).ToList();
            var tension = await Inicializa.voltaje();

            var te = tension.Select(c => new SelectListItem { Value = c.Id_VoltajeSistema.ToString(), Text = c.Voltaje.ToString() });
            ViewBag.ListadoSubestacion = new SelectList(subs, "Value", "Text", sub_Barra.SubAnterior);
            ViewBag.Cond = new SelectList(conds, "Value", "Text", sub_Barra.Conductor);
            ViewBag.ID_Voltaje = new SelectList(te, "Value", "Text", sub_Barra.ID_Voltaje);
            return View(sub_Barra);
        }

        // GET: Sub_Barra/Delete/5
        [TienePermiso(Servicio: 29)]//Servicio: eliminar barra
        public async Task<ActionResult> Delete(string Codigosub, string Codigobarra)
        {
            if (Codigosub == null || Codigobarra == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaBarras = new BarraRepositorio(db);
            var sub_Barra = await ListaBarras.FindAsync(Codigosub, Codigobarra);
            if (sub_Barra == null)
            {
                return HttpNotFound();
            }
            return View(sub_Barra);
        }

        // POST: Sub_Barra/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [TienePermiso(Servicio: 29)]//Servicio: eliminar barra
        public ActionResult DeleteConfirmed(string Codigosub, string Codigobarra)
        {
            Repositorio br = new Repositorio(db);
            Sub_Barra elimina_barra = db.Sub_Barra.FindAsync(Codigosub, Codigobarra).Result;
            int accion = br.GetNumAccion("B", "SUB", elimina_barra.NumAccion ?? 0);
            db.Sub_Barra.Remove(elimina_barra);
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

        public ActionResult Eliminar(string Codigosub, string Codigobarra)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_Barra bar = db.Sub_Barra.Find(Codigosub, Codigobarra);
                db.Sub_Barra.Remove(bar);
                int accion = br.GetNumAccion("B", "SUB", bar.NumAccion ?? 0);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<ActionResult> ListadoBarras()
        {
            var ListaCap = new BarraRepositorio(db);
            return PartialView("_VPBarra", await ListaCap.ObtenerListadoBarras());
        }
    }
}
