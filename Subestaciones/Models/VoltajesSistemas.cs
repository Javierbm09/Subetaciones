namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VoltajesSistemas
    {
        [Key]
        public short Id_VoltajeSistema { get; set; }

        [Display(Name = "Tensión")]
        public double Voltaje { get; set; }

        public bool Monofasico { get; set; }

        public bool Trifasico { get; set; }

        public bool Fuente { get; set; }

        public bool Servicio { get; set; }

        [StringLength(1)]
        public string Nivel { get; set; }

        public bool NC { get; set; }

        public bool? TransfSubPrimario { get; set; }

        public bool? TransfSubSecundario { get; set; }

        public bool? TransfSubTerciario { get; set; }

        public bool? TCUNom { get; set; }

        public bool? TPUNom { get; set; }
    }
}
