namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ES_TP_Devanado
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Nro_Dev { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Nro_TP { get; set; }

        public short? id_Voltaje_Secundario { get; set; }

        [StringLength(30)]
        [Display(Name = "Designación")]
        public string Designacion { get; set; }

        [StringLength(30)]
        [Display(Name = "Clase precisión")]
        public string ClasePresicion { get; set; }

        [Display(Name = "Capacidad(VA)")]
        public double? Capacidad { get; set; }

        [StringLength(20)]
        [Display(Name = "Tensión(V)")]
        public string Tension { get; set; }
    }
}
