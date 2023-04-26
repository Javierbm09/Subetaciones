namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Sub_Barra
    {
        [NotMapped]
        public string CodAnterior { get; set; }

        [NotMapped]
        public string SubAnterior { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        [Display(Name = "Subestación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        //[Remote("ValidarCodigos", "Sub_Barra", AdditionalFields = "SubAnterior", ErrorMessage = "El {0} ya existe.", HttpMethod = "POST")]
        //ValidarCodigos(string SubAnterior, string CodAnterior, string Codigosub, string Codigobarra)
        public string Subestacion { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        [Display(Name = "Nombre barra"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        //[Remote("ValidarCodigos", "Sub_Barra", AdditionalFields = "SubAnterior, CodAnterior, Subestacion", ErrorMessage = "El {0} ya existe.", HttpMethod = "POST")]
        public string codigo { get; set; }

        [StringLength(20)]
        [Display(Name = "Conductor")]
        public string Conductor { get; set; }

        [Display(Name = "Tensión")]
        public int? ID_Voltaje { get; set; }

        [Display(Name = "Corriente")]
        public float? corriente { get; set; }

        [Display(Name = "Conductores por fase ")]
        public int? CantidadCond { get; set; }

        [StringLength(7)]
        public string equipo1 { get; set; }

        [StringLength(7)]
        public string equipo2 { get; set; }

        public int? Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }
    }
}
