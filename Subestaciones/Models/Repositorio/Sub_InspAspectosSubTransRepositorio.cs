using Subestaciones.Models.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Subestaciones.Models.Repositorio
{
    public class Sub_InspAspectosSubTransRepositorio
    {
        private DBContext db;
        public Sub_InspAspectosSubTransRepositorio(DBContext db)
        {
            this.db = db;
        }

        public async Task<Sub_InspAspectosSubTransViewModel> ObtenerSub_InspAspectosSubTrans(string codigo, DateTime fecha,int aspecto)
        {
            var parametroCod = new SqlParameter("@codigo", codigo);
            var parametroFecha = new SqlParameter("@fecha", fecha);
            var parametroAspecto = new SqlParameter("@aspecto", aspecto);
            return await db.Database.SqlQuery<Sub_InspAspectosSubTransViewModel>(@"SELECT  Sub_InspAspectosSubTrans.NombreCelaje ,
                                                                Sub_InspAspectosSubTrans.CodigoSub ,
                                                                dbo.Sub_InspAspectosSubTrans.fecha ,
                                                                Sub_NomAspectoInspSubTrans.Aspecto as NombreAspecto ,
                                                                dbo.Sub_InspAspectosSubTrans.Aspecto ,
                                                                dbo.Sub_InspAspectosSubTrans.Defecto ,
                                                                dbo.Sub_InspAspectosSubTrans.Observaciones
                                                        FROM    Sub_InspAspectosSubTrans
                                                                INNER JOIN dbo.Sub_NomAspectoInspSubTrans ON Sub_NomAspectoInspSubTrans.Id_Aspecto = Sub_InspAspectosSubTrans.Aspecto
                                                                WHERE (Sub_InspAspectosSubTrans.CodigoSub = @codigo) AND 
                                                                      (Sub_InspAspectosSubTrans.fecha = @fecha) AND
                                                                      (Sub_InspAspectosSubTrans.Aspecto = @aspecto )", parametroCod, parametroFecha, parametroAspecto).FirstOrDefaultAsync();
        }

        public List<SelectListItem> defectos()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Tiene", Text= "Tiene" },
               new SelectListItem { Value= "No Tiene", Text= "No Tiene" },
               new SelectListItem { Value="No Procede", Text="No Procede"}};

        }

        public async Task<List<Sub_InspAspectosSubTransViewModel>> ObtenerSub_InspAspectos(string codigo, DateTime fecha)
        {            
            var parametroCod = new SqlParameter("@codigo", codigo);
            var parametroFecha = new SqlParameter("@fecha", fecha);
            return await db.Database.SqlQuery<Sub_InspAspectosSubTransViewModel>(@"SELECT  Sub_InspAspectosSubTrans.NombreCelaje ,
                                                                Sub_InspAspectosSubTrans.CodigoSub ,
                                                                dbo.Sub_InspAspectosSubTrans.fecha ,
                                                                dbo.Sub_InspAspectosSubTrans.Aspecto ,
                                                                dbo.Sub_InspAspectosSubTrans.Defecto ,
                                                                dbo.Sub_InspAspectosSubTrans.Observaciones,
                                                                Sub_NomAspectoInspSubTrans.Aspecto as NombreAspecto 
                                                        FROM    Sub_InspAspectosSubTrans
                                                                INNER JOIN dbo.Sub_NomAspectoInspSubTrans ON Sub_NomAspectoInspSubTrans.Id_Aspecto = Sub_InspAspectosSubTrans.Aspecto 
																WHERE (Sub_InspAspectosSubTrans.CodigoSub = @codigo) AND 
                                                                      (Sub_InspAspectosSubTrans.fecha = @fecha)", parametroCod, parametroFecha).ToListAsync();

        }
    }
}