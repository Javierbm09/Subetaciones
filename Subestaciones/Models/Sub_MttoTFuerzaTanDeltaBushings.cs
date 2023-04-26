namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoTFuerzaTanDeltaBushings
    {
        [Key]
        public int Id_TanDeltaBushing { get; set; }

        public int Id_MttoTFuerza { get; set; }

        [StringLength(50)]
        [Display(Name = "Seccion:")]
        public string Seccion { get; set; }

        [StringLength(50)]
        [Display(Name = "Esquema:")]
        public string Esquema { get; set; }

        [Display(Name = "Capacitancia:")]
        public double? Capacitancia { get; set; }

        [Display(Name = "Tangente Delta:")]
        public double? TanDelta { get; set; }
    }
}
