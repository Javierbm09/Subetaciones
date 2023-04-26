namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoDistRelacTransformacion
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        public string CodigoSub { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Fecha { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Transformador { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_EATransformador { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short NroTap { get; set; }

        [StringLength(30)]
        public string LabelFaseA { get; set; }

        [StringLength(30)]
        public string LabelFaseB { get; set; }

        [StringLength(30)]
        public string LabelFaseC { get; set; }

        public double? RelacTransFaseA { get; set; }

        public double? RelacTransFaseB { get; set; }

        public double? RelacTransFaseC { get; set; }

        public double? ValorEsperado { get; set; }

        [NotMapped]
        public double? RTdifAB { get; set; }

        [NotMapped]
        public double? RTdifBC { get; set; }

        [NotMapped]
        public double? RTdifCA { get; set; }

        public virtual Sub_MttoDistTransf Sub_MttoDistTransf { get; set; }
    }
}
