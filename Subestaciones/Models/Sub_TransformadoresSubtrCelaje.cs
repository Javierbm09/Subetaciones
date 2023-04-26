namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_TransformadoresSubtrCelaje
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string nombreCelaje { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(7)]
        public string codigoSub { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime fecha { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_EAdministrativa { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Transformador { get; set; }

        public bool? salidero { get; set; }

        [StringLength(6), Display(Name = "Nivel aceite")]
        public string nAceite { get; set; }

        [StringLength(7), Display(Name = "Pintura")]
        public string pintura { get; set; }

        [StringLength(7), Display(Name = "Aterramientos")]
        public string aterramiento { get; set; }

        [StringLength(7), Display(Name = "Bushing")]
        public string bushing { get; set; }

        [Display(Name = "Temperatura real")]
        public int? temperaturaReal { get; set; }

        [Display(Name = "Observaciones")]
        public string observaciones { get; set; }
    }
}
