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
    public class CapacitorRepositorio
    {
        private DBContext db;
        public CapacitorRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<Capacitores> FindAsync(string CodigoBanco)
        {
            var lista = await ObtenerListadoCapacitores();
            return lista.Find(c => c.codigobanco == CodigoBanco);
        }

        public async Task<List<Capacitores>> ObtenerListadoCapacitores()
        {
            return await (from cap in db.BancoCapacitores
                          join subD in db.Subestaciones on cap.Circuito equals subD.Codigo
                          join eo in db.Nomenclador_EstadoOperativo on cap.EstadoOperativo equals eo.Id_EstadoOperativo into estOp
                          from subEO in estOp.DefaultIfEmpty()
                          select new Capacitores
                          {
                              codigosub = subD.Codigo,//circuito
                              NombreSub = subD.Codigo + ", " + subD.NombreSubestacion,
                              codigobanco = cap.Codigo,
                              codigoantiguo = cap.CodigoAntiguo,
                              secc = cap.Seccionalizador,
                              EO = subEO.EstadoOperativo,
                              tipocontrol = cap.TipoControl,
                              CKVAR_Instalado = cap.CKVAR_Instalado,
                              Calle = subD.Calle,
                              Numero = subD.Numero,
                              Entrecalle1 = subD.Entrecalle1,
                              Entrecalle2 = subD.Entrecalle2,
                              BarrioPueblo = subD.BarrioPueblo,
                              Sucursal = subD.Sucursal

                          })
               .Union
          (from cap in db.BancoCapacitores
           join subT in db.SubestacionesTransmision on cap.Circuito equals subT.Codigo
           join eo in db.Nomenclador_EstadoOperativo on cap.EstadoOperativo equals eo.Id_EstadoOperativo into estOp
           from subEO in estOp.DefaultIfEmpty()
           select new Capacitores
           {
               codigosub = subT.Codigo,//circuito
               NombreSub = subT.Codigo + ", " + subT.NombreSubestacion,
               codigobanco = cap.Codigo,
               codigoantiguo = cap.CodigoAntiguo,
               secc = cap.Seccionalizador,
               EO = subEO.EstadoOperativo,
               tipocontrol = cap.TipoControl,
               CKVAR_Instalado = cap.CKVAR_Instalado,
               Calle = subT.Calle,
               Numero = subT.Numero,
               Entrecalle1 = subT.Entrecalle1,
               Entrecalle2 = subT.Entrecalle2,
               BarrioPueblo = subT.BarrioPueblo,
               Sucursal = subT.Sucursal

           }).ToListAsync();
        }
        public async Task<List<InstalacionDesconectivos>> seccionalizador()
        {
            return await db.Database.SqlQuery<InstalacionDesconectivos>(@"select * 
                                                                          from InstalacionDesconectivos secc").ToListAsync();
        }

        public async Task<List<InstalacionDesconectivos>> seccionalizador(string cod)
        {
            var codigo = new SqlParameter("@cod", cod);

            return await db.Database.SqlQuery<InstalacionDesconectivos>(@"select * 
                                                                          from InstalacionDesconectivos secc 
                                                                          where secc.CircuitoA = @cod", codigo).ToListAsync();
        }

        public async Task<List<Nomenclador_EstadoOperativo>> EO()
        {
            return await (from eo in db.Nomenclador_EstadoOperativo 
                          select eo).ToListAsync();
        }

        //De la siguiente manera llenamos manualmente,
        //Siendo el campo Text lo que ve el usuario y 
        //el campo Value lo que en realidad vale nuestro valor
        public SelectList TipoC()
        {
            List<TipoControlBC> Listado = new List<TipoControlBC>();
            var tipo = new TipoControlBC { tipo = "Ninguno" };
            Listado.Add(tipo);
            tipo = new TipoControlBC { tipo = "Voltaje" };
            Listado.Add(tipo);
            tipo = new TipoControlBC { tipo = "Reactivo" };
            Listado.Add(tipo);
            tipo = new TipoControlBC { tipo = "Tiempo" };
            Listado.Add(tipo);
            var tipoBC = (from Lista in Listado
                               select new SelectListItem { Value = Lista.tipo, Text = Lista.tipo }).ToList();
            return new SelectList(tipoBC, "Value", "Text");
            
        }

    }
}