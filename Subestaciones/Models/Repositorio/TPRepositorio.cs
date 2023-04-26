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
    public class TPRepositorio
    {
        private DBContext db;
        public TPRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<TP> FindAsync(string Nro_Serie)
        {
            var lista = await ObtenerListadoTP();
            return lista.Find(c => c.Nro_Serie == Nro_Serie);
        }

        public async Task<List<TP>> ObtenerListadoTP()
        {
            return await (from TP in db.ES_TransformadorPotencial
                          join subD in db.Subestaciones on TP.CodSub equals subD.Codigo
                          join volt in db.VoltajesSistemas on TP.id_Voltaje_Primario equals volt.Id_VoltajeSistema into voltaje
                          from subvolt in voltaje.DefaultIfEmpty()


                          select new TP
                          {
                              CodSub = subD.Codigo,
                              Nro_Serie = TP.Nro_Serie,
                              Fase = TP.Fase,
                              Cant_Devanado = TP.Cant_Devanado,
                              Frecuencia = TP.Frecuencia,
                              VoltInst = subvolt.Voltaje,
                              UbicadoEn = TP.Tipo_Equipo_Primario,
                              CodigoE = TP.Elemento_Electrico,
                              Id_EAdministrativa = TP.Id_EAdministrativa,
                              NumAccion = TP.NumAccion,
                              Tipo = TP.Tipo,
                              Inventario = TP.Inventario,
                              AnnoFab = TP.AnnoFab,
                              FechaInstalado = TP.FechaInstalado,
                              Fabricante = TP.Fabricante

                          })
               .Union
          (from TP in db.ES_TransformadorPotencial
           join subT in db.SubestacionesTransmision on TP.CodSub equals subT.Codigo
           join volt in db.VoltajesSistemas on TP.id_Voltaje_Primario equals volt.Id_VoltajeSistema into voltaje
           from subvolt in voltaje.DefaultIfEmpty()

           select new TP
           {
               CodSub = subT.Codigo,
               Nro_Serie = TP.Nro_Serie,
               Fase = TP.Fase,
               Cant_Devanado = TP.Cant_Devanado,
               Frecuencia = TP.Frecuencia,
               VoltInst = subvolt.Voltaje,
               UbicadoEn = TP.Tipo_Equipo_Primario,
               CodigoE = TP.Elemento_Electrico,
               Id_EAdministrativa = TP.Id_EAdministrativa,
               NumAccion = TP.NumAccion,
               Tipo = TP.Tipo,
               Inventario = TP.Inventario,
               AnnoFab = TP.AnnoFab,
               FechaInstalado = TP.FechaInstalado,
               Fabricante = TP.Fabricante

           }).ToListAsync();
        }

        public SelectList Fase(string val)
        {
            var lista = new SelectList(new List<SelectListItem>{ new SelectListItem { Value = "A  ", Text = "A"},
            new SelectListItem{ Value = "B  ", Text = "B"}, new SelectListItem{ Value = "C  ", Text = "C"} }, "Value", "Text");
            //List<Fase> Listado = new List<Fase>();
            //var F = new Fase { Id_Fase = "A", NombreFase = "A" };
            //Listado.Add(F);
            //F = new Fase { Id_Fase = "B", NombreFase = "B" };
            //Listado.Add(F);
            //F = new Fase { Id_Fase = "C", NombreFase = "C" };
            //Listado.Add(F);

            //List<SelectListItem> lista = new List<SelectListItem>();
            //SelectListItem item = new SelectListItem();
            //item.Value = "A";
            //item.Text = "A";
            //if (val == "A")
            //{
            //    item.Selected = true;
            //}            
            //lista.Add(item);
            //item = new SelectListItem();
            //item.Value = "B";
            //item.Text = "B";
            //if (val == "B")
            //{
            //    item.Selected = true;
            //}
            //lista.Add(item);
            //item = new SelectListItem();
            //item.Value = "C";
            //item.Text = "C";
            //if (val == "C")
            //{
            //    item.Selected = true;
            //}
            //lista.Add(item);


            //var Fase = (from Lista in Listado
            //            select new SelectListItem { Value = Lista.Id_Fase, Text = Lista.NombreFase }).ToList();
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

        public async Task<List<TP>> ObtenerTPSub(string sub)
        {
            return await (from TP in db.ES_TransformadorPotencial
                          join subD in db.Subestaciones on TP.CodSub equals subD.Codigo
                          join volt in db.VoltajesSistemas on TP.id_Voltaje_Primario equals volt.Id_VoltajeSistema into voltaje
                          from subvolt in voltaje.DefaultIfEmpty()
                          where TP.CodSub == sub

                          select new TP
                          {
                              CodSub = subD.Codigo,
                              Nro_Serie = TP.Nro_Serie,
                              Fase = TP.Fase,
                              Cant_Devanado = TP.Cant_Devanado,
                              Frecuencia = TP.Frecuencia,
                              VoltInst = subvolt.Voltaje,
                              UbicadoEn = TP.Tipo_Equipo_Primario,
                              CodigoE = TP.Elemento_Electrico,
                              Id_EAdministrativa = TP.Id_EAdministrativa,
                              NumAccion = TP.NumAccion,
                              Tipo = TP.Tipo,
                              Inventario = TP.Inventario,
                              AnnoFab = TP.AnnoFab,
                              FechaInstalado = TP.FechaInstalado,
                              Fabricante = TP.Fabricante

                          })
               .Union
          (from TP in db.ES_TransformadorPotencial
           join subT in db.SubestacionesTransmision on TP.CodSub equals subT.Codigo
           join volt in db.VoltajesSistemas on TP.id_Voltaje_Primario equals volt.Id_VoltajeSistema into voltaje
           from subvolt in voltaje.DefaultIfEmpty()
           where TP.CodSub == sub
           select new TP
           {
               CodSub = subT.Codigo,
               Nro_Serie = TP.Nro_Serie,
               Fase = TP.Fase,
               Cant_Devanado = TP.Cant_Devanado,
               Frecuencia = TP.Frecuencia,
               VoltInst = subvolt.Voltaje,
               UbicadoEn = TP.Tipo_Equipo_Primario,
               CodigoE = TP.Elemento_Electrico,
               Id_EAdministrativa = TP.Id_EAdministrativa,
               NumAccion = TP.NumAccion,
               Tipo = TP.Tipo,
               Inventario = TP.Inventario,
               AnnoFab = TP.AnnoFab,
               FechaInstalado = TP.FechaInstalado,
               Fabricante = TP.Fabricante

           }).ToListAsync();
        }

        public async Task<List<ES_TP_Devanado>> ObtenerListaDevanados(string SerieTP)
        {
            List<ES_TP_Devanado> devanadosTP = new List<ES_TP_Devanado>();

            string consulta = "select * from ES_TP_Devanado where Nro_TP =@tp";
            devanadosTP = await db.Database.SqlQuery<ES_TP_Devanado>(consulta, new SqlParameter("tp", SerieTP)).ToListAsync();

            return devanadosTP;

        }

        public void ActualizarDevanado(short Nro_Dev, string Nro_Serie, float Capacidad,string tension, string Clase_Precision, string Designacion)
        {
            ES_TP_Devanado devanado = db.ES_TP_Devanado.Find(Nro_Dev, Nro_Serie);
            if (devanado != null)
            {
                devanado.Nro_Dev = Nro_Dev;
                devanado.Nro_TP = Nro_Serie;
                devanado.Capacidad = Capacidad;
                devanado.ClasePresicion = Clase_Precision;
                devanado.Designacion = Designacion;
                devanado.Tension = tension;
                db.Entry(devanado).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException();
            }

        }
    }
}