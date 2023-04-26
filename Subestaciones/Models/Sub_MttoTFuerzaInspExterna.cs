namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoTFuerzaInspExterna
    {
        public int Id { get; set; }

        public int Id_MttoTFuerza { get; set; }

        [StringLength(50)]
        public string InspExtValvulas { get; set; }

        [StringLength(50)]
        public string InspExtNiveles { get; set; }

        [StringLength(50)]
        public string InspExtTornilleria { get; set; }

        [StringLength(50)]
        public string InspExtRadiador { get; set; }

        [StringLength(50)]
        public string InspExtBombas { get; set; }

        [StringLength(50)]
        public string InspExtConexiones { get; set; }

        [StringLength(50)]
        public string InspExtPresionSubita { get; set; }

        [StringLength(50)]
        public string InspExtVentiladores { get; set; }

        [StringLength(50)]
        public string InspExtTermometros { get; set; }

        [StringLength(50)]
        public string InspExtTermosifon { get; set; }

        [StringLength(50)]
        public string InspExtNitrogeno { get; set; }

        [StringLength(50)]
        public string InspExtTierras { get; set; }

        [StringLength(50)]
        public string InspExtRespiraderos { get; set; }

        [StringLength(50)]
        public string Busholtz { get; set; }

        [StringLength(100)]
        public string Otros { get; set; }
    }
}
