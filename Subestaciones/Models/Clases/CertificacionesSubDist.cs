using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class CertificacionesSubDist
    {
        public int? Id_EAdministrativa { get; set; }
    
        public int? NumAccion { get; set; }

        public string Observaciones { get; set; }

        public string CodigoSub { get; set; }

        public string NombreSub { get; set; }

        public DateTime? Fecha { get; set; }

        public string UEB { get; set; }

    }
}