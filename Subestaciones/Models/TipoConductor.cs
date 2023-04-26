namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoConductor")]
    public partial class TipoConductor
    {
        [Required]
        [StringLength(30)]
        public string Tipo { get; set; }

        [Key]
        public short Id_Tipo { get; set; }

        [StringLength(5)]
        public string Cartel { get; set; }
    }
}
