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
    public class MedicionesRepo
    {

        private DBContext db;
        public MedicionesRepo(DBContext db)
        {
            this.db = db;
        }
        public async Task<MedicionesTierra> Find(string sub, DateTime fecha)
        {
            var lista = await ObtenerListadoMediciones();
            return lista.Find(c => c.Subestacion == sub && c.Fecha == fecha);
        }

        public async Task<List<MedicionesTierra>> ObtenerListadoMediciones()
        {
            return ((from med in db.Sub_MedicionTierra
                     join subD in db.Subestaciones on med.Subestacion equals subD.Codigo
                     select new MedicionesTierra
                     {
                         SubestacionNombre = subD.Codigo + ", " + subD.NombreSubestacion,
                         Subestacion = med.Subestacion,
                         Empresa = med.Empresa,
                         Fecha = med.Fecha,
                         Id_EAdministrativa = (short)med.Id_EAdministrativa,
                         RealizadaPor = med.RealizadaPor
                     }).Union
          (from med in db.Sub_MedicionTierra
           join subT in db.SubestacionesTransmision on med.Subestacion equals subT.Codigo
           select new MedicionesTierra
           {
               SubestacionNombre = subT.Codigo + ", " + subT.NombreSubestacion,
               Subestacion = med.Subestacion,
               Empresa = med.Empresa,
               Fecha = med.Fecha,
               Id_EAdministrativa = (short)med.Id_EAdministrativa,
               RealizadaPor = med.RealizadaPor

           })).ToList();
        }

        public List<SelectListItem> tipoMalla()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value= "Electrodo", Text= "Electrodo" },
                new SelectListItem { Value= "Grupo de electrodo", Text= "Grupo de electrodo" },
                new SelectListItem { Value= "Grandes dimensiones", Text= "Grandes dimensiones" } };
               
        } 

        public List<SelectListItem> estadoSuelo()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value= "Suelo seco", Text= "Suelo seco" },
                new SelectListItem { Value= "Suelo húmedo", Text= "Suelo húmedo" },
                new SelectListItem { Value= "Carsificación", Text= "Carsificación" } };
        }

        public List<SelectListItem> tiposSuelos()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value= "Relleno", Text= "Relleno" },
                new SelectListItem { Value= "Limos", Text= "Limos" },
                new SelectListItem { Value= "Arcillas", Text= "Arcillas" },
                new SelectListItem { Value= "Margas", Text= "Margas" },
                new SelectListItem { Value= "Caliza", Text= "Caliza" },
                new SelectListItem { Value= "Limonitas", Text= "Limonitas" },
                new SelectListItem { Value= "Areniscas", Text= "Areniscas" },
                new SelectListItem { Value= "Serpentina", Text= "Serpentina" },
                new SelectListItem { Value= "Arena", Text= "Arena" },
                new SelectListItem { Value= "Gravas", Text= "Gravas" },
                new SelectListItem { Value= "Eluvio", Text= "Eluvio" }};

        }

        public List<SelectListItem> SN()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value= "Si", Text= "Si" },
                new SelectListItem { Value= "No", Text= "No" } };
        }

        public List<MedicionesTierra> ObtenerMedicion(string sub, DateTime fecha)
        {
            var parametrosub = new SqlParameter("@t", sub);
            var parametroFecha = new SqlParameter("@f", fecha);
            return db.Database.SqlQuery<MedicionesTierra>(@"Select Sub_PruebaAceiteAQReducido.*, 
                      Numemp, NoSerie, Subestaciones.Codigo + ', '+ Subestaciones.NombreSubestacion subestacion,
                      TransformadoresSubtransmision.Nombre, C.Capacidad capacidad, PesoAceite, TransformadoresSubtransmision.AnnoFabricacion
                      FROM Sub_PruebaAceiteAQReducido inner join Subestaciones on Sub_PruebaAceiteAQReducido.CodigoSub = Subestaciones.Codigo
                      inner join TransformadoresSubtransmision on Sub_PruebaAceiteAQReducido.Id_Transf = TransformadoresSubtransmision.Id_Transformador
                      left join Fabricantes on Fabricantes.Id_Fabricante= TransformadoresSubtransmision.idFabricante
                      left join Capacidades as C on C.Id_Capacidad = TransformadoresSubtransmision.Id_Capacidad
                      WHERE (Sub_PruebaAceiteAQReducido.Id_Transf = @t) and (Sub_PruebaAceiteAQReducido.Id_EAdministrativa = @ea) and (Sub_PruebaAceiteAQReducido.Fecha = @f)
                      union
                      SELECT Sub_PruebaAceiteAQReducido.*,
                      Numemp, NoSerie, SubestacionesTransmision.Codigo + ', '+ SubestacionesTransmision.NombreSubestacion subestacion, 
                      TransformadoresTransmision.Nombre, C.Capacidad capacidad, PesoAceite PesoAceite, TransformadoresTransmision.AnnoFabricacion
                      FROM Sub_PruebaAceiteAQReducido inner join SubestacionesTransmision on Sub_PruebaAceiteAQReducido.CodigoSub = SubestacionesTransmision.Codigo
                      inner join TransformadoresTransmision on Sub_PruebaAceiteAQReducido.Id_Transf = TransformadoresTransmision.Id_Transformador
                      left join Fabricantes on Fabricantes.Id_Fabricante= TransformadoresTransmision.idFabricante
                      left join Capacidades as C on C.Id_Capacidad = TransformadoresTransmision.Id_Capacidad
                    WHERE (Sub_PruebaAceiteAQReducido.Id_Transf = @t) and (Sub_PruebaAceiteAQReducido.Id_EAdministrativa = @ea) and (Sub_PruebaAceiteAQReducido.Fecha = @f)", parametrosub, parametroFecha).ToList();

        }

        public List<CaidaPotencial> ObtenerCaidaPotencial(string codigo, DateTime? f)
        {
            var parametroF = new SqlParameter("@f", f);
            var parametroCod = new SqlParameter("@cod", codigo);

            return db.Database.SqlQuery<CaidaPotencial>(@"Select Sub_MedicionTierra_CaidaPotencial.Fecha Fecha,isnull(Resistencia, 0)Resistencia, 
            isnull(DistanciaElectrodoPotencial, 0)DistanciaElectrodoPotencial
                      FROM Sub_MedicionTierra_CaidaPotencial 
                      WHERE (Sub_MedicionTierra_CaidaPotencial.Fecha = @f) and 
                            (Sub_MedicionTierra_CaidaPotencial.Subestacion = @cod)", parametroF, parametroCod).ToList();

        }
    }
}