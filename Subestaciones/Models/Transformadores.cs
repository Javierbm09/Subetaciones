namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transformadores
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_EAdministrativa { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Transformador { get; set; }

        public int NumAccion { get; set; }

        [StringLength(7)]
        public string Codigo { get; set; }

        [StringLength(10)]
        [Display(Name = "Nro Empresa")]
        public string Numemp { get; set; }

        public short? Id_Capacidad { get; set; }

        [StringLength(15)]
        public string SimboloTaps { get; set; }

        public short? Id_VoltajePrim { get; set; }

        [StringLength(3)]
        public string Fase { get; set; }

        [StringLength(15)]
        [Display(Name = "Nro Serie")]
        public string NoSerie { get; set; }

        public bool NecesidadEmitida { get; set; }

        [StringLength(11)]
        public string CE { get; set; }

        [StringLength(12)]
        public string PosicionBanco { get; set; }

        [StringLength(1)]
        public string EstadoOperativo { get; set; }

        public short? TapEncontrado { get; set; }

        public short? TapDejado { get; set; }

        public short? NumFase { get; set; }

        public int? UltAccionVer { get; set; }

        public short? PerteneceA { get; set; }

        public short? Id_Voltaje_Secun { get; set; }

        public short? NSeccion { get; set; }

        public short? Numero { get; set; }

        [StringLength(50)]
        public string Tipoalimentacion { get; set; }

        public bool NVerificado { get; set; }

        [StringLength(12)]
        public string Marca { get; set; }

        public short? AñoFabricacion { get; set; }

        public short? Id_Fabricante { get; set; }

        public short? Frecuencia { get; set; }

        public short? Id_CorrienteN { get; set; }

        [StringLength(4)]
        public string TipoEnfriamiento { get; set; }

        [StringLength(15)]
        public string PolaridadGrupo { get; set; }

        public double? Impedancia { get; set; }

        public bool? Autoprotegido { get; set; }

        public short? id_GrupoVoltaje { get; set; }

        public double? Peso { get; set; }

        public double? PesoAislante { get; set; }

        public bool? UnBushingPrimario { get; set; }

        public bool? CuatroBushingSecundario { get; set; }

        public bool Tercero { get; set; }
    }
}
