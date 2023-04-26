namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CircuitosSubtransmision")]
    public partial class CircuitosSubtransmision
    {
        [Key]
        [StringLength(7)]
        public string CodigoCircuito { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int id_EAdministrativa_Prov { get; set; }

        public int? NumAccion { get; set; }

        public short? VoltajeNominal { get; set; }

        [StringLength(7)]
        public string SubestacionTransmision { get; set; }

        [StringLength(7)]
        public string DesconectivoSalida { get; set; }

        [StringLength(20)]
        public string NombreCircuito { get; set; }

        public double? Kmslineas { get; set; }

        [StringLength(1)]
        public string EstadoOperativo { get; set; }

        public double? Kmsdeclarados { get; set; }

        public bool? Soterrado { get; set; }
    }
}
