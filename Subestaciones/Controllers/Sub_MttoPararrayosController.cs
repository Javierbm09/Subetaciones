using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models;
using Subestaciones.Models.Clases;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class Sub_MttoPararrayosController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Sub_MttoPararrayos
        public async Task<ActionResult> Index()
        {
            return View(await new MttoPararrayosRepositorio(db).ObtenerListadoMP());
        }

        // GET: Sub_MttoPararrayos/Details/5
        public async Task<ActionResult> Details(short idEA, int idMtto)
        {
            if (idEA == null || idMtto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_MttoPararrayos sub_MttoPararrayos = await db.Sub_MttoPararrayos.FindAsync(idEA, idMtto);
            if (sub_MttoPararrayos == null)
            {
                return HttpNotFound();
            }

            MttoPararrayosRepositorio mttoPararrayosRepositorio = new MttoPararrayosRepositorio(db);
            ViewBag.ListadoSubestaciones = await mttoPararrayosRepositorio.ObtenerListadoSubestaciones(sub_MttoPararrayos.subestacion);
            ViewBag.TipoMantenimiento = await mttoPararrayosRepositorio.ObtenerListadoTipoMantenimiento(sub_MttoPararrayos.tipoMantenimiento);
            ViewBag.ListaRevisado = await mttoPararrayosRepositorio.ObtenerListadoRevisado(sub_MttoPararrayos.revisadoPor);
            ViewBag.TipoEquipoProtegido = mttoPararrayosRepositorio.ObtenerListadoTipoEquipoProtegido(sub_MttoPararrayos.TequipoProt);
            ViewBag.ListaEquiposProtegidos = await new MttoPararrayosRepositorio(db).ObtenerListadoEquipos(sub_MttoPararrayos.subestacion,
                sub_MttoPararrayos.TequipoProt, sub_MttoPararrayos.CodigoEquipoProtegido);
            ViewBag.ListaVoltajesInstalados = await new MttoPararrayosRepositorio(db).ObtenerListadoVoltajesInstalados(sub_MttoPararrayos.subestacion,
                sub_MttoPararrayos.VoltajeInstalado);

            ViewBag.ListaDefectosPorcelanas = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.porcelanas);
            ViewBag.ListaDefectosPlatillosMembranas = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.platillosMembranas);
            ViewBag.ListaDefectosConexiones = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.conexiones);
            ViewBag.ListaDefectosTornilleria = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.tornilleria);
            ViewBag.ListaDefectosAterramientos = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.aterramientos);
            ViewBag.ListaDefectosPartesMetalicas = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.partesMetalicas);
            ViewBag.ListaEstadoMiliA = mttoPararrayosRepositorio.ObtenerListadoEstados(sub_MttoPararrayos.EstadoMiliA);
            ViewBag.ListaEstadoCuentaOp = mttoPararrayosRepositorio.ObtenerListadoEstados(sub_MttoPararrayos.EstadoCuentaOp);

            Sub_MttoPararrayosModel sub_MttoPararrayosModel = new Sub_MttoPararrayosModel();
            sub_MttoPararrayosModel.MttoPararrayos = sub_MttoPararrayos;
            var listaInstr = await mttoPararrayosRepositorio.ObtenerListadoInstrumentosUtilizados(sub_MttoPararrayos.id_EAdministrativa, sub_MttoPararrayos.id_MttoPararrayo);
            sub_MttoPararrayosModel.InstrumentosUtilizados = listaInstr.Select(int.Parse).ToList();

            sub_MttoPararrayosModel.MttoPararrayos_FaseA = await mttoPararrayosRepositorio.ObtenerDatosFases(sub_MttoPararrayos.id_EAdministrativa, sub_MttoPararrayos.id_MttoPararrayo, "A");
            sub_MttoPararrayosModel.MttoPararrayos_FaseB = await mttoPararrayosRepositorio.ObtenerDatosFases(sub_MttoPararrayos.id_EAdministrativa, sub_MttoPararrayos.id_MttoPararrayo, "B");
            sub_MttoPararrayosModel.MttoPararrayos_FaseC = await mttoPararrayosRepositorio.ObtenerDatosFases(sub_MttoPararrayos.id_EAdministrativa, sub_MttoPararrayos.id_MttoPararrayo, "C");

            if (sub_MttoPararrayosModel.InstrumentosUtilizados != null && sub_MttoPararrayosModel.InstrumentosUtilizados.Count != 0)
            {
                var lista = await mttoPararrayosRepositorio.ObtenerListadoInstrumentosDiferencia(sub_MttoPararrayosModel.InstrumentosUtilizados);
                ViewBag.ListaA = lista;
                ViewBag.ListaB = await mttoPararrayosRepositorio.ObtenerListadoInstrumentosSeleccionados(sub_MttoPararrayosModel.InstrumentosUtilizados);
            }
            else
            {
                ViewBag.ListaA = await mttoPararrayosRepositorio.ObtenerListadoInstrumentos();
                ViewBag.ListaB = new SelectList(new List<ValueTextInt>(), "Value", "Text");
            }

            return View(sub_MttoPararrayosModel);
        }

        // GET: Sub_MttoPararrayos/Create
        public async Task<ActionResult> Create()
        {
            MttoPararrayosRepositorio mttoPararrayosRepositorio = new MttoPararrayosRepositorio(db);
            ViewBag.ListadoSubestaciones = await mttoPararrayosRepositorio.ObtenerListadoSubestaciones();
            ViewBag.TipoMantenimiento = await mttoPararrayosRepositorio.ObtenerListadoTipoMantenimiento();
            ViewBag.ListaRevisado = await mttoPararrayosRepositorio.ObtenerListadoRevisado();
            ViewBag.TipoEquipoProtegido = mttoPararrayosRepositorio.ObtenerListadoTipoEquipoProtegido();
            ViewBag.ListaEquiposProtegidos = new SelectList(new List<string>());
            ViewBag.ListaVoltajesInstalados = new SelectList(new List<string>());

            ViewBag.ListaDefectosPorcelanas = mttoPararrayosRepositorio.ObtenerListadoDefectos();
            ViewBag.ListaDefectosPlatillosMembranas = mttoPararrayosRepositorio.ObtenerListadoDefectos();
            ViewBag.ListaDefectosConexiones = mttoPararrayosRepositorio.ObtenerListadoDefectos();
            ViewBag.ListaDefectosTornilleria = mttoPararrayosRepositorio.ObtenerListadoDefectos();
            ViewBag.ListaDefectosAterramientos = mttoPararrayosRepositorio.ObtenerListadoDefectos();
            ViewBag.ListaDefectosPartesMetalicas = mttoPararrayosRepositorio.ObtenerListadoDefectos();
            ViewBag.ListaEstadoMiliA = mttoPararrayosRepositorio.ObtenerListadoEstados();
            ViewBag.ListaEstadoCuentaOp = mttoPararrayosRepositorio.ObtenerListadoEstados();

            ViewBag.ListaA = new SelectList(new List<ValueTextInt>(), "Value", "Text");
            ViewBag.ListaB = new SelectList(new List<ValueTextInt>(), "Value", "Text");
            return View();
        }

        // POST: Sub_MttoPararrayos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MttoPararrayos,MttoPararrayos_FaseA,MttoPararrayos_FaseB,MttoPararrayos_FaseC,InstrumentosUtilizados," +
            "LecturaFugaA,LecturaFugaB,LecturaFugaC,EstadoMiliA,EstadoCuentaOp,VoltajeInstalado,NumSerieFaseA,NumSerieFaseB,NumSerieFaseC,TipoParaFaseA,TipoParaFaseB,TipoParaFaseC")]
            Sub_MttoPararrayosModel sub_MttoPararrayos)
        {
            if (sub_MttoPararrayos.MttoPararrayos.Mantenido == null)
            {
                sub_MttoPararrayos.MttoPararrayos.Mantenido = false;
            }

            var repo = new Repositorio(db);
            var idEA = (short)repo.GetId_EAdministrativaUsuario();
            var numAcc = repo.GetNumAccion("I", "SMP", 0);

            sub_MttoPararrayos.MttoPararrayos.id_EAdministrativa = idEA;
            sub_MttoPararrayos.MttoPararrayos.numAccion = numAcc;

            sub_MttoPararrayos.MttoPararrayos_FaseA.Fase = "A";
            sub_MttoPararrayos.MttoPararrayos_FaseB.Fase = "B";
            sub_MttoPararrayos.MttoPararrayos_FaseC.Fase = "C";
            sub_MttoPararrayos.MttoPararrayos_FaseA.id_EAdministrativa = idEA;
            sub_MttoPararrayos.MttoPararrayos_FaseB.id_EAdministrativa = idEA;
            sub_MttoPararrayos.MttoPararrayos_FaseC.id_EAdministrativa = idEA;
            sub_MttoPararrayos.MttoPararrayos_FaseA.subestacion = sub_MttoPararrayos.MttoPararrayos.subestacion;
            sub_MttoPararrayos.MttoPararrayos_FaseB.subestacion = sub_MttoPararrayos.MttoPararrayos.subestacion;
            sub_MttoPararrayos.MttoPararrayos_FaseC.subestacion = sub_MttoPararrayos.MttoPararrayos.subestacion;
            sub_MttoPararrayos.MttoPararrayos_FaseA.id_MttoPararrayo = 0;
            sub_MttoPararrayos.MttoPararrayos_FaseB.id_MttoPararrayo = 0;
            sub_MttoPararrayos.MttoPararrayos_FaseC.id_MttoPararrayo = 0;

            MttoPararrayosRepositorio mttoPararrayosRepositorio = new MttoPararrayosRepositorio(db);
            ViewBag.ListadoSubestaciones = await mttoPararrayosRepositorio.ObtenerListadoSubestaciones(sub_MttoPararrayos.MttoPararrayos.subestacion);
            ViewBag.TipoMantenimiento = await mttoPararrayosRepositorio.ObtenerListadoTipoMantenimiento(sub_MttoPararrayos.MttoPararrayos.tipoMantenimiento);
            ViewBag.ListaRevisado = await mttoPararrayosRepositorio.ObtenerListadoRevisado(sub_MttoPararrayos.MttoPararrayos.revisadoPor);
            ViewBag.TipoEquipoProtegido = mttoPararrayosRepositorio.ObtenerListadoTipoEquipoProtegido(sub_MttoPararrayos.MttoPararrayos.TequipoProt);
            ViewBag.ListaEquiposProtegidos = await new MttoPararrayosRepositorio(db).ObtenerListadoEquipos(sub_MttoPararrayos.MttoPararrayos.subestacion,
                sub_MttoPararrayos.MttoPararrayos.TequipoProt, sub_MttoPararrayos.MttoPararrayos.CodigoEquipoProtegido);
            ViewBag.ListaVoltajesInstalados = await new MttoPararrayosRepositorio(db).ObtenerListadoVoltajesInstalados(sub_MttoPararrayos.MttoPararrayos.subestacion,
                sub_MttoPararrayos.MttoPararrayos.VoltajeInstalado);

            ViewBag.ListaDefectosPorcelanas = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.MttoPararrayos.porcelanas);
            ViewBag.ListaDefectosPlatillosMembranas = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.MttoPararrayos.platillosMembranas);
            ViewBag.ListaDefectosConexiones = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.MttoPararrayos.conexiones);
            ViewBag.ListaDefectosTornilleria = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.MttoPararrayos.tornilleria);
            ViewBag.ListaDefectosAterramientos = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.MttoPararrayos.aterramientos);
            ViewBag.ListaDefectosPartesMetalicas = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.MttoPararrayos.partesMetalicas);
            ViewBag.ListaEstadoMiliA = mttoPararrayosRepositorio.ObtenerListadoEstados(sub_MttoPararrayos.MttoPararrayos.EstadoMiliA);
            ViewBag.ListaEstadoCuentaOp = mttoPararrayosRepositorio.ObtenerListadoEstados(sub_MttoPararrayos.MttoPararrayos.EstadoCuentaOp);

            if (sub_MttoPararrayos.InstrumentosUtilizados != null && sub_MttoPararrayos.InstrumentosUtilizados.Count != 0)
            {
                var lista = await mttoPararrayosRepositorio.ObtenerListadoInstrumentosDiferencia(sub_MttoPararrayos.InstrumentosUtilizados);
                ViewBag.ListaA = lista;
                ViewBag.ListaB = await mttoPararrayosRepositorio.ObtenerListadoInstrumentosSeleccionados(sub_MttoPararrayos.InstrumentosUtilizados);
            }
            else
            {
                ViewBag.ListaA = await mttoPararrayosRepositorio.ObtenerListadoInstrumentos();
                ViewBag.ListaB = new SelectList(new List<ValueTextInt>(), "Value", "Text");
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                db.Sub_MttoPararrayos.Add(sub_MttoPararrayos.MttoPararrayos);
                await db.SaveChangesAsync();
                var temp = await db.Sub_MttoPararrayos.FindAsync(sub_MttoPararrayos.MttoPararrayos.id_EAdministrativa, sub_MttoPararrayos.MttoPararrayos.id_MttoPararrayo);
                if (sub_MttoPararrayos.InstrumentosUtilizados != null && sub_MttoPararrayos.InstrumentosUtilizados.Count > 0)
                {
                    foreach (var item in sub_MttoPararrayos.InstrumentosUtilizados)
                    {
                        db.Sub_MttoPararrayos_Instrumentos.Add(new Sub_MttoPararrayos_Instrumentos
                        {
                            id_EAdministrativa = idEA,
                            Id_Instrumento = item.ToString(),
                            id_MttoPararrayo = temp.id_MttoPararrayo
                        });
                    }
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sub_MttoPararrayos);
        }

        // GET: Sub_MttoPararrayos/Edit/5
        public async Task<ActionResult> Edit(short idEA, int idMtto)
        {
            if (idEA == null || idMtto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_MttoPararrayos sub_MttoPararrayos = await db.Sub_MttoPararrayos.FindAsync(idEA, idMtto);
            if (sub_MttoPararrayos == null)
            {
                return HttpNotFound();
            }

            if (sub_MttoPararrayos.Mantenido == null)
            {
                sub_MttoPararrayos.Mantenido = false;
            }
            
            MttoPararrayosRepositorio mttoPararrayosRepositorio = new MttoPararrayosRepositorio(db);
            ViewBag.ListadoSubestaciones = await mttoPararrayosRepositorio.ObtenerListadoSubestaciones(sub_MttoPararrayos.subestacion);
            ViewBag.TipoMantenimiento = await mttoPararrayosRepositorio.ObtenerListadoTipoMantenimiento(sub_MttoPararrayos.tipoMantenimiento);
            ViewBag.ListaRevisado = await mttoPararrayosRepositorio.ObtenerListadoRevisado(sub_MttoPararrayos.revisadoPor);
            ViewBag.TipoEquipoProtegido = mttoPararrayosRepositorio.ObtenerListadoTipoEquipoProtegido(sub_MttoPararrayos.TequipoProt);
            ViewBag.ListaEquiposProtegidos = await new MttoPararrayosRepositorio(db).ObtenerListadoEquipos(sub_MttoPararrayos.subestacion,
                sub_MttoPararrayos.TequipoProt, sub_MttoPararrayos.CodigoEquipoProtegido);
            ViewBag.ListaVoltajesInstalados = await new MttoPararrayosRepositorio(db).ObtenerListadoVoltajesInstalados(sub_MttoPararrayos.subestacion,
                sub_MttoPararrayos.VoltajeInstalado);

            ViewBag.ListaDefectosPorcelanas = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.porcelanas);
            ViewBag.ListaDefectosPlatillosMembranas = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.platillosMembranas);
            ViewBag.ListaDefectosConexiones = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.conexiones);
            ViewBag.ListaDefectosTornilleria = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.tornilleria);
            ViewBag.ListaDefectosAterramientos = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.aterramientos);
            ViewBag.ListaDefectosPartesMetalicas = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.partesMetalicas);
            ViewBag.ListaEstadoMiliA = mttoPararrayosRepositorio.ObtenerListadoEstados(sub_MttoPararrayos.EstadoMiliA);
            ViewBag.ListaEstadoCuentaOp = mttoPararrayosRepositorio.ObtenerListadoEstados(sub_MttoPararrayos.EstadoCuentaOp);

            Sub_MttoPararrayosModel sub_MttoPararrayosModel = new Sub_MttoPararrayosModel();
            sub_MttoPararrayosModel.MttoPararrayos = sub_MttoPararrayos;
            var listaInstr = await mttoPararrayosRepositorio.ObtenerListadoInstrumentosUtilizados(sub_MttoPararrayos.id_EAdministrativa, sub_MttoPararrayos.id_MttoPararrayo);
            sub_MttoPararrayosModel.InstrumentosUtilizados = listaInstr.Select(int.Parse).ToList();

            sub_MttoPararrayosModel.MttoPararrayos_FaseA = await mttoPararrayosRepositorio.ObtenerDatosFases(sub_MttoPararrayos.id_EAdministrativa, sub_MttoPararrayos.id_MttoPararrayo, "A");
            sub_MttoPararrayosModel.MttoPararrayos_FaseB = await mttoPararrayosRepositorio.ObtenerDatosFases(sub_MttoPararrayos.id_EAdministrativa, sub_MttoPararrayos.id_MttoPararrayo, "B");
            sub_MttoPararrayosModel.MttoPararrayos_FaseC = await mttoPararrayosRepositorio.ObtenerDatosFases(sub_MttoPararrayos.id_EAdministrativa, sub_MttoPararrayos.id_MttoPararrayo, "C");

            if (sub_MttoPararrayosModel.InstrumentosUtilizados != null && sub_MttoPararrayosModel.InstrumentosUtilizados.Count != 0)
            {
                var lista = await mttoPararrayosRepositorio.ObtenerListadoInstrumentosDiferencia(sub_MttoPararrayosModel.InstrumentosUtilizados);
                ViewBag.ListaA = lista;
                ViewBag.ListaB = await mttoPararrayosRepositorio.ObtenerListadoInstrumentosSeleccionados(sub_MttoPararrayosModel.InstrumentosUtilizados);
            }
            else
            {
                ViewBag.ListaA = await mttoPararrayosRepositorio.ObtenerListadoInstrumentos();
                ViewBag.ListaB = new SelectList(new List<ValueTextInt>(), "Value", "Text");
            }

            return View(sub_MttoPararrayosModel);
        }

        // POST: Sub_MttoPararrayos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MttoPararrayos,MttoPararrayos_FaseA,MttoPararrayos_FaseB,MttoPararrayos_FaseC,InstrumentosUtilizados," +
            "LecturaFugaA,LecturaFugaB,LecturaFugaC,EstadoMiliA,EstadoCuentaOp,VoltajeInstalado,NumSerieFaseA,NumSerieFaseB,NumSerieFaseC,TipoParaFaseA,TipoParaFaseB,TipoParaFaseC")]
            Sub_MttoPararrayosModel sub_MttoPararrayos)
        {
            var repo = new Repositorio(db);
            sub_MttoPararrayos.MttoPararrayos.id_EAdministrativa = (short)repo.GetId_EAdministrativaUsuario();
            sub_MttoPararrayos.MttoPararrayos.numAccion = repo.GetNumAccion("M", "SMP", sub_MttoPararrayos.MttoPararrayos.numAccion ?? 0);

            if (sub_MttoPararrayos.MttoPararrayos.Mantenido == null)
            {
                sub_MttoPararrayos.MttoPararrayos.Mantenido = false;
            }

            sub_MttoPararrayos.MttoPararrayos_FaseA.Fase = "A";
            sub_MttoPararrayos.MttoPararrayos_FaseB.Fase = "B";
            sub_MttoPararrayos.MttoPararrayos_FaseC.Fase = "C";
            sub_MttoPararrayos.MttoPararrayos_FaseA.id_EAdministrativa = sub_MttoPararrayos.MttoPararrayos.id_EAdministrativa;
            sub_MttoPararrayos.MttoPararrayos_FaseB.id_EAdministrativa = sub_MttoPararrayos.MttoPararrayos.id_EAdministrativa;
            sub_MttoPararrayos.MttoPararrayos_FaseC.id_EAdministrativa = sub_MttoPararrayos.MttoPararrayos.id_EAdministrativa;
            sub_MttoPararrayos.MttoPararrayos_FaseA.subestacion = sub_MttoPararrayos.MttoPararrayos.subestacion;
            sub_MttoPararrayos.MttoPararrayos_FaseB.subestacion = sub_MttoPararrayos.MttoPararrayos.subestacion;
            sub_MttoPararrayos.MttoPararrayos_FaseC.subestacion = sub_MttoPararrayos.MttoPararrayos.subestacion;
            sub_MttoPararrayos.MttoPararrayos_FaseA.id_MttoPararrayo = sub_MttoPararrayos.MttoPararrayos.id_MttoPararrayo;
            sub_MttoPararrayos.MttoPararrayos_FaseB.id_MttoPararrayo = sub_MttoPararrayos.MttoPararrayos.id_MttoPararrayo;
            sub_MttoPararrayos.MttoPararrayos_FaseC.id_MttoPararrayo = sub_MttoPararrayos.MttoPararrayos.id_MttoPararrayo;

            MttoPararrayosRepositorio mttoPararrayosRepositorio = new MttoPararrayosRepositorio(db);
            ViewBag.ListadoSubestaciones = await mttoPararrayosRepositorio.ObtenerListadoSubestaciones(sub_MttoPararrayos.MttoPararrayos.subestacion);
            ViewBag.TipoMantenimiento = await mttoPararrayosRepositorio.ObtenerListadoTipoMantenimiento(sub_MttoPararrayos.MttoPararrayos.tipoMantenimiento);
            ViewBag.ListaRevisado = await mttoPararrayosRepositorio.ObtenerListadoRevisado(sub_MttoPararrayos.MttoPararrayos.revisadoPor);
            ViewBag.TipoEquipoProtegido = mttoPararrayosRepositorio.ObtenerListadoTipoEquipoProtegido(sub_MttoPararrayos.MttoPararrayos.TequipoProt);
            ViewBag.ListaEquiposProtegidos = await new MttoPararrayosRepositorio(db).ObtenerListadoEquipos(sub_MttoPararrayos.MttoPararrayos.subestacion,
                sub_MttoPararrayos.MttoPararrayos.TequipoProt, sub_MttoPararrayos.MttoPararrayos.CodigoEquipoProtegido);
            ViewBag.ListaVoltajesInstalados = await new MttoPararrayosRepositorio(db).ObtenerListadoVoltajesInstalados(sub_MttoPararrayos.MttoPararrayos.subestacion,
                sub_MttoPararrayos.MttoPararrayos.VoltajeInstalado);

            ViewBag.ListaDefectosPorcelanas = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.MttoPararrayos.porcelanas);
            ViewBag.ListaDefectosPlatillosMembranas = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.MttoPararrayos.platillosMembranas);
            ViewBag.ListaDefectosConexiones = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.MttoPararrayos.conexiones);
            ViewBag.ListaDefectosTornilleria = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.MttoPararrayos.tornilleria);
            ViewBag.ListaDefectosAterramientos = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.MttoPararrayos.aterramientos);
            ViewBag.ListaDefectosPartesMetalicas = mttoPararrayosRepositorio.ObtenerListadoDefectos(sub_MttoPararrayos.MttoPararrayos.partesMetalicas);
            ViewBag.ListaEstadoMiliA = mttoPararrayosRepositorio.ObtenerListadoEstados(sub_MttoPararrayos.MttoPararrayos.EstadoMiliA);
            ViewBag.ListaEstadoCuentaOp = mttoPararrayosRepositorio.ObtenerListadoEstados(sub_MttoPararrayos.MttoPararrayos.EstadoCuentaOp);

            if (sub_MttoPararrayos.InstrumentosUtilizados != null && sub_MttoPararrayos.InstrumentosUtilizados.Count != 0)
            {
                var lista = await mttoPararrayosRepositorio.ObtenerListadoInstrumentosDiferencia(sub_MttoPararrayos.InstrumentosUtilizados);
                ViewBag.ListaA = lista;
                ViewBag.ListaB = await mttoPararrayosRepositorio.ObtenerListadoInstrumentosSeleccionados(sub_MttoPararrayos.InstrumentosUtilizados);
            }
            else
            {
                ViewBag.ListaA = await mttoPararrayosRepositorio.ObtenerListadoInstrumentos();
                ViewBag.ListaB = new SelectList(new List<ValueTextInt>(), "Value", "Text");
            }

            if (ModelState.IsValid)
            {
                db.Entry(sub_MttoPararrayos.MttoPararrayos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sub_MttoPararrayos);
        }

        // GET: Sub_MttoPararrayos/Delete/5
        public async Task<ActionResult> Delete(short idEA, int idMtto)
        {
            if (idEA == null || idMtto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_MttoPararrayos sub_MttoPararrayos = await db.Sub_MttoPararrayos.FindAsync(idEA, idMtto);
            if (sub_MttoPararrayos == null)
            {
                return HttpNotFound();
            }
            return View(sub_MttoPararrayos);
        }

        // POST: Sub_MttoPararrayos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short idEA, int idMtto)
        {
            Repositorio br = new Repositorio(db);
            Sub_MttoPararrayos mttoPararrayo = db.Sub_MttoPararrayos.Find(idEA, idMtto);
            int accion = br.GetNumAccion("B", "SMP", mttoPararrayo.numAccion ?? 0);
            db.Sub_MttoPararrayos.Remove(mttoPararrayo);
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

        [HttpPost]
        public async Task<ActionResult> ListadoMP()
        {
            return PartialView("_VPTablaMP", await new MttoPararrayosRepositorio(db).ObtenerListadoMP());
        }

        [HttpPost]
        public async Task<ActionResult> VPEquipos(string subestacion, string tipoEquipo)
        {
            ViewBag.ListaEquiposProtegidos = await new MttoPararrayosRepositorio(db).ObtenerListadoEquipos(subestacion, tipoEquipo);
            return PartialView("_VPEquipos");
        }

        [HttpPost]
        public ActionResult VPTipoEquipos()
        {
            ViewBag.TipoEquipoProtegido = new MttoPararrayosRepositorio(db).ObtenerListadoTipoEquipoProtegido();
            return PartialView("_VPTipoEquipos");
        }

        [HttpPost]
        public async Task<ActionResult> VPVoltajeInstalado(string subestacion)
        {
            ViewBag.ListaVoltajesInstalados = await new MttoPararrayosRepositorio(db).ObtenerListadoVoltajesInstalados(subestacion);
            return PartialView("_VPVoltajeInstalado");
        }

        [HttpPost]
        public async Task<ActionResult> VPInstrumentos(string subestacion)
        {
            ViewBag.ListaA = await new MttoPararrayosRepositorio(db).ObtenerListadoInstrumentos();
            ViewBag.ListaB = new SelectList(new List<ValueTextInt>(), "Value", "Text");
            return PartialView("_VPInstrumentos");
        }

        [HttpPost]
        public async Task<ActionResult> DatosFases(string tipo, string codigo)
        {
            MttoPararrayosRepositorio mttoPararrayosRepositorio = new MttoPararrayosRepositorio(db);
            var FaseA = await mttoPararrayosRepositorio.ObtenerListadoFases(tipo, codigo, "A");
            var FaseB = await mttoPararrayosRepositorio.ObtenerListadoFases(tipo, codigo, "B");
            var FaseC = await mttoPararrayosRepositorio.ObtenerListadoFases(tipo, codigo, "C");

            return Json(new
            {
                fans = FaseA != null ? FaseA.NumeroSerie : "",
                fatp = FaseA != null ? FaseA.TipoPararrayo : "",
                fbns = FaseB != null ? FaseB.NumeroSerie : "",
                fbtp = FaseB != null ? FaseB.TipoPararrayo : "",
                fcns = FaseC != null ? FaseC.NumeroSerie : "",
                fctp = FaseC != null ? FaseC.TipoPararrayo : ""
            },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Eliminar(short idEA, int idMtto)
        {
            try
            {
                Repositorio br = new Repositorio(db);
                Sub_MttoPararrayos mttoPararrayo = db.Sub_MttoPararrayos.Find(idEA, idMtto);
                int accion = br.GetNumAccion("B", "SMP", mttoPararrayo.numAccion ?? 0);
                db.Sub_MttoPararrayos.Remove(mttoPararrayo);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
