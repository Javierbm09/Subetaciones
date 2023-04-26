using Subestaciones.Models.Clases;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Subestaciones.Models.Repositorio
{
    public class BateriasRepositorio
    {
        private DBContext db;
        public BateriasRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<Baterias> FindAsync(int? Id_Bateria, int IdredCD)
        {
            var lista = await ObtenerBaterias(IdredCD);
            return lista.Find(c => c.Id_Bateria == Id_Bateria);
        }

        public async Task<List<Baterias>> ObtenerBaterias(int IdredCD)
        {
            return await (from bateria in db.Sub_Baterias
                                        join redCD in db.Sub_RedCorrienteDirecta on bateria.Id_ServicioCDBat equals redCD.Id_ServicioCD
                                        join fabricante in db.Fabricantes on bateria.Fabricante equals fabricante.Id_Fabricante into Fab
                                        from FabBateria in Fab.DefaultIfEmpty()
                                        join nomBateria in db.NomencladorBaterias on bateria.Tipo equals nomBateria.IdBateria  into nomBaterias
                                        from Nomenclador in nomBaterias.DefaultIfEmpty()
                                        where bateria.Id_ServicioCDBat == IdredCD
                                        select new Baterias
                                        {
                                            id_EAdministrativa = bateria.id_EAdministrativa,
                                            NumAccion = bateria.NumAccion,
                                            Id_Bateria = bateria.Id_Bateria,
                                            Id_ServicioCDBat = bateria.Id_ServicioCDBat,
                                            modelo = Nomenclador.TipoBateria,
                                            TipoBat = Nomenclador.ClaseBateria,
                                            Tension = Nomenclador.TensionBateria,
                                            Capacidad = Nomenclador.CapacidadBateria,
                                            Densisdad = bateria.Densisdad,
                                            CantidadVasos = bateria.CantidadVasos,
                                            PesoCadaVaso = bateria.PesoCadaVaso,
                                            VoltajeFlotacion = bateria.VoltajeFlotacion,
                                            CantVasosPilotos = bateria.CantVasosPilotos,
                                            CantElectVasos = bateria.CantElectVasos,
                                            MesFab = bateria.MesFab,
                                            AnnoFab = bateria.AnnoFab,
                                            Fabricante = FabBateria.Nombre + ", " + FabBateria.Pais,
                                            VoltajeEcualizanteCargaBat = bateria.VoltajeEcualizanteCargaBat,
                                            TiempoCargaBat = bateria.TiempoCargaBat,
                                            PeriodicidadCargaBat = bateria.PeriodicidadCargaBat,
                                            Voltaje_Vasos = bateria.Voltaje_Vasos,
                                            Instalado = bateria.FechaInstalado

                                        }).ToListAsync();
        }
        public List<NomencladorBaterias> tipoBaterias()
        {
            return (from tipoBateria in db.NomencladorBaterias
                    select tipoBateria).ToList();
        }

        public List<Fabricantes> fab()
        {
            return (from Fabricantes in db.Fabricantes
                    where Fabricantes.FBateriasSub == true
                    select Fabricantes).ToList();
        }


        public NomencladorBaterias tipoBaterias(int id)
        {
            return (from tipoBateria in db.NomencladorBaterias
                    where tipoBateria.IdBateria == id
                    select tipoBateria).FirstOrDefault();
        }

        public SelectList mes()
        {
            List<Fase> Listado = new List<Fase>();
            var F = new Fase { Id_Fase = "1", NombreFase = "Enero" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "2", NombreFase = "Febrero" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "3", NombreFase = "Marzo" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "4", NombreFase = "Abril" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "5", NombreFase = "Mayo" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "6", NombreFase = "Junio" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "7", NombreFase = "Julio" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "8", NombreFase = "Agosto" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "9", NombreFase = "Septiembre" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "10", NombreFase = "Octubre" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "11", NombreFase = "Noviembre" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "12", NombreFase = "Diciembre" };
            Listado.Add(F);

            var Fase = (from Lista in Listado
                        select new SelectListItem { Value = Lista.Id_Fase, Text = Lista.NombreFase }).ToList();
            return new SelectList(Fase, "Value", "Text");
        }
    }
}