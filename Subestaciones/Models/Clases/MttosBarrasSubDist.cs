using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class MttosBarrasSubDist
    {
        public string CodigoSub { get; set; }

        [Display(Name = "Nombre de la barra")]
        public string CodigoBarra { get; set; }

        [Display(Name = "Tensión")]
        public double? voltaje { get; set; }

        [Display(Name = "Conductor")]
        public string Conductor { get; set; }

        public DateTime Fecha { get; set; }

        public string EstadoBarra { get; set; }

        [StringLength(20)]
        public string Conexiones { get; set; }

        [StringLength(20)]
        public string EstadoPuentes { get; set; }

    }
}