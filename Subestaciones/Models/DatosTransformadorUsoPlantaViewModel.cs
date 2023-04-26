using System.ComponentModel.DataAnnotations;

namespace Subestaciones.Models
{
    public class DatosTransformadorUsoPlantaViewModel
    {
        [StringLength(15)]
        [Display(Name = "Nro Serie")]
        public string NoSerie { get; set; }

        [StringLength(15)]
        [Display(Name = "Fabricante")]
        public string Fabricante { get; set; }

        [StringLength(20)]
        [Display(Name = "Tensión")]
        public string Tension { get; set; }

        [StringLength(20)]
        [Display(Name = "Impedancia")]
        public double? Impedancia { get; set; }

        [Display(Name = "Capacidad")]
        public double Capacidad { get; set; }

        [Display(Name = "Peso Aislante")]
        public double? PesoAislante { get; set; }

        [Display(Name = "Peso")]
        public double? Peso { get; set; }

        [Display(Name = "Tap Encontrado")]
        public short? TapEncontrado { get; set; }

        [Display(Name = "Tap Dejado")]
        public short? TapDejado { get; set; }

        [Display(Name = "Grupo Conexión")]
        public string PolaridadGrupo { get; set; }

        public short Id_EAdministrativa { get; set; }

        public int Id_Transformador { get; set; }

        public short Id_EATransformador { get; set; }

    }
}