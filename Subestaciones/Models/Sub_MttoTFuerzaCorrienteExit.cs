namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoTFuerzaCorrienteExit
    {
        public int Id_MttoTFuerza { get; set; }

        [Key]
        public int Id_CorrienteExit { get; set; }

        [Display(Name = "Tap:")]
        public int? Tap { get; set; }

        [Display(Name = "A-0:")]
        [Column("A-0")]
        public double A_0 { get; set; }

        [Display(Name = "B-0:")]
        [Column("B-0")]
        public double B_0 { get; set; }

        [Display(Name = "C-0:")]
        [Column("C-0")]
        public double C_0 { get; set; }

        [Display(Name = "Porciento Desviacion:")]
        public double? PorcientoDesviacion { get; set; }
    }
}
