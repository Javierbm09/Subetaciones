using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Subestaciones.Models
{
    [Table("Sub_NomBaterias")]

    public partial class NomencladorBaterias
    {
        [Key]
        [Column(TypeName = "numeric")]
        public decimal IdBateria { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoBateria { get; set; }

        public double CapacidadBateria { get; set; }

        public double TensionBateria { get; set; }

        [StringLength(30)]
        public string ClaseBateria { get; set; }
    }
}
