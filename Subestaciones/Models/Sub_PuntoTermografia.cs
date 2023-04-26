namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_PuntoTermografia
    {
        [Required]
        [StringLength(7)]
        public string Subestacion { get; set; }

        public DateTime fecha { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Punto { get; set; }

        public float? TempDetectada { get; set; }

        public int? estado { get; set; }

        [StringLength(10)]
        public string Fase { get; set; }

        public short? Material { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_EAdministrativa { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumAccion { get; set; }

        [Column(TypeName = "text")]
        public string Comentario { get; set; }

        public short? Id_EAdminDefecto { get; set; }

        [Column(TypeName = "image")]
        public byte[] foto { get; set; }

        public int? NumAccionDefecto { get; set; }

        [StringLength(50)]
        public string elemennto { get; set; }

        [StringLength(50)]
        public string descrpPtoCaleinte { get; set; }

        [Column(TypeName = "image")]
        public byte[] ImagenTermografica { get; set; }

        [NotMapped]
        public int delta { get; set; }
    }
}
