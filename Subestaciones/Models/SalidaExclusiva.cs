namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalidaExclusiva")]
    public partial class SalidaExclusiva
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_EAdministrativa { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumAccion { get; set; }

        [StringLength(25)]
        public string Cliente { get; set; }

        [StringLength(1)]
        public string Sector { get; set; }

        [StringLength(7)]
        public string Codigo { get; set; }

        [StringLength(15)]
        public string DesconectivoCliente { get; set; }

        public short? CorrienteNominal { get; set; }

        public short? CorrienteOperacion { get; set; }

        [StringLength(15)]
        public string EstadoDesconectivo { get; set; }

        [StringLength(15)]
        public string EstadoCliente { get; set; }
    }
}
