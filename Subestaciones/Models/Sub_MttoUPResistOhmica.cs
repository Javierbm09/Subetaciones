using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subestaciones.Models
{
    public partial class Sub_MttoUPResistOhmica
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

        [StringLength(10)]
        public string LabelFaseAPrim { get; set; }

        [StringLength(10)]
        public string LabelFaseBPrim { get; set; }

        [StringLength(10)]
        public string LabelFaseCPrim { get; set; }

        public double? ResistOhmFaseAPrim { get; set; }

        public double? ResistOhmFaseBPrim { get; set; }

        public double? ResistOhmFaseCPrim { get; set; }

        [StringLength(10)]
        public string LabelFaseASec { get; set; }

        [StringLength(10)]
        public string LabelFaseBSec { get; set; }

        [StringLength(10)]
        public string LabelFaseCSec { get; set; }

        public double? ResistOhmFaseASec { get; set; }

        public double? ResistOhmFaseBSec { get; set; }

        public double? ResistOhmFaseCSec { get; set; }

        public virtual Sub_MttoTransUsoP Sub_MttoTransUsoP { get; set; }
    }
}