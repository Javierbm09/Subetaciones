using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Sub_MttoTFuerzaList
    {
        public int Id_MttoTFuerza { get; set; }

        public int Id_TFuerza { get; set; }

        public int Id_EAdministrativaTFuerza { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public DateTime? fechaMtto { get; set; }

        public string subestacion { get; set; }

        public string RealizadoPor { get; set; }

        public string TipoMtto { get; set; }

        public string Mantenido { get; set; }

        public string NombreSubestacion { get; set; }
    }
}