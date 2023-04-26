using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Mttos
    {
        public string Nombre { get; set; }

        public string Codigo { get; set; }

        public string RealizadoPor { get; set; }

        public DateTime? Fecha { get; set; }

        public double? VoltajePrimario { get; set; }

        public double? VoltajeSecundario { get; set; }

        public double? CapacidadVentilador { get; set; }

        public double? AguaDestilacion { get; set; }

        public string NivelAceite { get; set; }
    }
}