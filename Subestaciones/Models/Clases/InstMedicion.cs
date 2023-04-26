using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class InstMedicion
    {
        [Key]
        public int Id_InstrumentoMedicion { get; set; }

        [Display(Name = "Mediciones")]
        public string TipoMedicion { get; set; }

        [StringLength(100)]
        [Display(Name = "Instrumento")]
        public string Instrumento { get; set; }

        [StringLength(50)]
        [Display(Name = "Modelo o Tipo")]
        public string ModeloTipo { get; set; }

        [StringLength(30)]
        [Display(Name = "Serie")]
        public string Serie { get; set; }

        [StringLength(50)]
        [Display(Name = "Fabricante")]
        public string Fabricante { get; set; }

        [Display(Name = "Año de Fabricación")]
        public short? AnoFabricacion { get; set; }

        [StringLength(20)]
        [Display(Name = "País")]
        public string Pais { get; set; }

        [Display(Name = "Fecha Verificado")]
        public DateTime? FechaVerificado { get; set; }

        [Display(Name = "Fecha Vencimiento")]
        public DateTime? FechaVencimiento { get; set; }

        [Display(Name = "Estado")]
        [StringLength(20)]
        public string Estado { get; set; }

        [StringLength(100)]
        [Display(Name = "Brigada o Resposable")]
        public string BrigadaResponsable { get; set; }

        [StringLength(500)]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
    }
}