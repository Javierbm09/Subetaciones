namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TipoGrupos_Sigere
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short ID_EAdministrativa { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumAccion { get; set; }

        public Guid id_tipo { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public int Fabricante { get; set; }

        public int? AnnoFabric { get; set; }

        [StringLength(50)]
        public string codigo_Fabric { get; set; }

        public int num_fases { get; set; }

        [Required]
        [StringLength(50)]
        public string Normas { get; set; }

        [Required]
        [StringLength(15)]
        public string grado_Prot { get; set; }

        [StringLength(50)]
        public string Clase_Termica { get; set; }

        [StringLength(50)]
        public string Regimen_Trab { get; set; }

        public int Eval_Salida { get; set; }

        public double Voltaje { get; set; }

        [Required]
        [StringLength(50)]
        public string frecuencia { get; set; }

        public int corriente { get; set; }

        public int velocidad { get; set; }

        public int? sobrevelocidad { get; set; }

        public int? Voltaje_Campo { get; set; }

        public double factor_Potencia { get; set; }

        public int? Rat_RotorBobina { get; set; }

        public int? Cod_ConvertStatic { get; set; }

        public int? Max_TempAmb { get; set; }

        public int? Max_TempRefrige { get; set; }

        public int? Min_TempAmb { get; set; }

        public int? Altitud_NivelMar { get; set; }

        public int? Presion_Hidrog { get; set; }

        public int? PesoTotal { get; set; }

        [StringLength(50)]
        public string Direccion_Rotacion { get; set; }

        [StringLength(50)]
        public string Instr_Conexion { get; set; }

        public double? Potencia { get; set; }

        public double? Factor_Carga { get; set; }
    }
}
