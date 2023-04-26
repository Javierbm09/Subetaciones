using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Subestaciones.Models
{
    public class DBContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public DBContext() : base("name=SubestacionesConnection")
        {
        }

        public virtual DbSet<Capacidades> Capacidades { get; set; }

        public DbSet<Personal> Personal { get; set; }

        public DbSet<Logueados> Logueados { get; set; }

        public virtual DbSet<Sub_NomCargadores> Sub_NomCargadores { get; set; }

        public DbSet<NomencladorBaterias> NomencladorBaterias { get; set; }

        public virtual DbSet<NomencladorTension> NomencladorTension { get; set; }

        public virtual DbSet<Sub_NomAspectoInspSubTrans> Sub_NomAspectoInspSubTrans { get; set; }

        public DbSet<GruposG> GruposGs { get; set; }

        public DbSet<Subestaciones> Subestaciones { get; set; }

        public DbSet<SubestacionesTransmision> SubestacionesTransmision { get; set; }

        public virtual DbSet<EstructurasAdministrativas> EstructurasAdministrativas { get; set; }

        public virtual DbSet<Fabricantes> Fabricantes { get; set; }

        public virtual DbSet<TipoGrupos_Sigere> TipoGrupos_Sigere { get; set; }

        public virtual DbSet<Sub_Barra> Sub_Barra { get; set; }

        public virtual DbSet<Sub_Celaje> Sub_Celaje { get; set; }
        public virtual DbSet<Sub_InspAspectosSubTrans> Sub_InspAspectosSubTrans { get; set; }

        public virtual DbSet<VoltajesSistemas> VoltajesSistemas { get; set; }

        public virtual DbSet<Material> Material { get; set; }

        public virtual DbSet<recubrimiento> recubrimiento { get; set; }

        public virtual DbSet<Seccion> Seccion { get; set; }

        public virtual DbSet<TipoConductor> TipoConductor { get; set; }

        public virtual DbSet<conductor> conductor { get; set; }

        public virtual DbSet<BancoCapacitores> BancoCapacitores { get; set; }

        public virtual DbSet<InstalacionDesconectivos> InstalacionDesconectivos { get; set; }

        public virtual DbSet<Nomenclador_EstadoOperativo> Nomenclador_EstadoOperativo { get; set; }

        public virtual DbSet<CorrientesNominalesPararrayos> CorrientesNominalesPararrayos { get; set; }

        public virtual DbSet<Sub_Pararrayos> Sub_Pararrayos { get; set; }

        public virtual DbSet<VoltajesNominalesPararrayos> VoltajesNominalesPararrayos { get; set; }

        public virtual DbSet<BancoTransformadores> BancoTransformadores { get; set; }

        public virtual DbSet<Sub_Alimentacion_TP_TC> Sub_Alimentacion_TP_TC { get; set; }

        public virtual DbSet<Sub_LineasSubestacion> Sub_LineasSubestacion { get; set; }

        public virtual DbSet<TransformadoresSubtransmision> TransformadoresSubtransmision { get; set; }

        public virtual DbSet<TransformadoresTransmision> TransformadoresTransmision { get; set; }

        public virtual DbSet<Sub_DesconectivoSubestacion> Sub_DesconectivoSubestacion { get; set; }

        public virtual DbSet<Sub_RedCorrienteAlterna> Sub_RedCorrienteAlterna { get; set; }

        public virtual DbSet<Transformadores> Transformadores { get; set; }

        public virtual DbSet<Sub_RedCorrienteDirecta> Sub_RedCorrienteDirecta { get; set; }

        public virtual DbSet<Sub_Baterias> Sub_Baterias { get; set; }

        public virtual DbSet<Sub_Cargador> Sub_Cargador { get; set; }

        public virtual DbSet<Sub_NomInstrumentoMedicion> Sub_NomInstrumentoMedicion { get; set; }

        public virtual DbSet<Sub_NomTipoMedicion> Sub_NomTipoMedicion { get; set; }

        public virtual DbSet<Sub_grupoconexion> Sub_grupoconexion { get; set; }

        public virtual DbSet<Sub_NomEnfriamiento> Sub_NomEnfriamiento { get; set; }

        public virtual DbSet<Sub_TipoTransf_Grupoconexion> Sub_TipoTransf_Grupoconexion { get; set; }

        public virtual DbSet<Bloque> Bloque { get; set; }

        public virtual DbSet<EsquemasBaja> EsquemasBaja { get; set; }

        public virtual DbSet<EsquemasAlta> EsquemasAlta { get; set; }

        public virtual DbSet<SalidaExclusiva> SalidaExclusiva { get; set; }

        public virtual DbSet<Sectores> Sectores { get; set; }

        public virtual DbSet<SalidaExclusivaSub> SalidaExclusivaSub { get; set; }

        public virtual DbSet<Configuracion> Configuracion { get; set; }

        public virtual DbSet<SubTermometrosTransfSubTransmision> SubTermometrosTransfSubTransmision { get; set; }

        public virtual DbSet<SubTermometrosTransfTransmision> SubTermometrosTransfTransmision { get; set; }

        public virtual DbSet<ES_TransformadorCorriente> ES_TransformadorCorriente { get; set; }

        public virtual DbSet<ES_TransformadorPotencial> ES_TransformadorPotencial { get; set; }

        public virtual DbSet<ES_Conexion_IM_TC_TP> ES_Conexion_IM_TC_TP { get; set; }

        public virtual DbSet<ES_Conexion_Rele_TC_TP> ES_Conexion_Rele_TC_TP { get; set; }

        public virtual DbSet<ES_Esquema_TC> ES_Esquema_TC { get; set; }

        public virtual DbSet<ES_EsquemaM_TC> ES_EsquemaM_TC { get; set; }

        public virtual DbSet<ES_Esquema_TP> ES_Esquema_TP { get; set; }

        public virtual DbSet<ES_EsquemaM_TP> ES_EsquemaM_TP { get; set; }

        public virtual DbSet<ES_TC_Devanado> ES_TC_Devanado { get; set; }

        public virtual DbSet<ES_TP_Devanado> ES_TP_Devanado { get; set; }

        public virtual DbSet<CircuitosPrimarios> CircuitosPrimarios { get; set; }

        public virtual DbSet<CircuitosSubtransmision> CircuitosSubtransmision { get; set; }

        public virtual DbSet<SubestacionesCabezasLineas> SubestacionesCabezasLineas { get; set; }

        public virtual DbSet<InstalacionesInterrumpibles> InstalacionesInterrumpibles { get; set; }

        public virtual DbSet<Emplazamiento_Sigere> Emplazamiento_Sigere { get; set; }

        public virtual DbSet<Fotos> Fotos { get; set; }

        public virtual DbSet<Defectos> Defectos { get; set; }

        public virtual DbSet<Elementos> Elementos { get; set; }

        public virtual DbSet<materialpostes> materialpostes { get; set; }

        public virtual DbSet<TipoDefecto> TipoDefecto { get; set; }

        public virtual DbSet<Sub_Mantenimientos> Sub_Mantenimientos { get; set; }

        public virtual DbSet<Adm_PersonalExtendido> Adm_PersonalExtendido { get; set; }

        public virtual DbSet<Adm_PersonalPWD> Adm_PersonalPWD { get; set; }

        public virtual DbSet<Sub_Termografias> Sub_Termografias { get; set; }

        public virtual DbSet<Sub_PuntoTermografia> Sub_PuntoTermografia { get; set; }

        public virtual DbSet<Inst_Nomenclador_Puente> Inst_Nomenclador_Puente { get; set; }

        public virtual DbSet<Inst_Nomenclador_Puente_Modelo> Inst_Nomenclador_Puente_Modelo { get; set; }

        public virtual DbSet<Inst_Nomenclador_Puente_Tipo> Inst_Nomenclador_Puente_Tipo { get; set; }

        public virtual DbSet<Inst_CapacidadFusible> Inst_CapacidadFusible { get; set; }

        public virtual DbSet<Inst_TensionFusible> Inst_TensionFusible { get; set; }

        public virtual DbSet<Inst_TipoFusible> Inst_TipoFusible { get; set; }

        public virtual DbSet<Inst_TipoPortaFusible> Inst_TipoPortaFusible { get; set; }

        public virtual DbSet<PortaFusibles> PortaFusibles { get; set; }

        public virtual DbSet<Breakers> Breakers { get; set; }

        public virtual DbSet<Inst_Nomenclador_Cuchillas> Inst_Nomenclador_Cuchillas { get; set; }

        public virtual DbSet<Inst_Nomenclador_Cuchillas_Mando> Inst_Nomenclador_Cuchillas_Mando { get; set; }

        public virtual DbSet<Inst_Nomenclador_Cuchillas_Operacion> Inst_Nomenclador_Cuchillas_Operacion { get; set; }

        public virtual DbSet<Inst_Nomenclador_Cuchillas_Tension> Inst_Nomenclador_Cuchillas_Tension { get; set; }

        public virtual DbSet<Inst_Nomenclador_InterruptorAire> Inst_Nomenclador_InterruptorAire { get; set; }

        public virtual DbSet<Inst_Nomenclador_InterruptorAire_Mando> Inst_Nomenclador_InterruptorAire_Mando { get; set; }

        public virtual DbSet<Inst_Nomenclador_InterruptorAire_Operacion> Inst_Nomenclador_InterruptorAire_Operacion { get; set; }

        public virtual DbSet<Inst_Nomenclador_InterruptorAire_Tension> Inst_Nomenclador_InterruptorAire_Tension { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos> Inst_Nomenclador_Desconectivos { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion> Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivo_MedidoExtincionArco> Inst_Nomenclador_Desconectivo_MedidoExtincionArco { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivo_PresionGas> Inst_Nomenclador_Desconectivo_PresionGas { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_Aislamiento> Inst_Nomenclador_Desconectivos_Aislamiento { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_ApertCable> Inst_Nomenclador_Desconectivos_ApertCable { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_BIL> Inst_Nomenclador_Desconectivos_BIL { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_CorrienteNominal> Inst_Nomenclador_Desconectivos_CorrienteNominal { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_Cortocircuito> Inst_Nomenclador_Desconectivos_Cortocircuito { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_EquipoUtilizado> Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_EquipoUtilizado { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Esquema> Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Esquema { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Funcion> Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Funcion { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_SCADA> Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_SCADA { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TensionInst> Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TensionInst { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TipoGabinete> Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TipoGabinete { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_Modelo> Inst_Nomenclador_Desconectivos_Modelo { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_SecuenciaOperacion> Inst_Nomenclador_Desconectivos_SecuenciaOperacion { get; set; }

        public virtual DbSet<Inst_Nomenclador_Desconectivos_TensionNominal> Inst_Nomenclador_Desconectivos_TensionNominal { get; set; }

        public virtual DbSet<SubMttoSubDistribucion> SubMttoSubDistribucion { get; set; }

        public virtual DbSet<Sub_MttoDistBarra> Sub_MttoDistBarra { get; set; }

        public virtual DbSet<Sub_MttoDistPararrayos> Sub_MttoDistPararrayos { get; set; }

        public virtual DbSet<Sub_MttoDistRelacTransformacion> Sub_MttoDistRelacTransformacion { get; set; }

        public virtual DbSet<Sub_MttoDistResistOhmica> Sub_MttoDistResistOhmica { get; set; }

        public virtual DbSet<Sub_MttoDistribDesconectivos> Sub_MttoDistribDesconectivos { get; set; }

        public virtual DbSet<Sub_MttoDistTransf> Sub_MttoDistTransf { get; set; }
        public virtual DbSet<Sub_MttoTransUsoP> Sub_MttoTransUsoP { get; set; }
        public virtual DbSet<Sub_MttoUPRelacTransformacion> Sub_MttoUPRelacTransformacion { get; set; }
        public virtual DbSet<Sub_MttoUPResistOhmica> Sub_MttoUPResistOhmica { get; set; }

        public virtual DbSet<TipoMantenimiento> TipoMantenimiento { get; set; }

        public virtual DbSet<Sub_CelajeSubDistribucion> Sub_CelajeSubDistribucion { get; set; }

        public virtual DbSet<Sub_CelajeSubDistInterruptor> Sub_CelajeSubDistInterruptor { get; set; }

        public virtual DbSet<Sub_TransformadoresSubtrCelaje> Sub_TransformadoresSubtrCelaje { get; set; }

        public virtual DbSet<Sub_Certificacion> Sub_Certificacion { get; set; }

        public virtual DbSet<Sub_CertificacionAspectos> Sub_CertificacionAspectos { get; set; }

        public virtual DbSet<Sub_CertificacionComision> Sub_CertificacionComision { get; set; }

        public virtual DbSet<Sub_CertificacionDetalles> Sub_CertificacionDetalles { get; set; }

        public virtual DbSet<Sub_CertificacionSubAspectos> Sub_CertificacionSubAspectos { get; set; }

        public virtual DbSet<Sub_PruebaAceiteAQReducido> Sub_PruebaAceiteAQReducido { get; set; }

        public virtual DbSet<Sub_PruebaAceiteAGDisueltos> Sub_PruebaAceiteAGDisueltos { get; set; }

        public virtual DbSet<Sub_MedicionTierra> Sub_MedicionTierra { get; set; }

        public virtual DbSet<Sub_MedicionTierra_CaidaPotencial> Sub_MedicionTierra_CaidaPotencial { get; set; }

        public virtual DbSet<Sub_MedicionTierra_ContinuidadMalla> Sub_MedicionTierra_ContinuidadMalla { get; set; }

        public virtual DbSet<LugarHabitado> LugarHabitado { get; set; }

        public virtual DbSet<Sub_MttoPararrayos> Sub_MttoPararrayos { get; set; }

        public virtual DbSet<Sub_MttoPararrayos_Fases> Sub_MttoPararrayos_Fases { get; set; }

        public virtual DbSet<Sub_MttoPararrayos_Instrumentos> Sub_MttoPararrayos_Instrumentos { get; set; }

        public virtual DbSet<Sub_MttoBateriaEstac_Instrumentos> Sub_MttoBateriaEstac_Instrumentos { get; set; }

        public virtual DbSet<Sub_MttoBateriasEstacionarias> Sub_MttoBateriasEstacionarias { get; set; }

        public virtual DbSet<Sub_MttoBateriasEstacionarias_Vasos> Sub_MttoBateriasEstacionarias_Vasos { get; set; }

        public virtual DbSet<Sub_MttoTFuerza> Sub_MttoTFuerza { get; set; }

        public virtual DbSet<Sub_MttoTFuerzaAceiteConmutador> Sub_MttoTFuerzaAceiteConmutador { get; set; }

        public virtual DbSet<Sub_MttoTFuerzaAceiteTanquePrinc> Sub_MttoTFuerzaAceiteTanquePrinc { get; set; }

        public virtual DbSet<Sub_MttoTFuerzaAislamBushings> Sub_MttoTFuerzaAislamBushings { get; set; }

        public virtual DbSet<Sub_MttoTFuerzaAislamEnrollado> Sub_MttoTFuerzaAislamEnrollado { get; set; }

        public virtual DbSet<Sub_MttoTFuerzaAuxiliares> Sub_MttoTFuerzaAuxiliares { get; set; }

        public virtual DbSet<Sub_MttoTFuerzaConmutador> Sub_MttoTFuerzaConmutador { get; set; }

        public virtual DbSet<Sub_MttoTFuerzaCorrienteExit> Sub_MttoTFuerzaCorrienteExit { get; set; }

        public virtual DbSet<Sub_MttoTFuerzaInspExterna> Sub_MttoTFuerzaInspExterna { get; set; }

        public virtual DbSet<Sub_MttoTFuerzaPresionBushing> Sub_MttoTFuerzaPresionBushing { get; set; }

        public virtual DbSet<Sub_MttoTFuerzaRelacTransf> Sub_MttoTFuerzaRelacTransf { get; set; }

        public virtual DbSet<Sub_MttoTFuerzaResistOhm> Sub_MttoTFuerzaResistOhm { get; set; }

        public virtual DbSet<Sub_MttoTFuerzaTanDeltaBushings> Sub_MttoTFuerzaTanDeltaBushings { get; set; }

        public virtual DbSet<Sub_MttoTFuerzaTanDeltaEnrollado> Sub_MttoTFuerzaTanDeltaEnrollado { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sub_RedCorrienteDirecta>()
                .Property(e => e.NombreServicioCD)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_RedCorrienteDirecta>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_RedCorrienteDirecta>()
                .Property(e => e.UsoRed)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_RedCorrienteDirecta>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Fabricantes>()
               .Property(e => e.Nombre)
               .IsUnicode(false);

            modelBuilder.Entity<Fabricantes>()
                .Property(e => e.Pais)
                .IsUnicode(false);

            modelBuilder.Entity<Fabricantes>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Fabricantes>()
                .Property(e => e.EMail)
                .IsUnicode(false);

            modelBuilder.Entity<Fabricantes>()
                .Property(e => e.Web)
                .IsUnicode(false);

            modelBuilder.Entity<NomencladorBaterias>()
                .Property(e => e.TipoBateria)
                .IsUnicode(false);

            modelBuilder.Entity<NomencladorBaterias>()
                .Property(e => e.ClaseBateria)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_Cargador>()
                .Property(e => e.EstOp)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_Cargador>()
                .Property(e => e.Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_Cargador>()
                .Property(e => e.NroSerie)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_DesconectivoSubestacion>()
               .Property(e => e.CodigoSub)
               .IsUnicode(false);

            modelBuilder.Entity<Sub_DesconectivoSubestacion>()
                .Property(e => e.CodigoDesconectivo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_RedCorrienteAlterna>()
                .Property(e => e.NombreServicioCA)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_RedCorrienteAlterna>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_RedCorrienteAlterna>()
                .HasMany(e => e.Sub_DesconectivoSubestacion)
                .WithRequired(e => e.Sub_RedCorrienteAlterna)
                .HasForeignKey(e => e.RedCA);

            modelBuilder.Entity<Transformadores>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Transformadores>()
                .Property(e => e.Numemp)
                .IsUnicode(false);

            modelBuilder.Entity<Transformadores>()
                .Property(e => e.SimboloTaps)
                .IsUnicode(false);

            modelBuilder.Entity<Transformadores>()
                .Property(e => e.Fase)
                .IsUnicode(false);

            modelBuilder.Entity<Transformadores>()
                .Property(e => e.NoSerie)
                .IsUnicode(false);

            modelBuilder.Entity<Transformadores>()
                .Property(e => e.CE)
                .IsUnicode(false);

            modelBuilder.Entity<Transformadores>()
                .Property(e => e.PosicionBanco)
                .IsUnicode(false);

            modelBuilder.Entity<Transformadores>()
                .Property(e => e.EstadoOperativo)
                .IsUnicode(false);

            modelBuilder.Entity<Transformadores>()
                .Property(e => e.Tipoalimentacion)
                .IsUnicode(false);

            modelBuilder.Entity<Transformadores>()
                .Property(e => e.Marca)
                .IsUnicode(false);

            modelBuilder.Entity<Transformadores>()
                .Property(e => e.TipoEnfriamiento)
                .IsUnicode(false);

            modelBuilder.Entity<Transformadores>()
                .Property(e => e.PolaridadGrupo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_NomCargadores>()
                .Property(e => e.TipoCargador)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_NomInstrumentoMedicion>()
               .Property(e => e.Instrumento)
               .IsUnicode(false);

            modelBuilder.Entity<Sub_NomInstrumentoMedicion>()
                .Property(e => e.ModeloTipo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_NomInstrumentoMedicion>()
                .Property(e => e.Serie)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_NomAspectoInspSubTrans>()
                .Property(e => e.Aspecto)
                .IsUnicode(false);            

            modelBuilder.Entity<Sub_NomInstrumentoMedicion>()
                .Property(e => e.Fabricante)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_NomInstrumentoMedicion>()
                .Property(e => e.Pais)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_NomInstrumentoMedicion>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_NomInstrumentoMedicion>()
                .Property(e => e.BrigadaResponsable)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_NomInstrumentoMedicion>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_NomTipoMedicion>()
                .Property(e => e.NombreTipoMedicion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_grupoconexion>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_NomEnfriamiento>()
                .Property(e => e.TipoEnfriamiento)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.Numemp)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.SimboloTaps)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.Fase)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.NoSerie)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.CE)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.PosicionBanco)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.EstadoOperativo)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.EstadoHermeticidad)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.EstadoPinturaTanque)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.EstadoPinturaRotulos)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.AcidezAceite)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.NivelAceite)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.ColoracionAceite)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.TabRegulable)
                .IsFixedLength();

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.TabPrimarioSecundario)
                .IsFixedLength();

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.GrupoConexion)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.TipoRegVoltaje)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.TipoCajaMando)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresSubtransmision>()
                .Property(e => e.NumeroInventario)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.Numemp)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.SimboloTaps)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.Fase)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.NoSerie)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.CE)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.PosicionBanco)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.EstadoOperativo)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.EstadoHermeticidad)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.EstadoPinturaTanque)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.EstadoPinturaRotulos)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.AcidezAceite)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.NivelAceite)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.ColoracionAceite)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.TabRegulable)
                .IsFixedLength();

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.TabPrimarioSecundario)
                .IsFixedLength();

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.GrupoConexion)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.TipoRegVoltaje)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.TipoCajaMando)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.NumeroInventario)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.BushingPrimFaseATipo)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.BushingPrimFaseBTipo)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.BushingPrimFaseCTipo)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.BushingPrimFaseNeutroTipo)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.BushingSecFasesTipo)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.BushingSecNeutroTipo)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.BushingTercFasesTipo)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.BushingPrimFaseASerie)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.BushingPrimFaseBSerie)
                .IsUnicode(false);

            modelBuilder.Entity<TransformadoresTransmision>()
                .Property(e => e.BushingPrimFaseCSerie)
                .IsUnicode(false);
            modelBuilder.Entity<Bloque>()
               .Property(e => e.Codigo)
               .IsUnicode(false);

            modelBuilder.Entity<Bloque>()
                .Property(e => e.tipobloque)
                .IsUnicode(false);

            modelBuilder.Entity<Bloque>()
                .Property(e => e.TipoSalida)
                .IsUnicode(false);

            modelBuilder.Entity<EsquemasBaja>()
                .Property(e => e.EsquemaPorBaja)
                .IsUnicode(false);

            modelBuilder.Entity<EsquemasBaja>()
                .Property(e => e.CodigoMostrado)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusiva>()
                .Property(e => e.Cliente)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusiva>()
                .Property(e => e.Sector)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusiva>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusiva>()
                .Property(e => e.DesconectivoCliente)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusiva>()
                .Property(e => e.EstadoDesconectivo)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusiva>()
                .Property(e => e.EstadoCliente)
                .IsUnicode(false);

            modelBuilder.Entity<Sectores>()
                .Property(e => e.Id_Sector)
                .IsUnicode(false);

            modelBuilder.Entity<Sectores>()
                .Property(e => e.Sector)
                .IsUnicode(false);

            modelBuilder.Entity<Sectores>()
                .Property(e => e.Organismo)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusivaSub>()
               .Property(e => e.Codigo)
               .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusivaSub>()
                .Property(e => e.DesconectivoCliente)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusivaSub>()
                .Property(e => e.EstadoDesconectivo)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusivaSub>()
                .Property(e => e.EstadoCliente)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusivaSub>()
                .Property(e => e.CalibreParrillaA)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusivaSub>()
                .Property(e => e.CalibreParrillaB)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusivaSub>()
                .Property(e => e.CalibreParrillaC)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusivaSub>()
                .Property(e => e.CalibreParrillaNeutro)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusivaSub>()
                .Property(e => e.Cliente)
                .IsUnicode(false);

            modelBuilder.Entity<SalidaExclusivaSub>()
                .Property(e => e.Sector)
                .IsUnicode(false);

            modelBuilder.Entity<Configuracion>()
               .Property(e => e.Nombre)
               .IsUnicode(false);

            modelBuilder.Entity<Configuracion>()
                .Property(e => e.Dato)
                .IsUnicode(false);

            modelBuilder.Entity<SubTermometrosTransfSubTransmision>()
               .Property(e => e.Numero)
               .IsUnicode(false);

            modelBuilder.Entity<SubTermometrosTransfSubTransmision>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<SubTermometrosTransfTransmision>()
                .Property(e => e.Numero)
                .IsUnicode(false);

            modelBuilder.Entity<SubTermometrosTransfTransmision>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorCorriente>()
                .Property(e => e.Nro_Serie)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorCorriente>()
                .Property(e => e.Fase)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorCorriente>()
                .Property(e => e.Relacion_Transformacion)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorCorriente>()
                .Property(e => e.CodSub)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorCorriente>()
                .Property(e => e.Tipo_Equipo_Primario)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorCorriente>()
                .Property(e => e.Elemento_Electrico)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorCorriente>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorCorriente>()
                .Property(e => e.Inventario)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorCorriente>()
                .Property(e => e.InPrimaria)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorCorriente>()
                .Property(e => e.Fabricante)
                .IsUnicode(false);

            modelBuilder.Entity<ES_Conexion_IM_TC_TP>()
                .Property(e => e.IM)
                .IsUnicode(false);

            modelBuilder.Entity<ES_Conexion_IM_TC_TP>()
                .Property(e => e.TC_TP)
                .IsUnicode(false);

            modelBuilder.Entity<ES_Conexion_IM_TC_TP>()
                .Property(e => e.Tipo_Equipo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ES_Conexion_Rele_TC_TP>()
                .Property(e => e.rele)
                .IsUnicode(false);

            modelBuilder.Entity<ES_Conexion_Rele_TC_TP>()
                .Property(e => e.TC_TP)
                .IsUnicode(false);

            modelBuilder.Entity<ES_Conexion_Rele_TC_TP>()
                .Property(e => e.Tipo_Equipo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ES_Esquema_TC>()
                .Property(e => e.TC)
                .IsUnicode(false);

            modelBuilder.Entity<ES_EsquemaM_TC>()
                .Property(e => e.TC)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorPotencial>()
                .Property(e => e.Nro_Serie)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorPotencial>()
                .Property(e => e.Fase)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorPotencial>()
                .Property(e => e.CodSub)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorPotencial>()
                .Property(e => e.Tipo_Equipo_Primario)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorPotencial>()
                .Property(e => e.Elemento_Electrico)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorPotencial>()
                .Property(e => e.Fabricante)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorPotencial>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorPotencial>()
                .Property(e => e.Inventario)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorPotencial>()
                .Property(e => e.InPrimaria)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TransformadorPotencial>()
                .Property(e => e.VoltajeNominal)
                .IsUnicode(false);

            modelBuilder.Entity<ES_Esquema_TP>()
              .Property(e => e.TP)
              .IsUnicode(false);

            modelBuilder.Entity<ES_EsquemaM_TP>()
                .Property(e => e.TP)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TC_Devanado>()
              .Property(e => e.Nro_TC)
              .IsUnicode(false);

            modelBuilder.Entity<ES_TC_Devanado>()
                .Property(e => e.Clase_Precision)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TC_Devanado>()
                .Property(e => e.Designacion)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TP_Devanado>()
                .Property(e => e.Nro_TP)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TP_Devanado>()
                .Property(e => e.Designacion)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TP_Devanado>()
                .Property(e => e.ClasePresicion)
                .IsUnicode(false);

            modelBuilder.Entity<ES_TP_Devanado>()
                .Property(e => e.Tension)
                .IsUnicode(false);

            modelBuilder.Entity<EsquemasAlta>()
               .Property(e => e.EsquemaPorAlta)
               .IsUnicode(false);

            modelBuilder.Entity<EsquemasAlta>()
                .Property(e => e.CodigoMostrado)
                .IsUnicode(false);

            modelBuilder.Entity<CircuitosPrimarios>()
              .Property(e => e.CodigoCircuito)
              .IsUnicode(false);

            modelBuilder.Entity<CircuitosPrimarios>()
                .Property(e => e.NombreAntiguo)
                .IsUnicode(false);

            modelBuilder.Entity<CircuitosPrimarios>()
                .Property(e => e.SubAlimentadora)
                .IsUnicode(false);

            modelBuilder.Entity<CircuitosPrimarios>()
                .Property(e => e.DesconectivoPrincipal)
                .IsUnicode(false);

            modelBuilder.Entity<CircuitosPrimarios>()
                .Property(e => e.EstadoOperativo)
                .IsUnicode(false);

            modelBuilder.Entity<CircuitosSubtransmision>()
                .Property(e => e.CodigoCircuito)
                .IsUnicode(false);

            modelBuilder.Entity<CircuitosSubtransmision>()
                .Property(e => e.SubestacionTransmision)
                .IsUnicode(false);

            modelBuilder.Entity<CircuitosSubtransmision>()
                .Property(e => e.DesconectivoSalida)
                .IsUnicode(false);

            modelBuilder.Entity<CircuitosSubtransmision>()
                .Property(e => e.NombreCircuito)
                .IsUnicode(false);

            modelBuilder.Entity<CircuitosSubtransmision>()
                .Property(e => e.EstadoOperativo)
                .IsUnicode(false);

            modelBuilder.Entity<SubestacionesCabezasLineas>()
               .Property(e => e.Codigolinea)
               .IsUnicode(false);

            modelBuilder.Entity<SubestacionesCabezasLineas>()
                .Property(e => e.SubestacionTransmicion)
                .IsUnicode(false);

            modelBuilder.Entity<SubestacionesCabezasLineas>()
                .Property(e => e.DesconectivoA)
                .IsUnicode(false);

            modelBuilder.Entity<SubestacionesCabezasLineas>()
                .Property(e => e.DesconectivoB)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionesInterrumpibles>()
               .Property(e => e.Codigo)
               .IsUnicode(false);

            modelBuilder.Entity<InstalacionesInterrumpibles>()
                .Property(e => e.Circuito)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionesInterrumpibles>()
                .Property(e => e.Padre)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionesInterrumpibles>()
                .Property(e => e.Sinonimo)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionesInterrumpibles>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionesInterrumpibles>()
                .Property(e => e.SeccionAlimentada)
                .IsUnicode(false);

            modelBuilder.Entity<Emplazamiento_Sigere>()
               .Property(e => e.Codigo)
               .IsUnicode(false);

            modelBuilder.Entity<Emplazamiento_Sigere>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Emplazamiento_Sigere>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Emplazamiento_Sigere>()
                .Property(e => e.Calle)
                .IsUnicode(false);

            modelBuilder.Entity<Emplazamiento_Sigere>()
                .Property(e => e.Numero)
                .IsUnicode(false);

            modelBuilder.Entity<Emplazamiento_Sigere>()
                .Property(e => e.Entrecalle1)
                .IsUnicode(false);

            modelBuilder.Entity<Emplazamiento_Sigere>()
                .Property(e => e.Entrecalle2)
                .IsUnicode(false);

            modelBuilder.Entity<Emplazamiento_Sigere>()
                .Property(e => e.BarrioPueblo)
                .IsUnicode(false);

            modelBuilder.Entity<Emplazamiento_Sigere>()
                .Property(e => e.CentroTransformacion)
                .IsUnicode(false);

            modelBuilder.Entity<Emplazamiento_Sigere>()
                .Property(e => e.centg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Fotos>()
                .Property(e => e.Instalacion)
                .IsUnicode(false);

            modelBuilder.Entity<Fotos>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Defectos>()
               .Property(e => e.TipoInstalacion)
               .IsUnicode(false);

            modelBuilder.Entity<Defectos>()
                .Property(e => e.Instalacion)
                .IsUnicode(false);

            modelBuilder.Entity<Defectos>()
                .Property(e => e.Seccion)
                .IsUnicode(false);

            modelBuilder.Entity<Defectos>()
                .Property(e => e.Dimension)
                .IsUnicode(false);

            modelBuilder.Entity<Defectos>()
                .Property(e => e.Id_LugarHabitado)
                .IsUnicode(false);

            modelBuilder.Entity<Defectos>()
                .Property(e => e.Id_Calle)
                .IsUnicode(false);

            modelBuilder.Entity<Defectos>()
                .Property(e => e.Id_Entrecalle1)
                .IsUnicode(false);

            modelBuilder.Entity<Defectos>()
                .Property(e => e.Id_Entrecalle2)
                .IsUnicode(false);

            modelBuilder.Entity<Defectos>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Defectos>()
                .Property(e => e.Emplazamiento)
                .IsUnicode(false);

            modelBuilder.Entity<Defectos>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Elementos>()
                .Property(e => e.Elemento)
                .IsUnicode(false);

            modelBuilder.Entity<Elementos>()
                .Property(e => e.Tipo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<materialpostes>()
                .Property(e => e.Material)
                .IsUnicode(false);

            modelBuilder.Entity<TipoDefecto>()
                .Property(e => e.Defecto)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_Mantenimientos>()
               .Property(e => e.Nombre)
               .IsUnicode(false);

            modelBuilder.Entity<Sub_Mantenimientos>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_Mantenimientos>()
                .Property(e => e.NivelAceite)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_Mantenimientos>()
                .Property(e => e.IndiceAcidez)
                .IsUnicode(false);

            modelBuilder.Entity<Adm_PersonalExtendido>()
                .Property(e => e.nombreUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<Adm_PersonalExtendido>()
                .Property(e => e.contrasenna)
                .IsUnicode(false);

            modelBuilder.Entity<Adm_PersonalPWD>()
                .Property(e => e.contrasenna)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_Termografias>()
               .Property(e => e.Subestacion)
               .IsUnicode(false);

            modelBuilder.Entity<Sub_PuntoTermografia>()
               .Property(e => e.Subestacion)
               .IsUnicode(false);

            modelBuilder.Entity<Sub_PuntoTermografia>()
                .Property(e => e.Fase)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PuntoTermografia>()
                .Property(e => e.Comentario)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PuntoTermografia>()
                .Property(e => e.elemennto)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PuntoTermografia>()
                .Property(e => e.descrpPtoCaleinte)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_CapacidadFusible>()
               .Property(e => e.TipoFusible)
               .IsUnicode(false);

            modelBuilder.Entity<PortaFusibles>()
                .Property(e => e.CodigoPortafusible)
                .IsUnicode(false);

            modelBuilder.Entity<PortaFusibles>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<PortaFusibles>()
                .Property(e => e.TEquipoProt)
                .IsUnicode(false);

            modelBuilder.Entity<PortaFusibles>()
                .Property(e => e.CE)
                .IsUnicode(false);

            modelBuilder.Entity<PortaFusibles>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<PortaFusibles>()
                .Property(e => e.Fase)
                .IsUnicode(false);

            modelBuilder.Entity<PortaFusibles>()
                .Property(e => e.Ubicacion)
                .IsUnicode(false);

            modelBuilder.Entity<PortaFusibles>()
                .Property(e => e.EstadoOperativo)
                .IsUnicode(false);

            modelBuilder.Entity<PortaFusibles>()
                .Property(e => e.Marca)
                .IsUnicode(false);

            modelBuilder.Entity<PortaFusibles>()
                .Property(e => e.TipoFusible)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Puente>()
               .Property(e => e.Codigo)
               .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Puente_Tipo>()
                .Property(e => e.DescripcionTipo)
                .IsUnicode(false);
            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.CodigoNuevo)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.TipoInstalacion)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.TipoSeccionalizador)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.CircuitoA)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.SeccionA)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.CircuitoB)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.SeccionB)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.Funcion)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.AjusteOperacion)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.Calle)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.Numero)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.Entrecalle1)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.EntreCalle2)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.BarrioPueblo)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.UbicadaEn)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.InstalacionAbrioA)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.InstalacionAbrioB)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.InstalacionAbrioC)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .Property(e => e.EstadoOperativo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_InspAspectosSubTrans>()
                .Property(e => e.NombreCelaje)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_InspAspectosSubTrans>()
                .Property(e => e.CodigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_InspAspectosSubTrans>()
                .Property(e => e.Defecto)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_InspAspectosSubTrans>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<InstalacionDesconectivos>()
                .HasOptional(e => e.InstalacionDesconectivos1)
                .WithRequired(e => e.InstalacionDesconectivos2);

            modelBuilder.Entity<Inst_Nomenclador_Puente>()
                .HasOptional(e => e.Inst_Nomenclador_Puente1)
                .WithRequired(e => e.Inst_Nomenclador_Puente2);
            //modelBuilder.Entity<Inst_Nomenclador_Puente>()
            //    .HasOptional(e => e.Inst_Nomenclador_Puente1);
            //.WithRequired(e => e.InstalacionDesconectivos);


            //modelBuilder.Entity<Inst_Nomenclador_Puente>()
            //  .HasMany(e => e.InstalacionDesconectivos1)
            //   .WithRequired(e => e.Inst_Nomenclador_Puente)
            //   .HasForeignKey(e => e.Codigo);

            modelBuilder.Entity<Inst_Nomenclador_Puente_Modelo>()
                .Property(e => e.Descripcion_Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Breakers>()
               .Property(e => e.CodigoBreaker)
               .IsUnicode(false);

            modelBuilder.Entity<Breakers>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Breakers>()
                .Property(e => e.modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Cuchillas>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_InterruptorAire>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_InterruptorAire_Operacion>()
                .HasMany(e => e.Inst_Nomenclador_InterruptorAire)
                .WithOptional(e => e.Inst_Nomenclador_InterruptorAire_Operacion)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos>()
              .HasMany(e => e.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion)
              .WithOptional(e => e.Inst_Nomenclador_Desconectivos)
              .WillCascadeOnDelete();

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.SerieInterruptor)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.NroEmpresa)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.NroInventario)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.SeriGabinete)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.Observacion)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.Marca)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivo_MedidoExtincionArco>()
               .HasMany(e => e.Inst_Nomenclador_Desconectivos)
               .WithOptional(e => e.Inst_Nomenclador_Desconectivo_MedidoExtincionArco)
               .HasForeignKey(e => e.Id_MedioExtinsion)
               .WillCascadeOnDelete();

            modelBuilder.Entity<Inst_Nomenclador_Desconectivo_PresionGas>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivo_PresionGas)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivos)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_Aislamiento>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivos_Aislamiento)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_ApertCable>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivos_ApertCable)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_BIL>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivos_BIL)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_CorrienteNominal>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivos_CorrienteNominal)
                .HasForeignKey(e => e.Id_Corriente)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_Cortocircuito>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivos_Cortocircuito)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.SerieInterruptor)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.NroEmpresa)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.NroInventario)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.SeriGabinete)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.Observacion)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.Marca)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>()
                .Property(e => e.Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_EquipoUtilizado>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_EquipoUtilizado)
                .HasForeignKey(e => e.EquipoUtilizado);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_SCADA>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_SCADA)
                .HasForeignKey(e => e.SCADA)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TensionInst>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TensionInst)
                .HasForeignKey(e => e.Id_TensionInstalacion);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TipoGabinete>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TipoGabinete)
                .HasForeignKey(e => e.Id_Gabinete);

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_Modelo>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivos_Modelo)
                .HasForeignKey(e => e.Id_ModeloDesconectivo)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_SecuenciaOperacion>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivos_SecuenciaOperacion)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Inst_Nomenclador_Desconectivos_TensionNominal>()
                .HasMany(e => e.Inst_Nomenclador_Desconectivos)
                .WithOptional(e => e.Inst_Nomenclador_Desconectivos_TensionNominal)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.CodigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.TipoCerca)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.EstadoCerca)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.CoronacionCerca)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.PinturaCerca)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.AterraminetoCerca)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.TipoPuerta)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.EstadoPuerta)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.CoronacionPuerta)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.PinturaPuerta)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.AterraminetoPuerta)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.TipoAlumbrado)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.EstadoAlumbrado)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.ControlAlumbrado)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.EstadoFotoCelda)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.TipoPiso)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.EstadoPiso)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.EstOrdenPiso)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.EstAreaExterior)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.FranjaContraIncendio)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.CartelesPuerta)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.CartelesCerca)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.CandadoPuerta)
                .IsUnicode(false);

            modelBuilder.Entity<SubMttoSubDistribucion>()
                .Property(e => e.EstParaFranklin)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistBarra>()
                .Property(e => e.CodigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistBarra>()
                .Property(e => e.CodigoBarra)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistBarra>()
                .Property(e => e.EstadoBarra)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistBarra>()
                .Property(e => e.Conexiones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistBarra>()
                .Property(e => e.EstadoPuentes)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistPararrayos>()
                .Property(e => e.CodigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistPararrayos>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistPararrayos>()
                .Property(e => e.EstAterramiento)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistRelacTransformacion>()
                .Property(e => e.CodigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistRelacTransformacion>()
                .Property(e => e.LabelFaseA)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistRelacTransformacion>()
                .Property(e => e.LabelFaseB)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistRelacTransformacion>()
                .Property(e => e.LabelFaseC)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistResistOhmica>()
                .Property(e => e.CodigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistResistOhmica>()
                .Property(e => e.LabelFaseAPrim)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistResistOhmica>()
                .Property(e => e.LabelFaseBPrim)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistResistOhmica>()
                .Property(e => e.LabelFaseCPrim)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistResistOhmica>()
                .Property(e => e.LabelFaseASec)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistResistOhmica>()
                .Property(e => e.LabelFaseBSec)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistResistOhmica>()
                .Property(e => e.LabelFaseCSec)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.CodigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.CodigoDesc)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.EstLimpiezaTanque)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.EstLimpiezaGabinete)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.EstPintura)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.EstAterramTanque)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.EstAterramGabinete)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.PresionSF6)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.PruebasFuncionales)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.EstRotulos)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.Ovservaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.LimpiezaAislamiento)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.LimpiezaContactos)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.Pintura)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.MttoMando)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.Aterramiento)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistribDesconectivos>()
                .Property(e => e.VerificacionCapacidad)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.CodigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoTanqueExpansion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoBusholtz_ReleIntegral)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoBushingAlta)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoBushingBaja)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.TuboExplosor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoValvulaSobrePresion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoValvulas)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoRadiadores)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.SaliderosResumidores)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoTiempo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.InstUtilizadoResistAisl)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoIndNivelaceite)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.NivelAceite)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoSilica)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoTermometro)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoPintura)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoVentilacion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoConexiones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.AterramientoTanque)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.AterramientoNeutro)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.InstUtilizadoResistOhm)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.InstUtiliRelacTransf)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.NormaRigidezDielectrica)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.InstUtilRigidezDielectrica)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoPrimVsSecTierra)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoSecVsPrimTierra)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .Property(e => e.EstadoPrimSecVsTierra)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoDistTransf>()
                .HasMany(e => e.Sub_MttoDistRelacTransformacion)
                .WithRequired(e => e.Sub_MttoDistTransf)
                .HasForeignKey(e => new { e.CodigoSub, e.Fecha, e.Id_Transformador, e.Id_EATransformador });

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.CodigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoTanqueExpansion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoBusholtz_ReleIntegral)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoBushingAlta)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoBushingBaja)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.TuboExplosor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoValvulaSobrePresion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoValvulas)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoRadiadores)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.SaliderosResumidores)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoTiempo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoIndNivelaceite)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.NivelAceite)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoSilica)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoTermometro)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoPintura)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoVentilacion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoConexiones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.AterramientoTanque)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.AterramientoNeutro)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.InstUtilizadoResistOhm)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.InstUtiliRelacTransf)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.NormaRigidezDielectrica)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoPrimVsSecTierra)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoSecVsPrimTierra)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .Property(e => e.EstadoPrimSecVsTierra)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .HasMany(e => e.Sub_MttoUPRelacTransformacion)
                .WithRequired(e => e.Sub_MttoTransUsoP)
                .HasForeignKey(e => new { e.CodigoSub, e.Fecha, e.Id_Transformador, e.Id_EATransformador });

            modelBuilder.Entity<Sub_MttoTransUsoP>()
                .HasMany(e => e.Sub_MttoUPResistOhmica)
                .WithRequired(e => e.Sub_MttoTransUsoP)
                .HasForeignKey(e => new { e.CodigoSub, e.Fecha, e.Id_Transformador, e.Id_EATransformador });

            modelBuilder.Entity<Sub_MttoUPRelacTransformacion>()
                .Property(e => e.CodigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoUPRelacTransformacion>()
                .Property(e => e.LabelFaseA)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoUPRelacTransformacion>()
                .Property(e => e.LabelFaseB)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoUPRelacTransformacion>()
                .Property(e => e.LabelFaseC)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoUPResistOhmica>()
                .Property(e => e.CodigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoUPResistOhmica>()
                .Property(e => e.LabelFaseAPrim)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoUPResistOhmica>()
                .Property(e => e.LabelFaseBPrim)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoUPResistOhmica>()
                .Property(e => e.LabelFaseCPrim)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoUPResistOhmica>()
                .Property(e => e.LabelFaseASec)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoUPResistOhmica>()
                .Property(e => e.LabelFaseBSec)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoUPResistOhmica>()
                .Property(e => e.LabelFaseCSec)
                .IsUnicode(false);

            modelBuilder.Entity<TipoMantenimiento>()
               .Property(e => e.TipoMtto)
               .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.nombreCelaje)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.codigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.dropOutAFaseA)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.dropOutBFaseA)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.dropOutAFaseB)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.dropOutBFaseB)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.dropOutAFaseC)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.dropOutBFaseC)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.dropOutBypassFaseA)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.dropOutBypassFaseB)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.dropOutBypassFaseC)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.interruptorAltaNAceite)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.interruptorAltaPintura)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.interruptorBajaNAceite)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.interruptorBajaPintura)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.pRayosAlta)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.pRayosBaja)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.mallaTSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.mallaTCerca)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.estadoCerca)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.estadoPuerta)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.otrasInformaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.estadoCarteles)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.estadoAlumbrado)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.estadoCandadoPuerta)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistribucion>()
                .Property(e => e.tipoCelaje)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistInterruptor>()
               .Property(e => e.codigoInterruptor)
               .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistInterruptor>()
                .Property(e => e.nombreCelaje)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistInterruptor>()
                .Property(e => e.codigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistInterruptor>()
                .Property(e => e.NAceite)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistInterruptor>()
                .Property(e => e.Pintura)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistInterruptor>()
                .Property(e => e.estadoBateria)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistInterruptor>()
                .Property(e => e.candadoGabinete)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CelajeSubDistInterruptor>()
                .Property(e => e.tipoInterruptor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_TransformadoresSubtrCelaje>()
                .Property(e => e.nombreCelaje)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_TransformadoresSubtrCelaje>()
                .Property(e => e.codigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_TransformadoresSubtrCelaje>()
                .Property(e => e.nAceite)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_TransformadoresSubtrCelaje>()
                .Property(e => e.pintura)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_TransformadoresSubtrCelaje>()
                .Property(e => e.aterramiento)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_TransformadoresSubtrCelaje>()
                .Property(e => e.bushing)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_TransformadoresSubtrCelaje>()
                .Property(e => e.observaciones)
                .IsUnicode(false);
            modelBuilder.Entity<Sub_Certificacion>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_Certificacion>()
                .Property(e => e.CodigoSub)
                .IsUnicode(false);
            modelBuilder.Entity<Sub_Celaje>()
                .Property(e => e.NombreCelaje)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_Celaje>()
                .Property(e => e.CodigoSub)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_Certificacion>()
                .HasMany(e => e.Sub_CertificacionComision)
                .WithOptional(e => e.Sub_Certificacion)
                .HasForeignKey(e => new { e.Id_EACertificacion, e.NumAccionCertificacion })
                .WillCascadeOnDelete();

            modelBuilder.Entity<Sub_Certificacion>()
                .HasMany(e => e.Sub_CertificacionDetalles)
                .WithOptional(e => e.Sub_Certificacion)
                .HasForeignKey(e => new { e.Id_EACertificacion, e.NumAccionCertificacion })
                .WillCascadeOnDelete();

            modelBuilder.Entity<Sub_CertificacionAspectos>()
                .Property(e => e.NombreAspecto)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CertificacionComision>()
                .Property(e => e.NombreComision)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CertificacionComision>()
                .Property(e => e.CargoComision)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CertificacionDetalles>()
                .Property(e => e.Cumplimiento)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CertificacionDetalles>()
                .Property(e => e.Responsable)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_CertificacionSubAspectos>()
                .Property(e => e.NombreSubAspecto)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAQReducido>()
               .Property(e => e.CodigoSub)
               .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAQReducido>()
                .Property(e => e.RealizadoenLaboraorio)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAQReducido>()
                .Property(e => e.NumeroControl)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAQReducido>()
                .Property(e => e.MuestreoSegProc)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAQReducido>()
                .Property(e => e.MuestreoSegProcPor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAQReducido>()
                .Property(e => e.Clasificacion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAQReducido>()
                .Property(e => e.EjecutadoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAQReducido>()
                .Property(e => e.RevisadoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAQReducido>()
                .Property(e => e.AspectoFisico)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAQReducido>()
                .Property(e => e.ResulSegunNormas)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAQReducido>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAQReducido>()
                .Property(e => e.EmitidoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAQReducido>()
                .Property(e => e.CargoEmitidoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAGDisueltos>()
               .Property(e => e.CodigoSub)
               .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAGDisueltos>()
                .Property(e => e.RealizadoenLaboraorio)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAGDisueltos>()
                .Property(e => e.NumeroControl)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAGDisueltos>()
                .Property(e => e.MuestreoSegProc)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAGDisueltos>()
                .Property(e => e.MuestreadoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAGDisueltos>()
                .Property(e => e.EjecutadoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAGDisueltos>()
                .Property(e => e.RevisadoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAGDisueltos>()
                .Property(e => e.ResulSegunNormas)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAGDisueltos>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAGDisueltos>()
                .Property(e => e.EmitidoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAGDisueltos>()
                .Property(e => e.CargoEmitidoPor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAGDisueltos>()
                .Property(e => e.MetodoEnsayo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_PruebaAceiteAGDisueltos>()
                .Property(e => e.NormaMuestreo)
                .IsUnicode(false);
            modelBuilder.Entity<Sub_MedicionTierra>()
               .Property(e => e.Subestacion)
               .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra>()
                .Property(e => e.RealizadaPor)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra>()
                .Property(e => e.Empresa)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra>()
                .Property(e => e.ObservacionesGenerales)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra>()
                .Property(e => e.Marca)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra>()
                .Property(e => e.Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra>()
                .Property(e => e.Serie)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra>()
                .Property(e => e.Operador)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra>()
                .Property(e => e.TipoMalla)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra>()
                .Property(e => e.EstadoSuelo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra>()
                .Property(e => e.TipoSuelo)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra>()
                .Property(e => e.MallaEquipotencial)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra>()
                .Property(e => e.CertificacionAPCI)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra>()
                .Property(e => e.CorrienteParasita)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra_CaidaPotencial>()
                .Property(e => e.Subestacion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra_ContinuidadMalla>()
                .Property(e => e.Subestacion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra_ContinuidadMalla>()
                .Property(e => e.NoMedicion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MedicionTierra_ContinuidadMalla>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<LugarHabitado>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.subestacion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.TequipoProt)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.CodigoEquipoProtegido)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.incidenciasUltimaRev)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.porcelanas)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.tornilleria)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.platillosMembranas)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.conexiones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.aterramientos)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.partesMetalicas)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.EstadoMiliA)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.EstadoCuentaOp)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos>()
                .Property(e => e.VoltajeInstalado)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos_Fases>()
                .Property(e => e.Fase)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos_Fases>()
                .Property(e => e.subestacion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoPararrayos_Instrumentos>()
                .Property(e => e.Id_Instrumento)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoBateriasEstacionarias>()
                .Property(e => e.EstadoExtVasos)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoBateriasEstacionarias>()
                .Property(e => e.subestacion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoBateriasEstacionarias>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoBateriasEstacionarias>()
                .Property(e => e.incidenciasUltimaRev)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoBateriasEstacionarias_Vasos>()
                .Property(e => e.subestacion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerza>()
                .Property(e => e.Subestacion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaAceiteConmutador>()
                .Property(e => e.NormaUtilizada)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaAceiteTanquePrinc>()
                .Property(e => e.NormaUtilizada)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaAislamBushings>()
                .Property(e => e.TipoBushing)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaAislamBushings>()
                .Property(e => e.FaseA_Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaAislamBushings>()
                .Property(e => e.FaseB_Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaAislamBushings>()
                .Property(e => e.FaseC_Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaAislamEnrollado>()
                .Property(e => e.TipoTransformador)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaAislamEnrollado>()
                .Property(e => e.LugarMedicion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaAislamEnrollado>()
                .Property(e => e.EstadoCA)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaAislamEnrollado>()
                .Property(e => e.EstadoIP)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaConmutador>()
                .Property(e => e.LimpElimSalidero)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaConmutador>()
                .Property(e => e.PintPartesOxidadas)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaConmutador>()
                .Property(e => e.MantAccesorios)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaConmutador>()
                .Property(e => e.OtrasMediciones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaConmutador>()
                .Property(e => e.EstadoAceiteConmutador)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaConmutador>()
                .Property(e => e.CambioAceiteConmutador)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaConmutador>()
                .Property(e => e.EstadoConmutador)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.InspExtValvulas)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.InspExtNiveles)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.InspExtTornilleria)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.InspExtRadiador)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.InspExtBombas)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.InspExtConexiones)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.InspExtPresionSubita)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.InspExtVentiladores)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.InspExtTermometros)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.InspExtTermosifon)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.InspExtNitrogeno)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.InspExtTierras)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.InspExtRespiraderos)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.Busholtz)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaInspExterna>()
                .Property(e => e.Otros)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaPresionBushing>()
                .Property(e => e.NivelVoltaje)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaResistOhm>()
                .Property(e => e.NivelVoltaje)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaTanDeltaBushings>()
                .Property(e => e.Seccion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaTanDeltaBushings>()
                .Property(e => e.Esquema)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaTanDeltaEnrollado>()
                .Property(e => e.Seccion)
                .IsUnicode(false);

            modelBuilder.Entity<Sub_MttoTFuerzaTanDeltaEnrollado>()
                .Property(e => e.Esquema)
                .IsUnicode(false);
        }

    }

}
