using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class DatosChapaTFuerzaViewModel
    {
        [Required(ErrorMessage = "Debe introducir el campo: Transformador de fuerza")]
        public int Id_Transformador { get; set; }

        public int Id_EAdministrativa { get; set; }

        [Display(Name = "Transformador de fuerza:")]
        public string Nombre { get; set; }

        [Display(Name = "Nro Serie:")]
        public string NoSerie { get; set; }

        [Display(Name = "Capacidad:")]
        public double? Capacidad { get; set; }

        [Display(Name = "Nro Empresa:")]
        public string Numemp { get; set; }

        [Display(Name = "Tensión Primaria:")]
        public double? VoltajeP { get; set; }

        [Display(Name = "Tensión Secundaria:")]
        public double? VoltajeS { get; set; }

        [Display(Name = "Tensión Terciaria:")]
        public double? VoltajeT { get; set; }

        public short? NroPosiciones { get; set; }

    }
}