using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class BreakerPorBaja
    {
        public string CodigoDesconectivo { get; set; }
        public double? TensionNominal { get; set; }
        public double? CorrienteNominal { get; set; }
    }
}