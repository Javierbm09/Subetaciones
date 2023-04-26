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
    public class Sub_MttoDistPararrayosController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_MttoDistPararrayos
        public ActionResult Index()
        {
            return View(db.Sub_MttoDistPararrayos.ToList());
        }

        // GET: Sub_MttoDistPararrayos/Details/5
        public ActionResult Details(string subcodigo, DateTime fecha, int idPara, int EA, string valor)
        {
            if ((subcodigo == null) && (fecha == null) && (EA == null) && (idPara == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMttoSubDistribucion mtto = db.SubMttoSubDistribucion.Find(subcodigo, fecha);

            var ListaPara= new SubMttoSubDistRepositorio(db);
            var sub_MttoDistPararrayos = ListaPara.MttoPararrayo(subcodigo, fecha, EA, idPara).FirstOrDefault();
            //Sub_MttoDistBarra mttoBarra = db.Sub_MttoDistBarra.Find(subcodigo, fecha, barra);
            if (sub_MttoDistPararrayos == null)
            {
                return HttpNotFound();
            }
            ViewBag.codSub = subcodigo;
            ViewBag.fechaMtto = fecha;
            ViewBag.valor = valor;

            return View(sub_MttoDistPararrayos);
        }

        // GET: Sub_MttoDistPararrayos/Create
        public ActionResult Create(string sub, DateTime fecha, string valor)
        {
            ViewBag.codSub = sub;
            ViewBag.fechaMtto = fecha;
            ViewBag.valor = valor;
            var repoMtto = new SubMttoSubDistRepositorio(db);

            ViewBag.estP = repoMtto.estadoPara();
            ViewBag.aterr = repoMtto.aterramientos();

            return View();
        }

        // POST: Sub_MttoDistPararrayos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodigoSub,Fecha,Id_AdminPararrayo,Id_Pararrayo,Estado,Aislamiento,EstAterramiento")] Sub_MttoDistPararrayos sub_MttoDistPararrayos)
        {
            if (ModelState.IsValid)
            {
                if (await ValidarExisteMtto(sub_MttoDistPararrayos.CodigoSub, sub_MttoDistPararrayos.Fecha, sub_MttoDistPararrayos.Id_AdminPararrayo, sub_MttoDistPararrayos.Id_Pararrayo))
                {

                    db.Sub_MttoDistPararrayos.Add(sub_MttoDistPararrayos);
                    db.SaveChanges();
                    db.SaveChanges();
                    if (Request.Form["submitButton"].ToString() == "Editar")
                    {
                        return RedirectToAction("Edit", "SubMttoSubDistribucions", new { codigo = sub_MttoDistPararrayos.CodigoSub, fecha = sub_MttoDistPararrayos.Fecha });

                    }
                    return RedirectToAction("ComponentesMtto", "SubMttoSubDistribucions", new { codigo = sub_MttoDistPararrayos.CodigoSub, fecha = sub_MttoDistPararrayos.Fecha });
                }
                else
                {
                    ModelState.AddModelError("barra", "Ya se le realizó un mantenimiento al pararrayo seleccionado en la fecha seleccionada.");

                }
            }

            ViewBag.codSub = sub_MttoDistPararrayos.CodigoSub;
            ViewBag.fechaMtto = sub_MttoDistPararrayos.Fecha;
            ViewBag.valor = Request.Form["submitButton"].ToString();

            var repoMtto = new SubMttoSubDistRepositorio(db);

            ViewBag.estP = repoMtto.estadoPara();
            ViewBag.aterr = repoMtto.aterramientos();

            return View(sub_MttoDistPararrayos);
        }

        private async Task<bool> ValidarExisteMtto(string Codigo, DateTime fecha, int EA, int para)
        {
            Sub_MttoDistPararrayos mttoPara = db.Sub_MttoDistPararrayos.Find(Codigo, fecha, EA, para);
            if (mttoPara == null)
            {
                return true;
            }
            return false;

            //return !listaMttos.Select(c => new { c.CodigoSub, c.Fecha }).Any(c => c.CodigoSub == Codigo && c.Fecha == fecha);
        }



        // GET: Sub_MttoDistPararrayos/Edit/5
        public ActionResult Edit(string subcodigo, DateTime fecha, int idPara, int EA, string valor)
        {
            if ((subcodigo == null) && (fecha == null) && (EA == null) && (idPara == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMttoSubDistribucion mtto = db.SubMttoSubDistribucion.Find(subcodigo, fecha);

            var ListaPara = new SubMttoSubDistRepositorio(db);
            var sub_MttoDistPararrayos = ListaPara.MttoPararrayo(subcodigo, fecha, EA, idPara).FirstOrDefault();
            //Sub_MttoDistBarra mttoBarra = db.Sub_MttoDistBarra.Find(subcodigo, fecha, barra);
            if (sub_MttoDistPararrayos == null)
            {
                return HttpNotFound();
            }
            ViewBag.codSub = subcodigo;
            ViewBag.fechaMtto = fecha;
            ViewBag.valor = valor;

            var repoMtto = new SubMttoSubDistRepositorio(db);

            ViewBag.estP = repoMtto.estadoPara();
            ViewBag.aterr = repoMtto.aterramientos();

            return View(sub_MttoDistPararrayos);
        }

        // POST: Sub_MttoDistPararrayos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoSub,Fecha,Id_AdminPararrayo,Id_Pararrayo,Estado,Aislamiento,EstAterramiento")] Sub_MttoDistPararrayos sub_MttoDistPararrayos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_MttoDistPararrayos).State = EntityState.Modified;
                db.SaveChanges();
                if (Request.Form["submitButton"].ToString() == "Editar")
                {
                    return RedirectToAction("Edit", "SubMttoSubDistribucions", new { codigo = sub_MttoDistPararrayos.CodigoSub, fecha = sub_MttoDistPararrayos.Fecha });

                }
                return RedirectToAction("ComponentesMtto", "SubMttoSubDistribucions", new { codigo = sub_MttoDistPararrayos.CodigoSub, fecha = sub_MttoDistPararrayos.Fecha });

            }

            ViewBag.codSub = sub_MttoDistPararrayos.CodigoSub;
            ViewBag.fechaMtto = sub_MttoDistPararrayos.Fecha;
            ViewBag.valor = Request.Form["submitButton"].ToString();

            var repoMtto = new SubMttoSubDistRepositorio(db);

            ViewBag.estP = repoMtto.estadoPara();
            ViewBag.aterr = repoMtto.aterramientos();

            return View(sub_MttoDistPararrayos);
        }

        // GET: Sub_MttoDistPararrayos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_MttoDistPararrayos sub_MttoDistPararrayos = db.Sub_MttoDistPararrayos.Find(id);
            if (sub_MttoDistPararrayos == null)
            {
                return HttpNotFound();
            }
            return View(sub_MttoDistPararrayos);
        }

        // POST: Sub_MttoDistPararrayos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sub_MttoDistPararrayos sub_MttoDistPararrayos = db.Sub_MttoDistPararrayos.Find(id);
            db.Sub_MttoDistPararrayos.Remove(sub_MttoDistPararrayos);
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

        public ActionResult EliminaMttoPara(string cod, int EA, int idPara, DateTime? fechaM)
        {
            try
            {
                Repositorio br = new Repositorio(db);

                Sub_MttoDistPararrayos mttoPara = db.Sub_MttoDistPararrayos.Find(cod, fechaM, EA, idPara);
                //int accion = br.GetNumAccion("M", "SIA", mttoDesc.NumAccion ?? 0);
                db.Sub_MttoDistPararrayos.Remove(mttoPara);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }



        public ActionResult _VPPara(string sub)
        {

            var ListaPara = new SubMttoSubDistRepositorio(db);
            var pararrayos = ListaPara.listaPararrayosSub(sub);

            ViewBag.listadoParas = pararrayos;
            ViewBag.cantP = pararrayos.Count();

            return PartialView();
        }
    }
}
