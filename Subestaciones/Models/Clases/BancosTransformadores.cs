using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class BancosTransformadores
    {
        [StringLength(7)]
        [Display(Name = "Banco:")]
        public string Codigo { get; set; }
        public string Seccionalizador { get; set; }

        [StringLength(7)]
        public string Circuito { get; set; }
    }
}