namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ES_TC_Devanado
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Devanado")]
        public short Nro_Dev { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
       
        public string Nro_TC { get; set; }

        [Display(Name = "Capacidad(VA)")]
        public double? Capacidad { get; set; }

        [StringLength(50)]
        [Display(Name = "Clase Precisión")]
        public string Clase_Precision { get; set; }

        [StringLength(50)]
        [Display(Name = "Designación")]
        public string Designacion { get; set; }
    }
}
