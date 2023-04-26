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
    public class SubMttoSubDistribucionsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: SubMttoSubDistribucions

        public async Task<ActionResult> Index()
        {
            var ListaMttos = new SubMttoSubDistRepositorio(db);
            return View(await ListaMttos.ObtenerMttos());

        }

        // GET: SubMttoSubDistribucions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMttoSubDistribucion subMttoSubDistribucion = db.SubMttoSubDistribucion.Find(id);
            if (subMttoSubDistribucion == null)
            {
                return HttpNotFound();
            }
            return View(subMttoSubDistribucion);
        }

        // GET: SubMttoSubDistribucions/Create
        public ActionResult Create()
        {
            var repo = new Repositorio(db);
            var repoMtto = new SubMttoSubDistRepositorio(db);
            ViewBag.PtosCount = 0;
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();
            //listados cerca
            ViewBag.tipoCerca = repoMtto.tipoCerca();
            ViewBag.estadoCerca = repoMtto.estados();
            ViewBag.coronacionCerca = repoMtto.coronacionYpintura();
            ViewBag.pinturaCerca = repoMtto.coronacionYpintura();
            ViewBag.aterramientosCerca = repoMtto.aterramientos();
            ViewBag.cartelesCerca = repoMtto.estadoTanque();
            //listados puerta
            ViewBag.tipoPuerta = repoMtto.tipoPuerta();
            ViewBag.estadoPuerta = repoMtto.estados();
            ViewBag.coronacionPuerta = repoMtto.coronacionYpintura();
            ViewBag.pinturaPuerta = repoMtto.coronacionYpintura();
            ViewBag.aterramientosPuerta = repoMtto.aterramientos();
            ViewBag.cartelesPuerta = repoMtto.estadoTanque();
            ViewBag.candadoPuerta = repoMtto.estadoTanque();
            //listados alumbrado
            ViewBag.tipoAlumbrado = repoMtto.tipoAlumbrado();
            ViewBag.estadoAlumbrado = repoMtto.estados();
            ViewBag.controlAlumbrado = repoMtto.controlAlumb();
            ViewBag.estadoFotocelda = repoMtto.estados();
            //listados area interior
            ViewBag.tipoPiso = repoMtto.tipoPiso();
            ViewBag.estadoPiso = repoMtto.estadoPiso();
            ViewBag.orden = repoMtto.ordenInterior();
            //listados area exterior
            ViewBag.estadoAreaExt = repoMtto.estadoAreaExt();
            ViewBag.franja = repoMtto.salideros();

            return View();
        }

        // POST: SubMttoSubDistribucions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubMttoSubDistribucion dmtto)
        {
            Repositorio repo = new Repositorio(db);
            var repoMtto = new SubMttoSubDistRepositorio(db);

            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();

            if (ModelState.IsValid)
            {
                if (await ValidarExisteMtto(dmtto.CodigoSub, dmtto.Fecha))
                {
                    dmtto.Id_EAdministrativa = (Int16)Id_Eadministrativa; //(int)Id_Eadministrativa;
                    dmtto.NumAccion = repo.GetNumAccion("I", "SIA", 0);


                    db.SubMttoSubDistribucion.Add(dmtto);
                    db.SaveChanges();
                    return RedirectToAction("ComponentesMtto", new { codigo = dmtto.CodigoSub, fecha = dmtto.Fecha });
                }
                else
                {
                    ModelState.AddModelError("mtto.CodigoSub", "Ya existe un mantenimiento a la subestación en la fecha.");

                }
            }
            ViewBag.PtosCount = 0;
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();

            //listados cerca
            ViewBag.tipoCerca = repoMtto.tipoCerca();
            ViewBag.estadoCerca = repoMtto.estados();
            ViewBag.coronacionCerca = repoMtto.coronacionYpintura();
            ViewBag.pinturaCerca = repoMtto.coronacionYpintura();
            ViewBag.aterramientosCerca = repoMtto.aterramientos();
            ViewBag.cartelesCerca = repoMtto.estadoTanque();
            //listados puerta
            ViewBag.tipoPuerta = repoMtto.tipoPuerta();
            ViewBag.estadoPuerta = repoMtto.estados();
            ViewBag.coronacionPuerta = repoMtto.coronacionYpintura();
            ViewBag.pinturaPuerta = repoMtto.coronacionYpintura();
            ViewBag.aterramientosPuerta = repoMtto.aterramientos();
            ViewBag.cartelesPuerta = repoMtto.estadoTanque();
            ViewBag.candadoPuerta = repoMtto.estadoTanque();
            //listados alumbrado
            ViewBag.tipoAlumbrado = repoMtto.tipoAlumbrado();
            ViewBag.estadoAlumbrado = repoMtto.estados();
            ViewBag.controlAlumbrado = repoMtto.controlAlumb();
            ViewBag.estadoFotocelda = repoMtto.estados();
            //listados area interior
            ViewBag.tipoPiso = repoMtto.tipoPiso();
            ViewBag.estadoPiso = repoMtto.estadoPiso();
            ViewBag.orden = repoMtto.ordenInterior();
            //listados area exterior
            ViewBag.estadoAreaExt = repoMtto.estadoAreaExt();
            ViewBag.franja = repoMtto.salideros();

            return View(dmtto);
        }

        private async Task<bool> ValidarExisteMtto(string Codigo, DateTime fecha)
        {
            var listaMttos = await new SubMttoSubDistRepositorio(db).ObtenerMttos();

            return !listaMttos.Select(c => new { c.CodigoSub, c.Fecha }).Any(c => c.CodigoSub == Codigo && c.Fecha == fecha);
        }

        public ActionResult ComponentesMtto(string codigo, DateTime fecha)
        {
            Repositorio repo = new Repositorio(db);
            var repoMtto = new SubMttoSubDistRepositorio(db);
            SubMttoSubDistribucion dmtto = db.SubMttoSubDistribucion.Find(codigo, fecha);

            if (dmtto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //MttoSubDistViewModel mttos = new MttoSubDistViewModel();
            //mttos.mtto = db.SubMttoSubDistribucion.Find(dmtto.CodigoSub, dmtto.Fecha);
            //SubMttoSubDistribucion subMttoSubDistribucion = db.SubMttoSubDistribucion.Find(dmtto.CodigoSub, dmtto.Fecha);
            if (dmtto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");


            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();
            ViewBag.cant = 0;

            var desconectivos = repoMtto.FindDescEnSub(dmtto.CodigoSub);
            ViewBag.cantD = desconectivos.Count();

            var lista = db.TransformadoresSubtransmision.Where(x => x.Codigo == dmtto.CodigoSub).ToList();
            ViewBag.cantT = lista.Count();

            var listaPara = db.Sub_Pararrayos.Where(x => x.Codigo == dmtto.CodigoSub).ToList();
            ViewBag.cantP = lista.Count();

            var barras = repoMtto.FindBarraEnSub(dmtto.CodigoSub);
            ViewBag.cantB = barras.Count();

            return View(dmtto);
        }

        // GET: SubMttoSubDistribucions/Edit/5
        public ActionResult Edit(string codigo, DateTime fecha)
        {
            if ((codigo == null) && (fecha == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMttoSubDistribucion subMttoSubDistribucion = db.SubMttoSubDistribucion.Find(codigo, fecha);
            if (subMttoSubDistribucion == null)
            {
                return HttpNotFound();
            }
            Repositorio repo = new Repositorio(db);
            var repoMtto = new SubMttoSubDistRepositorio(db);
            ViewBag.fechaMtto = fecha;

            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();

            var desconectivos = repoMtto.FindDescEnSub(subMttoSubDistribucion.CodigoSub);
            ViewBag.cantD = desconectivos.Count();

            var lista = db.TransformadoresSubtransmision.Where(x => x.Codigo == subMttoSubDistribucion.CodigoSub).ToList();
            ViewBag.cantT = lista.Count();

            var listaPara = db.Sub_Pararrayos.Where(x => x.Codigo == subMttoSubDistribucion.CodigoSub).ToList();
            ViewBag.cantP = lista.Count();

            var barras = repoMtto.FindBarraEnSub(subMttoSubDistribucion.CodigoSub);
            ViewBag.cantB = barras.Count();

            //listados cerca
            ViewBag.tipoCerca = repoMtto.tipoCerca();
            ViewBag.estadoCerca = repoMtto.estados();
            ViewBag.coronacionCerca = repoMtto.coronacionYpintura();
            ViewBag.pinturaCerca = repoMtto.coronacionYpintura();
            ViewBag.aterramientosCerca = repoMtto.aterramientos();
            ViewBag.cartelesCerca = repoMtto.estadoTanque();
            //listados puerta
            ViewBag.tipoPuerta = repoMtto.tipoPuerta();
            ViewBag.estadoPuerta = repoMtto.estados();
            ViewBag.coronacionPuerta = repoMtto.coronacionYpintura();
            ViewBag.pinturaPuerta = repoMtto.coronacionYpintura();
            ViewBag.aterramientosPuerta = repoMtto.aterramientos();
            ViewBag.cartelesPuerta = repoMtto.estadoTanque();
            ViewBag.candadoPuerta = repoMtto.estadoTanque();
            //listados alumbrado
            ViewBag.tipoAlumbrado = repoMtto.tipoAlumbrado();
            ViewBag.estadoAlumbrado = repoMtto.estados();
            ViewBag.controlAlumbrado = repoMtto.controlAlumb();
            ViewBag.estadoFotocelda = repoMtto.estados();
            //listados area interior
            ViewBag.tipoPiso = repoMtto.tipoPiso();
            ViewBag.estadoPiso = repoMtto.estadoPiso();
            ViewBag.orden = repoMtto.ordenInterior();
            //listados area exterior
            ViewBag.estadoAreaExt = repoMtto.estadoAreaExt();
            ViewBag.franja = repoMtto.salideros();

            return View(subMttoSubDistribucion);
        }

        // POST: SubMttoSubDistribucions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoSub,Fecha,TipoMantenimiento,Id_EAdministrativa,NumAccion,TipoCerca,EstadoCerca,CoronacionCerca,PinturaCerca,AterraminetoCerca,TipoPuerta,EstadoPuerta,CoronacionPuerta,PinturaPuerta,AterraminetoPuerta,TipoAlumbrado,EstadoAlumbrado,ControlAlumbrado,EstadoFotoCelda,CantidadLamparas,TipoPiso,EstadoPiso,EstOrdenPiso,EstAreaExterior,FranjaContraIncendio,Observaciones,RealizadoPor,CartelesPuerta,CartelesCerca,CandadoPuerta,CantLamparasServicio,EstParaFranklin,Mantenido,f")] SubMttoSubDistribucion subMttoSubDistribucion)
        {
            Repositorio repo = new Repositorio(db);
            var Id_EAdministrativa = repo.GetId_EAdministrativaUsuario();

            if (ModelState.IsValid)
            {
                subMttoSubDistribucion.Fecha = formatoFecha(subMttoSubDistribucion.f);
                subMttoSubDistribucion.Id_EAdministrativa = (Int16)Id_EAdministrativa; //(int)Id_EAdministrativa;
                subMttoSubDistribucion.NumAccion = repo.GetNumAccion("I", "SIA", 0);
                db.Entry(subMttoSubDistribucion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var repoMtto = new SubMttoSubDistRepositorio(db);

            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();

            var desconectivos = repoMtto.FindDescEnSub(subMttoSubDistribucion.CodigoSub);
            ViewBag.cantD = desconectivos.Count();

            var lista = db.TransformadoresSubtransmision.Where(x => x.Codigo == subMttoSubDistribucion.CodigoSub).ToList();
            ViewBag.cantT = lista.Count();

            var listaPara = db.Sub_Pararrayos.Where(x => x.Codigo == subMttoSubDistribucion.CodigoSub).ToList();
            ViewBag.cantP = lista.Count();

            var barras = repoMtto.FindBarraEnSub(subMttoSubDistribucion.CodigoSub);
            ViewBag.cantB = barras.Count();

            //listados cerca
            ViewBag.tipoCerca = repoMtto.tipoCerca();
            ViewBag.estadoCerca = repoMtto.estados();
            ViewBag.coronacionCerca = repoMtto.coronacionYpintura();
            ViewBag.pinturaCerca = repoMtto.coronacionYpintura();
            ViewBag.aterramientosCerca = repoMtto.aterramientos();
            ViewBag.cartelesCerca = repoMtto.estadoTanque();
            //listados puerta
            ViewBag.tipoPuerta = repoMtto.tipoPuerta();
            ViewBag.estadoPuerta = repoMtto.estados();
            ViewBag.coronacionPuerta = repoMtto.coronacionYpintura();
            ViewBag.pinturaPuerta = repoMtto.coronacionYpintura();
            ViewBag.aterramientosPuerta = repoMtto.aterramientos();
            ViewBag.cartelesPuerta = repoMtto.estadoTanque();
            ViewBag.candadoPuerta = repoMtto.estadoTanque();
            //listados alumbrado
            ViewBag.tipoAlumbrado = repoMtto.tipoAlumbrado();
            ViewBag.estadoAlumbrado = repoMtto.estados();
            ViewBag.controlAlumbrado = repoMtto.controlAlumb();
            ViewBag.estadoFotocelda = repoMtto.estados();
            //listados area interior
            ViewBag.tipoPiso = repoMtto.tipoPiso();
            ViewBag.estadoPiso = repoMtto.estadoPiso();
            ViewBag.orden = repoMtto.ordenInterior();
            //listados area exterior
            ViewBag.estadoAreaExt = repoMtto.estadoAreaExt();
            ViewBag.franja = repoMtto.salideros();
            return View(subMttoSubDistribucion);
        }

        private DateTime formatoFecha(string fecha)
        {
            var fechaArray = fecha.Split('-');
            DateTime newFecha = new DateTime(Convert.ToInt32(fechaArray[0]), Convert.ToInt32(fechaArray[1]), Convert.ToInt32(fechaArray[2]));
            return newFecha;
        }

        // GET: SubMttoSubDistribucions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMttoSubDistribucion subMttoSubDistribucion = db.SubMttoSubDistribucion.Find(id);
            if (subMttoSubDistribucion == null)
            {
                return HttpNotFound();
            }
            return View(subMttoSubDistribucion);
        }

        // POST: SubMttoSubDistribucions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SubMttoSubDistribucion subMttoSubDistribucion = db.SubMttoSubDistribucion.Find(id);
            db.SubMttoSubDistribucion.Remove(subMttoSubDistribucion);
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

        [TienePermiso(36)]// verifico que tenga permiso de crear y eliminar mtto
        public ActionResult Eliminar(string codigo, DateTime fecha)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                SubMttoSubDistribucion t = db.SubMttoSubDistribucion.Find(codigo, fecha);
                db.SubMttoSubDistribucion.Remove(t);
                int accion = br.GetNumAccion("B", "SIA", t.NumAccion);
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
        public async Task<ActionResult> ListadoMttos()
        {
            var ListaMtto = new SubMttoSubDistRepositorio(db);
            return PartialView("_VPMttoSubDist", await ListaMtto.ObtenerMttos());
        }



        public ActionResult _VPMttosTransf(string sub, string fechaM, string valor)
        {
            //lista de mttos de tansf dentro el mtto_VPMttosTransf
            var ListaMtto = new SubMttoSubDistRepositorio(db);
            ViewBag.valor = valor;

            var lista = ListaMtto.listaMttosTransf(sub, fechaM).ToList();

            ViewBag.listaMttoTransf = lista;
            ViewBag.cantMttoT = lista.Count();

            return PartialView();
            //return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public ActionResult _VPMttosDesc(string sub, string fechaM, string valor)
        {
            //lista de mttos de tansf dentro el mtto
            var ListaMtto = new SubMttoSubDistRepositorio(db);

            var listaD = ListaMtto.listaMttosDesc(sub, fechaM).ToList();
            ViewBag.valor = valor;

            ViewBag.listaMttoDesc = listaD;
            ViewBag.cantMttoDesc = listaD.Count();

            return PartialView();
            //return Json(listaD, JsonRequestBehavior.AllowGet);

        }


        public ActionResult _VPMttosBarras(string sub, string fechaM, string valor)
        {
            //lista de mttos de tansf dentro el mtto
            var ListaMtto = new SubMttoSubDistRepositorio(db);

            var listaB = ListaMtto.listaMttosBarras(sub, fechaM).ToList();

            ViewBag.listaMttoBarras = listaB;
            ViewBag.cantMttoBarras = listaB.Count();
            ViewBag.valor = valor;

            return PartialView();
            //return Json(listaD, JsonRequestBehavior.AllowGet);

        }


        public ActionResult _VPMttosPara(string sub, string fechaM, string valor)
        {
            //lista de mttos de tansf dentro el mtto
            var ListaMtto = new SubMttoSubDistRepositorio(db);

            var listaP = ListaMtto.listaMttosPararrayos(sub, fechaM).ToList();
            ViewBag.valor = valor;

            ViewBag.listaMttoPara = listaP;
            ViewBag.cantMttoPara = listaP.Count();

            return PartialView();
            //return Json(listaD, JsonRequestBehavior.AllowGet);

        }

        #endregion

        public ActionResult listaTransf(string sub, DateTime fechaM, int ea)
        {
            var lista = db.TransformadoresSubtransmision.Where(x => x.Codigo == sub).ToList();

            ViewBag.listaTransf = lista;
            ViewBag.cant = lista.Count();
            if (lista.Count != 0)
            {
                return RedirectToAction("Create", "Sub_MttoDistTransf", new { sub = sub, fecha = fechaM, ea = ea });
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

    }
}
