using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Subestaciones.Models.Clases
{
    public class SubestacionesDistribucion
    {
        [Display(Name ="Código")]
        public string Codigo { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        [Display(Name = "Tipo Subestación")]
        public string TipoSubestacion { get; set; }

        [Display(Name = "Esquema por Alta")]
        public string EsquemaAlta { get; set; }

        [Display(Name = "Tipo Tercero")]
        public bool? TipoTercero { get; set; }

        [Display(Name = "Tensión Nominal Alta")]
        public double? VoltajeNominal { get; set; }

        [Display(Name = "Código Antiguo")]
        public string CodigoAntiguo { get; set; }

        [Display(Name = "Nombre Subestación")]
        public string NombreSubestacion { get; set; }

        [Display(Name = "Cantidad Salidas")]
        public short? NumeroSalidas { get; set; }

        [Display(Name = "Estado Operativo")]
        public string EstadoOperativo { get; set; }

        [Display(Name = "Calle")]
        public string Calle { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Entre")]
        public string Entrecalle1 { get; set; }

        [Display(Name = "Y")]
        public string Entrecalle2 { get; set; }

        [Display(Name = "Lugar Habitado")]
        public string BarrioPueblo { get; set; }

        [Display(Name = "Sucursal")]
        public short Sucursal { get; set; }

        [Display(Name = "Largo")]
        public double? Largo { get; set; }

        [Display(Name = "Ancho")]
        public double? Ancho { get; set; }

        [Display(Name = "Latitud")]
        public double? Latitud { get; set; }

        [Display(Name = "Longitud")]
        public double? Longitud { get; set; }

        [Display(Name = "Fecha Puesta en Marcha")]
        public DateTime? FechaPuestaMarcha { get; set; }


    }

}