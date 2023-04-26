using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class AnalisisGD
    {

        [StringLength(7)]
        public string CodigoSub { get; set; }

        [Display(Name = "Subestación")]
        public string subestacion { get; set; }

        [Display(Name = "Transformador")]
        public string Nombre { get; set; }

        [Display(Name = "No serie")]
        public string NoSerie { get; set; }

        [Display(Name = "No empresa")]
        public string Numemp { get; set; }

        [Display(Name = "Año Fabricación")]
        public short? AnnoFabricacion { get; set; }

        [Display(Name = "Peso Aceite")]
        public double? PesoAceite { get; set; }

        [Display(Name = "Capacidad")]
        public double? capacidad { get; set; }

        public int Id_Transf { get; set; }

        public short Id_EAdminTransf { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        public DateTime Fecha { get; set; }

        [StringLength(100)]
        public string RealizadoenLaboraorio { get; set; }

        [StringLength(50)]
        public string NumeroControl { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaSeleccion { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaRecepcion { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaInicio { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaTerminacion { get; set; }

        [StringLength(50)]
        public string MuestreoSegProc { get; set; }

        [StringLength(75)]
        public string MuestreadoPor { get; set; }

        [StringLength(150)]
        public string EjecutadoPor { get; set; }

        [StringLength(150)]
        public string RevisadoPor { get; set; }

        [StringLength(200)]
        public string ResulSegunNormas { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        public int? ResNro { get; set; }

        [StringLength(75)]
        public string EmitidoPor { get; set; }

        [StringLength(75)]
        public string CargoEmitidoPor { get; set; }

        [StringLength(50)]
        public string MetodoEnsayo { get; set; }

        [StringLength(50)]
        public string NormaMuestreo { get; set; }

        public double? H2Hidrogeno { get; set; }

        public double? CH4Metano { get; set; }

        public double? C2H6Etano { get; set; }

        public double? C2H4Etileno { get; set; }

        public double? C2H2Acetileno { get; set; }

        public double? COMonoxidoCarbono { get; set; }

        public double? TempAceite { get; set; }

        public double? CO2DioxidoCarbono { get; set; }

        [NotMapped]
        public string f { get; set; }

        [NotMapped]
        public string fSelec { get; set; }

        [NotMapped]
        public string fIni { get; set; }

        [NotMapped]
        public string fRec { get; set; }

        [NotMapped]
        public string fTer { get; set; }

    }
}