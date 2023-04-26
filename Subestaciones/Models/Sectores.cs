namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sectores
    {
        [Key]
        [StringLength(1)]
        public string Id_Sector { get; set; }

        [Required]
        [StringLength(15)]
        public string Sector { get; set; }

        [StringLength(15)]
        public string Organismo { get; set; }
    }
}
