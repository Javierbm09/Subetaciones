namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoTFuerzaResistOhm
    {
        [Key]
        public int Id_ResistOhmica { get; set; }

        public int Id_MttoTFuerza { get; set; }

        public int? NroDeriv { get; set; }

        [StringLength(50)]
        public string NivelVoltaje { get; set; }

        public double? FaseAAntes { get; set; }

        public double? FaseADespues { get; set; }

        public double? FaseBAntes { get; set; }

        public double? FaseBDespues { get; set; }

        public double? FaseCAntes { get; set; }

        public double? FaseCDespues { get; set; }

        public double? PorcientoDesviacion { get; set; }
    }
}
