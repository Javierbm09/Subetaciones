using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Perdidas
    {
        public string Circuito { get; set; }
        public double? eEntregadaADistrib { get; set; }
        public double? transfEntregadaEnDistrib { get; set; }
        public double? eTotalDisponibleEnCto { get; set; }
        public double? factClientesPorCto { get; set; }
        public double? factSectorResidencial { get; set; }
        public double? factSectorEstatalMayor { get; set; }
        public double? factSectorEstatalMenor { get; set; }
        public double? consumoEmpresa { get; set; }
        public double? PerdidasTecnicas { get; set; }
        public double? pCtoDistribucion { get; set; }
        public double? pPorcPCtoDistribucion { get; set; }
        

    }
}