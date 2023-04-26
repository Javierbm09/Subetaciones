using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class InstrumentoViewModel
    {
        [StringLength(100)]
        [Display(Name = "Instrumento")]
        public string Instrumento { get; set; }

        [StringLength(50)]
        [Display(Name = "Modelo o Tipo")]
        public string ModeloTipo { get; set; }
    }
}