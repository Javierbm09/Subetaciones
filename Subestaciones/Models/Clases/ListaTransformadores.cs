using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class ListaTransformadores
    {
        public string Codigo { get; set; }
        public string Seleccionalizador { get; set; }
        public string Numemp { get; set; }
        public double? Capacidad { get; set; }
        public string Fabricante { get; set; }
        public double? VoltajePrim { get; set; }
        public double? VoltajeSecun { get; set; }
        public string Fase { get; set; }
        public string EstadoOperativo { get; set; }
        public short? TapDejado { get; set; }
        public short? NumFase { get; set; }
    }
}