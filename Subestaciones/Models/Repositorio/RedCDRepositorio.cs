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
    public class RedCDRepositorio
    {

        private DBContext db;
        public RedCDRepositorio(DBContext db)
        {
            this.db = db;
        }

        public async Task<RedCD> FindAsync(int Id_ServicioCD)
        {
            var lista = await ObtenerListadoRedesCD();
            return lista.Find(c => c.ID == Id_ServicioCD);
        }

        public async Task<List<RedCD>> ObtenerListadoRedesCD()
        {
            return await (from redCD in db.Sub_RedCorrienteDirecta
                          join subD in db.Subestaciones on redCD.Codigo equals subD.Codigo
                          join volt in db.NomencladorTension on redCD.idVoltaje equals volt.idTension into voltaje
                          from subvolt in voltaje.DefaultIfEmpty()

                          select new RedCD
                          {
                              ID = redCD.Id_ServicioCD,
                              NumAccion = redCD.NumAccion,
                              id_EAdministrativa = redCD.id_EAdministrativa,
                              Codigo = subD.Codigo,
                              NombreServicioCD = redCD.NombreServicioCD,
                              NombreSub = subD.Codigo + ", " + subD.NombreSubestacion,
                              idVoltaje = subvolt.tension,
                              UsoRed = redCD.UsoRed,
                              ControlAislamBarras = redCD.ControlAislamBarras != null ? (redCD.ControlAislamBarras == true ? "Si" : "No") : "---",
                              Observaciones = redCD.Observaciones
                          })
               .Union
          (
                from redCD in db.Sub_RedCorrienteDirecta
                join subT in db.SubestacionesTransmision on redCD.Codigo equals subT.Codigo
                join volt in db.NomencladorTension on redCD.idVoltaje equals volt.idTension into voltaje
                from subvolt in voltaje.DefaultIfEmpty()

                select new RedCD
                {
                    ID = redCD.Id_ServicioCD,
                    NumAccion = redCD.NumAccion,
                    id_EAdministrativa = redCD.id_EAdministrativa,
                    Codigo = subT.Codigo,
                    NombreServicioCD = redCD.NombreServicioCD,
                    NombreSub = subT.Codigo + ", " + subT.NombreSubestacion,
                    idVoltaje = subvolt.tension,
                    UsoRed = redCD.UsoRed,
                    ControlAislamBarras = redCD.ControlAislamBarras != null ? (redCD.ControlAislamBarras == true ? "Si" : "No") : "---",
                    Observaciones = redCD.Observaciones


                }).ToListAsync();
        }

        public async Task<List<RedCD>> ObtenerListadoRedesCDSub(string sub)
        {
            return await (from redCD in db.Sub_RedCorrienteDirecta
                          join subD in db.Subestaciones on redCD.Codigo equals subD.Codigo
                          join volt in db.NomencladorTension on redCD.idVoltaje equals volt.idTension into voltaje
                          from subvolt in voltaje.DefaultIfEmpty()
                          where redCD.Codigo == sub
                          select new RedCD
                          {
                              Id_ServicioCD = redCD.Id_ServicioCD,
                              ID = redCD.Id_ServicioCD,
                              NumAccion = redCD.NumAccion,
                              id_EAdministrativa = redCD.id_EAdministrativa,
                              Codigo = subD.Codigo,
                              NombreServicioCD = redCD.NombreServicioCD,
                              NombreSub = subD.Codigo + ", " + subD.NombreSubestacion,
                              idVoltaje = subvolt.tension,
                              UsoRed = redCD.UsoRed,
                              ControlAislamBarras = redCD.ControlAislamBarras != null ? (redCD.ControlAislamBarras == true ? "Si" : "No") : "---",
                              Observaciones = redCD.Observaciones
                          })
               .Union
          (
                from redCD in db.Sub_RedCorrienteDirecta
                join subT in db.SubestacionesTransmision on redCD.Codigo equals subT.Codigo
                join volt in db.NomencladorTension on redCD.idVoltaje equals volt.idTension into voltaje
                from subvolt in voltaje.DefaultIfEmpty()
                where redCD.Codigo == sub
                select new RedCD
                {
                    Id_ServicioCD = redCD.Id_ServicioCD,
                    ID = redCD.Id_ServicioCD,
                    NumAccion = redCD.NumAccion,
                    id_EAdministrativa = redCD.id_EAdministrativa,
                    Codigo = subT.Codigo,
                    NombreServicioCD = redCD.NombreServicioCD,
                    NombreSub = subT.Codigo + ", " + subT.NombreSubestacion,
                    idVoltaje = subvolt.tension,
                    UsoRed = redCD.UsoRed,
                    ControlAislamBarras = redCD.ControlAislamBarras != null ? (redCD.ControlAislamBarras == true ? "Si" : "No") : "---",
                    Observaciones = redCD.Observaciones


                }).ToListAsync();
        }

        public SelectList UsoRed()
        {
            List<UsoRed> Listado = new List<UsoRed>();
            var uso = new UsoRed { usoRed = "Comunicaciones" };
            Listado.Add(uso);
            uso = new UsoRed { usoRed = "Operaciones" };
            Listado.Add(uso);


            var UsoDeLaRed = (from Lista in Listado
                              select new SelectListItem { Value = Lista.usoRed, Text = Lista.usoRed }).ToList();
            return new SelectList(UsoDeLaRed, "Value", "Text");
        }



    }
}