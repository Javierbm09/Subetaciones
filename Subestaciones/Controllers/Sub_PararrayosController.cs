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
using System.Globalization;
using System.Data.SqlClient;

namespace Subestaciones.Controllers
{
    public class Sub_PararrayosController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_Pararrayos
        public ActionResult Index(string inserta)
        {
            ViewBag.Inserto = inserta;

            var ListaPararrayos =  new  PararrayosRepositorio(db);
            return  View(ListaPararrayos.ObtenerListadoPara());
        }

        // GET: Sub_Pararrayos/Details/5
        public ActionResult Details(short EA, int id_para)
        {
            if (EA == 0 || id_para == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaPararrayos = new PararrayosRepositorio(db);
            var pararrayo = ListaPararrayos.Find(EA, id_para);
            if (EA == 0 || id_para == 0)
            {
                return HttpNotFound();
            }
            return View(pararrayo);
        }

        // GET: Sub_Pararrayos/Create
        [TienePermiso(Servicio: 30)]
        public async Task<ActionResult> Create()
        {
            var repo = new Repositorio(db);
            var repopara = new PararrayosRepositorio(db);
            ViewBag.TequipoProt = new SelectList(repopara.TipoEquipo(), "Value", "Text");
            ViewBag.Fase = new SelectList(repopara.Fase(), "Value", "Text");
            ViewBag.VoltajeInstalado = new SelectList(repopara.VoltajeInstalado(), "Value", "Text");
            ViewBag.Material = new SelectList(repopara.Material(), "Value", "Text");
            ViewBag.Frecuencia = new SelectList(repopara.Frecuencia(), "Value", "Text");
            ViewBag.Aislamiento = new SelectList(repopara.Aislamiento(), "Value", "Text");
            ViewBag.Clase = new SelectList(repopara.Clase(), "Value", "Text");
            ViewBag.Codigo = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.Id_CorrienteN = new SelectList(await repo.corriente(), "Id_CorrNomPararrayo", "CorrNomPararrayo");
            ViewBag.Id_Voltaje = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");
            return View();
        }

        // POST: Sub_Pararrayos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [TienePermiso(Servicio: 12)]//Servicio: crear pararrayos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id_EAdministrativa,Id_pararrayo,NumAccion,Codigo,NumeroSerie,TipoPararrayo,Fase,TequipoProt,CE,Aislamiento,Id_Voltaje,Id_CorrienteN,Fabricante,AñoFabricacion,MOCV,Inventario,Material,Clase,Instalado,Frecuencia,VoltajeInstalado,Estado,Levantado,Ubicacion,EstadoOperativo,Marca,PararrayoPrimario,PararrayoSecundario")] Sub_Pararrayos sub_Pararrayos)
        {
           
            var repopara = new PararrayosRepositorio(db);
            var repo = new Repositorio(db);
            var idEA = repo.GetId_EAdministrativaUsuario(); //esta EA ya esta bien
            var unionSub = repo.ObtenerUnionSub(sub_Pararrayos.Codigo);
            var id_para = db.Database.SqlQuery<int>(@"declare @numPara int
                Select @NumPara = Max(id_pararrayo) + 1
                From Sub_Pararrayos
                Where id_EAdministrativa = {0}
                if @numPara is null
                set @numpara = 1
                Select @numPara as idPararrayo", idEA);
            if (ModelState.IsValid)
            {
                sub_Pararrayos.Id_EAdministrativa = (short) idEA;
                sub_Pararrayos.Id_pararrayo = id_para.ToList().First();
                sub_Pararrayos.NumAccion = repo.GetNumAccion("I", "SUP", 0);
                if (unionSub != null)
                {
                    db.Sub_Pararrayos.Add(sub_Pararrayos);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { inserta = "si" });
                }
            }
            ViewBag.TequipoProt = new SelectList(repopara.TipoEquipo(), "Value", "Text");
            ViewBag.Fase = new SelectList(repopara.Fase(), "Value", "Text");
            ViewBag.VoltajeInstalado = new SelectList(repopara.VoltajeInstalado(), "Value", "Text");
            ViewBag.Material = new SelectList(repopara.Material(), "Value", "Text");
            ViewBag.Frecuencia = new SelectList(repopara.Frecuencia(), "Value", "Text");
            ViewBag.Aislamiento = new SelectList(repopara.Aislamiento(), "Value", "Text");
            ViewBag.Clase = new SelectList(repopara.Clase(), "Value", "Text");
            ViewBag.Codigo = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text");
            ViewBag.Id_CorrienteN = new SelectList(await repo.corriente(), "Id_CorrNomPararrayo", "CorrNomPararrayo");
            ViewBag.Id_Voltaje = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");
            return View(sub_Pararrayos);
        }

        // GET: Sub_Pararrayos/Edit/5
        [TienePermiso(Servicio: 12)]//Servicio: editar pararrayos
        public async Task<ActionResult> Edit(short EA, int id_para)
        {
            if (EA == 0 || id_para == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Pararrayos pararrayo = db.Sub_Pararrayos.Find(EA, id_para);
            var repo = new Repositorio(db);
            var repopara = new PararrayosRepositorio(db);
            ViewBag.TequipoProt = new SelectList( repopara.TipoEquipo(), "Value", "Text", pararrayo.TequipoProt);
            ViewBag.Fase = new SelectList(repopara.Fase(), "Value", "Text", pararrayo.Fase);
            ViewBag.Frecuencia = new SelectList(repopara.Frecuencia(), "Value", "Text", pararrayo.Frecuencia);
            ViewBag.Aislamiento = new SelectList(repopara.Aislamiento(), "Value", "Text", pararrayo.Aislamiento);
            ViewBag.Clase = new SelectList(repopara.Clase(), "Value", "Text", pararrayo.Clase);
            ViewBag.VoltajeInstalado = new SelectList(repopara.VoltajeInstalado(), "Value", "Text", pararrayo.VoltajeInstalado);
            ViewBag.Material = new SelectList(repopara.Material(), "Value", "Text", pararrayo.Material);
            ViewBag.Codigo = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text", pararrayo.Codigo);
            ViewBag.Id_CorrienteN = new SelectList(await repo.corriente(), "Id_CorrNomPararrayo", "CorrNomPararrayo", pararrayo.Id_CorrienteN);
            ViewBag.Id_Voltaje = new SelectList(await repo.voltajePararrayos(), "Id_VoltNomPararrayo", "VoltNomPararrayo", pararrayo.Id_Voltaje);
            CodigoEquipos(pararrayo.TequipoProt, pararrayo.Codigo, pararrayo.CE);
            return View(pararrayo);
        }
       
        // POST: Sub_Pararrayos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id_EAdministrativa,Id_pararrayo,NumAccion,Codigo,NumeroSerie,TipoPararrayo,Fase,TequipoProt,CE,Aislamiento,Id_Voltaje,Id_CorrienteN,Fabricante,AñoFabricacion,MOCV,Inventario,Material,Clase,Instalado,Frecuencia,VoltajeInstalado,Estado,Levantado,Ubicacion,EstadoOperativo,Marca,PararrayoPrimario,PararrayoSecundario")] Sub_Pararrayos sub_Pararrayos)
        {
            var repo = new Repositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario(); //esta EA ya esta bien
            Repositorio brep = new Repositorio(db);
            if (ModelState.IsValid)
            {
                //var eaAnterior = Request.Form["EAAnterior"].ToString();
                //var idAnterior = Request.Form["IdAnterior"].ToString();

                //await db.Database.ExecuteSqlCommandAsync("UPDATE Sub_Pararrayos SET Id_EAdministrativa = @Id_EAdministrativa, Id_pararrayo = @Id_pararrayo WHERE Id_EAdministrativa = @eaAnterior and Id_pararrayo = @idAnterior", new SqlParameter("@CodigoEA", sub_Pararrayos.Id_EAdministrativa), new SqlParameter("@CodigoPara", sub_Pararrayos.Id_pararrayo), new SqlParameter("@eaAnterior", eaAnterior), new SqlParameter("@idAnterior", idAnterior));
                sub_Pararrayos.Id_EAdministrativa = (int)Id_Eadministrativa;
                sub_Pararrayos.NumAccion = brep.GetNumAccion("M", "SUP", sub_Pararrayos.NumAccion ?? 0);
                db.Entry(sub_Pararrayos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { inserta = "si" });
            }
            Sub_Pararrayos pararrayo = db.Sub_Pararrayos.Find(sub_Pararrayos.Id_EAdministrativa, sub_Pararrayos.Id_pararrayo);
            var repopara = new PararrayosRepositorio(db);
            ViewBag.TequipoProt = new SelectList(repopara.TipoEquipo(), "Value", "Text", pararrayo.TequipoProt);
            ViewBag.Fase = new SelectList(repopara.Fase(), "Value", "Text", pararrayo.Fase);
            ViewBag.Frecuencia = new SelectList(repopara.Frecuencia(), "Value", "Text", pararrayo.Frecuencia);
            ViewBag.Aislamiento = new SelectList(repopara.Aislamiento(), "Value", "Text", pararrayo.Aislamiento);
            ViewBag.Clase = new SelectList(repopara.Clase(), "Value", "Text", pararrayo.Clase);
            ViewBag.VoltajeInstalado = new SelectList(repopara.VoltajeInstalado(), "Value", "Text", pararrayo.VoltajeInstalado);
            ViewBag.Material = new SelectList(repopara.Material(), "Value", "Text", pararrayo.Material);
            ViewBag.Codigo = new SelectList(repo.subs().Select(c => new { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }), "Value", "Text", pararrayo.Codigo);
            ViewBag.Id_CorrienteN = new SelectList(await repo.corriente(), "Id_CorrNomPararrayo", "CorrNomPararrayo", pararrayo.Id_CorrienteN);
            ViewBag.Id_Voltaje = new SelectList(await repo.voltajePararrayos(), "Id_VoltNomPararrayo", "VoltNomPararrayo", pararrayo.Id_Voltaje);
            CodigoEquipos(pararrayo.TequipoProt, pararrayo.Codigo, pararrayo.CE);
            return View(sub_Pararrayos);
        }

        // GET: Sub_Pararrayos/Delete/5
        [TienePermiso(Servicio: 30)]
        public async Task< ActionResult> Delete(short EA, int id_para)
        {
            if (EA == 0 || id_para == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ListaPararrayos = new PararrayosRepositorio(db);
            var pararrayo = ListaPararrayos.Find(EA, id_para);
            return View(pararrayo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [TienePermiso(Servicio: 12)]//Servicio: eliminar pararrayo
        public ActionResult DeleteConfirmed(short EA, int id_para)
        {
            Repositorio br = new Repositorio(db);
            Sub_Pararrayos elimina_para = db.Sub_Pararrayos.FindAsync(EA, id_para).Result;
            int accion = br.GetNumAccion("B", "SUP", elimina_para.NumAccion??0);
            db.Sub_Pararrayos.Remove(elimina_para);
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

        #region Vistas Parciales (VP) AJAX
        [AllowAnonymous]
        public ActionResult CargarEquipo(string tipoequipo, string codsub)
        {
            CodigoEquipos(tipoequipo, codsub);
            return PartialView("_VPInstalaciones");
        }
        #endregion

        public void CodigoEquipos(string tipoequipo, string codsub)
        {
            if (tipoequipo == "B")
            {
                var barras = (
                    from sb in db.Sub_Barra
                    join vs in db.VoltajesSistemas
                    on sb.ID_Voltaje equals vs.Id_VoltajeSistema
                    into tension from tensionBarra in tension.DefaultIfEmpty()
                    where sb.Subestacion==(codsub)
                    select new SelectListItem { Value = sb.codigo, Text = "Código: " + sb.codigo + " Voltaje: " + tensionBarra.Voltaje.ToString() }
                ).ToList();
                ViewBag.CE = new SelectList(barras, "Value", "Text");
            }
            else if (tipoequipo == "L")
            {
                var lineas =
                    db.Sub_LineasSubestacion
                    .Where(c => c.Subestacion == (codsub))
                    .Select(c => new SelectListItem { Value = c.Circuito, Text = c.Circuito }).ToList()
                    .Union(db.SubestacionesCabezasLineas
                    .Where(c => c.SubestacionTransmicion == (codsub))
                    .Select(c => new SelectListItem { Value = c.Codigolinea, Text = c.Codigolinea })
                    );
                ViewBag.CE = new SelectList(lineas, "Value", "Text");
            }
            else if (tipoequipo == "T")
            {
                var transformadores = db.TransformadoresTransmision
                    .Where(c => c.Codigo == (codsub))
                    .Select(c => new SelectListItem { Value = c.Nombre, Text = c.Nombre })
                    .Union(db.TransformadoresSubtransmision
                    .Where(c => c.Codigo == (codsub))
                    .Select(c => new SelectListItem { Value = c.Nombre, Text = c.Nombre })
                    );
                ViewBag.CE = new SelectList(transformadores, "Value", "Text");
            }
            else if (tipoequipo == "D")
            {
                var codigo = new SqlParameter("@cod", codsub);

                var desconectivo = db.Database.SqlQuery<InstalacionDesconectivos>(@"select * 
                                                                          from InstalacionDesconectivos secc 
                                                                          where (TipoSeccionalizador<4) and secc.Ubicadaen = @cod", codigo).ToList();
                ViewBag.CE = new SelectList(desconectivo, "Codigo", "Codigo");
            }
            else if (tipoequipo == "I")
            {
                var codigo = new SqlParameter("@cod", codsub);

                var interruptor = db.Database.SqlQuery<InstalacionDesconectivos>(@"select * 
                                                                          from InstalacionDesconectivos secc 
                                                                          where (TipoSeccionalizador>=4) and secc.Ubicadaen = @cod", codigo).ToList();
                ViewBag.CE = new SelectList(interruptor, "Codigo", "Codigo");
            }
            else if (tipoequipo == "TC")
            {
                var TC = db.ES_TransformadorCorriente
                    .Where(c => c.CodSub == (codsub))
                    .Select(c => new SelectListItem { Value = c.Nro_Serie, Text = c.Nro_Serie }).ToList();

                ViewBag.CE = new SelectList(TC, "Value", "Text");
            }
            else if (tipoequipo == "TP")
            {
                var TP = db.ES_TransformadorPotencial
                    .Where(c => c.CodSub ==(codsub))
                    .Select(c => new SelectListItem { Value = c.Nro_Serie, Text = c.Nro_Serie }).ToList();

                ViewBag.CE = new SelectList(TP, "Value", "Text");
            }
            else if (tipoequipo == "TUP")
            {
                var TUP = db.BancoTransformadores
                    .Where(c => c.Circuito == (codsub))
                    .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo }).ToList();

                ViewBag.CE = new SelectList(TUP, "Value", "Text");
            }
        }
        public void CodigoEquipos(string tipoequipo, string codsub, string llave) 
        {
            if (tipoequipo == "B")
            {
                var barras = (
                    from sb in db.Sub_Barra
                    join vs in db.VoltajesSistemas
                    on sb.ID_Voltaje equals vs.Id_VoltajeSistema
                    into tension
                    from tensionBarra in tension.DefaultIfEmpty()
                    where sb.Subestacion == (codsub)
                    select new SelectListItem { Value = sb.codigo, Text = "Código: " + sb.codigo + " Voltaje: " + tensionBarra.Voltaje.ToString() }
                ).ToList();
                ViewBag.CE = new SelectList(barras, "Value", "Text", llave);
            }
            else if (tipoequipo == "L")
            {
                var lineas =
                    db.Sub_LineasSubestacion
                    .Where(c => c.Subestacion == (codsub))
                    .Select(c => new SelectListItem { Value = c.Circuito, Text = c.Circuito }).ToList()
                    .Union(db.SubestacionesCabezasLineas
                    .Where(c => c.SubestacionTransmicion == (codsub))
                    .Select(c => new SelectListItem { Value = c.Codigolinea, Text = c.Codigolinea })
                    );

                ViewBag.CE = new SelectList(lineas, "Value", "Text", llave);
            }
            else if (tipoequipo == "T")
            {
                var transformadores = db.TransformadoresTransmision
                    .Where(c => c.Codigo == (codsub))
                    .Select(c => new SelectListItem { Value = c.Nombre, Text = c.Nombre })
                    .Union(db.TransformadoresSubtransmision
                    .Where(c => c.Codigo == (codsub))
                    .Select(c => new SelectListItem { Value = c.Nombre, Text = c.Nombre })
                    );
                ViewBag.CE = new SelectList(transformadores, "Value", "Text", llave);
            }
            else if (tipoequipo == "D")
            {
                var codigo = new SqlParameter("@cod", codsub);

                var desconectivo = db.Database.SqlQuery<InstalacionDesconectivos>(@"select * 
                                                                          from InstalacionDesconectivos secc 
                                                                          where (TipoSeccionalizador<4) and secc.Ubicadaen = @cod", codigo).ToList();
                ViewBag.CE = new SelectList(desconectivo, "Value", "Text", llave);
            }
            else if (tipoequipo == "I")
            {
                var codigo = new SqlParameter("@cod", codsub);

                var interruptor = db.Database.SqlQuery<InstalacionDesconectivos>(@"select * 
                                                                          from InstalacionDesconectivos secc 
                                                                          where (TipoSeccionalizador>=4) and secc.Ubicadaen = @cod", codigo).ToList();
                ViewBag.CE = new SelectList(interruptor, "Codigo", "Codigo", llave);
            }
            else if (tipoequipo == "TC")
            {
                var TC = db.ES_TransformadorCorriente
                   .Where(c => c.CodSub == (codsub))
                   .Select(c => new SelectListItem { Value = c.Nro_Serie, Text = c.Nro_Serie }).ToList();

                ViewBag.CE = new SelectList(TC, "Value", "Text", llave);
            }
            else if (tipoequipo == "TP")
            {
                var TP = db.ES_TransformadorPotencial
                     .Where(c => c.CodSub == (codsub))
                     .Select(c => new SelectListItem { Value = c.Nro_Serie, Text = c.Nro_Serie }).ToList();

                ViewBag.CE = new SelectList(TP, "Value", "Text", llave);
            }
            else if (tipoequipo == "TUP")
            {
                var TUP = db.BancoTransformadores
                    .Where(c => c.Circuito == (codsub))
                    .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo }).ToList();

                ViewBag.CE = new SelectList(TUP, "Value", "Text", llave);
            }
        }

        public ActionResult Eliminar(short EA, int id_para)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_Pararrayos pararrayo = db.Sub_Pararrayos.Find(EA, id_para);
                int accion = br.GetNumAccion("B", "SUP", pararrayo.NumAccion ?? 0);
                db.Sub_Pararrayos.Remove(pararrayo);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public  ActionResult ListadoPararrayos()
        {
            var ListaPara = new PararrayosRepositorio(db);
            return PartialView("_VPPararrayos", ListaPara.ObtenerListadoPara());
        }

        
    }

}

