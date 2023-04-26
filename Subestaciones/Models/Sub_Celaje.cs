namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Linq;

    public partial class Sub_Celaje
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        [Display(Name = "Tipo de Inspección")]
        [Required]
        public string NombreCelaje { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(7)]
        [Display(Name = "Subestación")]
        [Required]
        public string CodigoSub { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        [Display(Name = "Fecha")]
        public DateTime fecha { get; set; }

        [Required]
        public short? RealizadoPor { get; set; }

        public int NumAccion { get; set; }

        public short id_EAdministrativa { get; set; }
    }
}
