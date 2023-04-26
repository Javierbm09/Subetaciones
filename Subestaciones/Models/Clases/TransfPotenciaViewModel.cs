using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class TransfPotenciaViewModel
    {
        public TransformadoresSubtransmision transformador { get; set; }
        public Bloque bloques { get; set; }
        public EsquemasBaja esquema { get; set; }
        public Sectores sector  { get; set; }
        public SalidaExclusivaSub cliente  { get; set; }
        public VoltajesSistemas tensionSecundario  { get; set; }
        public VoltajesSistemas tensionTerciaria  { get; set; }
    }
}