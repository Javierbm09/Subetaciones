namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_PruebaAceiteAGDisueltos
    {
        [Required]
        [StringLength(7), Display(Name = "Subestación")]
        public string CodigoSub { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Transf { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_EAdminTransf { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime Fecha { get; set; }

        [StringLength(100), Display(Name = "Realizado en")]
        public string RealizadoenLaboraorio { get; set; }

        [StringLength(50)]
        [Display(Name = "Nro control")]
        public string NumeroControl { get; set; }

        [Display(Name = "Fecha selección")]
        public DateTime? FechaSeleccion { get; set; }

        [Display(Name = "Fecha recepción")]
        public DateTime? FechaRecepcion { get; set; }

        [Display(Name = "Fecha inicio")]
        public DateTime? FechaInicio { get; set; }

        [Display(Name = "Fecha terminación")]
        public DateTime? FechaTerminacion { get; set; }

        [StringLength(50)]
        [Display(Name = "Muestreado según proc.")]
        public string MuestreoSegProc { get; set; }

        [StringLength(75)]
        public string MuestreadoPor { get; set; }

        [StringLength(150)]
        public string EjecutadoPor { get; set; }

        [StringLength(150)]
        public string RevisadoPor { get; set; }

        [StringLength(200)]
        public string ResulSegunNormas { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        public int? ResNro { get; set; }

        [StringLength(75)]
        public string EmitidoPor { get; set; }

        [StringLength(75)]
        public string CargoEmitidoPor { get; set; }

        [StringLength(50)]
        [Display(Name = "Metodo de ensayo")]
        public string MetodoEnsayo { get; set; }

        [StringLength(50)]
        public string NormaMuestreo { get; set; }

        public double? H2Hidrogeno { get; set; }

        public double? CH4Metano { get; set; }

        public double? C2H6Etano { get; set; }

        public double? C2H4Etileno { get; set; }

        public double? C2H2Acetileno { get; set; }

        public double? COMonoxidoCarbono { get; set; }

        [Display(Name = "Temperatura del aceite")]
        public double? TempAceite { get; set; }

        public double? CO2DioxidoCarbono { get; set; }

        [NotMapped]
        public string f { get; set; }

        [NotMapped]
        public string fSelec { get; set; }

        [NotMapped]
        public string fIni { get; set; }

        [NotMapped]
        public string fRec { get; set; }

        [NotMapped]
        public string fTer { get; set; }

    }
}
