namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_CelajeSubDistInterruptor
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        public string codigoInterruptor { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30), Display(Name ="Tipo inspección")]
        public string nombreCelaje { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(7)]
        public string codigoSub { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime fecha { get; set; }

        [Display(Name = "Salidero")]
        public bool? Salidero { get; set; }

        [StringLength(6)]
        [Display(Name = "Aceite")]
        public string NAceite { get; set; }

        [StringLength(7)]
        public string Pintura { get; set; }

        [Display(Name = "Cuenta operaciones")]
        public int? cuentaOP { get; set; }

        [StringLength(10)]
        [Display(Name = "Batería")]
        public string estadoBateria { get; set; }

        [StringLength(10)]
        [Display(Name = "Candado gabinete")]
        public string candadoGabinete { get; set; }

        [StringLength(5)]
        public string tipoInterruptor { get; set; }

        public short? id_EAdministrativa { get; set; }
    }
}
