namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoTFuerzaAislamEnrollado
    {
        public int Id_MttoTFuerza { get; set; }

        [Key]
        public int Id_MttoAislamEnroll { get; set; }

        [StringLength(30)]
        public string TipoTransformador { get; set; }

        [StringLength(50)]
        public string LugarMedicion { get; set; }

        public double? Med15Seg { get; set; }

        public double? Med60Seg { get; set; }

        public double? Med600Seg { get; set; }

        public double? CoefAbsorcion { get; set; }

        [StringLength(50)]
        public string EstadoCA { get; set; }

        public double? IndicePolarizacion { get; set; }

        [StringLength(50)]
        public string EstadoIP { get; set; }
    }
}
