namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VoltajesNominalesPararrayos
    {
        [Key]
        public short Id_VoltNomPararrayo { get; set; }

        public double VoltNomPararrayo { get; set; }
    }
}
