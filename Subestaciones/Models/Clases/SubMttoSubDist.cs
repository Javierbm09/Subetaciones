using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class SubMttoSubDist
    {
        [Display(Name ="Subestación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string CodigoSub { get; set; }

        public string NombreSub { get; set; }

       
        public DateTime Fecha { get; set; }

        [Display(Name = "Tipo mantenimiento"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string TipoMantenimiento { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        [StringLength(50)]
        public string TipoCerca { get; set; }

        [StringLength(20)]
        public string EstadoCerca { get; set; }

        [StringLength(20)]
        public string CoronacionCerca { get; set; }

        [StringLength(20)]
        public string PinturaCerca { get; set; }

        [StringLength(20)]
        public string AterraminetoCerca { get; set; }

        [StringLength(50)]
        public string TipoPuerta { get; set; }

        [StringLength(20)]
        public string EstadoPuerta { get; set; }

        [StringLength(20)]
        public string CoronacionPuerta { get; set; }

        [StringLength(20)]
        public string PinturaPuerta { get; set; }

        [StringLength(20)]
        public string AterraminetoPuerta { get; set; }

        [StringLength(50)]
        public string TipoAlumbrado { get; set; }

        [StringLength(20)]
        public string EstadoAlumbrado { get; set; }

        [StringLength(20)]
        public string ControlAlumbrado { get; set; }

        [StringLength(20)]
        public string EstadoFotoCelda { get; set; }

        public short? CantidadLamparas { get; set; }

        [StringLength(50)]
        public string TipoPiso { get; set; }

        [StringLength(20)]
        public string EstadoPiso { get; set; }

        [StringLength(20)]
        public string EstOrdenPiso { get; set; }

        [StringLength(20)]
        public string EstAreaExterior { get; set; }

        [StringLength(20)]
        public string FranjaContraIncendio { get; set; }

        [StringLength(100)]
        public string Observaciones { get; set; }

        [Display(Name = "Realizado por"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string RealizadoPor { get; set; }

        [StringLength(20)]
        public string CartelesPuerta { get; set; }

        [StringLength(20)]
        public string CartelesCerca { get; set; }

        [StringLength(20)]
        public string CandadoPuerta { get; set; }

        public short? CantLamparasServicio { get; set; }

        [StringLength(20)]
        public string EstParaFranklin { get; set; }

        public string Mantenido { get; set; }
    }
}