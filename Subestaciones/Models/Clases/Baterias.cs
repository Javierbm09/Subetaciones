using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Baterias
    {
        [Key]
        public int Id_Bateria { get; set; }

        [Display(Name = "Modelo")]
        [StringLength(10)]
        public string modelo { get; set; }

        public int? Tipo { get; set; }

        [Display(Name = "Tipo Batería")]
        public string TipoBat { get; set; }

        [Display(Name = "Capacidad")]
        public double? Capacidad { get; set; }

        [Display(Name = "Tensión")]
        public double? Tension { get; set; }

        public int id_EAdministrativa { get; set; }

        public int NumAccion { get; set; }

        [Display(Name = "Cantidad de Unidades")]
        public short? CantidadVasos { get; set; }

        [Display(Name="Tensión/Unidad")]
        public double? Voltaje_Vasos { get; set; }

        [Display(Name = "Densidad")]
        public float? Densisdad { get; set; }

        public int Id_ServicioCDBat { get; set; }

        [Display(Name = "Peso de cada Unidad")]
        public double? PesoCadaVaso { get; set; }

        [Display(Name = "Voltaje Flotación")]
        public double? VoltajeFlotacion { get; set; }

        [Display(Name = "Cant. Electrolitos/Unidad")]
        public int? CantElectVasos { get; set; }

        [Display(Name = "Cant. Electrolitos")]
        public int? CantElect { get; set; }

        [Display(Name = "Cant. Unidades Pilotos")]
        public int? CantVasosPilotos { get; set; }

        [Display(Name = "Mes")]
        public int? MesFab { get; set; }

        [Display(Name = "Año Fab")]
        public int? AnnoFab { get; set; }

        [Display(Name = "Fabricante")]
        public string Fabricante { get; set; }

        [Display(Name = "Voltaje Ecualizante (V)")]
        public double? VoltajeEcualizanteCargaBat { get; set; }

        [Display(Name = "Tiempo (h)")]
        public double? TiempoCargaBat { get; set; }

        [Display(Name = "Periodicidad (Meses)")]
        public int? PeriodicidadCargaBat { get; set; }

        [Display(Name = "Fecha de Instalado")]
        public DateTime? Instalado { get; set; }
    }
}