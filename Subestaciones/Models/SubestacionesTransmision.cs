namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubestacionesTransmision")]
    public partial class SubestacionesTransmision
    {
        [Key]
        [StringLength(7)]
        [Display(Name = "Código"),
            Required(ErrorMessage = "Debe introducir el campo: {0} "),
            RegularExpression(@"(^[a-zA-Z]{1}[r,R]{1}[0-9]{1,5})", ErrorMessage = "El campo {0} debe tener el siguiente formato SR12345")]
        public string Codigo { get; set; }

        [NotMapped]
        public string CodAntiguo { get; set; }// valor que se utiliza para poder actualizar el codigo si el mismo se edita, pues campo llave

        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string NombreSubestacion { get; set; }

        [StringLength(7)]
        [Display(Name = "Código antiguo")]
        public string CodigoAntiguo { get; set; }

        [Display(Name = "Esquema por Alta")]
        public short? EsquemaPorAlta { get; set; }

        [Display(Name = "Tensión Nominal")]
        public short? VoltajePrimario { get; set; }

        [StringLength(70)]
        public string Calle { get; set; }

        [StringLength(6)]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [StringLength(70)]
        [Display(Name = "Entre")]
        public string Entrecalle1 { get; set; }

        [StringLength(70)]
        [Display(Name = "Y")]
        public string Entrecalle2 { get; set; }

        [StringLength(70)]
        [Display(Name = "Lugar habitado")]
        public string BarrioPueblo { get; set; }

        public short Sucursal { get; set; }

        public short? NumeroTransformadores { get; set; }

        [Display(Name = "Número Salidas")]
        public short? NumeroSalidas { get; set; }

        public short Id_EAdministrativa { get; set; }

        public int id_EAdministrativa_Prov { get; set; }

        public int? NumAccion { get; set; }

        [StringLength(1)]
        [Display(Name = "Estado Operativo")]
        public string EstadoOperativo { get; set; }

        [StringLength(20)]
        [Display(Name = "Tipo de Subestación")]
        public string TipoSub { get; set; }

        public int? coddireccion { get; set; }

        public short? Id_EAdireccion { get; set; }

        [Display(Name = "Tipo Terceros")]
        public bool? Tipo_Terceros { get; set; }

        [Display(Name = "Largo (m)")]
        public double? Largo { get; set; }

        [Display(Name = "Ancho (m)")]
        public double? Ancho { get; set; }

        [Display(Name = "Latitud (⁰)")]
        public double? Latitud { get; set; }

        [Display(Name = "Longitud (⁰)")]
        public double? Longitud { get; set; }

        [Display(Name = "Fecha de Puesta en Marcha")]
        public DateTime? FechaPuestaMarcha { get; set; }
    }
}
