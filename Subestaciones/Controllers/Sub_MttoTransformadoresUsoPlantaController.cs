using Subestaciones.Models;
using Subestaciones.Models.Clases;
using Subestaciones.Models.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Subestaciones.Controllers
{
    public class Sub_MttoTransformadoresUsoPlantaController : Controller
    {
        private DBContext db = new DBContext();

        [HttpGet]
        // GET: Sub_MttoTransformadoresUsoPlanta
        public async Task<ActionResult> Index()
        {
            var ListaMttos = new Sub_MttoTransformadoresUsoPlantaRepositorio(db);
            return View(await ListaMttos.ObtenerMttos());
        }


        [HttpGet]
        public ActionResult Create()
        {
            var repo = new Repositorio(db);
            var repoMtto = new Sub_MttoTransformadoresUsoPlantaRepositorio(db);
            

            //listados general
            ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().OrderBy(c => c.Codigo).ThenBy(c => c.NombreSubestacion).Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();
            

            //listado banco transformadores
            ViewBag.banco = new SelectList(db.BancoTransformadores.Where(c => c.Codigo == "").Select(c => new { Value = c.Codigo, Text = c.Circuito }), "Value", "Text");

            //listado Numero Empresa
            ViewBag.numEmpresa = new SelectList(db.Transformadores.Where(c => c.Codigo == "").Select(c => new { Value = c.Id_Transformador, Text = c.Numemp }), "Value", "Text");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sub_MttoTransformadoresUsoPlantaViewModel sMttoTUP)
        {
            var repo = new Repositorio(db);
            var repoMtto = new Sub_MttoTransformadoresUsoPlantaRepositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();
            sMttoTUP.sub_MttoTransUsoP.Id_Eadministrativa = Id_Eadministrativa;


            if (ModelState.IsValid)
            {

                if (ValidarExisteMttoTUP(sMttoTUP.sub_MttoTransUsoP.CodigoSub, sMttoTUP.sub_MttoTransUsoP.Id_EATransformador, sMttoTUP.sub_MttoTransUsoP.Id_Transformador))
                {
                    ModelState.AddModelError("sub_MttoTransUsoP.MensajeExistenciaMtto", "Ya existe un mantenimiento para este transformador de uso planta.");
                }
                else
                {

                    sMttoTUP.sub_MttoTransUsoP.Numaccion = repo.GetNumAccion("I", "SSP", 0);

                    db.Sub_MttoTransUsoP.Add(sMttoTUP.sub_MttoTransUsoP);
                    if (sMttoTUP.sub_MttoTransUsoP.Mantenido == null)
                    {
                        sMttoTUP.sub_MttoTransUsoP.Mantenido = false;
                    }
                    db.SaveChanges();
                    return RedirectToAction("ComponetesMttoTrasformadoresUsoPlanta", new
                    {
                        CodigoSub = sMttoTUP.sub_MttoTransUsoP.CodigoSub,
                        Id_EATransformador = sMttoTUP.sub_MttoTransUsoP.Id_EATransformador,
                        Id_Transformador = sMttoTUP.sub_MttoTransUsoP.Id_Transformador,
                    });
                }
            }

            //listados general
            ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().OrderBy(c => c.Codigo).ThenBy(c => c.NombreSubestacion).Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();

            //listado banco transformadores
            ViewBag.banco = new SelectList(db.BancoTransformadores.Where(c => c.Codigo == sMttoTUP.sub_MttoTransUsoP.CodigoSub).Select(c => new { Value = c.Codigo, Text = c.Circuito }), "Value", "Text");

            //listado Numero Empresa
            ViewBag.numEmpresa = new SelectList(db.Transformadores.Where(c => c.Codigo == "").Select(c => new { Value = c.Id_Transformador, Text = c.Numemp }), "Value", "Text");

            

            return View(sMttoTUP);

        }

        private bool ValidarExisteMttoTUP(string codigoSub, short id_EATransformador, int id_Transformador)
        {
            var parametroidTUP = new SqlParameter("@CodigoSub", codigoSub);
            var parametroidEATUP = new SqlParameter("@Id_EATransformador", id_EATransformador);
            var parametroidTrans = new SqlParameter("@Id_Transformador", id_Transformador);

            var cant = db.Database.SqlQuery<Sub_MttoTransUsoP>(@"SELECT  *
                                                   FROM    Sub_MttoTransUsoP
                                                   WHERE   CodigoSub = @CodigoSub
                                                   AND Id_EATransformador = @Id_EATransformador
                                                   AND Id_Transformador = @Id_Transformador", parametroidTUP, parametroidEATUP, parametroidTrans).ToList();
            return !(cant.Count == 0);
        }


        public ActionResult ComponetesMttoTrasformadoresUsoPlanta(string CodigoSub, short Id_EATransformador, int Id_Transformador)
        {
            var repo = new Repositorio(db);
            var repoMtto = new Sub_MttoTransformadoresUsoPlantaRepositorio(db);

            Sub_MttoTransformadoresUsoPlantaViewModel dMttoTUPVM = new Sub_MttoTransformadoresUsoPlantaViewModel();

            Sub_MttoTransUsoP dmtto = db.Sub_MttoTransUsoP.Where(c => c.CodigoSub == CodigoSub && c.Id_EATransformador == Id_EATransformador && c.Id_Transformador == Id_Transformador).First();

            if (dmtto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dMttoTUPVM.sub_MttoTransUsoP = dmtto;

            ViewBag.ListaSub = new SelectList(db.SubestacionesTransmision.ToList().OrderBy(c => c.Codigo).ThenBy(c => c.NombreSubestacion).Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.TiposMtto = repoMtto.tipoMtto();
            ViewBag.personal = repo.RealizadoPor();
            ViewBag.estadoTanqueExpansion = repoMtto.EstadoTanqueExpansion();
            ViewBag.estadoIndNivelaceite = repoMtto.EstadoIndNivelaceite();
            ViewBag.nivelAceite = repoMtto.Nivelaceite();
            ViewBag.aterramientoTanque = repoMtto.AterramientoTanque();
            ViewBag.saliderosResumi = repoMtto.SaliderosResumideros();
            ViewBag.incrementoMedicion = repoMtto.ObtenerIncremto(2);
            ViewBag.incrementoMedicionRigidez = repoMtto.ObtenerIncremto(7);
            ViewBag.normaRigidezDielectrica = repoMtto.NormaRigidezDielectrica();

            //listado banco transformadores
            ViewBag.banco = repoMtto.ObtenerBanco(Id_EATransformador, Id_Transformador);
            ViewBag.nombreSub = repoMtto.ObtenerNombreSub(CodigoSub); 


            if (dMttoTUPVM.sub_MttoTransUsoP.Mantenido == null)
            {
                dMttoTUPVM.sub_MttoTransUsoP.Mantenido = false;
            }

            return View(dMttoTUPVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComponentesExtras(Sub_MttoTransformadoresUsoPlantaViewModel sMttoTUP)
        {

            sMttoTUP.sub_MttoTransUsoP.CodigoSub = sMttoTUP.sub_MttoTransUsoP.CodigoSub;

            if (ModelState.IsValid)
            {

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("ComponetesMttoTrasformadoresUsoPlanta", new
            {
                CodigoSub = sMttoTUP.sub_MttoTransUsoP.CodigoSub,
                Id_EATransformador = sMttoTUP.sub_MttoTransUsoP.Id_EATransformador,
                Id_Transformador = sMttoTUP.sub_MttoTransUsoP.Id_Transformador,
            });

        }

        public ActionResult Cargar_transformador_usoPlanta(string codSubTrans)
        {
            var repoMtto = new Sub_MttoTransformadoresUsoPlantaRepositorio(db);
            ViewBag.banco = new SelectList(repoMtto.listaBanco(codSubTrans).Select(c => new { Value = c.Codigo, Text = c.Codigo }), "Value", "Text");

            return PartialView("_VPBancoTransformadoresSelect");
        }

        public JsonResult Cargar_NombreSub(string codSubTrans)
        {
            var repoMtto = new Sub_MttoTransformadoresUsoPlantaRepositorio(db);
            return Json(repoMtto.cargarNombreSub(codSubTrans), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cargar_NumEmpresa(string codigoBanco)
        {
            var repoMtto = new Sub_MttoTransformadoresUsoPlantaRepositorio(db);
            ViewBag.numEmpresa = new SelectList(repoMtto.NumEmp(codigoBanco).Select(c => new { Value = c.Numemp, Text = c.Numemp }), "Value", "Text");
            return PartialView("_VPNumEmpresaSelect");
        }

        public JsonResult Cargar_datos_MttoTUP(string numEmpresa)
        {
            var repoMtto = new Sub_MttoTransformadoresUsoPlantaRepositorio(db);
            return Json(repoMtto.datosMttoTUP(numEmpresa), JsonRequestBehavior.AllowGet);
        }
    }
}