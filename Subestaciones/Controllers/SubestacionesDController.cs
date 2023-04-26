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
using Subestaciones.Models.Clases;
using Subestaciones.Models.Repositorio;
using System.Web.Script.Serialization;
using System.IO;
using System.Collections;
using Subestaciones.Models.Mostrar;


namespace Subestaciones.Controllers
{
    public class SubestacionesDController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Subestaciones
        public async Task<ActionResult> Index()
        {
            var ListaSub = new SubDistribucionRepositorio(db);
            return View(await ListaSub.ObtenerListadoSubD());
        }

        // GET: Subestaciones/Details/5
        public ActionResult Details(string codigo)
        {

            Subestaciones.Models.Clases.SubestacionesDistViewModel DSubD = new SubestacionesDistViewModel();
            if (codigo == null || codigo == "")
            {
                DSubD.DSubestaciones = db.Subestaciones.FirstOrDefault();
                if (DSubD.DSubestaciones != null)
                {
                    codigo = DSubD.DSubestaciones.Codigo;
                }
                else
                {
                    ViewBag.mensaje = "No existen datos de subestaciones.";
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            else
            {
                DSubD.DSubestaciones = db.Subestaciones.Find(codigo);
            }
            if (DSubD.DSubestaciones == null)
            {
                ViewBag.mensaje = "No existen datos de subestaciones.";
                return View("~/Views/Shared/Error.cshtml");
            }
            var listasub = db.Subestaciones.ToList().Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " " + c.NombreSubestacion });
            ViewBag.listaSubestaciones = new SelectList(listasub, "Value", "Text", codigo);
            var repo = new Repositorio(db);
            var repoSub = new SubDistribucionRepositorio(db);
            var repodefectos = new DefectoRepositorio(db);

            var _arbol = new List<TreeViewNode>();
            var _arbol_hijos = new List<TreeViewNode>();

            GenerarArbolesJerarquias(codigo, codigo, out _arbol, out _arbol_hijos);

            ViewBag.Json = new JavaScriptSerializer().Serialize(_arbol);
            ViewBag.JerarquiaHijo_Json = new JavaScriptSerializer().Serialize(_arbol_hijos);
            ViewBag.mes = repodefectos.ObtenerMes();
            var listaEnteros = repodefectos.ObtenerAnno();
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (var item in listaEnteros)
            {
                SelectListItem slI = new SelectListItem
                {
                    Value = item.ToString(),
                    Text = item.ToString()
                };

                lista.Add(slI);
            }
            ViewBag.anho = lista;

            return View(DSubD);
            //return View();
        }

        // GET: Subestaciones/Create
        [TienePermiso(25)]// verifico que tenga permiso de crear y eliminar subestaciones
        public async Task<ActionResult> Create()
        {
            var repo = new Repositorio(db);
            var repoSub = new SubDistribucionRepositorio(db);

            ViewBag.LineasCount = 0;
            ViewBag.BloquesCount = 0;
            ViewBag.Sucursales = repo.ObtenerSucursales();
            ViewBag.EsquemasXAlta = repo.ObtenerEsquemaAlta();
            ViewBag.EsquemasXBaja = repo.ObtenerEsquemaBaja();
            ViewBag.Priorizado = repoSub.ObtenerTipoTerceros();
            ViewBag.VoltajeNominalAlta = repo.ObtenerVoltajes();
            ViewBag.TipoTercero = repoSub.ObtenerTipoTerceros();
            ViewBag.TiposSubs = repoSub.ObtenerTiposSubestacion();
            ViewBag.EstadosOperativos = repo.ObtenerEstadosOperativos();
            ViewBag.CircuitosXAlta = repo.CircuitosXAlta();
            ViewBag.Tiposbloque = repo.ObtenerTiposBloque();
            ViewBag.TiposSalida = repo.ObtenerTiposSalida();
            ViewBag.sector = repo.ObtenerSectores();
            ViewBag.VoltajeTerciario = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");
            ViewBag.VoltajeSecundario = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");


            return View();
        }

        // POST: Subestaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubestacionesDistViewModel DSubestacion)
        {
            Repositorio repo = new Repositorio(db);
            var repoSub = new SubDistribucionRepositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();

            if (ModelState.IsValid)
            {
                if (await ValidarExisteCodigo(DSubestacion.DSubestaciones.Codigo))
                {
                    DSubestacion.DSubestaciones.Id_EAdministrativa = (Int16)Id_Eadministrativa; //Estaba: (Int)Id_Eadministrativa;
                    DSubestacion.DSubestaciones.NumAccion = repo.GetNumAccion("I", "USD", 0);

                    DSubestacion.DSubestaciones.Codigo = DSubestacion.DSubestaciones.Codigo.ToUpperInvariant();

                    db.Subestaciones.Add(DSubestacion.DSubestaciones);
                 

                    db.SaveChanges();


                    return RedirectToAction("Edit", new { codigo = DSubestacion.DSubestaciones.Codigo , inserta ="Si"});

                }
                else
                {
                    ModelState.AddModelError("DSubestaciones.Codigo", "Ya existe una subestación con ese Código.");
                }
            }

            ViewBag.Sucursales = repo.ObtenerSucursales();
            ViewBag.EsquemasXAlta = repo.ObtenerEsquemaAlta();
            ViewBag.EstadosOperativos = repo.ObtenerEstadosOperativos();
            ViewBag.EsquemasXBaja = repo.ObtenerEsquemaBaja();
            ViewBag.TipoTercero = repoSub.ObtenerTipoTerceros();
            ViewBag.Priorizado = repoSub.ObtenerTipoTerceros();
            ViewBag.VoltajeNominalAlta = repo.ObtenerVoltajes();
            ViewBag.TiposSubs = repoSub.ObtenerTiposSubestacion();
            ViewBag.LineasCount = DSubestacion?.Circuitos_Alta?.Count ?? 0;
            ViewBag.BloquesCount = DSubestacion?.Bloques_Transformacion?.Count ?? 0;
            ViewBag.CircuitosXAlta = repo.CircuitosXAlta();
            ViewBag.Tiposbloque = repo.ObtenerTiposBloque();
            ViewBag.TiposSalida = repo.ObtenerTiposSalida();
            ViewBag.sector = repo.ObtenerSectores();
            ViewBag.VoltajeTerciario = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");
            ViewBag.VoltajeSecundario = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");
            //DSubestacion.Bloques_Transformacion;

            return View(DSubestacion);
        }

        private async Task<bool> ValidarExisteCodigo(string Codigo)
        {
            var listaSubsD = await new SubDistribucionRepositorio(db).ObtenerListadoSubD();

            return !listaSubsD.Select(c => new { c.Codigo }).Any(c => c.Codigo == Codigo);
        }


        // GET: Subestaciones/Edit/5
        [TienePermiso(6)]// verifico que el usario tenga permiso de editar subestaciones
        public async Task<ActionResult> Edit(string codigo, string inserta)
        {

            Subestaciones.Models.Clases.SubestacionesDistViewModel DSubD = new SubestacionesDistViewModel();
            DSubD.DSubestaciones = db.Subestaciones.Find(codigo);

            ViewBag.Inserto = inserta;
            var repo = new Repositorio(db);
            var repoSub = new SubDistribucionRepositorio(db);

            var _arbol = new List<TreeViewNode>();
            var _arbol_hijos = new List<TreeViewNode>();

            GenerarArbolesJerarquias(codigo, codigo, out _arbol, out _arbol_hijos);

            ViewBag.Json = new JavaScriptSerializer().Serialize(_arbol);
            ViewBag.JerarquiaHijo_Json = new JavaScriptSerializer().Serialize(_arbol_hijos);

          
            ViewBag.Sucursales = repo.ObtenerSucursales();
            ViewBag.EsquemasXAlta = repo.ObtenerEsquemaAlta();
            ViewBag.EsquemasXBaja = repo.ObtenerEsquemaBaja();
            ViewBag.Priorizado = repoSub.ObtenerPriorizados();
            ViewBag.VoltajeNominalAlta = repo.ObtenerVoltajes();
            ViewBag.TipoTercero = repoSub.ObtenerTipoTerceros();
            ViewBag.TiposSubs = repoSub.ObtenerTiposSubestacion();
            ViewBag.EstadosOperativos = repo.ObtenerEstadosOperativos();
            ViewBag.CircuitosXAlta = repo.CircuitosXAlta();
            ViewBag.Tiposbloque = repo.ObtenerTiposBloque();
            ViewBag.TiposSalida = repo.ObtenerTiposSalida();
            ViewBag.sector = repo.ObtenerSectores();
            ViewBag.VoltajeTerciario = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");
            ViewBag.VoltajeSecundario = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");
            ObtenerLugarHabitado(DSubD.DSubestaciones.Sucursal, DSubD.DSubestaciones.BarrioPueblo);

            List<Bloque> Bloques_Transformacion = db.Bloque.Where(s => s.Codigo == codigo).ToList();
            ViewBag.cantBloques = Bloques_Transformacion.Count();
            return View(DSubD);
        }

        // POST: Subestaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SubestacionesDistViewModel DSubestacion)
        {
            Repositorio repo = new Repositorio(db);
            var repoSub = new SubDistribucionRepositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();

            if (ModelState.IsValid)
            {
                if ((DSubestacion.DSubestaciones.CodAntiguo != DSubestacion.DSubestaciones.Codigo)) /*verifico que hayan cambiado el codigo si lo cambiaron verifico q no exista el nuevo*/
                {
                    if (await ValidarExisteCodigo(DSubestacion.DSubestaciones.Codigo))
                    {
                        await db.Database.ExecuteSqlCommandAsync("UPDATE Subestaciones SET codigo = @cod WHERE codigo = @codAnt", new SqlParameter("@cod", DSubestacion.DSubestaciones.Codigo), new SqlParameter("@codAnt", DSubestacion.DSubestaciones.CodAntiguo));

                        repo.GetNumAccion("M", "USD", DSubestacion.DSubestaciones.NumAccion);

                        db.Entry(DSubestacion.DSubestaciones).State = EntityState.Modified;

                        await db.Database.ExecuteSqlCommandAsync("UPDATE Bloque SET codigo = @cod WHERE Codigo = @codAnt", new SqlParameter("@cod", DSubestacion.DSubestaciones.Codigo), new SqlParameter("@codAnt", DSubestacion.DSubestaciones.CodAntiguo));
                        await db.Database.ExecuteSqlCommandAsync("UPDATE SalidaExclusivaSub SET codigo = @cod WHERE Codigo = @codAnt", new SqlParameter("@cod", DSubestacion.DSubestaciones.Codigo), new SqlParameter("@codAnt", DSubestacion.DSubestaciones.CodAntiguo));


                        db.SaveChanges();


                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("DSubestaciones.Codigo", "Ya existe una subestación con ese Código.");
                    }
                }
                else
                {
                    // modifico sin cambio de codigo

                    //modifico datos de subs
                    repo.GetNumAccion("M", "USD", DSubestacion.DSubestaciones.NumAccion);

                    db.Entry(DSubestacion.DSubestaciones).State = EntityState.Modified;


                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception E)
                    {
                        ViewBag.Error = E.Message;
                        throw;
                    }

                    return RedirectToAction("Index");
                }
            }//si el modelo no es valido
            Subestaciones.Models.Clases.SubestacionesDistViewModel DSubD = new SubestacionesDistViewModel();
            DSubD.DSubestaciones = db.Subestaciones.Find(DSubestacion.DSubestaciones.Codigo);

            ViewBag.Sucursales = repo.ObtenerSucursales();
            ViewBag.EsquemasXAlta = repo.ObtenerEsquemaAlta();
            ViewBag.EsquemasXBaja = repo.ObtenerEsquemaBaja();
            ViewBag.Priorizado = repoSub.ObtenerTipoTerceros();
            ViewBag.VoltajeNominalAlta = repo.ObtenerVoltajes();
            ViewBag.TipoTercero = repoSub.ObtenerTipoTerceros();
            ViewBag.TiposSubs = repoSub.ObtenerTiposSubestacion();
            ViewBag.EstadosOperativos = repo.ObtenerEstadosOperativos();
            ViewBag.CircuitosXAlta = repo.CircuitosXAlta();
            ViewBag.Tiposbloque = repo.ObtenerTiposBloque();
            ViewBag.TiposSalida = repo.ObtenerTiposSalida();
            ViewBag.sector = repo.ObtenerSectores();
            ViewBag.VoltajeTerciario = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");
            ViewBag.VoltajeSecundario = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");
            ObtenerLugarHabitado(DSubD.DSubestaciones.Sucursal, DSubD.DSubestaciones.BarrioPueblo);

            return View(DSubestacion);
        }

        public void GenerarArbolesJerarquias(string id, string desconectivoPrincipal, out List<TreeViewNode> _arbolPadres, out List<TreeViewNode> _arbolHijos)
        {
            var _arbol = new List<TreeViewNode>();
            var _arbol_hijos = new List<TreeViewNode>();

            var lineasPadre = db.Sub_LineasSubestacion.Where(c => c.Subestacion.Equals(id)).ToList();
            List<InstalacionesInterrumpibles> interrumpibles = db.InstalacionesInterrumpibles.ToList();
            List<CircuitosPrimarios> circuitosHijos = db.CircuitosPrimarios.Where(c => c.SubAlimentadora.Equals(id)).ToList();


            var _state = new StateViewNode();

            Stack _pilaArbol = new Stack(); // esta pila es para invertir el orden de la jerarquía de instalaciones padres.
            _state.opened = true;

            var _nodoSubestacion = new TreeViewNode();
            _nodoSubestacion.id = id;
            _nodoSubestacion.parent = lineasPadre.Count() > 0 ? lineasPadre.Where(c => c.Subestacion == id).FirstOrDefault().Seccionalizador : "#";

            _nodoSubestacion.text = id;
            _nodoSubestacion.state = _state;
            _nodoSubestacion.icon = AsignarImagen(id);

            _pilaArbol.Push(_nodoSubestacion);


            foreach (var seccionalizador in lineasPadre)
            {
                var codigoDesconectivo = seccionalizador.Seccionalizador;

                while (codigoDesconectivo != null)
                {
                    var registro_interrumpible = interrumpibles.Where(c => c.Codigo.Equals(codigoDesconectivo)).FirstOrDefault();

                    if (registro_interrumpible != null)
                    {
                        var TipoInstalacion = registro_interrumpible.Tipo;
                        var _nodo = new TreeViewNode();
                        _nodo.id = registro_interrumpible.Codigo;
                        _nodo.parent = "#";//registro_interrumpible.Padre;

                        _nodo.text = _nodo.id;
                        _nodo.state = _state;
                        _nodo.icon = AsignarImagen(codigoDesconectivo);

                        _pilaArbol.Push(_nodo);
                        //_arbol.Add(_nodo);
                    }

                    codigoDesconectivo = registro_interrumpible?.Padre;
                }
            }

            int cantidadNodos = _pilaArbol.Count;

            for (int i = 0; i < cantidadNodos; i++)
            {
                _arbol.Add((TreeViewNode)_pilaArbol.Pop());
            }

            var _nodoPadre = new TreeViewNode();
            _nodoPadre.id = id;
            _nodoPadre.parent = "#";
            _nodoPadre.text = id;
            _nodoPadre.icon = AsignarImagen(id);//"fa fa-map-signs"; //"fa fa-window-minimize";
            _nodoPadre.state = _state;

            _arbol_hijos.Add(_nodoPadre);

            foreach (var desc in circuitosHijos)
            {
                var _nodo = new TreeViewNode();
                _nodo.id = desc.CodigoCircuito;
                _nodo.parent = id;
                _nodo.text = desc.CodigoCircuito;

                _nodo.icon = AsignarImagen(desc.CodigoCircuito);

                _nodo.state = _state;
                _arbol_hijos.Add(_nodo);
            }

            _arbolPadres = _arbol;
            _arbolHijos = _arbol_hijos;
        }

        public string AsignarImagen(string codigo)
        {
            string icon = "";
            if (codigo == null)
            {
                icon = "fa fa-code-fork";
            }
            else
            {
                switch (codigo.Substring(1, 1))
                {
                    case "D":
                        icon = "fa fa-code-fork";
                        break;
                    case "B":
                        icon = "fa fa-database";
                        break;
                    case "E":
                        icon = "fa fa-square-o";
                        break;
                    case "C":
                        icon = "fa fa-microphone";
                        break;
                    case "G":
                        icon = "";
                        break;
                    case "P":
                        icon = "fa fa-circle";
                        break;
                    case "A":
                    case "K":
                    case "U":
                    case "J":
                    case "X":
                    case "S":
                    case "":
                        icon = "fa fa-map-signs";
                        break;
                    default:
                        icon = "fa fa-code-fork";
                        break;
                }
            }

            //if (codigo.Substring(0,1) == "D") return "fa fa-code-fork";
            //if (codigo.Substring(0, 1) == "P") return "fa fa-circle";
            //if (codigo.Substring(0, 1) == "E") return "fa fa-square-o";
            //if (codigo.Substring(0, 1) == "B") return "fa fa-database";
            //if (codigo.Substring(0, 1) == "C") return "fa fa-microphone";
            //if (codigo.Substring(0, 1) == "A" || codigo.Substring(0, 1) == "S" || codigo.Substring(0, 1) == "J" || 
            //    codigo.Substring(0, 1) == "K" || codigo.Substring(0, 1) == "U")
            //    return "fa fa-map-signs";
            return icon;
        }

        public JsonResult Eliminar(string Codigo)
        {

            try
            {
                Subestaciones.Models.Subestaciones subD = db.Subestaciones.Find(Codigo);
                db.Subestaciones.Remove(subD);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            //}
        }


        // GET: Subestaciones/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subestaciones.Models.Subestaciones subestaciones = db.Subestaciones.Find(id);
            if (subestaciones == null)
            {
                return HttpNotFound();
            }
            return View(subestaciones);
        }

        // POST: Subestaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Subestaciones.Models.Subestaciones subestaciones = db.Subestaciones.Find(id);
            db.Subestaciones.Remove(subestaciones);
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

        public ActionResult ObtenerNuevaFila(int RowNumber, string CodigoSub)
        {
            ViewBag.indice = RowNumber;
            ViewBag.Codigo = CodigoSub;
            return View();
        }

        public JsonResult ObtenerCircuitos()
        {
            var repo = new Repositorio(db);
            return Json(repo.CircuitosXAlta(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerSeccionalizadores(string CodCircuito)
        {
            var repo = new SubDistribucionRepositorio(db);
            return Json(repo.Seccionalizadores(CodCircuito), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerTipoBloque()
        {
            var repo = new Repositorio(db);
            return Json(repo.ObtenerTiposBloque(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerTipoSalida()
        {
            var repo = new Repositorio(db);
            return Json(repo.ObtenerTiposSalida(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerEsquemaBaja()
        {
            var repo = new Repositorio(db);
            return Json(repo.ObtenerEsquemaBaja(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerVoltajeSec()
        {
            var repo = new Repositorio(db);
            return Json(repo.ObtenerVoltajes(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerSector()
        {
            var repo = new Repositorio(db);
            return Json(repo.ObtenerSectores(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerPriorizado()
        {
            var repo = new SubDistribucionRepositorio(db);
            return Json(repo.ObtenerTipoTerceros(), JsonRequestBehavior.AllowGet);
        }

        #region Vistas Parciales  
        [HttpPost]
        public async Task<ActionResult> ListadoSubestaciones()
        {
            var ListaD = new SubDistribucionRepositorio(db);
            //return View(await ListaIM.ObtenerListadoIM());
            return PartialView("_VPSubDistribucion", await ListaD.ObtenerListadoSubD());
        }

        public ActionResult CargarLugarHabitado(int id_sucursal)
        {
            ObtenerLugarHabitado(id_sucursal);

            return PartialView("_VPLugarHabitado");
        }

        public void ObtenerLugarHabitado(int id_sucursal)
        {
            var ListadoLugarHabitado = (from LH in db.LugarHabitado

                                        where LH.Id_Sucursal == id_sucursal
                                        select new SelectListItem
                                        {
                                            Value = LH.nombre,
                                            Text = LH.nombre

                                        }).ToList();
            ViewBag.LH = new SelectList(ListadoLugarHabitado, "Value", "Text");
        }

        public void ObtenerLugarHabitado(int id_sucursal, string lh)
        {
            var LugarH = db.LugarHabitado.Where(c => c.nombre == lh).FirstOrDefault();
            var ListadoLugarHabitado = (from LH in db.LugarHabitado

                                        where LH.Id_Sucursal == id_sucursal
                                        select new SelectListItem
                                        {
                                            Value = LH.nombre,
                                            Text = LH.nombre

                                        }).ToList();
            ViewBag.LH = new SelectList(ListadoLugarHabitado, "Value", "Text", lh);
        }

        public async Task<ActionResult> VPDatosGenerales(string codigo)
        {
            Subestaciones.Models.Clases.SubestacionesDistViewModel DSubD = new SubestacionesDistViewModel();


            DSubD.DSubestaciones = db.Subestaciones.Find(codigo);
            DSubD.Circuitos_Alta = db.Sub_LineasSubestacion.Where(s => s.Subestacion == codigo).ToList();
            DSubD.Bloques_Transformacion = db.Bloque.Where(s => s.Codigo == codigo).ToList();
            DSubD.CentralesElectricas = db.Emplazamiento_Sigere.Where(s => s.CentroTransformacion == codigo).ToList();
            var parametrocodigo = new SqlParameter("@sub", codigo);
            DSubD.DCircuitos_Baja = db.Database.SqlQuery<Circuitos_Baja>(@"SELECT  CP.CodigoCircuito CodigoCircuito,
                                                                                   CP.Id_EAdministrativa Id_EAdministrativa,
                                                                                   CP.id_EAdministrativa_Prov id_EAdministrativa_Prov,
                                                                                   CP.NumAccion NumAccion,
                                                                                   CP.SubAlimentadora SubAlimentadora,
                                                                                   CP.DesconectivoPrincipal DesconectivoPrincipal,
                                                                                   CP.Kmslineas Kmslineas
                                                                                   FROM    CircuitosPrimarios CP
                                                                                   WHERE   CP.SubAlimentadora = @sub
                                                                                    union 
                                                                           SELECT  CP.CodigoCircuito CodigoCircuito,
                                                                                   CP.Id_EAdministrativa Id_EAdministrativa,
                                                                                   CP.id_EAdministrativa_Prov id_EAdministrativa_Prov,
                                                                                   CP.NumAccion NumAccion,
                                                                                   CP.SubestacionTransmision SubAlimentadora,
                                                                                   CP.DesconectivoSalida DesconectivoPrincipal,
                                                                                   CP.Kmslineas Kmslineas
                                                                                   FROM    dbo.CircuitosSubtransmision CP
                                                                                   WHERE   CP.SubestacionTransmision = @sub", parametrocodigo).ToList();

            var repo = new Repositorio(db);
            var repoSub = new SubDistribucionRepositorio(db);

            var _arbol = new List<TreeViewNode>();
            var _arbol_hijos = new List<TreeViewNode>();

            GenerarArbolesJerarquias(codigo, codigo, out _arbol, out _arbol_hijos);

            ViewBag.Json = new JavaScriptSerializer().Serialize(_arbol);
            ViewBag.JerarquiaHijo_Json = new JavaScriptSerializer().Serialize(_arbol_hijos);

            ViewBag.LineasCount = 0;
            ViewBag.BloquesCount = 0;
            ViewBag.Sucursales = repo.ObtenerSucursales();
            ViewBag.EsquemasXAlta = repo.ObtenerEsquemaAlta();
            ViewBag.EsquemasXBaja = repo.ObtenerEsquemaBaja();
            ViewBag.Priorizado = repoSub.ObtenerTipoTerceros();
            ViewBag.VoltajeNominalAlta = repo.ObtenerVoltajes();
            ViewBag.TipoTercero = repoSub.ObtenerTipoTerceros();
            ViewBag.TiposSubs = repoSub.ObtenerTiposSubestacion();
            ViewBag.EstadosOperativos = repo.ObtenerEstadosOperativos();
            ViewBag.CircuitosXAlta = repo.CircuitosXAlta();
            ViewBag.Tiposbloque = repo.ObtenerTiposBloque();
            ViewBag.TiposSalida = repo.ObtenerTiposSalida();
            ViewBag.sector = repo.ObtenerSectores();
            ViewBag.VoltajeTerciario = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");
            ViewBag.VoltajeSecundario = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");

            return PartialView(DSubD);
        }


        public string jstree_padre(string codigo)
        {
            var _arbol = new List<TreeViewNode>();
            var _arbol_hijos = new List<TreeViewNode>();

            GenerarArbolesJerarquias(codigo, codigo, out _arbol, out _arbol_hijos);

            return new JavaScriptSerializer().Serialize(_arbol);
        }

        public string jstree_hjjo(string codigo)
        {
            var _arbol = new List<TreeViewNode>();
            var _arbol_hijos = new List<TreeViewNode>();

            GenerarArbolesJerarquias(codigo, codigo, out _arbol, out _arbol_hijos);

            return new JavaScriptSerializer().Serialize(_arbol_hijos);
        }

        //public ActionResult VPJerarquiaInstalaciones(string codigo)
        //{
        //    var _arbol = new List<TreeViewNode>();
        //    var _arbol_hijos = new List<TreeViewNode>();

        //    GenerarArbolesJerarquias(codigo, codigo, out _arbol, out _arbol_hijos);
        //    ViewBag.Json = new JavaScriptSerializer().Serialize(_arbol);

        //    return PartialView();
        //}
        [HttpGet]
        public async Task<JsonResult> ObtenerListaCentralesElectricas(string codSub)
        {
            var centrales = await new SubDistribucionRepositorio(db).ObtenerListaCentralesE(codSub);
            return Json(centrales, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaCircuitosBaja(string codSub)
        {
            var circuitos = await new SubDistribucionRepositorio(db).ObtenerListaCircuitosBaja(codSub);
            return Json(circuitos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaCircuitosAlta(string codSub)
        {
            var circuitos = await new SubDistribucionRepositorio(db).ObtenerListaCircuitosAlta(codSub);
            return Json(circuitos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> InsertarCircuitoAlta(string sub, string circuito, string seccionalizador)
        {
            if ((sub != null) && (circuito != null))
            {
                await new SubDistribucionRepositorio(db).InsertaCircuitoAlta(sub, circuito, seccionalizador);
                return Json("true");
            }
            throw new ArgumentNullException();
        }

        [HttpPost]
        public ActionResult ActualizarCircuito(string sub, string circuitoAnterior, string circuito, string seccionalizador)
        {
            if ((sub != null) && (circuito != null))
            {
                try
                {
                    new SubDistribucionRepositorio(db).ActualizarCircuito(sub, circuitoAnterior, circuito, seccionalizador);
                    return Json("true");
                }
                catch (Exception e)
                {
                    throw e;

                    throw new HttpException((int)HttpStatusCode.NotFound, "Ocurrió un error al editar el circuito.");
                }
            }
            throw new ArgumentNullException();
        }

        [HttpPost]
        public ActionResult EliminarCircuito(string sub, string circuito)
        {
            if (circuito != null)
            {
                try
                {
                    new SubDistribucionRepositorio(db).EliminarCircuito(sub, circuito);
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

        [HttpGet]
        public async Task<JsonResult> ObtenerListaBloques(string codSub)
        {
            var bloques = await new SubDistribucionRepositorio(db).ObtenerListaBloquesTransformacion(codSub);
            return Json(bloques, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertarBloque(string sub, string tipoBloque, string tipoSalida, short esquema, bool priorizado, short? VS, short? VT, string sector, string Cliente, int EA, int numA)
        {
            if (sub != null) 
            {
                new SubDistribucionRepositorio(db).InsertaBloque(sub, tipoBloque, tipoSalida, esquema, priorizado, VS, VT, sector, Cliente, EA, numA);
                return Json("true");
            }
            throw new ArgumentNullException();
        }

        [HttpPost]
        public ActionResult ActualizarBloque(BloqueSub bloqueActualizar)
        {
            if (bloqueActualizar != null) 
            {
                try
                {
                    new SubDistribucionRepositorio(db).ActualizarBloque(bloqueActualizar);
                    return Json("true");
                }
                catch (Exception e)
                {
                    throw e;

                    throw new HttpException((int)HttpStatusCode.NotFound, "Ocurrió un error al editar el circuito.");
                }
            }
            throw new ArgumentNullException();
        }

        [HttpPost]
        public ActionResult EliminarBloque(string sub, int bloque)
        {
            if (sub != null)
            {
                try
                {
                    new SubDistribucionRepositorio(db).EliminarBloque(sub, bloque);
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

        public ActionResult VPTransformadores(string codigo)
        {
            var ListaTransfSub = new TransfSubtRepositorio(db);
            return PartialView(ListaTransfSub.ObtenerListadoTransformadorEnSub(codigo));
        }

        public async Task<ActionResult> VPDefectos(string codigo)
        {
            var ListaDefectos = new DefectoRepositorio(db);
            return PartialView(await ListaDefectos.ObtenerDefectos(codigo));
        }

        public async Task<ActionResult> VPProyectos(string codigo)
        {
            var ListaProyectos = new DefectoRepositorio(db);
            return PartialView(await ListaProyectos.ObtenerProyectos(codigo));
        }

        public async Task<ActionResult> VPMejoras(string codigo)
        {
            var ListaMejoras = new DefectoRepositorio(db);
            return PartialView(await ListaMejoras.ObtenerMejoras(codigo));
        }

        public async Task<ActionResult> VPMttos(string codigo)
        {
            var ListaMttos = new DefectoRepositorio(db);
            return PartialView(await ListaMttos.ObtenerMttos(codigo));
        }

        public async Task<ActionResult> VPTermografias(string codigo)
        {
            var ListaTermografias = new DefectoRepositorio(db);
            return PartialView(await ListaTermografias.ObtenerTermografias(codigo, 'E'));
        }

        public async Task<ActionResult> VPPtosCalientes(short EA, int numA)
        {
            var ListaPtos = new DefectoRepositorio(db);
            return PartialView(await ListaPtos.ObtenerPtos(EA, numA));
        }

        public async Task<ActionResult> VPFoto(short EA, int numA)
        {
            var ListaFotos = new DefectoRepositorio(db);
            return PartialView(await ListaFotos.ObtenerFoto(EA, numA));
        }

        public ActionResult VPFotoSub(string codigo)
        {
            var e = db.Fotos.Where(c => c.Instalacion == codigo).ToList();
            return PartialView(e);
        }

        public async Task<ActionResult> VPTC(string codigo)
        {
            var ListaTCs = new TCRepositorio(db);
            return PartialView(await ListaTCs.ObtenerTCSub(codigo));
        }

        public async Task<ActionResult> VPTP(string codigo)
        {
            var ListaTPs = new TPRepositorio(db);
            return PartialView(await ListaTPs.ObtenerTPSub(codigo));
        }

        public async Task<ActionResult> VPDesc(string codigo)
        {
            var ListaDesc = new DefectoRepositorio(db);
            return PartialView(await ListaDesc.ObtenerDesconectivos(codigo));
        }

        public ActionResult VPRedCD(string codigo)
        {
            return PartialView(db.Sub_RedCorrienteDirecta.Where(c => c.Codigo == codigo).ToList());
            //var ListaRedCD = new RedCDRepositorio(db);
            //return PartialView(await ListaRedCD.ObtenerListadoRedesCDSub(codigo));
        }

        public async Task<ActionResult> VPBaterias(int idServCD)
        {
            var ListaBat = new BateriasRepositorio(db);
            return PartialView(await ListaBat.ObtenerBaterias(idServCD));
        }

        public async Task<ActionResult> VPCargadores(int idServCD)
        {
            var ListaCar = new BancoCargadoresRepositorio(db);
            return PartialView(await ListaCar.ObtenerCargadores(idServCD));
        }

        public ActionResult VPRedCA(string codigo)
        {

            return PartialView(db.Sub_RedCorrienteAlterna.Where(c => c.Codigo == codigo).ToList());
        }

        public async Task<ActionResult> VPTUP(string codigo)
        {
            var listaTUP = new RedCorrienteAlternaRepositorio(db);


            return PartialView(await listaTUP.ObtenerListadoTransformadoresBanco(codigo));
        }

        public async Task<ActionResult> VPBreakers(string codigo, int idServCA)
        {
            var listaBreakers = new RedCorrienteAlternaRepositorio(db);


            return PartialView(await listaBreakers.ObtenerListaBreakers(codigo, idServCA));
        }

        public async Task<ActionResult> VPInspecciones(string codigo)
        {
            var ListaInspec = new DefectoRepositorio(db);
            return PartialView(await ListaInspec.ObtenerInspecciones(codigo));
        }

        public async Task<ActionResult> VPPerdidas(string codigo, string mes, string anho)
        {
            var ListaPerdidas = new DefectoRepositorio(db);
            return PartialView(await ListaPerdidas.ObtenerPerdidas(codigo, mes, anho));
        }

        public async Task<ActionResult> VPProtecciones(string codigo)
        {
            var ListaProtecs = new DefectoRepositorio(db);
            return PartialView(await ListaProtecs.ObtenerProtecciones(codigo));
        }

        #endregion

    }
}
