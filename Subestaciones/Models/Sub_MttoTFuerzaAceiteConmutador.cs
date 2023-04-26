namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoTFuerzaAceiteConmutador
    {
        public int Id_MttoTFuerza { get; set; }

        [Key]
        public int Id_MttoMedAceite { get; set; }

        public double? Prueba1 { get; set; }

        public double? Prueba2 { get; set; }

        public double? Prueba3 { get; set; }

        public double? Prueba4 { get; set; }

        public double? Prueba5 { get; set; }

        public double? Prueba6 { get; set; }

        public double? Promedio { get; set; }

        public double? TempAmbiente { get; set; }

        public double? TempTransformador { get; set; }

        public double? HumRelativa { get; set; }

        [StringLength(53)]
        public string NormaUtilizada { get; set; }

        public int? Id_Instrumento { get; set; }
    }
}
