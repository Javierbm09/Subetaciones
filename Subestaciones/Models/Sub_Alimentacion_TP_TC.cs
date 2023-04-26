namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_Alimentacion_TP_TC
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        public string Subestacion { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string CodigoEquipo { get; set; }

        public short? ID_EAdministrativa { get; set; }

        public int? numaccion { get; set; }

        [StringLength(1)]
        public string AlimentadoPor { get; set; }

        [StringLength(7)]
        public string CodigoAlimentacion { get; set; }

        [StringLength(5)]
        public string Posicion { get; set; }

        [StringLength(1)]
        public string Fase { get; set; }

        [StringLength(50)]
        public string NroSerie { get; set; }

        public int? id_Plantilla { get; set; }
    }
}
