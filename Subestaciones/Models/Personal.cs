
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace Subestaciones.Models
{

    [Table("Personal")]
    public class Personal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personal()
        {
            this.Adm_PersonalPWD = new HashSet<Adm_PersonalPWD>();
        }

        
        [Key]
        public int Id_Persona { get; set; }
        public string Nombre { get; set; }

        public string Password { get; set; }

        public short? id_grupo { get; set; }

        public int? id_EAdministrativa { get; set; }

        public int? id_EA_Persona { get; set; }

        public int id_EAdministrativa_Prov { get; set; }
        //public virtual Adm_PersonalExtendido Adm_PersonalExtendido { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adm_PersonalPWD> Adm_PersonalPWD { get; set; }
        //public virtual EstructurasAdministrativas EstructurasAdministrativas { get; set; }

    }
}
