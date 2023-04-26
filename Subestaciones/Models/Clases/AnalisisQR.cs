using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Subestaciones.Models.Clases
{
    public class AnalisisQR
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

        [StringLength(100), Display(Name = "Realizado en")]
        public string RealizadoenLaboraorio { get; set; }

        [StringLength(50)]
        [Display(Name = "Nro control")]
        public string NumeroControl { get; set; }

        [Display(Name = "Fecha selección")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaSeleccion { get; set; }

        [Display(Name = "Fecha recepción")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaRecepcion { get; set; }

        [Display(Name = "Fecha inicio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaInicio { get; set; }

        [Display(Name = "Fecha terminación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaTerminacion { get; set; }

        [StringLength(50)]
        [Display(Name = "Muestreado según proc.")]
        public string MuestreoSegProc { get; set; }

        [StringLength(75)]
        [Display(Name = "Por")]
        public string MuestreoSegProcPor { get; set; }

        [StringLength(1)]
        [Display(Name = "Clasificación")]
        public string Clasificacion { get; set; }

        [StringLength(150), Display(Name = "Ejecutador por")]
        public string EjecutadoPor { get; set; }

        [StringLength(150), Display(Name = "Revisado por")]
        public string RevisadoPor { get; set; }

        public double? TempMuestra { get; set; }

        public double? Densidada20GC { get; set; }

        public double? NrodeNeutralizacion { get; set; }

        public double? AguaporKFisher { get; set; }

        public double? HumedadenPapel { get; set; }

        public double? TensionInterfacial { get; set; }

        public double? PuntoInflamacion { get; set; }

        public double? RigidezDielectrica { get; set; }

        public double? SedimentoyCienoPrecip { get; set; }

        public double? Viscosidada40GC { get; set; }

        public double? FactorDisipacionTAmb { get; set; }

        public double? FactorDisipaciona70GC { get; set; }

        public double? FactorDisipacion90GC { get; set; }

        [StringLength(150)]
        public string AspectoFisico { get; set; }

        [StringLength(200)]
        public string ResulSegunNormas { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        public int? ResNro { get; set; }

        [StringLength(75)]
        public string EmitidoPor { get; set; }

        [StringLength(75)]
        public string CargoEmitidoPor { get; set; }

        [NotMapped]
        public string f { get; set; }

        [NotMapped]
        public string fechaSelec { get; set; }

        [NotMapped]
        public string fechaIni { get; set; }

        [NotMapped]
        public string fechaRec { get; set; }

        [NotMapped]
        public string fechaTer { get; set; }
    }
}