namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransformadoresTransmision")]
    public partial class TransformadoresTransmision
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_EAdministrativa { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Transformador { get; set; }

        public int NumAccion { get; set; }

        [StringLength(7)]
        [Display(Name = "Subestación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Codigo { get; set; }

        [StringLength(5)]
        [Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Nombre { get; set; }

        [StringLength(30)]
        [Display(Name = "Nro Inventario")]
        public string NumeroInventario { get; set; }

        [StringLength(30)]
        [Display(Name = "Grupo Conexión")]
        public string GrupoConexion { get; set; }

        [Display(Name = "Fecha de Instalado")]
        public DateTime? FechaDeInstalado { get; set; }

        [StringLength(10)]
        [Display(Name = "Nro. Empresa")]
        public string Numemp { get; set; }

        [Display(Name = "Enfriamiento")]
        public short? TipoEnfriamiento { get; set; }

        [Display(Name = "Frecuencia (Hz)")]
        public double? FrecuenciaN { get; set; }

        [Display(Name = "Corriente Primaria (A)")]
        public double? CorrienteAlta { get; set; }
         
        [Display(Name = "Corriente Secundaria (A)")]
        public double? CorrienteBaja { get; set; }

        [Display(Name = "Corriente Terciaria (A)")]
        public double? CorrienteTerciaria { get; set; }

        [StringLength(50)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Display(Name = "Máx Temperatura")]
        public double? MaxTemperatura { get; set; }

        [Display(Name = "Año Fabricación")]
        [RegularExpression(@"(^[1-2][0-9]{1,5})", ErrorMessage = "El valor no es valido para el año")]
        public short? AnnoFabricacion { get; set; }

        [Display(Name = "Pérdidas Bajo Carga")]
        public double? PerdidasBajoCarga { get; set; }

        [Display(Name = "Nivel Radio Interferencia")]
        public double? NivelRadioInterf { get; set; }

        [Display(Name = "Tensión Primaria (kV)")]
        public short? Id_VoltajePrim { get; set; }

        [Display(Name = "Tensión Secundaria (kV)")]
        public short? Id_Voltaje_Secun { get; set; }

        [Display(Name = "Tensión Impulso (kV)")]
        public double? VoltajeImpulso { get; set; }

        [Display(Name = "Peso Total")]
        public double? PesoTotal { get; set; }

        [Display(Name = "Peso Aceite")]
        public double? PesoAceite { get; set; }

        [Display(Name = "Peso Núcleo")]
        public double? PesoNucleo { get; set; }

        [Display(Name = "Peso Transporte")]
        public double? PesoTansporte { get; set; }

        [StringLength(15)]
        [Display(Name = "Número de Serie")]
        public string NoSerie { get; set; }

        [Display(Name = "Fabricante")]
        public int? idFabricante { get; set; }

        [Display(Name = "Pérdidas en Vacío")]
        public double? PerdidasVacio { get; set; }

        [Display(Name = "Capacidad")]
        public short? Id_Capacidad { get; set; }

        [Display(Name = "Cant. Radiadores")]
        public short? CantRadiadores { get; set; }

        [Display(Name = "Cant. Ventiladores")]
        public short? CapacidadVentilador { get; set; }

        [Display(Name = "% Zcc P-S")]
        public double? PorcientoZccPS { get; set; }

        [Display(Name = "% Zcc S-T")]
        public double? PorcientoZccST { get; set; }

        [Display(Name = "% Zcc P-T")]
        public double? PorcientoZccPT { get; set; }

        [Display(Name = "Estado Operativo")]
        [StringLength(1)]
        public string EstadoOperativo { get; set; }

        [StringLength(700)]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Display(Name = "Tubo Explosor")]
        public bool? TuboExplosor { get; set; }

        [Display(Name = "Válvula Sobrepresión")]
        public bool? ValvulaSobrePresion { get; set; }

        [Display(Name = "Nro. Fase")]
        public short? NumFase { get; set; }

        [Display(Name = "Termosifones")]
        public bool? TieneTermosifones { get; set; }

        [Display(Name = "Cant. Termosifones")]
        public short? CantTermosifones { get; set; }

        [Display(Name = "Regulación Voltaje Prim")]
        public string TipoRegVoltaje { get; set; }

        [Display(Name = "Nro Posiciones")]
        public short? NroPosiciones { get; set; }


        [Display(Name = "Posición Trabajo")]
        public short? PosicionTrabajo { get; set; }

        [Display(Name = "Tipo Caja Mando")]
        [StringLength(50)]
        public string TipoCajaMando { get; set; }

        [Display(Name = "Nro Posiciones")]
        [StringLength(12)]
        public string PosicionBanco { get; set; }

        [StringLength(15)]
        public string SimboloTaps { get; set; }

        [StringLength(3)]
        public string Fase { get; set; }

        public bool NecesidadEmitida { get; set; }

        [StringLength(11)]
        public string CE { get; set; }

        public short? TapEncontrado { get; set; }

        public short? TapDejado { get; set; }
       
        public int? UltAccionVer { get; set; }

        [StringLength(10)]
        public string EstadoHermeticidad { get; set; }

        [StringLength(15)]
        public string EstadoPinturaTanque { get; set; }

        [StringLength(10)]
        public string EstadoPinturaRotulos { get; set; }

        [StringLength(6)]
        public string AcidezAceite { get; set; }

        [StringLength(6)]
        public string NivelAceite { get; set; }

        [StringLength(6)]
        public string ColoracionAceite { get; set; }

        public short? PerteneceA { get; set; }

        public short? Id_Bloque { get; set; }

        [NotMapped, Display(Name = "Tipo de bloque")]
        public string Bloque { get; set; }

        [NotMapped, Display(Name = "Esquema por baja")]
        public string esquemaBloque { get; set; }

        [NotMapped, Display(Name = "Sector cliente")]
        public string sectorClienteBloque { get; set; }

        [NotMapped, Display(Name = "Cliente")]
        public string clienteBloque { get; set; }

        [NotMapped, Display(Name = "Tipo de salida")]
        public string tipoSalidaBloque { get; set; }

        [NotMapped, Display(Name = "Tensión terciario")]
        public double? tensionTerciarioBloque { get; set; }

        [NotMapped, Display(Name = "Tension salida")]
        public double? tensionSalidaBloque { get; set; }

        [MaxLength(1)]
        public byte[] TabRegulable { get; set; }

        [MaxLength(1)]
        public byte[] TabPrimarioSecundario { get; set; }

        public short? CantidadRegulacion { get; set; }

        public short? CuentaOperaciones { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TabFecha { get; set; }

        public short? VoltajeSecundario { get; set; }

        [Display(Name = "Porciento Impedancia")]
        public double? PorcientoImpedancia { get; set; }

        public double? NivelRuido { get; set; }

        public short? CantVentiladores { get; set; }

        [Display(Name = "Tensión Terciario (kV)")]
        public short? VoltajeTerciario { get; set; }

        [Display(Name = "Nro posiciones")]
        public short? RegVoltajeSecNroPos { get; set; }

        [Display(Name = "Posición Trabajo")]
        public short? RegVoltajeSecPosTrab { get; set; }
       
        [StringLength(30)]
        public string BushingPrimFaseATipo { get; set; }

        [StringLength(30)]
        public string BushingPrimFaseBTipo { get; set; }

        [StringLength(30)]
        public string BushingPrimFaseCTipo { get; set; }

        [StringLength(30)]
        public string BushingPrimFaseNeutroTipo { get; set; }

        [StringLength(30)]
        public string BushingSecFasesTipo { get; set; }

        [StringLength(30)]
        public string BushingSecNeutroTipo { get; set; }

        [StringLength(30)]
        public string BushingTercFasesTipo { get; set; }

        [StringLength(30)]
        public string BushingPrimFaseASerie { get; set; }

        [StringLength(30)]
        public string BushingPrimFaseBSerie { get; set; }

        [StringLength(30)]
        public string BushingPrimFaseCSerie { get; set; }

        public double? BushingPrimFaseAUn { get; set; }

        public double? BushingPrimFaseBUn { get; set; }

        public double? BushingPrimFaseCUn { get; set; }

        public double? BushingPrimFaseAIn { get; set; }

        public double? BushingPrimFaseBIn { get; set; }

        public double? BushingPrimFaseCIn { get; set; }

        public double? BushingSecFasesUn { get; set; }

        public double? BushingSecFasesIn { get; set; }

        public double? BushingTercFasesUn { get; set; }

        public double? BushingTercFasesIn { get; set; }


    }
}
