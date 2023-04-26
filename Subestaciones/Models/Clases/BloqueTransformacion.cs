using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class BloqueTransformacion
    {
        public string Codigo { get; set; }


        [Display(Name = "id_bloque")]
        public int Id_bloque { get; set; }

        [Display(Name = "Tipo Bloque")]
        public string tipobloque { get; set; }

        [Display(Name = "Esquema por Baja")]
        public string EsquemaPorBaja { get; set; }

        [Display(Name = "Tensión Salida")]
        public double? VoltajeSalida { get; set; }

        [Display(Name = "Tensión Terciario")]
        public double? VoltajeTerciario { get; set; }

        [Display(Name = "Tipo Salida")]
        public string TipoSalida { get; set; }

        [Display(Name = "Sector Cliente")]
        public string Sector { get; set; }

        [Display(Name = "Cliente")]
        public string Cliente { get; set; }

        public bool? Priorizado { get; set; }

    }
}