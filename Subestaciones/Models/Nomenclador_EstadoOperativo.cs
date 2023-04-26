namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Nomenclador_EstadoOperativo
    {
        [Key]
        [StringLength(1)]
        public string Id_EstadoOperativo { get; set; }

        [Required]
        [StringLength(30)]
        public string EstadoOperativo { get; set; }
    }
}
