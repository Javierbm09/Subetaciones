using Subestaciones.Models.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Subestaciones.Models.Repositorio
{
    public class AnalisisGDRepositorio
    {
        private DBContext db;

        public AnalisisGDRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<AnalisisGD> Find(int idT, int EA, DateTime fecha)
        {
            var lista = await ObtenerListadoAnalisisGD();
            return lista.Find(c => c.Id_Transf == idT && c.Id_EAdminTransf == EA && c.Fecha == fecha);
        }

        public async Task<List<AnalisisGD>> ObtenerListadoAnalisisGD()
        {
            return (from AGD in db.Sub_PruebaAceiteAGDisueltos
                    join subD in db.Subestaciones on AGD.CodigoSub equals subD.Codigo
                    join Transf in db.TransformadoresSubtransmision on AGD.Id_Transf equals Transf.Id_Transformador
                    select new AnalisisGD
                    {
                        subestacion = subD.Codigo + ", " + subD.NombreSubestacion,
                        Nombre = Transf.Nombre,
                        NoSerie = Transf.NoSerie,
                        Numemp = Transf.Numemp,
                        Fecha = AGD.Fecha,
                        RealizadoenLaboraorio = AGD.RealizadoenLaboraorio,
                        Id_Transf = AGD.Id_Transf,
                        Id_EAdminTransf = AGD.Id_EAdminTransf,
                        RevisadoPor = AGD.RevisadoPor,
                        EjecutadoPor = AGD.EjecutadoPor,
                        CodigoSub = AGD.CodigoSub
                    }).Union
          (from AGD in db.Sub_PruebaAceiteAGDisueltos
           join subT in db.SubestacionesTransmision on AGD.CodigoSub equals subT.Codigo
           join TransfT in db.TransformadoresTransmision on AGD.Id_Transf equals TransfT.Id_Transformador
           select new AnalisisGD
           {
               subestacion = subT.Codigo + ", " + subT.NombreSubestacion,
               Nombre = TransfT.Nombre,
               NoSerie = TransfT.NoSerie,
               Numemp = TransfT.Numemp,
               Fecha = AGD.Fecha,
               RealizadoenLaboraorio = AGD.RealizadoenLaboraorio,
               Id_Transf = AGD.Id_Transf,
               Id_EAdminTransf = AGD.Id_EAdminTransf,
               RevisadoPor = AGD.RevisadoPor,
               EjecutadoPor = AGD.EjecutadoPor,
               CodigoSub = AGD.CodigoSub

           }).ToList();
        }

        public List<UnionTransformadores> ObtenerListadoTransf(string cod)
        {
            var parametrocodigo = new SqlParameter("@codigo", cod);
            return db.Database.SqlQuery<UnionTransformadores>(@"Select Id_EAdministrativa, Id_Transformador, TransformadoresSubtransmision.NumAccion, Codigo Subestacion, Numemp, NoSerie, 
                      TransformadoresSubtransmision.Nombre, C.Capacidad capacidad, PesoAceite, TransformadoresSubtransmision.AnnoFabricacion
FROM
  TransformadoresSubtransmision
  left join Fabricantes on Fabricantes.Id_Fabricante= TransformadoresSubtransmision.idFabricante
  left join Capacidades as C on C.Id_Capacidad = TransformadoresSubtransmision.Id_Capacidad
WHERE
  (TransformadoresSubtransmision.Codigo = @codigo)
union
SELECT Id_EAdministrativa Id_EAdministrativa, Id_Transformador Id_Transformador, TransformadoresTransmision.NumAccion NumAccion, Codigo Subestacion, Numemp, NoSerie, 
                      TransformadoresTransmision.Nombre, C.Capacidad capacidad, PesoAceite PesoAceite, TransformadoresTransmision.AnnoFabricacion
FROM
  TransformadoresTransmision
  left join Fabricantes on Fabricantes.Id_Fabricante= TransformadoresTransmision.idFabricante
  left join Capacidades as C on C.Id_Capacidad = TransformadoresTransmision.Id_Capacidad
WHERE
  (TransformadoresTransmision.Codigo = @codigo)", parametrocodigo).ToList();

        }

        //public SelectList listaTrans(string codigo)
        //{
        //    var TransSub = db.TransformadoresSubtransmision.ToList();
        //    var TransT = db.TransformadoresTransmision.ToList();
        //    var Listado = (from transS in TransSub
        //                   where transS.Codigo == codigo 
        //                   select new SelectListItem
        //                   {
        //                       Value = string.Format("{0}",transS.Id_Transformador),
        //                       Text = transS.Nombre
        //                   }
        //                   ).Union(from trnsT in TransT
        //                           where trnsT.Codigo == codigo

        //                           select new SelectListItem
        //                            {
        //                                Value = string.Format("{0}", trnsT.Id_Transformador),
        //                                Text = trnsT.Nombre

        //                            }).ToList();
        //    return new SelectList(Listado, "Value", "Text");
        //}

        //seleccionar prueba realizada
        public List<AnalisisGD> ObtenerPrueba(int transf, int ea, DateTime fecha)
        {
            var parametroTransf = new SqlParameter("@t", transf);
            var parametroEA = new SqlParameter("@ea", ea);
            var parametroFecha = new SqlParameter("@f", fecha);
            return db.Database.SqlQuery<AnalisisGD>(@"Select Sub_PruebaAceiteAGDisueltos.*, 
                      Numemp, NoSerie, Subestaciones.Codigo + ', '+ Subestaciones.NombreSubestacion subestacion,
                      TransformadoresSubtransmision.Nombre, C.Capacidad capacidad, PesoAceite, TransformadoresSubtransmision.AnnoFabricacion
                      FROM Sub_PruebaAceiteAGDisueltos inner join Subestaciones on Sub_PruebaAceiteAGDisueltos.CodigoSub = Subestaciones.Codigo
                      inner join TransformadoresSubtransmision on Sub_PruebaAceiteAGDisueltos.Id_Transf = TransformadoresSubtransmision.Id_Transformador
                      left join Fabricantes on Fabricantes.Id_Fabricante= TransformadoresSubtransmision.idFabricante
                      left join Capacidades as C on C.Id_Capacidad = TransformadoresSubtransmision.Id_Capacidad
                      WHERE (Sub_PruebaAceiteAGDisueltos.Id_Transf = @t) and (Sub_PruebaAceiteAGDisueltos.Id_EAdministrativa = @ea) and (Sub_PruebaAceiteAGDisueltos.Fecha = @f)
                      union
                      SELECT Sub_PruebaAceiteAGDisueltos.*,
                      Numemp, NoSerie, SubestacionesTransmision.Codigo + ', '+ SubestacionesTransmision.NombreSubestacion subestacion, 
                      TransformadoresTransmision.Nombre, C.Capacidad capacidad, PesoAceite PesoAceite, TransformadoresTransmision.AnnoFabricacion
                      FROM Sub_PruebaAceiteAGDisueltos inner join SubestacionesTransmision on Sub_PruebaAceiteAGDisueltos.CodigoSub = SubestacionesTransmision.Codigo
                      inner join TransformadoresTransmision on Sub_PruebaAceiteAGDisueltos.Id_Transf = TransformadoresTransmision.Id_Transformador
                      left join Fabricantes on Fabricantes.Id_Fabricante= TransformadoresTransmision.idFabricante
                      left join Capacidades as C on C.Id_Capacidad = TransformadoresTransmision.Id_Capacidad
                    WHERE (Sub_PruebaAceiteAGDisueltos.Id_Transf = @t) and (Sub_PruebaAceiteAGDisueltos.Id_EAdministrativa = @ea) and (Sub_PruebaAceiteAGDisueltos.Fecha = @f)", parametroTransf, parametroEA, parametroFecha).ToList();

        }

        public List<AnalisisGD> ObtenerPrueba(int transf, int ea, string codigo)
        {
            var parametroTransf = new SqlParameter("@t", transf);
            var parametroEA = new SqlParameter("@ea", ea);
            var parametroCod = new SqlParameter("@cod", codigo);

            return db.Database.SqlQuery<AnalisisGD>(@"Select Sub_PruebaAceiteAGDisueltos.Fecha Fecha, 
                        isnull(H2Hidrogeno, 0)H2Hidrogeno, 
                        isnull(CH4Metano, 0)CH4Metano,
                        isnull(C2H6Etano, 0)C2H6Etano,
                        isnull(C2H4Etileno, 0)C2H4Etileno,
                        isnull(C2H2Acetileno, 0)C2H2Acetileno,
                        isnull(COMonoxidoCarbono, 0)COMonoxidoCarbono,
                        isnull(CO2DioxidoCarbono, 0)CO2DioxidoCarbono
                      FROM Sub_PruebaAceiteAGDisueltos 
                      WHERE (Sub_PruebaAceiteAGDisueltos.Id_Transf = @t) and 
                            (Sub_PruebaAceiteAGDisueltos.Id_EAdminTransf = @ea) and  
                            (Sub_PruebaAceiteAGDisueltos.CodigoSub = @cod)", parametroTransf, parametroEA, parametroCod).ToList();

        }

    }

}
