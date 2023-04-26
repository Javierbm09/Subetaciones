namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LugarHabitado")]
    public partial class LugarHabitado
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_LHab { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_EAdministrativa { get; set; }

        [StringLength(35)]
        public string nombre { get; set; }

        public short? Tipo { get; set; }

        public short? id_eadpertenecea { get; set; }

        public short? Pertenecea { get; set; }

        public int? NumAccion { get; set; }

        public int? Id_Cartografia { get; set; }

        public short? Id_Sucursal { get; set; }
    }
}
