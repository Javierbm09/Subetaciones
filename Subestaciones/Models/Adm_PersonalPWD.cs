namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adm_PersonalPWD
    {
        [Key]
        [Column(Order = 0)]
        public Guid idCambio { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime fechaCambio { get; set; }

        public int Id_Persona { get; set; }

        public int id_EAdministrativa_Prov { get; set; }

        [Required]
        public string contrasenna { get; set; }
    }
}
