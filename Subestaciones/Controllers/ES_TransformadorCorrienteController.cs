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
    public class ES_TransformadorCorrienteController : Controller
    {
        private DBContext db = new DBContext();

        // GET: ES_TransformadorCorriente
        public async Task<ActionResult> Index(string inserta)
        {
            var ListaTCs = new TCRepositorio(db);
            ViewBag.Inserto = inserta;

            return View(await ListaTCs.ObtenerListadoTC());
        }

        // GET: ES_TransformadorCorriente/Details/5
        public async Task<ActionResult> Details(string Nro_Serie)
        {
            if (Nro_Serie == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaTC = new TCRepositorio(db);
            var TC = await ListaTC.FindAsync(Nro_Serie);
            if (Nro_Serie == null)
            {
                return HttpNotFound();
            }
            return View(TC);
        }

        // GET: ES_TransformadorCorriente/Create
        [TienePermiso(Servicio: 45)]
        public ActionResult Create()
        {
            var repo = new Repositorio(db);
            var repoTC = new TCRepositorio(db);
            ViewBag.CodigoSub = repo.listaSubestaciones();
            ViewBag.TequipoProt = new SelectList(repoTC.TipoEquipo(), "Value", "Text");
            ViewBag.Fase = repoTC.Fase(""); //new SelectList(repoTC.Fase(), "Value", "Text");
            ViewBag.Frecuencia = repoTC.Frecuencia(0);
            ViewBag.devanados = repoTC.CantDevanados(3);
            ViewBag.InTrabajoPrim = repoTC.InTrabajoPrimaria();
            ViewBag.InPrim = repoTC.InPrimaria();
            ViewBag.InSec = repoTC.InSecundaria();
            ViewBag.Id_Voltaje = new SelectList(repoTC.VoltajeInstalado(), "Id_VoltajeSistema", "Voltaje");
            return View();
        }

        // POST: ES_TransformadorCorriente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create([Bind(Include = "Nro_Serie,Fase,Relacion_Transformacion,Cant_Devanado,Frecuencia,Fs_Fi,id_Voltaje_Nominal,Ubicado,id_Plantilla,CodSub,Tipo_Equipo_Primario,Elemento_Electrico,Id_EAdministrativa,NumAccion,InTrabajoPrim,InSecundaria,Tipo,Inventario,AnnoFab,FechaInstalado,Peso,InPrimaria,Fabricante")] ES_TransformadorCorriente eS_TransformadorCorriente)
        {
            var repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario(); //esta EA ya esta bien
            if (ModelState.IsValid)
            {
                if (ValidarTCFase(eS_TransformadorCorriente.CodSub, eS_TransformadorCorriente.Fase, eS_TransformadorCorriente.Elemento_Electrico, eS_TransformadorCorriente.id_Voltaje_Nominal))
                {
                    if (await ValidarNoSerie(eS_TransformadorCorriente.NoSerieAnt, eS_TransformadorCorriente.Nro_Serie))
                    {
                        eS_TransformadorCorriente.Id_EAdministrativa = (int)Id_Eadministrativa;
                        eS_TransformadorCorriente.NumAccion = repo.GetNumAccion("I", "STC", 0);
                        if (eS_TransformadorCorriente.Cant_Devanado == null) { eS_TransformadorCorriente.Cant_Devanado = 0; }
                        db.ES_TransformadorCorriente.Add(eS_TransformadorCorriente);
                        db.SaveChanges();

                        if (eS_TransformadorCorriente.Cant_Devanado > 0)
                        {
                            for (short i = 1; i <= eS_TransformadorCorriente.Cant_Devanado; i++)
                            {
                                ES_TC_Devanado devanados = new ES_TC_Devanado
                                {
                                    Nro_TC = eS_TransformadorCorriente.Nro_Serie,
                                    Nro_Dev = i,
                                };
                                db.ES_TC_Devanado.Add(devanados);
                            }
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index", new { inserta = "Si" });

                    }
                    else
                    {
                        ModelState.AddModelError("Nro_Serie", "Ya existe un TC con ese No Serie.");
                    }
                }
                else { ModelState.AddModelError("Nro_Serie", "Ya existe un TC asociado a la Fase del equipo protegido seleccionado."); }
            }
            ModelState.AddModelError("Nro_Serie", "Ya existe un TC en la fase del equipo seleccionado.");
            var repoTC = new TCRepositorio(db);
            ViewBag.CodigoSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TequipoProt = new SelectList(repoTC.TipoEquipo(), "Value", "Text");
            ViewBag.Fase = repoTC.Fase("");
            ViewBag.Frecuencia = repoTC.Frecuencia(0);
            ViewBag.devanados = repoTC.CantDevanados(3);
            ViewBag.Id_Voltaje = new SelectList(repoTC.VoltajeInstalado(), "Id_VoltajeSistema", "Voltaje");
            ViewBag.Cant = 0;
            ViewBag.InPrim = repoTC.InPrimaria();
            ViewBag.InTrabajoPrim = repoTC.InTrabajoPrimaria();
            ViewBag.InSec = repoTC.InSecundaria();
            return View(eS_TransformadorCorriente);
        }

        private async Task<bool> ValidarNoSerie(string NoSerieAnt, string Nro_Serie)
        {
            var listaTC = await new TCRepositorio(db).ObtenerListadoTC();

            return !listaTC.Select(c => new { c.Nro_Serie }).Where(c => c.Nro_Serie == Nro_Serie).Any(c => c.Nro_Serie != NoSerieAnt);
        }

        private bool ValidarTCFase(string CodSub, string Fase, string Tipo_Equipo_Primario, short? volt)
        {
            var listado = db.ES_TransformadorCorriente.Where(x => x.CodSub == CodSub && x.Fase == Fase && x.Elemento_Electrico == Tipo_Equipo_Primario && x.id_Voltaje_Nominal == volt).ToList();

            if (listado.Count() == 0) return true; else return false;

        }

        private bool ValidarTCFase(string CodSub, string Fase, string Tipo_Equipo_Primario, short? volt, string FaseAnt)
        {
            if (Fase != FaseAnt)
            {
                var listado = db.ES_TransformadorCorriente.Where
                    (
                    x => x.CodSub == CodSub && x.Fase == Fase && x.Elemento_Electrico == Tipo_Equipo_Primario && x.id_Voltaje_Nominal == volt
                    )
                    .ToList();

                if (listado.Count() == 0) return true; else return false;
            }
            else return true;
        }

        [HttpPost]
        public async Task<ActionResult> ValidarSerie(string NoSerieAnt, string Nro_Serie)
        {
            try
            {
                return Json(await ValidarNoSerie(NoSerieAnt, Nro_Serie));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        // GET: ES_TransformadorCorriente/Edit/5
        [TienePermiso(Servicio: 46)]//Servicio: editar TC
        public ActionResult Edit(string Nro_Serie)
        {
            if (Nro_Serie == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ES_TransformadorCorriente TC = db.ES_TransformadorCorriente.Find(Nro_Serie);

            var repo = new Repositorio(db);
            var repoTC = new TCRepositorio(db);
            ViewBag.CodigoSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TequipoProt = new SelectList(repoTC.TipoEquipo(), "Value", "Text");
            ViewBag.Fases = repoTC.Fase(TC.Fase);
            ViewBag.devanados = repoTC.CantDevanados(TC.Cant_Devanado);
            ViewBag.Frecuencia = repoTC.Frecuencia(TC.Frecuencia);
            ViewBag.InPrim = repoTC.InPrimaria();
            ViewBag.InTrabajoPrim = repoTC.InTrabajoPrimaria();
            ViewBag.InSec = repoTC.InSecundaria();
            ViewBag.Id_Voltaje = new SelectList(repoTC.VoltajeInstalado(), "Id_VoltajeSistema", "Voltaje");
            CodigoEquipos(TC.Tipo_Equipo_Primario,
               TC.CodSub, TC.Elemento_Electrico);
            return View(TC);
        }

        // POST: ES_TransformadorCorriente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Nro_Serie,Fase,Relacion_Transformacion,Cant_Devanado,Frecuencia,Fs_Fi," +
            "id_Voltaje_Nominal,Ubicado,id_Plantilla,CodSub,Tipo_Equipo_Primario,Elemento_Electrico,Id_EAdministrativa,NumAccion," +
            "InTrabajoPrim,InSecundaria,Tipo,Inventario,AnnoFab,FechaInstalado,Peso,InPrimaria," +
            "Fabricante, NoSerieAnt, FaseAnt")] ES_TransformadorCorriente eS_TransformadorCorriente)
        {
            var repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario(); //esta EA ya esta bien

            if (ModelState.IsValid)
            {
                if (ValidarTCFase(eS_TransformadorCorriente.CodSub, eS_TransformadorCorriente.Fase, eS_TransformadorCorriente.Elemento_Electrico, eS_TransformadorCorriente.id_Voltaje_Nominal, eS_TransformadorCorriente.FaseAnt))
                {
                    if (eS_TransformadorCorriente.NoSerieAnt != eS_TransformadorCorriente.Nro_Serie)
                    {
                        if (await ValidarNoSerie(eS_TransformadorCorriente.NoSerieAnt, eS_TransformadorCorriente.Nro_Serie))
                        {
                            await db.Database.ExecuteSqlCommandAsync("UPDATE ES_TransformadorCorriente SET Nro_Serie = @Nro_Serie WHERE Nro_Serie = @NoSerieAnt",
                                new SqlParameter("@Nro_Serie", eS_TransformadorCorriente.Nro_Serie),
                                new SqlParameter("@NoSerieAnt", eS_TransformadorCorriente.NoSerieAnt));

                            eS_TransformadorCorriente.Id_EAdministrativa = Id_Eadministrativa;
                            eS_TransformadorCorriente.NumAccion = repo.GetNumAccion("M", "STC", eS_TransformadorCorriente.NumAccion ?? 0);
                            db.Entry(eS_TransformadorCorriente).State = EntityState.Modified;
                            db.SaveChanges();

                            return RedirectToAction("Index");
                        }
                        else
                            ModelState.AddModelError("Nro_Serie", "Ya existe un TC con ese número de serie.");
                    }
                    else if (eS_TransformadorCorriente.NoSerieAnt == eS_TransformadorCorriente.Nro_Serie)
                    {
                        eS_TransformadorCorriente.Id_EAdministrativa = Id_Eadministrativa;
                        eS_TransformadorCorriente.NumAccion = repo.GetNumAccion("M", "STC", eS_TransformadorCorriente.NumAccion ?? 0);
                        db.Entry(eS_TransformadorCorriente).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    //db.Entry(eS_TransformadorCorriente).State = EntityState.Modified;
                    //db.SaveChanges();
                    //return RedirectToAction("Index");
                }
                else { ModelState.AddModelError("Nro_Serie", "Ya existe un TC asociado a la Fase del equipo protegido seleccionado."); }
            }
            ModelState.AddModelError("Nro_Serie", "Ya existe un TC asociado a la Fase del equipo protegido seleccionado.");
            var repoTC = new TCRepositorio(db);
            ViewBag.CodigoSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TequipoProt = new SelectList(repoTC.TipoEquipo(), "Value", "Text");
            ViewBag.Fase = repoTC.Fase(eS_TransformadorCorriente.Fase);
            ViewBag.Frecuencia = repoTC.Frecuencia(eS_TransformadorCorriente.Frecuencia);
            ViewBag.Id_Voltaje = new SelectList(repoTC.VoltajeInstalado(), "Id_VoltajeSistema", "Voltaje");
            ViewBag.devanados = repoTC.CantDevanados(eS_TransformadorCorriente.Cant_Devanado);
            ViewBag.InPrim = repoTC.InPrimaria();
            ViewBag.InTrabajoPrim = repoTC.InTrabajoPrimaria();
            ViewBag.InSec = repoTC.InSecundaria();
            CodigoEquipos(eS_TransformadorCorriente.Tipo_Equipo_Primario, eS_TransformadorCorriente.CodSub, eS_TransformadorCorriente.Elemento_Electrico);
            return View(eS_TransformadorCorriente);
        }

        // GET: ES_TransformadorCorriente/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ES_TransformadorCorriente eS_TransformadorCorriente = db.ES_TransformadorCorriente.Find(id);
            if (eS_TransformadorCorriente == null)
            {
                return HttpNotFound();
            }
            return View(eS_TransformadorCorriente);
        }

        [HttpPost]
        [TienePermiso(Servicio: 45)]
        public JsonResult Eliminar(String NoSerie)
        {
            if ((ValidarSiAsociadoAEsquemaM(NoSerie)) || (ValidarSiAsociadoAEsquemaP(NoSerie)))
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    Repositorio br = new Repositorio(db);
                    ES_TransformadorCorriente TC = db.ES_TransformadorCorriente.Find(NoSerie);
                    db.ES_TransformadorCorriente.Remove(TC);
                    int accion = br.GetNumAccion("B", "STC", TC.NumAccion ?? 0);
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
        }

        [HttpPost]
        [TienePermiso(Servicio: 45)]
        public JsonResult EliminarConexiones(String NoSerie)
        {
            try
            {
                List<ES_EsquemaM_TC> listEsquemaMTC = new List<ES_EsquemaM_TC>();
                listEsquemaMTC = db.ES_EsquemaM_TC.Where(c => c.TC == NoSerie).ToList();

                foreach (var item in listEsquemaMTC)
                {
                    db.ES_EsquemaM_TC.Remove(item);
                    db.SaveChanges();
                }


                List<ES_Esquema_TC> listEsquemaTC = new List<ES_Esquema_TC>();
                listEsquemaTC = db.ES_Esquema_TC.Where(c => c.TC == NoSerie).ToList();

                foreach (var item in listEsquemaTC)
                {
                    db.ES_Esquema_TC.Remove(item);
                    db.SaveChanges();
                }

                List<ES_Conexion_IM_TC_TP> listConexionTC = new List<ES_Conexion_IM_TC_TP>();
                listConexionTC = db.ES_Conexion_IM_TC_TP.Where(c => c.TC_TP == NoSerie).ToList();

                foreach (var item in listConexionTC)
                {
                    db.ES_Conexion_IM_TC_TP.Remove(item);
                    db.SaveChanges();
                }

                List<ES_Conexion_Rele_TC_TP> listConexionReleTC = new List<ES_Conexion_Rele_TC_TP>();
                listConexionReleTC = db.ES_Conexion_Rele_TC_TP.Where(c => c.TC_TP == NoSerie).ToList();

                foreach (var item in listConexionReleTC)
                {
                    db.ES_Conexion_Rele_TC_TP.Remove(item);
                    db.SaveChanges();
                }
                Eliminar(NoSerie);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }


        }

        // POST: ES_TransformadorCorriente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ES_TransformadorCorriente eS_TransformadorCorriente = db.ES_TransformadorCorriente.Find(id);
            db.ES_TransformadorCorriente.Remove(eS_TransformadorCorriente);
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

        public bool ValidarSiAsociadoAEsquemaM(string NoSerie)
        {
            var TCM = db.ES_EsquemaM_TC.Where(a => a.TC == NoSerie).FirstOrDefault();

            return TCM != null ? true : false;
        }

        public bool ValidarSiAsociadoAEsquemaP(string NoSerie)
        {
            var TCP = db.ES_Esquema_TC.Where(a => a.TC == NoSerie).FirstOrDefault();

            return TCP != null ? true : false;
        }

        [HttpPost]
        public async Task<ActionResult> ListadoTC()
        {

            var ListaTC = new TCRepositorio(db);
            return PartialView("_VPTransformadoresCorriente", await ListaTC.ObtenerListadoTC());
        }

        [AllowAnonymous]
        public ActionResult CargarDevanados(string NroSerie)
        {
            var ListaDevanados = db.ES_TC_Devanado.Where(x => x.Nro_TC == NroSerie);
            ViewBag.devanados = ListaDevanados;
            ViewBag.cantidad = ListaDevanados.Count();
            return PartialView("_VPDevanados");
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaDevanados(string TC)
        {
            var devanados = await new TCRepositorio(db).ObtenerListaDevanados(TC);
            return Json(devanados, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ActualizarDevanado(short Nro_Dev, string Nro_Serie, float Capacidad, string Clase_Precision, string Designacion)
        {
            if (Nro_Dev != null && Nro_Serie != null)
            {
                try
                {
                    new TCRepositorio(db).ActualizarDevanado(Nro_Dev, Nro_Serie, Capacidad, Clase_Precision, Designacion);
                    return Json("true");
                }
                catch (Exception e)
                {
                    throw e;

                    throw new HttpException((int)HttpStatusCode.NotFound, "Ocurrió un error al editar el devanado.");
                }
            }
            throw new ArgumentNullException();
        }

        [HttpPost]
        public ActionResult AgregarDevanado(short dev, short devInicial, string serie)
        {
            if (serie != null)
            {
                try
                {
                    for (short i = devInicial; i <= dev; i++)
                    {
                        ES_TC_Devanado devanados = new ES_TC_Devanado
                        {
                            Nro_TC = serie,
                            Nro_Dev = i,
                        };
                        db.ES_TC_Devanado.Add(devanados);

                    }
                    ES_TransformadorCorriente TC = db.ES_TransformadorCorriente.Find(serie);
                    TC.Cant_Devanado = dev;
                    db.Entry(TC).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json("true");

                }

                catch (Exception e)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            throw new ArgumentNullException();
        }

        [HttpPost]
        public ActionResult EliminarDevanado(short dev, short devInicial, string serie)
        {
            if (serie != null)
            {
                try
                {
                    for (short i = devInicial; i > dev; i--)
                    {
                        ES_TC_Devanado devanado = db.ES_TC_Devanado.Find(i, serie);
                        if (devanado != null)
                        {
                            db.ES_TC_Devanado.Remove(devanado);
                        }

                    }
                    ES_TransformadorCorriente TC = db.ES_TransformadorCorriente.Find(serie);
                    TC.Cant_Devanado = dev;
                    db.Entry(TC).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json("true");

                }

                catch (Exception e)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            throw new ArgumentNullException();
        }

        [AllowAnonymous]
        public ActionResult CargarEquipo(string tipoequipo, string codsub)
        {
            CodigoEquipos(tipoequipo, codsub);
            return PartialView("_VPCodigoEquipo");
        }



        public void CodigoEquipos(string tipoequipo, string codsub)
        {
            if (tipoequipo == "Barra")
            {
                var barras = (
                    from sb in db.Sub_Barra
                    join vs in db.VoltajesSistemas
                    on sb.ID_Voltaje equals vs.Id_VoltajeSistema
                    where sb.Subestacion.Contains(codsub)
                    select new SelectListItem { Value = sb.codigo, Text = "Código: " + sb.codigo + " Voltaje: " + vs.Voltaje.ToString() }
                ).ToList();
                ViewBag.Elemento_Electrico = new SelectList(barras, "Value", "Text");
            }

            else if (tipoequipo == "Transformador")
            {
                var transformadores = db.TransformadoresTransmision
                    .Where(c => c.Codigo.Contains(codsub))
                    .Select(c => new SelectListItem { Value = c.Nombre, Text = c.Nombre })
                    .Union(db.TransformadoresSubtransmision
                    .Where(c => c.Codigo.Contains(codsub))
                    .Select(c => new SelectListItem { Value = c.Nombre, Text = c.Nombre })
                    ).ToList();
                ViewBag.Elemento_Electrico = new SelectList(transformadores, "Value", "Text");
            }
            else if (tipoequipo == "Interruptor")
            {
                var desconectivo = db.InstalacionDesconectivos
                    .Where(c => c.UbicadaEn.Contains(codsub) && (c.TipoSeccionalizador == "3" || c.TipoSeccionalizador == "4" || c.TipoSeccionalizador == "5" || c.TipoSeccionalizador == "6" || c.TipoSeccionalizador == "7"))
                    .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo }).ToList();

                ViewBag.Elemento_Electrico = new SelectList(desconectivo, "Value", "Text");
            }

            else if (tipoequipo == "Generador")
            {
                var Generador = db.GruposGs
                    .Where(c => c.Instalacion_Transformadora.Contains(codsub))
                    .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo }).ToList();

                ViewBag.Elemento_Electrico = new SelectList(Generador, "Value", "Text");
            }


        }

        public void CodigoEquipos(string tipoequipo, string codsub, string NoSerie)
        {
            if (tipoequipo == "Barra")
            {
                var barras = (
                    from sb in db.Sub_Barra
                    join vs in db.VoltajesSistemas
                    on sb.ID_Voltaje equals vs.Id_VoltajeSistema
                    where sb.Subestacion.Contains(codsub)
                    select new SelectListItem { Value = sb.codigo, Text = "Código: " + sb.codigo + " Voltaje: " + vs.Voltaje.ToString() }
                ).ToList();
                ViewBag.Elemento_Electrico = new SelectList(barras, "Value", "Text", NoSerie);
            }

            else if (tipoequipo == "Transformador")
            {
                var transformadores = db.TransformadoresTransmision
                    .Where(c => c.Codigo.Contains(codsub))
                    .Select(c => new SelectListItem { Value = c.Nombre, Text = c.Nombre })
                    .Union(db.TransformadoresSubtransmision
                    .Where(c => c.Codigo.Contains(codsub))
                    .Select(c => new SelectListItem { Value = c.Nombre, Text = c.Nombre })
                    );
                ViewBag.Elemento_Electrico = new SelectList(transformadores, "Value", "Text", NoSerie);
            }
            else if (tipoequipo == "Interruptor")
            {
                var desconectivo = db.InstalacionDesconectivos
                    .Where(c => c.UbicadaEn.Contains(codsub) && (c.TipoSeccionalizador == "3" || c.TipoSeccionalizador == "4" || c.TipoSeccionalizador == "5" || c.TipoSeccionalizador == "6" || c.TipoSeccionalizador == "7"))
                    .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo }).ToList();

                ViewBag.Elemento_Electrico = desconectivo/*new SelectList(desconectivo, "Value", "Text")*/;
            }

            else if (tipoequipo == "Generador")
            {
                var Generador = db.GruposGs
                    .Where(c => c.Instalacion_Transformadora.Contains(codsub))
                    .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo }).ToList();

                ViewBag.Elemento_Electrico = new SelectList(Generador, "Value", "Text", NoSerie);
            }


        }


    }
}
