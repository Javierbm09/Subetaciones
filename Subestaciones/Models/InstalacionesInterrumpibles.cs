namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InstalacionesInterrumpibles
    {
        [Key]
        [StringLength(7)]
        public string Codigo { get; set; }

        public double? Kvainstalados { get; set; }

        public double? Kvareales { get; set; }

        [StringLength(7)]
        public string Circuito { get; set; }

        [StringLength(7)]
        public string Padre { get; set; }

        [StringLength(7)]
        public string Sinonimo { get; set; }

        public short? FuseIdealA { get; set; }

        public short? FuseIdealB { get; set; }

        public short? FuseIdealC { get; set; }

        [StringLength(1)]
        public string Tipo { get; set; }

        public short? Id_EstAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        public int? ConsumidoresA { get; set; }

        public int? ConsumidoresB { get; set; }

        public int? ConsumidoresC { get; set; }

        [StringLength(7)]
        public string SeccionAlimentada { get; set; }
    }
}
