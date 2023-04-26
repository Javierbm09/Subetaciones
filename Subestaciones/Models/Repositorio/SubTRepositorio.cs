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
    public class SubTRepositorio
    {

        private DBContext db;
        public SubTRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<SubestacionesTrans> FindAsync(string codigo)
        {
            var lista = await ObtenerListadoSubT();
            return lista.Find(c => c.Codigo == codigo);
        }

        public async Task<List<SubestacionesTrans>> ObtenerListadoSubT()
        {
            return await (from subT in db.SubestacionesTransmision
                          join EO in db.Nomenclador_EstadoOperativo on subT.EstadoOperativo equals EO.Id_EstadoOperativo into SubEO
                          from EstadoOperativo in SubEO.DefaultIfEmpty()
                          join volt in db.VoltajesSistemas on subT.VoltajePrimario equals volt.Id_VoltajeSistema into voltaje
                          from subvolt in voltaje.DefaultIfEmpty()
                          join esquema in db.EsquemasAlta on subT.EsquemaPorAlta equals esquema.Id_EsquemaAlta into EsquemaxAlta
                          from EsquemasAlta in EsquemaxAlta.DefaultIfEmpty()
                          //join bloques in db.Bloque on subT.Codigo equals bloques.Codigo into BloqueD
                          //from bloqueTransf in BloqueD.DefaultIfEmpty()
                          select new SubestacionesTrans
                          {
                              Codigo = subT.Codigo,
                              CodigoAntiguo = subT.CodigoAntiguo,
                              TipoSubestacion = subT.TipoSub,
                              NumeroSalidas = subT.NumeroSalidas,
                              NombreSubestacion = subT.NombreSubestacion,
                              TipoTercero = subT.Tipo_Terceros,
                              EsquemaAlta = EsquemasAlta.EsquemaPorAlta,
                              EstadoOperativo = EstadoOperativo.EstadoOperativo,
                              VoltajeNominal = subvolt.Voltaje
                             

                          }).ToListAsync();
        }

        public async Task<List<SubestacionesCabezasLineas>> ObtenerListaLineas(string sub)
        {
            List<SubestacionesCabezasLineas> lineas = new List<SubestacionesCabezasLineas>();

            if (!String.IsNullOrEmpty(sub))
            {

                string consulta = "select * from SubestacionesCabezasLineas where SubestacionTransmicion=@sub";


                lineas = await db.Database.SqlQuery<SubestacionesCabezasLineas>(consulta, new SqlParameter("sub", sub)).ToListAsync();
            }

            return lineas;

        }

        public async Task<List<TransformadorTransmision>> ObtenerListaTansformadores(string sub)
        {
            List<TransformadorTransmision> transfs = new List<TransformadorTransmision>();

            if (!String.IsNullOrEmpty(sub))
            {

                string consulta = (@"SELECT T.Id_EAdministrativa Id_EAdministrativa,T.Id_Transformador Id_Transformador, T.NumAccion NumAccion, T.Codigo Subestacion, T.Nombre Nombre, 
                                    T.Numemp Numemp, C.Capacidad Capacidad,T.Fase Fase, T.NoSerie NoSerie, T.NumFase NumFase, T.EstadoOperativo EstadoOperativo, Vs1.Voltaje Id_VoltajePrim,     
                                    Vs2.Voltaje Id_Voltaje_Secun, Vs3.Voltaje VoltajeTerciario,
                                    T.AnnoFabricacion AnnoFabricacion, T.PorcientoImpedancia PorcientoImpedancia, T.GrupoConexion GrupoConexion, T.PesoTotal PesoTotal,
                                    T.CorrienteAlta CorrientePrimaria, T.FrecuenciaN Frecuencia, T.TipoEnfriamiento Enfriamiento,
                                    T.PerdidasVacio PerdidasVacio, T.PerdidasBajoCarga PerdidasBajoCarga, T.NivelRuido NivelRuido, T.VoltajeImpulso TensionImpulso, T.PesoAceite,
                                    T.PesoNucleo PesoNucleo, T.NivelRadioInterf NivelRadioInterf, T.CorrienteBaja CorrienteSecundaria,
                                    T.Tipo Tipo, T.CantVentiladores CantVentiladores, T.CantRadiadores CantRadiadores, T.Observaciones Observaciones, T.PesoTansporte PesoTansporte,
                                    T.TipoRegVoltaje TipoRegVoltaje, T.NroPosiciones NroPosiciones, T.TipoCajaMando TipoCajaMando, T.TuboExplosor TuboExplosor, T.ValvulaSobrePresion ValvulaSobrePresion,
                                    T.PosicionTrabajo PosicionTrabajo, T.NumeroInventario NumeroInventario, T.FechaDeInstalado FechaDeInstalado
                                    FROM TransformadoresTransmision T
                                    left join Capacidades C on T.Id_Capacidad = C.Id_Capacidad
                                    left join VoltajesSistemas as VS1 on T.Id_VoltajePrim = VS1.Id_VoltajeSistema
                                    left join VoltajesSistemas as VS2 on T.Id_Voltaje_Secun = VS2.Id_VoltajeSistema
                                    left join VoltajesSistemas as VS3 on T.VoltajeTerciario = VS3.Id_VoltajeSistema
                                    where Codigo = @sub");


                transfs = await db.Database.SqlQuery<TransformadorTransmision>(consulta, new SqlParameter("sub", sub)).ToListAsync();
            }

            return transfs;

        }

        
    }
}