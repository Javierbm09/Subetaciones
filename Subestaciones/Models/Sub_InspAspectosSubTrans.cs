namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_InspAspectosSubTrans
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string NombreCelaje { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(7)]
        public string CodigoSub { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime fecha { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Aspecto { get; set; }

        [StringLength(50)]
        public string Defecto { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }
    }
}
