namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Inst_Nomenclador_Puente_Modelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Modelo { get; set; }

        public int? Id_Administrativa { get; set; }

        public int? id_EAdministrativa_Prov { get; set; }

        public int? NumAccion { get; set; }

        [StringLength(30)]
        public string Descripcion_Modelo { get; set; }
    }
}
