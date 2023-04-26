namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoTFuerzaAislamBushings
    {
        public int Id_MttoTFuerza { get; set; }

        [Key]
        public int Id_AislamBushings { get; set; }

        [StringLength(50)]
        public string TipoBushing { get; set; }

        public double? FaseA_R15 { get; set; }

        public double? FaseA_R60 { get; set; }

        public double? FaseA_K { get; set; }

        [StringLength(50)]
        public string FaseA_Estado { get; set; }

        public double? FaseB_R15 { get; set; }

        public double? FaseB_R60 { get; set; }

        public double? FaseB_K { get; set; }

        [StringLength(50)]
        public string FaseB_Estado { get; set; }

        public double? FaseC_R15 { get; set; }

        public double? FaseC_R60 { get; set; }

        public double? FaseC_K { get; set; }

        [StringLength(50)]
        public string FaseC_Estado { get; set; }
    }
}
