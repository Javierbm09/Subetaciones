namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EstructurasAdministrativas
    {
        [Key]
        [Column(Order = 0)]
        public int Id_EAdministrativa { get; set; }

        [StringLength(35)]
        public string Nombre { get; set; }

        [StringLength(40)]
        public string Dir_Calle { get; set; }

        [StringLength(7)]
        public string Dir_Numero { get; set; }

        [StringLength(40)]
        public string Dir_Entre1 { get; set; }

        [StringLength(40)]
        public string Dir_Entre2 { get; set; }

        [StringLength(20)]
        public string Dir_Barrio { get; set; }

        [StringLength(50)]
        public string Jefe { get; set; }

        [StringLength(1)]
        public string Codigo { get; set; }

        public short? Tipo { get; set; }

        public int? Subordinada { get; set; }

        [StringLength(15)]
        public string N_Telefono { get; set; }

        [StringLength(15)]
        public string N_Fax { get; set; }

        [StringLength(50)]
        public string E_Mail { get; set; }

        public double? Km_Linea { get; set; }

        public int? N_Consumidores { get; set; }

        public short? KVAInstalados { get; set; }

        public bool Calculado { get; set; }

        public int? PerteneceA { get; set; }

        public short? EstructuraAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        public int? Centro_de_costo { get; set; }

        public int? coddireccion { get; set; }

        public short? Id_EAdireccion { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_EAdministrativa_Prov { get; set; }
    }
}
