namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Configuracion")]
    public partial class Configuracion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_Dato { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        public string Dato { get; set; }

        public bool? Personal { get; set; }
    }
}
