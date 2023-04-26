using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subestaciones.Models
{
    public class Sub_NomAspectoInspSubTrans
    {
        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Aspecto { get; set; }

        [StringLength(50)]
        public string Aspecto { get; set; }
    }
}