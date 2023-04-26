namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ES_Conexion_Rele_TC_TP
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string rele { get; set; }

        [Key]
        [Column("TC/TP", Order = 1)]
        [StringLength(50)]
        public string TC_TP { get; set; }

        [Required]
        [StringLength(2)]
        public string Tipo_Equipo { get; set; }

        public short Conexion { get; set; }

        public short Devanado { get; set; }

        public int esquema { get; set; }
    }
}
