namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoDistBarra
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        public string CodigoSub { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Fecha { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string CodigoBarra { get; set; }

        [StringLength(20)]
        public string EstadoBarra { get; set; }

        [StringLength(20)]
        public string Conexiones { get; set; }

        [StringLength(20)]
        public string EstadoPuentes { get; set; }
    }
}
