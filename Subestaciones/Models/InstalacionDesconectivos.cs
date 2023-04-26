namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InstalacionDesconectivos
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        public string Codigo { get; set; }

        [StringLength(7)]
        public string CodigoNuevo { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        public short? NumeroFases { get; set; }

        public short? CorrienteNominal { get; set; }

        [StringLength(1)]
        public string TipoInstalacion { get; set; }

        [StringLength(1)]
        public string TipoSeccionalizador { get; set; }

        [Required]
        [StringLength(7)]
        public string CircuitoA { get; set; }

        [StringLength(7)]
        public string SeccionA { get; set; }

        [StringLength(7)]
        public string CircuitoB { get; set; }

        [StringLength(7)]
        public string SeccionB { get; set; }

        [StringLength(1)]
        public string Funcion { get; set; }

        [StringLength(10)]
        public string AjusteOperacion { get; set; }

        public int? IdFusible { get; set; }

        [StringLength(50)]
        public string Calle { get; set; }

        [StringLength(6)]
        public string Numero { get; set; }

        [StringLength(50)]
        public string Entrecalle1 { get; set; }

        [StringLength(50)]
        public string EntreCalle2 { get; set; }

        [StringLength(50)]
        public string BarrioPueblo { get; set; }

        public short Sucursal { get; set; }

        [StringLength(7)]
        public string UbicadaEn { get; set; }

        public bool? TieneCorrienteA { get; set; }

        public bool? TieneCorrienteB { get; set; }

        public bool? TieneCorrienteC { get; set; }

        public bool? SiempreVoltaje { get; set; }

        public int? ClientesExtras { get; set; }

        [StringLength(7)]
        public string InstalacionAbrioA { get; set; }

        [StringLength(7)]
        public string InstalacionAbrioB { get; set; }

        [StringLength(7)]
        public string InstalacionAbrioC { get; set; }

        public DateTime? FechaAbrioA { get; set; }

        public DateTime? FechaAbrioB { get; set; }

        public DateTime? FechaAbrioC { get; set; }

        public int? coddireccion { get; set; }

        public short? Id_EAdireccion { get; set; }

        [StringLength(1)]
        public string EstadoOperativo { get; set; }

        public bool? EstadoFaseA { get; set; }

        public bool? EstadoFaseB { get; set; }

        public bool? EstadoFaseC { get; set; }

        public Guid? Tipo { get; set; }

        public short? Automatico { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_EAdministrativa_Prov { get; set; }

        public virtual InstalacionDesconectivos InstalacionDesconectivos1 { get; set; }

        public virtual InstalacionDesconectivos InstalacionDesconectivos2 { get; set; }
    }
}
