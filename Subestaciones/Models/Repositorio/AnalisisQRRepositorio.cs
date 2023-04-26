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
    public class AnalisisQRRepositorio
    {

        private DBContext db;
        public AnalisisQRRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<AnalisisQR> Find(int idT,int EA , DateTime fecha)
        {
            var lista = await ObtenerListadoAnalisisQR();
            return lista.Find(c => c.Id_Transf == idT && c.Id_EAdminTransf == EA && c.Fecha == fecha);
        }

        public async Task<List<AnalisisQR>> ObtenerListadoAnalisisQR()
        {
            return ( (from AQR in db.Sub_PruebaAceiteAQReducido
                          join subD in db.Subestaciones on AQR.CodigoSub equals subD.Codigo
                          join Transf in db.TransformadoresSubtransmision on AQR.Id_Transf equals Transf.Id_Transformador
                           select new AnalisisQR
                          {
                              subestacion = subD.Codigo + ", " + subD.NombreSubestacion,
                              Nombre = Transf.Nombre,
                              NoSerie = Transf.NoSerie,
                              Numemp = Transf.Numemp,
                              Fecha = AQR.Fecha,
                              RealizadoenLaboraorio = AQR.RealizadoenLaboraorio,
                              Id_Transf = AQR.Id_Transf,
                              Id_EAdminTransf = AQR.Id_EAdminTransf,
                              RevisadoPor = AQR.RevisadoPor,
                              EjecutadoPor = AQR.EjecutadoPor,
                              CodigoSub = AQR.CodigoSub
                          }).Union
          (from AQR in db.Sub_PruebaAceiteAQReducido
           join subT in db.SubestacionesTransmision on AQR.CodigoSub equals subT.Codigo
           join TransfT in db.TransformadoresTransmision on AQR.Id_Transf equals TransfT.Id_Transformador
            select new AnalisisQR
           {
               subestacion = subT.Codigo + ", " + subT.NombreSubestacion,
               Nombre = TransfT.Nombre,
                NoSerie = TransfT.NoSerie,
               Numemp = TransfT.Numemp,
               Fecha = AQR.Fecha,
               RealizadoenLaboraorio = AQR.RealizadoenLaboraorio,
               Id_Transf = AQR.Id_Transf,
               Id_EAdminTransf = AQR.Id_EAdminTransf,
               RevisadoPor = AQR.RevisadoPor,
               EjecutadoPor = AQR.EjecutadoPor,
               CodigoSub = AQR.CodigoSub

           })).ToList();
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
        public List<AnalisisQR> ObtenerPrueba(int transf, int ea, DateTime fecha)
        {
            var parametroTransf = new SqlParameter("@t", transf);
            var parametroEA = new SqlParameter("@ea", ea);
            var parametroFecha = new SqlParameter("@f", fecha);
            return db.Database.SqlQuery<AnalisisQR>(@"Select Sub_PruebaAceiteAQReducido.*, 
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
                    WHERE (Sub_PruebaAceiteAQReducido.Id_Transf = @t) and (Sub_PruebaAceiteAQReducido.Id_EAdministrativa = @ea) and (Sub_PruebaAceiteAQReducido.Fecha = @f)", parametroTransf, parametroEA, parametroFecha).ToList();
                                                                
        }

        public List<AnalisisQR> ObtenerPrueba(int transf, int ea, string codigo)
        {
             var parametroTransf = new SqlParameter("@t", transf);
            var parametroEA = new SqlParameter("@ea", ea);
            var parametroCod = new SqlParameter("@cod", codigo);

            return db.Database.SqlQuery<AnalisisQR>(@"Select Sub_PruebaAceiteAQReducido.Fecha Fecha, 
isnull(Densidada20GC, 0)Densidada20GC, 
isnull(NrodeNeutralizacion, 0)NrodeNeutralizacion,
isnull(AguaporKFisher, 0)AguaporKFisher,
isnull(HumedadenPapel, 0)HumedadenPapel,
isnull(TensionInterfacial, 0)TensionInterfacial,
isnull(PuntoInflamacion, 0)PuntoInflamacion,
isnull(RigidezDielectrica, 0)RigidezDielectrica,
isnull(SedimentoyCienoPrecip, 0)SedimentoyCienoPrecip,
isnull(Viscosidada40GC, 0)Viscosidada40GC,
isnull(FactorDisipacionTAmb, 0)FactorDisipacionTAmb,
isnull(FactorDisipaciona70GC, 0)FactorDisipaciona70GC,
isnull(FactorDisipacion90GC, 0)FactorDisipacion90GC
                      FROM Sub_PruebaAceiteAQReducido 
                      WHERE (Sub_PruebaAceiteAQReducido.Id_Transf = @t) and 
                            (Sub_PruebaAceiteAQReducido.Id_EAdminTransf = @ea) and  
                            (Sub_PruebaAceiteAQReducido.CodigoSub = @cod)", parametroTransf, parametroEA, parametroCod).ToList();

        }

    }
}