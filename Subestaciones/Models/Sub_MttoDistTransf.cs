namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoDistTransf
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sub_MttoDistTransf()
        {
            Sub_MttoDistRelacTransformacion = new HashSet<Sub_MttoDistRelacTransformacion>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        public string CodigoSub { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Fecha { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Transformador { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_EATransformador { get; set; }

        [Display(Name ="Tap encontrado")]
        public short? Tap_Encontrado { get; set; }

        [Display(Name ="Tap dejado")]
        public short? Tap_Dejado { get; set; }

        [StringLength(20)]
        [Display(Name ="Tanque expansión")]
        public string EstadoTanqueExpansion { get; set; }

        [StringLength(20)]
        [Display(Name = "Busholtz/Relé integral")]
        public string EstadoBusholtz_ReleIntegral { get; set; }

        [StringLength(20)]
        [Display(Name = "Bushing alta")]
        public string EstadoBushingAlta { get; set; }

        [StringLength(20)]
        [Display(Name = "Bushing baja")]
        public string EstadoBushingBaja { get; set; }

        [StringLength(20)]
        [Display(Name = "Tubo Explosor")]
        public string TuboExplosor { get; set; }

        [StringLength(20)]
        [Display(Name = "Válvula sobrepresión")]
        public string EstadoValvulaSobrePresion { get; set; }

        [StringLength(20)]
        [Display(Name = "Válvulas")]
        public string EstadoValvulas { get; set; }

        [StringLength(20)]
        [Display(Name = "Radiadores")]
        public string EstadoRadiadores { get; set; }

        [StringLength(20)]
        [Display(Name = "Salideros o Resumid.")]
        public string SaliderosResumidores { get; set; }

        [Display(Name = "Temp.Amb (°C)")]
        public double? TempAmbiente { get; set; }
        
        [Display(Name = "Temp.Transf(°C)")]
        public double? TempTransformador { get; set; }

        [Display(Name = "% Hum. Relativa")]
        public double? HumedadRelativa { get; set; }

        [StringLength(50)]
        [Display(Name ="Estado tiempo")]
        public string EstadoTiempo { get; set; }

        [Display(Name = "Tubo Explosor")]
        public double? ResistAislABTR15 { get; set; }

        [Display(Name = "Tubo Explosor")]
        public double? ResistAislBATR15 { get; set; }

        [Display(Name = "Tubo Explosor")]
        public double? ResistAislABR15 { get; set; }

        [Display(Name = "Tubo Explosor")]
        public double? ResistAislABTR60 { get; set; }

        [Display(Name = "Tubo Explosor")]
        public double? ResistAislABR60 { get; set; }

        [StringLength(100)]
        [Display(Name = "Instrumento utilizado")]
        public string InstUtilizadoResistAisl { get; set; }

        [StringLength(20)]
        [Display(Name = "Indicador nivel aceite")]
        public string EstadoIndNivelaceite { get; set; }

        [StringLength(20)]
        [Display(Name = "Nivel aceite")]
        public string NivelAceite { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado sílica")]
        public string EstadoSilica { get; set; }

        [StringLength(20)]
        [Display(Name = "Termómetro")]
        public string EstadoTermometro { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado pintura")]
        public string EstadoPintura { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado ventilación")]
        public string EstadoVentilacion { get; set; }

        [StringLength(20)]
        [Display(Name = "Conexiones")]
        public string EstadoConexiones { get; set; }

        [StringLength(20)]
        [Display(Name = "Aterramiento tanque")]
        public string AterramientoTanque { get; set; }

        [StringLength(20)]
        [Display(Name = "Aterramiento neutro")]
        public string AterramientoNeutro { get; set; }

        [StringLength(100)]
        [Display(Name = "Tubo Explosor")]
        public string InstUtilizadoResistOhm { get; set; }

        [StringLength(100)]
        [Display(Name = "Tubo Explosor")]
        public string InstUtiliRelacTransf { get; set; }

        [StringLength(500)]
        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }

        [Display(Name = "Prueba1")]
        [Range(0,99, ErrorMessage = "Solo puede insertar números")]
        public double? RigidezDielectricaPrueba1 { get; set; }

        [Display(Name = "Prueba2")]
        public double? RigidezDielectricaPrueba2 { get; set; }

        [Display(Name = "Prueba3")]
        public double? RigidezDielectricaPrueba3 { get; set; }

        [Display(Name = "Prueba4")]
        public double? RigidezDielectricaPrueba4 { get; set; }

        [Display(Name = "Prueba5")]
        public double? RigidezDielectricaPrueba5 { get; set; }

        [Display(Name = "Promedio")]
        public double? PromedioRigidezDielectrica { get; set; }
        

        [Display(Name = "Temperatura Ambiente")]
        public double? TempAmbRigidezDielectrica { get; set; }

        [Display(Name = "Temp. Transformador")]
        public double? TempTransfRigidezDielectrica { get; set; }

        [Display(Name = "Humedad Relativa")]
        public double? HumRelatRigidezDielectrica { get; set; }

        [StringLength(50)]
        [Display(Name = "Norma utilizada")]
        public string NormaRigidezDielectrica { get; set; }

        [StringLength(100)]
        [Display(Name = "Instrumento utilizado")]
        public string InstUtilRigidezDielectrica { get; set; }

        [Display(Name = "Prueba6")]
        public double? RigidezDielectricaPrueba6 { get; set; }

        [Display(Name = "Tubo Explosor")]
        public double? ResistAislBATR60 { get; set; }

        [Display(Name = "Valor esperado")]
        public double? ValorEsperadoRelacTransf { get; set; }

        [StringLength(10)]
        [Display(Name = "Tubo Explosor")]
        public string EstadoPrimVsSecTierra { get; set; }

        [StringLength(10)]
        [Display(Name = "Tubo Explosor")]
        public string EstadoSecVsPrimTierra { get; set; }

        [StringLength(10)]
        [Display(Name = "Tubo Explosor")]
        public string EstadoPrimSecVsTierra { get; set; }

        [NotMapped]
        public string KPrimVsSecTierra { get; set; }

        [NotMapped]
        public string KSecVsPrimTierra { get; set; }

        [NotMapped]
        public string KPrimSecVsTierra { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sub_MttoDistRelacTransformacion> Sub_MttoDistRelacTransformacion { get; set; }
    }
}
