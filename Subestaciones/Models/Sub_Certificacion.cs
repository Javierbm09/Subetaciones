namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_Certificacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sub_Certificacion()
        {
            Sub_CertificacionComision = new HashSet<Sub_CertificacionComision>();
            Sub_CertificacionDetalles = new HashSet<Sub_CertificacionDetalles>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_EAdministrativa { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumAccion { get; set; }

        [StringLength(1500)]
        public string Observaciones { get; set; }

        [StringLength(7), Display(Name = "Subestación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string CodigoSub { get; set; }

        [NotMapped]
        public string nombreOBE { get; set; }

        public DateTime? Fecha { get; set; }

        public short? UEB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sub_CertificacionComision> Sub_CertificacionComision { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sub_CertificacionDetalles> Sub_CertificacionDetalles { get; set; }
    }
}
