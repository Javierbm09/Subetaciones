using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class RedCD
    {
        public int Id_ServicioCD { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre del Servicio CD")]
        public string NombreServicioCD { get; set; }

        [Display(Name = "Subestación")]
        public string NombreSub { get; set; }

        [Required]
        [StringLength(7)]
        [Display(Name = "Subestación")]
        public string Codigo { get; set; }

        public int id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        [Display(Name = "Tensión CD (V)")]
        public double? idVoltaje { get; set; }

        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Uso de la Red")]
        public string UsoRed { get; set; }

        [Display(Name = "Control de aislamiento por barras")]
        public string ControlAislamBarras { get; set; }

        [StringLength(750)]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
    }
}