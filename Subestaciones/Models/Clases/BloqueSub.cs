using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class BloqueSub
    {
     
        public string Codigo { get; set; }

        public int Id_bloque { get; set; }

        public string tipobloque { get; set; }

        public short? idEsquemaPorBaja { get; set; }

        public string EsquemaPorBaja { get; set; }

        public short? idVoltajeSecundario { get; set; }

        public double? VoltajeSecundario { get; set; }

        public short? idVoltajeTerciario { get; set; }
        
        public double? VoltajeTerciario { get; set; }

        public string TipoSalida { get; set; }

        public bool? Priorizado { get; set; }

        public string idSector { get; set; }

        public string Sector { get; set; }

        public string Cliente { get; set; }

    }
}