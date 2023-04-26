using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class CertificacionSubDistViewModel
    {
        public Sub_Certificacion DCertificaciones { get; set; }


        public List<Sub_CertificacionComision> Comision { get; set; }

        public List<Sub_CertificacionDetalles> Detalles { get; set; }


    }
}