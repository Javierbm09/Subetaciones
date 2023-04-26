namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_CertificacionDetalles
    {
        public short? NroAspecto { get; set; }

        public short? NroSubAspecto { get; set; }

        [StringLength(40)]
        public string Cumplimiento { get; set; }

        [StringLength(100)]
        public string Responsable { get; set; }

        public int? Id_EACertificacion { get; set; }

        public int? NumAccionCertificacion { get; set; }

        [Key]
        public int Id_CertificacionDetalles { get; set; }

        public virtual Sub_Certificacion Sub_Certificacion { get; set; }
    }
}
