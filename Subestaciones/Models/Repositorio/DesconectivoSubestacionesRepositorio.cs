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
    public class DesconectivoSubestacionesRepositorio
    {

        private DBContext db;

        public DesconectivoSubestacionesRepositorio(DBContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BreakerPorBaja>> ListadoBreakerPorBajaEnumerable(short? redCA)
        {
            return await (from Sub_DS in db.Sub_DesconectivoSubestacion
                          where Sub_DS.RedCA.Equals(redCA) 
                          select new BreakerPorBaja
                          {
                              CodigoDesconectivo = Sub_DS.CodigoDesconectivo,
                              TensionNominal = Sub_DS.TensionNominal,
                              CorrienteNominal = Sub_DS.CorrienteNominal
                          }).ToListAsync();
        }

        public List<Desconectivos> ListaDesconectivos()
        {
            return db.Database.SqlQuery<Desconectivos>(@"SELECT  InstalacionDesconectivos.Codigo AS Codigo ,
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
                AD.TipoAutomático AS Automatica
                FROM InstalacionDesconectivos 
                INNER JOIN EstructurasAdministrativas EA ON ( EA.Id_EAdministrativa = InstalacionDesconectivos.Sucursal )
                LEFT OUTER JOIN TipoInstalacion ON ( InstalacionDesconectivos.TipoInstalacion = Tipoinstalacion.LetraInstalacion )
                INNER JOIN TipoDesconectivos ON ( InstalacionDesconectivos.TipoSeccionalizador = Tipodesconectivos.Id_TipoDesconectivo )
                INNER JOIN FuncionDesconectivos ON ( InstalacionDesconectivos.Funcion = Funciondesconectivos.Id_FuncionDesconectivos )
                LEFT OUTER JOIN Automático_Desconectivo AD ON ( InstalacionDesconectivos.Automatico = AD.Codigo )
                WHERE   InstalacionDesconectivos.TipoInstalacion = 'E'
                OR InstalacionDesconectivos.TipoInstalacion = 'R'").ToList();

        }

        public Desconectivos FindDesc(string cod)
        {
            var lista = ListaDesconectivos();
            return lista.Find(c => c.Codigo == cod);
        }

        public ModelosDesconectivos FindNom(int? nom)
        {
            var lista =  listaModelos();
            return lista.Find(c => c.Id_Nomenclador == nom);
        }

        public List<ModelosDesconectivos> listaModelos()
        {
            return db.Database.SqlQuery<ModelosDesconectivos>(@"SELECT Inst_Nomenclador_Desconectivos.Id_Nomenclador Id_Nomenclador,
        Inst_Nomenclador_Desconectivos.id_EAdministrativa_Prov,
        Inst_Nomenclador_Desconectivos.Id_Administrativa,
        Inst_Nomenclador_Desconectivos.NumAccion,
        Inst_Nomenclador_Desconectivos_Modelo.DescripcionModelo modelo,        
        Inst_Nomenclador_Desconectivos.Tanque Tanque,
        Inst_Nomenclador_Desconectivos.PesoGabinete pesoGab,
        Inst_Nomenclador_Desconectivos.PesoInterruptor pesoInt,
        Inst_Nomenclador_Desconectivos.PesoGas pesoGas,
        Inst_Nomenclador_Desconectivos.PesoTotal pesoTotal,
		Inst_Nomenclador_Desconectivos.Id_ModeloDesconectivo,
		Inst_Nomenclador_Desconectivos.Id_Fabricante,
		Inst_Nomenclador_Desconectivos.Id_MedioExtinsion,
		Inst_Nomenclador_Desconectivos.Id_PresionGas,
		Inst_Nomenclador_Desconectivos.Id_Bil,
		Inst_Nomenclador_Desconectivos.Id_ApertCable,
		Inst_Nomenclador_Desconectivos.Id_Aislamiento,
		Inst_Nomenclador_Desconectivos.Id_Corriente,
		Inst_Nomenclador_Desconectivos.Id_Cortocircuito,
		Inst_Nomenclador_Desconectivos.Id_Tension,
		Inst_Nomenclador_Desconectivos.Id_SecuenciaOperacion,
        Fabricantes.Nombre Fabricante,
        Inst_Nomenclador_Desconectivo_MedidoExtincionArco.DescripcionMedidoExtincionArco ExtArco,
        Inst_Nomenclador_Desconectivo_PresionGas.DescripcionPresionGas presion,
        Inst_Nomenclador_Desconectivos_BIL.DescripcionBIL Bil,
        Inst_Nomenclador_Desconectivos_ApertCable.DescripcionApertCable corrienteA,
        Inst_Nomenclador_Desconectivos_Aislamiento.DescripcionAislamiento Aislamiento,
        Inst_Nomenclador_Desconectivos_CorrienteNominal.DescripcionCorrienteNominal corrienteN,
        Inst_Nomenclador_Desconectivos_Cortocircuito.DescripcionCortoCircuito corrienteCorto,
        Inst_Nomenclador_Desconectivos_TensionNominal.DescripcionTensionCuchillas tension,
        Inst_Nomenclador_Desconectivos_SecuenciaOperacion.DescripcionSecuenciaOperacion SecOpe,
        Inst_Nomenclador_Desconectivos_Modelo.DescripcionModelo ModeloDesconectivo,
        Inst_Nomenclador_Desconectivos.Descripcion tipoDesc
        FROM Inst_Nomenclador_Desconectivos
        LEFT JOIN Fabricantes ON Inst_Nomenclador_Desconectivos.Id_Fabricante = Fabricantes.Id_Fabricante
        LEFT JOIN Inst_Nomenclador_Desconectivo_MedidoExtincionArco ON Inst_Nomenclador_Desconectivos.Id_MedioExtinsion = Inst_Nomenclador_Desconectivo_MedidoExtincionArco.Id_MedidoExtincionArco
        LEFT JOIN Inst_Nomenclador_Desconectivo_PresionGas ON Inst_Nomenclador_Desconectivos.Id_PresionGas = Inst_Nomenclador_Desconectivo_PresionGas.Id_PresionGas
        LEFT JOIN Inst_Nomenclador_Desconectivos_Aislamiento ON Inst_Nomenclador_Desconectivos.Id_Aislamiento = Inst_Nomenclador_Desconectivos_Aislamiento.Id_Aislamiento
        LEFT JOIN Inst_Nomenclador_Desconectivos_ApertCable ON Inst_Nomenclador_Desconectivos.Id_ApertCable = Inst_Nomenclador_Desconectivos_ApertCable.Id_ApertCable
        LEFT JOIN Inst_Nomenclador_Desconectivos_BIL ON Inst_Nomenclador_Desconectivos.Id_Bil = Inst_Nomenclador_Desconectivos_BIL.Id_BIL
        LEFT JOIN Inst_Nomenclador_Desconectivos_CorrienteNominal ON Inst_Nomenclador_Desconectivos.Id_Corriente = Inst_Nomenclador_Desconectivos_CorrienteNominal.Id_CorrienteNominal
        LEFT JOIN Inst_Nomenclador_Desconectivos_Cortocircuito ON Inst_Nomenclador_Desconectivos.Id_Cortocircuito = Inst_Nomenclador_Desconectivos_Cortocircuito.Id_CortoCircuito
        LEFT JOIN Inst_Nomenclador_Desconectivos_Modelo ON Inst_Nomenclador_Desconectivos.Id_ModeloDesconectivo = Inst_Nomenclador_Desconectivos_Modelo.Id_Modelo
        LEFT JOIN Inst_Nomenclador_Desconectivos_SecuenciaOperacion ON Inst_Nomenclador_Desconectivos.Id_SecuenciaOperacion = Inst_Nomenclador_Desconectivos_SecuenciaOperacion.Id_SecuenciaOperacion
        LEFT JOIN Inst_Nomenclador_Desconectivos_TensionNominal ON Inst_Nomenclador_Desconectivos.Id_Tension = Inst_Nomenclador_Desconectivos_TensionNominal.Id_Tension").ToList();
        }

        public IEnumerable<ModelosDesconectivos> ListaModelos(int desc)
        {
            var lista = listaModelos();
            return lista.Where(c => c.tipoDesc == desc).ToList();
        }

        public SelectList SiNo()
        {
            return new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value= "1", Text= "Si" },
                new SelectListItem { Value="0", Text="No"}
               
            }, "Value", "Text");
        }

        public IEnumerable<Inst_Nomenclador_Desconectivos_Modelo> ObtenerModelosDesconectivos()
        {
            return db.Database.SqlQuery<Inst_Nomenclador_Desconectivos_Modelo>(@"select * from Inst_Nomenclador_Desconectivos_Modelo").ToList();
        }

        public IEnumerable<Fabricantes> ObtenerFabricanteModelosDesconectivos()
        {
            return db.Database.SqlQuery<Fabricantes>(@"select * from Fabricantes").ToList();
        }

        public IEnumerable<Inst_Nomenclador_Desconectivos_TensionNominal> ObtenerTensionModelosDesconectivos()
        {
            return db.Database.SqlQuery<Inst_Nomenclador_Desconectivos_TensionNominal> (@"select * from Inst_Nomenclador_Desconectivos_TensionNominal").ToList();
        }

        public IEnumerable<Inst_Nomenclador_Desconectivos_CorrienteNominal> ObtenerCorrienteNModelosDesconectivos()
        {
            return db.Database.SqlQuery<Inst_Nomenclador_Desconectivos_CorrienteNominal>(@"select * from Inst_Nomenclador_Desconectivos_CorrienteNominal").ToList();
        }

        public IEnumerable<Inst_Nomenclador_Desconectivos_BIL> ObtenerBilModelosDesconectivos()
        {
            return db.Database.SqlQuery<Inst_Nomenclador_Desconectivos_BIL>(@"select * from Inst_Nomenclador_Desconectivos_BIL").ToList();
        }

        public IEnumerable<Inst_Nomenclador_Desconectivos_Cortocircuito> ObtenerICortoCircuitoModelosDesconectivos()
        {
            return db.Database.SqlQuery<Inst_Nomenclador_Desconectivos_Cortocircuito>(@"select * from Inst_Nomenclador_Desconectivos_Cortocircuito").ToList();
        }

        public IEnumerable<Inst_Nomenclador_Desconectivos_ApertCable> ObtenerIAperturaCableModelosDesconectivos()
        {
            return db.Database.SqlQuery<Inst_Nomenclador_Desconectivos_ApertCable>(@"select * from Inst_Nomenclador_Desconectivos_ApertCable").ToList();
        }
        public IEnumerable<Inst_Nomenclador_Desconectivos_SecuenciaOperacion> ObtenerSecuenciaOperacionesModelosDesconectivos()
        {
            return db.Database.SqlQuery<Inst_Nomenclador_Desconectivos_SecuenciaOperacion>(@"select * from Inst_Nomenclador_Desconectivos_SecuenciaOperacion").ToList();
        }

        public IEnumerable<Inst_Nomenclador_Desconectivo_MedidoExtincionArco> ObtenerExtincionArcoModelosDesconectivos()
        {
            return db.Database.SqlQuery<Inst_Nomenclador_Desconectivo_MedidoExtincionArco>(@"select * from  Inst_Nomenclador_Desconectivo_MedidoExtincionArco").ToList();
        }

        public IEnumerable<Inst_Nomenclador_Desconectivo_PresionGas> ObtenerPresionGasModelosDesconectivos()
        {
            return db.Database.SqlQuery<Inst_Nomenclador_Desconectivo_PresionGas>(@"select * from Inst_Nomenclador_Desconectivo_PresionGas").ToList();
        }

        public IEnumerable<Inst_Nomenclador_Desconectivos_Aislamiento> ObtenerAislamientoModelosDesconectivos()
        {
            return db.Database.SqlQuery<Inst_Nomenclador_Desconectivos_Aislamiento>(@"select * from  Inst_Nomenclador_Desconectivos_Aislamiento").ToList();
        }

        public void InsertarModelo(int EA, int EAProv, int numA, int modelo, int descripcion, int? fab, int? tension, int? corriente, double? pGas, double? pInt, double? pGab, double? pTotal, int? bil, int? cortoCircuito, int? tanque, int? apertCable, int? secc, int? extArco, int? aislamiento, int? presionGas)
        {

            var id_nom = db.Database.SqlQuery<int>(@"declare @numNomModelo int
                Select @numNomModelo = Max(Id_Nomenclador) + 1
                From Inst_Nomenclador_Desconectivos
                if @numNomModelo is null
                set @numNomModelo = 1
                Select @numNomModelo as Id_Nomenclador");
            try
            {
                Inst_Nomenclador_Desconectivos modeloInsertar = new Inst_Nomenclador_Desconectivos();
                modeloInsertar.Id_Nomenclador = id_nom.ToList().First();
                modeloInsertar.Id_Administrativa = EA;
                modeloInsertar.id_EAdministrativa_Prov = EAProv;
                modeloInsertar.NumAccion = numA;
                modeloInsertar.Id_ModeloDesconectivo = modelo;
                modeloInsertar.Id_Fabricante = fab;
                modeloInsertar.Id_Corriente = corriente;
                modeloInsertar.Id_Tension = tension;
                modeloInsertar.Id_PresionGas = presionGas;
                modeloInsertar.PesoGas = pGas;
                modeloInsertar.PesoInterruptor = pInt;
                modeloInsertar.PesoGabinete = pGab;
                modeloInsertar.PesoTotal = pTotal;
                modeloInsertar.Id_Bil = bil;
                modeloInsertar.Id_Cortocircuito = cortoCircuito;
                modeloInsertar.Tanque = tanque;
                modeloInsertar.Id_ApertCable = apertCable;
                modeloInsertar.Id_SecuenciaOperacion = secc;
                modeloInsertar.Id_MedioExtinsion = extArco;
                modeloInsertar.Id_Aislamiento = aislamiento;
                modeloInsertar.Descripcion = descripcion;

                db.Entry(modeloInsertar).State = EntityState.Added;
                db.SaveChanges();
            }
            catch (Exception)
            {
                //throw (e);
                throw new HttpException((int)HttpStatusCode.BadRequest, "Ocurrió un error al insertar el modelo.");
            }

        }

        public void ActualizarNomModelo(int idNom, int EA, int EAProv, int numA, int modelo, int descripcion, int? fab, int? tension, int? corriente, double? pGas, double? pInt, double? pGab, double? pTotal, int? bil, int? cortoCircuito, int? tanque, int? apertCable, int? secc, int? extArco, int? aislamiento, int? presionGas)
        {
            Inst_Nomenclador_Desconectivos nomModeloEditar = db.Inst_Nomenclador_Desconectivos.Find(idNom);
            if (nomModeloEditar != null)
            {
                nomModeloEditar.Id_Nomenclador = idNom;
                nomModeloEditar.Id_Administrativa = EA;
                nomModeloEditar.id_EAdministrativa_Prov = EAProv;
                nomModeloEditar.NumAccion = numA;
                nomModeloEditar.Id_ModeloDesconectivo = modelo;
                nomModeloEditar.Id_Fabricante = fab;
                nomModeloEditar.Id_Corriente = corriente;
                nomModeloEditar.Id_Tension = tension;
                nomModeloEditar.Id_PresionGas = presionGas;
                nomModeloEditar.PesoGas = pGas;
                nomModeloEditar.PesoInterruptor = pInt;
                nomModeloEditar.PesoGabinete = pGab;
                nomModeloEditar.PesoTotal = pTotal;
                nomModeloEditar.Id_Bil = bil;
                nomModeloEditar.Id_Cortocircuito = cortoCircuito;
                nomModeloEditar.Tanque = tanque;
                nomModeloEditar.Id_ApertCable = apertCable;
                nomModeloEditar.Id_SecuenciaOperacion = secc;
                nomModeloEditar.Id_MedioExtinsion = extArco;
                nomModeloEditar.Id_Aislamiento = aislamiento;
                nomModeloEditar.Descripcion = descripcion;


                db.Entry(nomModeloEditar).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException();
            }

        }

        public void EliminarModelo(int modelo)
        {
            Inst_Nomenclador_Desconectivos modeloEliminar = db.Inst_Nomenclador_Desconectivos.Find(modelo);
            if (modeloEliminar != null)
            {
                try
                {
                    db.Inst_Nomenclador_Desconectivos.Remove(modeloEliminar);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
            else
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Lo sentimos no se puede eliminar, el modelo no existe.");
            }
        }

        public async Task InsertaBreakerBaja(string sub, short? red, string codD, double? tensionN, double? corrienteN)
        {
            if (!await ValidarSiExisteBreakerBaja(sub, red, codD))
            {
                try
                {
                    Sub_DesconectivoSubestacion breakerBaja = new Sub_DesconectivoSubestacion();
                    breakerBaja.RedCA = (short)red;
                    breakerBaja.CodigoSub = sub;
                    breakerBaja.CodigoDesconectivo = codD;
                    breakerBaja.TensionNominal = tensionN;
                    breakerBaja.CorrienteNominal = corrienteN;

                    db.Entry(breakerBaja).State = EntityState.Added;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new HttpException((int)HttpStatusCode.BadRequest, "Ocurrió un error al insertar el Breaker.");
                }
            }
            else
            {
                throw new HttpException(httpCode: (int)HttpStatusCode.Conflict, message: "Lo sentimos no se puede insertar el Breaker, ya existe en la subestación.");

            }
        }

        public async Task<bool> ValidarSiExisteBreakerBaja(string sub, short? red, string codD)
        {
            return await db.Sub_DesconectivoSubestacion.AnyAsync(c => c.CodigoSub == sub && c.RedCA == red && c.CodigoDesconectivo == codD);
        }
    }
}