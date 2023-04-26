using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class Sub_CelajeViewModel
    {

        public Personal personal { get; set; }

        public Sub_Celaje sub_Celaje { get; set; }

        public List<Sub_InspAspectosSubTransViewModel> sub_InspAspectosSubTransViewModel { get; set; }

        public Sub_Celaje_Sub_TransViewModel sub_Celaje_Sub_TransViewModel { get; set; }

    }
}