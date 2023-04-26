namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MedicionTierra_CaidaPotencial
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Subestacion { get; set; }

        public DateTime Fecha { get; set; }

        public int Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        public float? DistanciaElectrodoPotencial { get; set; }

        public float? Resistencia { get; set; }
    }
}
