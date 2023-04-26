namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Material")]
    public partial class Material
    {
        [Key]
        [StringLength(30)]
        public string Tipo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Material { get; set; }

        [StringLength(5)]
        public string Abreviatura { get; set; }
    }
}
