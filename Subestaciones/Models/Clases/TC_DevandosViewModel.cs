using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class TC_DevandosViewModel
    {
        public ES_TransformadorCorriente ES_TransformadorCorriente  { get; set; }
        public List<ES_TC_Devanado> Devanados { get; set; }
    }
}