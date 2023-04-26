using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Protecciones
    {
        public string nombre { get; set; }

        public string equipoPriamrio { get; set; }

        public string elementoElectrico { get; set; }

        public char tipoEquipo { get; set; }

        public string rele { get; set; }

        public string descripcion { get; set; }

        public int devanado { get; set; }

        public int anno { get; set; }
    }
}