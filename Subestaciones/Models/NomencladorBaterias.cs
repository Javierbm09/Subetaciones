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
        public int IdBateria { get; set; }

        [StringLength(20)]
        [Display(Name = "Modelo de Baterías"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string TipoBateria { get; set; }

        [Display(Name = "Capacidad")]
        public double CapacidadBateria { get; set; }

        [Display(Name = "Tensión Nominal")]
        public double TensionBateria { get; set; }

        [StringLength(30)]
        [Display(Name = "Tipo")]
        public string ClaseBateria { get; set; }
    }
}
