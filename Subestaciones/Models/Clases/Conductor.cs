using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Subestaciones.Models.Clases
{
    public class Conductor
    {
        [Display(Name = "Código")]
        public string codigo { get; set; }
        [Display(Name = "Tipo conductor")]
        public string TCond { get; set; }
        [Display(Name = "Material")]
        public string material { get; set; }
        [Display(Name = "Calibre")]
        public string calibre { get; set; }
        [Display(Name = "Recubrimiento")]
        public string recubrimiento { get; set; }
      
    }
}