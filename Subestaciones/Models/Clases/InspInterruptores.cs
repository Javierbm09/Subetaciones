using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class InspInterruptores
    {
        [Display(Name = "Código")]
        public string codigoInterruptor { get; set; }

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
        public string codigoSub { get; set; }

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

        [Display(Name = "Fecha mtto")]
        public DateTime fecha { get; set; }

        [StringLength(30), Display(Name = "Tipo inspección")]
        public string nombreCelaje { get; set; }

        [Display(Name = "Salidero")]
        public bool? Salidero { get; set; }

        [StringLength(6)]
        [Display(Name = "Aceite")]
        public string NAceite { get; set; }

        [Display(Name = "Pintura")]
        [StringLength(7)]
        public string Pintura { get; set; }

        [Display(Name = "Cuenta operaciones")]
        public int? cuentaOP { get; set; }

        [StringLength(10)]
        [Display(Name = "Batería")]
        public string estadoBateria { get; set; }

        [StringLength(10)]
        [Display(Name = "Candado gabinete")]
        public string candadoGabinete { get; set; }

    }
}