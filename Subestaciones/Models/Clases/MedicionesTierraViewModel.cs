using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class MedicionesTierraViewModel
    {

        public Sub_MedicionTierra Mediciones { get; set; }


        public List<Sub_MedicionTierra_CaidaPotencial> CaidaPotencial { get; set; }

        public List<Sub_MedicionTierra_ContinuidadMalla> ContinuidadMalla { get; set; }

    }
}