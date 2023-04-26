namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoTFuerza
    {
        [Key]
        public int Id_MttoTFuerza { get; set; }

        public int Id_TFuerza { get; set; }

        public int Id_EAdministrativaTFuerza { get; set; }

        [Required]
        [StringLength(7)]
        [Display(Name = "Subestación:")]
        public string Subestacion { get; set; }

        public int Num_Accion { get; set; }

        [Required]
        [Display(Name = "Revisado Por:")]
        public short? RevisadoPor { get; set; }

        [Required]
        [Display(Name = "Fecha:")]
        public DateTime? Fecha { get; set; }

        [Required]
        [Display(Name = "Tipo de mantenimiento:")]
        public int? tipoMantenimiento { get; set; }

        [Display(Name = "Mantenido:")]
        public bool? Mantenido { get; set; }

        public short? Id_EAdministrativa { get; set; }

        [NotMapped]
        public string MensajeExistenciaMtto { get; set; }
    }
}
