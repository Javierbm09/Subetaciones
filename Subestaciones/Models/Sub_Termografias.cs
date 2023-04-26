namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_Termografias
    {
        [StringLength(7)]
        [Display(Name = "Subestación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Subestacion { get; set; }

        [Display(Name = "Fecha y hora")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy dd:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime fecha { get; set; }

        [Display(Name = "Temperatura Ambiente(ºC)")]
        public float? TempMedio { get; set; }

        [Column(TypeName = "image")]
        [Display(Name = "Imagen real        ")]
        public byte[] Imagen { get; set; }

        [Display(Name = "Carga de la subestación(MVA)")]
        public float? transferencia { get; set; }

        [StringLength(50)]

        public string DescripcionEquipo { get; set; }

        [Display(Name = "Ejecutado por")]
        public int? EjecutadaPor { get; set; }

        public bool? Ejecutado { get; set; }

        public short? InformadoA { get; set; }

        public DateTime? FechaIA { get; set; }

        public short? Elemento { get; set; }

        [NotMapped]
        [Display(Name = "Cantidad de puntos calientes")]
        public int cantPto { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_EAdministrativa { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumAccion { get; set; }

        [Column(TypeName = "image")]
        [Display(Name = "Imagen termográfica")]
        public byte[] ImagenTermografica { get; set; }
    }
}
