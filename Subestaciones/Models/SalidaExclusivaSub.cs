namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalidaExclusivaSub")]
    public partial class SalidaExclusivaSub
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        public string Codigo { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short id_bloque { get; set; }

        public int Id_EAdministrativa { get; set; }

        public int NumAccion { get; set; }

        public short? Ruta { get; set; }

        public short? Folio { get; set; }

        [StringLength(15)]
        public string DesconectivoCliente { get; set; }

        public short? CorrienteNominal { get; set; }

        public short? CorrienteOperacion { get; set; }

        [StringLength(15)]
        public string EstadoDesconectivo { get; set; }

        [StringLength(15)]
        public string EstadoCliente { get; set; }

        [StringLength(10)]
        public string CalibreParrillaA { get; set; }

        [StringLength(10)]
        public string CalibreParrillaB { get; set; }

        [StringLength(10)]
        public string CalibreParrillaC { get; set; }

        [StringLength(10)]
        public string CalibreParrillaNeutro { get; set; }

        [StringLength(25)]
        public string Cliente { get; set; }

        [StringLength(1)]
        public string Sector { get; set; }
    }
}
