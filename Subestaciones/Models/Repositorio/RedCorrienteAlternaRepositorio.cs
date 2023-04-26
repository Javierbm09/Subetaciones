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
    public class RedCorrienteAlternaRepositorio
    {
        private DBContext db;

        public RedCorrienteAlternaRepositorio(DBContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BancosTransformadores>> ObtenerListadoBancosTransformadores(string codigoSubestacion)
        {
            return await (from BT in db.BancoTransformadores
                          where BT.Circuito.Equals(codigoSubestacion)
                          select new BancosTransformadores { Codigo = BT.Codigo, Seccionalizador = BT.Seccionalizador }).ToListAsync();
        }

        public async Task<IEnumerable<ListaTransformadores>> ObtenerListadoTransformadores(string codigoBancoTransformador)
        {
            return await (from T in db.Transformadores
                          join F in db.Fabricantes on (int)T.Id_Fabricante equals F.Id_Fabricante
                                 into Fjoined
                          from fabricante in Fjoined.DefaultIfEmpty()
                          join C in db.Capacidades on T.Id_Capacidad equals C.Id_Capacidad
                                 into Cjoined
                          from capacidades in Cjoined.DefaultIfEmpty()
                          join VP in db.VoltajesSistemas on T.Id_VoltajePrim equals VP.Id_VoltajeSistema
                                 into VPjoined
                          from voltajePrimario in VPjoined.DefaultIfEmpty()
                          join VS in db.VoltajesSistemas on T.Id_Voltaje_Secun equals VS.Id_VoltajeSistema
                                 into VSjoined
                          from voltajeSecundario in VSjoined.DefaultIfEmpty()
                          where T.Codigo == codigoBancoTransformador
                          select new ListaTransformadores
                          {
                              Codigo = T.Codigo,
                              Numemp = T.Numemp,
                              Capacidad = capacidades.Capacidad,
                              Fabricante = fabricante.Nombre,
                              VoltajePrim = voltajePrimario.Voltaje,
                              VoltajeSecun = voltajeSecundario.Voltaje,
                              Fase = T.Fase,
                              EstadoOperativo = T.EstadoOperativo,
                              TapDejado = T.TapDejado,
                              NumFase = T.NumFase
                          }).ToListAsync();
        }

        public async Task<IEnumerable<ListaTransformadores>> ObtenerListadoTransformadoresBanco(string codigoSub)
        {
            return await (from T in db.Transformadores
                          join F in db.Fabricantes on (int)T.Id_Fabricante equals F.Id_Fabricante
                                 into Fjoined
                          from fabricante in Fjoined.DefaultIfEmpty()
                          join C in db.Capacidades on T.Id_Capacidad equals C.Id_Capacidad
                                 into Cjoined
                          from capacidades in Cjoined.DefaultIfEmpty()
                          join VP in db.VoltajesSistemas on T.Id_VoltajePrim equals VP.Id_VoltajeSistema
                                 into VPjoined
                          from voltajePrimario in VPjoined.DefaultIfEmpty()
                          join VS in db.VoltajesSistemas on T.Id_Voltaje_Secun equals VS.Id_VoltajeSistema
                                 into VSjoined
                          from voltajeSecundario in VSjoined.DefaultIfEmpty()
                          join BT in db.BancoTransformadores on T.Codigo equals BT.Codigo
                          where BT.Circuito == codigoSub
                          select new ListaTransformadores
                          {
                              Codigo = T.Codigo,
                              Numemp = T.Numemp,
                              Seleccionalizador = BT.Seccionalizador,
                              Capacidad = capacidades.Capacidad,
                              Fabricante = fabricante.Nombre,
                              VoltajePrim = voltajePrimario.Voltaje,
                              VoltajeSecun = voltajeSecundario.Voltaje,
                              Fase = T.Fase,
                              EstadoOperativo = T.EstadoOperativo,
                              TapDejado = T.TapDejado,
                              NumFase = T.NumFase
                          }).ToListAsync();
        }

        public async Task<IEnumerable<BreakerPorBaja>> ObtenerListaBreakers(string codigoSubestacion, int? idRedCA)
        {
            return await (from BPB in db.Sub_DesconectivoSubestacion
                          where BPB.CodigoSub == codigoSubestacion && BPB.RedCA == idRedCA
                          select new BreakerPorBaja
                          {
                              CodigoDesconectivo = BPB.CodigoDesconectivo,
                              TensionNominal = BPB.TensionNominal,
                              CorrienteNominal = BPB.CorrienteNominal
                          }).ToListAsync();
        }
    }
}