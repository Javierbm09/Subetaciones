namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BancoTransformadores
    {
        [Key]
        [StringLength(7)]
        public string Codigo { get; set; }

        [StringLength(7)]
        public string CodigoAntiguo { get; set; }

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

        [StringLength(7)]
        public string Seccionalizador { get; set; }

        [StringLength(7)]
        public string Circuito { get; set; }

        public short? Conexion { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        public int? Id_VoltajeSalida { get; set; }

        [StringLength(15)]
        public string TipoSalida { get; set; }

        public int? Id_VoltajePrimario { get; set; }

        public int? PerteneceA { get; set; }

        [Required]
        [StringLength(1)]
        public string EstadoOperativo { get; set; }

        public short? Id_TipoCarga { get; set; }

        public int? coddireccion { get; set; }

        public short? Id_EAdireccion { get; set; }

        public Guid? Id_Seccion { get; set; }

        public bool? Paralelo { get; set; }

        public bool ClientePriorizado { get; set; }
    }
}
