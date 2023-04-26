using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Sub_InspAspectosSubTransViewModel
    {
       
        [Column(Order = 0)]
        [StringLength(30)]
        [Display(Name = "Tipo de Inspección")]
        public string NombreCelaje { get; set; }

        
        [Column(Order = 1)]
        [StringLength(7)]
        [Display(Name = "Subestación")]
        public string CodigoSub { get; set; }

        
        [Column(Order = 2, TypeName = "smalldatetime")]
        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }


        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Aspecto { get; set; }

        [StringLength(50)]
        public string Defecto { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        [StringLength(50)]
        public string NombreAspecto { get; set; }

        public string RealizadoPor { get; set; }

    }
}