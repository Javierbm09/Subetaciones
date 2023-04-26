using Subestaciones.Models.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Subestaciones.Models.Repositorio
{
    public class Sub_CelajeInspSubTransRepositorio
    {
        private DBContext db;
        public Sub_CelajeInspSubTransRepositorio(DBContext db)
        {
            this.db = db;
        }

        public async Task<List<Sub_Celaje_Sub_TransViewModel>> ObtenerSub_CelajeSubTrans()
        {
            return await db.Database.SqlQuery<Sub_Celaje_Sub_TransViewModel>(@"SELECT  dbo.SubestacionesTransmision.NombreSubestacion NombreSub ,
                                                        dbo.Sub_Celaje.CodigoSub ,
                                                        dbo.Sub_Celaje.NombreCelaje ,
                                                        dbo.Sub_Celaje.fecha ,
                                                        dbo.Sub_Celaje.NumAccion ,
                                                        dbo.Sub_Celaje.id_EAdministrativa ,
                                                        dbo.Personal.Nombre RealizadoPor
                                                FROM    dbo.Sub_Celaje
                                                        INNER JOIN dbo.SubestacionesTransmision ON dbo.Sub_Celaje.CodigoSub = dbo.SubestacionesTransmision.Codigo
                                                        INNER JOIN dbo.Personal ON dbo.Sub_Celaje.RealizadoPor = dbo.Personal.Id_Persona;").ToListAsync();
        }

        public List<SelectListItem> tipoInsp()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Mensual", Text= "Mensual" },
               new SelectListItem { Value= "Especial", Text= "Especial" }};

        }
    }
}