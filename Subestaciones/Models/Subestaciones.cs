namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subestaciones
    {
        [Key]
        [StringLength(7)]
        [Display(Name = "Código"),Required(ErrorMessage = "Debe introducir el campo: {0} ")] 
        [RegularExpression(@"(^[a-zA-Z]{1}[e,E]{1}[0-9]{1,5})", ErrorMessage = "El campo {0} debe tener el siguiente formato SE12345")]
        public string Codigo { get; set; }

        [NotMapped]
        public string CodAntiguo { get; set; }// valor que se utiliza para poder actualizar el codigo si el mismo se edita, pues campo llave

        public short Id_EAdministrativa { get; set; }

        public int id_EAdministrativa_Prov { get; set; }

        public int? NumAccion { get; set; }

        [Display(Name = "Esquema por Alta")]
        public short? TipoSubestacion { get; set; }

        [Display(Name = "Tensión Nominal Alta")]
        public short? VoltajeNominal { get; set; }

        [StringLength(7)]
        [Display(Name = "Código Antiguo")]
        public string CodigoAntiguo { get; set; }

        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string NombreSubestacion { get; set; }

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

        [Display(Name = "Sucursal"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public short Sucursal { get; set; }

        [StringLength(7)]
        public string Cto { get; set; }

        [StringLength(7)]
        public string Seccionalizador { get; set; }

        [StringLength(15)]
        public string TipoSalida { get; set; }

        [Display(Name = "Número Salidas")]
        public short? NumeroSalidas { get; set; }

        [StringLength(1)]
        [Display(Name = "Estado Operativo")]
        public string EstadoOperativo { get; set; }

        public short? Id_TipoCarga { get; set; }

        [StringLength(20)]
        [Display(Name = "Tipo de Subestación")]
        public string TipoSub { get; set; }

        public int? coddireccion { get; set; }

        public short? Id_EAdireccion { get; set; }

        public Guid? id_seccion { get; set; }

        public Guid rowguid { get; set; }

        public Guid uniqdentifier { get; set; }

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
