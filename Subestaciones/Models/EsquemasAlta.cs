namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EsquemasAlta")]
    public partial class EsquemasAlta
    {
        [Key]
        public short Id_EsquemaAlta { get; set; }

        [StringLength(50)]
        public string EsquemaPorAlta { get; set; }

        [StringLength(100)]
        public string CodigoMostrado { get; set; }
    }
}
