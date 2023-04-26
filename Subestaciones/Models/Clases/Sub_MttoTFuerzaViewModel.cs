using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Sub_MttoTFuerzaViewModel
    {
        public Sub_MttoTFuerza Sub_MTF { get; set; }

        public DatosChapaTFuerzaViewModel DatosCTF { get; set; }

        public Sub_MttoTFuerzaAuxiliares Sub_MTFA { get; set; }

        public List<Sub_MttoTFuerzaAislamEnrollado> Sub_MTFAE { get; set; }

        public Sub_MttoTFuerzaTanDeltaEnrollado Sub_MTFTDE { get; set; }
        
        public Sub_MttoTFuerzaPresionBushing Sub_MTFPB { get; set; }

        public Sub_MttoTFuerzaTanDeltaBushings Sub_MTFTDB { get; set; }

        public List<Sub_MttoTFuerzaAislamBushings> Sub_MTFAB { get; set; }

        public List<Sub_MttoTFuerzaRelacTransf> Sub_MTFRT { get; set; }

        public List<Sub_MttoTFuerzaResistOhm> Sub_MTFRO { get; set; }

        public Sub_MttoTFuerzaCorrienteExit Sub_MTFCE { get; set; }

        [NotMapped]
        public double? VoltajeP { get; set; }

        [NotMapped]
        public double? VoltajeS { get; set; }

        [NotMapped]
        public double? VoltajeT { get; set; }

    }
}