namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seccion")]
    public partial class Seccion
    {
        [Key]
        [StringLength(10)]
        public string Cartel { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id_Seccion { get; set; }

        [StringLength(4)]
        public string Galga { get; set; }

        [StringLength(10)]
        public string Calibre { get; set; }

        [Column("Seccion")]
        [StringLength(10)]
        public string Seccion1 { get; set; }
    }
}
