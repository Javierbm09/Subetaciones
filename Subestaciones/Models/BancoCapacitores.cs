namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class BancoCapacitores
    {
        [NotMapped]
        public string CodigoAnterior { get; set; }

        [Key]
        [StringLength(7, ErrorMessage = "El campo {0} puede tener hasta {1} caracteres")]
        [Display(Name = "Código banco"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        [RegularExpression(@"(^[A-Z]{1}[C]{1}[0-9]{1,5})", ErrorMessage = "El campo {0} debe tener el siguiente formato: primera letra mayúscula del municipio y segunda letra C mayúscula, ejemplo SC12345")]
        [Remote("ValidarCodigoBanco", "BancoCapacitores", AdditionalFields = "CodigoAnterior", ErrorMessage = "El {0} ya existe.", HttpMethod = "POST")]
        public string Codigo { get; set; }

        [StringLength(7, ErrorMessage = "El campo {0} puede tener hasta {1} caracteres")]
        [Display(Name = "Código antiguo")]
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

        public short Sucursal { get; set; }

        
        [StringLength(7)]
        [Display(Name = "Seccionalizador"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Seccionalizador { get; set; }
                
        [StringLength(7)]
        [Display(Name = "Subestación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Circuito { get; set; }

        public short? Conexion { get; set; }

        [StringLength(10)]
        [Display(Name = "Tipo de control")]
        public string TipoControl { get; set; }

        [Display(Name = "CKVAR instalado")]
        public double? CKVAR_Instalado { get; set; }

        public int? Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        [StringLength(1)]
        [Display(Name = "Estado operativo")]
        public string EstadoOperativo { get; set; }

        public int? coddireccion { get; set; }

        public short? Id_EAdireccion { get; set; }

        public Guid? Id_Seccion { get; set; }

        [Display(Name = "Fecha instalado")]
        public DateTime? FechaInstalado { get; set; }
    }
}
