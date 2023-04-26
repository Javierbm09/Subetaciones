namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoTFuerzaTanDeltaEnrollado
    {
        public int Id_MttoTFuerza { get; set; }

        [Key]
        public int Id_TanDeltaEnrollados { get; set; }
       
        [Display(Name = "Seccion:")]
        [StringLength(50)]
        public string Seccion { get; set; }
        
        [Display(Name = "Esquema:")]
        [StringLength(50)]
        public string Esquema { get; set; }
        
        [Display(Name = "Capacitancia:")]
        public double? Capacitancia { get; set; }

        [Display(Name = "Tangente Delta:")]
        public double? TangenteDelta { get; set; }
    }
}
