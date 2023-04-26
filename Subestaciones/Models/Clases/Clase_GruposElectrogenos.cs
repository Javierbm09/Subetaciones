using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Clase_GruposElectrogenos
    {
        public string Subestacion { get; set; }
        public string Cod { get; set; }
        public string Cod_Emergencia { get; set; }
        public double FactorPot { get; set; }
        public string Fabr { get; set; }
        public double ? Pot { get; set; }
        public int Velocidad { get; set; }
        public double Tension { get; set; }
        public double ? FactorCarga { get; set; }
    }
}

