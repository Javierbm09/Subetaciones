namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_NomCargadores
    {
        [Key]
        public short IdCargador { get; set; }

        [Required]
        [StringLength(20)]
        [Display (Name = "Tipo de Cargador")]
        public string TipoCargador { get; set; }

        [Display(Name = "Tensión CD")]
        public double VoltajeCorrienteDirecta { get; set; }

        [Display(Name = "Corriente")]
        public double Corriente { get; set; }

        [Display(Name = "Tensión CA")]
        public double? VoltajeCA { get; set; }
    }
}
