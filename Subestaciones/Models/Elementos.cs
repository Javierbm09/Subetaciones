namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Elementos
    {
        [Required]
        [StringLength(255)]
        public string Elemento { get; set; }

        [Key]
        public short Id_Elemento { get; set; }

        [StringLength(1)]
        public string Tipo { get; set; }
    }
}
