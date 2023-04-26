namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class materialpostes
    {
        [Key]
        public short Id_Material { get; set; }

        [StringLength(20)]
        public string Material { get; set; }
    }
}
