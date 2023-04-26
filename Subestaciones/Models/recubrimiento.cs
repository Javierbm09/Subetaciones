namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("recubrimiento")]
    public partial class recubrimiento
    {
        [Key]
        [StringLength(30)]
        public string Tipo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_recubre { get; set; }

        [StringLength(4)]
        public string Cartel { get; set; }
    }
}
