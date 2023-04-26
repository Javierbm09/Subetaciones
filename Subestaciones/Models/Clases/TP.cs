using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Subestaciones.Models.Clases
{
    public class TP
    {
        [Key]
        [Display(Name = "Serie")]
        [StringLength(50)]
        public string Nro_Serie { get; set; }

        [StringLength(10)]
        [Display(Name = "Fase")]
        public string Fase { get; set; }

        [Display(Name = "Cantidad Devanados")]
        public short? Cant_Devanado { get; set; }

        [Display(Name = "Frecuencia (Hz)")]
        public double? Frecuencia { get; set; }

        [Display(Name = "Voltaje Instalado (V)")]
        public double? VoltInst { get; set; }

        [StringLength(7)]
        [Display(Name = "Subestación")]
        public string CodSub { get; set; }

        [StringLength(15)]
        [Display(Name = "Ubicado en")]
        public string UbicadoEn { get; set; }

        [StringLength(20)]
        [Display(Name = "Código Equipo")]
        public string CodigoE { get; set; }

        public int? Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        [StringLength(50)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [StringLength(50)]
        [Display(Name = "Inventario")]
        public string Inventario { get; set; }

        [Display(Name = "Año Fab")]
        public short? AnnoFab { get; set; }

        [Display(Name = "Fecha Instalado")]
        public DateTime? FechaInstalado { get; set; }

        [Display(Name = "Peso (kg)")]
        public double? Peso { get; set; }

        [StringLength(50)]
        [Display(Name = "Fabricante")]
        public string Fabricante { get; set; }
    }
}