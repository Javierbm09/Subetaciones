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
    public class MttoPararrayosRepositorio
    {
        private DBContext db;
        public MttoPararrayosRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<MttosPararrayos> FindAsync(int? id)
        {
            var lista = await ObtenerListadoMP();
            return lista.Find(c => c.id_MttoPararrayo == id);
        }

        public async Task<List<MttosPararrayos>> ObtenerListadoMP()
        {
            return await db.Database.SqlQuery<MttosPararrayos>(@"SELECT M.id_EAdministrativa ,
                    id_MttoPararrayo ,
                    subestacion ,
                    CASE TequipoProt
                        WHEN 'L' THEN 'Línea'
                        WHEN 'B' THEN 'Barra'
                        WHEN 'T' THEN 'Transformador'
                        WHEN 'D' THEN 'Desconectivo'
                        WHEN 'TC' THEN 'TC'
                        WHEN 'TP' THEN 'TP'
                        WHEN 'TUP' THEN 'TUP'
                        WHEN 'I' THEN 'Interruptor'
                    END AS TequipoProt ,
                    CodigoEquipoProtegido ,
                    T.TipoMtto ,
                    FORMAT(fechaMantenimiento, 'dd/MM/yyyy') as fechaMantenimiento ,
                    Nombre ,
                    CASE 
		              WHEN Mantenido IS NULL THEN 'False'
		              ELSE Mantenido
		            END AS Mantenido
            FROM    dbo.Sub_MttoPararrayos M
                    INNER JOIN dbo.Personal P ON M.revisadoPor = P.Id_Persona
                    INNER JOIN dbo.TipoMantenimiento T ON M.tipoMantenimiento = T.IdTipoMtto").ToListAsync();
        }

        public async Task<SelectList> ObtenerListadoSubestaciones(string valor = "")
        {
            return new SelectList(await db.SubestacionesTransmision.OrderBy(o => o.Codigo).ThenBy(o => o.NombreSubestacion).Select(s => new
            { Text = s.Codigo + (s.NombreSubestacion != null ? " (" + s.NombreSubestacion + ")" : ""), Value = s.Codigo }).ToListAsync(), "Value", "Text", valor);
        }

        public async Task<SelectList> ObtenerListadoTipoMantenimiento(int? valor = 0)
        {
            return new SelectList(await db.TipoMantenimiento.Where(o => o.EsTipoMttoSubDist == true).ToListAsync(), "IdTipoMtto", "TipoMtto", valor != null ? (short)valor : 0);
        }

        public async Task<SelectList> ObtenerListadoRevisado(short? valor = 0)
        {
            return new SelectList(await db.Database.SqlQuery<ValueTextInt>(@"SELECT Id_Persona as Value, Nombre as Text FROM dbo.Personal P WHERE P.id_EAdministrativa_Prov=
                        (SELECT CAST(Configuracion.Dato AS INT) FROM dbo.Configuracion WHERE Configuracion.Id_Dato=1014) order by Nombre").ToListAsync(), "Value", "Text", valor != null ? (int)valor : 0);
        }

        public SelectList ObtenerListadoTipoEquipoProtegido(string valor = "")
        {
            return new SelectList(new Dictionary<string, string>() { { "L", "Línea" }, { "B", "Barra" }, { "T", "Transformador" },
                { "D", "Desconectivo" }, { "TC", "TC" }, { "TP","TP" }, { "TUP", "TUP" }, { "I", "Interruptor" } }, "Key", "Value", valor);
        }

        public SelectList ObtenerListadoDefectos(string valor = "")
        {
            return new SelectList(new List<string>() { "No Existe", "Bien", "Reparado", "Cambio", "Fuera de Servicio", "Salideros" }, valor);
        }

        public SelectList ObtenerListadoEstados(string valor = "")
        {
            return new SelectList(new List<string>() { "Bueno", "Malo", "No Existe", "No Procede" }, valor);
        }

        public async Task<SelectList> ObtenerListadoVoltajesInstalados(string subestacion, string valor = "")
        {
            return new SelectList(await db.Database.SqlQuery<ValueTextString>($@"Select DISTINCT Nombre as Value, VoltajeInstalado as Text
                FROM dbo.TransformadoresTransmision T INNER JOIN dbo.Sub_Pararrayos P ON T.Nombre = P.CE where  T.Codigo = '{subestacion}'")
                .ToListAsync(), "Text", "Text", valor);
        }

        public async Task<SelectList> ObtenerListadoInstrumentos()
        {
            return new SelectList(await db.Database.SqlQuery<ValueTextInt>(@"SELECT Id_InstrumentoMedicion AS Value, Instrumento AS Text
                FROM dbo.Sub_NomInstrumentoMedicion WHERE Id_TipoMedicion = 1 or Id_TipoMedicion=2").ToListAsync(), "Value", "Text");
        }

        public async Task<SelectList> ObtenerListadoInstrumentosSeleccionados(List<int> ids)
        {
            var lista = new List<ValueTextInt>();
            foreach (var item in ids)
            {
                lista.Add(new ValueTextInt
                {
                    Value = item,
                    Text = await db.Database.SqlQuery<string>($@"SELECT Instrumento AS Text
                    FROM dbo.Sub_NomInstrumentoMedicion WHERE Id_InstrumentoMedicion = {item}").FirstAsync()
                });
            }
            return new SelectList(lista, "Value", "Text");
        }

        public async Task<SelectList> ObtenerListadoInstrumentosDiferencia(List<int> ids)
        {
            var listaA = new List<ValueTextInt>();
            var listaB = await db.Database.SqlQuery<ValueTextInt>(@"SELECT Id_InstrumentoMedicion AS Value, Instrumento AS Text
                FROM dbo.Sub_NomInstrumentoMedicion WHERE Id_TipoMedicion = 1 or Id_TipoMedicion=2").ToListAsync();
            foreach (var item in ids)
            {
                listaA.Add(new ValueTextInt
                {
                    Value = item,
                    Text = await db.Database.SqlQuery<string>($@"SELECT Instrumento AS Text
                    FROM dbo.Sub_NomInstrumentoMedicion WHERE Id_InstrumentoMedicion = {item}").FirstAsync()
                });
            }
            return new SelectList(listaB.Except(listaA), "Value", "Text");
        }

        public async Task<List<string>> ObtenerListadoInstrumentosUtilizados(short idEA, int idMtto)
        {
            return await db.Database.SqlQuery<string>($@"SELECT Id_Instrumento FROM Sub_MttoPararrayos_Instrumentos 
                WHERE id_EAdministrativa = {idEA} AND id_MttoPararrayo = {idMtto}").ToListAsync();
        }

        public async Task<FasesPararrayos> ObtenerListadoFases(string tipo, string codigo, string fase)
        {
            return await db.Database.SqlQuery<FasesPararrayos>($@"SELECT NumeroSerie, TipoPararrayo FROM dbo.Sub_Pararrayos 
                where TequipoProt='{tipo}' and CE='{codigo}' and Fase = '{fase}' ").FirstOrDefaultAsync();
        }

        public async Task<Sub_MttoPararrayos_Fases> ObtenerDatosFases(short idEA, int idMtto, string fase)
        {
            return await db.Database.SqlQuery<Sub_MttoPararrayos_Fases>($@"SELECT MPF.* FROM Sub_MttoPararrayos_Fases MPF
                WHERE MPF.id_EAdministrativa = {idEA} AND MPF.id_MttoPararrayo = {idMtto} AND MPF.fase = '{fase}'").FirstOrDefaultAsync();
        }

        public async Task<SelectList> ObtenerListadoEquipos(string subestacion, string tipoEquipo, string valor = "")
        {
            switch (tipoEquipo)
            {
                case "L":
                    return new SelectList(await db.Database.SqlQuery<string>($@"
                        SELECT DISTINCT Codigolinea CE FROM SubestacionesCabezasLineas L
                        INNER JOIN Sub_Pararrayos P ON L.Codigolinea = P.CE
                        WHERE L.SubestacionTransmicion = '{subestacion}'").ToListAsync(), valor);
                case "B":
                    return new SelectList(await db.Database.SqlQuery<string>($@"
                        SELECT DISTINCT B.codigo CE FROM Sub_Barra B
                        INNER JOIN dbo.Sub_Pararrayos P ON B.codigo = P.CE
                        WHERE B.Subestacion = '{subestacion}'").ToListAsync(), valor);
                case "T":
                    return new SelectList(await db.Database.SqlQuery<string>($@"
                        Select DISTINCT Nombre CE FROM dbo.TransformadoresTransmision T
                        INNER JOIN dbo.Sub_Pararrayos P ON T.Nombre = P.CE
                        WHERE T.Codigo = '{subestacion}'").ToListAsync(), valor);
                case "D":
                    return new SelectList(await db.Database.SqlQuery<string>($@"
                        SELECT DISTINCT D.codigo CE FROM dbo.InstalacionDesconectivos D
                        INNER JOIN dbo.Sub_Pararrayos P ON D.Codigo = P.CE
                        WHERE (TipoSeccionalizador<4) and Ubicadaen = '{subestacion}'").ToListAsync(), valor);
                case "TC":
                    return new SelectList(await db.Database.SqlQuery<string>($@"
                        SELECT DISTINCT Nro_Serie CE FROM dbo.ES_TransformadorCorriente TC
                        INNER JOIN dbo.Sub_Pararrayos P ON TC.Nro_Serie = P.CE
                        WHERE CodSub = '{subestacion}'").ToListAsync(), valor);
                case "TP":
                    return new SelectList(await db.Database.SqlQuery<string>($@"
                        SELECT DISTINCT Nro_Serie CE FROM ES_TransformadorPotencial TP
                        INNER JOIN dbo.Sub_Pararrayos P ON TP.Nro_Serie = P.CE
                        WHERE CodSub = '{subestacion}'").ToListAsync(), valor);
                case "TUP":
                    return new SelectList(await db.Database.SqlQuery<string>($@"
                        SELECT DISTINCT TUP.Codigo CE FROM BancoTransformadores TUP
                        INNER JOIN dbo.Sub_Pararrayos P ON TUP.Codigo = P.CE
                        WHERE circuito = '{subestacion}'").ToListAsync(), valor);
                case "I":
                    return new SelectList(await db.Database.SqlQuery<string>($@"
                        SELECT DISTINCT I.Codigo CE FROM InstalacionDesconectivos I
                        INNER JOIN dbo.Sub_Pararrayos P ON I.Codigo = P.CE
                        WHERE (TipoSeccionalizador>=4) and UbicadaEn = '{subestacion}'").ToListAsync(), valor);
                default:
                    return new SelectList(new List<string>());
            }
        }
    }
}