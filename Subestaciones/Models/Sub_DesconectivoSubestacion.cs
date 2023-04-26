namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    
    public partial class Sub_DesconectivoSubestacion
    {
        [NotMapped]
        public string CodDescAnterior { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RedCA { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(7)]
        [Display(Name = "C�digo Subestacion")]
        public string CodigoSub { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(7)]
        [Display(Name = "C�digo"), Required(AllowEmptyStrings = false, ErrorMessage = "Debe insertar un c�digo")]        
        public string CodigoDesconectivo { get; set; }

        [Display(Name = "Tensi�n Nominal"), Required( ErrorMessage = "Debe insertar una Tensi�n Nominal")]
        public double? TensionNominal { get; set; }

        [Display(Name = "Corriente Nominal"), Required(ErrorMessage = "Debe insertar una Corriente Nominal")]
        public double? CorrienteNominal { get; set; }

        public virtual Sub_RedCorrienteAlterna Sub_RedCorrienteAlterna { get; set; }
    }

}