namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("conductor")]
    public partial class conductor
    {
        [Key]
        [StringLength(20)]
        public string Codigo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Calibre { get; set; }

        public int? Id_TipoConductor { get; set; }

        public int? Id_material { get; set; }

        public int? Id_Seccion { get; set; }

        public int? Id_recubre { get; set; }

        public int? Id_VoltajeAislamiento { get; set; }

        public short? Area { get; set; }

        public int? NumAccion { get; set; }

        public bool? FibraOptica { get; set; }

        public bool? Protector { get; set; }
    }
}
