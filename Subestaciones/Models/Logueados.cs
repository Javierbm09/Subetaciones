using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Subestaciones.Models
{
    [Table("Logueados")]
    public class Logueados
    {
        [Key, Column(Order = 1)]
        public short Modulo { get; set; }
        [Key, Column(Order = 2)]
        public string Maquina { get; set; }
        public string Usuario { get; set; }
    }
}


