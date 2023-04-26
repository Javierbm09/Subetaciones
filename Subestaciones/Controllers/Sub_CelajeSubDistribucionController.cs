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
    public class Sub_CelajeSubDistribucionController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_CelajeSubDistribucion
        public async Task<ActionResult> Index()
        {
            var ListaInsps = new InspSubDistRepo(db);
            return View(await ListaInsps.ObtenerInsps());
        }

        // GET: Sub_CelajeSubDistribucion/Details/5
        public ActionResult Details(string nombInsp, string codigo, DateTime fechaIns)
        {
            if ((nombInsp == null)||(codigo==null)||(fechaIns==null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_CelajeSubDistribucion sub_CelajeSubDistribucion = db.Sub_CelajeSubDistribucion.Find(nombInsp, codigo, fechaIns);
            if (sub_CelajeSubDistribucion == null)
            {
                return HttpNotFound();
            }

            Repositorio repo = new Repositorio(db);
            var repoInsp = new InspSubDistRepo(db);
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");

            ViewBag.fechaInsp = fechaIns;

            ViewBag.personal = repo.RealizadoPor();
            ViewBag.cant = 0;

            var interruptores = repoInsp.FindInterruptorSub(codigo);
            ViewBag.cantI = interruptores.Count();

            var lista = db.TransformadoresSubtransmision.Where(x => x.Codigo == codigo).ToList();
            ViewBag.cantT = lista.Count();
            ViewBag.valor = "Detail";

            ViewBag.tipo = repoInsp.tipoInsp();
            ViewBag.mallaTS = repoInsp.estados();
            ViewBag.mallaTC = repoInsp.estados();
            ViewBag.estadoPuerta = repoInsp.estados();
            ViewBag.estadoCerca = repoInsp.estados();
            ViewBag.estadoHierba = repoInsp.SN();
            ViewBag.desorden = repoInsp.SN();
            ViewBag.paraA = repoInsp.estadoPararrayoA();
            ViewBag.paraB = repoInsp.estadoPararrayoB();
            ViewBag.estadoCarteles = repoInsp.estadoOtros();
            ViewBag.estadoAlumbrado = repoInsp.estadoOtros();
            ViewBag.estadoCandado = repoInsp.estadoOtros();
            ViewBag.dropAA = repoInsp.estadoDropOut();
            ViewBag.dropAB = repoInsp.estadoDropOut();
            ViewBag.dropAC = repoInsp.estadoDropOut();
            ViewBag.dropBA = repoInsp.estadoDropOut();
            ViewBag.dropBB = repoInsp.estadoDropOut();
            ViewBag.dropBC = repoInsp.estadoDropOut();
            ViewBag.dropBypassA = repoInsp.estadoDropOut();
            ViewBag.dropBypassB = repoInsp.estadoDropOut();
            ViewBag.dropBypassC = repoInsp.estadoDropOut();

            return View(sub_CelajeSubDistribucion);
        }

        // GET: Sub_CelajeSubDistribucion/Create
        public ActionResult Create()
        {
            var repo = new Repositorio(db);
            var repoInsp = new InspSubDistRepo(db);

            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();

            ViewBag.tipo = repoInsp.tipoInsp();
            ViewBag.mallaTS = repoInsp.estados();
            ViewBag.mallaTC = repoInsp.estados();
            ViewBag.estadoPuerta = repoInsp.estados();
            ViewBag.estadoCerca = repoInsp.estados();
            ViewBag.estadoHierba = repoInsp.SN();
            ViewBag.desorden = repoInsp.SN();
            ViewBag.paraA = repoInsp.estadoPararrayoA();
            ViewBag.paraB = repoInsp.estadoPararrayoB();
            ViewBag.estadoCarteles = repoInsp.estadoOtros();
            ViewBag.estadoAlumbrado = repoInsp.estadoOtros();
            ViewBag.estadoCandado = repoInsp.estadoOtros();
            ViewBag.dropAA = repoInsp.estadoDropOut();
            ViewBag.dropAB = repoInsp.estadoDropOut();
            ViewBag.dropAC = repoInsp.estadoDropOut();
            ViewBag.dropBA = repoInsp.estadoDropOut();
            ViewBag.dropBB = repoInsp.estadoDropOut();
            ViewBag.dropBC = repoInsp.estadoDropOut();
            ViewBag.dropBypassA = repoInsp.estadoDropOut();
            ViewBag.dropBypassB = repoInsp.estadoDropOut();
            ViewBag.dropBypassC = repoInsp.estadoDropOut();
            return View();
        }

        // POST: Sub_CelajeSubDistribucion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "nombreCelaje,codigoSub,fecha,realizadoPor,numAccion,id_EAdministrativa,dropOutAFaseA,dropOutBFaseA,dropOutAFaseB,dropOutBFaseB,dropOutAFaseC,dropOutBFaseC,dropOutBypassFaseA,dropOutBypassFaseB,dropOutBypassFaseC,interruptorAltaSalidero,interruptorAltaNAceite,interruptorAltaPintura,interruptorAltaCuentaOP,interruptorBajaSalidero,interruptorBajaNAceite,interruptorBajaPintura,interruptorBajaCuentaOP,pRayosAlta,pRayosBaja,observaciones,mallaTSub,mallaTCerca,hierba,desordenSub,estadoCerca,estadoPuerta,otrasInformaciones,estadoCarteles,estadoAlumbrado,estadoCandadoPuerta,tipoCelaje")] Sub_CelajeSubDistribucion sub_CelajeSubDistribucion)
        {
            Repositorio repo = new Repositorio(db);
            var repoInsp = new InspSubDistRepo(db);

            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();

            if (ModelState.IsValid)
            {
                if (await ValidarExisteIns(sub_CelajeSubDistribucion.nombreCelaje,sub_CelajeSubDistribucion.codigoSub, sub_CelajeSubDistribucion.fecha))
                {
                    sub_CelajeSubDistribucion.id_EAdministrativa = (Int16)Id_Eadministrativa; // (int)Id_Eadministrativa;
                    sub_CelajeSubDistribucion.numAccion = repo.GetNumAccion("I", "SIE", 0);


                    db.Sub_CelajeSubDistribucion.Add(sub_CelajeSubDistribucion);
                    db.SaveChanges();
                    return RedirectToAction("ComponentesInsp", new {nombInsp = sub_CelajeSubDistribucion.nombreCelaje, codigo = sub_CelajeSubDistribucion.codigoSub, fecha = sub_CelajeSubDistribucion.fecha });
                }
                else
                {
                    ModelState.AddModelError("codigoSub", "Ya existe una inspección a la subestación en la fecha.");

                }

               
            }

            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();

            ViewBag.tipo = repoInsp.tipoInsp();
            ViewBag.mallaTS = repoInsp.estados();
            ViewBag.mallaTC = repoInsp.estados();
            ViewBag.estadoPuerta = repoInsp.estados();
            ViewBag.estadoCerca = repoInsp.estados();
            ViewBag.estadoHierba = repoInsp.SN();
            ViewBag.desorden = repoInsp.SN();
            ViewBag.paraA = repoInsp.estadoPararrayoA();
            ViewBag.paraB = repoInsp.estadoPararrayoB();
            ViewBag.estadoCarteles = repoInsp.estadoOtros();
            ViewBag.estadoAlumbrado = repoInsp.estadoOtros();
            ViewBag.estadoCandado = repoInsp.estadoOtros();
            ViewBag.dropAA = repoInsp.estadoDropOut();
            ViewBag.dropAB = repoInsp.estadoDropOut();
            ViewBag.dropAC = repoInsp.estadoDropOut();
            ViewBag.dropBA = repoInsp.estadoDropOut();
            ViewBag.dropBB = repoInsp.estadoDropOut();
            ViewBag.dropBC = repoInsp.estadoDropOut();
            ViewBag.dropBypassA = repoInsp.estadoDropOut();
            ViewBag.dropBypassB = repoInsp.estadoDropOut();
            ViewBag.dropBypassC = repoInsp.estadoDropOut();
            return View(sub_CelajeSubDistribucion);
        }

        private async Task<bool> ValidarExisteIns(string nomb,string Codigo, DateTime fecha)
        {
            var listaInsps = await new InspSubDistRepo(db).ObtenerInsps();

            return !listaInsps.Select(c => new { c.nombreCelaje, c.codigoSub, c.fecha }).Any(c => c.nombreCelaje == nomb && c.codigoSub == Codigo && c.fecha == fecha);
        }

        public ActionResult ComponentesInsp(string nombInsp, string codigo, DateTime fecha)
        {
            Repositorio repo = new Repositorio(db);
            var repoInsp = new InspSubDistRepo(db);
            Sub_CelajeSubDistribucion dinsp = db.Sub_CelajeSubDistribucion.Find(nombInsp, codigo, fecha);

            if (dinsp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
              if (dinsp == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");


            ViewBag.personal = repo.RealizadoPor();
            ViewBag.cant = 0;

            var interruptores = repoInsp.FindInterruptorSub(dinsp.codigoSub);
            ViewBag.cantI = interruptores.Count();

            var lista = db.TransformadoresSubtransmision.Where(x => x.Codigo == dinsp.codigoSub).ToList();
            ViewBag.cantT = lista.Count();


            return View(dinsp);
        }


        // GET: Sub_CelajeSubDistribucion/Edit/5
        public ActionResult Edit(string nombInsp, string codigo, DateTime fechaIns)
        {
            Repositorio repo = new Repositorio(db);
            var repoInsp = new InspSubDistRepo(db);
            Sub_CelajeSubDistribucion dinsp = db.Sub_CelajeSubDistribucion.Find(nombInsp, codigo, fechaIns);

            if (dinsp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (dinsp == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");

            ViewBag.fechaInsp = fechaIns;

            ViewBag.personal = repo.RealizadoPor();
            ViewBag.cant = 0;

            var interruptores = repoInsp.FindInterruptorSub(dinsp.codigoSub);
            ViewBag.cantI = interruptores.Count();

            var lista = db.TransformadoresSubtransmision.Where(x => x.Codigo == dinsp.codigoSub).ToList();
            ViewBag.cantT = lista.Count();

            ViewBag.tipo = repoInsp.tipoInsp();
            ViewBag.mallaTS = repoInsp.estados();
            ViewBag.mallaTC = repoInsp.estados();
            ViewBag.estadoPuerta = repoInsp.estados();
            ViewBag.estadoCerca = repoInsp.estados();
            ViewBag.estadoHierba = repoInsp.SN();
            ViewBag.desorden = repoInsp.SN();
            ViewBag.paraA = repoInsp.estadoPararrayoA();
            ViewBag.paraB = repoInsp.estadoPararrayoB();
            ViewBag.estadoCarteles = repoInsp.estadoOtros();
            ViewBag.estadoAlumbrado = repoInsp.estadoOtros();
            ViewBag.estadoCandado = repoInsp.estadoOtros();
            ViewBag.dropAA = repoInsp.estadoDropOut();
            ViewBag.dropAB = repoInsp.estadoDropOut();
            ViewBag.dropAC = repoInsp.estadoDropOut();
            ViewBag.dropBA = repoInsp.estadoDropOut();
            ViewBag.dropBB = repoInsp.estadoDropOut();
            ViewBag.dropBC = repoInsp.estadoDropOut();
            ViewBag.dropBypassA = repoInsp.estadoDropOut();
            ViewBag.dropBypassB = repoInsp.estadoDropOut();
            ViewBag.dropBypassC = repoInsp.estadoDropOut();

            return View(dinsp);
        }

        // POST: Sub_CelajeSubDistribucion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nombreCelaje,codigoSub,fecha,realizadoPor,numAccion,id_EAdministrativa,dropOutAFaseA,dropOutBFaseA,dropOutAFaseB,dropOutBFaseB,dropOutAFaseC,dropOutBFaseC,dropOutBypassFaseA,dropOutBypassFaseB,dropOutBypassFaseC,interruptorAltaSalidero,interruptorAltaNAceite,interruptorAltaPintura,interruptorAltaCuentaOP,interruptorBajaSalidero,interruptorBajaNAceite,interruptorBajaPintura,interruptorBajaCuentaOP,pRayosAlta,pRayosBaja,observaciones,mallaTSub,mallaTCerca,hierba,desordenSub,estadoCerca,estadoPuerta,otrasInformaciones,estadoCarteles,estadoAlumbrado,estadoCandadoPuerta,tipoCelaje")] Sub_CelajeSubDistribucion sub_CelajeSubDistribucion)
        {
            Repositorio repo = new Repositorio(db);
            var repoInsp = new InspSubDistRepo(db);

            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();

            if (ModelState.IsValid)
            {
                sub_CelajeSubDistribucion.id_EAdministrativa = (Int16)Id_Eadministrativa; //(int)Id_Eadministrativa;

                sub_CelajeSubDistribucion.numAccion = repo.GetNumAccion("M", "SIE", 0);

                    db.Entry(sub_CelajeSubDistribucion).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }

            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();


            var interruptores = repoInsp.FindInterruptorSub(sub_CelajeSubDistribucion.codigoSub);
            ViewBag.cantI = interruptores.Count();

            var lista = db.TransformadoresSubtransmision.Where(x => x.Codigo == sub_CelajeSubDistribucion.codigoSub).ToList();
            ViewBag.cantT = lista.Count();

            ViewBag.tipo = repoInsp.tipoInsp();
            ViewBag.mallaTS = repoInsp.estados();
            ViewBag.mallaTC = repoInsp.estados();
            ViewBag.estadoPuerta = repoInsp.estados();
            ViewBag.estadoCerca = repoInsp.estados();
            ViewBag.estadoHierba = repoInsp.SN();
            ViewBag.desorden = repoInsp.SN();
            ViewBag.paraA = repoInsp.estadoPararrayoA();
            ViewBag.paraB = repoInsp.estadoPararrayoB();
            ViewBag.estadoCarteles = repoInsp.estadoOtros();
            ViewBag.estadoAlumbrado = repoInsp.estadoOtros();
            ViewBag.estadoCandado = repoInsp.estadoOtros();
            ViewBag.dropAA = repoInsp.estadoDropOut();
            ViewBag.dropAB = repoInsp.estadoDropOut();
            ViewBag.dropAC = repoInsp.estadoDropOut();
            ViewBag.dropBA = repoInsp.estadoDropOut();
            ViewBag.dropBB = repoInsp.estadoDropOut();
            ViewBag.dropBC = repoInsp.estadoDropOut();
            ViewBag.dropBypassA = repoInsp.estadoDropOut();
            ViewBag.dropBypassB = repoInsp.estadoDropOut();
            ViewBag.dropBypassC = repoInsp.estadoDropOut();

            return View(sub_CelajeSubDistribucion);
        }

        // GET: Sub_CelajeSubDistribucion/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_CelajeSubDistribucion sub_CelajeSubDistribucion = db.Sub_CelajeSubDistribucion.Find(id);
            if (sub_CelajeSubDistribucion == null)
            {
                return HttpNotFound();
            }
            return View(sub_CelajeSubDistribucion);
        }

        [TienePermiso(34)]// verifico que tenga permiso de crear y eliminar mtto
        public ActionResult Eliminar(string nombI, string codigo, DateTime fecha)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_CelajeSubDistribucion t = db.Sub_CelajeSubDistribucion.Find(nombI, codigo, fecha);
                db.Sub_CelajeSubDistribucion.Remove(t);
                int accion = br.GetNumAccion("B", "SIE", t.numAccion);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        // POST: Sub_CelajeSubDistribucion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sub_CelajeSubDistribucion sub_CelajeSubDistribucion = db.Sub_CelajeSubDistribucion.Find(id);
            db.Sub_CelajeSubDistribucion.Remove(sub_CelajeSubDistribucion);
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

        public ActionResult EliminaInspTransf(string nombInps, string cod, int? idTransf, DateTime? fechaM, int? EA)
        {
            try
            {
                Repositorio br = new Repositorio(db);

                Sub_TransformadoresSubtrCelaje inspTransf = db.Sub_TransformadoresSubtrCelaje.Find(nombInps, cod, fechaM, EA, idTransf);
                //int accion = br.GetNumAccion("M", "SIA", mttoDesc.NumAccion ?? 0);
                db.Sub_TransformadoresSubtrCelaje.Remove(inspTransf);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult EliminaInspInt(string nombInps, string cod, string codInt, DateTime? fechaM)
        {
            try
            {
                Repositorio br = new Repositorio(db);

                Sub_CelajeSubDistInterruptor inspInt = db.Sub_CelajeSubDistInterruptor.Find(codInt, nombInps, cod, fechaM);
                //int accion = br.GetNumAccion("M", "SIA", mttoDesc.NumAccion ?? 0);
                db.Sub_CelajeSubDistInterruptor.Remove(inspInt);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }


        #region Vista Parciales
        [HttpPost]
        public async Task<ActionResult> ListadoInsp()
        {
            var ListaMtto = new InspSubDistRepo(db);
            return PartialView("_VPInspSubDist", await ListaMtto.ObtenerInsps());
        }

        public ActionResult _VPInspTransf(string nombre,string sub, DateTime fechaM, string valor)
        {
            //lista de mttos de tansf dentro de la inspeccion  _VPMttosTransf
            var ListaInt = new InspSubDistRepo(db);

            var lista = ListaInt.listaInspTransf(nombre, sub, fechaM).ToList();

            ViewBag.listaInspTransf = lista;
            ViewBag.cantInspT = lista.Count();
            ViewBag.valor = valor;

            return PartialView();
            //return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public ActionResult _VPInspInterrup(string nombre, string sub, DateTime fechaM, string valor)
        {
            //lista de insp de interruptores dentro de la inspeccion
            var ListaInt = new InspSubDistRepo(db);

            var listaI = ListaInt.listaInspInterr(nombre, sub, fechaM).ToList();

            ViewBag.listaInspInt = listaI;
            ViewBag.cantInspInt= listaI.Count();
            ViewBag.valor = valor;

            return PartialView();
            //return Json(listaD, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}
