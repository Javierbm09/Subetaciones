namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_CertificacionAspectos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short NroAspecto { get; set; }

        [StringLength(100)]
        public string NombreAspecto { get; set; }
    }
}
