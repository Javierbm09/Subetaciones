using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Subestaciones.Models
{
    [Table("Sub_NomCargadores")]    

    public partial class Cargadores    
    {
        [Key]
        public short IdCargador  { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoCargador { get; set; }

        public double VoltajeCorrienteDirecta { get; set; }

        public double Corriente { get; set; }

        public double? VoltajeCA { get; set; }
    }
}
