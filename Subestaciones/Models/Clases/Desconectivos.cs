using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Desconectivos
    {
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [StringLength(7)]
        [Display(Name = "Código nuevo")]
        public string CodigoNuevo { get; set; }

        public int id_EAdministrativa_Prov { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        [Display(Name = "Num Fases")]
        public short? NumeroFases { get; set; }

        [Display(Name = "Corriente nominal")]
        public short? CorrienteNominal { get; set; }

        [StringLength(1)]
        [Display(Name = "Tipo instalación")]
        public string TipoInstalacion { get; set; }

        [StringLength(1)]
        [Display(Name = "Tipo desconectivo")]
        public string TipoSeccionalizador { get; set; }

        [Display(Name = "Tipo")]

        public string tipoDesc { get; set; }

        [Required]
        [StringLength(7)]
        public string CircuitoA { get; set; }

        [StringLength(7)]
        public string SeccionA { get; set; }

        [StringLength(7)]
        public string CircuitoB { get; set; }

        [StringLength(7)]
        public string SeccionB { get; set; }

        [StringLength(1)]
        [Display(Name = "Funcion desconectivo")]
        public string Funcion { get; set; }

        [Display(Name = "Estado operativo")]
        public string EstadoOperativo { get; set; }

        [Display(Name = "Automática")]
        public string Automatica { get; set; }

        [StringLength(50)]
        public string BarrioPueblo { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Ubicada en")]
        public string UbicadaEn { get; set; }

        [Display(Name = "Fabricante")]
        public string fabricante { get; set; }

        [Display(Name = "Año fabricación")]
        public int? anhoFab { get; set; }

        [Display(Name = "Tipo")]
        public string DescripcionTipoPortaFusible { get; set; }

        [Display(Name = "Tipo")]
        public string DescripcionTipoFusible { get; set; }

        [Display(Name = "Tensión(kV)")]
        public int? DescripcionTensionFusible { get; set; }

        [Display(Name = "Capacidad(A)")]
        public double? DescripcionCapacidadFusible { get; set; }
    }
}