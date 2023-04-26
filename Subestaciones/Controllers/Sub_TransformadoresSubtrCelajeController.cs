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
    public class Sub_TransformadoresSubtrCelajeController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_TransformadoresSubtrCelaje
        public ActionResult Index()
        {
            return View(db.Sub_TransformadoresSubtrCelaje.ToList());
        }

        // GET: Sub_TransformadoresSubtrCelaje/Details/5
        public ActionResult Details(string nomb, string Codigo, DateTime fechaIns, int t, int EA, string valor)
        {
            if ((nomb == null) && (Codigo == null) && (fechaIns == null) && (t == null) && (EA == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Sub_TransformadoresSubtrCelaje sub_TransformadoresSubtrCelaje = db.Sub_TransformadoresSubtrCelaje.Find(nomb, Codigo, fecha, t, EA);
            var ListaTransF = new InspSubDistRepo(db);

            var inspTF = ListaTransF.findInspTransf (nomb, Codigo, fechaIns, t, EA).FirstOrDefault();

            if (inspTF == null)
            {
                return HttpNotFound();
            }

            ViewBag.nomb = nomb;
            ViewBag.codSub = Codigo;
            ViewBag.fechaInsp = fechaIns;
            ViewBag.valor = valor;

            return View(inspTF);
        }

        // GET: Sub_TransformadoresSubtrCelaje/Create
        public ActionResult Create(string nombreCelaje, string sub, DateTime? fechaIns, string valor)
        {

            var repoInsp = new InspSubDistRepo(db);

            ViewBag.nomb = nombreCelaje;
            ViewBag.codSub = sub;
            ViewBag.fechaInsp = fechaIns;
            ViewBag.valor = valor;
            ViewBag.aceite = repoInsp.nivelAceite();
            ViewBag.estpintura = repoInsp.estados();
            ViewBag.aterr = repoInsp.estadoPararrayoA();
            ViewBag.bush = repoInsp.estados();

            return View();
        }

        // POST: Sub_TransformadoresSubtrCelaje/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombreCelaje,codigoSub,fecha,Id_EAdministrativa,Id_Transformador,salidero,nAceite,pintura,aterramiento,bushing,temperaturaReal,observaciones")] Sub_TransformadoresSubtrCelaje sub_TransformadoresSubtrCelaje)
        {
            if (ModelState.IsValid)
            {
                if (ValidarExisteInsTransf(sub_TransformadoresSubtrCelaje.nombreCelaje, sub_TransformadoresSubtrCelaje.codigoSub, sub_TransformadoresSubtrCelaje.fecha, sub_TransformadoresSubtrCelaje.Id_EAdministrativa, sub_TransformadoresSubtrCelaje.Id_Transformador))
                {
                    db.Sub_TransformadoresSubtrCelaje.Add(sub_TransformadoresSubtrCelaje);
                    db.SaveChanges();
                    if (Request.Form["submitButton"].ToString() == "Editar")
                    {
                        return RedirectToAction("Edit", "Sub_CelajeSubDistribucion", new { nombInsp = sub_TransformadoresSubtrCelaje.nombreCelaje, codigo = sub_TransformadoresSubtrCelaje.codigoSub, fechaIns = sub_TransformadoresSubtrCelaje.fecha });

                    }
                    return RedirectToAction("ComponentesInsp", "Sub_CelajeSubDistribucion", new { nombInsp = sub_TransformadoresSubtrCelaje.nombreCelaje, codigo = sub_TransformadoresSubtrCelaje.codigoSub, fecha = sub_TransformadoresSubtrCelaje.fecha });
                }

                else
                {
                    ModelState.AddModelError("codigoSub", "Ya existe una inspección al transformador en la fecha.");

                }
            }
            var repoInsp = new InspSubDistRepo(db);

            ViewBag.nomb = sub_TransformadoresSubtrCelaje.nombreCelaje;
            ViewBag.codSub = sub_TransformadoresSubtrCelaje.codigoSub;
            ViewBag.fechaInsp = sub_TransformadoresSubtrCelaje.fecha;
            ViewBag.valor = Request.Form["submitButton"].ToString();
            ViewBag.aceite = repoInsp.nivelAceite();
            ViewBag.estpintura = repoInsp.estados();
            ViewBag.aterr = repoInsp.estadoPararrayoA();
            ViewBag.bush = repoInsp.estados();

            return View(sub_TransformadoresSubtrCelaje);
        }

        // GET: Sub_TransformadoresSubtrCelaje/Edit/5
        public ActionResult Edit(string nomb, string Codigo, DateTime fechaIns, int t, int EA, string valor)
        {
            if ((nomb == null) && (Codigo == null) && (fechaIns == null) && (t == null) && (EA == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaTransF = new InspSubDistRepo(db);

            var inspTF = ListaTransF.findInspTransf(nomb, Codigo, fechaIns, t, EA).FirstOrDefault();

            if (inspTF == null)
            {
                return HttpNotFound();
            }

            ViewBag.nomb = nomb;
            ViewBag.codSub = Codigo;
            ViewBag.fechaInsp = fechaIns;
            ViewBag.valor = valor;

            var repoInsp = new InspSubDistRepo(db);


            ViewBag.aceite = repoInsp.nivelAceite();
            ViewBag.estpintura = repoInsp.estados();
            ViewBag.aterr = repoInsp.estadoPararrayoA();
            ViewBag.bush = repoInsp.estados();

            return View(inspTF);
        }

        // POST: Sub_TransformadoresSubtrCelaje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nombreCelaje,codigoSub,fecha,Id_EAdministrativa,Id_Transformador,salidero,nAceite,pintura,aterramiento,bushing,temperaturaReal,observaciones")] Sub_TransformadoresSubtrCelaje sub_TransformadoresSubtrCelaje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_TransformadoresSubtrCelaje).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.Form["submitButton"].ToString() == "Editar")
                {
                    return RedirectToAction("Edit", "Sub_CelajeSubDistribucion", new { nombInsp = sub_TransformadoresSubtrCelaje.nombreCelaje, codigo = sub_TransformadoresSubtrCelaje.codigoSub, fechaIns = sub_TransformadoresSubtrCelaje.fecha });

                }

                return RedirectToAction("ComponentesInsp", "Sub_CelajeSubDistribucion", new { nombInsp = sub_TransformadoresSubtrCelaje.nombreCelaje, codigo = sub_TransformadoresSubtrCelaje.codigoSub, fecha = sub_TransformadoresSubtrCelaje.fecha });

            }

            ViewBag.nomb = sub_TransformadoresSubtrCelaje.nombreCelaje;
            ViewBag.codSub = sub_TransformadoresSubtrCelaje.codigoSub;
            ViewBag.fechaInsp = sub_TransformadoresSubtrCelaje.fecha;

            var repoInsp = new InspSubDistRepo(db);
            ViewBag.valor = Request.Form["submitButton"].ToString();


            ViewBag.aceite = repoInsp.nivelAceite();
            ViewBag.estpintura = repoInsp.estados();
            ViewBag.aterr = repoInsp.estadoPararrayoA();
            ViewBag.bush = repoInsp.estados();

            return View(sub_TransformadoresSubtrCelaje);
        }

        // GET: Sub_TransformadoresSubtrCelaje/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_TransformadoresSubtrCelaje sub_TransformadoresSubtrCelaje = db.Sub_TransformadoresSubtrCelaje.Find(id);
            if (sub_TransformadoresSubtrCelaje == null)
            {
                return HttpNotFound();
            }
            return View(sub_TransformadoresSubtrCelaje);
        }

        // POST: Sub_TransformadoresSubtrCelaje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sub_TransformadoresSubtrCelaje sub_TransformadoresSubtrCelaje = db.Sub_TransformadoresSubtrCelaje.Find(id);
            db.Sub_TransformadoresSubtrCelaje.Remove(sub_TransformadoresSubtrCelaje);
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

        
        private bool ValidarExisteInsTransf(string nomb, string Codigo, DateTime fecha, int t, int EA)
        {
            Sub_TransformadoresSubtrCelaje inspT = db.Sub_TransformadoresSubtrCelaje.Find(nomb, Codigo, fecha, t, EA);


            return (inspT == null);
        }

        public ActionResult _VPTransf(string sub)
        {

            var lista = db.TransformadoresSubtransmision.Where(x => x.Codigo == sub).ToList();

            ViewBag.listaTransf = lista;
            ViewBag.cant = lista.Count();

            return PartialView();
        }

        public JsonResult DatoTransf(string sub, int ea, int t)
        {
            var lista = db.TransformadoresSubtransmision.Where(x => x.Codigo == sub).ToList();

            ViewBag.listaTransf = lista;
            ViewBag.cant = lista.Count();
            //SubMttoSubDistRepositorio repo = new SubMttoSubDistRepositorio(db);
            //var transf = repo.FindTransf(sub, ea, t);

            var ListaTransformadores = new TransfSubtRepositorio(db);
            var Transformador = ListaTransformadores.Find(ea, t, "TS");
            ViewBag.nroPos = Transformador.NroPosiciones;

            return Json(Transformador, JsonRequestBehavior.AllowGet);
        }
    }
}
