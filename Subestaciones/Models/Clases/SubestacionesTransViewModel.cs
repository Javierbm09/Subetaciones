using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class SubestacionesTransViewModel
    {
        public SubestacionesTransmision TSubestaciones { get; set; }
        public List<SubestacionesCabezasLineas> Lineas { get; set; }
        public List<Circuitos_Baja> CircuitosXBaja { get; set; }
        public List<Bloque> Bloques_Transformacion { get; set; }
        public List<TransformadorTransmision> TransformadoresT { get; set; }
        public List<Emplazamiento_Sigere> CentralesElectricas { get; set; }
    }
}