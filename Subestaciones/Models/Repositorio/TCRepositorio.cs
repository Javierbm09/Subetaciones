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
    public class TCRepositorio
    {
        private DBContext db;
        public TCRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<TC> FindAsync(string Nro_Serie)
        {
            var lista = await ObtenerListadoTC();
            return lista.Find(c => c.Nro_Serie == Nro_Serie);
        }

        public async Task<List<TC>> ObtenerListadoTC()
        {
            return await (from TC in db.ES_TransformadorCorriente
                          join subD in db.Subestaciones on TC.CodSub equals subD.Codigo
                          join volt in db.VoltajesSistemas on TC.id_Voltaje_Nominal equals volt.Id_VoltajeSistema into voltaje
                          from subvolt in voltaje.DefaultIfEmpty()


                          select new TC
                          {
                              CodSub = subD.Codigo,
                              Nro_Serie = TC.Nro_Serie,
                              Fase = TC.Fase,
                              Relacion_Transformacion = TC.Relacion_Transformacion,
                              Cant_Devanado = TC.Cant_Devanado,
                              Frecuencia = TC.Frecuencia,
                              Fs_Fi = TC.Fs_Fi,
                              VoltInst = subvolt.Voltaje,
                              UbicadoEn = TC.Tipo_Equipo_Primario,
                              CodigoE = TC.Elemento_Electrico,
                              Id_EAdministrativa = TC.Id_EAdministrativa,
                              NumAccion = TC.NumAccion,
                              Tipo = TC.Tipo,
                              Inventario = TC.Inventario,
                              AnnoFab = TC.AnnoFab,
                              FechaInstalado = TC.FechaInstalado,
                              InPrimaria = TC.InPrimaria,
                              Fabricante = TC.Fabricante

                          })
               .Union
          (from TC in db.ES_TransformadorCorriente
           join subT in db.SubestacionesTransmision on TC.CodSub equals subT.Codigo
           join volt in db.VoltajesSistemas on TC.id_Voltaje_Nominal equals volt.Id_VoltajeSistema into voltaje
           from subvolt in voltaje.DefaultIfEmpty()

           select new TC
           {
               CodSub = subT.Codigo,
               Nro_Serie = TC.Nro_Serie,
               Fase = TC.Fase,
               Relacion_Transformacion = TC.Relacion_Transformacion,
               Cant_Devanado = TC.Cant_Devanado,
               Frecuencia = TC.Frecuencia,
               Fs_Fi = TC.Fs_Fi,
               VoltInst = subvolt.Voltaje,
               UbicadoEn = TC.Tipo_Equipo_Primario,
               CodigoE = TC.Elemento_Electrico,
               Id_EAdministrativa = TC.Id_EAdministrativa,
               NumAccion = TC.NumAccion,
               Tipo = TC.Tipo,
               Inventario = TC.Inventario,
               AnnoFab = TC.AnnoFab,
               FechaInstalado = TC.FechaInstalado,
               InPrimaria = TC.InPrimaria,
               Fabricante = TC.Fabricante

           }).ToListAsync();
        }

        public SelectList CantDevanados(short? valor)
        {
            var lista = new SelectList(new List<SelectListItem>
            {
                new SelectListItem{ Value = "1", Text = "1"},
                new SelectListItem{ Value = "2", Text = "2"},
                new SelectListItem{ Value = "3", Text = "3"}
            },
            "Value", "Text");
            return lista;
        }

        public SelectList Tension(string val)
        {
            var lista = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "100", Text = "100"},
                new SelectListItem{ Value = "110", Text = "110"},
                new SelectListItem{ Value = "120", Text = "120"} ,
                new SelectListItem { Value = "100/√3", Text = "100/√3" },
                new SelectListItem { Value = "110/√3", Text = "110/√3" } ,
                new SelectListItem { Value = "120/√3", Text = "120/√3" }
            },
                "Value", "Text");
            return lista;
        }


        public SelectList Fase(string val)
        {
            var lista = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "A  ", Text = "A"},
                new SelectListItem{ Value = "B  ", Text = "B"},
                new SelectListItem{ Value = "C  ", Text = "C"}
            },
                "Value", "Text");

            return lista;
        }

        public SelectList InTrabajoPrimaria()
        {
            var lista = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "50", Text = "50"},
                new SelectListItem{ Value = "100", Text = "100"},
                new SelectListItem{ Value = "200", Text = "200"},
                new SelectListItem{ Value = "300", Text = "300"},
                new SelectListItem{ Value = "400", Text = "400"},
                new SelectListItem{ Value = "600", Text = "600"},
                new SelectListItem{ Value = "1200", Text = "1200"}
            },
                "Value", "Text");

            return lista;
        }

        public SelectList InPrimaria()
        {
            var lista = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "50-100", Text = "50-100"},
                new SelectListItem{ Value = "100-200", Text = "100-200"},
                new SelectListItem{ Value = "200-400", Text = "200-400"},
                new SelectListItem{ Value = "300-600", Text = "300-600"},
                new SelectListItem{ Value = "600-1200", Text = "600-1200"}

            },
                "Value", "Text");

            return lista;
        }

        public SelectList InSecundaria()
        {
            var lista = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "1"},
                new SelectListItem{ Value = "5", Text = "5"}
            },
                "Value", "Text");

            return lista;
        }

        public List<SelectListItem> Frecuencia(double? val)
        {
            //List<FrecuenciaPararrayo> Listado = new List<FrecuenciaPararrayo>();
            //var frec = new FrecuenciaPararrayo { frecuencia = "50" };
            //Listado.Add(frec);
            //frec = new FrecuenciaPararrayo { frecuencia = "60" };
            //Listado.Add(frec);

            //var frecuenciaP = (from Lista in Listado
            //                   select new SelectListItem { Value = Lista.frecuencia, Text = Lista.frecuencia }).ToList();
            //return new SelectList(frecuenciaP, "Value", "Text");
            List<SelectListItem> lista = new List<SelectListItem>();
            SelectListItem item = new SelectListItem();
            item.Value = "50";
            item.Text = "50";
            if (val == 50)
            {
                item.Selected = true;
            }
            lista.Add(item);
            item = new SelectListItem();
            item.Value = "60";
            item.Text = "60";
            if (val == 60)
            {
                item.Selected = true;
            }

            lista.Add(item);

            return lista;
        }

        public SelectList TipoEquipo()
        {
            List<TipoEquipoPararrayo> Listado = new List<TipoEquipoPararrayo>();
            var TE = new TipoEquipoPararrayo { Id_TEPararrayo = "Interruptor", TEPararrayo = "Interruptor" };
            Listado.Add(TE);
            TE = new TipoEquipoPararrayo { Id_TEPararrayo = "Barra", TEPararrayo = "Barra" };
            Listado.Add(TE);
            TE = new TipoEquipoPararrayo { Id_TEPararrayo = "Transformador", TEPararrayo = "Transformador" };
            Listado.Add(TE);
            TE = new TipoEquipoPararrayo { Id_TEPararrayo = "Generador", TEPararrayo = "Generador" };
            Listado.Add(TE);

            var Tipo = (from Lista in Listado
                        select new SelectListItem { Value = Lista.Id_TEPararrayo, Text = Lista.TEPararrayo }).ToList();
            return new SelectList(Tipo, "Value", "Text");
        }

        public IEnumerable<VoltajesSistemas> VoltajeInstalado()
        {
            return (from tension in db.VoltajesSistemas
                    where tension.TCUNom == true
                    select tension).ToList();
        }

        public IEnumerable<ES_TC_Devanado> devanadosTC(string NoSerie)
        {
            return (from devanados in db.ES_TC_Devanado
                    where devanados.Nro_TC.Equals(NoSerie)
                    select devanados).ToList();
        }

        public async Task<List<ES_TC_Devanado>> ObtenerListaDevanados(string SerieTC)
        {
            List<ES_TC_Devanado> devanadosTC = new List<ES_TC_Devanado>();

            string consulta = "select * from ES_TC_Devanado where Nro_TC =@tc";
                devanadosTC = await db.Database.SqlQuery<ES_TC_Devanado>(consulta, new SqlParameter("tc", SerieTC)).ToListAsync();
           
            return devanadosTC;

        }

        public void ActualizarDevanado(short Nro_Dev, string Nro_Serie, float Capacidad, string Clase_Precision, string Designacion)
        {
            ES_TC_Devanado devanado = db.ES_TC_Devanado.Find(Nro_Dev, Nro_Serie);
            if (devanado != null)
            {
                devanado.Nro_Dev = Nro_Dev;
                devanado.Nro_TC = Nro_Serie;
                devanado.Capacidad = Capacidad;
                devanado.Clase_Precision = Clase_Precision;
                devanado.Designacion = Designacion;
                db.Entry(devanado).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException();
            }

        }

        [HttpPost]
        public void EliminarDevanado(short dev, string serie)
        {

            ES_TC_Devanado devanado = db.ES_TC_Devanado.Find(dev, serie);

            if (devanado != null)
            {
                try
                {
                    db.ES_TC_Devanado.Remove(devanado);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
           
        }

        public async Task<List<TC>> ObtenerTCSub(string sub)
        {
            return await (from TC in db.ES_TransformadorCorriente
                          join subD in db.Subestaciones on TC.CodSub equals subD.Codigo
                          join volt in db.VoltajesSistemas on TC.id_Voltaje_Nominal equals volt.Id_VoltajeSistema into voltaje
                          from subvolt in voltaje.DefaultIfEmpty()
                          where TC.CodSub == sub

                          select new TC
                          {
                              CodSub = subD.Codigo,
                              Nro_Serie = TC.Nro_Serie,
                              Fase = TC.Fase,
                              Relacion_Transformacion = TC.Relacion_Transformacion,
                              Cant_Devanado = TC.Cant_Devanado,
                              Frecuencia = TC.Frecuencia,
                              Fs_Fi = TC.Fs_Fi,
                              VoltInst = subvolt.Voltaje,
                              UbicadoEn = TC.Tipo_Equipo_Primario,
                              CodigoE = TC.Elemento_Electrico,
                              Id_EAdministrativa = TC.Id_EAdministrativa,
                              NumAccion = TC.NumAccion,
                              Tipo = TC.Tipo,
                              Inventario = TC.Inventario,
                              AnnoFab = TC.AnnoFab,
                              FechaInstalado = TC.FechaInstalado,
                              InPrimaria = TC.InPrimaria,
                              Fabricante = TC.Fabricante

                          })
               .Union
          (from TC in db.ES_TransformadorCorriente
           join subT in db.SubestacionesTransmision on TC.CodSub equals subT.Codigo
           join volt in db.VoltajesSistemas on TC.id_Voltaje_Nominal equals volt.Id_VoltajeSistema into voltaje
           from subvolt in voltaje.DefaultIfEmpty()
           where TC.CodSub == sub
           select new TC
           {
               CodSub = subT.Codigo,
               Nro_Serie = TC.Nro_Serie,
               Fase = TC.Fase,
               Relacion_Transformacion = TC.Relacion_Transformacion,
               Cant_Devanado = TC.Cant_Devanado,
               Frecuencia = TC.Frecuencia,
               Fs_Fi = TC.Fs_Fi,
               VoltInst = subvolt.Voltaje,
               UbicadoEn = TC.Tipo_Equipo_Primario,
               CodigoE = TC.Elemento_Electrico,
               Id_EAdministrativa = TC.Id_EAdministrativa,
               NumAccion = TC.NumAccion,
               Tipo = TC.Tipo,
               Inventario = TC.Inventario,
               AnnoFab = TC.AnnoFab,
               FechaInstalado = TC.FechaInstalado,
               InPrimaria = TC.InPrimaria,
               Fabricante = TC.Fabricante

           }).ToListAsync();

        }
    }
}