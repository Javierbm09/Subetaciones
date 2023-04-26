namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Inst_Nomenclador_Cuchillas_Operacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inst_Nomenclador_Cuchillas_Operacion()
        {
            Inst_Nomenclador_Cuchillas = new HashSet<Inst_Nomenclador_Cuchillas>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Operacion { get; set; }

        public int? Id_Administrativa { get; set; }

        public int? id_EAdministrativa_Prov { get; set; }

        public int NumAccion { get; set; }

        [Required]
        [StringLength(50)]
        public string DescripcionOperacionCuchillas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inst_Nomenclador_Cuchillas> Inst_Nomenclador_Cuchillas { get; set; }
    }
}
