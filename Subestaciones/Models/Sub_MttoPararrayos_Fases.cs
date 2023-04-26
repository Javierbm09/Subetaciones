namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoPararrayos_Fases
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string Fase { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short id_EAdministrativa { get; set; }

        [Display(Name = "Aislamiento 15 seg (GΩ)")]
        public double? Aislamiento15secc1 { get; set; }

        [Display(Name = "Aislamiento 15 seg (GΩ)")]
        public double? Aislamiento15secc2 { get; set; }

        [Display(Name = "Aislamiento 60 seg (GΩ)")]
        public double? Aislamiento60secc1 { get; set; }

        [Display(Name = "Aislamiento 60 seg (GΩ)")]
        public double? Aislamiento60secc2 { get; set; }

        [Display(Name = "Aislamiento DAR")]
        public double? AislamientoDARsecc1 { get; set; }

        [Display(Name = "Aislamiento DAR")]
        public double? AislamientoDARsecc2 { get; set; }

        [Display(Name = "Voltaje (V)")]
        public short? VoltajeSecc1 { get; set; }

        [Display(Name = "Voltaje (V)")]
        public short? VoltajeSecc2 { get; set; }

        [Display(Name = "Corriente de Filtración a CD (µA)")]
        public double? CorrFiltracionCDSecc1 { get; set; }

        [Display(Name = "Corriente de Filtración a CD (µA)")]
        public double? CorrFiltracionCDSecc2 { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_MttoPararrayo { get; set; }

        [Required]
        [StringLength(7)]
        public string subestacion { get; set; }
    }
}
