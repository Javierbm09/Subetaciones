using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Inspecciones
    {
        public string TipoInspeccion { get; set; }

        public string RealizadoPor { get; set; }

        public DateTime Fecha { get; set; }
    }
}