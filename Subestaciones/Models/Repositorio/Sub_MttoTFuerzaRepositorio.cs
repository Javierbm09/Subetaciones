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
    public class Sub_MttoTFuerzaRepositorio
    {

        private DBContext db;
        public Sub_MttoTFuerzaRepositorio(DBContext db)
        {
            this.db = db;
        }

        public async Task<List<Sub_MttoTFuerzaList>> ObtenerMttos()
        {
            return await db.Database.SqlQuery<Sub_MttoTFuerzaList>(@"SELECT  SubestacionesTransmision.NombreSubestacion NombreSubestacion,
                                                                                            
		                                                                     SubestacionesTransmision.Codigo subestacion,
                                                                                            
		                                                                     TipoMantenimiento.TipoMtto TipoMtto,

		                                                                     Sub_MttoTFuerza.Id_MttoTFuerza,
                                                                                            
		                                                                     Sub_MttoTFuerza.Id_TFuerza,
                                                                                            
		                                                                     Sub_MttoTFuerza.Id_EAdministrativaTFuerza,
                                                                                            
		                                                                     Sub_MttoTFuerza.Id_EAdministrativa,

		                                                                     Sub_MttoTFuerza.Fecha fechaMtto,
                                                                                           
		                                                                     CASE Sub_MttoTFuerza.Mantenido
                                                                                                       
		                                                                     WHEN 0 THEN 'No'
                                                                                                        
		                                                                     WHEN 1 THEN 'Si'
                                                                                                        
		                                                                     END Mantenido,
                                                                                            
		                                                                     Personal.Nombre RealizadoPor
                                                                                            
                                                                     FROM    Sub_MttoTFuerza 
                                                                                            
                                                                     inner join SubestacionesTransmision on Sub_MttoTFuerza.Subestacion = SubestacionesTransmision.Codigo
                                                                           
                                                                     inner join TipoMantenimiento on Sub_MttoTFuerza.tipoMantenimiento = TipoMantenimiento.IdTipoMtto
                                                                                          
                                                                     inner join Personal on Sub_MttoTFuerza.RevisadoPor = Personal.Id_Persona").ToListAsync();
        }

        public SelectList tipoMtto()
        {
            var tipo = (from tipos in db.TipoMantenimiento
                        where tipos.EstipoMttoSubTransTrasfFuerza == true
                        select new SelectListItem
                        {
                            Value = tipos.IdTipoMtto.ToString(),
                            Text = tipos.TipoMtto

                        }).ToList();
            return new SelectList(tipo, "Value", "Text");
        }

        public SelectList instrumentoUtilizadoAislamientoEnrrollado()
        {
            var instrumento = (from instrumentos in db.Sub_NomInstrumentoMedicion
                               join tipoMedicion in db.Sub_NomTipoMedicion on instrumentos.Id_TipoMedicion equals tipoMedicion.Id_TipoMedicion
                               where tipoMedicion.Id_TipoMedicion == 2
                               select new SelectListItem
                               {
                                   Value = instrumentos.Id_InstrumentoMedicion.ToString(),
                                   Text = instrumentos.Instrumento
                               }).ToList();

            return new SelectList(instrumento, "Value", "Text");
        }

        public SelectList instrumentoUtilizadoTDEnrrollado()
        {
            var instrumento = (from instrumentos in db.Sub_NomInstrumentoMedicion
                               join tipoMedicion in db.Sub_NomTipoMedicion on instrumentos.Id_TipoMedicion equals tipoMedicion.Id_TipoMedicion
                               where tipoMedicion.Id_TipoMedicion == 4
                               select new SelectListItem
                               {
                                   Value = instrumentos.Id_InstrumentoMedicion.ToString(),
                                   Text = instrumentos.Instrumento
                               }).ToList();

            return new SelectList(instrumento, "Value", "Text");
        }

        public List<DatosChapaTFuerzaViewModel> listaTFuerza(string codigoSub)
        {
            var parametrocodigoSub = new SqlParameter("@codigo", codigoSub);

            return db.Database.SqlQuery<DatosChapaTFuerzaViewModel>(@"SELECT TF.Id_Transformador,
                                                                             TF.Nombre
                                                                      FROM  dbo.TransformadoresTransmision TF
                                                                      WHERE TF.Codigo = @codigo", parametrocodigoSub).ToList();
        }

        public List<DatosChapaTFuerzaViewModel> datosParaCadaTFuerza(string codigoSub, int idTransformador)
        {

            var parametroCodSub = new SqlParameter("@codSub", codigoSub);
            var parametroIDT = new SqlParameter("@idT", idTransformador);

            return db.Database.SqlQuery<DatosChapaTFuerzaViewModel>(@"SELECT   TF.Id_Transformador,
                                                                               TF.Id_EAdministrativa,
	                                                                           TF.Nombre,
	                                                                           TF.NoSerie,
                                                                               TF.Numemp,
                                                                               TF.NroPosiciones,
                                                                               C.Capacidad,
                                                                               VSP.Voltaje VoltajeP,
                                                                               VSS.Voltaje VoltajeS,
                                                                               VST.Voltaje VoltajeT
                                                                        FROM  dbo.TransformadoresTransmision TF
                                                                        LEFT JOIN dbo.Capacidades AS C ON C.Id_Capacidad = TF.Id_Capacidad 
                                                                        LEFT JOIN dbo.VoltajesSistemas AS VSP ON VSP.Id_VoltajeSistema = TF.Id_VoltajePrim
                                                                        LEFT JOIN dbo.VoltajesSistemas AS VSS ON VSS.Id_VoltajeSistema = TF.Id_Voltaje_Secun
                                                                        LEFT JOIN dbo.VoltajesSistemas AS VST ON VST.Id_VoltajeSistema = TF.VoltajeTerciario
                                                                        WHERE TF.Codigo = @codSub AND TF.Id_Transformador = @idT ", parametroCodSub, parametroIDT).ToList();

        }


        //public List<DatosChapaTFuerzaViewModel> mttoComponentes(int idBateria, int redEA/*, string codigoSub*/)
        //{

        //    var parametroIdBateria = new SqlParameter("@id", idBateria);
        //    var parametroRed = new SqlParameter("@red", redEA);
        //    //var parametrocodigoSub = new SqlParameter("@codigo", codigoSub);

        //    return db.Database.SqlQuery<DatosChapaTFuerzaViewModel>(@"SELECT   SubNB.TipoBateria ,

        //                                                                            SubB.CantidadVasos,

        //                                                                      SubMBE.id_MttoBatEstacionarias,

        //                                                                      SubMBE.subestacion,

        //                                                                      SubMBE.EA_RedCD,

        //                                                                      SubB.Id_Bateria,

        //                                                                      SubB.id_EAdministrativa,

        //                                                                      SubNB.CapacidadBateria ,

        //                                                                      SubNB.TensionBateria,

        //                                                                      SubNB.ClaseBateria,

        //                                                                      SubRCD.NombreServicioCD

        //                                                                    FROM    dbo.Sub_Baterias AS SubB

        //                                                                      INNER JOIN dbo.Sub_MttoBateriasEstacionarias AS SubMBE ON SubB.Id_Bateria = SubMBE.id_Bateria

        //                                                                      INNER JOIN dbo.Sub_NomBaterias AS SubNB ON SubB.Tipo = SubNB.IdBateria

        //                                                                      INNER JOIN dbo.Sub_RedCorrienteDirecta AS SubRCD ON SubRCD.Id_ServicioCD = SubB.Id_ServicioCDBat

        //                                                                    WHERE   SubB.Id_Bateria = @id AND SubMBE.EA_RedCD = @red /* AND SubRCD.codigo = @codigo*/", parametroIdBateria, parametroRed/*, parametrocodigoSub*/).ToList();

        //}

        //public List<Sub_NomInstrumentoMedicion> tipoInstrumentos(int idMBE, int idBateria, short redEA)
        //{
        //    var parametroIdMBE = new SqlParameter("@idMBE", idMBE);
        //    var parametroIdBateria = new SqlParameter("@id", idBateria);
        //    var parametroRed = new SqlParameter("@red", redEA);

        //    return db.Database.SqlQuery<Sub_NomInstrumentoMedicion>(@"SELECT	*

        //                                                                FROM	dbo.Sub_NomInstrumentoMedicion

        //                                                                WHERE	Id_TipoMedicion = 1 OR Id_TipoMedicion = 6 OR Id_TipoMedicion = 7 AND Id_InstrumentoMedicion NOT IN (

        //                                                                  SELECT Id_InstrumentoMedicion 

        //                                                                  FROM dbo.Sub_MttoBateriaEstac_Instrumentos 

        //                                                                  WHERE id_MttoBatEstacionarias = @idMBE AND id_Bateria= @id AND EA_RedCD = @red)", parametroIdMBE, parametroIdBateria, parametroRed).ToList();

        //}



        public void InsertarTDBushings(int idMTF, string sec, string esq, double cap, double tanD)
        {
            try
            {
                Sub_MttoTFuerzaTanDeltaBushings tDBushings = new Sub_MttoTFuerzaTanDeltaBushings();
                tDBushings.Id_MttoTFuerza = idMTF;
                tDBushings.Seccion = sec;
                tDBushings.Esquema = esq;
                tDBushings.Capacitancia = cap;
                tDBushings.TanDelta = tanD;

                db.Entry(tDBushings).State = EntityState.Added;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Ocurrió un error al insertar el tDBushings.");
            }
        }
        
        public void ActualizarTDBushings(int idTDB, int idMTF, string sec, string esq, double cap, double tanD)
        {
            Sub_MttoTFuerzaTanDeltaBushings tDBushings = db.Sub_MttoTFuerzaTanDeltaBushings.Find(idTDB);
            if (tDBushings != null)
            {
                tDBushings.Id_MttoTFuerza = idMTF;
                tDBushings.Seccion = sec;
                tDBushings.Esquema = esq;
                tDBushings.Capacitancia = cap;
                tDBushings.TanDelta = tanD;

                db.Entry(tDBushings).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Ocurrió un error al insertar el tDBushings.");
            }
        }

        public async Task<List<Sub_MttoTFuerzaTanDeltaBushings>> listarTDBushings(int idMTF)
        {
            var parametroIdMTF = new SqlParameter("@idMTF", idMTF);

            return await db.Database.SqlQuery<Sub_MttoTFuerzaTanDeltaBushings>(@"SELECT Id_TanDeltaBushing,
                                                                                 Id_MttoTFuerza,
                                                                                 Seccion,
                                                                                 Esquema,
                                                                                 Capacitancia,
                                                                                 TanDelta 
                                                                          FROM dbo.Sub_MttoTFuerzaTanDeltaBushings
                                                                          WHERE Id_MttoTFuerza = @idMTF", parametroIdMTF).ToListAsync();
        }
        
        
        public void InsertarTDEnrrollado(int idMTF, string sec, string esq, double cap, double tanD)
        {
            try
            {
                Sub_MttoTFuerzaTanDeltaEnrollado tDEnrollado = new Sub_MttoTFuerzaTanDeltaEnrollado();
                tDEnrollado.Id_MttoTFuerza = idMTF;
                tDEnrollado.Seccion = sec;
                tDEnrollado.Esquema = esq;
                tDEnrollado.Capacitancia = cap;
                tDEnrollado.TangenteDelta = tanD;

                db.Entry(tDEnrollado).State = EntityState.Added;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Ocurrió un error al insertar el tDBushings.");
            }
        }
        
        public void ActualizarTDEnrrollado(int idTDE, int idMTF, string sec, string esq, double cap, double tanD)
        {
            Sub_MttoTFuerzaTanDeltaEnrollado tDEnrollado = db.Sub_MttoTFuerzaTanDeltaEnrollado.Find(idTDE);
            if (tDEnrollado != null)
            {
                tDEnrollado.Id_MttoTFuerza = idMTF;
                tDEnrollado.Seccion = sec;
                tDEnrollado.Esquema = esq;
                tDEnrollado.Capacitancia = cap;
                tDEnrollado.TangenteDelta = tanD;

                db.Entry(tDEnrollado).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Ocurrió un error al insertar el tDBushings.");
            }
        }

        public async Task<List<Sub_MttoTFuerzaTanDeltaEnrollado>> listarTDEnrrollado(int idMTF)
        {
            var parametroIdMTF = new SqlParameter("@idMTF", idMTF);

            return await db.Database.SqlQuery<Sub_MttoTFuerzaTanDeltaEnrollado>(@"SELECT Id_TanDeltaEnrollados,
                                                                                 Id_MttoTFuerza,
                                                                                 Seccion,
                                                                                 Esquema,
                                                                                 Capacitancia,
                                                                                 TangenteDelta 
                                                                          FROM dbo.Sub_MttoTFuerzaTanDeltaEnrollado
                                                                          WHERE Id_MttoTFuerza = @idMTF", parametroIdMTF).ToListAsync();
        }

        public void InsertarCorrienteExit(int idMTF, int tap, double a, double b, double c, double porcDesv)
        {
            try
            {
                Sub_MttoTFuerzaCorrienteExit corrienteExit = new Sub_MttoTFuerzaCorrienteExit();
                corrienteExit.Id_MttoTFuerza = idMTF;
                corrienteExit.Tap = tap;
                corrienteExit.A_0 = a;
                corrienteExit.B_0 = b;
                corrienteExit.C_0 = c;
                corrienteExit.PorcientoDesviacion = porcDesv;

                db.Entry(corrienteExit).State = EntityState.Added;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Ocurrió un error al insertar la Corriente Exitación.");
            }
        }

        public async Task<List<Sub_MttoTFuerzaCorrienteExit>> ListarCorrienteExit(int idMTF)
        {
            var parametroIdMTF = new SqlParameter("@idMTF", idMTF);

            return await db.Database.SqlQuery<Sub_MttoTFuerzaCorrienteExit>(@"SELECT  Id_MttoTFuerza,
                                                                                      Id_CorrienteExit,
                                                                                      Tap,
                                                                                      [A-0] AS A_0,
                                                                                      [B-0] AS B_0,
                                                                                      [C-0] AS C_0,
                                                                                      PorcientoDesviacion
                                                                              FROM dbo.Sub_MttoTFuerzaCorrienteExit
                                                                              WHERE Id_MttoTFuerza = 86", parametroIdMTF).ToListAsync();
        }

        //public void ActualizarCorrienteExit(int idCE, int idMTF, double a, double b, double c, double porcDesv)
        //{
        //    Sub_MttoTFuerzaCorrienteExit corrienteExit = db.Sub_MttoTFuerzaCorrienteExit.Find(idCE);
        //    if (corrienteExit != null)
        //    {
        //        corrienteExit.Id_MttoTFuerza = idMTF;
        //        corrienteExit.A_0 = a;
        //        corrienteExit.B_0 = b;
        //        corrienteExit.C_0 = c;
        //        corrienteExit.PorcientoDesviacion = porcDesv;

        //        db.Entry(corrienteExit).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        throw new HttpException((int)HttpStatusCode.BadRequest, "Ocurrió un error al insertar el tDBushings.");
        //    }
        //}

        //public List<Sub_MttoBateriaEstac_Instrumentos> EliminarInstrumento(string instrumento, int idMBE, int idBateria, short redEA)
        //{
        //    var parametroInst = new SqlParameter("@nombreInst", instrumento);
        //    var parametroIdMBE = new SqlParameter("@idMBE", idMBE);
        //    var parametroIdBateria = new SqlParameter("@id", idBateria);
        //    var parametroRed = new SqlParameter("@red", redEA);

        //    return db.Database.SqlQuery<Sub_MttoBateriaEstac_Instrumentos>(@"SELECT   SubMBEI.Id_InstrumentoMedicion,

        //                                                                               SubMBEI.id_MttoBatEstacionarias,

        //                                                                               SubMBEI.id_Bateria,

        //                                                                               SubMBEI.EA_RedCD

        //                                                                      FROM dbo.Sub_MttoBateriaEstac_Instrumentos AS SubMBEI 

        //                                                                      INNER JOIN dbo.Sub_NomInstrumentoMedicion AS SubNIM ON SubMBEI.Id_InstrumentoMedicion = SubNIM.Id_InstrumentoMedicion

        //                                                                      WHERE SubNIM.Instrumento = @nombreInst AND  SubMBEI.id_MttoBatEstacionarias = @idMBE AND SubMBEI.id_Bateria = @id AND SubMBEI.EA_RedCD = @red", parametroInst, parametroIdMBE, parametroIdBateria, parametroRed).ToList();
        //}


        //public List<SelectListItem> nivel()
        //{
        //    return new List<SelectListItem> {
        //        new SelectListItem { Value="Bajo", Text="Bajo"},
        //        new SelectListItem { Value= "Normal", Text= "Normal" },
        //        new SelectListItem { Value = "No procede", Text = "No procede" }};
        //}
    }
}