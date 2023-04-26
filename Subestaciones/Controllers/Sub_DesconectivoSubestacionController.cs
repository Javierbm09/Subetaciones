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
using System.Data.SqlClient;

namespace Subestaciones.Controllers
{
    public class Sub_DesconectivoSubestacionController : Controller
    {
        private DBContext db = new DBContext();

        [HttpGet]
        public ActionResult Desconectivos()
        {
            var descLista = new DesconectivoSubestacionesRepositorio(db);
            ViewBag.desc = descLista.ListaDesconectivos();

            return View();

        }

        [HttpPost]
        public ActionResult Desconectivos(InstalacionDesconectivos instalacion)
        {
            if (instalacion.TipoSeccionalizador == "0") // el desconectivo es tipo puente
            {
                int id = db.Inst_Nomenclador_Puente.Where(c => c.Codigo == instalacion.Codigo).Select(c => c.Id_Puente).FirstOrDefault();
                
                return RedirectToAction("Edit", "Inst_Nomenclador_Puente", new { id = id });

            }

            if (instalacion.TipoSeccionalizador == "1") // el desconectivo es tipo portafusible
            {
                //aqui tengo que ver si es tipo portafusible o breaker, para ir a editar segun corresponda, si cambian el tipo de poratfusible
                // pues hay que eliminar de la tabla del tipo que era y agregar en la del nuevo tipo.
                int id = db.Inst_Nomenclador_Puente.Where(c => c.Codigo == instalacion.Codigo).Select(c => c.Id_Puente).FirstOrDefault();

                return RedirectToAction("Edit", "Inst_Nomenclador_Puente", new { id = id });

            }
            //else return PartialView("_VPDatosDesconectivos");
            var descLista = new DesconectivoSubestacionesRepositorio(db);
            ViewBag.desc = descLista.ListaDesconectivos();

            return View();

        }



        // GET: Sub_DesconectivoSubestacion
        public ActionResult Index()
        {
            var sub_DesconectivoSubestacion = db.Sub_DesconectivoSubestacion.Include(s => s.Sub_RedCorrienteAlterna);
            return View(sub_DesconectivoSubestacion.ToList());
        }

        // GET: Sub_DesconectivoSubestacion/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_DesconectivoSubestacion sub_DesconectivoSubestacion = db.Sub_DesconectivoSubestacion.Find(id);
            if (sub_DesconectivoSubestacion == null)
            {
                return HttpNotFound();
            }
            return View(sub_DesconectivoSubestacion);
        }

        [HttpGet]
        public ActionResult GetBreakerPorbaja(short? idRedCA)
        {
            var breaker = (from BPB in db.Sub_DesconectivoSubestacion
                                     where BPB.RedCA.Equals((short)idRedCA)
                                     select new BreakerPorBaja
                                     {
                                         CodigoDesconectivo = BPB.CodigoDesconectivo,
                                         TensionNominal = BPB.TensionNominal,
                                         CorrienteNominal = BPB.CorrienteNominal,

                                     }).ToList();

            return Json(new { data = breaker}, JsonRequestBehavior.AllowGet );
        }
        // GET: Sub_DesconectivoSubestacion/Edit/5
        public ActionResult Save(int? RedCA, string CodigoSub, string CodigoDesconectivo)
        {
            if (RedCA == 0 || CodigoSub == "" || CodigoDesconectivo == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Sub_DesconectivoSubestacion breaker = db.Sub_DesconectivoSubestacion.Find(RedCA, CodigoSub, CodigoDesconectivo);

            return View("_VPSave", breaker);
        }

        [HttpPost]
        public ActionResult _VPSave(Sub_DesconectivoSubestacion desconectivoSubestacion) {
            
            if (ModelState.IsValid)
            {
                if (desconectivoSubestacion.RedCA > 0 ) {
                    // Salvar
                    db.Sub_DesconectivoSubestacion.Add(desconectivoSubestacion);
                }
                else
                {
                    // Editar
                    var v = db.Sub_DesconectivoSubestacion.Where(a => a.RedCA == desconectivoSubestacion.RedCA && a.CodigoDesconectivo == desconectivoSubestacion.CodigoDesconectivo && a.CodigoSub == desconectivoSubestacion.CodigoSub).FirstOrDefault();

                    if (v != null) {
                        v.CodigoDesconectivo = desconectivoSubestacion.CodigoDesconectivo;
                        v.CodigoSub = desconectivoSubestacion.CodigoSub;
                        v.CorrienteNominal = desconectivoSubestacion.CorrienteNominal;
                        v.RedCA = desconectivoSubestacion.RedCA;
                        v.Sub_RedCorrienteAlterna = desconectivoSubestacion.Sub_RedCorrienteAlterna;
                        v.TensionNominal = desconectivoSubestacion.TensionNominal;                         
                    }
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception E)
                {
                    //throw;
                    ModelState.AddModelError("desconectivoSubestacion.CodigoDesconectivo", "Ya existe un breaker por baja con ese código en la Red de Corriente Alterna de la subestación.");
                }
            }
            return new JsonResult { Data = desconectivoSubestacion  };
        }

        [HttpGet]
        public ActionResult Delete(short? idRedCA, string codigoSubestacion, string codigoDesconectivo)
        {
            var v = db.Sub_DesconectivoSubestacion.Where(a => a.RedCA == idRedCA && a.CodigoDesconectivo == codigoDesconectivo && a.CodigoSub == codigoSubestacion).FirstOrDefault();
            if (v != null)
            {
                return View(v);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        //[ActionName("Delete")]
        public JsonResult EliminaDesconectivoSubestacion(short idRedCA, string codigoSubestacion, string codigoDesconectivo)
        {
            bool status = false;
            var v = db.Sub_DesconectivoSubestacion.Where(a => a.RedCA == idRedCA && a.CodigoDesconectivo == codigoDesconectivo && a.CodigoSub == codigoSubestacion).FirstOrDefault();
            if (v != null)
            {
                db.Sub_DesconectivoSubestacion.Remove(v);
                db.SaveChanges();

                status = true;
            }
            
            return new JsonResult { Data =  new { status = status} };
        }

        // GET: Sub_DesconectivoSubestacion/Create
        public async Task<ActionResult> Create(short id, string codigoSubestacion)
        {
            var redcCa = await db.Sub_RedCorrienteAlterna.FindAsync(id); 
            Sub_DesconectivoSubestacion desconectivo = new Sub_DesconectivoSubestacion { RedCA = id, CodigoSub = codigoSubestacion };
            ViewBag.RedCA = redcCa.NombreServicioCA;

            return View(desconectivo);
        }

        // POST: Sub_DesconectivoSubestacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RedCA,CodigoSub,CodigoDesconectivo,TensionNominal,CorrienteNominal,CodDescAnterior")] Sub_DesconectivoSubestacion sub_DesconectivoSubestacion)
        {
            if (ModelState.IsValid)
            {    //si  CodDescAnterior es diferente de null, estoy editando     
                if (sub_DesconectivoSubestacion.CodDescAnterior == null) {//inserto
                                                                          //verifico que no exista el cogido de desconectivo en la red
                    if (ValidarExisteDesconectivoEnRed(sub_DesconectivoSubestacion.RedCA, sub_DesconectivoSubestacion.CodigoSub, sub_DesconectivoSubestacion.CodigoDesconectivo))
                    {
                        db.Sub_DesconectivoSubestacion.Add(sub_DesconectivoSubestacion);
                        db.SaveChanges();

                        return RedirectToAction("Create", "Sub_DesconectivoSubestacion", new { id = sub_DesconectivoSubestacion.RedCA, codigoSubestacion = sub_DesconectivoSubestacion.CodigoSub });
                        //return new JsonResult { Data = sub_DesconectivoSubestacion };
                    } 
                    //else
                    //{
                    //    ModelState.AddModelError("CodigoDesconectivo", "Ya existe un breaker por baja con ese código en la Red de Corriente Alterna de la subestación.");
                    //}
                }
                else { //edito
                    if (sub_DesconectivoSubestacion.CodDescAnterior != sub_DesconectivoSubestacion.CodigoDesconectivo)
                    {
                        //verifico que no exista el cogido de desconectivo en la red
                        if (ValidarExisteDesconectivoEnRed(sub_DesconectivoSubestacion.RedCA, sub_DesconectivoSubestacion.CodigoSub, sub_DesconectivoSubestacion.CodigoDesconectivo))
                        {
                            await db.Database.ExecuteSqlCommandAsync("UPDATE Sub_DesconectivoSubestacion SET CodigoDesconectivo = @CodigoDesconectivo WHERE CodigoDesconectivo = @CodDescAnterior", new SqlParameter("@CodigoDesconectivo", sub_DesconectivoSubestacion.CodigoDesconectivo), new SqlParameter("@CodDescAnterior", sub_DesconectivoSubestacion.CodDescAnterior));
                        }
                        else
                        {
                            ModelState.AddModelError("Codigo", "Ya existe un breaker por baja con ese código en la Red de Corriente Alterna de la subestación.");
                        }
                    }
                    db.Entry(sub_DesconectivoSubestacion).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Edit", "Sub_DesconectivoSubestacion", new { id = sub_DesconectivoSubestacion.RedCA, codigoSubestacion = sub_DesconectivoSubestacion.CodigoSub });


                }
            }

            //ViewBag.RedCA = new SelectList(db.Sub_RedCorrienteAlterna, "Id_RedCA", "NombreServicioCA", sub_DesconectivoSubestacion.RedCA);
            //return View(sub_DesconectivoSubestacion);

            Sub_DesconectivoSubestacion desconectivo = new Sub_DesconectivoSubestacion { RedCA = sub_DesconectivoSubestacion.RedCA, CodigoSub = sub_DesconectivoSubestacion.CodigoSub };

            return PartialView("_VPSave", desconectivo);
            
        }

        // GET: Sub_DesconectivoSubestacion/Edit/5
        public async Task<ActionResult> Edit(short id, string codigoSubestacion)
        {
            var redcCa = await db.Sub_RedCorrienteAlterna.FindAsync(id);
            Sub_DesconectivoSubestacion desconectivo = new Sub_DesconectivoSubestacion { RedCA = id, CodigoSub = codigoSubestacion};
            ViewBag.RedCA = redcCa.NombreServicioCA;
            //ViewBag.codigoDesc = desconectivo.CodigoDesconectivo;

            return View(desconectivo);
        }

        private bool ValidarExisteDesconectivoEnRed(short id, string codigoSubestacion, string codigoDesconectivo)
        {

            var listaBreakers = db.Sub_DesconectivoSubestacion.Where(a => a.RedCA == id && a.CodigoSub == codigoSubestacion && a.CodigoDesconectivo == codigoDesconectivo).FirstOrDefault();
            return listaBreakers == null ? true : false;
        }

        // POST: Sub_DesconectivoSubestacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RedCA,CodigoSub,CodigoDesconectivo,TensionNominal,CorrienteNominal,CodDescAnterior")] Sub_DesconectivoSubestacion sub_DesconectivoSubestacion)
        {
            if (ModelState.IsValid)
            {
                // busco si no existe el codigo anterior, de ser asi inserto, si no edito
                if (sub_DesconectivoSubestacion.CodDescAnterior == null)
                {
                    db.Sub_DesconectivoSubestacion.Add(sub_DesconectivoSubestacion);
                    db.SaveChanges();

                    return RedirectToAction("Create", "Sub_DesconectivoSubestacion", new { id = sub_DesconectivoSubestacion.RedCA, codigoSubestacion = sub_DesconectivoSubestacion.CodigoSub });

                }

                //aqui edito, verifico que el codigo fuera cambiado, si cambio actualizo BD, si no edito y ya
                else
                {
                    if (sub_DesconectivoSubestacion.CodDescAnterior != sub_DesconectivoSubestacion.CodigoDesconectivo)
                    {
                        await db.Database.ExecuteSqlCommandAsync("UPDATE Sub_DesconectivoSubestacion SET CodigoDesconectivo = @CodigoDesconectivo WHERE CodigoDesconectivo = @CodDescAnterior", new SqlParameter("@CodigoDesconectivo", sub_DesconectivoSubestacion.CodigoDesconectivo), new SqlParameter("@CodDescAnterior", sub_DesconectivoSubestacion.CodDescAnterior));
                    }
                    db.Entry(sub_DesconectivoSubestacion).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction( "Edit", "Sub_DesconectivoSubestacion", new { id = sub_DesconectivoSubestacion.RedCA, codigoSubestacion = sub_DesconectivoSubestacion.CodigoSub });
                }
            
            }
            ViewBag.RedCA = new SelectList(db.Sub_RedCorrienteAlterna, "Id_RedCA", "NombreServicioCA", sub_DesconectivoSubestacion.RedCA);
            return View(sub_DesconectivoSubestacion);
        }



        // GET: Sub_DesconectivoSubestacion/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_DesconectivoSubestacion sub_DesconectivoSubestacion = db.Sub_DesconectivoSubestacion.Find(id);
            if (sub_DesconectivoSubestacion == null)
            {
                return HttpNotFound();
            }
            return View(sub_DesconectivoSubestacion);
        }

        // POST: Sub_DesconectivoSubestacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Sub_DesconectivoSubestacion sub_DesconectivoSubestacion = db.Sub_DesconectivoSubestacion.Find(id);
            db.Sub_DesconectivoSubestacion.Remove(sub_DesconectivoSubestacion);
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

        #region Vistas Parciales (VP) Ajax
        [AllowAnonymous]
        public async Task<ActionResult> CargarTablaBreakerPorBaja(short? redCA)
        {
            DesconectivoSubestacionesRepositorio repositorio = new DesconectivoSubestacionesRepositorio(db);
            var listado = await repositorio.ListadoBreakerPorBajaEnumerable(redCA);

            ViewBag.bancosTransformadores = listado;

            return PartialView("_VPTablaBreakerPorBaja");
        }

        public ActionResult CargarFomularioBreakerPorBaja(short idRedCA, string codigoSubestacion, string codigoDesc= "")
        {
            ViewBag.idRedCA = idRedCA;
            ViewBag.CodSub = codigoSubestacion;
            Sub_DesconectivoSubestacion desconectivo = null;
            if (codigoDesc == "")
            {
                desconectivo = new Sub_DesconectivoSubestacion { RedCA = idRedCA, CodigoSub = codigoSubestacion };
            }
            else
            {
                desconectivo = db.Sub_DesconectivoSubestacion.Find(idRedCA, codigoSubestacion, codigoDesc);
                if (desconectivo==null)
                {
                    return HttpNotFound();
                }
            }

            return PartialView("_VPSave", desconectivo);
            //return RedirectToAction("_VPSave");
        }

        public ActionResult EditarBreakerPorBaja(short idRedCA, string codigoSubestacion, string codigoDesc)
        {
            ViewBag.idRedCA = idRedCA;
            ViewBag.CodSub = codigoSubestacion;

            Sub_DesconectivoSubestacion desconectivo = new Sub_DesconectivoSubestacion { RedCA = idRedCA, CodigoSub = codigoSubestacion, CodigoDesconectivo = codigoDesc };

            return PartialView("_VPEdit", desconectivo);
            //return RedirectToAction("_VPSave");
        }

        //public ActionResult editarPuente(int id_puente, int tipo, int modelo, int b)
        //{
        //    Inst_Nomenclador_Puente puente = db.Inst_Nomenclador_Puente.Find(id_puente);
        //    puente.Id_Tipo = tipo;
        //    puente.Id_Modelo = modelo;
        //    puente.Bimetalica = b;
        //    db.Entry(puente).State = EntityState.Modified;
        //    db.SaveChanges();
        //    var TipoPuentes = (
        //            from sb in db.Inst_Nomenclador_Puente_Tipo
        //            select new SelectListItem { Value = sb.Id_Tipo.ToString(), Text = sb.DescripcionTipo.ToString() }
        //        ).ToList();

        //    var ModeloPuentes = (
        //        from sb in db.Inst_Nomenclador_Puente_Modelo
        //        select new SelectListItem { Value = sb.Id_Modelo.ToString(), Text = sb.Descripcion_Modelo.ToString() }
        //    ).ToList();
        //    ViewBag.tipo = new SelectList(TipoPuentes, "Value", "Text", puente.Id_Tipo);
        //    ViewBag.modelo = new SelectList(ModeloPuentes, "Value", "Text", puente.Id_Modelo);
        //    return PartialView("_VPDatosDesconectivos");
            
        //}

        public ActionResult editarDesc(string tipoDes, string codigoDesc)
        {
            if (tipoDes == "0") // el desconectivo es tipo puente
            {
                int id = db.Inst_Nomenclador_Puente.Where(c => c.Codigo == codigoDesc).Select(c => c.Id_Puente).FirstOrDefault();
                return RedirectToAction("Edit", "Inst_Nomenclador_Puente", new { id = id });

            }
            else return PartialView("_VPDatosDesconectivos");

        }

        public ActionResult CargarDesc()
        {
            return PartialView("_VPDatosDesconectivos");
           
            //else if (tipoequipo == "L")
            //{
            //    var lineas =
            //        db.Sub_LineasSubestacion
            //        .Where(c => c.Subestacion.Contains(codsub))
            //        .Select(c => new SelectListItem { Value = c.Circuito, Text = c.Circuito }).ToList();

            //    ViewBag.CE = new SelectList(lineas, "Value", "Text");
            //}
            //else if (tipoequipo == "T")
            //{
            //    var transformadores = db.TransformadoresTransmision
            //        .Where(c => c.Codigo.Contains(codsub))
            //        .Select(c => new SelectListItem { Value = c.Nombre, Text = c.Nombre })
            //        .Union(db.TransformadoresSubtransmision
            //        .Where(c => c.Codigo.Contains(codsub))
            //        .Select(c => new SelectListItem { Value = c.Nombre, Text = c.Nombre })
            //        );
            //    ViewBag.CE = new SelectList(transformadores, "Value", "Text");
            //}
            //else if (tipoequipo == "D")
            //{
            //    var desconectivo = db.InstalacionDesconectivos
            //        .Where(c => c.UbicadaEn.Contains(codsub))
            //        .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo }).ToList();

            //    ViewBag.CE = new SelectList(desconectivo, "Value", "Text");
            //}
            //else if (tipoequipo == "TC")
            //{
            //    var TC = db.Sub_Alimentacion_TP_TC
            //        .Where(c => c.Subestacion.Contains(codsub) && c.CodigoEquipo.Contains("TC"))
            //        .Select(c => new SelectListItem { Value = c.CodigoEquipo, Text = c.CodigoEquipo }).ToList();

            //    ViewBag.CE = new SelectList(TC, "Value", "Text");
            //}
            //else if (tipoequipo == "TP")
            //{
            //    var TP = db.Sub_Alimentacion_TP_TC
            //        .Where(c => c.Subestacion.Contains(codsub) && c.CodigoEquipo.Contains("TP"))
            //        .Select(c => new SelectListItem { Value = c.CodigoEquipo, Text = c.CodigoEquipo }).ToList();

            //    ViewBag.CE = new SelectList(TP, "Value", "Text");
            //}
            //else if (tipoequipo == "TUP")
            //{
            //    var TUP = db.BancoTransformadores
            //        .Where(c => c.Circuito.Contains(codsub))
            //        .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo }).ToList();

            //    ViewBag.CE = new SelectList(TUP, "Value", "Text");
            //}
        }

        [HttpPost]
        public async Task<ActionResult> InsertarBreakerBaja(string sub, short? red, string codD, double? tensionN, double? corrienteN)
        {
            if ((sub != null) && (red != null) && (codD != null) && (tensionN != null) && (corrienteN != null))
            {
                await new DesconectivoSubestacionesRepositorio(db).InsertaBreakerBaja(sub, red, codD, tensionN, corrienteN);
                return Json("true");
            }
            throw new ArgumentNullException();
        }
        #endregion
    }
}
