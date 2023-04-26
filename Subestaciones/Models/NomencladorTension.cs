namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sub_VoltajeRedCD")]

    public partial class NomencladorTension
    {
        [Key]
        public short idTension { get; set; }

        public double tension { get; set; }
    }
}
