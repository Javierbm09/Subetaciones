namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_RedCorrienteAlterna
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sub_RedCorrienteAlterna()
        {
            Sub_DesconectivoSubestacion = new HashSet<Sub_DesconectivoSubestacion>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id_RedCA { get; set; }

        [StringLength(50)]
        [Display(Name = "Nombre del Servicio CA"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string NombreServicioCA { get; set; }
        
        [StringLength(7)]
        [Display(Name = "Subestación")]
        [Required(ErrorMessage = "Debe selccionar el campo: {0}")]
        public string Codigo { get; set; }

        public int NumAccion { get; set; }

        public short Id_EAdministrativa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sub_DesconectivoSubestacion> Sub_DesconectivoSubestacion { get; set; }


    }
}
