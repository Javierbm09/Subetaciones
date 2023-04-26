namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_NomTipoMedicion
    {
        [Key]
        public short Id_TipoMedicion { get; set; }

        [StringLength(200)]
        public string NombreTipoMedicion { get; set; }
    }
}
