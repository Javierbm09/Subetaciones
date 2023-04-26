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
    public class IMRepositorio
    {
        private DBContext db;
        public IMRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<InstMedicion> FindAsync(int? id)
        {
            var lista = await ObtenerListadoIM();
            return lista.Find(c => c.Id_InstrumentoMedicion == id);
        }

        public async Task<List<InstMedicion>> ObtenerListadoIM()
        {
            return await (from InstMedicion in db.Sub_NomInstrumentoMedicion
                          join mediciones in db.Sub_NomTipoMedicion on InstMedicion.Id_TipoMedicion equals mediciones.Id_TipoMedicion
                          
                          select new InstMedicion
                          {Id_InstrumentoMedicion= InstMedicion.Id_InstrumentoMedicion,
                              TipoMedicion = mediciones.NombreTipoMedicion,
                              Instrumento = InstMedicion.Instrumento,
                              ModeloTipo = InstMedicion.ModeloTipo,
                              Serie = InstMedicion.Serie,
                              Fabricante = InstMedicion.Fabricante,
                              AnoFabricacion = InstMedicion.AnoFabricacion,
                              Pais = InstMedicion.Pais,
                              FechaVerificado = InstMedicion.FechaVerificado,
                              FechaVencimiento = InstMedicion.FechaVencimiento,
                              Estado = InstMedicion.Estado,
                              BrigadaResponsable = InstMedicion.BrigadaResponsable

                          }).ToListAsync();
        }

        //para listar los tipos de mediciones
        public async Task<List<Sub_NomTipoMedicion>> TipoMediciones()
        {
            return await (from TipoMediciones in db.Sub_NomTipoMedicion
                          select TipoMediciones).ToListAsync();
        }

        public SelectList Estados()
        {
            List<TipoControlBC> Listado = new List<TipoControlBC>();
            var tipo = new TipoControlBC { tipo = "Apto" };
            Listado.Add(tipo);
            tipo = new TipoControlBC { tipo = "No Apto" };
            Listado.Add(tipo);
            tipo = new TipoControlBC { tipo = "Laboratorio" };
            Listado.Add(tipo);
            tipo = new TipoControlBC { tipo = "Baja" };
            Listado.Add(tipo);

            var est = (from Lista in Listado
                         select new SelectListItem { Value = Lista.tipo, Text = Lista.tipo }).ToList();
            return new SelectList(est, "Value", "Text");

        }

    }
}