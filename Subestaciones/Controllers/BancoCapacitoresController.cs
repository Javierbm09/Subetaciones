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
    public class BancoCapacitoresController : Controller
    {
        private DBContext db = new DBContext();
        
        // GET: BancoCapacitores
        public async Task<ActionResult> Index(string inserta)
        {
            ViewBag.Inserto = inserta;

            var ListaCap = new CapacitorRepositorio(db);
            return View(await ListaCap.ObtenerListadoCapacitores());
        }

        // GET: BancoCapacitores/Details/5
        public async Task<ActionResult> Details(string Codigo)
        {
            if (Codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaCap = new CapacitorRepositorio(db);
            var banco = await ListaCap.FindAsync(Codigo);
            if (Codigo == null)
            {
                return HttpNotFound();
            }
            return View(banco);
        }

        // GET: BancoCapacitores/Create
        public async Task<ActionResult> Create()
        {
            var InicializaSub = new Repositorio(db);
            var InicializaCap = new CapacitorRepositorio(db);
            ViewBag.CircuitoList = new SelectList(InicializaSub.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TipoControl = new SelectList(InicializaCap.TipoC(), "Value", "Text");
            //ViewBag.Seccionalizador = new SelectList(await InicializaCap.seccionalizador(), "Codigo", "Codigo");
            ViewBag.EstadoOperativo = new SelectList(await InicializaCap.EO(), "Id_EstadoOperativo", "EstadoOperativo");
            return View();
        }

        // POST: BancoCapacitores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [TienePermiso(Servicio: 28)]//Servicio: crear capacitor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Codigo,CodigoAntiguo,Calle,Numero,Entrecalle1,Entrecalle2,BarrioPueblo,Sucursal,Seccionalizador,Circuito,Conexion,TipoControl,CKVAR_Instalado,Id_EAdministrativa,NumAccion,EstadoOperativo,coddireccion,Id_EAdireccion,Id_Seccion,FechaInstalado")] BancoCapacitores bancoCapacitores)
        {
            var repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario(); //esta EA ya esta bien
            var unionSub = repo.ObtenerUnionSub(bancoCapacitores.Circuito);

           if (ModelState.IsValid)
            {
                bancoCapacitores.Id_EAdministrativa = Id_Eadministrativa;
                bancoCapacitores.NumAccion = repo.GetNumAccion("I", "SUC", 0);
                if (unionSub != null)
                {
                    bancoCapacitores.Calle = unionSub.Calle;

                    db.BancoCapacitores.Add(bancoCapacitores);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { inserta = "si" });
                }
            }
           
            var InicializaSub = new Repositorio(db);
            var InicializaCap = new CapacitorRepositorio(db);
            ViewBag.TipoControl = new SelectList(InicializaCap.TipoC(), "Value", "Text");
            ViewBag.Circuito = new SelectList(InicializaSub.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.Secc = new SelectList(await InicializaCap.seccionalizador(), "Codigo", "Codigo");
            ViewBag.EstadoOperativo = new SelectList(await InicializaCap.EO(), "Id_EstadoOperativo", "EstadoOperativo");

            return View(bancoCapacitores);
           
        }

        // GET: BancoCapacitores/Edit/5
        [TienePermiso(Servicio: 10)]
        public async Task<ActionResult> Edit(string Codigo)
        {
            if (Codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BancoCapacitores bancoCapacitores = db.BancoCapacitores.Find(Codigo);
            var repo = new Repositorio(db);
            var banco = new CapacitorRepositorio(db);
                        
            ViewBag.TipoControl = new SelectList(banco.TipoC(), "Value", "Text", bancoCapacitores.TipoControl);
            ViewBag.Circuito = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text", bancoCapacitores.Circuito);
            ViewBag.Seccionalizador = new SelectList(await banco.seccionalizador(), "Codigo", "Codigo", bancoCapacitores.Seccionalizador);
            ViewBag.EstadoOperativo = new SelectList(await banco.EO(), "Id_EstadoOperativo", "EstadoOperativo", bancoCapacitores.EstadoOperativo);
            return View(bancoCapacitores);
        }

        [HttpPost]
        public async Task<ActionResult> ValidarCodigoBanco(string CodigoAnterior, string Codigo)
        {
            try
            {
                var listaCap = await new CapacitorRepositorio(db).ObtenerListadoCapacitores();
                return Json(!listaCap.Select(c=> c.codigobanco).Where(c => c == Codigo).Any(c => c != CodigoAnterior));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        // POST: BancoCapacitores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [TienePermiso(Servicio: 10)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Circuito,Codigo,CodigoAntiguo,Calle,Numero,Entrecalle1,Entrecalle2,BarrioPueblo,Sucursal,Seccionalizador,Conexion,TipoControl,CKVAR_Instalado,Id_EAdministrativa,NumAccion,EstadoOperativo,coddireccion,Id_EAdireccion,Id_Seccion,CodigoAnterior,FechaInstalado")] BancoCapacitores bancoCapacitores)
        {
            var repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario(); //esta EA ya esta bien
            Repositorio bancorep = new Repositorio(db);
            if (ModelState.IsValid)
            {
                await db.Database.ExecuteSqlCommandAsync("UPDATE BancoCapacitores SET Codigo = @Codigo WHERE Codigo = @CodigoAnterior", new SqlParameter("@Codigo", bancoCapacitores.Codigo), new SqlParameter("@CodigoAnterior", bancoCapacitores.CodigoAnterior));

                bancoCapacitores.Id_EAdministrativa = Id_Eadministrativa;
                bancoCapacitores.NumAccion = bancorep.GetNumAccion("M", "SUC", bancoCapacitores.NumAccion ?? 0);
                db.Entry(bancoCapacitores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { inserta = "si" });
            }
            return View(bancoCapacitores);
        }

        // GET: BancoCapacitores/Delete/5
        [TienePermiso(Servicio: 28)]
        public async Task<ActionResult> Delete(string codigo)
        {
            if (codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cap = new CapacitorRepositorio(db);
            var capacitor = await cap.FindAsync(codigo);
                     
            if (capacitor == null)
            {
                return HttpNotFound();
            }
            return View(capacitor);
        }

        // POST: BancoCapacitores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string codigo)
        {
            Repositorio br = new Repositorio(db);
            BancoCapacitores elimina_cap = db.BancoCapacitores.FindAsync(codigo).Result;
            int accion = br.GetNumAccion("B", "SUC", elimina_cap.NumAccion ?? 0);
            db.BancoCapacitores.Remove(elimina_cap);
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

        [AllowAnonymous]
        public async Task<ActionResult> CargarSecc(string codsub)
        {
            var InicializaCap = new CapacitorRepositorio(db);
            ViewBag.Secc = new SelectList(await InicializaCap.seccionalizador(codsub), "Codigo", "Codigo");

            return PartialView("_VPSeccionalizador");
        }

        public ActionResult Eliminar(string NoSerie)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                BancoCapacitores Cap = db.BancoCapacitores.Find(NoSerie);
                db.BancoCapacitores.Remove(Cap);
                int accion = br.GetNumAccion("B", "SUC", Cap.NumAccion ?? 0);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public async Task<ActionResult> ListadoCapacitores()
        {
            var ListaCap = new CapacitorRepositorio(db);
            return PartialView("_VPCapacitores", await ListaCap.ObtenerListadoCapacitores());
        }
    }
}
