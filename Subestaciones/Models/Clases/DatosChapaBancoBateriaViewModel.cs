using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class DatosChapaBancoBateriaViewModel
    {
        
        public int id_MttoBatEstacionarias { get; set; }

        public int Id_Bateria { get; set; }

        public short? CantidadVasos { get; set; }

        public int id_EAdministrativa { get; set; }

        public short EA_RedCD { get; set; }

        [Required]
        [Display(Name = "Banco de baterías")]
        public string TipoBateria { get; set; }

        [Display(Name = "Capacidad")]
        public double CapacidadBateria { get; set; }

        [Display(Name = "Tension Nominal")]
        public double TensionBateria { get; set; }

        [Display(Name = "Tipo")]
        public string ClaseBateria { get; set; }

        [Display(Name = "Ubicado en la red de corriente directa")]
        public string NombreServicioCD { get; set; }
    }
}