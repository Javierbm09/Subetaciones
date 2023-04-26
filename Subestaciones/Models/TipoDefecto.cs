namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoDefecto")]
    public partial class TipoDefecto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_Defecto { get; set; }

        [StringLength(50)]
        public string Defecto { get; set; }
    }
}
