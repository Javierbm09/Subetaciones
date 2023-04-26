using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models.Repositorio;
using Subestaciones.Models;
using System.Threading.Tasks;
using Subestaciones.Models.Clases;
using System.IO;
using System.Data.SqlClient;

namespace Subestaciones.Controllers
{
    public class Sub_TermografiasController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_Termografias
        [TienePermiso(21)]// verifico que tenga permiso ver las termografias
        public async Task<ActionResult> Index()
        {
            var ListaTerm = new TermografiasRepo(db);
            return View(await ListaTerm.ObtenerTermografias());
        }

        // GET: Sub_Termografias/Details/5
        [TienePermiso(21)]// verifico que tenga permiso ver las termografias
        public ActionResult Details(int EA, int numAccion)
        {

            TermografiasViewModel Termografs = new TermografiasViewModel();

            Termografs.termografias = db.Sub_Termografias.Find(EA, numAccion);

            if (Termografs.termografias == null)
            {
                ViewBag.mensaje = "No existen datos de termografías.";
                return View("~/Views/Shared/Error.cshtml");
            }

            Termografs.ptosCalientes = db.Sub_PuntoTermografia.Where(s => s.Id_EAdministrativa == EA && s.NumAccion == numAccion).ToList();

            var repo = new Repositorio(db);
            ViewBag.PtosCount = Termografs.ptosCalientes.Count;
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();
            return View(Termografs);
        }

        // GET: Sub_Termografias/Create
        [TienePermiso(35)]// verifico que tenga permiso de crear y eliminar termografia
        public ActionResult Create()
        {
            var repo = new Repositorio(db);
            var repoT = new TermografiasRepo(db);
            ViewBag.PtosCount = 0;
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.ListaE = new SelectList(repoT.elementos("E").Select(e => new { Value = e.Id_Elemento, Text = e.Elemento }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();
            return View();
        }

        // POST: Sub_Termografias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TermografiasViewModel termo)
        {
            var repo = new Repositorio(db);

            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario(); //esta EA ya esta bien

            if (ModelState.IsValid)
            {
                if (ValidarExisteTermografiaEnMes(termo.termografias.Subestacion, termo.termografias.fecha))
                {
                    ModelState.AddModelError("termografias.fecha", "Ya se realizó una termografía en este mes en la subestación.");
                }
                else
                {
                    HttpPostedFileBase file = Request.Files["ImageData"];

                    HttpPostedFileBase file1 = Request.Files["ImageDataT"];

                    termo.termografias.Id_EAdministrativa = (short)Id_Eadministrativa;

                    termo.termografias.NumAccion = repo.GetNumAccion("I", "SUT", 0);

                    if (file != null)
                        termo.termografias.Imagen = ConvertToBytes(file);

                    if (file1 != null)
                        termo.termografias.ImagenTermografica = ConvertToBytes(file1);

                    db.Sub_Termografias.Add(termo.termografias);

                    if (termo.ptosCalientes != null)
                    {
                        var id_pto = db.Database.SqlQuery<int>(@"declare @pto int
                                    Select @pto = Max(Punto) + 1
                                    From Sub_PuntoTermografia
                                    Where Id_EAdministrativa = {0} and NumAccion = {1}
                                    if @pto is null
                                    set @pto = 1
                                    Select @pto as idpto", termo.termografias.Id_EAdministrativa, termo.termografias.NumAccion).First();
                        foreach (var item in termo.ptosCalientes)
                        {
                            item.Id_EAdministrativa = termo.termografias.Id_EAdministrativa;
                            item.NumAccion = termo.termografias.NumAccion;
                            item.Punto = id_pto++;
                            item.fecha = termo.termografias.fecha;

                            if ((item.TempDetectada > 0) && (item.TempDetectada <= 10))
                                item.estado = 55;
                            else if ((item.TempDetectada > 10) && (item.TempDetectada <= 20))
                                item.estado = 267;
                            else if ((item.TempDetectada > 20) && (item.TempDetectada <= 30))
                                item.estado = 268;
                            else if ((item.TempDetectada > 30) && (item.TempDetectada <= 40))
                                item.estado = 269;
                            else if ((item.TempDetectada > 40) && (item.TempDetectada <= 50))
                                item.estado = 270;
                            else if ((item.TempDetectada > 50) && (item.TempDetectada <= 60))
                                item.estado = 271;
                            else if ((item.TempDetectada > 60) && (item.TempDetectada <= 70))
                                item.estado = 272;
                            else if (item.TempDetectada > 70)
                                item.estado = 273;

                            db.Sub_PuntoTermografia.Add(item);
                        }
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            var repoT = new TermografiasRepo(db);

            ViewBag.PtosCount = termo?.ptosCalientes?.Count ?? 0;
            ViewBag.ListaE = new SelectList(repoT.elementos().Select(e => new { Value = e.Id_Elemento, Text = e.Elemento }), "Value", "Text");
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();
            return View(termo);
        }

        public ActionResult Reporte(int EA, int numAccion)
        {

            TermografiasViewModel Termografs = new TermografiasViewModel();


            Termografs.termografias = db.Sub_Termografias.Find(EA, numAccion);

            if (Termografs.termografias == null)
            {
                ViewBag.mensaje = "No existen datos de termografías.";
                return View("~/Views/Shared/Error.cshtml");
            }
            Termografs.ptosCalientes = db.Sub_PuntoTermografia.Where(s => s.Id_EAdministrativa == EA && s.NumAccion == numAccion).ToList();

            var repo = new Repositorio(db);
            ViewBag.PtosCount = Termografs.ptosCalientes.Count;
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();
            return View(Termografs);
        }


        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        private bool ValidarExisteTermografiaEnMes(string Codigo, DateTime fecha)
        {

            var parametromes = new SqlParameter("@mes", fecha.Month);
            var parametroanho = new SqlParameter("@anho", fecha.Year);
            var parametrocod = new SqlParameter("@sub", Codigo);

            var cant = db.Database.SqlQuery<Sub_Termografias>(@"SELECT  *
                                                   FROM    Sub_Termografias
                                                   WHERE   Subestacion =@sub
                                                   AND MONTH(fecha) =@mes
                                                   AND YEAR(fecha) =@anho", parametrocod, parametromes, parametroanho).ToList();
            return !(cant.Count == 0);

        }

        // GET: Sub_Termografias/Edit/5
        public ActionResult Edit(int EA, int numAccion)
        {
            TermografiasViewModel Termografs = new TermografiasViewModel();

            Termografs.termografias = db.Sub_Termografias.Find(EA, numAccion);

            if (Termografs.termografias == null)
            {
                ViewBag.mensaje = "No existen datos de termografías.";
                return View("~/Views/Shared/Error.cshtml");
            }

            Termografs.ptosCalientes = db.Sub_PuntoTermografia.Where(s => s.Id_EAdministrativa == EA && s.NumAccion == numAccion).ToList();

            var repo = new Repositorio(db);
            ViewBag.PtosCount = Termografs.ptosCalientes.Count;
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();
            return View(Termografs);
        }

        // POST: Sub_Termografias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TermografiasViewModel termo)
        {
            Repositorio repo = new Repositorio(db);

            var repoSub = new SubDistribucionRepositorio(db);

            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();

            if (ModelState.IsValid)
            {
                termo.termografias.Id_EAdministrativa = (short)Id_Eadministrativa;

                repo.GetNumAccion("M", "SUT", termo.termografias.NumAccion);

                HttpPostedFileBase file = Request.Files["ImageData"];

                HttpPostedFileBase file1 = Request.Files["ImageDataT"];

                if (file.FileName != "")
                {
                    termo.termografias.Imagen = ConvertToBytes(file);
                }
                else
                {
                    var imagenReal = from imgR in db.Sub_Termografias
                                     where imgR.Id_EAdministrativa == (short)Id_Eadministrativa && imgR.NumAccion == termo.termografias.NumAccion
                                     select imgR.Imagen;

                    termo.termografias.Imagen = imagenReal.First();
                }

                if (file1.FileName != "")
                {
                    termo.termografias.ImagenTermografica = ConvertToBytes(file1);
                }
                else
                {
                    var imagenTer = from imgT in db.Sub_Termografias
                                     where imgT.Id_EAdministrativa == (short)Id_Eadministrativa && imgT.NumAccion == termo.termografias.NumAccion
                                     select imgT.ImagenTermografica;

                    termo.termografias.ImagenTermografica = imagenTer.First();
                }

                db.Entry(termo.termografias).State = EntityState.Modified;

                if (termo.ptosCalientes != null)
                {
                    foreach (Sub_PuntoTermografia item in termo.ptosCalientes)
                    {
                        if (db.Sub_PuntoTermografia.Any(b => b.Punto == item.Punto && b.Id_EAdministrativa == item.Id_EAdministrativa && b.NumAccion == item.NumAccion))
                        {
                            var ptoCaliente = db.Sub_PuntoTermografia.Find(item.Punto, item.Id_EAdministrativa, item.NumAccion);
                            ptoCaliente.elemennto = item.elemennto;
                            ptoCaliente.descrpPtoCaleinte = item.descrpPtoCaleinte;
                            ptoCaliente.Fase = item.Fase;
                            ptoCaliente.TempDetectada = item.TempDetectada;
                            ptoCaliente.delta = item.delta;
                            db.Entry(ptoCaliente).State = EntityState.Modified;
                        }
                        else
                        {
                            var id_pto = db.Database.SqlQuery<int>(@"declare @pto int
                                    Select @pto = Max(Punto) + 1
                                    From Sub_PuntoTermografia
                                    Where Id_EAdministrativa = {0} and NumAccion = {1}
                                    if @pto is null
                                    set @pto = 1
                                    Select @pto as idpto", termo.termografias.Id_EAdministrativa, termo.termografias.NumAccion).First();
                            item.Id_EAdministrativa = termo.termografias.Id_EAdministrativa;
                            item.NumAccion = termo.termografias.NumAccion;
                            item.Punto = id_pto;
                            item.fecha = termo.termografias.fecha;
                            if ((item.TempDetectada > 0) && (item.TempDetectada <= 10))
                                item.estado = 55;
                            else if ((item.TempDetectada > 10) && (item.TempDetectada <= 20))
                                item.estado = 267;
                            else if ((item.TempDetectada > 20) && (item.TempDetectada <= 30))
                                item.estado = 268;
                            else if ((item.TempDetectada > 30) && (item.TempDetectada <= 40))
                                item.estado = 269;
                            else if ((item.TempDetectada > 40) && (item.TempDetectada <= 50))
                                item.estado = 270;
                            else if ((item.TempDetectada > 50) && (item.TempDetectada <= 60))
                                item.estado = 271;
                            else if ((item.TempDetectada > 60) && (item.TempDetectada <= 70))
                                item.estado = 272;
                            else if (item.TempDetectada > 70)
                                item.estado = 273;
                            db.Sub_PuntoTermografia.Add(item);
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PtosCount = termo.ptosCalientes.Count;
            ViewBag.ListaSub = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.personal = repo.RealizadoPor();
            return View(termo);
        }

        // GET: Sub_Termografias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Termografias sub_Termografias = db.Sub_Termografias.Find(id);
            if (sub_Termografias == null)
            {
                return HttpNotFound();
            }
            return View(sub_Termografias);
        }

        // POST: Sub_Termografias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sub_Termografias sub_Termografias = db.Sub_Termografias.Find(id);
            db.Sub_Termografias.Remove(sub_Termografias);
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

        public ActionResult Eliminar(int EA, int numAccion)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_Termografias t = db.Sub_Termografias.Find(EA, numAccion);
                db.Sub_Termografias.Remove(t);
                int accion = br.GetNumAccion("B", "SUT", t.NumAccion);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult EliminarPto(int punto, int EA, int numAccion)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_PuntoTermografia t = db.Sub_PuntoTermografia.Find(punto, EA, numAccion);
                db.Sub_PuntoTermografia.Remove(t);
                int accion = br.GetNumAccion("B", "SUT", t.NumAccion);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ObtenerElementos(string sub)
        {
            var repo = new TermografiasRepo(db);
            return Json(new SelectList(repo.elementos(sub).Select(e => new { Value = e.Id_Elemento, Text = e.Elemento }), "Value", "Text"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerFase()
        {
            var repo = new TermografiasRepo(db);
            return Json(repo.ObtenerFaseTermografia(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> ListadoTermografias()
        {
            var ListaTermog = new TermografiasRepo(db);
            return PartialView("_VPTermografias", await ListaTermog.ObtenerTermografias());
        }

        [HttpPost]
        public async Task<ActionResult> _VPnts(string codigo)
        {
            var ListaDefectos = new DefectoRepositorio(db);
            return PartialView("_VPnts", await ListaDefectos.ObtenerDefectosPendienteSubs(codigo));
        }
    }
}
