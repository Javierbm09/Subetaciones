namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_Pararrayos
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_EAdministrativa { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_pararrayo { get; set; }

        public int? NumAccion { get; set; }

        [StringLength(7)]
        [Display(Name = "Subestación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Codigo { get; set; }

        [StringLength(30, ErrorMessage = "El campo {0} puede tener hasta {1} caracteres")]
        [Display(Name = "Número serie")]
        public string NumeroSerie { get; set; }

        [StringLength(70)]
        [Display(Name = "Tipo")]
        public string TipoPararrayo { get; set; }

        [StringLength(3)]
        [Display(Name = "Fase"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Fase { get; set; }

        [StringLength(3)]
        [Display(Name = "Tipo equipo protegido"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string TequipoProt { get; set; }

        [StringLength(11)]
        [Display(Name = "Código"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string CE { get; set; }

        [StringLength(30)]
        [Display(Name = "Aislamiento")]
        public string Aislamiento { get; set; }

        [Display(Name = "Tensión nominal (kV)")]
        public short? Id_Voltaje { get; set; }

        [Display(Name = "Corriente nominal (kA)")]
        public int? Id_CorrienteN { get; set; }

        [StringLength(70)]
        [Display(Name = "Fabricante")]
        public string Fabricante { get; set; }

        [Display(Name = "Año fabricación")]
        public short? AñoFabricacion { get; set; }

        [StringLength(4)]
        [Display(Name = "MOCV (kV)")]
        public string MOCV { get; set; }

        [StringLength(50)]
        [Display(Name = "Inventario")]
        public string Inventario { get; set; }

        [StringLength(50)]
        [Display(Name = "Material")]
        public string Material { get; set; }

        [StringLength(20)]
        [Display(Name = "Clase")]
        public string Clase { get; set; }

        [Display(Name = "Instalado")]
        public DateTime? Instalado { get; set; }

        [StringLength(5)]
        [Display(Name = "Frecuencia (Hz)")]
        public string Frecuencia { get; set; }

        [StringLength(4)]
        [Display(Name = "Voltaje instalado (kV)")]
        public string VoltajeInstalado { get; set; }

        [StringLength(15)]
        public string Estado { get; set; }

        public bool Levantado { get; set; }

        [StringLength(20)]
        public string Ubicacion { get; set; }

        [StringLength(1)]
        public string EstadoOperativo { get; set; }

        [StringLength(12)]
        public string Marca { get; set; }

        [MaxLength(1)]
        public byte[] PararrayoPrimario { get; set; }

        [MaxLength(1)]
        public byte[] PararrayoSecundario { get; set; }
    }
}
