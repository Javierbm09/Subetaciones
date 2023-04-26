using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class MttosPararrayos
    {

        
        public short id_EAdministrativa { get; set; }

        public int id_MttoPararrayo { get; set; }

        [Display(Name = "Subestación")]
        public string subestacion { get; set; }

        [Display(Name = "Tipo Equipo Protegido")]
        public string TequipoProt { get; set; }

        [Display(Name = "Código Equipo Protegido")]
        public string CodigoEquipoProtegido { get; set; }
        
        [Display(Name = "Tipo Mantenimiento")]
        public string TipoMtto { get; set; }

        [Display(Name = "Fecha")]
        public string fechaMantenimiento { get; set; }

        [Display(Name = "Realizado Por")]
        public string Nombre { get; set; }
        
        public bool? Mantenido { get; set; }
    }
}