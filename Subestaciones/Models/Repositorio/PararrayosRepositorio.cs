using Subestaciones.Models.Clases;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
//using System.Data.Lin

namespace Subestaciones.Models.Repositorio
{
    public class PararrayosRepositorio
    {
        private DBContext db;
        public PararrayosRepositorio(DBContext db)
        {
            this.db = db;
        }
        public Pararrayos Find(short EA, int id_para)
        {
            var lista = ObtenerListadoPara();
            return lista.Find(c => c.Id_EAdministrativa == EA && c.Id_pararrayo == id_para);
        }
        public List<Pararrayos> ObtenerListadoPara()
        {
            return db.Database.SqlQuery<Pararrayos>(@"select Id_EAdministrativa = para.Id_EAdministrativa,
                          Id_pararrayo = para.Id_pararrayo,
                          EquipoProteg = para.TequipoProt,
                          CASE para.TequipoProt
                              WHEN 'L' THEN 'Línea'
                              WHEN 'B' THEN 'Barra'
                              WHEN 'T' THEN 'Transformador'
                              WHEN 'D' THEN 'Desconectivo'
                              WHEN 'TC' THEN 'TC'
                              WHEN 'TP' THEN 'TP'
                              WHEN 'TUP' THEN 'TUP'
                              WHEN 'I' THEN 'Interruptor' end AS
                          TipoEquipoProteg,
                          NombreSub = subD.Codigo + ', ' + subD.NombreSubestacion,
                          Codigo = para.CE,
                          Fabricante = para.Fabricante,
                          Tipo = para.TipoPararrayo,
                          VInst = para.VoltajeInstalado,
                          tension = voltaje.VoltNomPararrayo,
                          MOCV = para.MOCV,
                          CorrienteN = corr.CorrNomPararrayo,
                          Fase = para.Fase,
                          NumeroSerie = para.NumeroSerie,
                          Inventario = para.Inventario,
                          Material = para.Material,
                          Frecuencia = para.Frecuencia,
                          Aislamiento = para.Aislamiento,
                          Clase = para.Clase,
                          AñoFabricacion = para.AñoFabricacion,
                          Instalado = para.Instalado
                      from Sub_Pararrayos para
                      LEFT JOIN Sub_LineasSubestacion lineas on(para.CE = lineas.Circuito) and(para.Codigo = lineas.Subestacion)
                      LEFT JOIN CorrientesNominalesPararrayos corr on para.Id_CorrienteN = corr.Id_CorrNomPararrayo
                      LEFT JOIN VoltajesNominalesPararrayos voltaje on para.Id_Voltaje = voltaje.Id_VoltNomPararrayo
                      INNER JOIN Subestaciones subD on para.Codigo = subD.Codigo
                    union
                    select Id_EAdministrativa = para.Id_EAdministrativa,
                          Id_pararrayo = para.Id_pararrayo,
                          EquipoProteg = para.TequipoProt,
                          CASE para.TequipoProt
                              WHEN 'L' THEN 'Línea'
                              WHEN 'B' THEN 'Barra'
                              WHEN 'T' THEN 'Transformador'
                              WHEN 'D' THEN 'Desconectivo'
                              WHEN 'TC' THEN 'TC'
                              WHEN 'TP' THEN 'TP'
                              WHEN 'TUP' THEN 'TUP'
                              WHEN 'I' THEN 'Interruptor' end AS
                          TipoEquipoProteg,
                          NombreSub = subT.Codigo + ', ' + subT.NombreSubestacion,
                          Codigo = para.CE,
                          Fabricante = para.Fabricante,
                          Tipo = para.TipoPararrayo,
                          VInst = para.VoltajeInstalado,
                          tension = voltaje.VoltNomPararrayo,
                          MOCV = para.MOCV,
                          CorrienteN = corr.CorrNomPararrayo,
                          Fase = para.Fase,
                          NumeroSerie = para.NumeroSerie,
                          Inventario = para.Inventario,
                          Material = para.Material,
                          Frecuencia = para.Frecuencia,
                          Aislamiento = para.Aislamiento,
                          Clase = para.Clase,
                          AñoFabricacion = para.AñoFabricacion,
                          Instalado = para.Instalado
                      from Sub_Pararrayos para
                      LEFT JOIN Sub_LineasSubestacion lineas on(para.CE = lineas.Circuito) and(para.Codigo = lineas.Subestacion)
                      LEFT JOIN CorrientesNominalesPararrayos corr on para.Id_CorrienteN = corr.Id_CorrNomPararrayo
                      LEFT JOIN VoltajesNominalesPararrayos voltaje on para.Id_Voltaje = voltaje.Id_VoltNomPararrayo
                      INNER JOIN SubestacionesTransmision subT on para.Codigo = subT.Codigo ").ToList();
        }

        public SelectList TipoEquipo()
        {
            List<TipoEquipoPararrayo> Listado = new List<TipoEquipoPararrayo>();
            var TE = new TipoEquipoPararrayo { Id_TEPararrayo="L", TEPararrayo="Línea" };
            Listado.Add(TE);
            TE = new TipoEquipoPararrayo { Id_TEPararrayo = "B", TEPararrayo = "Barra" };
            Listado.Add(TE);
            TE = new TipoEquipoPararrayo { Id_TEPararrayo = "T", TEPararrayo = "Transformador" };
            Listado.Add(TE);
            TE = new TipoEquipoPararrayo { Id_TEPararrayo = "D", TEPararrayo = "Desconectivo" };
            Listado.Add(TE);
            TE = new TipoEquipoPararrayo { Id_TEPararrayo = "TC", TEPararrayo = "TC" };
            Listado.Add(TE);
            TE = new TipoEquipoPararrayo { Id_TEPararrayo = "TP", TEPararrayo = "TP" };
            Listado.Add(TE);
            TE = new TipoEquipoPararrayo { Id_TEPararrayo = "TUP", TEPararrayo = "TUP" };
            Listado.Add(TE);
            TE = new TipoEquipoPararrayo { Id_TEPararrayo = "I", TEPararrayo = "Interruptor" };
            Listado.Add(TE);
            var Tipo = (from Lista in Listado
                     select new SelectListItem { Value = Lista.Id_TEPararrayo, Text = Lista.TEPararrayo }).ToList();
            return new SelectList(Tipo, "Value", "Text");
        }

        public SelectList Fase()
        {
            List<Fase> Listado = new List<Fase>();
            var F = new Fase { Id_Fase = "A", NombreFase = "A" };
            Listado.Add(F);
             F = new Fase { Id_Fase = "B", NombreFase = "B" };
            Listado.Add(F);
             F = new Fase { Id_Fase = "C", NombreFase = "C" };
            Listado.Add(F);

            
            var Fase = (from Lista in Listado
                        select new SelectListItem { Value = Lista.Id_Fase, Text = Lista.NombreFase }).ToList();
            return new SelectList(Fase, "Value", "Text");
        }

        public SelectList VoltajeInstalado()
        {
            List<VoltajeInstalado> Listado = new List<VoltajeInstalado>();
            var volt = new VoltajeInstalado { Idvoltaje = "220", voltaje = "220 kv"};
            Listado.Add(volt);
            volt = new VoltajeInstalado { Idvoltaje = "110", voltaje = "110 kv" };
            Listado.Add(volt);
            volt = new VoltajeInstalado { Idvoltaje = "34.5", voltaje = "34.5 kv" };
            Listado.Add(volt);
            volt = new VoltajeInstalado { Idvoltaje = "13.8", voltaje = "13.8 kv" };
            Listado.Add(volt);
            volt = new VoltajeInstalado { Idvoltaje = "4.33", voltaje = "4.33 kv" };
            Listado.Add(volt);
            var VoltInst = (from Lista in Listado
                        select new SelectListItem { Value = Lista.Idvoltaje, Text = Lista.voltaje }).ToList();
            return new SelectList(VoltInst, "Value", "Text");
        }

        public SelectList Material()
        {
            List<MaterialPararrayo> Listado = new List<MaterialPararrayo>();
            var mat = new MaterialPararrayo { material = "Óxido Metálico" };
            Listado.Add(mat);
            mat = new MaterialPararrayo { material = "Carburo Silicio" };
            Listado.Add(mat);
            
            var material = (from Lista in Listado
                            select new SelectListItem { Value = Lista.material, Text = Lista.material }).ToList();
            return new SelectList(material, "Value", "Text");
        }

        public SelectList Frecuencia()
        {
            List<FrecuenciaPararrayo> Listado = new List<FrecuenciaPararrayo>();
            var frec = new FrecuenciaPararrayo { frecuencia = "50" };
            Listado.Add(frec);
            frec = new FrecuenciaPararrayo { frecuencia = "60" };
            Listado.Add(frec);
            frec = new FrecuenciaPararrayo { frecuencia = "50/60" };
            Listado.Add(frec);

            var frecuenciaP = (from Lista in Listado
                            select new SelectListItem { Value = Lista.frecuencia, Text = Lista.frecuencia }).ToList();
            return new SelectList(frecuenciaP, "Value", "Text");
        }

        public SelectList Aislamiento()
        {
            List<Aislamiento> Listado = new List<Aislamiento>();
            var aislamiento = new Aislamiento { aisl = "Porcelana" };
            Listado.Add(aislamiento);
            aislamiento = new Aislamiento { aisl = "Polímero" };
            Listado.Add(aislamiento);
            

            var aislamientoP = (from Lista in Listado
                               select new SelectListItem { Value = Lista.aisl, Text = Lista.aisl }).ToList();
            return new SelectList(aislamientoP, "Value", "Text");
        }

        public SelectList Clase()
        {
            List<Clase> Listado = new List<Clase>();
            var clasep = new Clase { clasepara = "Distribución" };
            Listado.Add(clasep);
            clasep = new Clase { clasepara = "Intermedio" };
            Listado.Add(clasep);
            clasep = new Clase { clasepara = "Estación" };
            Listado.Add(clasep);

            var frecuenciaP = (from Lista in Listado
                               select new SelectListItem { Value = Lista.clasepara, Text = Lista.clasepara }).ToList();
            return new SelectList(frecuenciaP, "Value", "Text");
        }
    }
}