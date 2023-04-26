namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_LineasSubestacion
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        public string Subestacion { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(7)]
        public string Circuito { get; set; }

        [StringLength(7)]
        [Required(ErrorMessage ="Campo obligatorio")]
        public string Seccionalizador { get; set; }
    }
}
