using Subestaciones.Models.Clases;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Subestaciones.Models.Repositorio
{
    public class MoverTransfRepo
    {
        private DBContext db;
        public MoverTransfRepo(DBContext db)
        {
            this.db = db;
        }

        public async Task<List<MoverTransf>> ObtenerMoverTransf()
        {
            return await (from TS in db.TransformadoresSubtransmision
                          join sub in db.Subestaciones on TS.Codigo equals sub.Codigo into subS
                          from subestacion in subS.DefaultIfEmpty()
                          join EA in db.EstructurasAdministrativas on TS.Codigo equals EA.Dir_Calle into almacen
                          from almacenes in almacen.DefaultIfEmpty()
                          join B in db.Bloque on subestacion.Codigo equals B.Codigo
                          join E in db.EsquemasBaja on B.EsquemaPorBaja equals E.Id_EsquemaPorBaja into EB
                          from esquemas in EB.DefaultIfEmpty()

                          select new MoverTransf
                          {
                              idTransformador = TS.Id_Transformador,
                              NombreTransformador = TS.Nombre,
                              codAlmacen = almacenes.Dir_Calle,
                              almacen = almacenes.Nombre,
                              codSub = subestacion.Codigo,
                              nombresubestacion = subestacion.NombreSubestacion,
                              tipoBloque = B.tipobloque,
                              tipoSalida = B.TipoSalida,
                              esquemaBaja = esquemas.EsquemaPorBaja


                          }).Union(from TT in db.TransformadoresTransmision
                                   join subT in db.SubestacionesTransmision on TT.Codigo equals subT.Codigo into subTrans
                                   from subestacionT in subTrans.DefaultIfEmpty()
                                   join EA in db.EstructurasAdministrativas on TT.Codigo equals EA.Dir_Calle into almacen
                                   from almacenes in almacen.DefaultIfEmpty()
                                   join B in db.Bloque on subestacionT.Codigo equals B.Codigo
                                   join E in db.EsquemasBaja on B.EsquemaPorBaja equals E.Id_EsquemaPorBaja into EB
                                   from esquemas in EB.DefaultIfEmpty()

                                   select new MoverTransf
                                   {
                                       idTransformador = TT.Id_Transformador,
                                       NombreTransformador = TT.Nombre,
                                       codAlmacen = almacenes.Dir_Calle,
                                       almacen = almacenes.Nombre,
                                       codSub = subestacionT.Codigo,
                                       nombresubestacion = subestacionT.NombreSubestacion,
                                       tipoBloque = B.tipobloque,
                                       tipoSalida = B.TipoSalida,
                                       esquemaBaja = esquemas.EsquemaPorBaja
                                   }).ToListAsync();
        }


        public List<MoverTransf> ObtenerTransDistribucionEnAlmacen()
        {
            var parametroSub = new SqlParameter("@tipo", 6);
            return db.Database.SqlQuery<MoverTransf>(@"select TransformadoresSubtransmision.Codigo codSub,	
                     TransformadoresSubtransmision.Nombre NombreTransformador,
                     TransformadoresSubtransmision.NoSerie NoSerie,	
                     TransformadoresSubtransmision.Numemp Numemp,
                     Capacidades.Capacidad Capacidad,
                     TransformadoresSubtransmision.PorcientoImpedancia PorcientoImpedancia,
                     TransformadoresSubtransmision.PosicionTrabajo PosicionTrabajo,
                     V.Voltaje Voltaje,
                     TransformadoresSubtransmision.NumeroInventario NumeroInventario,
                     EA.Nombre as almacen, 
                     EA.Dir_Calle as codAlmacen,
                     TransformadoresSubtransmision.Id_Transformador idTransformador,
                     TransformadoresSubtransmision.id_EAdministrativa idEA,
                     cast(TransformadoresSubtransmision.Id_Transformador as varchar)+'_'+cast(TransformadoresSubtransmision.id_EAdministrativa as varchar)  llave,
                     VS.voltaje  Volt_Secund,
                     VT.voltaje  Volt_Terc,
                     TransformadoresSubtransmision.CorrienteAlta CorrienteAlta,
                     TransformadoresSubtransmision.CorrienteBaja CorrienteBaja,
                     Fabricantes.Nombre+' '+Fabricantes.Pais  Fabricante,
                     TransformadoresSubtransmision.AnnoFabricacion AnnoFabricacion,
                     TransformadoresSubtransmision.GrupoConexion GrupoConexion,
                     TransformadoresSubtransmision.pesototal pesototal,
                    TransformadoresSubtransmision.PesoAceite PesoAceite,
                    TransformadoresSubtransmision.CantVentiladores CantVentiladores,
                    TransformadoresSubtransmision.CantRadiadores CantRadiadores,
                    TransformadoresSubtransmision.Observaciones Observaciones,
                    case TransformadoresSubtransmision.EstadoOperativo
			              WHEN 'S' THEN 'En Servicio'
			              WHEN 'D' THEN 'Desconectado'
			             ELSE ''
			             END Estado_Operativo
                    from TransformadoresSubtransmision 
                    left join Capacidades   on Capacidades.Id_Capacidad =  TransformadoresSubtransmision.Id_Capacidad
	                left join VoltajesSistemas V on V.Id_VoltajeSistema=TransformadoresSubtransmision.Id_VoltajePrim
	                left join VoltajesSistemas VS on VS.Id_VoltajeSistema=TransformadoresSubtransmision.Id_Voltaje_Secun
	                left join VoltajesSistemas VT on VT.Id_VoltajeSistema=TransformadoresSubtransmision.VoltajeTerciario
	                left join Fabricantes ON Fabricantes.Id_Fabricante= TransformadoresSubtransmision.idFabricante
	                inner join EstructurasAdministrativas EA on TransformadoresSubtransmision.Codigo=EA.Dir_Calle
                    where EA.Dir_Calle<>''  and EA.Tipo=@tipo", parametroSub).ToList();

        }

        public List<MoverTransf> ObtenerTransTransmisionEnAlmacen()
        {
            var parametroSub = new SqlParameter("@tipo", 6);
            return db.Database.SqlQuery<MoverTransf>(@"
                        select TS.Codigo codSub,
                        TS.Nombre NombreTransformador,
                        TS.NoSerie NoSerie,
                        TS.Numemp Numemp,
                        C.Capacidad Capacidad,
                        TS.PorcientoImpedancia PorcientoImpedancia,
                        TS.PosicionTrabajo PosicionTrabajo,
                        V.Voltaje Voltaje,
                        TS.NumeroInventario NumeroInventario,
                        EA.Nombre  almacen,
                        EA.Dir_Calle  codAlmacen,
                        TS.Id_Transformador idTransformador,
                        TS.id_EAdministrativa idEA,
                        cast(TS.Id_Transformador as varchar)+'_'+cast(TS.id_EAdministrativa as varchar) llave,
	                    VS.voltaje  Volt_Secund,
	                    VT.voltaje Volt_Terc,
	                    TS.CorrienteAlta CorrienteAlta,
	                    TS.CorrienteBaja CorrienteBaja,
	                    F.Nombre+' '+F.Pais Fabricante,
	                    TS.AnnoFabricacion AnnoFabricacion,
	                    TS.GrupoConexion GrupoConexion,
	                    TS.pesototal pesototal,
	                    TS.PesoAceite PesoAceite,
	                    TS.CantVentiladores CantVentiladores,
	                    TS.CantRadiadores CantRadiadores,
	                    TS.Observaciones Observaciones,
		                case TS.EstadoOperativo
			                      WHEN 'S' THEN 'En Servicio'
			                      WHEN 'D' THEN 'Desconectado'
			                     ELSE ''
			                     END Estado_Operativo
                        from TransformadoresTransmision TS
                        left join Capacidades  C on C.Id_Capacidad =  TS.Id_Capacidad
	                    left join VoltajesSistemas V on V.Id_VoltajeSistema=TS.Id_VoltajePrim
	                    left join VoltajesSistemas VS on VS.Id_VoltajeSistema=TS.Id_Voltaje_Secun
	                    left join VoltajesSistemas VT on VT.Id_VoltajeSistema=TS.VoltajeTerciario
	                    left join Fabricantes F ON F.Id_Fabricante= TS.idFabricante
	                    inner join EstructurasAdministrativas EA on TS.Codigo=EA.Dir_Calle
                        where EA.Dir_Calle<>''  and EA.Tipo=@tipo", parametroSub).ToList();
        }

        public List<MoverTransf> ObtenerTransEnSubestacionDistribucion()
        {
            return db.Database.SqlQuery<MoverTransf>(@"select S.codigo, 
                   S.nombresubestacion nombresubestacion,
                   EA.Nombre as Sucursal,
                   EA1.Nombre as OBE,
                   EA2.Nombre as Empresa,
                   TS.Id_Transformador idTransformador,
                   TS.id_EAdministrativa idEA,
                   cast(TS.Id_Transformador as varchar)+'_'+cast(TS.id_EAdministrativa as varchar) as llave,
                   TS.Nombre NombreTransformador, 
                   TS.NoSerie, 
                   TS.Numemp, 
                   C.Capacidad, 
                   TS.PorcientoImpedancia, 
                   TS.PosicionTrabajo, 
                   V.Voltaje, 
                   TS.NumeroInventario,
                   B.TipoBloque tipoBloque, 
                   B.TipoSalida tipoSalida, 
                   EB.EsquemaPorBaja esquemaBaja,
                   VS.voltaje as Volt_Secund, 
                   VT.voltaje as Volt_Terc, 
                   TS.CorrienteAlta, 
                   TS.CorrienteBaja, 
                   F.Nombre + ' ' + F.Pais as Fabricante, 
                   TS.AnnoFabricacion, 
                   TS.GrupoConexion, 
                   TS.pesototal,
                   TS.PesoAceite, 
                   TS.CantVentiladores, 
                   TS.CantRadiadores, 
                   TS.Observaciones,
                   Estado_Operativo = case TS.EstadoOperativo
                                  WHEN 'S' THEN 'En Servicio'
                                  WHEN 'D' THEN 'Desconectado'
                                 ELSE ''
                                 END
                   from Subestaciones S
                   left join EstructurasAdministrativas EA on S.Sucursal = EA.Id_EAdministrativa
                   left  join EstructurasAdministrativas EA1 on EA.Subordinada = EA1.Id_EAdministrativa
                   left join EstructurasAdministrativas EA2 on EA1.Subordinada = EA2.Id_EAdministrativa
                   inner join bloque B on S.Codigo = B.Codigo
                   right join EsquemasBaja EB on B.EsquemaPorBaja = EB.Id_EsquemaPorBaja
                   inner join TransformadoresSubtransmision TS on B.Codigo = TS.Codigo and B.Id_bloque = TS.Id_Bloque
                   left join Capacidades  C on C.Id_Capacidad = TS.Id_Capacidad
                   left join VoltajesSistemas V on V.Id_VoltajeSistema = TS.Id_VoltajePrim
                   left join VoltajesSistemas VS on VS.Id_VoltajeSistema = TS.Id_Voltaje_Secun
                   left join VoltajesSistemas VT on VT.Id_VoltajeSistema = TS.VoltajeTerciario
                   left join Fabricantes F ON F.Id_Fabricante = TS.idFabricante").ToList();
        }

        public List<MoverTransf> ObtenerTransEnSubestacionTransmision()
        {
            return db.Database.SqlQuery<MoverTransf>(@"
                  select S.codigo, 
                  S.nombresubestacion nombresubestacion, 
                  EA.Nombre as Sucursal, 
                  EA1.Nombre as OBE, 
                  EA2.Nombre as Empresa, 
                  TS.Id_Transformador idTransformador, 
                  TS.id_EAdministrativa idEA, 
                  cast(TS.Id_Transformador as varchar) + '_' + cast(TS.id_EAdministrativa as varchar) as llave,
                  TS.Nombre NombreTransformador, 
                  TS.NoSerie, 
                  TS.Numemp, 
                  C.Capacidad, 
                  TS.PorcientoImpedancia, 
                  TS.PosicionTrabajo, 
                  V.Voltaje, 
                  TS.NumeroInventario,
                  B.TipoBloque tipoBloque, 
                  B.TipoSalida tipoSalida, 
                  EB.EsquemaPorBaja esquemaBaja,
                  VS.voltaje as Volt_Secund, 
                  VT.voltaje as Volt_Terc, 
                  TS.CorrienteAlta, 
                  TS.CorrienteBaja, 
                  F.Nombre + ' ' + F.Pais as Fabricante, 
                  TS.AnnoFabricacion, 
                  TS.GrupoConexion, 
                  TS.pesototal,
                  TS.PesoAceite, 
                  TS.CantVentiladores, 
                  TS.CantRadiadores, 
                  TS.Observaciones,
                  Estado_Operativo = case TS.EstadoOperativo

                      WHEN 'S' THEN 'En Servicio'

                      WHEN 'D' THEN 'Desconectado'

                     ELSE ''

                     END

                from SubestacionesTransmision S
                left join EstructurasAdministrativas EA on S.Sucursal = EA.Id_EAdministrativa
                left join EstructurasAdministrativas EA1 on EA.Subordinada = EA1.Id_EAdministrativa
                left join EstructurasAdministrativas EA2 on EA1.Subordinada = EA2.Id_EAdministrativa
                inner join bloque B on S.Codigo = B.Codigo
                right join EsquemasBaja EB on B.EsquemaPorBaja = EB.Id_EsquemaPorBaja
                inner join TransformadoresTransmision TS on B.Codigo = TS.Codigo and B.Id_bloque = TS.Id_Bloque
                left join Capacidades  C on C.Id_Capacidad = TS.Id_Capacidad
                left join VoltajesSistemas V on V.Id_VoltajeSistema = TS.Id_VoltajePrim
                left join VoltajesSistemas VS on VS.Id_VoltajeSistema = TS.Id_Voltaje_Secun
                left join VoltajesSistemas VT on VT.Id_VoltajeSistema = TS.VoltajeTerciario
                left join Fabricantes F ON F.Id_Fabricante = TS.idFabricante
                order by S.codigo").ToList();
        }

        public List<EstructurasAdministrativas> ListaAlmacenes()
        {
            return db.Database.SqlQuery<EstructurasAdministrativas>(@"select * from EstructurasAdministrativas where tipo =6").ToList();
        }

        public List<unionSub> subestacionesDistribucion()
        {
            return db.Database.SqlQuery<unionSub>(@"select S.codigo Codigo,   
                    S.nombresubestacion NombreSubestacion,
                    EA.Nombre as SucursalNombre,
                    EA1.Nombre as OBE,
                    EA2.Nombre as Empresa,
	                B.id_bloque, 
                    B.TipoBloque,
                    B.TipoSalida,
                    EB.EsquemaPorBaja,
                    VS.Voltaje as Tensión_Sec,
                    VT.Voltaje as Tensión_Terc
                    from Subestaciones S 
                    left join EstructurasAdministrativas EA on S.Sucursal = EA.Id_EAdministrativa
                    left join EstructurasAdministrativas EA1 on EA.Subordinada = EA1.Id_EAdministrativa
                    left join EstructurasAdministrativas EA2 on EA1.Subordinada = EA2.Id_EAdministrativa
                    inner join bloque B on S.Codigo = B.Codigo
                    left join EsquemasBaja EB on B.EsquemaPorBaja = EB.Id_EsquemaPorBaja
                    left  join VoltajesSistemas VS on VS.Id_VoltajeSistema = B.VoltajeSecundario
                    left  join VoltajesSistemas VT on VT.Id_VoltajeSistema = B.VoltajeTerciario").ToList();
        }

        public List<unionSub> subestacionesTransmision()
        {
            return db.Database.SqlQuery<unionSub>(@"select S.codigo Codigo,
                    S.nombresubestacion NombreSubestacion,
                    EA.Nombre as SucursalNombre,
                    EA1.Nombre as OBE,
                    EA2.Nombre as Empresa,
                    B.id_bloque, 
                    B.TipoBloque,
                    B.TipoSalida,
                    EB.EsquemaPorBaja,
                    VS.Voltaje as Tensión_Sec,
                    VT.Voltaje as Tensión_Terc
                    from SubestacionesTransmision S
                    left  join EstructurasAdministrativas EA on S.Sucursal = EA.Id_EAdministrativa
                    left join EstructurasAdministrativas EA1 on EA.Subordinada = EA1.Id_EAdministrativa
                    left join EstructurasAdministrativas EA2 on EA1.Subordinada = EA2.Id_EAdministrativa
                    inner join bloque B on S.Codigo = B.Codigo
                    left join EsquemasBaja EB on B.EsquemaPorBaja = EB.Id_EsquemaPorBaja
                    left  join VoltajesSistemas VS on VS.Id_VoltajeSistema = B.VoltajeSecundario
                    left  join VoltajesSistemas VT on VT.Id_VoltajeSistema = B.VoltajeTerciario
                    order by S.codigo").ToList();
        }
    }
}