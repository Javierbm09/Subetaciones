namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Fotos
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_EAdministrativa { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumAccion { get; set; }

        [Required]
        [StringLength(7)]
        public string Instalacion { get; set; }

        public short Version { get; set; }

        [StringLength(20)]
        public string Nombre { get; set; }

        [Column(TypeName = "image")]
        public byte[] Foto { get; set; }

        public short tipo { get; set; }
    }
}
