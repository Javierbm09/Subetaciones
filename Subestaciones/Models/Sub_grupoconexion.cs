namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_grupoconexion
    {
        [Key]
        public short id_tipo { get; set; }

        [Required]
        [StringLength(30)]
        public string tipo { get; set; }
    }
}
