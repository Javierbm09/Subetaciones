using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class TermografiasViewModel
    {
        public Sub_Termografias termografias { get; set; }

        public List<Sub_PuntoTermografia> ptosCalientes { get; set; }

    }
}