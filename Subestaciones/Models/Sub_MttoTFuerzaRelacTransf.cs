namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoTFuerzaRelacTransf
    {
        public int Id_MttoTFuerza { get; set; }

        [Key]
        public int Id_RelacTransf { get; set; }

        public int? NroDerivacion { get; set; }

        public double? FaseAAntes { get; set; }

        public double? FaseADespues { get; set; }

        public double? FaseBAntes { get; set; }

        public double? FaseBDespues { get; set; }

        public double? FaseCAntes { get; set; }

        public double? FaseCDespues { get; set; }

        public double? ValorTeorico { get; set; }

        public double? PorcDesviacion { get; set; }
    }
}
