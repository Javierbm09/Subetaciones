using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Subestaciones.Models.Clases
{
    public class Pararrayos
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_EAdministrativa { get; set; }
        public string CodigoSub { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_pararrayo { get; set; }

        [Display(Name = "Tipo de equipo protegido")]
        public string EquipoProteg { get; set; }

        public string TipoEquipoProteg { get; set; }

        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Display(Name = "Subestación")]
        public string NombreSub { get; set; }

        [Display(Name = "Fabricante")]
        public string Fabricante { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Display(Name = "Voltaje instalado (kV)")]
        public string VInst { get; set; }

        [Display(Name = "Tensión nominal (kV)")]
        public double? tension { get; set; }

        [StringLength(4)]
        [Display(Name = "MOCV (kV)")]
        public string MOCV { get; set; }

        [Display(Name = "Corriente nominal (kA)")]
        public double? CorrienteN { get; set; }

        [StringLength(3)]
        [Display(Name = "Fase")]
        public string Fase { get; set; }

        [StringLength(50)]
        [Display(Name = "Inventario")]
        public string Inventario { get; set; }

        [StringLength(50)]
        [Display(Name = "Material")]
        public string Material { get; set; }

        [StringLength(5)]
        [Display(Name = "Frecuencia (Hz)")]
        public string Frecuencia { get; set; }

        [StringLength(30)]
        [Display(Name = "Aislamiento")]
        public string Aislamiento { get; set; }

        [StringLength(20)]
        [Display(Name = "Clase")]
        public string Clase { get; set; }

        [StringLength(4)]
        [Display(Name = "Año fabricación")]
        public short? AñoFabricacion { get; set; }

        [Display(Name = "Instalado")]
        public DateTime? Instalado { get; set; }

        [StringLength(30)]
        [Display(Name = "Número serie")]
        public string NumeroSerie { get; set; }
    }
}