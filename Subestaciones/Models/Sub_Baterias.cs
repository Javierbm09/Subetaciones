namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_Baterias
    {
        [Key]
        public int Id_Bateria { get; set; }

        [Display(Name = "Modelo"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public int? Tipo { get; set; }

        public int id_EAdministrativa { get; set; }

        public int NumAccion { get; set; }

        [Display(Name = "Cantidad de Unidades")]
        public short? CantidadVasos { get; set; }

        [Display(Name = "Tensión/Unidad")]
        [Column("Voltaje/Vasos")]
        public double? Voltaje_Vasos { get; set; }

        [Display(Name = "Densidad (mg/cm3)")]
        public float? Densisdad { get; set; }

        public int Id_ServicioCDBat { get; set; }

        [Display(Name = "Peso por Unidad")]
        public double? PesoCadaVaso { get; set; }

        [Display(Name = "Voltaje Flotación")]
        public double? VoltajeFlotacion { get; set; }

        [Display(Name = "Cant. Electrolitos/Unidad")]
        public int? CantElectVasos { get; set; }

        [Display(Name = "Cant. Electrolitos")]
        public int? CantElect { get; set; }

        [Display(Name = "Cant. Unidades Pilotos")]
        public int? CantVasosPilotos { get; set; }

        [Display(Name = "Mes Fabricación")]
        public int? MesFab { get; set; }

        [Display(Name = "Año Fabricación")]
        public int? AnnoFab { get; set; }

        [Display(Name = "Fabricante")]
        public int? Fabricante { get; set; }

        [Display(Name = "Voltaje Ecualizante (V)")]
        public double? VoltajeEcualizanteCargaBat { get; set; }

        [Display(Name = "Tiempo (h)")]
        public double? TiempoCargaBat { get; set; }

        [Display(Name = "Periodicidad (Meses)")]
        public int? PeriodicidadCargaBat { get; set; }

        [Display(Name = "Instalado")]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaInstalado { get; set; }
    }
}
