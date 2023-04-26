namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Emplazamiento_Sigere
    {
        [Key]
        [StringLength(5)]
        public string Codigo { get; set; }

        [StringLength(150)]
        public string Nombre { get; set; }

        [StringLength(20)]
        public string Tipo { get; set; }

        public short Provincia { get; set; }

        public short Fuente_Energia { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int NumAccion { get; set; }

        [StringLength(20)]
        public string Calle { get; set; }

        [StringLength(6)]
        public string Numero { get; set; }

        [StringLength(20)]
        public string Entrecalle1 { get; set; }

        [StringLength(20)]
        public string Entrecalle2 { get; set; }

        [StringLength(25)]
        public string BarrioPueblo { get; set; }

        public short? Sucursal { get; set; }

        [Required]
        [StringLength(7)]
        public string CentroTransformacion { get; set; }

        public int? Id_EAdireccion { get; set; }

        public int? coddireccion { get; set; }

        public bool? tipo_fuelCTE { get; set; }

        public bool planta_pico { get; set; }

        [StringLength(10)]
        public string centg { get; set; }
    }
}
