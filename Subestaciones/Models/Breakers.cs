namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Breakers
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short id_EAdministrativa { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Breaker { get; set; }

        [NotMapped]
        [Display(Name = "Tipo portafusible")]
        public int tipoPortafusible { get; set; }

        public int? NumAccion { get; set; }

        [StringLength(7)]
        [Display(Name = "Código")]
        public string CodigoBreaker { get; set; }

        [StringLength(7)]
        public string Codigo { get; set; }

        [Display(Name = "Fabricante")]
        public int? id_fabricante { get; set; }

        [Display(Name = "Tensión (kV)")]
        public short? id_VoltajeN { get; set; }

        [Display(Name = "Capacidad (A)")]
        public int? Capacidad_breaker { get; set; }

        [StringLength(250)]
        [Display(Name = "Modelo")]
        public string modelo { get; set; }

        public int? id_EAdministrativa_Prov { get; set; }
    }
}
