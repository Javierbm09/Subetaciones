namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GruposG")]
    public partial class GruposG
    {
        [Key]
        [StringLength(7)]
        public string Codigo { get; set; }

        [StringLength(5)]
        public string Emplazamiento { get; set; }

        public int? Bateria { get; set; }

        public byte? Id_Grupo { get; set; }

        public DateTime? FechaIni { get; set; }

        public Guid? Tipo { get; set; }

        public short? Number { get; set; }

        [StringLength(7)]
        public string Instalacion_Transformadora { get; set; }

        [StringLength(20)]
        public string Calle { get; set; }

        [StringLength(12)]
        public string Numero { get; set; }

        [StringLength(20)]
        public string Entrecalle1 { get; set; }

        [StringLength(20)]
        public string Entrecalle2 { get; set; }

        public short? Sucursal { get; set; }

        public int? coddireccion { get; set; }

        public bool Eq_Sincronizacion { get; set; }

        public bool NormalmenteSinc { get; set; }

        [StringLength(20)]
        public string NoSerie { get; set; }

        [StringLength(7)]
        public string Desconectivo { get; set; }

        public Guid? Id_Seccion { get; set; }

        public int? Id_EAdireccion { get; set; }

        [StringLength(30)]
        public string BarrioPueblo { get; set; }

        public short? Id_EAdministrativa { get; set; }

        [StringLength(1)]
        public string TipoGeneracion { get; set; }

        [StringLength(10)]
        public string Codigo_Emergencia { get; set; }

        public int? NumAccion { get; set; }

        [StringLength(1)]
        public string EstadoOperativo { get; set; }

        public short? Generación { get; set; }

        [StringLength(5)]
        public string centg { get; set; }

        public byte? batg { get; set; }

        public byte? unigg { get; set; }
    }
}
