namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PortaFusibles
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_EAdministrativa { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Portafusible { get; set; }

        [NotMapped]
        [Display(Name = "Tipo de portafusible")]

        public int tipoPortafusible { get; set; }

        public int NumAccion { get; set; }

        [StringLength(7)]
        [Display(Name = "Código")]
        public string CodigoPortafusible { get; set; }

        [StringLength(7)]
        public string Codigo { get; set; }

        [Display(Name = "Tipo")]
        public short? TipoFuse { get; set; }

        [StringLength(13)]
        public string TEquipoProt { get; set; }

        [StringLength(11)]
        public string CE { get; set; }

        [StringLength(15)]
        public string Estado { get; set; }

        [StringLength(3)]
        public string Fase { get; set; }

        [StringLength(20)]
        public string Ubicacion { get; set; }

        [StringLength(1)]
        public string EstadoOperativo { get; set; }

        [StringLength(12)]
        public string Marca { get; set; }


        [Display(Name = "Tensión (kV)")]

        public short? Id_VoltajeN { get; set; }

        public short? CorrienteNominal { get; set; }

        public double? FusibleAjuste { get; set; }

        public double? CorrienteCortoCto { get; set; }

        public int? Id_Fabricante { get; set; }

        [Display(Name = "Tipo")]
        public short? Id_Fusible { get; set; }

        [StringLength(1)]
        [Display(Name = "Tipo")]
        public string TipoFusible { get; set; }

        public short? Id_FabricanteFusible { get; set; }


        [Display(Name = "Capacidad (A)")]
        public int? CapacidadFusible { get; set; }

        public int id_EAdministrativa_Prov { get; set; }
    }
}
