namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoTFuerzaConmutador
    {
        public int Id { get; set; }

        public int Id_MttoTFuerza { get; set; }

        [StringLength(1)]
        public string LimpElimSalidero { get; set; }

        [StringLength(1)]
        public string PintPartesOxidadas { get; set; }

        [StringLength(1)]
        public string MantAccesorios { get; set; }

        public string OtrasMediciones { get; set; }

        [StringLength(50)]
        public string EstadoAceiteConmutador { get; set; }

        [StringLength(1)]
        public string CambioAceiteConmutador { get; set; }

        [StringLength(50)]
        public string EstadoConmutador { get; set; }

        public int? NroCuentaOperaciones { get; set; }

        public bool? MttoMecanismoTransm { get; set; }
    }
}
