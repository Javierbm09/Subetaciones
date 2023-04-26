using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Subestaciones.Models.Clases
{
    public class Barras
    {

        public string sub { get; set; }

        [Display(Name = "Nombre de la barra")]
        public string barra { get; set; }

        [Display(Name = "Subestación")]
        public string NombreSub { get; set; }

        [Display(Name = "Conductor")]
        public string cond { get; set; }

        [Display(Name = "Corriente")]
        public float? corr { get; set; }

        [Display(Name = "Cantidad de conductores")]
        public int? cantC { get; set; }

        [Display(Name = "Tensión")]
        public double? tension { get; set; }

       

    }
}