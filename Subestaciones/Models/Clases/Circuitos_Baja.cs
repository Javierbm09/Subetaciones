using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Subestaciones.Models.Clases
{
    public class Circuitos_Baja
    {

        [StringLength(7)]
        public string CodigoCircuito { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int id_EAdministrativa_Prov { get; set; }

        public int? NumAccion { get; set; }

        [StringLength(7)]
        public string SubAlimentadora { get; set; }

        [StringLength(7)]
        public string DesconectivoPrincipal { get; set; }

        public double? Kmslineas { get; set; }

    }
}