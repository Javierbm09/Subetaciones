namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoPararrayos
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short id_EAdministrativa { get; set; }

        public double? corrienteDescarga { get; set; }

        public int? cantOperaciones { get; set; }

        public double? corrienteFiltracionCD { get; set; }

        public int? numAccion { get; set; }

        [Column(TypeName = "text")]
        [DataType(DataType.MultilineText)]
        public string observaciones { get; set; }

        [Required]
        [StringLength(7)]
        [Display(Name = "Listado de Subestaciones")]
        public string subestacion { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_MttoPararrayo { get; set; }

        [Display(Name = "Revisado Por")]
        public short? revisadoPor { get; set; }

        [Display(Name = "Fecha")]
        public DateTime? fechaMantenimiento { get; set; }

        [StringLength(3)]
        [Display(Name = "Tipo Equipo Protegido*")]
        public string TequipoProt { get; set; }

        [StringLength(50)]
        [Display(Name = "Equipo Protegido*")]
        public string CodigoEquipoProtegido { get; set; }

        [Column(TypeName = "text")]
        public string incidenciasUltimaRev { get; set; }

        [StringLength(20)]
        [Display(Name = "Porcelanas")]
        public string porcelanas { get; set; }

        [StringLength(20)]
        [Display(Name = "Tornillería")]
        public string tornilleria { get; set; }

        [StringLength(20)]
        [Display(Name = "Platillos y Membranas")]
        public string platillosMembranas { get; set; }

        [StringLength(20)]
        [Display(Name = "Conexiones")]
        public string conexiones { get; set; }

        [StringLength(20)]
        [Display(Name = "Aterramientos")]
        public string aterramientos { get; set; }

        [StringLength(20)]
        [Display(Name = "Partes Metálicas")]
        public string partesMetalicas { get; set; }

        [Display(Name = "Fase A")]
        public int? cuentaOpFaseA { get; set; }

        [Display(Name = "Fase B")]
        public int? cuentaOpFaseB { get; set; }

        [Display(Name = "Fase C")]
        public int? cuentaOpFaseC { get; set; }

        [Display(Name = "Tipo de Mantenimiento*")]
        public int? tipoMantenimiento { get; set; }

        public bool? Mantenido { get; set; }

        [Display(Name = "Fase A")]
        public int? LecturaFugaA { get; set; }

        [Display(Name = "Fase B")]
        public int? LecturaFugaB { get; set; }

        [Display(Name = "Face C")]
        public int? LecturaFugaC { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado Miliamperímetro")]
        public string EstadoMiliA { get; set; }

        [StringLength(50)]
        [Display(Name = "Estado del Cuenta Operaciones")]
        public string EstadoCuentaOp { get; set; }

        [StringLength(4)]
        [Display(Name = "Voltaje Instalado")]
        public string VoltajeInstalado { get; set; }
    }
}
