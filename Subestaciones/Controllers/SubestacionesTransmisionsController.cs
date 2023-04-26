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
    public class SubestacionesTransmisionsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: SubestacionesTransmisions
        public async Task<ActionResult> Index()
        {
            var ListaSub = new SubTRepositorio(db);
            return View(await ListaSub.ObtenerListadoSubT());
        }

        // GET: SubestacionesTransmisions/Details/5
        public ActionResult Details(string codigo)
        {
            Subestaciones.Models.Clases.SubestacionesTransViewModel TSubT = new SubestacionesTransViewModel();
            if (codigo == null || codigo == "")
            {
                TSubT.TSubestaciones = db.SubestacionesTransmision.FirstOrDefault();
                if (TSubT.TSubestaciones != null)
                {
                    codigo = TSubT.TSubestaciones.Codigo;
                }
                else
                {
                    ViewBag.mensaje = "No existen datos de subestaciones.";
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            else
            {
                TSubT.TSubestaciones = db.SubestacionesTransmision.Find(codigo);
            }
            if (TSubT.TSubestaciones == null)
            {
                ViewBag.mensaje = "No existen datos de subestaciones.";
                return View("~/Views/Shared/Error.cshtml");
            }

            var listasub = db.SubestacionesTransmision.ToList().Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " " + c.NombreSubestacion });
            ViewBag.listaSubestaciones = new SelectList(listasub, "Value", "Text", codigo);


            var _arbol = new List<TreeViewNode>();
            var _arbol_hijos = new List<TreeViewNode>();
            GenerarArbolesJerarquias(codigo, codigo,  out _arbol_hijos);
            var repodefectos = new DefectoRepositorio(db);

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

            GenerarArbolesJerarquias(codigo, codigo, out _arbol_hijos);

            //ViewBag.Json = new JavaScriptSerializer().Serialize(_arbol);
            ViewBag.JerarquiaHijo_Json = new JavaScriptSerializer().Serialize(_arbol_hijos);


            return View(TSubT);
        }

        // GET: SubestacionesTransmisions/Create
        [TienePermiso(26)]// verifico que tenga permiso de crear y eliminar subestaciones
        public async Task<ActionResult> Create()
        {
            var repo = new Repositorio(db);
            var repoSub = new SubDistribucionRepositorio(db);

            var _arbol = new List<TreeViewNode>();
            var _arbol_hijos = new List<TreeViewNode>();



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

        // POST: SubestacionesTransmisions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubestacionesTransViewModel TSubestacion)
        {
            Repositorio repo = new Repositorio(db);
            var repoSub = new SubDistribucionRepositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();
            var Id_EadministrativaProv = repo.GetId_EAdministrativaProv();

            if (ModelState.IsValid)
            {
                if (await ValidarExisteCodigo(TSubestacion.TSubestaciones.Codigo))
                {
                    TSubestacion.TSubestaciones.Id_EAdministrativa = (Int16)Id_Eadministrativa; //Estaba (int)Id_Eadministrativa;
                    TSubestacion.TSubestaciones.id_EAdministrativa_Prov = (int)Id_EadministrativaProv;
                    TSubestacion.TSubestaciones.NumAccion = repo.GetNumAccion("I", "UST", 0);

                    TSubestacion.TSubestaciones.Codigo = TSubestacion.TSubestaciones.Codigo.ToUpperInvariant(); //para que se guarde el codigo en mayuscula

                    db.SubestacionesTransmision.Add(TSubestacion.TSubestaciones);

                    db.SaveChanges();


                    return RedirectToAction("Edit", new { TSubestacion.TSubestaciones.Codigo, inserta = "Si"});
                }
                else
                {
                    ModelState.AddModelError("TSubestaciones.Codigo", "Ya existe una subestación con ese Código.");
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
            //ViewBag.LineasCount = DSubestacion?.Circuitos_Alta?.Count ?? 0;
            ViewBag.BloquesCount = TSubestacion?.Bloques_Transformacion?.Count ?? 0;
            ViewBag.CircuitosXAlta = repo.CircuitosXAlta();
            ViewBag.Tiposbloque = repo.ObtenerTiposBloque();
            ViewBag.TiposSalida = repo.ObtenerTiposSalida();
            ViewBag.sector = repo.ObtenerSectores();
            ViewBag.VoltajeTerciario = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");
            ViewBag.VoltajeSecundario = new SelectList(await repo.voltaje(), "Id_VoltajeSistema", "Voltaje");
            //DSubestacion.Bloques_Transformacion;

            return View(TSubestacion);
        }
        public string jstree_hjjo(string codigo)
        {
            var _arbol = new List<TreeViewNode>();
            var _arbol_hijos = new List<TreeViewNode>();

            GenerarArbolesJerarquias(codigo, codigo, out _arbol_hijos);

            return new JavaScriptSerializer().Serialize(_arbol_hijos);
        }

        public void GenerarArbolesJerarquias(string id, string desconectivoPrincipal, out List<TreeViewNode> _arbolHijos)
        {
            var _arbol = new List<TreeViewNode>();
            var _arbol_hijos = new List<TreeViewNode>();

            List<InstalacionesInterrumpibles> interrumpibles = db.InstalacionesInterrumpibles.ToList();
            //var lineasPadre = db.Sub_LineasSubestacion.Where(c => c.Subestacion.Equals(id)).ToList();
            List<CircuitosPrimarios> circuitosHijosPrimarios = db.CircuitosPrimarios.Where(c => c.SubAlimentadora.Equals(id)).ToList();
            List<CircuitosSubtransmision> circuitosHijosSubtransmision = db.CircuitosSubtransmision.Where(c => c.SubestacionTransmision.Equals(id)).ToList();
            //List<InstalacionDesconectivos> desconectivos = db.InstalacionDesconectivos.Where(c => c.CircuitoA.Equals(id) || c.CircuitoB.Equals(id)).ToList();
            //List<BancoCapacitores> bancocapacitores = db.BancoCapacitores.Where(c => c.Circuito.Equals(id)).ToList();
            //List<BancoTransformadores> bancotransformadores = db.BancoTransformadores.Where(c => c.Circuito.Equals(id)).ToList();            

            var codigoDesconectivo = desconectivoPrincipal;
            var _state = new StateViewNode();
            Stack _pilaArbol = new Stack(); // esta pila es para invertir el orden de la jerarquía de instalaciones padres.

            _state.opened = true;

            //var _nodoSubestacion = new TreeViewNode();
            //_nodoSubestacion.id = id;
            //_nodoSubestacion.parent = lineasPadre.First().Seccionalizador;
            //_nodoSubestacion.text = id;
            //_nodoSubestacion.state = _state;
            //_nodoSubestacion.icon = AsignarImagen(id);

            //_pilaArbol.Push(_nodoSubestacion);

            //foreach (var seccionalizador in lineasPadre)
            //{
            //    codigoDesconectivo = seccionalizador.Seccionalizador;

            //    while (codigoDesconectivo != null)
            //    {
            //        var registro_interrumpible = interrumpibles.Where(c => c.Codigo.Equals(codigoDesconectivo)).FirstOrDefault();

            //        if (registro_interrumpible != null)
            //        {
            //            var TipoInstalacion = registro_interrumpible.Tipo;
            //            var _nodo = new TreeViewNode();
            //            _nodo.id = registro_interrumpible.Codigo;
            //            _nodo.parent = "#";//registro_interrumpible.Padre;

            //            _nodo.text = _nodo.id;
            //            _nodo.state = _state;
            //            _nodo.icon = AsignarImagen(codigoDesconectivo);

            //            _pilaArbol.Push(_nodo);
            //            //_arbol.Add(_nodo);
            //        }
            //        codigoDesconectivo = registro_interrumpible?.Padre;
            //    }
            //}

            //int cantidadNodos = _pilaArbol.Count;

            //for (int i = 0; i < cantidadNodos; i++)
            //{
            //    _arbol.Add((TreeViewNode)_pilaArbol.Pop());
            //}

            var _nodoPadre = new TreeViewNode();
            _nodoPadre.id = id;
            _nodoPadre.parent = "#";
            _nodoPadre.text = id;
            _nodoPadre.icon = AsignarImagen(id);//"fa fa-map-signs"; //"fa fa-window-minimize";
            _nodoPadre.state = _state;
            _arbol_hijos.Add(_nodoPadre);

            foreach (var CircuitoPrim in circuitosHijosPrimarios)
            {
                var _nodo = new TreeViewNode();
                _nodo.id = CircuitoPrim.CodigoCircuito;
                _nodo.parent = id;
                _nodo.text = CircuitoPrim.CodigoCircuito;

                _nodo.icon = AsignarImagen(CircuitoPrim.CodigoCircuito);

                _nodo.state = _state;
                _arbol_hijos.Add(_nodo);
            }

            foreach (var CircuitoSub in circuitosHijosSubtransmision)
            {
                var _nodo = new TreeViewNode();
                _nodo.id = CircuitoSub.CodigoCircuito;
                _nodo.parent = id;
                _nodo.text = CircuitoSub.CodigoCircuito;

                _nodo.icon = AsignarImagen(CircuitoSub.CodigoCircuito);

                _nodo.state = _state;
                _arbol_hijos.Add(_nodo);
            }
            //foreach (var capacitores in bancocapacitores)
            //{
            //    var _nodo = new TreeViewNode();
            //    _nodo.id = capacitores.Codigo;
            //    _nodo.parent = id;
            //    _nodo.text = capacitores.Codigo;
            //    _nodo.icon = AsignarImagen(capacitores.Codigo);
            //    _nodo.state = _state;
            //    _arbol_hijos.Add(_nodo);
            //}

            //foreach (var transformadores in bancotransformadores)
            //{
            //    var _nodo = new TreeViewNode();
            //    _nodo.id = transformadores.Codigo;
            //    _nodo.parent = id;
            //    _nodo.text = transformadores.Codigo;
            //    _nodo.icon = AsignarImagen(transformadores.Codigo);
            //    _nodo.state = _state;
            //    _arbol_hijos.Add(_nodo);
            //}

            //_arbolPadres = _arbol;
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

        private async Task<bool> ValidarExisteCodigo(string Codigo)
        {
            var listaSubsT = await new SubTRepositorio(db).ObtenerListadoSubT();

            return !listaSubsT.Select(c => new { c.Codigo }).Any(c => c.Codigo == Codigo);
        }
        // GET: SubestacionesTransmisions/Edit/5
        public async Task<ActionResult> Edit(string codigo, string inserta)
        {
            Subestaciones.Models.Clases.SubestacionesTransViewModel SubT = new SubestacionesTransViewModel();
            SubT.TSubestaciones = db.SubestacionesTransmision.Find(codigo);
           
            var repo = new Repositorio(db);
            var repoSub = new SubDistribucionRepositorio(db);

            var _arbol = new List<TreeViewNode>();
            var _arbol_hijos = new List<TreeViewNode>();

            GenerarArbolesJerarquias(codigo, codigo, out _arbol_hijos);

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
            ObtenerLugarHabitado(SubT.TSubestaciones.Sucursal, SubT.TSubestaciones.BarrioPueblo);
            ViewBag.Inserto = inserta;



            return View(SubT);
        }

        // POST: SubestacionesTransmisions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SubestacionesTransViewModel TSubestacion)
        {
            Repositorio repo = new Repositorio(db);
            var repoSub = new SubDistribucionRepositorio(db);
            var Id_Eadministrativa = repo.GetId_EAdministrativaUsuario();

            if (ModelState.IsValid)
            {
                if ((TSubestacion.TSubestaciones.CodAntiguo != TSubestacion.TSubestaciones.Codigo)) /*verifico que hayan cambiado el codigo si lo cambiaron verifico q no exista el nuevo*/
                {
                    if (await ValidarExisteCodigo(TSubestacion.TSubestaciones.Codigo))
                    {
                        await db.Database.ExecuteSqlCommandAsync("UPDATE SubestacionesTransmision SET codigo = @cod WHERE codigo = @codAnt", new SqlParameter("@cod", TSubestacion.TSubestaciones.Codigo), new SqlParameter("@codAnt", TSubestacion.TSubestaciones.CodAntiguo));

                        repo.GetNumAccion("M", "USD", TSubestacion.TSubestaciones.NumAccion);
                        TSubestacion.TSubestaciones.Codigo = TSubestacion.TSubestaciones.Codigo.ToUpperInvariant(); //para que se guarde el codigo en mayuscula

                        db.Entry(TSubestacion.TSubestaciones).State = EntityState.Modified;


                        if (TSubestacion.Bloques_Transformacion != null)
                        {
                            foreach (Bloque item in TSubestacion.Bloques_Transformacion)
                            {
                                if (db.Bloque.Any(b => b.Codigo == item.Codigo && b.Id_bloque == item.Id_bloque))
                                {

                                    if (item.TipoSalida == "Exclusiva")
                                    {
                                        if (db.SalidaExclusivaSub.Any(se => se.Codigo == item.Codigo && se.id_bloque == item.Id_bloque))
                                        {
                                            var TipoSalidaExclusiva = db.SalidaExclusivaSub.Find(item.Codigo, item.Id_bloque);
                                            TipoSalidaExclusiva.Cliente = item.Cliente;
                                            TipoSalidaExclusiva.Sector = item.sector;
                                            db.Entry(TipoSalidaExclusiva).State = EntityState.Modified;
                                        }

                                        else
                                        {
                                            SalidaExclusivaSub salidaExclusivaSub = new SalidaExclusivaSub
                                            {
                                                Id_EAdministrativa = TSubestacion.TSubestaciones.Id_EAdministrativa,
                                                NumAccion = (int)TSubestacion.TSubestaciones.NumAccion,
                                                Codigo = item.Codigo,
                                                id_bloque = (short)item.Id_bloque,
                                                Sector = item.sector,
                                                Cliente = item.Cliente
                                            };
                                            db.SalidaExclusivaSub.Add(salidaExclusivaSub);
                                        }
                                    }
                                    else
                                    {
                                        if (db.SalidaExclusivaSub.Any(se => se.Codigo == item.Codigo && se.id_bloque == item.Id_bloque))
                                        {
                                            var TipoSalidaExclusiva = db.SalidaExclusivaSub.Find(item.Codigo, item.Id_bloque);
                                            db.Entry(TipoSalidaExclusiva).State = EntityState.Deleted;
                                        }
                                    }
                                    db.Entry(item).State = EntityState.Modified;
                                }
                                else
                                {
                                    if (TSubestacion.Bloques_Transformacion != null)
                                    {
                                        var id_bloque = db.Database.SqlQuery<int>(@"declare @numBloque int
                                    Select @numBloque = Max(Id_bloque) + 1
                                    From Bloque
                                    Where Codigo = {0}
                                    if @numBloque is null
                                    set @numBloque = 1
                                    Select @numBloque as idBloque", TSubestacion.TSubestaciones.Codigo).First();
                                        foreach (var b in TSubestacion.Bloques_Transformacion)
                                        {

                                            b.Id_bloque = id_bloque++;
                                            db.Bloque.Add(b);
                                            if (b.TipoSalida == "Exclusiva")
                                            {
                                                SalidaExclusivaSub salidaExclusivaSub = new SalidaExclusivaSub
                                                {
                                                    Id_EAdministrativa = TSubestacion.TSubestaciones.Id_EAdministrativa,
                                                    NumAccion = (int)TSubestacion.TSubestaciones.NumAccion,
                                                    id_bloque = (short)b.Id_bloque,
                                                    Sector = b.sector,
                                                    Cliente = b.Cliente
                                                };
                                                db.SalidaExclusivaSub.Add(salidaExclusivaSub);
                                            }
                                        }
                                    }


                                }
                            }
                        }

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
                    repo.GetNumAccion("M", "UST", TSubestacion.TSubestaciones.NumAccion);
                    TSubestacion.TSubestaciones.Codigo = TSubestacion.TSubestaciones.Codigo.ToUpperInvariant(); //para que se guarde el codigo en mayuscula
                    db.Entry(TSubestacion.TSubestaciones).State = EntityState.Modified;

                    if (TSubestacion.Bloques_Transformacion != null)
                    {
                        foreach (Bloque item in TSubestacion.Bloques_Transformacion)
                        {
                            //var bloque = db.Bloque.Find(item.Codigo, item.Id_bloque);
                            if (db.Bloque.Any(b => b.Codigo == item.Codigo && b.Id_bloque == item.Id_bloque))
                            {

                                if (item.TipoSalida == "Exclusiva")
                                {

                                    if (db.SalidaExclusivaSub.Any(se => se.Codigo == item.Codigo && se.id_bloque == item.Id_bloque))
                                    {
                                        var TipoSalidaExclusiva = db.SalidaExclusivaSub.Find(item.Codigo, item.Id_bloque);
                                        TipoSalidaExclusiva.Cliente = item.Cliente;
                                        TipoSalidaExclusiva.Sector = item.sector;
                                        db.Entry(TipoSalidaExclusiva).State = EntityState.Modified;
                                    }

                                    else
                                    {
                                        SalidaExclusivaSub salidaExclusivaSub = new SalidaExclusivaSub
                                        {
                                            Id_EAdministrativa = TSubestacion.TSubestaciones.Id_EAdministrativa,
                                            NumAccion = (int)TSubestacion.TSubestaciones.NumAccion,
                                            Codigo = item.Codigo,
                                            id_bloque = (short)item.Id_bloque,
                                            Sector = item.sector,
                                            Cliente = item.Cliente
                                        };
                                        db.SalidaExclusivaSub.Add(salidaExclusivaSub);
                                    }
                                }
                                else
                                {
                                    if (db.SalidaExclusivaSub.Any(se => se.Codigo == item.Codigo && se.id_bloque == item.Id_bloque))
                                    {
                                        var TipoSalidaExclusiva = db.SalidaExclusivaSub.Find(item.Codigo, item.Id_bloque);
                                        db.Entry(TipoSalidaExclusiva).State = EntityState.Deleted;
                                    }
                                }
                                db.Entry(item).State = EntityState.Modified;
                            }
                            else
                            {
                                var id_bloque = db.Database.SqlQuery<int>(@"declare @numBloque int
                Select @numBloque = Max(Id_bloque) + 1
                From Bloque
                Where Codigo = {0}
                if @numBloque is null
                set @numBloque = 1
                Select @numBloque as idBloque", TSubestacion.TSubestaciones.Codigo).First();
                                item.Id_bloque = id_bloque++;
                                db.Bloque.Add(item);
                                if (item.TipoSalida == "Exclusiva")
                                {
                                    SalidaExclusivaSub salidaExclusivaSub = new SalidaExclusivaSub
                                    {
                                        Id_EAdministrativa = TSubestacion.TSubestaciones.Id_EAdministrativa,
                                        NumAccion = (int)TSubestacion.TSubestaciones.NumAccion,
                                        Codigo = item.Codigo,
                                        id_bloque = (short)item.Id_bloque,
                                        Sector = item.sector,
                                        Cliente = item.Cliente
                                    };
                                    db.SalidaExclusivaSub.Add(salidaExclusivaSub);
                                }
                            }
                        }
                    }
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
            Subestaciones.Models.Clases.SubestacionesTransViewModel SubT = new SubestacionesTransViewModel();
            SubT.TSubestaciones = db.SubestacionesTransmision.Find(TSubestacion.TSubestaciones.Codigo);
            SubT.Lineas = db.SubestacionesCabezasLineas.Where(s => s.SubestacionTransmicion == TSubestacion.TSubestaciones.Codigo).ToList();
            SubT.Bloques_Transformacion = db.Bloque.Where(s => s.Codigo == TSubestacion.TSubestaciones.Codigo).ToList();
            SubT.CircuitosXBaja = db.Database.SqlQuery<Circuitos_Baja>(@"SELECT  CP.CodigoCircuito CodigoCircuito,
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
                WHERE   CP.SubestacionTransmision = @sub", new SqlParameter("@sub", TSubestacion.TSubestaciones.Codigo)).ToList();

            TSubestacion.TransformadoresT = db.Database.SqlQuery<TransformadorTransmision>(@"SELECT T.Id_EAdministrativa Id_EAdministrativa, 
T.Id_Transformador Id_Transformador, T.NumAccion NumAccion, T.Codigo Subestacion, T.Nombre Nombre, T.Numemp Numemp, C.Capacidad capacidad,
T.Fase Fase, T.NoSerie NoSerie,T.NumFase NumFase,T.EstadoOperativo EstadoOperativo, Vs1.Voltaje TensionPrimaria,Vs2.Voltaje TensionSecundaria,Vs3.Voltaje TensionTerciario,
T.AnnoFabricacion AnnoFabricacion, T.PorcientoImpedancia PorcientoImpedancia, T.GrupoConexion GrupoConexion, T.PesoTotal PesoTotal,
T.CorrienteAlta CorrientePrimaria, T.FrecuenciaN Frecuencia, T.TipoEnfriamiento Enfriamiento,
T.PerdidasVacio PerdidasVacio, T.PerdidasBajoCarga PerdidasBajoCarga, T.NivelRuido NivelRuido,  T.VoltajeImpulso TensionImpulso, T.PesoAceite, 
T.PesoNucleo PesoNucleo, T.NivelRadioInterf NivelRadioInterf, T.CorrienteBaja CorrienteSecundaria,
T.Tipo Tipo, T.CantVentiladores CantVentiladores, T.CantRadiadores CantRadiadores, T.Observaciones Observaciones, T.PesoTansporte PesoTansporte, 
T.TipoRegVoltaje TipoRegVoltaje, T.NroPosiciones NroPosiciones, T.TipoCajaMando TipoCajaMando, T.TuboExplosor TuboExplosor, T.ValvulaSobrePresion ValvulaSobrePresion,
T.PosicionTrabajo PosicionTrabajo, T.NumeroInventario NumeroInventario, T.FechaDeInstalado FechaDeInstalado
FROM TransformadoresTransmision T 
left join Capacidades C on T.Id_Capacidad=C.Id_Capacidad
left join VoltajesSistemas as VS1 on T.Id_VoltajePrim= VS1.Id_VoltajeSistema
left join VoltajesSistemas as VS2 on T.Id_Voltaje_Secun= VS2.Id_VoltajeSistema
left join VoltajesSistemas as VS3 on T.VoltajeTerciario= VS3.Id_VoltajeSistema
where Codigo= @sub", new SqlParameter("@sub", TSubestacion.TSubestaciones.Codigo)).ToList();
            ViewBag.LineasCount = SubT.Lineas.Count;
            ViewBag.BloquesCount = SubT.Bloques_Transformacion.Count;
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
            ObtenerLugarHabitado(SubT.TSubestaciones.Sucursal, SubT.TSubestaciones.BarrioPueblo);

            return View(TSubestacion);
        }


        public JsonResult Eliminar(string Codigo)
        {

            try
            {
                SubestacionesTransmision subT = db.SubestacionesTransmision.Find(Codigo);
                db.SubestacionesTransmision.Remove(subT);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            //}
        }

        [HttpPost]
        public async Task<ActionResult> ListadoSubestaciones()
        {
            var ListaD = new SubTRepositorio(db);
            //return View(await ListaIM.ObtenerListadoIM());
            return PartialView("_VPSubTransmision", await ListaD.ObtenerListadoSubT());
        }

        // GET: SubestacionesTransmisions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubestacionesTransmision subestacionesTransmision = db.SubestacionesTransmision.Find(id);
            if (subestacionesTransmision == null)
            {
                return HttpNotFound();
            }
            return View(subestacionesTransmision);
        }

        // POST: SubestacionesTransmisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SubestacionesTransmision subestacionesTransmision = db.SubestacionesTransmision.Find(id);
            db.SubestacionesTransmision.Remove(subestacionesTransmision);
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

        [HttpGet]
        public async Task<JsonResult> ObtenerListaLineas(string codSub)
        {
            var centrales = await new SubTRepositorio(db).ObtenerListaLineas(codSub);
            return Json(centrales, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerListaTransformadoresTrans(string codSub)
        {
            var centrales = await new SubTRepositorio(db).ObtenerListaTansformadores(codSub);
            return Json(centrales, JsonRequestBehavior.AllowGet);
        }



        #region Vistas Parciales  
        [HttpPost]
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
            Subestaciones.Models.Clases.SubestacionesTransViewModel TSubT = new SubestacionesTransViewModel();


            TSubT.TSubestaciones = db.SubestacionesTransmision.Find(codigo);
            TSubT.Lineas = db.SubestacionesCabezasLineas.Where(s => s.SubestacionTransmicion == codigo).ToList();
            TSubT.Bloques_Transformacion = db.Bloque.Where(s => s.Codigo == codigo).ToList();
            TSubT.CentralesElectricas = db.Emplazamiento_Sigere.Where(s => s.CentroTransformacion == codigo).ToList();
            TSubT.CircuitosXBaja = db.Database.SqlQuery<Circuitos_Baja>(@"SELECT  CP.CodigoCircuito CodigoCircuito,
                CP.Id_EAdministrativa Id_EAdministrativa,
                CP.id_EAdministrativa_Prov id_EAdministrativa_Prov,
                CP.NumAccion NumAccion,
                CP.SubAlimentadora SubAlimentadora,
                CP.DesconectivoPrincipal DesconectivoPrincipal,
                CP.Kmslineas Kmslineas
                FROM CircuitosPrimarios CP
                WHERE CP.SubAlimentadora = @sub
                union 
                SELECT  CP.CodigoCircuito CodigoCircuito,
                CP.Id_EAdministrativa Id_EAdministrativa,
                CP.id_EAdministrativa_Prov id_EAdministrativa_Prov,
                CP.NumAccion NumAccion,
                CP.SubestacionTransmision SubAlimentadora,
                CP.DesconectivoSalida DesconectivoPrincipal,
                CP.Kmslineas Kmslineas
                FROM dbo.CircuitosSubtransmision CP
                WHERE CP.SubestacionTransmision = @sub", new SqlParameter("@sub", codigo)).ToList();
//            TSubT.TransformadoresT = db.Database.SqlQuery<TransformadorTransmision>(@"SELECT T.Id_EAdministrativa Id_EAdministrativa, 
//T.Id_Transformador Id_Transformador, T.NumAccion NumAccion, T.Codigo Subestacion, T.Nombre Nombre, T.Numemp Numemp, C.Capacidad capacidad,
//T.Fase Fase, T.NoSerie NoSerie,T.NumFase NumFase,T.EstadoOperativo EstadoOperativo, Vs1.Voltaje TensionPrimaria,Vs2.Voltaje TensionSecundaria,Vs3.Voltaje TensionTerciario,
//T.AnnoFabricacion AnnoFabricacion, T.PorcientoImpedancia PorcientoImpedancia, T.GrupoConexion GrupoConexion, T.PesoTotal PesoTotal,
//T.CorrienteAlta CorrientePrimaria, T.FrecuenciaN Frecuencia, T.TipoEnfriamiento Enfriamiento,
//T.PerdidasVacio PerdidasVacio, T.PerdidasBajoCarga PerdidasBajoCarga, T.NivelRuido NivelRuido,  T.VoltajeImpulso TensionImpulso, T.PesoAceite, 
//T.PesoNucleo PesoNucleo, T.NivelRadioInterf NivelRadioInterf, T.CorrienteBaja CorrienteSecundaria,
//T.Tipo Tipo, T.CantVentiladores CantVentiladores, T.CantRadiadores CantRadiadores, T.Observaciones Observaciones, T.PesoTansporte PesoTansporte, 
//T.TipoRegVoltaje TipoRegVoltaje, T.NroPosiciones NroPosiciones, T.TipoCajaMando TipoCajaMando, T.TuboExplosor TuboExplosor, T.ValvulaSobrePresion ValvulaSobrePresion,
//T.PosicionTrabajo PosicionTrabajo, T.NumeroInventario NumeroInventario, T.FechaDeInstalado FechaDeInstalado
//FROM TransformadoresTransmision T 
//left join Capacidades C on T.Id_Capacidad=C.Id_Capacidad
//left join VoltajesSistemas as VS1 on T.Id_VoltajePrim= VS1.Id_VoltajeSistema
//left join VoltajesSistemas as VS2 on T.Id_Voltaje_Secun= VS2.Id_VoltajeSistema
//left join VoltajesSistemas as VS3 on T.VoltajeTerciario= VS3.Id_VoltajeSistema
//where Codigo= @sub", new SqlParameter("@sub", codigo)).ToList();

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

            return PartialView(TSubT);
        }

        public ActionResult VPTransformadores(string codigo)
        {
            var ListaTransfT = new TransfTransRepositorio(db);
            return PartialView(ListaTransfT.ObtenerListadoTransformadorSub(codigo));
        }

        public async Task<ActionResult> VPDefectos(string codigo)
        {
            var ListaDefectos = new DefectoRepositorio(db);
            return PartialView(await ListaDefectos.ObtenerDefectosSubT(codigo));
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

        public async Task<ActionResult> VPTermografiasT(string codigo)
        {
            var ListaTermografias = new DefectoRepositorio(db);
            return PartialView(await ListaTermografias.ObtenerTermografias(codigo, 'R'));
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

        public async Task<ActionResult> VPPerdidas(string codigo, string mes, string anho)
        {
            var ListaPerdidas = new DefectoRepositorio(db);
            return PartialView(await ListaPerdidas.ObtenerPerdidas(codigo, mes, anho));
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

        public async Task<ActionResult> VPRedCD(string codigo)
        {
            var ListaRedCD = new RedCDRepositorio(db);
            return PartialView(await ListaRedCD.ObtenerListadoRedesCDSub(codigo));
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

        public async Task<ActionResult> VPProtecciones(string codigo)
        {
            var ListaProtecs = new DefectoRepositorio(db);
            return PartialView(await ListaProtecs.ObtenerProtecciones(codigo));
        }

        public ActionResult VPFotoSub(string codigo)
        {
            var e = db.Fotos.Where(c => c.Instalacion == codigo).ToList();
            return PartialView(e);
        }
        #endregion
    }
}
