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
    public class SubMttoSubDistRepositorio
    {

        private DBContext db;
        public SubMttoSubDistRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<SubMttoSubDist> FindAsync(string codigo, DateTime fecha)
        {
            var lista = await ObtenerMttos();
            return lista.Find(c => (c.CodigoSub == codigo) && (c.Fecha == fecha));
        }

        public async Task<List<SubMttoSubDist>> ObtenerMttos()
        {
            return await db.Database.SqlQuery<SubMttoSubDist>(@"select Subestaciones.NombreSubestacion NombreSub,
                    Subestaciones.Codigo CodigoSub,
                    TipoMantenimiento.TipoMtto TipoMantenimiento,
                    SubMttoSubDistribucion.Fecha,
                    CASE SubMttoSubDistribucion.Mantenido 
                                WHEN 0 THEN 'No'
                                WHEN 1 THEN 'Si'
                                END Mantenido,
                    Personal.Nombre RealizadoPor
                    from SubMttoSubDistribucion 
                    inner join Subestaciones on SubMttoSubDistribucion.CodigoSub = Subestaciones.Codigo
                    inner join TipoMantenimiento on SubMttoSubDistribucion.TipoMantenimiento = TipoMantenimiento.IdTipoMtto
                    inner join Personal on SubMttoSubDistribucion.RealizadoPor = Personal.Id_Persona").ToListAsync();
        }

        public SelectList tipoMtto()
        {
            var tipo = (from tipos in db.TipoMantenimiento
                        where tipos.EsTipoMttoSubDist == true
                        select new SelectListItem
                        {
                            Value = tipos.IdTipoMtto.ToString(),
                            Text = tipos.TipoMtto

                        }).ToList();
            return new SelectList(tipo, "Value", "Text");

        }

        public async Task<List<SubMttoSubDist>> ObtenerTransfSub(string codigo)
        {
            var parametrocodigo = new SqlParameter("@codigo", codigo);
            return await db.Database.SqlQuery<SubMttoSubDist>(@"Select * 
            From TransformadoresTransmision 
            where TransformadoresTransmision.Codigo = @codigo", parametrocodigo).ToListAsync();
        }

        public List<TransformadorSubtransmision> ObtenerDatoTransf()
        {
            //var parametrocodigo = new SqlParameter("@codigo", codigo);
            //var parametroea = new SqlParameter("@ea", ea);
            //var parametrot = new SqlParameter("@trans", t);
            return db.Database.SqlQuery<TransformadorSubtransmision>(@"SELECT *
FROM 
  TransformadoresSubtransmision").ToList();
            //WHERE
            //  (TransformadoresSubtransmision.Id_EAdministrativa = @ea) and
            //  (TransformadoresSubtransmision.Id_Transformador = @trans) and
            //  (TransformadoresSubtransmision.Codigo = @codigo)", parametroea, parametrot, parametrocodigo).ToListAsync();
        }

        public TransformadorSubtransmision FindTransf(string codigo, int? ea, int? t)
        {
            var lista = ObtenerDatoTransf();
            return lista.Find(c => c.Id_EAdministrativa == ea && c.Id_EAdministrativa == t && c.Subestacion == codigo);
        }



        public List<MttoTransfDist> listaMttosTransf(string sub, string fechaM)
        {
            var parametrocodigo = new SqlParameter("@codigo", sub);
            var parametrofecha = new SqlParameter("@f", fechaM);
            return db.Database.SqlQuery<MttoTransfDist>(@"SELECT TransformadoresSubtransmision.Nombre Nombre, 
                TransformadoresSubtransmision.Id_Transformador,
                TransformadoresSubtransmision.Codigo Subestacion,
                TransformadoresSubtransmision.Id_EAdministrativa,   
                TransformadoresSubtransmision.Numemp Numemp, 
                TransformadoresSubtransmision.NoSerie NoSerie,
                Fabricantes.Nombre +','+ Fabricantes.Pais Fabricante,
                Sub_MttoDistTransf.Fecha fecha
                from Sub_MttoDistTransf inner join TransformadoresSubtransmision on  Sub_MttoDistTransf.Id_Transformador =  TransformadoresSubtransmision.Id_Transformador 
                left join Fabricantes on TransformadoresSubtransmision.idFabricante = Fabricantes.Id_Fabricante
                where Sub_MttoDistTransf.CodigoSub = @codigo and Sub_MttoDistTransf.Fecha=@f", parametrocodigo, parametrofecha).ToList();


        }

        public List<MttosDesconectivosSubDist> listaMttosDesc(string codigo, string fecha)
        {
            var parametrocodigo = new SqlParameter("@codigo", codigo);
            var parametrofecha = new SqlParameter("@f", fecha);

            return db.Database.SqlQuery<MttosDesconectivosSubDist>(@"SELECT  InstalacionDesconectivos.Codigo AS Codigo ,
                InstalacionDesconectivos.id_EAdministrativa_Prov ,  
                InstalacionDesconectivos.Id_EAdministrativa,
                InstalacionDesconectivos.CodigoNuevo ,
                InstalacionDesconectivos.NumeroFases ,
                InstalacionDesconectivos.CorrienteNominal ,
                InstalacionDesconectivos.UbicadaEn ,
                InstalacionDesconectivos.CircuitoA ,
                InstalacionDesconectivos.SeccionA ,
                InstalacionDesconectivos.CircuitoB ,
                InstalacionDesconectivos.SeccionB ,
                InstalacionDesconectivos.BarrioPueblo ,
                InstalacionDesconectivos.TipoSeccionalizador tipoDesc,
                InstalacionDesconectivos.NumAccion,
                EA.Nombre ,
                TipoInstalacion.NombreInstalacion TipoInstalacion ,
                Tipodesconectivos.TipoDesconectivo TipoSeccionalizador,
                Funciondesconectivos.FuncionDesconectivos Funcion,
                CASE InstalacionDesconectivos.estadooperativo
                WHEN 'A' THEN 'Abierto'
                WHEN NULL THEN ( SELECT CASE InstalacionDesconectivos.funcion
                WHEN 1 THEN 'Abierto'
                ELSE 'Operativo'
                END)
                ELSE 'Operativo'
                END AS EstadoOperativo ,
                AD.TipoAutomático AS Automatica,
                TipoReconectador.AnnoFab as anhoFab ,
				Fabricantes.Nombre AS fabricante ,
                Inst_TipoPortaFusible.DescripcionTipoPortaFusible ,
				Inst_TipoFusible.DescripcionTipoFusible ,
				Inst_CapacidadFusible.DescripcionCapacidadFusible ,
				Inst_TensionFusible.DescripcionTensionFusible, 
                Sub_MttoDistribDesconectivos.Fecha fecha
                FROM Sub_MttoDistribDesconectivos inner join InstalacionDesconectivos on InstalacionDesconectivos.Codigo = Sub_MttoDistribDesconectivos.CodigoDesc
				LEFT JOIN TipoReconectador ON TipoReconectador.Id_Tipo = InstalacionDesconectivos.Tipo
				LEFT JOIN Fabricantes ON TipoReconectador.Fabricante = Fabricantes.Id_Fabricante
                left JOIN EstructurasAdministrativas EA ON ( EA.Id_EAdministrativa = InstalacionDesconectivos.Sucursal )
                LEFT OUTER JOIN TipoInstalacion ON ( InstalacionDesconectivos.TipoInstalacion = Tipoinstalacion.LetraInstalacion )
                INNER JOIN TipoDesconectivos ON ( InstalacionDesconectivos.TipoSeccionalizador = Tipodesconectivos.Id_TipoDesconectivo )
                INNER JOIN FuncionDesconectivos ON ( InstalacionDesconectivos.Funcion = Funciondesconectivos.Id_FuncionDesconectivos )
                LEFT OUTER JOIN Automático_Desconectivo AD ON ( InstalacionDesconectivos.Automatico = AD.Codigo )
                left join PortaFusibles on PortaFusibles.CodigoPortafusible=InstalacionDesconectivos.Codigo 
                LEFT JOIN dbo.Inst_TipoPortaFusible ON Inst_TipoPortaFusible.Id_TipoPortaFusible = PortaFusibles.Id_Fusible
				LEFT JOIN Inst_TipoFusible ON Inst_TipoFusible.Id_TipoFusible = Inst_TipoPortaFusible.Id_TipoPortaFusible
				LEFT JOIN Inst_CapacidadFusible ON Inst_CapacidadFusible.Id_CapacidadFusible = PortaFusibles.CapacidadFusible
				LEFT JOIN dbo.Inst_TensionFusible ON Inst_TensionFusible.Id_TensionFusible = PortaFusibles.Id_VoltajeN
                where Sub_MttoDistribDesconectivos.CodigoSub = @codigo and Sub_MttoDistribDesconectivos.Fecha=@f", parametrocodigo, parametrofecha).ToList();
        }

        public List<Desconectivos> FindDescEnSub(string codigo)
        {
            var lista = listaDescSub();
            var desc = lista.Where(c => c.UbicadaEn == codigo);
            return desc.ToList();
        }



        public List<Desconectivos> listaDescSub()
        {
            //var parametrocodigo = new SqlParameter("@codigo", codigo);

            return db.Database.SqlQuery<Desconectivos>(@"SELECT InstalacionDesconectivos.Codigo AS Codigo ,
                InstalacionDesconectivos.id_EAdministrativa_Prov ,  
                InstalacionDesconectivos.Id_EAdministrativa,
                InstalacionDesconectivos.CodigoNuevo ,
                InstalacionDesconectivos.NumeroFases ,
                InstalacionDesconectivos.CorrienteNominal ,
                InstalacionDesconectivos.UbicadaEn ,
                InstalacionDesconectivos.CircuitoA ,
                InstalacionDesconectivos.SeccionA ,
                InstalacionDesconectivos.CircuitoB ,
                InstalacionDesconectivos.SeccionB ,
                InstalacionDesconectivos.BarrioPueblo ,
                InstalacionDesconectivos.TipoSeccionalizador tipoDesc,
                InstalacionDesconectivos.NumAccion,
                EA.Nombre ,
                TipoInstalacion.NombreInstalacion TipoInstalacion ,
                Tipodesconectivos.TipoDesconectivo TipoSeccionalizador,
                Funciondesconectivos.FuncionDesconectivos Funcion,
                CASE InstalacionDesconectivos.estadooperativo
                WHEN 'A' THEN 'Abierto'
                WHEN NULL THEN ( SELECT CASE InstalacionDesconectivos.funcion
                WHEN 1 THEN 'Abierto'
                ELSE 'Operativo'
                END)
                ELSE 'Operativo'
                END AS EstadoOperativo ,
                AD.TipoAutomático AS Automatica ,
                TipoReconectador.AnnoFab as anhoFab ,
				Fabricantes.Nombre AS fabricante, 
                Inst_TipoPortaFusible.DescripcionTipoPortaFusible,
                Inst_TipoFusible.DescripcionTipoFusible ,
				Inst_TensionFusible.DescripcionTensionFusible,
                Inst_CapacidadFusible.DescripcionCapacidadFusible 
                FROM InstalacionDesconectivos 
				LEFT JOIN TipoReconectador ON TipoReconectador.Id_Tipo = InstalacionDesconectivos.Tipo
				LEFT JOIN Fabricantes ON TipoReconectador.Fabricante = Fabricantes.Id_Fabricante
                left JOIN EstructurasAdministrativas EA ON ( EA.Id_EAdministrativa = InstalacionDesconectivos.Sucursal )
                LEFT OUTER JOIN TipoInstalacion ON ( InstalacionDesconectivos.TipoInstalacion = Tipoinstalacion.LetraInstalacion )
                INNER JOIN TipoDesconectivos ON ( InstalacionDesconectivos.TipoSeccionalizador = Tipodesconectivos.Id_TipoDesconectivo )
                INNER JOIN FuncionDesconectivos ON ( InstalacionDesconectivos.Funcion = Funciondesconectivos.Id_FuncionDesconectivos )
                LEFT OUTER JOIN Automático_Desconectivo AD ON ( InstalacionDesconectivos.Automatico = AD.Codigo )
                left join PortaFusibles on PortaFusibles.CodigoPortafusible=InstalacionDesconectivos.Codigo 
                LEFT JOIN dbo.Inst_TipoPortaFusible ON Inst_TipoPortaFusible.Id_TipoPortaFusible = PortaFusibles.Id_Fusible
				LEFT JOIN Inst_TipoFusible ON Inst_TipoFusible.Id_TipoFusible = Inst_TipoPortaFusible.Id_TipoPortaFusible
				LEFT JOIN Inst_CapacidadFusible ON Inst_CapacidadFusible.Id_CapacidadFusible = PortaFusibles.CapacidadFusible
				LEFT JOIN dbo.Inst_TensionFusible ON Inst_TensionFusible.Id_TensionFusible = PortaFusibles.Id_VoltajeN").ToList();
        }
        public List<Barras> FindBarraEnSub(string codigo)
        {
            var lista = listaBarrasSub();
            var bar = lista.Where(c => c.sub == codigo);
            return bar.ToList();
        }
        public List<Barras> listaBarrasSub()
        {

            return db.Database.SqlQuery<Barras>(@"SELECT Subestaciones.NombreSubestacion NombreSub,
            Sub_Barra.Subestacion sub,
            Sub_Barra.corriente corr,
            Sub_Barra.CantidadCond cantC,
            Sub_Barra.codigo barra,
            v.Voltaje tension,     
            Conductor.Codigo+'; '+Tipoconductor.Tipo+'; '+ Material.Tipo+'; Calibre:'+Seccion.Calibre+'; Galga:'+Seccion.Galga+'; Recubrimiento:'+Recubrimiento.Tipo cond
            FROM Sub_Barra 
            inner join Subestaciones on Sub_Barra.Subestacion = Subestaciones.Codigo
            LEFT JOIN dbo.VoltajesSistemas v ON dbo.Sub_Barra.ID_Voltaje=v.Id_VoltajeSistema
            LEFT join Conductor on (Sub_Barra.Conductor=dbo.conductor.Codigo)
            LEFT JOIN TipoConductor ON (Conductor.Id_TipoConductor = Tipoconductor.Id_Tipo)  
            LEFT JOIN Material ON (Conductor.Id_material = Material.Id_Material)  
            LEFT JOIN Seccion ON (Conductor.Id_Seccion = Seccion.Id_Seccion)  
            LEFT JOIN Recubrimiento ON (Conductor.Id_recubre = Recubrimiento.Id_recubre)  
            LEFT JOIN VoltajeAislamiento ON (Conductor.Id_VoltajeAislamiento = Voltajeaislamiento.Id_VoltajeAislamiento)").ToList();
        }

        public List<MttosBarrasSubDist> listaMttosBarras(string codigo, string fecha)
        {
            var parametrocodigo = new SqlParameter("@codigo", codigo);
            var parametrofecha = new SqlParameter("@f", fecha);

            return db.Database.SqlQuery<MttosBarrasSubDist>(@"SELECT Sub_Barra.Subestacion CodigoSub,
            Sub_Barra.codigo CodigoBarra,
            v.Voltaje voltaje,
            Sub_MttoDistBarra.Fecha,
            Conductor.Codigo+'; '+Tipoconductor.Tipo+'; '+ Material.Tipo+'; Calibre:'+Seccion.Calibre+'; Galga:'+Seccion.Galga+'; Recubrimiento:'+Recubrimiento.Tipo Conductor
            FROM Sub_MttoDistBarra inner join
            Sub_Barra on Sub_MttoDistBarra.CodigoBarra = Sub_Barra.codigo and Sub_MttoDistBarra.CodigoSub = Sub_Barra.Subestacion
            LEFT JOIN dbo.VoltajesSistemas v ON dbo.Sub_Barra.ID_Voltaje=v.Id_VoltajeSistema
            left join Conductor on (Sub_Barra.Conductor=dbo.conductor.Codigo)
            LEFT JOIN TipoConductor ON (Conductor.Id_TipoConductor = Tipoconductor.Id_Tipo)  
            LEFT JOIN Material ON (Conductor.Id_material = Material.Id_Material)  
            LEFT JOIN Seccion ON  (Conductor.Id_Seccion = Seccion.Id_Seccion)  
            LEFT JOIN Recubrimiento ON (Conductor.Id_recubre = Recubrimiento.Id_recubre)  
            left JOIN VoltajeAislamiento ON (Conductor.Id_VoltajeAislamiento = Voltajeaislamiento.Id_VoltajeAislamiento) 
            where Sub_MttoDistBarra.CodigoSub = @codigo and Sub_MttoDistBarra.Fecha=@f", parametrocodigo, parametrofecha).ToList();

        }


        public List<MttosBarrasSubDist> MttoBarra(string codigo, DateTime fecha, string barra)
        {
            var parametrocodigo = new SqlParameter("@codigo", codigo);
            var parametrofecha = new SqlParameter("@f", fecha);
            var parametrobarra = new SqlParameter("@b", barra);

            return db.Database.SqlQuery<MttosBarrasSubDist>(@"SELECT Sub_Barra.Subestacion CodigoSub,
            Sub_Barra.codigo CodigoBarra,
            v.Voltaje voltaje,
            Sub_MttoDistBarra.Fecha,
            Conductor.Codigo+'; '+Tipoconductor.Tipo+'; '+ Material.Tipo+'; Calibre:'+Seccion.Calibre+'; Galga:'+Seccion.Galga+'; Recubrimiento:'+Recubrimiento.Tipo Conductor,
            Sub_MttoDistBarra.EstadoBarra,
            Sub_MttoDistBarra.EstadoPuentes,
            Sub_MttoDistBarra.Conexiones
            FROM Sub_MttoDistBarra inner join
            Sub_Barra on Sub_MttoDistBarra.CodigoBarra = Sub_Barra.codigo and Sub_MttoDistBarra.CodigoSub = Sub_Barra.Subestacion
            LEFT JOIN dbo.VoltajesSistemas v ON dbo.Sub_Barra.ID_Voltaje=v.Id_VoltajeSistema
            left join Conductor on (Sub_Barra.Conductor=dbo.conductor.Codigo)
            LEFT JOIN TipoConductor ON (Conductor.Id_TipoConductor = Tipoconductor.Id_Tipo)  
            LEFT JOIN Material ON (Conductor.Id_material = Material.Id_Material)  
            LEFT JOIN Seccion ON  (Conductor.Id_Seccion = Seccion.Id_Seccion)  
            LEFT JOIN Recubrimiento ON (Conductor.Id_recubre = Recubrimiento.Id_recubre)  
            left JOIN VoltajeAislamiento ON (Conductor.Id_VoltajeAislamiento = Voltajeaislamiento.Id_VoltajeAislamiento) 
            where Sub_MttoDistBarra.CodigoSub = @codigo and Sub_MttoDistBarra.Fecha=@f and Sub_MttoDistBarra.CodigoBarra=@b", parametrocodigo, parametrofecha, parametrobarra).ToList();

        }


        //pararrayos
        public List<MttosPararrayosSubDist> listaMttosPararrayos(string codigo, string fechaM)
        {
            var parametrocodigo = new SqlParameter("@codigo", codigo);
            var parametrofecha = new SqlParameter("@f", fechaM);

            return db.Database.SqlQuery<MttosPararrayosSubDist>(@"select Sub_Pararrayos.Id_EAdministrativa Id_AdminPararrayo,
 Sub_Pararrayos.Id_pararrayo,
  Sub_Pararrayos.Codigo CodigoSub,
 Sub_Pararrayos.CE Codigo,
 Sub_Pararrayos.NumeroSerie serie,
 Sub_Pararrayos.Fase,
Sub_Pararrayos.Fabricante fab,
 Tipo =
      CASE TequipoProt
         WHEN 'L' THEN 'Línea'
         WHEN 'TUP' THEN 'Transformador Uso Planta'
         WHEN 'D' THEN 'Desconectivo'
         WHEN 'T' THEN 'Transformador de Potencia'
         WHEN 'TP' THEN 'Transformador de Potencial'
         WHEN 'TC' THEN 'Transformador de Corriente'                  
         ELSE ' '
      END ,
      VoltajesSistemas.Voltaje tension,
      CorrientesNominalesPararrayos.CorrNomPararrayo corr, 
      Sub_MttoDistPararrayos.Aislamiento,
      Sub_MttoDistPararrayos.EstAterramiento,
      Sub_MttoDistPararrayos.Estado,
      Sub_MttoDistPararrayos.Fecha      
      from Sub_MttoDistPararrayos inner join
      Sub_Pararrayos  on Sub_MttoDistPararrayos.Id_AdminPararrayo= Sub_Pararrayos.Id_EAdministrativa
      and Sub_MttoDistPararrayos.Id_Pararrayo=Sub_Pararrayos.Id_pararrayo
      left join VoltajesSistemas on Sub_Pararrayos.Id_Voltaje=VoltajesSistemas.Id_VoltajeSistema
      left join CorrientesNominalesPararrayos on Sub_Pararrayos.Id_CorrienteN=CorrientesNominalesPararrayos.Id_CorrNomPararrayo
      where Sub_MttoDistPararrayos.CodigoSub = @codigo and Sub_MttoDistPararrayos.Fecha=@f", parametrocodigo, parametrofecha).ToList();

        }

        public List<MttosPararrayosSubDist> MttoPararrayo(string codigo, DateTime fecha, int EA, int idP)
        {
            var parametrocodigo = new SqlParameter("@codigo", codigo);
            var parametrofecha = new SqlParameter("@f", fecha);
            var parametroEA = new SqlParameter("@EA", EA);
            var parametroidP = new SqlParameter("@para", idP);

            return db.Database.SqlQuery<MttosPararrayosSubDist>(@"select Sub_Pararrayos.Id_EAdministrativa Id_AdminPararrayo,
 Sub_Pararrayos.Id_pararrayo,
  Sub_Pararrayos.Codigo CodigoSub,
 Sub_Pararrayos.CE Codigo,
 Sub_Pararrayos.NumeroSerie serie,
 Sub_Pararrayos.Fase,
Sub_Pararrayos.Fabricante fab,
 Tipo =
      CASE TequipoProt
         WHEN 'L' THEN 'Línea'
         WHEN 'TUP' THEN 'Transformador Uso Planta'
         WHEN 'D' THEN 'Desconectivo'
         WHEN 'T' THEN 'Transformador de Potencia'
         WHEN 'TP' THEN 'Transformador de Potencial'
         WHEN 'TC' THEN 'Transformador de Corriente'                  
         ELSE ' '
      END ,
      VoltajesSistemas.Voltaje tension,
      CorrientesNominalesPararrayos.CorrNomPararrayo corr, 
      Sub_MttoDistPararrayos.Aislamiento,
      Sub_MttoDistPararrayos.EstAterramiento,
      Sub_MttoDistPararrayos.Estado,
      Sub_MttoDistPararrayos.Fecha      
      from Sub_MttoDistPararrayos inner join
      Sub_Pararrayos  on Sub_MttoDistPararrayos.Id_AdminPararrayo= Sub_Pararrayos.Id_EAdministrativa
      and Sub_MttoDistPararrayos.Id_Pararrayo=Sub_Pararrayos.Id_pararrayo
      left join VoltajesSistemas on Sub_Pararrayos.Id_Voltaje=VoltajesSistemas.Id_VoltajeSistema
      left join CorrientesNominalesPararrayos on Sub_Pararrayos.Id_CorrienteN=CorrientesNominalesPararrayos.Id_CorrNomPararrayo
      where Sub_MttoDistPararrayos.CodigoSub = @codigo and Sub_MttoDistPararrayos.Fecha=@f and Sub_MttoDistPararrayos.Id_AdminPararrayo=@EA and Sub_MttoDistPararrayos.Id_Pararrayo =@para", parametrocodigo, parametrofecha, parametroEA, parametroidP).ToList();

        }

        public List<Pararrayos> listaPararrayosSub(string codigo)
        {
            var parametrocodigo = new SqlParameter("@codigo", codigo);

            return db.Database.SqlQuery<Pararrayos>(@"select Sub_Pararrayos.Id_EAdministrativa ,
 Sub_Pararrayos.Id_pararrayo,
  Sub_Pararrayos.Codigo CodigoSub,
 Sub_Pararrayos.CE Codigo,
 Sub_Pararrayos.NumeroSerie NumeroSerie,
 Sub_Pararrayos.Fase,
Sub_Pararrayos.Fabricante,
 EquipoProteg =
      CASE TequipoProt
         WHEN 'L' THEN 'Línea'
         WHEN 'TUP' THEN 'Transformador Uso Planta'
         WHEN 'D' THEN 'Desconectivo'
         WHEN 'T' THEN 'Transformador de Potencia'
         WHEN 'TP' THEN 'Transformador de Potencial'
         WHEN 'TC' THEN 'Transformador de Corriente'                  
         ELSE ' '
      END ,
      VoltajesSistemas.Voltaje tension,
      CorrientesNominalesPararrayos.CorrNomPararrayo CorrienteN
 from Sub_Pararrayos left join VoltajesSistemas on Sub_Pararrayos.Id_Voltaje=VoltajesSistemas.Id_VoltajeSistema
 inner join Subestaciones on Sub_Pararrayos.Codigo = Subestaciones.Codigo
 left join CorrientesNominalesPararrayos on Sub_Pararrayos.Id_CorrienteN=CorrientesNominalesPararrayos.Id_CorrNomPararrayo
 where Sub_Pararrayos.Codigo=@codigo", parametrocodigo).ToList();
        }

        public List<SelectListItem> estados()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value= "Buen Estado", Text= "Buen Estado" },
                new SelectListItem { Value="Mal Estado", Text="Mal Estado"}
            };
        }

        public List<SelectListItem> estadoTanque()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value= "Buen Estado", Text= "Buen Estado" },
                new SelectListItem { Value="Mal Estado", Text="Mal Estado"},
                new SelectListItem { Value="No Tiene", Text="No Tiene"}};

        }
        public List<SelectListItem> nivel()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value= "Alto", Text= "Alto" },
                new SelectListItem { Value="Normal", Text="Normal"},
                new SelectListItem { Value="Bajo", Text="Bajo"}};

        }

        public List<SelectListItem> aterramientos()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value= "Correcto", Text= "Correcto" },
                new SelectListItem { Value="Incorrecto", Text="Incorrecto"},
                new SelectListItem { Value="No Tiene", Text="No Tiene"}};

        }

        public List<SelectListItem> salideros()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value= "Tiene", Text= "Tiene" },
                new SelectListItem { Value="No Tiene", Text="No Tiene"}};

        }


        public List<SelectListItem> tiposPruebas()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value= "IEC-156-1995", Text= "IEC-156-1995" },
                new SelectListItem { Value= "CEI 10-1-1987", Text= "CEI 10-1-1987" },
                new SelectListItem { Value= "VDE 0370/84", Text= "VDE 0370/84" },
                new SelectListItem { Value= "UNE 21-309-89", Text= "UNE 21-309-89" },
                new SelectListItem { Value= "GOST 6581-87", Text= "GOST 6581-87" },
                new SelectListItem { Value= "NFC 27-221:1974", Text= "NFC 27-221:1974" },
                new SelectListItem { Value= "BS 5874-1980", Text= "BS 5874-1980" },
                new SelectListItem { Value= "ASTM D877-87", Text= "ASTM D877-87" },
                new SelectListItem { Value= "ASTM D1816-84", Text= "ASTM D1816-84" },
                new SelectListItem { Value= "ASTM D1816-84", Text= "ASTM D1816-84" }};

        }

        public List<SelectListItem> tipoCerca()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value= "Malla metálica", Text= "Malla metálica" },
                new SelectListItem { Value="Rejas", Text="Rejas"},
                new SelectListItem { Value="Muros", Text="Muros"},
                new SelectListItem { Value="Mixta", Text="Mixta"},
                new SelectListItem { Value="No Tiene", Text="No Tiene"}};

        }

        public List<SelectListItem> tipoPuerta()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value= "Malla metálica", Text= "Malla metálica" },
                new SelectListItem { Value="Rejas", Text="Rejas"},
                new SelectListItem { Value="Mixta", Text="Mixta"},
                new SelectListItem { Value="No Tiene", Text="No Tiene"}};

        }

        public List<SelectListItem> coronacionYpintura()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Buen Estado", Text= "Buen Estado" },
                new SelectListItem { Value="Mal Estado", Text="Mal Estado"},
                new SelectListItem { Value="No Tiene", Text="No Tiene"},
                new SelectListItem { Value="No Procede", Text="No Procede"}};

        }

        public List<SelectListItem> tipoAlumbrado()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Sodio", Text= "Sodio" },
                new SelectListItem { Value="Mercurio", Text="Mercurio"},
                new SelectListItem { Value="Led", Text="Led"},
                new SelectListItem { Value="No tiene", Text="No tiene"}};

        }

        public List<SelectListItem> controlAlumb()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Fotocelda", Text= "Fotocelda" },
                new SelectListItem { Value="Manual", Text="Manual"}};

        }
        public List<SelectListItem> tipoPiso()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Losa", Text= "Losa" },
               new SelectListItem { Value= "Gravilla", Text= "Gravilla" },
               new SelectListItem { Value="Tierra", Text="Tierra"}};

        }

        public List<SelectListItem> estadoPiso()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Buen estado", Text= "Buen estado" },
               new SelectListItem { Value= "Mal estado", Text= "Mal estado" },
               new SelectListItem { Value="Contaminado", Text="Contaminado"},
               new SelectListItem { Value="Enyerbado", Text="Enyerbado"}};

        }

        public List<SelectListItem> estadoPara()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Bueno", Text= "Bueno" },
               new SelectListItem { Value= "Malo", Text= "Malo" },
               new SelectListItem { Value="Fuera de norma", Text="Fuera de norma"},
               new SelectListItem { Value="No tiene", Text="No tiene"}};

        }

        public List<SelectListItem> ordenInterior()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Ordenado", Text= "Ordenado" },
               new SelectListItem { Value= "Desordenado", Text= "Desordenado" }};

        }

        public List<SelectListItem> estadoAreaExt()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Limpia", Text= "Limpia" },
               new SelectListItem { Value= "Sucia", Text= "Sucia" }};

        }

        public List<SelectListItem> BM()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value= "Bueno", Text= "Bueno" },
                new SelectListItem { Value="Malo", Text="Malo"}

            };
        }

        public List<SelectListItem> pruebasRealizadas()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value= "Ejecutada", Text= "Ejecutada" },
                new SelectListItem { Value="No ejecutada", Text="No ejecutada"}

            };
        }

        public List<SelectListItem> verif()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value= "Adecuada", Text= "Adecuada" },
                new SelectListItem { Value="Inadecuada", Text="Inadecuada"}

            };
        }

    }
}