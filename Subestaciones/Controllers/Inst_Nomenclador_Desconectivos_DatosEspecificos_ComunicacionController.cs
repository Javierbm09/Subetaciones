using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class Inst_Nomenclador_Desconectivos_DatosEspecificos_ComunicacionController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion
        public ActionResult Index()
        {
            var inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion = db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Include(i => i.Inst_Nomenclador_Desconectivos);
            return View(inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.ToList());
        }

        // GET: Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion/Details/5
        public ActionResult Details(int id, int tipo, string tipoD)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion = db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Find(id);

            ViewBag.nomID = new SelectList(db.Inst_Nomenclador_Desconectivos, "Id_Nomenclador", "Id_Nomenclador");

            ViewBag.tipoD = tipoD;
            ViewBag.idTipoD = 4;


            if ((inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion != null) && (inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Id_Nomenclador != null))
            {
                Inst_Nomenclador_Desconectivos d = db.Inst_Nomenclador_Desconectivos.Find(inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Id_Nomenclador);

                //if ((d.Descripcion == 8) || (d != null))
                if ((d.Descripcion == 8) || (d == null))
                {
                    ViewBag.tipoD = "Seccionalizador";
                }
                if (d != null)
                    ViewBag.idTipoD = d.Descripcion;

            }


            var nomDesc = (
              from sb in db.Inst_Nomenclador_Desconectivos_Modelo
              join nomDesconectivo in db.Inst_Nomenclador_Desconectivos on sb.Id_Modelo equals nomDesconectivo.Id_ModeloDesconectivo into modelos
              from modeloDesc in modelos.DefaultIfEmpty()
              where modeloDesc.Descripcion == tipo

              select new SelectListItem
              {
                  Value = sb.Id_Modelo.ToString(),
                  Text = sb.DescripcionModelo.ToString()

              }
            ).ToList();
            ViewBag.nom = new SelectList(nomDesc, "Value", "Text");

            var gabinete = (
                from sb in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TipoGabinete
                select new SelectListItem { Value = sb.Id_TipoGabinete.ToString(), Text = sb.DescripcionTipoGabinete.ToString() }
            ).ToList();
            var tension = (from t in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TensionInst
                           select new SelectListItem { Value = t.Id_TensionInst.ToString(), Text = t.DescripcionTensionInst.ToString() }
            ).ToList();
            var esquema = (
               from sb in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Esquema
               select new SelectListItem { Value = sb.Id_Esquema.ToString(), Text = sb.DescripcionEsquema.ToString() }
           ).ToList();
            var funcion = (from t in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Funcion
                           select new SelectListItem { Value = t.Id_Funcion.ToString(), Text = t.DescripcionFuncion.ToString() }
            ).ToList();
            var descLista = new DesconectivoSubestacionesRepositorio(db);

            ViewBag.tipoGab = new SelectList(gabinete, "Value", "Text");
            ViewBag.tensionGab = new SelectList(tension, "Value", "Text");
            ViewBag.e = new SelectList(esquema, "Value", "Text");
            ViewBag.f = new SelectList(funcion, "Value", "Text");
            ViewBag.auto = descLista.SiNo();
            var scada = (
               from sb in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_SCADA
               select new SelectListItem { Value = sb.Id_SCADA.ToString(), Text = sb.DescripcionSCADA.ToString() }
           ).ToList();
            var equipo = (from t in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_EquipoUtilizado
                          select new SelectListItem { Value = t.Id_EquipoUtilizado.ToString(), Text = t.DescripcionEquipoUtilizado.ToString() }
            ).ToList();

            ViewBag.listaScada = new SelectList(scada, "Value", "Text");
            ViewBag.equipo = new SelectList(equipo, "Value", "Text");
            ViewBag.telemed = descLista.SiNo();
            ViewBag.SNtelemando = descLista.SiNo();

            return View(inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion);
        }

        // GET: Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion/Create
        public ActionResult Create()
        {
            ViewBag.Id_Nomenclador = new SelectList(db.Inst_Nomenclador_Desconectivos, "Id_Nomenclador", "Id_Nomenclador");
            return View();
        }

        // POST: Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_DatosEspComun,Codigo,Id_Nomenclador,Id_Administrativa,Id_EAdministrativa_Prov,NumAccion,AnnoFabricacion,SerieInterruptor,NroEmpresa,NroInventario,SeriGabinete,Ubicacion,Altitud,Latitud,FechaInstalacion,Observacion,Telemedion,Telemando,SCADA,EquipoUtilizado,Marca,Modelo,LazoAutomatico,Id_Esquema,Id_Funcion,Id_TensionInstalacion,Id_Gabinete")] Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion)
        {
            if (ModelState.IsValid)
            {
                db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Add(inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Nomenclador = new SelectList(db.Inst_Nomenclador_Desconectivos, "Id_Nomenclador", "Id_Nomenclador", inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Id_Nomenclador);
            return View(inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion);
        }

        // GET: Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion/Edit/5
        public ActionResult Edit(int id, int tipo, string tipoD)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion = db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Find(id);

            ViewBag.nomID = new SelectList(db.Inst_Nomenclador_Desconectivos, "Id_Nomenclador", "Id_Nomenclador");

                ViewBag.tipoD = tipoD;
                ViewBag.idTipoD = 4;

            if ((inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion != null)&&(inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Id_Nomenclador!=null))
            { 
                Inst_Nomenclador_Desconectivos d = db.Inst_Nomenclador_Desconectivos.Find(inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Id_Nomenclador);

                //if ((d.Descripcion == 8) || (d != null))
                if ((d.Descripcion == 8) || (d == null))
                {
                    ViewBag.tipoD = "Seccionalizador";
                }
                if (d!=null)
                    ViewBag.idTipoD = d.Descripcion;
            }

            var nomDesc = (
                      from sb in db.Inst_Nomenclador_Desconectivos_Modelo
                      join nomDesconectivo in db.Inst_Nomenclador_Desconectivos on sb.Id_Modelo equals nomDesconectivo.Id_ModeloDesconectivo into modelos
                      from modeloDesc in modelos.DefaultIfEmpty()
                      where modeloDesc.Descripcion == tipo

                      select new SelectListItem
                      {
                          Value = sb.Id_Modelo.ToString(),
                          Text = sb.DescripcionModelo.ToString()
                      }
                ).ToList();
            ViewBag.nomModelo = new SelectList(nomDesc, "Value", "Text");

            var gabinete = (
                from sb in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TipoGabinete
                select new SelectListItem { Value = sb.Id_TipoGabinete.ToString(), Text = sb.DescripcionTipoGabinete.ToString() }
            ).ToList();
            var tension = (from t in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TensionInst
                           select new SelectListItem { Value = t.Id_TensionInst.ToString(), Text = t.DescripcionTensionInst.ToString() }
            ).ToList();
            var esquema = (
               from sb in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Esquema
               select new SelectListItem { Value = sb.Id_Esquema.ToString(), Text = sb.DescripcionEsquema.ToString() }
           ).ToList();
            var funcion = (from t in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Funcion
                           select new SelectListItem { Value = t.Id_Funcion.ToString(), Text = t.DescripcionFuncion.ToString() }
            ).ToList();
            var descLista = new DesconectivoSubestacionesRepositorio(db);

            ViewBag.tipoGab = new SelectList(gabinete, "Value", "Text");
            ViewBag.tensionGab = new SelectList(tension, "Value", "Text");
            ViewBag.e = new SelectList(esquema, "Value", "Text");
            ViewBag.f = new SelectList(funcion, "Value", "Text");
            ViewBag.auto = descLista.SiNo();
            var scada = (
               from sb in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_SCADA
               select new SelectListItem { Value = sb.Id_SCADA.ToString(), Text = sb.DescripcionSCADA.ToString() }
           ).ToList();
            var equipo = (from t in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_EquipoUtilizado
                          select new SelectListItem { Value = t.Id_EquipoUtilizado.ToString(), Text = t.DescripcionEquipoUtilizado.ToString() }
            ).ToList();

            ViewBag.listaScada = new SelectList(scada, "Value", "Text");
            ViewBag.equipo = new SelectList(equipo, "Value", "Text");
            ViewBag.telemed = descLista.SiNo();
            ViewBag.SNtelemando = descLista.SiNo();

            return View(inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion);
        }

        // POST: Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_DatosEspComun,Codigo,Id_Nomenclador,Id_Administrativa,Id_EAdministrativa_Prov,NumAccion,AnnoFabricacion,SerieInterruptor,NroEmpresa,NroInventario,SeriGabinete,Ubicacion,Altitud,Latitud,FechaInstalacion,Observacion,Telemedion,Telemando,SCADA,EquipoUtilizado,Marca,Modelo,LazoAutomatico,Id_Esquema,Id_Funcion,Id_TensionInstalacion,Id_Gabinete")] Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion)
        {
            if (ModelState.IsValid)
            {
                //inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Id_Nomenclador = 
                db.Entry(inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "InstalacionDesconectivos", new { inserta = "si" });

            }

            ViewBag.nomID = new SelectList(db.Inst_Nomenclador_Desconectivos, "Id_Nomenclador", "Id_Nomenclador");

            ViewBag.tipoD = ViewBag.tipoD;
            var nomDesc = (
                from sb in db.Inst_Nomenclador_Desconectivos_Modelo
                select new SelectListItem { Value = sb.Id_Modelo.ToString(), Text = sb.DescripcionModelo.ToString()
                }
            ).ToList();
            ViewBag.nom = new SelectList(nomDesc, "Value", "Text");

            if ((inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion != null) && (inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Id_Nomenclador != null))
            {
                Inst_Nomenclador_Desconectivos d = db.Inst_Nomenclador_Desconectivos.Find(inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Id_Nomenclador);

                if ((d.Descripcion == 8) || (d != null))
                {
                    ViewBag.tipoD = "Seccionalizador";

                }
            }

            var descLista = new DesconectivoSubestacionesRepositorio(db);
            //Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion desc = db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Find(id);

            var gabinete = (
                from sb in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TipoGabinete
                select new SelectListItem { Value = sb.Id_TipoGabinete.ToString(), Text = sb.DescripcionTipoGabinete.ToString() }
            ).ToList();
            var tension = (from t in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TensionInst
                           select new SelectListItem { Value = t.Id_TensionInst.ToString(), Text = t.DescripcionTensionInst.ToString() }
            ).ToList();
            var esquema = (
               from sb in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Esquema
               select new SelectListItem { Value = sb.Id_Esquema.ToString(), Text = sb.DescripcionEsquema.ToString() }
           ).ToList();
            var funcion = (from t in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Funcion
                           select new SelectListItem { Value = t.Id_Funcion.ToString(), Text = t.DescripcionFuncion.ToString() }
            ).ToList();

            ViewBag.tipoGab = new SelectList(gabinete, "Value", "Text");
            ViewBag.tensionGab = new SelectList(tension, "Value", "Text");
            ViewBag.e = new SelectList(esquema, "Value", "Text");
            ViewBag.f = new SelectList(funcion, "Value", "Text");
            ViewBag.auto = descLista.SiNo();
            var scada = (
               from sb in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_SCADA
               select new SelectListItem { Value = sb.Id_SCADA.ToString(), Text = sb.DescripcionSCADA.ToString() }
           ).ToList();
            var equipo = (from t in db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_EquipoUtilizado
                          select new SelectListItem { Value = t.Id_EquipoUtilizado.ToString(), Text = t.DescripcionEquipoUtilizado.ToString() }
            ).ToList();

            ViewBag.listaScada = new SelectList(scada, "Value", "Text");
            ViewBag.equipo = new SelectList(equipo, "Value", "Text");
            ViewBag.telemed = descLista.SiNo();
            ViewBag.SNtelemando = descLista.SiNo();
            return View(inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion);
        }

        [HttpPost]
        public ActionResult CambiaTipo(int id, int tipo)
        {//cuando se cambia el tipo de desconectivo se actualiza en la tabla Inst_Nomenclador_Desconectivos el valor descripcion
            //elimino el fusible
            Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion de = db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Find(id);
            Inst_Nomenclador_Desconectivos d = db.Inst_Nomenclador_Desconectivos.Find(de.Id_Nomenclador);

            d.Descripcion = tipo;
            db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();

            var tipoD = "Recerrador";
            if (d.Descripcion == 8)
            {
                tipoD = "Seccionalizador";
            }
            return RedirectToAction("Edit", "Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion", new { id = id, tipo =tipoD});
        }

        // GET: Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion = db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Find(id);
            if (inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion == null)
            {
                return HttpNotFound();
            }
            return View(inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion);
        }

        // POST: Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion = db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Find(id);
            db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Remove(inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion);
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

        #region Vista Parciales

        public ActionResult _VPDatosGenerales(int? modelo)
        {
            var descLista = new DesconectivoSubestacionesRepositorio(db);
            var nomDesc = (
                from sb in db.Inst_Nomenclador_Desconectivos_Modelo
                join nomDesconectivo in db.Inst_Nomenclador_Desconectivos on sb.Id_Modelo equals nomDesconectivo.Id_ModeloDesconectivo into modelos
                from modeloDesc in modelos.DefaultIfEmpty()
                where modeloDesc.Descripcion == modelo
                select new SelectListItem { Value = sb.Id_Modelo.ToString(), Text = sb.DescripcionModelo.ToString() }
            ).ToList();
            ViewBag.nom = new SelectList(nomDesc, "Value", "Text", modelo);
            return PartialView(descLista.FindNom(modelo));
        }

        public ActionResult _VPDetallesDatosGenerales(int? modelo, string tipoD, int idTipoD, string bandera)
        {
            var descLista = new DesconectivoSubestacionesRepositorio(db);
          
            ViewBag.tipo = tipoD;
            ViewBag.idTipo = idTipoD;
            ViewBag.detalles = bandera;
           
            ///aqui pasar todos los listados para definir modelo
            return PartialView(descLista.FindNom(modelo));
        }


        #endregion

        [HttpGet]
        public JsonResult ObtenerModelos()
        {
            var modelosDesconectivos = new DesconectivoSubestacionesRepositorio(db).ObtenerModelosDesconectivos();
            return Json(modelosDesconectivos, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ObtenerFabricantes()
        {
            var FabricantesModeloDesconectivos = new DesconectivoSubestacionesRepositorio(db).ObtenerFabricanteModelosDesconectivos();
            return Json(FabricantesModeloDesconectivos, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ObtenerTensionNominal()
        {
            var TensionModeloDesconectivos = new DesconectivoSubestacionesRepositorio(db).ObtenerTensionModelosDesconectivos();
            return Json(TensionModeloDesconectivos, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ObtenerCorrienteNominal()
        {
            var CorrienteNModeloDesconectivos = new DesconectivoSubestacionesRepositorio(db).ObtenerCorrienteNModelosDesconectivos();
            return Json(CorrienteNModeloDesconectivos, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ObtenerBil()
        {
            var BilModeloDesconectivos = new DesconectivoSubestacionesRepositorio(db).ObtenerBilModelosDesconectivos();
            return Json(BilModeloDesconectivos, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ObtenerICortoCircuito()
        {
            var ICortoModeloDesconectivos = new DesconectivoSubestacionesRepositorio(db).ObtenerICortoCircuitoModelosDesconectivos();
            return Json(ICortoModeloDesconectivos, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ObtenerIAperturaCable()
        {
            var IAperturaModeloDesconectivos = new DesconectivoSubestacionesRepositorio(db).ObtenerIAperturaCableModelosDesconectivos();
            return Json(IAperturaModeloDesconectivos, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ObtenerSecuenciaOperaciones()
        {
            var SecuenciaModeloDesconectivos = new DesconectivoSubestacionesRepositorio(db).ObtenerSecuenciaOperacionesModelosDesconectivos();
            return Json(SecuenciaModeloDesconectivos, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ObtenerExtincionArco()
        {
            var ExtArcoModeloDesconectivos = new DesconectivoSubestacionesRepositorio(db).ObtenerExtincionArcoModelosDesconectivos();
            return Json(ExtArcoModeloDesconectivos, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ObtenerAislamiento()
        {
            var AislamientoModeloDesconectivos = new DesconectivoSubestacionesRepositorio(db).ObtenerAislamientoModelosDesconectivos();
            return Json(AislamientoModeloDesconectivos, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ObtenerPresionGas()
        {
            var PresionGasModeloDesconectivos = new DesconectivoSubestacionesRepositorio(db).ObtenerPresionGasModelosDesconectivos();
            return Json(PresionGasModeloDesconectivos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerListaModelos(int tipoDesc)
        {
            var ListaModeloDesconectivos = new DesconectivoSubestacionesRepositorio(db).ListaModelos(tipoDesc);
            return Json(ListaModeloDesconectivos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertarModelo(int EA, int EAProv, int numA, int modelo,int descripcion, int? fab, int? tension, int? corriente, double? pGas, double? pInt, double? pGab, double? pTotal, int? bil, int? cortoCircuito, int? tanque, int? apertCable, int? secc, int? extArco, int? aislamiento, int? presionGas)
        {
            if ((EA != 0) && (modelo != 0))
            {
                new DesconectivoSubestacionesRepositorio(db).InsertarModelo(EA, EAProv, numA, modelo, descripcion, fab, tension, corriente, pGas, pInt, pGab, pTotal, bil, cortoCircuito, tanque, apertCable, secc, extArco, aislamiento, presionGas);
                return Json("true");
            }
            throw new ArgumentNullException();
        }

        [HttpPost]
        public ActionResult ActualizarModelo(int idnom, int EA, int EAProv, int numA, int modelo, int descripcion, int? fab, int? tension, int? corriente, double? pGas, double? pInt, double? pGab, double? pTotal, int? bil, int? cortoCircuito, int? tanque, int? apertCable, int? secc, int? extArco, int? aislamiento, int? presionGas)
        {
            if (idnom != 0)
            {
                try
                {
                    new DesconectivoSubestacionesRepositorio(db).ActualizarNomModelo(idnom, EA, EAProv, numA, modelo, descripcion, fab, tension, corriente, pGas, pInt, pGab, pTotal, bil, cortoCircuito, tanque, apertCable, secc, extArco, aislamiento, presionGas);
                    return Json("true");
                }
                catch (Exception e)
                {
                    throw e;

                    throw new HttpException((int)HttpStatusCode.NotFound, "Ocurrió un error al editar el termómetro.");
                }
            }
            throw new ArgumentNullException();
        }

        [HttpPost]
        public ActionResult EliminarModelo(int id_modelo)
        {
            if (id_modelo != 0)
            {
                try
                {
                    new DesconectivoSubestacionesRepositorio(db).EliminarModelo(id_modelo);
                    return Json("true");
                }
                catch (Exception e)
                {
                    throw e;
                    throw new HttpException(404, "this is a 404 error");
                }
            }
            throw new ArgumentNullException();
        }
    }
}
