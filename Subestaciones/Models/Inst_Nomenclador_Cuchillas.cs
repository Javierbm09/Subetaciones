namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Inst_Nomenclador_Cuchillas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Cuchilla { get; set; }

        [Required]
        [StringLength(7)]
        [Display(Name ="Código")]
        public string Codigo { get; set; }

        [Display(Name = "Fabricante")]
        public int Id_Fabricante { get; set; }

        [Display(Name = "Tensión (kV)")]
        public int Id_Tension { get; set; }

        [Display(Name = "Operación")]
        public int Id_Operacion { get; set; }

        [Display(Name = "Mando")]
        public int Id_Mando { get; set; }

        public int Id_Administrativa { get; set; }

        public int NumAccion { get; set; }

        public int id_EAdministrativa_Prov { get; set; }

        public virtual Inst_Nomenclador_Cuchillas_Mando Inst_Nomenclador_Cuchillas_Mando { get; set; }

        public virtual Inst_Nomenclador_Cuchillas_Operacion Inst_Nomenclador_Cuchillas_Operacion { get; set; }

        public virtual Inst_Nomenclador_Cuchillas_Tension Inst_Nomenclador_Cuchillas_Tension { get; set; }
    }
}
