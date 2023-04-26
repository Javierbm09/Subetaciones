namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_CelajeSubDistribucion
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30), Display(Name = "Tipo inspección")]
        public string nombreCelaje { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(7), Display(Name = "Subestación")]
        public string codigoSub { get; set; }

        [Key]
        [Column(Order = 2), Display(Name = "Fecha")]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }", ApplyFormatInEditMode = true)]
        public DateTime fecha { get; set; }

        [Display(Name = "Realizado por")]
        public short? realizadoPor { get; set; }

        public int? numAccion { get; set; }

        public short? id_EAdministrativa { get; set; }

        [StringLength(11)]
        public string dropOutAFaseA { get; set; }

        [StringLength(11)]
        public string dropOutBFaseA { get; set; }

        [StringLength(11)]
        public string dropOutAFaseB { get; set; }

        [StringLength(11)]
        public string dropOutBFaseB { get; set; }

        [StringLength(11)]
        public string dropOutAFaseC { get; set; }

        [StringLength(11)]
        public string dropOutBFaseC { get; set; }

        [StringLength(11)]
        public string dropOutBypassFaseA { get; set; }

        [StringLength(11)]
        public string dropOutBypassFaseB { get; set; }

        [StringLength(11)]
        public string dropOutBypassFaseC { get; set; }

        public bool? interruptorAltaSalidero { get; set; }

        [StringLength(6)]
        public string interruptorAltaNAceite { get; set; }

        [StringLength(7)]
        public string interruptorAltaPintura { get; set; }

        public int? interruptorAltaCuentaOP { get; set; }

        public bool? interruptorBajaSalidero { get; set; }

        [StringLength(6)]
        public string interruptorBajaNAceite { get; set; }

        [StringLength(7)]
        public string interruptorBajaPintura { get; set; }

        public int? interruptorBajaCuentaOP { get; set; }

        [Display(Name = "P.Rayos Alta")]
        [StringLength(8)]
        public string pRayosAlta { get; set; }

        [Display(Name = "P.Rayos")]
        [StringLength(8)]
        public string pRayosBaja { get; set; }

        [Display(Name = "Observaciones")]
        public string observaciones { get; set; }

        [StringLength(7)]
        [Display(Name = "Malla T. Sub.")]
        public string mallaTSub { get; set; }

        [StringLength(7)]
        [Display(Name = "Malla T. Cerca")]
        public string mallaTCerca { get; set; }

        [Display(Name = "Hierba")]
        public bool? hierba { get; set; }

        [Display(Name = "Desorden sub.")]
        public bool? desordenSub { get; set; }

        [StringLength(7)]
        [Display(Name = "Cerca")]
        public string estadoCerca { get; set; }

        [StringLength(7)]
        [Display(Name = "Puerta")]
        public string estadoPuerta { get; set; }

        public string otrasInformaciones { get; set; }

        [StringLength(20)]
        [Display(Name = "Carteles")]
        public string estadoCarteles { get; set; }

        [StringLength(20)]
        [Display(Name = "Alumbrado")]
        public string estadoAlumbrado { get; set; }

        [StringLength(20)]
        [Display(Name = "Candado puerta")]
        public string estadoCandadoPuerta { get; set; }

        [StringLength(10)]
        public string tipoCelaje { get; set; }
    }
}
