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
    public class BancoCargadoresRepositorio
    {
        private DBContext db;
        public BancoCargadoresRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<BancoCargadores> FindAsync(int Id_Cargador, int IdredCD)
        {
            var lista = await ObtenerCargadores(IdredCD);
            return lista.Find(c => c.Id_Cargador == Id_Cargador);
        }

        public async Task<List<BancoCargadores>> ObtenerCargadores(int IdredCD)
        {
            return await (from cargadores in db.Sub_Cargador
                          join redCD in db.Sub_RedCorrienteDirecta on cargadores.Id_ServicioCDCarg equals redCD.Id_ServicioCD
                          join fabricante in db.Fabricantes on cargadores.Fabricante equals fabricante.Id_Fabricante into Fab
                          from FabCargador in Fab.DefaultIfEmpty()
                          join nomCargador in db.Sub_NomCargadores on cargadores.tipo equals nomCargador.IdCargador into nomCargadores
                          from Nomenclador in nomCargadores.DefaultIfEmpty()
                          join redCA in db.Sub_RedCorrienteAlterna on cargadores.Id_RedCA equals redCA.Id_RedCA into redes
                          from CA in redes.DefaultIfEmpty()
                          where cargadores.Id_ServicioCDCarg == IdredCD
                          select new BancoCargadores
                          {
                              id_EAdministrativa = cargadores.id_EAdministrativa,
                              Id_ServicioCDCarg = cargadores.Id_ServicioCDCarg,
                              NumAccion = cargadores.NumAccion,
                              Id_Cargador = cargadores.Id_Cargador,
                              Id_RedCA = CA.NombreServicioCA,
                              tipoCargador = Nomenclador.TipoCargador,
                              volCA = Nomenclador.VoltajeCA,
                              volCD = Nomenclador.VoltajeCorrienteDirecta,
                              corriente = Nomenclador.Corriente,
                              NombreServicio = redCD.NombreServicioCD,
                              tipo = Nomenclador.TipoCargador,
                              EstOp = cargadores.EstOp,
                              Modelo = cargadores.Modelo,
                              MesFab = cargadores.MesFab,
                              AnnoFab = cargadores.AnnoFab,
                              VoltajeMaxCD = cargadores.VoltajeMaxCD,
                              VoltajeMinCD = cargadores.VoltajeMinCD,
                              Fabricante = FabCargador.Nombre,
                              NroSerie = cargadores.NroSerie,
                              FechaInstalado = cargadores.FechaInstalado
                          }).ToListAsync();
        }

        public List<Sub_NomCargadores> tipoCargadores()
        {
            return (from tipoCargador in db.Sub_NomCargadores
                    select tipoCargador).ToList();
        }

        public List<Fabricantes> fab()
        {
            return (from Fabricantes in db.Fabricantes
                    where Fabricantes.FCargadoresSub == true
                    select Fabricantes).ToList();
        }

        public List<Sub_RedCorrienteAlterna> redCA(string cod)
        {
            return (from redes in db.Sub_RedCorrienteAlterna
                    where redes.Codigo == cod
                    select redes).ToList();
        }

        public SelectList EstadoO()
        {
            List<Fase> Listado = new List<Fase>();
            var F = new Fase { Id_Fase = "NA", NombreFase = "NA" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "NE", NombreFase = "NE" };
            Listado.Add(F);

            var Fase = (from Lista in Listado
                        select new SelectListItem { Value = Lista.Id_Fase, Text = Lista.NombreFase }).ToList();
            return new SelectList(Fase, "Value", "Text");
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