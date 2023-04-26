using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Sub_Celaje_Sub_TransViewModel
    {

        public string NombreSub { get; set; }

        
        [Column(Order = 0)]
        [StringLength(30)]
        [Display(Name = "Tipo de Inspección")]
        [Required]
        public string NombreCelaje { get; set; }

        
        [Column(Order = 1)]
        [StringLength(7)]
        [Display(Name = "Subestación")]
        [Required]
        public string CodigoSub { get; set; }

        
        [Column(Order = 2, TypeName = "smalldatetime")]
        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }

        [Required]
        public string RealizadoPor { get; set; }

        public int NumAccion { get; set; }

        public short id_EAdministrativa { get; set; }        


    }
}