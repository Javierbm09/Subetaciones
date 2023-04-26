namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ES_TransformadorCorriente
    {
        [NotMapped]
        public string NoSerieAnt { get; set; }

        [NotMapped]
        public string FaseAnt { get; set; }

        [Key]
        [StringLength(50)]
        [Display(Name = "Nro serie"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Nro_Serie { get; set; }

        [StringLength(3)]
        [Display(Name = "Fase"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Fase { get; set; }

        [StringLength(20)]
        [Display(Name = "Relaci&oacute;n Transformación")]
        public string Relacion_Transformacion { get; set; }

        [Display(Name = "Cantidad devanados"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public short? Cant_Devanado { get; set; }

        public double? Frecuencia { get; set; }

        [Column("Fs/Fi")]
        public int? Fs_Fi { get; set; }

        [Display(Name = "Voltaje instalado (kV)")]
        public short? id_Voltaje_Nominal { get; set; }

        public bool? Ubicado { get; set; }

        public int? id_Plantilla { get; set; }

        [StringLength(7)]
        [Display(Name = "Subestación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string CodSub { get; set; }

        [StringLength(15)]
        [Display(Name = "Ubicado en"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Tipo_Equipo_Primario { get; set; }

        [StringLength(20)]
        [Display(Name = "Código equipo"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Elemento_Electrico { get; set; }

        public int? Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        [Display(Name = "In trabajo primaria (A)")]
        public double? InTrabajoPrim { get; set; }

        [Display(Name = "In secundaria (A)")]
        public double? InSecundaria { get; set; }

        [StringLength(50)]
        public string Tipo { get; set; }

        [StringLength(50)]
        public string Inventario { get; set; }

        [Display(Name = "Año fabricación")]
        [RegularExpression("[0-9][0-9][0-9][0-9]", ErrorMessage = "El valor suministrado no es correcto")]
        public short? AnnoFab { get; set; }

        [Display(Name = "Fecha instalado")]
        public DateTime? FechaInstalado { get; set; }

        [Display(Name = "Peso (kg)")]
        public double? Peso { get; set; }

        [StringLength(50)]
        [Display(Name = "In primaria (A)")]
        public string InPrimaria { get; set; }

        [StringLength(50)]
        public string Fabricante { get; set; }
    }
}
