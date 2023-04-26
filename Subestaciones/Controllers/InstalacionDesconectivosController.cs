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
    public class InstalacionDesconectivosController : Controller
    {
        private DBContext db = new DBContext();

        // GET:   
        public ActionResult Index(string inserta)
        {
            var descLista = new DesconectivoSubestacionesRepositorio(db);
            ViewBag.desc = descLista.ListaDesconectivos();
            ViewBag.Inserto = inserta;

            return View();

        }

        // GET: InstalacionDesconectivos/Details/5
        public ActionResult Details(string tipo, string cod, short idEA, int idEAProv)
        {
            Repositorio repo = new Repositorio(db);

            if (tipo == null || cod == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (tipo == "0") // el desconectivo es tipo grampa (puente)
            {
                int id = db.Inst_Nomenclador_Puente.Where(c => c.Codigo == cod).Select(c => c.Id_Puente).FirstOrDefault();
                
                if (id == 0)
                {//el desconectivo no existe en la tabla puente y lo agrego
                    var id_puente = db.Database.SqlQuery<int>(@"declare @numPuente int
                                    Select @numPuente = Max(Id_Puente) + 1
                                    From Inst_Nomenclador_Puente
                                    if @numPuente is null
                                    set @numPuente = 1
                                    Select @numPuente as id_puente").First();
                   
                    Inst_Nomenclador_Puente puente = new Inst_Nomenclador_Puente
                    {
                        Id_Administrativa = idEA,
                        NumAccion = repo.GetNumAccion("I", "GDD", 0),
                        Codigo = cod,
                        Id_Puente = id_puente,
                        id_EAdministrativa_Prov = idEAProv
                    };
                    db.Inst_Nomenclador_Puente.Add(puente);

                    db.SaveChanges();
                    id = id_puente;
                }
                return RedirectToAction("Details", "Inst_Nomenclador_Puente", new { id = id });
            }
            else
            if ((tipo == "1") || (tipo == "9")) // el desconectivo es tipo portafusible
            {

                int id = db.PortaFusibles.Where(c => c.CodigoPortafusible == cod).Select(c => c.Id_Portafusible).FirstOrDefault();
                short ea = db.PortaFusibles.Where(c => c.CodigoPortafusible == cod).Select(c => c.Id_EAdministrativa).FirstOrDefault();
                if (id != 0 || ea != 0)
                {
                    //el portafusible es de tipo fusible
                    return RedirectToAction("Details", "PortaFusibles", new { ea = ea, id = id });
                }
                int idB = db.Breakers.Where(c => c.CodigoBreaker == cod).Select(c => c.Id_Breaker).FirstOrDefault();
                int eaB = db.Breakers.Where(c => c.CodigoBreaker == cod).Select(c => c.id_EAdministrativa).FirstOrDefault();
                if (idB != 0 || eaB != 0)
                {
                    //el portafusible es de tipo breaker
                    return RedirectToAction("Details", "Breakers", new { ea = eaB, id = idB });
                }
                else
                {// no esta definido el tipo de portafusible y se inserta por defecto como fusible
                    //agrego el fuse
                    var id_fuse = db.Database.SqlQuery<int>(@"declare @numFuse int
                                    Select @numFuse = Max(Id_Portafusible) + 1
                                    From PortaFusibles
                                    Where Id_EAdministrativa = {0}
                                    if @numFuse is null
                                    set @numFuse = 1
                                    Select @numFuse as idFuse", ea).First();
                    PortaFusibles p = new PortaFusibles
                    {
                        Id_EAdministrativa = idEA,
                        NumAccion = repo.GetNumAccion("I", "GDD", 0),
                        CodigoPortafusible = cod,
                        Id_Portafusible = id_fuse,
                        id_EAdministrativa_Prov = idEAProv
                    };
                    db.PortaFusibles.Add(p);

                    db.SaveChanges();
                    return RedirectToAction("Details", "PortaFusibles", new { ea = idEA, id = id_fuse });
                }
            }
            else
            if (tipo == "2") // el desconectivo es tipo cuchilla
            {
                int id = db.Inst_Nomenclador_Cuchillas.Where(c => c.Codigo == cod).Select(c => c.Id_Cuchilla).FirstOrDefault();
                if (id == 0)
                {//el desconectivo no existe en la tabla cuchilla y lo agrego
                    var id_cuchilla = db.Database.SqlQuery<int>(@"declare @numCuchilla int
                                    Select @numCuchilla = Max(Id_Cuchilla) + 1
                                    From Inst_Nomenclador_Cuchillas
                                    if @numCuchilla is null
                                    set @numCuchilla = 1
                                    Select @numCuchilla as id_cuchilla").First();
                    var id_fab = db.Database.SqlQuery<int>(@"declare @fab int
                                    Select @fab = Max(Id_Fabricante) 
                                    From Fabricantes
                                    Select @fab as id_fab").First();

                    var id_mando = db.Database.SqlQuery<int>(@"declare @mando int
                                    Select @mando = Max(Id_Mando) 
                                    From Inst_Nomenclador_Cuchillas_Mando
                                    Select @mando as id_mando").First();
                    var id_op = db.Database.SqlQuery<int>(@"declare @op int
                                    Select @op = Max(Id_Operacion) 
                                    From Inst_Nomenclador_Cuchillas_Operacion
                                    Select @op as id_op").First();

                    var id_tension = db.Database.SqlQuery<int>(@"declare @t int
                                    Select @t = Max(Id_Tension) 
                                    From Inst_Nomenclador_Cuchillas_Tension
                                    Select @t as id_tension").First();
                    Inst_Nomenclador_Cuchillas cuchilla = new Inst_Nomenclador_Cuchillas
                    {
                        Id_Administrativa = idEA,
                        NumAccion = repo.GetNumAccion("I", "GDD", 0),
                        Codigo = cod,
                        Id_Cuchilla = id_cuchilla,
                        id_EAdministrativa_Prov = idEAProv,
                        Id_Fabricante=id_fab,
                        Id_Mando = id_mando,
                        Id_Operacion = id_op,
                        Id_Tension = id_tension
                    };
                    db.Inst_Nomenclador_Cuchillas.Add(cuchilla);

                    db.SaveChanges();
                    id = id_cuchilla;
                }
                return RedirectToAction("Details", "Inst_Nomenclador_Cuchillas", new { id = id });
            }
            else
            if (tipo == "3") // el desconectivo es tipo interruptor de aire
            {
                int id = db.Inst_Nomenclador_InterruptorAire.Where(c => c.Codigo == cod).Select(c => c.Id_InterruptorAire).FirstOrDefault();
                var id_intAire = db.Database.SqlQuery<int>(@"declare @id int
                                    Select @id = Max(Id_InterruptorAire) + 1
                                    From Inst_Nomenclador_InterruptorAire
                                    if @id is null
                                    set @id = 1
                                    Select @id as idDatos").First();
                if (id == 0)
                {

                    Inst_Nomenclador_InterruptorAire datosIntAire = new Inst_Nomenclador_InterruptorAire
                    {
                        //aqui si no existe el desconectivo de aire lo inserto con valor no null 
                        Id_Administrativa = idEA,
                        NumAccion = repo.GetNumAccion("I", "GDD", 0),
                        Codigo = cod,
                        Id_InterruptorAire = id_intAire,
                        id_EAdministrativa_Prov = idEAProv,
                        Id_Mando = 1,
                        Id_Tension = 1,
                        Id_Operacion = 1,
                        Id_Fabricante = 3
                    };
                    db.Inst_Nomenclador_InterruptorAire.Add(datosIntAire);

                    db.SaveChanges();

                    id = id_intAire;
                }

                return RedirectToAction("Details", "Inst_Nomenclador_InterruptorAire", new { id = id});
            }
            else
                 if ((tipo == "4") || (tipo == "5") || (tipo == "6") || (tipo == "7") || (tipo == "8")) // el desconectivo es tipo recerrador, interruptor de aceite, interruptor sf6, interruptor vacío o Seccionalizador.
            {
                int id = db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Where(c => c.Codigo == cod).Select(c => c.Id_DatosEspComun).FirstOrDefault();
                int ea = db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Where(c => c.Codigo == cod).Select(c => c.Id_DatosEspComun).FirstOrDefault();
                Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion de = db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Find(id);

                if (de == null)//si ni existe el desconectivo en la tabla se inserta
                {
                    //int t = Integer.parseInt(tipo);
                    //var idnom = db.Inst_Nomenclador_Desconectivos.Where(c => c.Descripcion == t).Select(c => c.Id_Nomenclador).FirstOrDefault();
                    var id_datosEsp = db.Database.SqlQuery<int>(@"declare @id int
                                    Select @id = Max(Id_DatosEspComun) + 1
                                    From inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion
                                    if @id is null
                                    set @id = 1
                                    Select @id as idDatos").First();
                    Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion datos = new Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion
                    {
                        Id_Administrativa = idEA,
                        NumAccion = repo.GetNumAccion("I", "GDD", 0),
                        Codigo = cod,
                        Id_DatosEspComun = id_datosEsp,
                        Id_EAdministrativa_Prov = idEAProv

                    };
                    db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Add(datos);

                    db.SaveChanges();

                }



                string tipoD = "Recerrador";

                if (tipo == "5")
                {
                    tipoD = "Interruptor de aceite";
                }
                else
                if (tipo == "6")
                {
                    tipoD = "Interruptor de SF6";
                }
                else
                if (tipo == "7")
                {
                    tipoD = "Interruptor de vacio";
                }
                else
                if ((tipo == "4") && (de != null))
                {

                    Inst_Nomenclador_Desconectivos d = db.Inst_Nomenclador_Desconectivos.Find(de.Id_Nomenclador);

                    if ((d!=null)&&(d.Descripcion == 8))
                    {
                        tipoD = "Seccionalizador";
                    }
                }
                return RedirectToAction("Details", "Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion", new { id = id, tipo = tipo, tipoD = tipoD });
            }
            else
            return RedirectToAction("Index");

        }

        // GET: InstalacionDesconectivos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InstalacionDesconectivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,id_EAdministrativa_Prov,CodigoNuevo,Id_EAdministrativa,NumAccion,NumeroFases,CorrienteNominal,TipoInstalacion,TipoSeccionalizador,CircuitoA,SeccionA,CircuitoB,SeccionB,Funcion,AjusteOperacion,IdFusible,Calle,Numero,Entrecalle1,EntreCalle2,BarrioPueblo,Sucursal,UbicadaEn,TieneCorrienteA,TieneCorrienteB,TieneCorrienteC,SiempreVoltaje,ClientesExtras,InstalacionAbrioA,InstalacionAbrioB,InstalacionAbrioC,FechaAbrioA,FechaAbrioB,FechaAbrioC,coddireccion,Id_EAdireccion,EstadoOperativo,EstadoFaseA,EstadoFaseB,EstadoFaseC,Tipo,Automatico")] InstalacionDesconectivos instalacionDesconectivos)
        {
            if (ModelState.IsValid)
            {
                db.InstalacionDesconectivos.Add(instalacionDesconectivos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(instalacionDesconectivos);
        }

        // GET: InstalacionDesconectivos/Edit/5
        public ActionResult Edit(string tipo, string cod, short idEA, int idEAProv)
        {
            Repositorio repo = new Repositorio(db);

            if (tipo == null || cod == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            if (tipo == "0") // el desconectivo es tipo puente
            {
                int id = db.Inst_Nomenclador_Puente.Where(c => c.Codigo == cod).Select(c => c.Id_Puente).FirstOrDefault();

                if (id == 0)
                {//el desconectivo no existe en la tabla puente y lo agrego
                    var id_puente = db.Database.SqlQuery<int>(@"declare @numPuente int
                                    Select @numPuente = Max(Id_Puente) + 1
                                    From Inst_Nomenclador_Puente
                                    if @numPuente is null
                                    set @numPuente = 1
                                    Select @numPuente as id_puente").First();
                    Inst_Nomenclador_Puente puente = new Inst_Nomenclador_Puente
                    {
                        Id_Administrativa = idEA,
                        NumAccion = repo.GetNumAccion("I", "GDD", 0),
                        Codigo = cod,
                        Id_Puente = id_puente,
                        id_EAdministrativa_Prov = idEAProv
                    };
                    db.Inst_Nomenclador_Puente.Add(puente);

                    db.SaveChanges();
                    id = id_puente;
                }
                return RedirectToAction("Edit", "Inst_Nomenclador_Puente", new { id = id });

            }

            if ((tipo == "1")||(tipo == "9")) // el desconectivo es tipo portafusible
            {

                int id = db.PortaFusibles.Where(c => c.CodigoPortafusible == cod).Select(c => c.Id_Portafusible).FirstOrDefault();
                short ea = db.PortaFusibles.Where(c => c.CodigoPortafusible == cod).Select(c => c.Id_EAdministrativa).FirstOrDefault();
                if (id != 0 || ea != 0)
                {
                    //el portafusible es de tipo fusible
                    return RedirectToAction("Edit", "PortaFusibles", new { ea = ea, id = id });
                }
                int idB = db.Breakers.Where(c => c.CodigoBreaker == cod).Select(c => c.Id_Breaker).FirstOrDefault();
                int eaB = db.Breakers.Where(c => c.CodigoBreaker == cod).Select(c => c.id_EAdministrativa).FirstOrDefault();
                if (idB != 0 || eaB != 0)
                {
                    //el portafusible es de tipo breaker
                    return RedirectToAction("Edit", "Breakers", new { ea = eaB, id = idB });
                }
                else
                {// no esta definido el tipo de desconectivo y se inserta por defecto como fusible
                    //agrego el fuse
                    var id_fuse = db.Database.SqlQuery<int>(@"declare @numFuse int
                                    Select @numFuse = Max(Id_Portafusible) + 1
                                    From PortaFusibles
                                    Where Id_EAdministrativa = {0}
                                    if @numFuse is null
                                    set @numFuse = 1
                                    Select @numFuse as idFuse", ea).First();
                    PortaFusibles p = new PortaFusibles
                    {
                        Id_EAdministrativa = idEA,
                        NumAccion = repo.GetNumAccion("I", "GDD", 0),
                        CodigoPortafusible = cod,
                        Id_Portafusible = id_fuse,
                        id_EAdministrativa_Prov = idEAProv
                    };
                    db.PortaFusibles.Add(p);

                    db.SaveChanges();
                    return RedirectToAction("Edit", "PortaFusibles", new { ea = idEA, id = id_fuse });
                }

            }
            if (tipo == "2") // el desconectivo es tipo cuchilla
            {
                int id = db.Inst_Nomenclador_Cuchillas.Where(c => c.Codigo == cod).Select(c => c.Id_Cuchilla).FirstOrDefault();
                if (id == 0)
                {//el desconectivo no existe en la tabla cuchilla y lo agrego con valores ficticios de fabricante, operacion, mando y tension
                    var id_cuchilla = db.Database.SqlQuery<int>(@"declare @numCuchilla int
                                    Select @numCuchilla = Max(Id_Cuchilla) + 1
                                    From Inst_Nomenclador_Cuchillas
                                    if @numCuchilla is null
                                    set @numCuchilla = 1
                                    Select @numCuchilla as id_cuchilla").First();

                    var id_fab = db.Database.SqlQuery<int>(@"declare @fab int
                                    Select @fab = Max(Id_Fabricante)
                                    From Fabricantes
                                    Select @fab as id_fab").First();

                    var id_mando = db.Database.SqlQuery<int>(@"declare @mando int
                                    Select @mando = Max(Id_Mando)
                                    From Inst_Nomenclador_Cuchillas_Mando
                                    Select @mando as id_mando").First();
                    var id_op = db.Database.SqlQuery<int>(@"declare @op int
                                    Select @op = Max(Id_Operacion)
                                    From Inst_Nomenclador_Cuchillas_Operacion
                                    Select @op as id_op").First();

                    var id_tension = db.Database.SqlQuery<int>(@"declare @t int
                                    Select @t = Max(Id_Tension)
                                    From Inst_Nomenclador_Cuchillas_Tension
                                    Select @t as id_tension").First();
                    Inst_Nomenclador_Cuchillas cuchilla = new Inst_Nomenclador_Cuchillas
                    {
                        Id_Administrativa = idEA,
                        NumAccion = repo.GetNumAccion("I", "GDD", 0),
                        Codigo = cod,
                        Id_Cuchilla = id_cuchilla,
                        id_EAdministrativa_Prov = idEAProv,
                        Id_Fabricante = id_fab,
                        Id_Mando = id_mando,
                        Id_Operacion = id_op,
                        Id_Tension = id_tension
                    };
                    db.Inst_Nomenclador_Cuchillas.Add(cuchilla);

                    db.SaveChanges();
                    id = id_cuchilla;
                }
                return RedirectToAction("Edit", "Inst_Nomenclador_Cuchillas", new { id = id });
            }
            if (tipo == "3") // el desconectivo es tipo interruptor de aire
            {
                int id = db.Inst_Nomenclador_InterruptorAire.Where(c => c.Codigo == cod).Select(c => c.Id_InterruptorAire).FirstOrDefault();
                var id_intAire = db.Database.SqlQuery<int>(@"declare @id int
                                    Select @id = Max(Id_InterruptorAire) + 1
                                    From Inst_Nomenclador_InterruptorAire
                                    if @id is null
                                    set @id = 1
                                    Select @id as idDatos").First();
                if (id == 0)
                {
                   
                    Inst_Nomenclador_InterruptorAire datosIntAire = new Inst_Nomenclador_InterruptorAire
                    {
                        //aqui si no existe el desconectivo de aire lo inserto con valor no null 
                        Id_Administrativa = idEA,
                        NumAccion = repo.GetNumAccion("I", "GDD", 0),
                        Codigo = cod,
                        Id_InterruptorAire = id_intAire,
                        id_EAdministrativa_Prov = idEAProv,
                        Id_Mando = 1,
                        Id_Tension =1,
                        Id_Operacion =1,
                        Id_Fabricante=3
                    };
                    db.Inst_Nomenclador_InterruptorAire.Add(datosIntAire);

                    db.SaveChanges();

                    id = id_intAire;
                }

                return RedirectToAction("Edit", "Inst_Nomenclador_InterruptorAire", new { id = id, inserta =1 });
            }

            if ((tipo == "4")|| (tipo == "5")|| (tipo == "6")||(tipo == "7") || (tipo == "8")) // el desconectivo es tipo recerrador, interruptor de aceite, interruptor sf6, interruptor vacío o Seccionalizador.
            {
                int id = db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Where(c => c.Codigo == cod).Select(c => c.Id_DatosEspComun).FirstOrDefault();
                int ea = db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Where(c => c.Codigo == cod).Select(c => c.Id_DatosEspComun).FirstOrDefault();
                Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion de = db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Find(id);

                if (de == null)//si ni existe el desconectivo en la tabla se inserta
                {
                    //int t = Integer.parseInt(tipo);
                    //var idnom = db.Inst_Nomenclador_Desconectivos.Where(c => c.Descripcion == t).Select(c => c.Id_Nomenclador).FirstOrDefault();
                    var id_datosEsp = db.Database.SqlQuery<int>(@"declare @id int
                                    Select @id = Max(Id_DatosEspComun) + 1
                                    From inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion
                                    if @id is null
                                    set @id = 1
                                    Select @id as idDatos").First();
                    Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion datos = new Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion
                    {
                        Id_Administrativa = idEA,
                        NumAccion = repo.GetNumAccion("I", "GDD", 0),
                        Codigo = cod,
                        Id_DatosEspComun = id_datosEsp,
                        Id_EAdministrativa_Prov = idEAProv

                    };
                    db.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion.Add(datos);

                    db.SaveChanges();

                    //return RedirectToAction("Edit", "Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion", new { ea = idEA, id = id_fuse });
                }



                string tipoD = "Recerrador";
               
                if (tipo == "5")
                {
                    tipoD = "Interruptor de aceite";
                }
                else
                if (tipo == "6")
                {
                     tipoD = "Interruptor de SF6";
                }
                else
                if (tipo == "7")
                {
                     tipoD = "Interruptor de vacio";
                }
                else
                if ((tipo == "4")&&(de.Id_Nomenclador!=null))
                {
                    
                    Inst_Nomenclador_Desconectivos d = db.Inst_Nomenclador_Desconectivos.Find(de.Id_Nomenclador);
                     
                    if(d.Descripcion == 8) { 
                      tipoD = "Seccionalizador";
                    }
                }
                return RedirectToAction("Edit", "Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion", new { id = id, tipo = tipo , tipoD = tipoD});
            }

            //else return PartialView("_VPDatosDesconectivos");
            var descLista = new DesconectivoSubestacionesRepositorio(db);
            ViewBag.desc = descLista.ListaDesconectivos();

            return RedirectToAction("Index");

        }

        // POST: InstalacionDesconectivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,id_EAdministrativa_Prov,CodigoNuevo,Id_EAdministrativa,NumAccion,NumeroFases,CorrienteNominal,TipoInstalacion,TipoSeccionalizador,CircuitoA,SeccionA,CircuitoB,SeccionB,Funcion,AjusteOperacion,IdFusible,Calle,Numero,Entrecalle1,EntreCalle2,BarrioPueblo,Sucursal,UbicadaEn,TieneCorrienteA,TieneCorrienteB,TieneCorrienteC,SiempreVoltaje,ClientesExtras,InstalacionAbrioA,InstalacionAbrioB,InstalacionAbrioC,FechaAbrioA,FechaAbrioB,FechaAbrioC,coddireccion,Id_EAdireccion,EstadoOperativo,EstadoFaseA,EstadoFaseB,EstadoFaseC,Tipo,Automatico")] InstalacionDesconectivos instalacionDesconectivos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instalacionDesconectivos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instalacionDesconectivos);
        }

        // GET: InstalacionDesconectivos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstalacionDesconectivos instalacionDesconectivos = db.InstalacionDesconectivos.Find(id);
            if (instalacionDesconectivos == null)
            {
                return HttpNotFound();
            }
            return View(instalacionDesconectivos);
        }

        // POST: InstalacionDesconectivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            InstalacionDesconectivos instalacionDesconectivos = db.InstalacionDesconectivos.Find(id);
            db.InstalacionDesconectivos.Remove(instalacionDesconectivos);
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
    }
}
