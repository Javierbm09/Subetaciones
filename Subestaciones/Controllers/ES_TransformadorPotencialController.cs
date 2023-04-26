using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class ES_TransformadorPotencialController : Controller
    {
        private DBContext db = new DBContext();

        // GET: ES_TransformadorPotencial
        public async Task<ActionResult> Index(string inserta)
        {
            var ListaTPs = new TPRepositorio(db);
            ViewBag.Inserto = inserta;

            return View(await ListaTPs.ObtenerListadoTP());
        }

        // GET: ES_TransformadorPotencial/Details/5
        public async Task<ActionResult> Details(string Nro_Serie)
        {
            if (Nro_Serie == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaTP = new TPRepositorio(db);
            var TP = await ListaTP.FindAsync(Nro_Serie);
            if (Nro_Serie == null)
            {
                return HttpNotFound();
            }
            return View(TP);
        }

        // GET: ES_TransformadorPotencial/Create
        [TienePermiso(Servicio: 47)]
        public ActionResult Create()
        {
            var repo = new Repositorio(db);
            var repoTC = new TCRepositorio(db);
            ViewBag.CodigoSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TequipoProt = new SelectList(repoTC.TipoEquipo(), "Value", "Text");
            ViewBag.Fase = repoTC.Fase(""); //new SelectList(repoTC.Fase(), "Value", "Text");
            ViewBag.Frecuencia = repoTC.Frecuencia(0);
            ViewBag.devanados = repoTC.CantDevanados(3);

            ViewBag.Id_Voltaje = new SelectList(repoTC.VoltajeInstalado(), "Id_VoltajeSistema", "Voltaje");
            return View();
        }

        // POST: ES_TransformadorPotencial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Nro_Serie,id_Voltaje_Primario,Cant_Devanado,Fase,id_Plantilla,Ubicado,CodSub,Tipo_Equipo_Primario,Elemento_Electrico,Id_EAdministrativa,NumAccion,Fabricante,Tipo,Inventario,InPrimaria,InTrabajoPrim,InSecundaria,Frecuencia,AnnoFab,FechaInstalado,Peso,VoltajeNominal")] ES_TransformadorPotencial eS_TransformadorPotencial)
        {
            var repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario(); //esta EA ya esta bien
            if (ModelState.IsValid)
            {
                if (ValidarTPFase(eS_TransformadorPotencial.CodSub, eS_TransformadorPotencial.Fase, eS_TransformadorPotencial.Elemento_Electrico, eS_TransformadorPotencial.id_Voltaje_Primario))
                {
                    if (await ValidarNoSerie(eS_TransformadorPotencial.NoSerieAnt, eS_TransformadorPotencial.Nro_Serie))
                    {
                        eS_TransformadorPotencial.Id_EAdministrativa = (int)Id_Eadministrativa;
                        eS_TransformadorPotencial.NumAccion = repo.GetNumAccion("I", "STP", 0);
                        db.ES_TransformadorPotencial.Add(eS_TransformadorPotencial);
                        db.SaveChanges();
                        if (eS_TransformadorPotencial.Cant_Devanado > 0)
                        {
                            for (short i = 1; i <= eS_TransformadorPotencial.Cant_Devanado; i++)
                            {
                                ES_TP_Devanado devanados = new ES_TP_Devanado
                                {
                                    Nro_TP = eS_TransformadorPotencial.Nro_Serie,
                                    Nro_Dev = i,
                                };
                                db.ES_TP_Devanado.Add(devanados);
                            }
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index", new { inserta = "Si" });

                    }
                    else
                    {
                        ModelState.AddModelError("Nro_Serie", "Ya existe un TP con ese No Serie.");
                    }
                }
                else { ModelState.AddModelError("Fase", "Ya existe un TP asociado a la Fase del equipo protegido seleccionado."); }

            }

            var repoTC = new TCRepositorio(db);
            ViewBag.CodigoSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TequipoProt = new SelectList(repoTC.TipoEquipo(), "Value", "Text");
            ViewBag.Fase = repoTC.Fase("");
            ViewBag.Frecuencia = repoTC.Frecuencia(0);
            ViewBag.Id_Voltaje = new SelectList(repoTC.VoltajeInstalado(), "Id_VoltajeSistema", "Voltaje");
            ViewBag.Cant = 0;
            ViewBag.devanados = repoTC.CantDevanados(3);

            return View(eS_TransformadorPotencial);
        }

        private async Task<bool> ValidarNoSerie(string NoSerieAnt, string Nro_Serie)
        {
            var listaTP = await new TPRepositorio(db).ObtenerListadoTP();

            return !listaTP.Select(c => new { c.Nro_Serie }).Where(c => c.Nro_Serie == Nro_Serie).Any(c => c.Nro_Serie != NoSerieAnt);
        }

        private bool ValidarTPFase(string CodSub, string Fase, string EE, short volt)
        {
            var listado = db.ES_TransformadorPotencial.Where(x => x.CodSub == CodSub && x.Fase == Fase && x.Elemento_Electrico == EE && x.id_Voltaje_Primario == volt).ToList();

            if (listado.Count() == 0) return true; else return false;

        }

        private bool ValidarTCFase(string CodSub, string Fase, string EE, short? volt, string FaseAnt)
        {
            if (Fase != FaseAnt)
            {
                var listado = db.ES_TransformadorPotencial.Where(x => x.CodSub == CodSub && x.Fase == Fase && x.Elemento_Electrico == EE && x.id_Voltaje_Primario == volt).ToList();

                if (listado.Count() == 0) return true; else return false;
            }
            else return true;
        }

        // GET: ES_TransformadorPotencial/Edit/5
        [TienePermiso(Servicio: 48)]
        public ActionResult Edit(string Nro_Serie)
        {
            if (Nro_Serie == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ES_TransformadorPotencial TP = db.ES_TransformadorPotencial.Find(Nro_Serie);

            var repo = new Repositorio(db);
            var repoTC = new TCRepositorio(db);
            ViewBag.CodigoSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TequipoProt = new SelectList(repoTC.TipoEquipo(), "Value", "Text");
            ViewBag.Fase = repoTC.Fase(TP.Fase);
            ViewBag.devanados = repoTC.CantDevanados(TP.Cant_Devanado);
            ViewBag.Frecuencia = repoTC.Frecuencia(TP.Frecuencia);
            ViewBag.Id_Voltaje = new SelectList(repoTC.VoltajeInstalado(), "Id_VoltajeSistema", "Voltaje");
            CodigoEquipos(TP.Tipo_Equipo_Primario, TP.CodSub, TP.Elemento_Electrico);
            return View(TP);
        }

        // POST: ES_TransformadorPotencial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Nro_Serie,id_Voltaje_Primario,Cant_Devanado,Fase,id_Plantilla,Ubicado,CodSub,Tipo_Equipo_Primario,Elemento_Electrico,Id_EAdministrativa,NumAccion,Fabricante,Tipo,Inventario,InPrimaria,InTrabajoPrim,InSecundaria,Frecuencia,AnnoFab,FechaInstalado,Peso,VoltajeNominal,  NoSerieAnt, FaseAnt")] ES_TransformadorPotencial eS_TransformadorPotencial)
        {
            var repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario(); //esta EA ya esta bien
            if (ModelState.IsValid)
            {
                if (ValidarTCFase(eS_TransformadorPotencial.CodSub, eS_TransformadorPotencial.Fase, eS_TransformadorPotencial.Elemento_Electrico, eS_TransformadorPotencial.id_Voltaje_Primario, eS_TransformadorPotencial.FaseAnt))
                {
                    if (eS_TransformadorPotencial.NoSerieAnt != eS_TransformadorPotencial.Nro_Serie)
                    {
                        if (await ValidarNoSerie(eS_TransformadorPotencial.NoSerieAnt, eS_TransformadorPotencial.Nro_Serie))
                        {
                            await db.Database.ExecuteSqlCommandAsync("UPDATE ES_TransformadorPotencial SET Nro_Serie = @Nro_Serie WHERE Nro_Serie = @NoSerieAnt",
                                new SqlParameter("@Nro_Serie", eS_TransformadorPotencial.Nro_Serie),
                                new SqlParameter("@NoSerieAnt", eS_TransformadorPotencial.NoSerieAnt));

                            eS_TransformadorPotencial.Id_EAdministrativa = (int)Id_Eadministrativa;
                            eS_TransformadorPotencial.NumAccion = repo.GetNumAccion("M", "STP", eS_TransformadorPotencial.NumAccion ?? 0);
                            db.Entry(eS_TransformadorPotencial).State = EntityState.Modified;
                            db.SaveChanges();

                            return RedirectToAction("Index");
                        }
                        else
                            ModelState.AddModelError("Nro_Serie", "Ya existe la TP en la subestación.");
                    }
                    else if (eS_TransformadorPotencial.NoSerieAnt == eS_TransformadorPotencial.Nro_Serie)
                    {
                        eS_TransformadorPotencial.Id_EAdministrativa = (int)Id_Eadministrativa;
                        eS_TransformadorPotencial.NumAccion = repo.GetNumAccion("M", "STP", eS_TransformadorPotencial.NumAccion ?? 0);
                        db.Entry(eS_TransformadorPotencial).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    //db.Entry(eS_TransformadorCorriente).State = EntityState.Modified;
                    //db.SaveChanges();
                    //return RedirectToAction("Index");
                }
                else { ModelState.AddModelError("Fase", "Ya existe un TP asociado a la Fase del equipo protegido seleccionado."); }
            }
            var repoTC = new TCRepositorio(db);
            ViewBag.CodigoSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TequipoProt = new SelectList(repoTC.TipoEquipo(), "Value", "Text");
            ViewBag.Fase = repoTC.Fase(eS_TransformadorPotencial.Fase);
            ViewBag.Frecuencia = repoTC.Frecuencia(eS_TransformadorPotencial.Frecuencia);
            ViewBag.Id_Voltaje = new SelectList(repoTC.VoltajeInstalado(), "Id_VoltajeSistema", "Voltaje");
            CodigoEquipos(eS_TransformadorPotencial.Tipo_Equipo_Primario, eS_TransformadorPotencial.CodSub, eS_TransformadorPotencial.Elemento_Electrico);
            return View(eS_TransformadorPotencial);
        }

        // GET: ES_TransformadorPotencial/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ES_TransformadorPotencial eS_TransformadorPotencial = db.ES_TransformadorPotencial.Find(id);
            if (eS_TransformadorPotencial == null)
            {
                return HttpNotFound();
            }
            return View(eS_TransformadorPotencial);
        }

        // POST: ES_TransformadorPotencial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ES_TransformadorPotencial eS_TransformadorPotencial = db.ES_TransformadorPotencial.Find(id);
            db.ES_TransformadorPotencial.Remove(eS_TransformadorPotencial);
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

        public async Task<ActionResult> ListadoTP()
        {            
            return PartialView("_VPTransformadoresPotencial",await new TPRepositorio(db).ObtenerListadoTP());
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
                    ES_TransformadorPotencial TP = db.ES_TransformadorPotencial.Find(NoSerie);
                    db.ES_TransformadorPotencial.Remove(TP);
                    int accion = br.GetNumAccion("B", "STP", TP.NumAccion ?? 0);
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
                List<ES_EsquemaM_TP> listEsquemaMTP = new List<ES_EsquemaM_TP>();
                listEsquemaMTP = db.ES_EsquemaM_TP.Where(c => c.TP == NoSerie).ToList();

                foreach (var item in listEsquemaMTP)
                {
                    db.ES_EsquemaM_TP.Remove(item);
                    db.SaveChanges();
                }


                List<ES_Esquema_TP> listEsquemaTP = new List<ES_Esquema_TP>();
                listEsquemaTP = db.ES_Esquema_TP.Where(c => c.TP == NoSerie).ToList();

                foreach (var item in listEsquemaTP)
                {
                    db.ES_Esquema_TP.Remove(item);
                    db.SaveChanges();
                }

                List<ES_Conexion_IM_TC_TP> listConexionTP = new List<ES_Conexion_IM_TC_TP>();
                listConexionTP = db.ES_Conexion_IM_TC_TP.Where(c => c.TC_TP == NoSerie).ToList();

                foreach (var item in listConexionTP)
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
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public bool ValidarSiAsociadoAEsquemaM(string NoSerie)
        {
            var TCM = db.ES_EsquemaM_TP.Where(a => a.TP == NoSerie).FirstOrDefault();

            return TCM != null ? true : false;
        }

        public bool ValidarSiAsociadoAEsquemaP(string NoSerie)
        {
            var TCP = db.ES_Esquema_TP.Where(a => a.TP == NoSerie).FirstOrDefault();

            return TCP != null ? true : false;
        }

        #region Vistas Parciales (VP) AJAX
        [AllowAnonymous]
        public ActionResult CargarDevanados(string NroSerie)
        {
            var ListaDevanados = db.ES_TP_Devanado.Where(x => x.Nro_TP == NroSerie);
            ViewBag.devanados = ListaDevanados;
            ViewBag.cantidad = ListaDevanados.Count();
            return PartialView("_VPDevanados");
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaDevanados(string TP)
        {
            var devanados = await new TPRepositorio(db).ObtenerListaDevanados(TP);
            return Json(devanados, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult ActualizarDevanado(short Nro_Dev, string Nro_Serie, string tension, float Capacidad, string Clase_Precision, string Designacion)
        {
            if (Nro_Dev != 0 && Nro_Serie != null)
            {
                try
                {
                    new TPRepositorio(db).ActualizarDevanado(Nro_Dev, Nro_Serie, Capacidad, tension, Clase_Precision, Designacion);
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
                        ES_TP_Devanado devanados = new ES_TP_Devanado
                        {
                            Nro_TP = serie,
                            Nro_Dev = i,
                        };
                        db.ES_TP_Devanado.Add(devanados);

                    }
                    ES_TransformadorPotencial TP = db.ES_TransformadorPotencial.Find(serie);
                    TP.Cant_Devanado = dev;
                    db.Entry(TP).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json("true");

                }

                catch (Exception)
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
                        ES_TP_Devanado devanado = db.ES_TP_Devanado.Find(i, serie);
                        if (devanado != null)
                        {
                            db.ES_TP_Devanado.Remove(devanado);
                        }

                    }
                    ES_TransformadorPotencial TP = db.ES_TransformadorPotencial.Find(serie);
                    TP.Cant_Devanado = dev;
                    db.Entry(TP).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json("true");

                }

                catch (Exception)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            throw new ArgumentNullException();
        }

        public ActionResult CargarEquipo(string tipoequipo, string codsub)
        {
            CodigoEquipos(tipoequipo, codsub);
            return PartialView("_VPCodigoEquipo");
        }
        #endregion


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

