using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Sub_MttoBateriasEstacionariasList
    {
        public int id_MttoBatEstacionarias { get; set; }

        public int id_Bateria { get; set; }

        public short EA_RedCD { get; set; }

        public DateTime? fechaMtto { get; set; }

        public string subestacion { get; set; }

        public string RealizadoPor { get; set; }

        public string TipoMtto { get; set; }

        public string Mantenido { get; set; }

        public string NombreSubestacion { get; set; }
    }
}