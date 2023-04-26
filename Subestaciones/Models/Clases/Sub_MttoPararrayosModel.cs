namespace Subestaciones.Models.Clases
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoPararrayosModel
    {
        public Sub_MttoPararrayos MttoPararrayos { get; set; }

        public Sub_MttoPararrayos_Fases MttoPararrayos_FaseA { get; set; }

        public Sub_MttoPararrayos_Fases MttoPararrayos_FaseB { get; set; }

        public Sub_MttoPararrayos_Fases MttoPararrayos_FaseC { get; set; }

        public List<int> InstrumentosUtilizados { get; set; }

        [Display(Name = "Nº Serie")]
        public string NumSerieFaseA { get; set; }

        [Display(Name = "Nº Serie")]
        public string NumSerieFaseB { get; set; }

        [Display(Name = "Nº Serie")]
        public string NumSerieFaseC { get; set; }

        [Display(Name = "Tipo Pararrayo")]
        public string TipoParaFaseA { get; set; }

        [Display(Name = "Tipo Pararrayo")]
        public string TipoParaFaseB { get; set; }

        [Display(Name = "Tipo Pararrayo")]
        public string TipoParaFaseC { get; set; }
    }
}
