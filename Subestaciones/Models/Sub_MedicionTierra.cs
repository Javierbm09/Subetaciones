namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MedicionTierra
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7), Display(Name = "Subestación")]
        public string Subestacion { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Fecha { get; set; }

        [NotMapped]
        public string f { get; set; }

        public int Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        [StringLength(50), Display(Name = "Realizado por")]
        public string RealizadaPor { get; set; }

        [StringLength(50), Display(Name = "Empresa")]
        public string Empresa { get; set; }

        [StringLength(500)]
        public string ObservacionesGenerales { get; set; }

        [StringLength(50)]
        public string Marca { get; set; }

        [StringLength(50)]
        public string Modelo { get; set; }

        [StringLength(50)]
        public string Serie { get; set; }

        [StringLength(50)]
        public string Operador { get; set; }

        [Display(Name = "Ángulo")]
        public float? Angulo { get; set; }

        [StringLength(100), Display(Name = "Tipo de malla")]
        public string TipoMalla { get; set; }

        public float? Diagonal { get; set; }

        public float? Temperatura { get; set; }

        [StringLength(50), Display(Name = "Estado del suelo")]
        public string EstadoSuelo { get; set; }

        [StringLength(50), Display(Name = "Tipo del suelo")]
        public string TipoSuelo { get; set; }

        [Display(Name = "Distancia Electrodo Corriente")]
        public float? DistElectCorriente { get; set; }

        [StringLength(50), Display(Name = "Distancia Electrodo Corriente")]
        public string MallaEquipotencial { get; set; }

        [StringLength(50), Display(Name = "Certificación API")]
        public string CertificacionAPCI { get; set; }

        public DateTime? FechaCertificacion { get; set; }

        [Display(Name = "Resistencia Resultante(Ω)")]
        public float? ResistenciaResultante { get; set; }

        [StringLength(50), Display(Name = " I Parásita")]
        public string CorrienteParasita { get; set; }

        [Display(Name = "Número de Puntos")]
        public float? NumeroPuntos { get; set; }

        [Column(TypeName = "image")]
        [Display(Name = "Número de Puntos")]
        public byte[] ImagenPuntos { get; set; }
    }
}
