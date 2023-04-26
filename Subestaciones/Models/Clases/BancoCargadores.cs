using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class BancoCargadores
    {
        public string NombreServicio { get; set; }

        public int id_EAdministrativa { get; set; }

        public int NumAccion { get; set; }

        [Display(Name = "Modelo")]
        public string tipo { get; set; }

        [Display(Name = "Tipo Cargador")]
        public string tipoCargador { get; set; }

        [Display(Name = "Tensión CD")]
        public double? volCD { get; set; }

        [Display(Name = "Corriente (A)")]
        public double? corriente { get; set; }

        [Display(Name = "Tensión CA")]
        public double? volCA { get; set; }

        public int Id_Cargador { get; set; }

        public int Id_ServicioCDCarg { get; set; }

        [StringLength(5)]
        [Display(Name = "Estado Operativo")]
        public string EstOp { get; set; }

        [Display(Name = "Mes")]
        public int? MesFab { get; set; }

        [Display(Name = "Año Fab")]
        public int? AnnoFab { get; set; }

        [Display(Name = "Fabricante")]
        public string Fabricante { get; set; }

        [StringLength(50)]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [StringLength(50)]
        [Display(Name = "Nro Serie")]
        public string NroSerie { get; set; }

        [Display(Name = "Voltaje Máximo CD")]
        public double? VoltajeMaxCD { get; set; }

        [Display(Name = "Voltaje Mínimo CD")]
        public double? VoltajeMinCD { get; set; }

        [Display(Name = "Red CA")]
        public string Id_RedCA { get; set; }

        [Display(Name = "Instalado")]
        public DateTime? FechaInstalado { get; set; }
    }
}