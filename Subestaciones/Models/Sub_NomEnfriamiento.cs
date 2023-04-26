namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_NomEnfriamiento
    {
        [Key]
        public int Codigo { get; set; }

        [StringLength(30)]
        public string TipoEnfriamiento { get; set; }
    }
}
