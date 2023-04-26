namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bloque")]
    public partial class Bloque
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        
        public string Codigo { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_bloque { get; set; }

        [StringLength(50)]
        [Display(Name = "Tipo Bloque"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string tipobloque { get; set; }

        public short? EsquemaPorBaja { get; set; }

        public short? VoltajeSecundario { get; set; }

        public short? VoltajeTerciario { get; set; }

        [StringLength(15)]
        public string TipoSalida { get; set; }

        public bool? Priorizado { get; set; }

        [NotMapped]
        public string sector { get; set; }

        [NotMapped]
        public string Cliente { get; set; }
    }
}
