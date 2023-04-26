using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class MedicionesTierra
    {
        public string Subestacion { get; set; }

        public string SubestacionNombre { get; set; }

        public DateTime Fecha { get; set; }

        public short Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        public string RealizadaPor { get; set; }

        public string Empresa { get; set; }

    }
}