namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoBateriasEstacionarias
    {
        //[Key]
        //[Column(Order = 0)]
        //public int id_MttoBatEstacionarias { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_Bateria { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short EA_RedCD { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado exterior de los vasos")]
        public string EstadoExtVasos { get; set; }

        [StringLength(50)]
        [Display(Name = "Nivel del electrolito")]
        public string NivelElectrolito { get; set; }

        [Display(Name = "Revisión del estado de los bornes")]
        public int? EstadoAprieteBornes { get; set; }

        [Display(Name = "Aplicacion vaselina")]
        public int? AplicacionVaselina { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        public DateTime? fechaMtto { get; set; }

        [StringLength(7)]
        [Display(Name = "Listado de Subestaciones")]
        [Required]
        public string subestacion { get; set; }

        [StringLength(750)]
        [Column(TypeName = "text")]
        public string Observaciones { get; set; }

        public int numAccion { get; set; }

        [StringLength(500)]
        public string incidenciasUltimaRev { get; set; }

        [Required]
        [Display(Name = "Realizado por")]
        public int? RealizadoPor { get; set; }

        [Required]
        [Display(Name = "Tipo de mantenimiento")]
        public short? TipoMtto { get; set; }

        public bool? Mantenido { get; set; }

        [Display(Name = "Limpieza de los bornes")]
        public int? LimpiezaBornes { get; set; }

        [Display(Name = "Apriete de los bornes")]
        public int? AprieteBornes { get; set; }

        [Display(Name = "Limpieza del panel del baterías")]
        public int? LimpiezaPanelBaterias { get; set; }

        [Display(Name = "Limpieza del panel del cargador")]
        public int? LimpiezaPanelCargador { get; set; }

        [Display(Name = "Aterramiento a los paneles")]
        public int? AterramientoPaneles { get; set; }

        [NotMapped]
        public string MensajeExistenciaMtto { get; set; }

        [NotMapped]
        public string NombreSubestacion { get; set; }
    
    }
}