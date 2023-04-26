using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Termografias
    {
        public DateTime? Fecha { get; set; }

        public int? TempAmbiente { get; set; }

        public int? TransfLinea { get; set; }

        public int? puntos { get; set; }

        public string sub { get; set; }

        public string subNomb { get; set; }

        public string EjecutadoPor { get; set; }

        public string Ejecutado { get; set; }

        public string Elemento { get; set; }

        public string  Designacion { get; set; }

        public int idEA { get; set; }

        public int numA { get; set; }

        public byte[] Foto { get; set; }

    }
}