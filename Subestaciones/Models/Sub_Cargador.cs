namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_Cargador
    {
        public int id_EAdministrativa { get; set; }

        public int NumAccion { get; set; }

        [Display(Name = "Modelo"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public short? tipo { get; set; }

        [Key]
        public int Id_Cargador { get; set; }


        public int Id_ServicioCDCarg { get; set; }

        [Display(Name = "Estado Operativo")]
        [StringLength(5)]
        public string EstOp { get; set; }

        [Display(Name = "Mes Fabricación")]
        public int? MesFab { get; set; }

        [Display(Name = "Año Fabricación")]
        public int? AnnoFab { get; set; }

        [Display(Name = "Fabricante")]
        public int? Fabricante { get; set; }

        [Display(Name = "Modelo")]
        [StringLength(50)]
        public string Modelo { get; set; }

        [Display(Name = "No Serie")]
        [StringLength(50)]
        public string NroSerie { get; set; }

        [Display(Name = "Voltaje Máximo CD")]
        public double? VoltajeMaxCD { get; set; }

        [Display(Name = "Voltaje Mínimo CD")]
        public double? VoltajeMinCD { get; set; }

        [Display(Name = "Red CA")]
        public int? Id_RedCA { get; set; }

        [Display(Name = "Instalado")]
        public DateTime? FechaInstalado { get; set; }
    }
}
