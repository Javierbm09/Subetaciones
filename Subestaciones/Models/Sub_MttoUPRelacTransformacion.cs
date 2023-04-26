using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subestaciones.Models
{
    public partial class Sub_MttoUPRelacTransformacion
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        public string CodigoSub { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "smalldatetime")]
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

        public virtual Sub_MttoTransUsoP Sub_MttoTransUsoP { get; set; }
    }
}