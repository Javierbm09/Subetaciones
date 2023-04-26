namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubMttoSubDistribucion")]
    public partial class SubMttoSubDistribucion
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        [Display (Name ="Subestación")]
        public string CodigoSub { get; set; }

        [Key]
        [Column(Order = 1)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Display (Name ="Tipo mantenimiento"), Required(ErrorMessage = "Debe suministrar el tipo de mantenimiento")]
        public short? TipoMantenimiento { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        [StringLength(50)]
        [Display (Name ="Tipo")]
        public string TipoCerca { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado")]
        public string EstadoCerca { get; set; }

        [StringLength(20)]
        [Display (Name ="Coronación")]
        public string CoronacionCerca { get; set; }

        [StringLength(20)]
        [Display (Name ="Pintura")]
        public string PinturaCerca { get; set; }

        [StringLength(20)]
        [Display (Name ="Aterramiento")]
        public string AterraminetoCerca { get; set; }

        [StringLength(50)]
        [Display (Name ="Tipo")]
        public string TipoPuerta { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado")]
        public string EstadoPuerta { get; set; }

        [StringLength(20)]
        [Display (Name ="Coronación")]
        public string CoronacionPuerta { get; set; }

        [StringLength(20)]
        [Display (Name ="Pintura")]
        public string PinturaPuerta { get; set; }

        [StringLength(20)]
        [Display (Name ="Aterramiento")]
        public string AterraminetoPuerta { get; set; }

        [StringLength(50)]
        [Display (Name ="Tipo")]
        public string TipoAlumbrado { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado")]
        public string EstadoAlumbrado { get; set; }

        [StringLength(20)]
        [Display(Name = "Control")]
        public string ControlAlumbrado { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado fotocelda")]
        public string EstadoFotoCelda { get; set; }

        [Display(Name = "Cant. lámparas")]
        public short? CantidadLamparas { get; set; }

        [StringLength(50)]
        [Display(Name = "Tipo piso")]
        public string TipoPiso { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado piso")]
        public string EstadoPiso { get; set; }

        [StringLength(20)]
        [Display(Name = "Orden")]
        public string EstOrdenPiso { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado")]
        public string EstAreaExterior { get; set; }

        [StringLength(20)]
        [Display(Name = "Franja contra incendios")]
        public string FranjaContraIncendio { get; set; }

        [StringLength(100)]
        public string Observaciones { get; set; }

        [Display (Name ="Realizado por"), Required(ErrorMessage = "Debe suministrar el ejecutor del mantenimiento")]
        public short? RealizadoPor { get; set; }

        [StringLength(20)]
        [Display(Name = "Carteles")]
        public string CartelesPuerta { get; set; }

        [StringLength(20)]
        [Display(Name = "Carteles")]
        public string CartelesCerca { get; set; }

        [StringLength(20)]
        [Display(Name = "Candado")]
        public string CandadoPuerta { get; set; }

        [Display(Name = "Cant. lámparas servicio")]
        public short? CantLamparasServicio { get; set; }

        [StringLength(20)]
        public string EstParaFranklin { get; set; }

        public bool? Mantenido { get; set; }

        [NotMapped]
        public string f { get; set; }
    }
}
