using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subestaciones.Models
{
    [Table("GruposVoltaje")]
    public partial class GruposVoltaje
    {
        [Key]
        public int id_grupovoltaje { get; set; }

        
        [Display(Name = "Tensión")]
        public double TPrimaria1 { get; set; }

        public double? TPrimaria2 { get; set; }

        public double TSecundaria1 { get; set; }

        public double? Tsecundaria2 { get; set; }

        public bool? monofasico { get; set; }

        public bool? trifasico { get; set; }
    }
}