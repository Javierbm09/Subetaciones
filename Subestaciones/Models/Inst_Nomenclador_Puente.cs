namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Inst_Nomenclador_Puente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Puente { get; set; }

        [Required]
        [StringLength(7)]
        public string Codigo { get; set; }

        public int? Id_Modelo { get; set; }

        public int Id_Tipo { get; set; }

        public int? Bimetalica { get; set; }

        public int Id_Administrativa { get; set; }

        public int NumAccion { get; set; }

        public int id_EAdministrativa_Prov { get; set; }

        public virtual Inst_Nomenclador_Puente Inst_Nomenclador_Puente1 { get; set; }

        public virtual Inst_Nomenclador_Puente Inst_Nomenclador_Puente2 { get; set; }
    }
}
