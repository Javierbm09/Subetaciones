using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class MttosPararrayosSubDist
    {

        
        public string CodigoSub { get; set; }

       
        public DateTime Fecha { get; set; }

       
        public int Id_AdminPararrayo { get; set; }

        [Display(Name = "Estado pararrayo")]
        public int Id_Pararrayo { get; set; }

        public string Estado { get; set; }
        
        [Display(Name = "Aislamiento(GΩ)")]
        public double? Aislamiento { get; set; }

        [Display(Name = "Aterramiento")]
        public string EstAterramiento { get; set; }

        [Display(Name = "Número Serie")]
        public string serie { get; set; }

        public string Fase { get; set; }

        [Display(Name = "Tensión Nominal")]
        public double? tension { get; set; }

        [Display(Name = "Corriente Nominal")]
        public double? corr { get; set; }

        [Display(Name = "Fabricante")]
        public string fab { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Display(Name = "Código")]
        public string Codigo { get; set; }
    }
}