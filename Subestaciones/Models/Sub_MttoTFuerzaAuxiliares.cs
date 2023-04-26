namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoTFuerzaAuxiliares
    {
        public int Id { get; set; }

        public int Id_MttoTFuerza { get; set; }

        [Display(Name = "Porciento humedad relativa:")]
        public double? EnrrAislHumRelativa { get; set; }

        [Display(Name = "Temp. Aceite:")]
        public double? EnrrAislTempAceite { get; set; }

        [Display(Name = "Temp. Ambiente:")]
        public double? EnrrAislTempAmbiente { get; set; }

        [Display(Name = "Instrumento utilizado:")]
        public int? EnrrAislInstUtilizado { get; set; }

        [Display(Name = "Realizado por:")]
        public int? EnrrAislRealizadoPor { get; set; }

        [Display(Name = "Revisado por:")]
        public int? EnrrAislRevisadoPor { get; set; }

        [Display(Name = "Porciento humedad relativa:")]
        public double? EnrrTDHumRelativa { get; set; }

        [Display(Name = "Temp. Aceite:")]
        public double? EnrrTDTempAceite { get; set; }

        [Display(Name = "Temp. Ambiente:")]
        public double? EnrrTDTempAmbiente { get; set; }

        [Display(Name = "Instrumento utilizado:")]
        public double? EnrrTDInstUtilizado { get; set; }
        
        [Display(Name = "Realizado por:")]
        public int? EnrrTDRealizadoPor { get; set; }

        [Display(Name = "Revisado por:")]
        public int? EnrrTDRevisadoPor { get; set; }

        [Display(Name = "Porciento humedad relativa:")]
        public double? BushAislHumRelativa { get; set; }

        [Display(Name = "Temp. Equipo:")]
        public double? BushAislTempEquipo { get; set; }

        [Display(Name = "Temp. Ambiente:")]
        public double? BushAislTempAmbiente { get; set; }

        [Display(Name = "Instrumento utilizado:")]
        public int? BushAislInstUtilizado { get; set; }

        [Display(Name = "Porciento humedad relativa:")]
        public double? BushTDHumRelativa { get; set; }

        [Display(Name = "Temp. Equipo:")]
        public double? BushTDTempEquipo { get; set; }

        [Display(Name = "Temp. Ambiente:")]
        public double? BushTDTempAmbiente { get; set; }

        [Display(Name = "Instrumento utilizado:")]
        public int? BushTDInstUtilizado { get; set; }
    }
}
