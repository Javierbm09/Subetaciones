using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Subestaciones.Models.Clases;

namespace Subestaciones.Models.Repositorio
{
    public class DefectoRepositorio
    {
        private DBContext db;
        public DefectoRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<DefectoViewModel> FindAsync(string Codigosub)
        {
            var lista = await ObtenerDefectos(Codigosub);
            return lista.Find(c => c.Instalacion == Codigosub);
        }

        public async Task<List<DefectoViewModel>> ObtenerDefectos(string codigo)
        {
            var parametrocodigo = new SqlParameter("@codigo", codigo);
            return await db.Database.SqlQuery<DefectoViewModel>(@"SELECT Defectos.Id_EAdministrativa Id_EAdministrativa, Defectos.NumAccion, 
                CASE WHEN Defectos.resuelto = 0 THEN 'No Resuelto' WHEN Defectos.cancelado = 1 THEN 'Cancelado' WHEN(Defectos.resuelto = 0 AND Defectos.cancelado = 0) 
                THEN 'Pendiente' END AS Estado,
                Defectos.Fecha Fecha, TipoDefecto.Defecto Defecto, Elementos.Elemento Elemento, materialpostes.Material Material, Defectos.Dimension Dimension, Defectos.Observaciones Observaciones
                FROM Defectos LEFT OUTER JOIN
                TipoDefecto ON Defectos.Id_Defecto = TipoDefecto.Id_Defecto LEFT OUTER JOIN
                Elementos ON Defectos.Elemento = Elementos.Id_Elemento LEFT OUTER JOIN
                materialpostes ON Defectos.Material = materialpostes.Id_Material
                WHERE Defectos.Instalacion = @codigo AND TipoInstalacion = 'E'", parametrocodigo).ToListAsync();

        }

        //obtener defecto pendiente de subestacion
        public async Task<List<DefectoViewModel>> ObtenerDefectosPendienteSubs(string codigo)
        {
            var parametrocodigo = new SqlParameter("@codigo", codigo);
            return await db.Database.SqlQuery<DefectoViewModel>(@"select DF.Fecha ,
        DF.FechaFin ,
        E.Elemento,
        MP.Material, 
        TD.Defecto,
case DF.prioridad
  when 1 then 'Urgente'
  when 2 then 'Para mantenimiento'
  when 3 then 'Fuera de servicio'
end prioridad
 from Defectos  DF left outer join Elementos E on
DF.Elemento=E.Id_Elemento  left outer join materialpostes MP on
DF.Material=MP.Id_Material left outer join TipoDefecto TD on
DF.Defecto=TD.Id_Defecto

where (Instalacion=@codigo) and DF.resuelto=0 and DF.Cancelado=0", parametrocodigo).ToListAsync();

        }

        public async Task<List<DefectoViewModel>> ObtenerDefectosSubT(string codigo)
        {
            var parametrocodigo = new SqlParameter("@codigo", codigo);
            return await db.Database.SqlQuery<DefectoViewModel>(@"SELECT Defectos.Id_EAdministrativa Id_EAdministrativa, Defectos.NumAccion, 
                CASE WHEN Defectos.resuelto = 0 THEN 'No Resuelto' WHEN Defectos.cancelado = 1 THEN 'Cancelado' WHEN(Defectos.resuelto = 0 AND Defectos.cancelado = 0) 
                THEN 'Pendiente' END AS Estado,
                Defectos.Fecha Fecha, TipoDefecto.Defecto Defecto, Elementos.Elemento Elemento, materialpostes.Material Material, Defectos.Dimension Dimension, Defectos.Observaciones Observaciones
                FROM Defectos LEFT OUTER JOIN
                TipoDefecto ON Defectos.Id_Defecto = TipoDefecto.Id_Defecto LEFT OUTER JOIN
                Elementos ON Defectos.Elemento = Elementos.Id_Elemento LEFT OUTER JOIN
                materialpostes ON Defectos.Material = materialpostes.Id_Material
                WHERE Defectos.Instalacion = @codigo AND TipoInstalacion = 'R'", parametrocodigo).ToListAsync();

        }

        public async Task<List<Proyectos>> ObtenerProyectos(string codigo)
        {
            var parametrocodigo = new SqlParameter("@Subestacion", codigo);
            return await db.Database.SqlQuery<Proyectos>(@"SELECT ProyProyecto.NombreProyecto NombreProyecto, 
               CASE ProyProyecto.EstadoProyecto 
               WHEN 0 THEN 'Solicitud'
               WHEN 1 THEN 'Proyectado'
               WHEN 2 THEN 'Financiado'
               WHEN 3 THEN 'Pendiente de Inicio'  
               WHEN 4 THEN 'Cancelado'
               WHEN 5 THEN 'En Ejecución'
               WHEN 6 THEN 'Paralizado'
               WHEN 7 THEN 'Terminado'    
               WHEN 8 THEN 'Cerrado'                       
               END as EstadoDelProyecto, 
               ProyInstalacionesAfectadas.Instalacion Instalacion
               FROM ProyProyecto INNER JOIN
               ProyInstalacionesAfectadas ON ProyProyecto.NumAccion = ProyInstalacionesAfectadas.NumAccion AND 
               ProyProyecto.id_EAdministrativa = ProyInstalacionesAfectadas.id_EAdministrativa
               WHERE ProyInstalacionesAfectadas.Instalacion = @Subestacion", parametrocodigo).ToListAsync();

        }

        public async Task<List<Mejoras>> ObtenerMejoras(string codigo)
        {
            var parametrocodigo = new SqlParameter("@Subestacion", codigo);
            return await db.Database.SqlQuery<Mejoras>(@"SELECT Distinct( ProyProyecto.NombreProyecto) Nombre, ProyProyecto.Preliminar Preliminar,
            CASE ProyProyecto.EstadoProyecto 
            WHEN 0 THEN 'Solicitud'
            WHEN 1 THEN 'Proyectado'
            WHEN 2 THEN 'Financiado'
            WHEN 3 THEN 'Pendiente de Inicio'  
            WHEN 4 THEN 'Cancelado'
            WHEN 5 THEN 'En Ejecución'
            WHEN 6 THEN 'Paralizado'
            WHEN 7 THEN 'Terminado'    
            WHEN 8 THEN 'Cerrado'                       
            END EstadoDelProyecto, 
            ProyProyecto.ValorPresup Presupuesto
            FROM ProyMejoras INNER JOIN
            ProyProyecto ON ProyMejoras.numaccionp = ProyProyecto.NumAccion AND 
            ProyMejoras.id_eadministrativap = ProyProyecto.id_EAdministrativa
            WHERE (ProyMejoras.instalacion =@Subestacion)", parametrocodigo).ToListAsync();

        }

        public async Task<List<Mttos>> ObtenerMttos(string codigo)
        {
            var parametrocodigo = new SqlParameter("@Subestacion", codigo);
            return await (from SM in db.Sub_Mantenimientos
                          join P in db.Personal on (int)SM.RealizadoPor equals P.Id_Persona


                          where SM.Codigo == codigo
                          select new Mttos
                          {
                              Nombre = SM.Nombre,
                              Fecha = SM.Fecha,
                              RealizadoPor = P.Nombre,
                              VoltajePrimario = SM.VoltajePrimario,
                              VoltajeSecundario = SM.VoltajeSecundario,
                              CapacidadVentilador = SM.CapacidadVentilador,
                              AguaDestilacion = SM.AguaDestilacion,
                              NivelAceite = SM.NivelAceite
                          }).ToListAsync();

            //return await db.Database.SqlQuery<Mttos>(@"SELECT Sub_Mantenimientos.Nombre Nombre, 
            //                 Sub_Mantenimientos.Fecha Fecha, Personal.Nombre RealizadoPor, 
            //                 Sub_Mantenimientos.VoltajePrimario VoltajePrimario, Sub_Mantenimientos.VoltajeSecundario VoltajeSecundario, 
            //                 Sub_Mantenimientos.CapacidadVentilador CapacidadVentilador, Sub_Mantenimientos.AguaDestilacion AguaDestilacion, 
            //                 Sub_Mantenimientos.NivelAceite NivelAceite
            //FROM Sub_Mantenimientos inner join Personal on Sub_Mantenimientos.RealizadoPor = Personal.Id_Persona
            //WHERE (Sub_Mantenimientos.Codigo =@Subestacion)", parametrocodigo).ToListAsync();

        }

        public async Task<List<Termografias>> ObtenerTermografias(string codigo, char tipo)
        {
            var parametrocodigo = new SqlParameter("@Subestacion", codigo);
            var parametrotipo = new SqlParameter("@tipo", tipo);
            return await db.Database.SqlQuery<Termografias>(@"SELECT Sub_Termografias.NumAccion numA, CAST(Sub_Termografias.Id_EAdministrativa AS INT) idEA, Sub_Termografias.fecha Fecha, CAST(Sub_Termografias.TempMedio AS INT) TempAmbiente, CAST(Sub_Termografias.transferencia AS INT) TransfLinea, Elementos.Elemento Elemento, OperariosJefes.Nombre EjecutadoPor, Sub_Termografias.DescripcionEquipo Designacion, 
            CASE  Sub_Termografias.Ejecutado 
            WHEN 0 THEN 'No'
            WHEN 1 THEN 'Si' END Ejecutado
            FROM Sub_Termografias left JOIN OperariosJefes on Sub_Termografias.EjecutadaPor = OperariosJefes.Id_Persona
            left join  Elementos ON Sub_Termografias.Elemento=Elementos.Id_Elemento 
            left join TipoInstalacionElemento on  Elementos.Id_Elemento = TipoInstalacionElemento.Id_Elemento AND TipoInstalacionElemento.Id_TipoInstalacion=@tipo
            WHERE Sub_Termografias.Subestacion =@Subestacion", parametrocodigo, parametrotipo).ToListAsync();

        }

        public async Task<List<PuntosCalientes>> ObtenerPtos(short EA, int numA)
        {
            var parametroEA = new SqlParameter("@EA", EA);
            var parametroNumA = new SqlParameter("@NumAccion", numA);
            return await db.Database.SqlQuery<PuntosCalientes>(@"SELECT Sub_PuntoTermografia.TempDetectada, materialpostes.Material, TipoDefecto.Defecto
            FROM Sub_PuntoTermografia
            LEFT JOIN MaterialPostes ON Sub_PuntoTermografia.Material = materialpostes.Id_Material
            LEFT JOIN TipoDefecto ON Sub_PuntoTermografia.estado = TipoDefecto.id_Defecto
            where (Sub_PuntoTermografia.Id_EAdministrativa=@EA) and (Sub_PuntoTermografia.NumAccion=@NumAccion)", parametroEA, parametroNumA).ToListAsync();

        }

        public async Task<List<Termografias>> ObtenerFoto(short EA, int numA)
        {
            var parametroEA = new SqlParameter("@EA", EA);
            var parametroNumA = new SqlParameter("@NumAccion", numA);
            return await db.Database.SqlQuery<Termografias>(@"SELECT Sub_PuntoTermografia.foto Foto
            FROM Sub_PuntoTermografia 
            where (Sub_PuntoTermografia.Id_EAdministrativa=@EA) and (Sub_PuntoTermografia.NumAccion=@NumAccion)", parametroEA, parametroNumA).ToListAsync();

        }

        public async Task<List<Desconectivos>> ObtenerDesconectivos(string codigo)
        {
            var parametroSub = new SqlParameter("@sub", codigo);
            return await db.Database.SqlQuery<Desconectivos>(@"SELECT InstalacionDesconectivos.Codigo Codigo,
            InstalacionDesconectivos.NumeroFases NumeroFases,
            InstalacionDesconectivos.CorrienteNominal CorrienteNominal,
            FuncionDesconectivos.FuncionDesconectivos Funcion
            FROM  InstalacionDesconectivos
            LEFT OUTER JOIN FuncionDesconectivos ON InstalacionDesconectivos.Funcion = FuncionDesconectivos.Id_FuncionDesconectivos
            where (InstalacionDesconectivos.ubicadaen=@sub)", parametroSub).ToListAsync();

        }

        public async Task<List<Inspecciones>> ObtenerInspecciones(string codigo)
        {
            var parametroSub = new SqlParameter("@sub", codigo);
            return await db.Database.SqlQuery<Inspecciones>(@"select Celaje.NombreCelaje TipoInspeccion, Celaje.fecha Fecha,Personal.Nombre RealizadoPor
        FROM Sub_Celaje Celaje INNER JOIN Personal ON Celaje.realizadoPor = Personal.Id_Persona
        WHERE Celaje.CodigoSub=@sub", parametroSub).ToListAsync();

        }

        public async Task<List<Perdidas>> ObtenerPerdidas(string codigo, string mes, string anho)
        {
            var parametroSub = new SqlParameter("@sub", codigo);
            var parametroAnho = new SqlParameter("@anho",anho);
            var parametroMes =new SqlParameter("@mes", mes);
            return await db.Database.SqlQuery<Perdidas>(@"SELECT  CircuitosPrimarios.CodigoCircuito Circuito, 
	   MES_Balance_Distribucion_EA.eEntregadaADistrib eEntregadaADistrib,
	   MES_Balance_Distribucion_EA.transfEntregadaEnDistrib transfEntregadaEnDistrib,
	   MES_Balance_Distribucion_EA.eTotalDisponibleEnCto eTotalDisponibleEnCto,
	   MES_Balance_Distribucion_EA.factClientesPorCto factClientesPorCto,
	   MES_Balance_Distribucion_EA.factSectorResidencial factSectorResidencial,
	   MES_Balance_Distribucion_EA.factSectorEstatalMayor factSectorEstatalMayor,
	   MES_Balance_Distribucion_EA.factSectorEstatalMenor factSectorEstatalMenor,
	   MES_Balance_Distribucion_EA.consumoEmpresa consumoEmpresa,
	   MES_Balance_Distribucion_EA.pCtoDistribucion pCtoDistribucion,
	   MES_Balance_Distribucion_EA.pPorcPCtoDistribucion pPorcPCtoDistribucion,
	   MES_BalancePorZBEMensual.PerdidasTotales pCtoDistribucion,
	   MES_BalancePorZBEMensual.PorcientoPerdidasTotales pPorcPCtoDistribucion,
	   MES_BalancePorZBEMensual.PerdidasTecnicas PerdidasTecnicas
FROM Subestaciones INNER JOIN
  CircuitosPrimarios ON CircuitosPrimarios.SubAlimentadora = Subestaciones.Codigo INNER JOIN
  MES_Balance_Distribucion_EA ON CircuitosPrimarios.CodigoCircuito = MES_Balance_Distribucion_EA.cto INNER JOIN 
     MES_ZonaDBalanceElectrica ON MES_Balance_Distribucion_EA.cto= MES_ZonaDBalanceElectrica.codigoInstBase INNER JOIN
     MES_BalancePorZBEMensual ON MES_BalancePorZBEMensual.idZBE = MES_ZonaDBalanceElectrica.idZB 
     AND MES_Balance_Distribucion_EA.mes=MES_BalancePorZBEMensual.mes
     AND MES_Balance_Distribucion_EA.anno=MES_BalancePorZBEMensual.anno
     WHERE  (MES_Balance_Distribucion_EA.mes=@mes) AND (MES_Balance_Distribucion_EA.anno=@anho) AND (Subestaciones.Codigo=@sub)", parametroSub, parametroAnho, parametroMes).ToListAsync();

        }

        public async Task<List<Protecciones>> ObtenerProtecciones(string codigo)
        {
            var parametroSub = new SqlParameter("@sub", codigo);
            return await db.Database.SqlQuery<Protecciones>(@"SELECT dbo.ES_Esquemas_Prot.Nombre nombre,
        dbo.ES_Esquemas_Prot.Tipo_Equipo_Primario equipoPriamrio,
        dbo.ES_Esquemas_Prot.Elemento_Electrico elementoElectrico,
        dbo.ES_Conexion_Rele_TC_TP.Tipo_Equipo tipoEquipo,
        dbo.ES_Conexion_Rele_TC_TP.rele rele,
        dbo.ES_Nomenclador_Conexion.Descripcion descripcion,
        dbo.ES_Conexion_Rele_TC_TP.Devanado devanado
        FROM dbo.ES_Esquemas_Prot
        INNER JOIN dbo.ES_Conexion_Rele_TC_TP ON dbo.ES_Esquemas_Prot.id_Esquema = dbo.ES_Conexion_Rele_TC_TP.esquema
        INNER JOIN dbo.ES_Nomenclador_Conexion ON dbo.ES_Conexion_Rele_TC_TP.Conexion = dbo.ES_Nomenclador_Conexion.id_Conexion
        WHERE dbo.ES_Esquemas_Prot.Subestacion = @sub", parametroSub).ToListAsync();

        }

        public  List<int> ObtenerAnno()
        {
            return  db.Database.SqlQuery<int>(@"SELECT DISTINCT (anno) anno FROM MES_Balance_Distribucion_EA UNION
SELECT DISTINCT(anno ) anno FROM MES_BalancePorZBEMensual ORDER BY anno DESC").ToList();

        }

        public List<SelectListItem> ObtenerMes()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value = "", Text = "" },
                new SelectListItem { Value= "1", Text= "Enero" },
                new SelectListItem { Value= "2", Text="Febrero"},
                new SelectListItem { Value= "3", Text= "Marzo" },
                new SelectListItem { Value= "4", Text="Abril"},
                new SelectListItem { Value= "5", Text= "Mayo" },
                new SelectListItem { Value= "5", Text="Junio"},
                new SelectListItem { Value= "6", Text= "Julio" },
                new SelectListItem { Value= "8", Text="Agosto"},
                new SelectListItem { Value= "9", Text= "Septiembre" },
                new SelectListItem { Value= "10", Text="Octubre"},
                new SelectListItem { Value= "11", Text= "Noviembre" },
                new SelectListItem { Value= "12", Text="Diciembre"}

            };

        }

    }
}