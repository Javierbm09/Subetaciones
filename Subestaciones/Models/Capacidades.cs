namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Capacidades
    {
        [Key]
        public short Id_Capacidad { get; set; }

        [Display(Name = "Capacidad")]
        public double Capacidad { get; set; }

        public bool Monofasico { get; set; }

        public bool Trifasico { get; set; }

        public bool PreferidoBancos { get; set; }

        public bool PreferidosSubDist { get; set; }

        public bool PreferidoSubTrans { get; set; }

        public double? PerdidasVacioMonof { get; set; }

        public double? PerdidasVacioTrifas { get; set; }
    }
}
