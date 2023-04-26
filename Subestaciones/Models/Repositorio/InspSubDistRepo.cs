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
    public class InspSubDistRepo
    {
        private DBContext db;

        public InspSubDistRepo(DBContext db)
        {
            this.db = db;
        }


        public async Task<List<InspSubDist>> ObtenerInsps()
        {
            return await db.Database.SqlQuery<InspSubDist>(@"select Subestaciones.NombreSubestacion nombreSub,
            Subestaciones.Codigo codigoSub,
            Sub_CelajeSubDistribucion.nombreCelaje,
            Sub_CelajeSubDistribucion.fecha,
            Personal.Nombre realizadoPor
            from Sub_CelajeSubDistribucion
            inner join Subestaciones on Sub_CelajeSubDistribucion.codigoSub = Subestaciones.Codigo
            inner join Personal on Sub_CelajeSubDistribucion.realizadoPor = Personal.Id_Persona").ToListAsync();
        }

        public List<Desconectivos> FindInterruptorSub(string codigo)
        {
            var parametrocodigo = new SqlParameter("@codSub", codigo);

            return db.Database.SqlQuery<Desconectivos>(@"SELECT I.Codigo, F.FuncionDesconectivos tipoDesc
            FROM dbo.InstalacionDesconectivos I
            JOIN dbo.FuncionDesconectivos F ON I.Funcion = F.Id_FuncionDesconectivos
            WHERE I.UbicadaEn=@codSub AND (I.TipoSeccionalizador = '3' OR I.TipoSeccionalizador = '4' OR I.TipoSeccionalizador = '5'
               OR I.TipoSeccionalizador = '6' OR I.TipoSeccionalizador = '7')", parametrocodigo).ToList();
        }


        public List<InspTF> listaInspTransf(string nombre, string sub, DateTime fechaM)
        {
            var parametrocodigo = new SqlParameter("@codigo", sub);
            var parametrofecha = new SqlParameter("@f", fechaM);
            var parametronomb = new SqlParameter("@nomb", nombre);
            return db.Database.SqlQuery<InspTF>(@"SELECT TransformadoresSubtransmision.Nombre nombre, 
                TransformadoresSubtransmision.Id_Transformador,
                TransformadoresSubtransmision.Codigo codigoSub,
                TransformadoresSubtransmision.Id_EAdministrativa,   
                TransformadoresSubtransmision.Numemp Numemp, 
                TransformadoresSubtransmision.NoSerie NoSerie,
                Fabricantes.Nombre +','+ Fabricantes.Pais Fabricante,
                Sub_TransformadoresSubtrCelaje.fecha,
                Sub_TransformadoresSubtrCelaje.nombreCelaje
                from Sub_TransformadoresSubtrCelaje inner join TransformadoresSubtransmision on  Sub_TransformadoresSubtrCelaje.Id_Transformador =  TransformadoresSubtransmision.Id_Transformador 
                left join Fabricantes on TransformadoresSubtransmision.idFabricante = Fabricantes.Id_Fabricante
                where Sub_TransformadoresSubtrCelaje.nombreCelaje= @nomb and Sub_TransformadoresSubtrCelaje.codigoSub = @codigo and Sub_TransformadoresSubtrCelaje.fecha=@f", parametronomb, parametrocodigo, parametrofecha).ToList();

        }

        public List<InspTF> findInspTransf(string nombre, string sub, DateTime fechaM, int idt, int EA)
        {
            var parametrocodigo = new SqlParameter("@codigo", sub);
            var parametrofecha = new SqlParameter("@f", fechaM);
            var parametronomb = new SqlParameter("@nomb", nombre);
            var parametroidt = new SqlParameter("@idt", idt);
            var parametroEA = new SqlParameter("@EA", EA);
            return db.Database.SqlQuery<InspTF>(@"SELECT TransformadoresSubtransmision.Nombre nombre, 
                TransformadoresSubtransmision.Id_Transformador,
                TransformadoresSubtransmision.Codigo codigoSub,
                TransformadoresSubtransmision.Id_EAdministrativa,   
                TransformadoresSubtransmision.Numemp Numemp, 
                TransformadoresSubtransmision.NoSerie NoSerie,
                TransformadoresSubtransmision.PorcientoImpedancia,
                TransformadoresSubtransmision.GrupoConexion,
                TransformadoresSubtransmision.PesoAceite,
                TransformadoresSubtransmision.PesoTotal,
                TransformadoresSubtransmision.NroPosiciones,
                Capacidades.Capacidad capacidad,
                Fabricantes.Nombre +','+ Fabricantes.Pais Fabricante,
                vs.Voltaje TensionSecundaria,
                VoltajesSistemas.Voltaje TensionPrimaria,
                Sub_TransformadoresSubtrCelaje.*
                from Sub_TransformadoresSubtrCelaje inner join TransformadoresSubtransmision on  Sub_TransformadoresSubtrCelaje.Id_Transformador =  TransformadoresSubtransmision.Id_Transformador 
                left join Fabricantes on TransformadoresSubtransmision.idFabricante = Fabricantes.Id_Fabricante
                left join Capacidades on TransformadoresSubtransmision.Id_Capacidad = Capacidades.Id_Capacidad 
                left join VoltajesSistemas on TransformadoresSubtransmision.Id_VoltajePrim = VoltajesSistemas.Id_VoltajeSistema 
                left join VoltajesSistemas VS on TransformadoresSubtransmision.Id_Voltaje_Secun = VS.Id_VoltajeSistema 
                where Sub_TransformadoresSubtrCelaje.nombreCelaje= @nomb and Sub_TransformadoresSubtrCelaje.codigoSub = @codigo and Sub_TransformadoresSubtrCelaje.fecha=@f and Sub_TransformadoresSubtrCelaje.Id_EAdministrativa =@EA and Sub_TransformadoresSubtrCelaje.Id_Transformador=@idt", parametronomb, parametrocodigo, parametrofecha, parametroEA, parametroidt).ToList();

        }

        public List<InspInterruptores> listaInspInterr(string nombre, string codigo, DateTime fecha)
        {
            var parametrocodigo = new SqlParameter("@codigo", codigo);
            var parametrofecha = new SqlParameter("@f", fecha);
            var parametronomb = new SqlParameter("@nomb", nombre);

            return db.Database.SqlQuery<InspInterruptores>(@"SELECT  InstalacionDesconectivos.Codigo AS codigoInterruptor ,
                InstalacionDesconectivos.id_EAdministrativa_Prov ,  
                InstalacionDesconectivos.Id_EAdministrativa,
                InstalacionDesconectivos.CodigoNuevo ,
                InstalacionDesconectivos.NumeroFases ,
                InstalacionDesconectivos.CorrienteNominal ,
                InstalacionDesconectivos.UbicadaEn codigoSub,
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
                Sub_CelajeSubDistInterruptor.fecha,
                Sub_CelajeSubDistInterruptor.nombreCelaje
                FROM Sub_CelajeSubDistInterruptor inner join InstalacionDesconectivos on InstalacionDesconectivos.Codigo = Sub_CelajeSubDistInterruptor.codigoInterruptor
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
                where Sub_CelajeSubDistInterruptor.nombreCelaje = @nomb and Sub_CelajeSubDistInterruptor.CodigoSub = @codigo and Sub_CelajeSubDistInterruptor.fecha=@f", parametronomb, parametrocodigo, parametrofecha).ToList();
        }

        public List<InspInterruptores> FindInspInterr(string nombre, string inter, string codigo, DateTime fecha)
        {
            var parametrocodigo = new SqlParameter("@codigo", codigo);
            var parametrofecha = new SqlParameter("@f", fecha);
            var parametronomb = new SqlParameter("@nomb", nombre);
            var parametrointer = new SqlParameter("@inter", inter);

            return db.Database.SqlQuery<InspInterruptores>(@"SELECT  InstalacionDesconectivos.Codigo AS CodigoInt ,
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
                Sub_CelajeSubDistInterruptor.fecha,
                Sub_CelajeSubDistInterruptor.*
                FROM Sub_CelajeSubDistInterruptor inner join InstalacionDesconectivos on InstalacionDesconectivos.Codigo = Sub_CelajeSubDistInterruptor.codigoInterruptor
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
                where Sub_CelajeSubDistInterruptor.codigoInterruptor=@inter and Sub_CelajeSubDistInterruptor.nombreCelaje = @nomb and Sub_CelajeSubDistInterruptor.CodigoSub = @codigo and Sub_CelajeSubDistInterruptor.fecha=@f", parametrointer, parametronomb, parametrocodigo, parametrofecha).ToList();
        }
        public List<SelectListItem> tipoInsp()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Mensual", Text= "Mensual" },
               new SelectListItem { Value= "Especial", Text= "Especial" }};

        }

        public List<SelectListItem> estados()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Bien", Text= "Bien" },
               new SelectListItem { Value= "Regular", Text= "Regular" },
               new SelectListItem { Value="Mal", Text="Mal"}};

        }

        public List<SelectListItem> SN()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "1", Text= "Si" },
               new SelectListItem { Value= "0", Text= "No" }
             };

        }

        public List<SelectListItem> estadoPararrayoA()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Bien", Text= "Bien" },
               new SelectListItem { Value= "Regular", Text= "Regular" },
               new SelectListItem { Value="Mal", Text="Mal"},
               new SelectListItem { Value="No tiene", Text="No tiene"}};

        }

        public List<SelectListItem> estadoPararrayoB()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Bien", Text= "Bien" },
               new SelectListItem { Value= "Regular", Text= "Regular" },
               new SelectListItem { Value="Mal", Text="Mal"},
               new SelectListItem { Value="No tiene", Text="No tiene"},
               new SelectListItem { Value="Fuera norma", Text="Fuera norma"}};

        }

        public List<SelectListItem> estadoOtros()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Buenos", Text= "Buenos" },
               new SelectListItem { Value= "Malos", Text= "Malos" },
               new SelectListItem { Value="No tiene", Text="No tiene"}};

        }

        public List<SelectListItem> estadoDropOut()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Buen estado", Text= "Buen estado" },
               new SelectListItem { Value= "Mal estado", Text= "Mal estado" },
               new SelectListItem { Value="Esquelético", Text="Esquelético"}};

        }

        public List<SelectListItem> nivelAceite()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Normal", Text= "Normal" },
               new SelectListItem { Value= "Bajo", Text= "Bajo" }};

        }

        public List<SelectListItem> estadoOtrosInt()
        {
            return new List<SelectListItem> {
               new SelectListItem { Value= "Bueno", Text= "Bueno" },
               new SelectListItem { Value= "Malo", Text= "Malo" },
               new SelectListItem { Value="No tiene", Text="No tiene"}};

        }
    }
}