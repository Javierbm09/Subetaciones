namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoMantenimiento")]
    public partial class TipoMantenimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short IdTipoMtto { get; set; }

        [StringLength(150)]
        public string TipoMtto { get; set; }

        public bool? EsTipoMttoSubDist { get; set; }

        [Required]
        [Display(Name = "Tipo de mantenimiento:")]
        public bool? EstipoMttoSubTrans { get; set; }

        public bool? EstipoMttoSubTransTrasfFuerza { get; set; }
    }
}
