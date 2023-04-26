namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_RedCorrienteDirecta
    {
        
        [StringLength(50)]
        [Display(Name = "Nombre del Servicio"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string NombreServicioCD { get; set; }

      
        [StringLength(7)]
        [Display(Name = "Subestación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Codigo { get; set; }

        public int id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        [Display(Name = "Tensión CD (V)"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public short? idVoltaje { get; set; }

        [Key]
        public int Id_ServicioCD { get; set; }

        [StringLength(50)]
        [Display(Name = "Uso de la red")]
        public string UsoRed { get; set; }

        [Display(Name = "Control de Aislamiento por Barras")]
        public bool? ControlAislamBarras { get; set; }

        [StringLength(750)]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
    }
}
