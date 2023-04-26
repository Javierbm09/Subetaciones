namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SubestacionesCabezasLineas
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        public string Codigolinea { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(7)]
        public string SubestacionTransmicion { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        public short? Esquema { get; set; }

        [StringLength(7)]
        public string DesconectivoA { get; set; }

        [StringLength(7)]
        public string DesconectivoB { get; set; }
    }
}
