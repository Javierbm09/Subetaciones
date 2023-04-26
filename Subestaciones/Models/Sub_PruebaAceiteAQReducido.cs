namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_PruebaAceiteAQReducido
    {
        [Required]
        [StringLength(7), Display(Name ="Subestación")]
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
        [Display(Name ="Nro control")]
        public string NumeroControl { get; set; }

        [Display(Name ="Fecha selección")]
        public DateTime? FechaSeleccion { get; set; }

        [Display(Name = "Fecha recepción")]
        public DateTime? FechaRecepcion { get; set; }

        [Display(Name ="Fecha inicio")]
        public DateTime? FechaInicio { get; set; }

        [Display(Name ="Fecha terminación")]
        public DateTime? FechaTerminacion { get; set; }

        [StringLength(50)]
        [Display(Name ="Muestreado según proc.")]
        public string MuestreoSegProc { get; set; }

        [StringLength(75)]
        [Display(Name ="Por")]
        public string MuestreoSegProcPor { get; set; }

        [StringLength(1)]
        [Display(Name ="Clasificación")]
        public string Clasificacion { get; set; }

        [StringLength(150), Display (Name ="Ejecutador por")]
        public string EjecutadoPor { get; set; }

        [StringLength(150), Display(Name = "Revisado por")]
        public string RevisadoPor { get; set; }

        public double? TempMuestra { get; set; }

        public double? Densidada20GC { get; set; }

        public double? NrodeNeutralizacion { get; set; }

        public double? AguaporKFisher { get; set; }

        public double? HumedadenPapel { get; set; }

        public double? TensionInterfacial { get; set; }

        public double? PuntoInflamacion { get; set; }

        public double? RigidezDielectrica { get; set; }

        public double? SedimentoyCienoPrecip { get; set; }

        public double? Viscosidada40GC { get; set; }

        public double? FactorDisipacionTAmb { get; set; }

        public double? FactorDisipaciona70GC { get; set; }

        public double? FactorDisipacion90GC { get; set; }

        [StringLength(150)]
        public string AspectoFisico { get; set; }

        [StringLength(200)]
        public string ResulSegunNormas { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        public int? ResNro { get; set; }

        [StringLength(75)]
        public string EmitidoPor { get; set; }

        [StringLength(75)]
        public string CargoEmitidoPor { get; set; }

        [NotMapped]
        public string f { get; set; }

        [NotMapped]
        public string fechaSelec { get; set; }

        [NotMapped]
        public string fechaIni { get; set; }

        [NotMapped]
        public string fechaRec { get; set; }

        [NotMapped]
        public string fechaTer { get; set; }

    }
}
