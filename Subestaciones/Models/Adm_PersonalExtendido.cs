namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adm_PersonalExtendido
    {
        [Required]
        [StringLength(100)]
        public string nombreUsuario { get; set; }

        [Required]
        public string contrasenna { get; set; }

        [Key]
        [Column(Order = 0)]
        public int Id_Persona { get; set; }

        [Key]
        [Column(Order = 1)]
        public int id_EAdministrativa_Prov { get; set; }

        public virtual Personal Personal { get; set; }
    }
}
