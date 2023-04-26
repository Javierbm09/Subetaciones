namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EsquemasBaja")]
    public partial class EsquemasBaja
    {
        [Key]
        public short Id_EsquemaPorBaja { get; set; }

        [StringLength(50)]
        [Display(Name = "Esquema por baja")]
        public string EsquemaPorBaja { get; set; }

        [StringLength(70)]
        public string CodigoMostrado { get; set; }
    }
}
