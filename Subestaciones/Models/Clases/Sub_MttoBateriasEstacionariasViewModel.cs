using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Sub_MttoBateriasEstacionariasViewModel
    {
        public Sub_MttoBateriasEstacionarias Sub_MBE { get; set; }
        public List<Sub_MttoBateriasEstacionarias_Vasos> Sub_MBEV { get; set; }
        public DatosChapaBancoBateriaViewModel DatosCBBVM { get; set; }
        public Sub_MttoBateriaEstac_Instrumentos Sub_MBEI { get; set; }

        //public Sub_Baterias Sub_B { get; set; }

        //public NomencladorBaterias Sub_NB { get; set; }

        //public Sub_RedCorrienteDirecta Sub_RCD { get; set; }


        //public Sub_NomInstrumentoMedicion Sub_NIM { get; set; }

    }
}