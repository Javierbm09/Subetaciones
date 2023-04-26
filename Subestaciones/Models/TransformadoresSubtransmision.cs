namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransformadoresSubtransmision")]
    public partial class TransformadoresSubtransmision
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
        [Display(Name = "Ubicado"), Required(ErrorMessage = "Debe suministrar el valor: {0} ")]
        public string Codigo { get; set; }

        [StringLength(10)]
        [Display(Name = "Nro Empresa")]
        public string Numemp { get; set; }

        [Display(Name = "Capacidad")]
        public short? Id_Capacidad { get; set; }

        [StringLength(15)]
        public string SimboloTaps { get; set; }

        [Display(Name = "Tensión Primaria (kV)")]
        public short? Id_VoltajePrim { get; set; }

        [StringLength(3)]
        public string Fase { get; set; }

        [StringLength(15)]
        [Display(Name = "Nro Serie")]
        public string NoSerie { get; set; }

        public bool NecesidadEmitida { get; set; }

        [StringLength(11)]
        public string CE { get; set; }

        [StringLength(12)]
        public string PosicionBanco { get; set; }

        [StringLength(1)]
        [Display(Name = "Estado Operativo")]
        public string EstadoOperativo { get; set; }

        public short? TapEncontrado { get; set; }

        public short? TapDejado { get; set; }

        [Display(Name = "Nro Fase")]
        public short? NumFase { get; set; }

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

        [Display(Name = "Tensión Secundaria (kV)")]
        public short? Id_Voltaje_Secun { get; set; }

        [Display(Name = "Bloque de transformación")]
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

        [NotMapped, Display(Name = "Tensión salida")]
        public double? tensionSalidaBloque { get; set; }

        [MaxLength(1)]
        public byte[] TabRegulable { get; set; }

        [MaxLength(1)]
        public byte[] TabPrimarioSecundario { get; set; }

        public short? CantidadRegulacion { get; set; }

        public short? CuentaOperaciones { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TabFecha { get; set; }

        [Display(Name = "Año Fabricación")]
        [RegularExpression(@"(^[1-2][0-9]{1,5})", ErrorMessage = "El valor no es valido para el año")]
        public short? AnnoFabricacion { get; set; }

        [Display(Name ="Fabricante")]
        public int? idFabricante { get; set; }

        public short? VoltajeSecundario { get; set; }

        [Display(Name = "Porciento Impedancia")]
        public double? PorcientoImpedancia { get; set; }

        [StringLength(30)]
        [Display(Name = "Grupo Conexión")]
        public string GrupoConexion { get; set; }

        [Display(Name = "Peso Total (Kg)")]
        public double? PesoTotal { get; set; }

        public short? CapacidadVentilador { get; set; }

        [StringLength(5)]
        [Required(ErrorMessage = "Debe suministrar el : {0} ")]
        public string Nombre { get; set; }

        [Display(Name = "Corriente Primaria (A)")]
        public double? CorrienteAlta { get; set; }

        [Display(Name = "Frecuencia (Hz)")]
        public double? FrecuenciaN { get; set; }

        [Display(Name ="Enfriamiento")]
        public short? TipoEnfriamiento { get; set; }

        [Display(Name = "Pérdida Vacío")]
        public double? PerdidasVacio { get; set; }

        [Display(Name = "Pérdida Bajo Carga")]
        public double? PerdidasBajoCarga { get; set; }

        public double? NivelRuido { get; set; }

        [Display(Name ="Máx. Temperatura")]
        public double? MaxTemperatura { get; set; }

        [Display(Name = "Tensión Impulso (kV)")]
        public double? VoltajeImpulso { get; set; }

        [Display(Name = "Peso Aceite (Kg)")]
        public double? PesoAceite { get; set; }

        [Display(Name = "Peso Núcleo (Kg)")]
        public double? PesoNucleo { get; set; }

        public double? NivelRadioInterf { get; set; }

        [Display(Name = "Corriente Secundaria (A)")]
        public double? CorrienteBaja { get; set; }

        [StringLength(50)]
        public string Tipo { get; set; }

        [Display(Name = "Cant. Ventiladores")]
        public short? CantVentiladores { get; set; }

        [Display(Name = "Cant. Radiadores")]
        public short? CantRadiadores { get; set; }

        [StringLength(700)]
        public string Observaciones { get; set; }

        [Display(Name = "Peso Transporte (Kg)")]
        public double? PesoTansporte { get; set; }

        [StringLength(20)]
        [Display(Name = "Regulación Voltaje")]
        public string TipoRegVoltaje { get; set; }

        public short? NroPosiciones { get; set; }

        [StringLength(50)]
        [Display(Name = "Tipo Caja de Mando")]
        public string TipoCajaMando { get; set; }

        [Display(Name = "Tiene Tubo Explosor")]
        public bool? TuboExplosor { get; set; }

        [Display(Name = "Tiene Válvula Sobrepresión")]
        public bool? ValvulaSobrePresion { get; set; }

        [Display(Name = "Tensión Terciaria (kV)")]
        public short? VoltajeTerciario { get; set; }

        [Display(Name = "Posición de Trabajo")]
        public short? PosicionTrabajo { get; set; }

        [StringLength(30)]
        [Display(Name ="Nro Inventario")]
        public string NumeroInventario { get; set; }

        [Display(Name = "Fecha Instalado")]
        public DateTime? FechaDeInstalado { get; set; }
    }
}
