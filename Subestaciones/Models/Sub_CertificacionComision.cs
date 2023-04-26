namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_CertificacionComision
    {
        public int? Id_EACertificacion { get; set; }

        public int? NumAccionCertificacion { get; set; }

        [StringLength(200)]
        public string NombreComision { get; set; }

        [StringLength(200)]
        public string CargoComision { get; set; }

        [Key]
        public int Id_CertificacionComision { get; set; }

        public virtual Sub_Certificacion Sub_Certificacion { get; set; }
    }
}
