using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subestaciones.Models.Clases
{
    public class Capacitores
    {
        public string codigosub { get; set; } //se corresponde con el circuito 
        [Display(Name = "Subestación")]
        public string NombreSub { get; set; }
        [Display(Name = "Código del banco")]
        public string codigobanco { get; set; }
        [Display(Name = "Código antiguo")]
        public string codigoantiguo { get; set; }
        [Display(Name = "Seccionalizador")]
        public string secc { get; set; }
        [Display(Name = "Estado operativo")]
        public string EO { get; set; }
        [Display(Name = "Tipo de control")]
        public string tipocontrol { get; set; }
        [Display(Name = "CKVAR Instalado")]
        public double? CKVAR_Instalado { get; set; }
        [StringLength(20)]
        public string Calle { get; set; }
        [StringLength(6)]
        public string Numero { get; set; }
        [StringLength(20)]
        public string Entrecalle1 { get; set; }
        [StringLength(20)]
        public string Entrecalle2 { get; set; }
        [StringLength(25)]
        public string BarrioPueblo { get; set; }
        public short Sucursal { get; set; }
    }
}