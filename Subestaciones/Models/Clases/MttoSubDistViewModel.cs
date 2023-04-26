using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class MttoSubDistViewModel 
    {
        //public SubMttoSubDistribucion mtto { get; set; }

        public Sub_MttoDistTransf transf { get; set; }

        public List<Sub_MttoDistResistOhmica> resistO { get; set; }

        public List<Sub_MttoDistRelacTransformacion> relacT { get; set; }

        //public Sub_MttoDistribDesconectivos desc { get; set; }

        //public List<Sub_MttoDistPararrayos> para { get; set; }

        //public List<Sub_MttoDistBarra> barra { get; set; }



    }
}