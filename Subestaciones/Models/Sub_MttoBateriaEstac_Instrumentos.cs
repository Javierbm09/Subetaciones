namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoBateriaEstac_Instrumentos
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "Lista de instrumentos")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_InstrumentoMedicion { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_MttoBatEstacionarias { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_Bateria { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short EA_RedCD { get; set; }

    }
}
