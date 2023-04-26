using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class InspSubDist
    {

        [StringLength(30)]
        public string nombreCelaje { get; set; }

        
        [StringLength(7)]
        public string codigoSub { get; set; }

        public string nombreSub { get; set; }

        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime fecha { get; set; }

        public string realizadoPor { get; set; }

        public int? numAccion { get; set; }

        public int id_EAdministrativa { get; set; }

        [StringLength(11)]
        public string dropOutAFaseA { get; set; }

        [StringLength(11)]
        public string dropOutBFaseA { get; set; }

        [StringLength(11)]
        public string dropOutAFaseB { get; set; }

        [StringLength(11)]
        public string dropOutBFaseB { get; set; }

        [StringLength(11)]
        public string dropOutAFaseC { get; set; }

        [StringLength(11)]
        public string dropOutBFaseC { get; set; }

        [StringLength(11)]
        public string dropOutBypassFaseA { get; set; }

        [StringLength(11)]
        public string dropOutBypassFaseB { get; set; }

        [StringLength(11)]
        public string dropOutBypassFaseC { get; set; }

        public bool? interruptorAltaSalidero { get; set; }

        [StringLength(6)]
        public string interruptorAltaNAceite { get; set; }

        [StringLength(7)]
        public string interruptorAltaPintura { get; set; }

        public int? interruptorAltaCuentaOP { get; set; }

        public bool? interruptorBajaSalidero { get; set; }

        [StringLength(6)]
        public string interruptorBajaNAceite { get; set; }

        [StringLength(7)]
        public string interruptorBajaPintura { get; set; }

        public int? interruptorBajaCuentaOP { get; set; }

        [StringLength(8)]
        public string pRayosAlta { get; set; }

        [StringLength(8)]
        public string pRayosBaja { get; set; }

        public string observaciones { get; set; }

        [StringLength(7)]
        public string mallaTSub { get; set; }

        [StringLength(7)]
        public string mallaTCerca { get; set; }

        public bool? hierba { get; set; }

        public bool? desordenSub { get; set; }

        [StringLength(7)]
        public string estadoCerca { get; set; }

        [StringLength(7)]
        public string estadoPuerta { get; set; }

        public string otrasInformaciones { get; set; }

        [StringLength(20)]
        public string estadoCarteles { get; set; }

        [StringLength(20)]
        public string estadoAlumbrado { get; set; }

        [StringLength(20)]
        public string estadoCandadoPuerta { get; set; }

        [StringLength(10)]
        public string tipoCelaje { get; set; }

    }
}