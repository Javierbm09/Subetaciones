using Subestaciones.Models.Clases;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using System.Data.SqlClient;

namespace Subestaciones.Models.Repositorio
{
    public class Sub_MttoTransformadoresUsoPlantaRepositorio
    {
        private DBContext db;
        public Sub_MttoTransformadoresUsoPlantaRepositorio(DBContext db)
        {
            this.db = db;
        }

        public async Task<List<Sub_MttoTransUsoPListViewModel>> ObtenerMttos()
        {
            return await db.Database.SqlQuery<Sub_MttoTransUsoPListViewModel>(@"SELECT  SubestacionesTransmision.NombreSubestacion NombreSubestacion,
                                                                                            
		                                                                     SubestacionesTransmision.Codigo subestacion,
                                                                                            
		                                                                     TipoMantenimiento.TipoMtto TipoMtto,

		                                                                     Sub_MttoTransUsoP.Id_Transformador,
                                                                                            
		                                                                     Sub_MttoTransUsoP.CodigoSub,
                                                                                            
		                                                                     Sub_MttoTransUsoP.Id_EATransformador,
                                                                                            
		                                                                     Sub_MttoTransUsoP.Id_EAdministrativa,

		                                                                     Sub_MttoTransUsoP.Fecha fechaMtto,
                                                                                           
		                                                                     CASE Sub_MttoTransUsoP.Mantenido
                                                                                                       
		                                                                     WHEN 0 THEN 'No'
                                                                                                        
		                                                                     WHEN 1 THEN 'Si'
                                                                                                        
		                                                                     END Mantenido,
                                                                                            
		                                                                     Personal.Nombre RealizadoPor
                                                                                            
                                                                     FROM    Sub_MttoTransUsoP 
                                                                                            
                                                                     inner join SubestacionesTransmision on Sub_MttoTransUsoP.CodigoSub = SubestacionesTransmision.Codigo
                                                                           
                                                                     inner join TipoMantenimiento on Sub_MttoTransUsoP.tipoMantenimiento = TipoMantenimiento.IdTipoMtto
                                                                                          
                                                                     inner join Personal on Sub_MttoTransUsoP.EjecutadoPor = Personal.Id_Persona").ToListAsync();
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

        public List<BancosTransformadores> listaBanco(string codigoSubTrans)
        {
            var parametrocodigoSubTrans = new SqlParameter("@codigo", codigoSubTrans);

            return db.Database.SqlQuery<BancosTransformadores>(@"SELECT Codigo FROM dbo.BancoTransformadores WHERE Circuito = @codigo", parametrocodigoSubTrans).ToList();
        }

        public List<TransformadoresViewModel> NumEmp(string codigoBanco)
        {
            var parametrocodigoBanco = new SqlParameter("@codigoBanco", codigoBanco);

            return db.Database.SqlQuery<TransformadoresViewModel>(@"SELECT Numemp FROM dbo.Transformadores WHERE Codigo = @codigoBanco", parametrocodigoBanco).ToList();
        }

        public List<SelectListItem> EstadoTanqueExpansion()
        {
            return new List<SelectListItem>
            {
                        new SelectListItem { Value="Buen Estado", Text="Buen Estado"},
                        new SelectListItem { Value= "Mal Estado", Text= "Mal Estado" },
                        new SelectListItem { Value = "No Tiene", Text = "No Tiene" }};
        }

        public List<SelectListItem> EstadoIndNivelaceite()
        {
            return new List<SelectListItem>
            {
                        new SelectListItem { Value="Buen Estado", Text="Buen Estado"},
                        new SelectListItem { Value= "Mal Estado", Text= "Mal Estado" }};

        }

        public List<SelectListItem> Nivelaceite()
        {
            return new List<SelectListItem>
            {
                        new SelectListItem { Value="Alto", Text="Alto"},
                        new SelectListItem { Value= "Normal", Text= "Normal" },
                        new SelectListItem { Value= "Bajo", Text= "Bajo" }};

        }

        public List<SelectListItem> AterramientoTanque()
        {
            return new List<SelectListItem>
            {
                        new SelectListItem { Value="Correcto", Text="Correcto"},
                        new SelectListItem { Value= "Incorrecto", Text= "Incorrecto" },
                        new SelectListItem { Value= "No Tiene", Text= "No Tiene" }};

        }

        public List<SelectListItem> SaliderosResumideros()
        {
            return new List<SelectListItem>
            {
                        new SelectListItem { Value="Tiene", Text="Tiene"},
                        new SelectListItem { Value= "No Tiene", Text= "No Tiene" }};

        }


        public SelectList ObtenerIncremto(int id_TipoMedicion)
        {
            var instrumento = (from instrumentos in db.Sub_NomInstrumentoMedicion
                               join tipoMedicion in db.Sub_NomTipoMedicion on instrumentos.Id_TipoMedicion equals tipoMedicion.Id_TipoMedicion
                               where tipoMedicion.Id_TipoMedicion == id_TipoMedicion
                               select new SelectListItem
                               {
                                   Value = instrumentos.Id_InstrumentoMedicion.ToString(),
                                   Text = instrumentos.Instrumento + " - " + instrumentos.ModeloTipo + " - " + instrumentos.Serie
                               }).ToList();

            return new SelectList(instrumento, "Value", "Text");
        }

        public List<SelectListItem> NormaRigidezDielectrica()
        {
            return new List<SelectListItem>
            {
                        new SelectListItem { Value="IEC-156-1995", Text="IEC-156-1995"},
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

        public List<Sub_MttoTransUsoPListViewModel> cargarNombreSub(string codSubTrans)
        {

            var parametroCodSubTrans = new SqlParameter("@codSubTrans", codSubTrans);

            return db.Database.SqlQuery<Sub_MttoTransUsoPListViewModel>(@"SELECT NombreSubestacion FROM dbo.SubestacionesTransmision WHERE Codigo = @codSubTrans ", parametroCodSubTrans).ToList();

        }


        public List<DatosTransformadorUsoPlantaViewModel> datosMttoTUP(string numEmpresa)
        {

            var parametroNumEmpresa = new SqlParameter("@numEmpresa", numEmpresa);

            return db.Database.SqlQuery<DatosTransformadorUsoPlantaViewModel>(@"SELECT  T.Codigo ,
                                                                                    CAST(T.Id_EAdministrativa AS VARCHAR) + ','
                                                                                    + CAST(T.Id_Transformador AS VARCHAR) ID ,
                                                                                    T.Id_EAdministrativa ,
                                                                                    T.Id_Transformador ,
                                                                                    T.Numemp ,
                                                                                    T.NoSerie ,
                                                                                    ISNULL(F.Nombre, '') + ', ' + ISNULL(F.Pais, '') Fabricante ,
                                                                                    ISNULL(CAST(GV.TPrimaria1 AS VARCHAR), '') + '/'
                                                                                    + ISNULL(CAST(GV.TPrimaria2 AS VARCHAR), '') + '----'
                                                                                    + ISNULL(CAST(GV.TSecundaria1 AS VARCHAR), '') + '/'
                                                                                    + ISNULL(CAST(GV.Tsecundaria2 AS VARCHAR), '') Tension ,
                                                                                    T.Impedancia ,
                                                                                    ISNULL(C.Capacidad, '') Capacidad ,
                                                                                    T.Peso ,
                                                                                    T.PesoAislante ,
                                                                                    T.TapEncontrado ,
                                                                                    T.TapDejado ,
                                                                                    T.PolaridadGrupo ,
                                                                                    VP.Voltaje VP ,
                                                                                    VS.Voltaje VS
                                                                            FROM    Transformadores T
                                                                                    LEFT JOIN dbo.Fabricantes F ON T.Id_Fabricante = F.Id_Fabricante
                                                                                    LEFT JOIN dbo.GruposVoltaje GV ON T.id_GrupoVoltaje = GV.id_grupovoltaje
                                                                                    LEFT JOIN dbo.Capacidades C ON T.Id_Capacidad = C.Id_Capacidad
                                                                                    LEFT OUTER JOIN dbo.VoltajesSistemas VP ON T.Id_VoltajePrim = VP.Id_VoltajeSistema
                                                                                    LEFT JOIN dbo.VoltajesSistemas VS ON T.Id_Voltaje_Secun = VS.Id_VoltajeSistema
                                                                            WHERE   T.Numemp = @numEmpresa ", parametroNumEmpresa).ToList();

        }

        public string ObtenerBanco(short Id_EATransformador, int Id_Transformador)
        {
            var parametroId_EATransformador = new SqlParameter("@Id_EATransformador", Id_EATransformador);
            var parametroId_Transformador = new SqlParameter("@Id_Transformador", Id_Transformador);

            return db.Database.SqlQuery<string>(@"SELECT Codigo FROM dbo.Transformadores  WHERE Id_EAdministrativa = @Id_EATransformador AND Id_Transformador = @Id_Transformador ", parametroId_EATransformador, parametroId_Transformador).First();

        }

        public string ObtenerNombreSub(string codigoSub)
        {
            var parametroNombreSub = new SqlParameter("@codigoSub", codigoSub);

            return db.Database.SqlQuery<string>(@"SELECT NombreSubestacion FROM dbo.SubestacionesTransmision WHERE Codigo = @codigoSub ", parametroNombreSub).First();

        }


    }
}