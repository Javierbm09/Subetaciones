using Subestaciones.Models.Clases;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Subestaciones.Models.Repositorio
{
    public class SubMttoBateriasEstacionariasRepositorio
    {
        private DBContext db;
        public SubMttoBateriasEstacionariasRepositorio(DBContext db)
        {
            this.db = db;
        }

        public SelectList tipoMtto()
        {
            var tipo = (from tipos in db.TipoMantenimiento
                        where tipos.EstipoMttoSubTrans == true
                        select new SelectListItem
                        {
                            Value = tipos.IdTipoMtto.ToString(),
                            Text = tipos.TipoMtto

                        }).ToList();
            return new SelectList(tipo, "Value", "Text");

        }


        public List<Sub_NomInstrumentoMedicion> tipoInstrumentos(int idMBE, int idBateria, short redEA)
        {
            var parametroIdMBE = new SqlParameter("@idMBE", idMBE);
            var parametroIdBateria = new SqlParameter("@id", idBateria);
            var parametroRed = new SqlParameter("@red", redEA);

            return db.Database.SqlQuery<Sub_NomInstrumentoMedicion>(@"SELECT	*

                                                                        FROM	dbo.Sub_NomInstrumentoMedicion

                                                                        WHERE	Id_TipoMedicion = 1 OR Id_TipoMedicion = 6 OR Id_TipoMedicion = 7 AND Id_InstrumentoMedicion NOT IN (

		                                                                        SELECT Id_InstrumentoMedicion 

		                                                                        FROM dbo.Sub_MttoBateriaEstac_Instrumentos 

		                                                                        WHERE id_MttoBatEstacionarias = @idMBE AND id_Bateria= @id AND EA_RedCD = @red)", parametroIdMBE, parametroIdBateria, parametroRed).ToList();

        }

        public List<DatosChapaBancoBateriaViewModel> listaBaterias(string codigoSub)
        {
            var parametrocodigoSub = new SqlParameter("@codigo", codigoSub);

            return db.Database.SqlQuery<DatosChapaBancoBateriaViewModel>(@"SELECT   SubNB.TipoBateria,
                        
                                                                        SubB.Id_Bateria,

		                                                                SubB.id_EAdministrativa,

		                                                                SubNB.CapacidadBateria,

		                                                                SubNB.TensionBateria,

		                                                                SubNB.ClaseBateria,

		                                                                SubRCD.NombreServicioCD

                                                                FROM    dbo.Sub_Baterias AS SubB

		                                                                INNER JOIN dbo.Sub_NomBaterias AS SubNB ON SubB.Tipo = SubNB.IdBateria

		                                                                INNER JOIN dbo.Sub_RedCorrienteDirecta AS SubRCD ON SubRCD.Id_ServicioCD = SubB.Id_ServicioCDBat

                                                                WHERE   SubRCD.codigo = @codigo", parametrocodigoSub).ToList();

        }

        public async Task insertarInstrumento(int idIM, int idMBE, int idB, short redEA)
        {
            if (!await ValidarSiExisteInstrumento(idIM, idMBE, idB, redEA))
            {
                try
                {
                    Sub_MttoBateriaEstac_Instrumentos instrumento = new Sub_MttoBateriaEstac_Instrumentos();
                    instrumento.Id_InstrumentoMedicion = idIM;
                    instrumento.id_MttoBatEstacionarias = idMBE;
                    instrumento.id_Bateria = idB;
                    instrumento.EA_RedCD = redEA;

                    db.Entry(instrumento).State = EntityState.Added;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new HttpException((int)HttpStatusCode.BadRequest, "Ocurrió un error al insertar el instrumento.");
                }
            }
            else
            {
                throw new HttpException(httpCode: (int)HttpStatusCode.Conflict, message: "Lo sentimos no se puede insertar el instrumento, ya existe en la subestación.");
            }
        }

        public async Task<bool> ValidarSiExisteInstrumento(int idIM, int idMBE, int idB, short redEA)
        {
            return await db.Sub_MttoBateriaEstac_Instrumentos.AnyAsync(c => c.Id_InstrumentoMedicion == idIM
                                                                         && c.id_MttoBatEstacionarias == idMBE
                                                                         && c.id_Bateria == idB
                                                                         && c.EA_RedCD == redEA);
        }

        public async Task<List<InstrumentoViewModel>> listarInstrumentos(int idMBE, int idBateria, short redEA)
        {
            var parametroIdMBE = new SqlParameter("@idMBE", idMBE);
            var parametroIdBateria = new SqlParameter("@id", idBateria);
            var parametroRed = new SqlParameter("@red", redEA);

            return await db.Database.SqlQuery<InstrumentoViewModel>(@"SELECT  SubNIM.Instrumento,

                                                                                    SubNIM.ModeloTipo

                                                                                    FROM    dbo.Sub_MttoBateriaEstac_Instrumentos AS SubMBEI 
        
                                                                                    INNER JOIN dbo.Sub_NomInstrumentoMedicion AS SubNIM ON SubMBEI.Id_InstrumentoMedicion = SubNIM.Id_InstrumentoMedicion

                                                                                    WHERE SubMBEI.id_MttoBatEstacionarias= @idMBE AND SubMBEI.id_Bateria= @id AND SubMBEI.EA_RedCD = @red   ", parametroIdMBE, parametroIdBateria, parametroRed).ToListAsync();

        }

        public List<Sub_MttoBateriaEstac_Instrumentos> EliminarInstrumento(string instrumento, int idMBE, int idBateria, short redEA)
        {
            var parametroInst = new SqlParameter("@nombreInst", instrumento);
            var parametroIdMBE = new SqlParameter("@idMBE", idMBE);
            var parametroIdBateria = new SqlParameter("@id", idBateria);
            var parametroRed = new SqlParameter("@red", redEA);

            return db.Database.SqlQuery<Sub_MttoBateriaEstac_Instrumentos>(@"SELECT   SubMBEI.Id_InstrumentoMedicion,

                                                                                       SubMBEI.id_MttoBatEstacionarias,

                                                                                       SubMBEI.id_Bateria,

                                                                                       SubMBEI.EA_RedCD

                                                                              FROM dbo.Sub_MttoBateriaEstac_Instrumentos AS SubMBEI 

                                                                              INNER JOIN dbo.Sub_NomInstrumentoMedicion AS SubNIM ON SubMBEI.Id_InstrumentoMedicion = SubNIM.Id_InstrumentoMedicion
                                                                              
                                                                              WHERE SubNIM.Instrumento = @nombreInst AND  SubMBEI.id_MttoBatEstacionarias = @idMBE AND SubMBEI.id_Bateria = @id AND SubMBEI.EA_RedCD = @red", parametroInst, parametroIdMBE, parametroIdBateria, parametroRed).ToList();

        }

        public List<DatosChapaBancoBateriaViewModel> redCorrienteDirecta(int idBateria, string codigoSub)
        {

            var parametroIdBateria = new SqlParameter("@id", idBateria);
            var parametrocodigoSub = new SqlParameter("@codigo", codigoSub);

            return db.Database.SqlQuery<DatosChapaBancoBateriaViewModel>(@"SELECT  SubNB.TipoBateria ,
		
		                                                                           SubB.Id_Bateria,

		                                                                           SubB.CantidadVasos,

		                                                                           SubB.id_EAdministrativa,
			   
		                                                                           SubNB.CapacidadBateria ,

		                                                                           SubNB.TensionBateria,

		                                                                           SubNB.ClaseBateria,

		                                                                           SubRCD.NombreServicioCD

                                                                           FROM    dbo.Sub_Baterias AS SubB

		                                                                           INNER JOIN dbo.Sub_NomBaterias AS SubNB ON SubB.Tipo = SubNB.IdBateria

		                                                                           INNER JOIN dbo.Sub_RedCorrienteDirecta AS SubRCD ON SubRCD.Id_ServicioCD = SubB.Id_ServicioCDBat

                                                                           WHERE   SubB.Id_Bateria = @id AND SubRCD.codigo = @codigo", parametroIdBateria, parametrocodigoSub).ToList();

        }

        public List<DatosChapaBancoBateriaViewModel> mttoComponentes(int idBateria, short redEA, string codigoSub)
        {

            var parametroIdBateria = new SqlParameter("@id", idBateria);
            var parametroRed = new SqlParameter("@red", redEA);
            var parametrocodigoSub = new SqlParameter("@codigo", codigoSub);

            return db.Database.SqlQuery<DatosChapaBancoBateriaViewModel>(@"SELECT   SubNB.TipoBateria ,

                                                                                    SubB.CantidadVasos,

		                                                                            SubMBE.id_MttoBatEstacionarias,

		                                                                            SubMBE.subestacion,

		                                                                            SubMBE.EA_RedCD,

		                                                                            SubB.Id_Bateria,

		                                                                            SubB.id_EAdministrativa,

		                                                                            SubNB.CapacidadBateria ,

		                                                                            SubNB.TensionBateria,

		                                                                            SubNB.ClaseBateria,

		                                                                            SubRCD.NombreServicioCD

                                                                            FROM    dbo.Sub_Baterias AS SubB
		
		                                                                            INNER JOIN dbo.Sub_MttoBateriasEstacionarias AS SubMBE ON SubB.Id_Bateria = SubMBE.id_Bateria

		                                                                            INNER JOIN dbo.Sub_NomBaterias AS SubNB ON SubB.Tipo = SubNB.IdBateria

		                                                                            INNER JOIN dbo.Sub_RedCorrienteDirecta AS SubRCD ON SubRCD.Id_ServicioCD = SubB.Id_ServicioCDBat

                                                                            WHERE   SubB.Id_Bateria = @id AND SubMBE.EA_RedCD = @red  AND SubRCD.codigo = @codigo", parametroIdBateria, parametroRed, parametrocodigoSub).ToList();

        }

        public async Task<List<Sub_MttoBateriasEstacionariasList>> ObtenerMttos()
        {
            return await db.Database.SqlQuery<Sub_MttoBateriasEstacionariasList>(@"SELECT   SubestacionesTransmision.NombreSubestacion NombreSubestacion,
                                                                                            
                                                                                            SubestacionesTransmision.Codigo subestacion,
                                                                                            
                                                                                            TipoMantenimiento.TipoMtto TipoMtto,

                                                                                            Sub_MttoBateriasEstacionarias.id_MttoBatEstacionarias,
                                                                                            
                                                                                            Sub_MttoBateriasEstacionarias.fechaMtto,
                                                                                            
                                                                                            Sub_MttoBateriasEstacionarias.id_Bateria,
                                                                                            
                                                                                            Sub_MttoBateriasEstacionarias.EA_RedCD,
                                                                                           
                                                                                            CASE Sub_MttoBateriasEstacionarias.Mantenido
                                                                                                       
                                                                                            WHEN 0 THEN 'No'
                                                                                                        
                                                                                            WHEN 1 THEN 'Si'
                                                                                                        
                                                                                            END Mantenido,
                                                                                            
                                                                                            Personal.Nombre RealizadoPor
                                                                                            
                                                                                            FROM Sub_MttoBateriasEstacionarias 
                                                                                            
                                                                                            inner join dbo.SubestacionesTransmision on Sub_MttoBateriasEstacionarias.subestacion = dbo.SubestacionesTransmision.Codigo
                                                                           
                                                                                            inner join TipoMantenimiento on Sub_MttoBateriasEstacionarias.TipoMtto = TipoMantenimiento.IdTipoMtto
                                                                                          
                                                                                            inner join Personal on Sub_MttoBateriasEstacionarias.RealizadoPor = Personal.Id_Persona").ToListAsync();
        }


        public List<SelectListItem> nivel()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value="Bajo", Text="Bajo"},
                new SelectListItem { Value= "Normal", Text= "Normal" },
                new SelectListItem { Value = "No procede", Text = "No procede" }};
        }

    }
}