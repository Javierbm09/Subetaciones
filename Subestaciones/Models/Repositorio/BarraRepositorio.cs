using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subestaciones.Models;
using Subestaciones.Models.Clases;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Subestaciones.Models.Repositorio
{

    public class BarraRepositorio

    {
        private DBContext db;
        public BarraRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<Barras> FindAsync(string Codigosub, string Codigobarra)
        {
            var lista = await ObtenerListadoBarras();
            return lista.Find(c => c.sub == Codigosub && c.barra == Codigobarra);
        }

        public async Task<List<Barras>> ObtenerListadoBarras()
        {
            return await (from barra in db.Sub_Barra
                          join subD in db.Subestaciones on barra.Subestacion equals subD.Codigo
                          join volt in db.VoltajesSistemas on barra.ID_Voltaje equals volt.Id_VoltajeSistema into voltaje
                          from subvolt in voltaje.DefaultIfEmpty()
                          select new Barras
                          {
                              sub = subD.Codigo,
                              barra = barra.codigo,
                              NombreSub = subD.Codigo + ", " + subD.NombreSubestacion,
                              cond = barra.Conductor,
                              corr = barra.corriente,
                              cantC = barra.CantidadCond,
                              tension = subvolt.Voltaje

                          })
               .Union
          (from barra in db.Sub_Barra
           join subT in db.SubestacionesTransmision on barra.Subestacion equals subT.Codigo
           join volt in db.VoltajesSistemas on barra.ID_Voltaje equals volt.Id_VoltajeSistema into voltaje
           from subvolt in voltaje.DefaultIfEmpty()
           select new Barras
           {
               sub = subT.Codigo,
               barra = barra.codigo,
               NombreSub = subT.Codigo + ", " + subT.NombreSubestacion,
               cond = barra.Conductor,
               corr = barra.corriente,
               cantC = barra.CantidadCond,
               tension = subvolt.Voltaje
           }).ToListAsync();
        }

        // esto es para mostrar los conductores
        public List<Conductor> conductores()
        {
            return (from conductor in db.conductor
                    join tCond in db.TipoConductor on conductor.Id_TipoConductor equals tCond.Id_Tipo
                    join mat in db.Material on conductor.Id_material equals mat.Id_Material
                    join calibre in db.Seccion on conductor.Id_Seccion equals calibre.Id_Seccion
                    join recub in db.recubrimiento on conductor.Id_recubre equals recub.Id_recubre
                    select new Conductor
                    {
                        codigo = conductor.Codigo,
                        TCond = tCond.Tipo,
                        material = mat.Tipo,
                        calibre = calibre.Calibre,
                        recubrimiento = recub.Tipo

                    }).ToList();
        }

        public async Task<List<VoltajesSistemas>> voltaje()
        {
            return await (from tension in db.VoltajesSistemas
                          select tension).ToListAsync();
        }       
    }
    
}