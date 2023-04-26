namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion
    {
        [Required]
        [StringLength(7)]
        [Display(Name = "Código")]

        public string Codigo { get; set; }

        [NotMapped]
        [Display(Name ="Tipo recerrador")]
        public int? tipoDesc { get; set; }

        [Display(Name ="Modelo")]
        public int? Id_Nomenclador { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_DatosEspComun { get; set; }

        public int Id_Administrativa { get; set; }

        public int Id_EAdministrativa_Prov { get; set; }

        public int NumAccion { get; set; }

        [Display(Name = "Año fabricación")]
        public int? AnnoFabricacion { get; set; }

        [StringLength(50)]
        [Display(Name ="Número serie")]
        public string SerieInterruptor { get; set; }

        [StringLength(50)]
        [Display(Name ="Número empresa")]
        public string NroEmpresa { get; set; }

        [StringLength(50)]
        [Display(Name ="Número inventario")]
        public string NroInventario { get; set; }

        [StringLength(50)]
        [Display(Name ="Serie del gabinete")]
        public string SeriGabinete { get; set; }

        [Display(Name ="Longitud")]
        public double? Ubicacion { get; set; }

        public double? Altitud { get; set; }

        public double? Latitud { get; set; }

        [Display(Name ="Fecha de instalación")]
        public DateTime? FechaInstalacion { get; set; }

        [StringLength(50)]
        [Display(Name ="Observaciones")]
        public string Observacion { get; set; }

        [Display(Name ="Telemedición")]
        public int? Telemedion { get; set; }

        public int? Telemando { get; set; }

        public int? SCADA { get; set; }

        [Display(Name ="Equipo utilizado")]
        public int? EquipoUtilizado { get; set; }

        [StringLength(50)]
        public string Marca { get; set; }

        [StringLength(50)]
        public string Modelo { get; set; }

        [Display(Name ="Automática")]
        public int? LazoAutomatico { get; set; }

        [Display(Name ="Esquema")]
        public int? Id_Esquema { get; set; }

        [Display(Name ="Función")]
        public int? Id_Funcion { get; set; }

        [Display(Name ="Tensión de instalación (kV)")]
        public int? Id_TensionInstalacion { get; set; }

        [Display(Name ="Tipo gabinete")]
        public int? Id_Gabinete { get; set; }

        public virtual Inst_Nomenclador_Desconectivos Inst_Nomenclador_Desconectivos { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_SCADA Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_SCADA { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_EquipoUtilizado Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_EquipoUtilizado { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Funcion Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Funcion { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TensionInst Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TensionInst { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Esquema Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_Esquema { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TipoGabinete Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion_TipoGabinete { get; set; }
    }
}
