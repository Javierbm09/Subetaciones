using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subestaciones.Models
{
    public partial class Sub_MttoTransUsoP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sub_MttoTransUsoP()
        {
            Sub_MttoUPRelacTransformacion = new HashSet<Sub_MttoUPRelacTransformacion>();
            Sub_MttoUPResistOhmica = new HashSet<Sub_MttoUPResistOhmica>();
        }

        public int? Id_Eadministrativa { get; set; }

        public int Numaccion { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        [Display(Name = "Codigo Subestación")]
        public string CodigoSub { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "smalldatetime")]
        public DateTime Fecha { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Transformador { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id_EATransformador { get; set; }

        [Display(Name = "Tipo Mantenimiento:")]
        public short TipoMantenimiento { get; set; }

        [Required]
        [Display(Name = "Revisado Por:")]
        public short EjecutadoPor { get; set; }

        public bool? Mantenido { get; set; }

        public short? Tap_Encontrado { get; set; }

        public short? Tap_Dejado { get; set; }

        [StringLength(20)]
        [Display(Name = "Tanque Expansión:")]
        public string EstadoTanqueExpansion { get; set; }

        [StringLength(20)]
        [Display(Name = "Busholtz/Relé Integral:")]
        public string EstadoBusholtz_ReleIntegral { get; set; }

        [StringLength(20)]
        [Display(Name = "Bushing Alta:")]
        public string EstadoBushingAlta { get; set; }

        [StringLength(20)]
        [Display(Name = "Bushing Baja:")]
        public string EstadoBushingBaja { get; set; }

        [StringLength(20)]
        [Display(Name = "Tubo Explosor:")]
        public string TuboExplosor { get; set; }

        [StringLength(20)]
        [Display(Name = "Válvula sobrepresión:")]
        public string EstadoValvulaSobrePresion { get; set; }

        [StringLength(20)]
        [Display(Name = "Válvulas:")]
        public string EstadoValvulas { get; set; }

        [StringLength(20)]
        [Display(Name = "Radiadores:")]
        public string EstadoRadiadores { get; set; }

        [StringLength(20)]
        [Display(Name = "Salideros o Resumidores:")]
        public string SaliderosResumidores { get; set; }

        [Display(Name = "Temperatura Ambiente:")]
        public double? TempAmbiente { get; set; }

        [Display(Name = "Temp Transformador:")]
        public double? TempTransformador { get; set; }

        [Display(Name = "Humedad relativa:")]
        public double? HumedadRelativa { get; set; }

        [StringLength(50)]
        public string EstadoTiempo { get; set; }

        public double? ResistAislABTR15 { get; set; }

        public double? ResistAislBATR15 { get; set; }

        public double? ResistAislABR15 { get; set; }

        public double? ResistAislABTR60 { get; set; }

        public double? ResistAislABR60 { get; set; }

        public short? InstUtilizadoResistAisl { get; set; }

        [StringLength(20)]
        [Display(Name = "Indicador nivel aceite:")]
        public string EstadoIndNivelaceite { get; set; }

        [StringLength(20)]
        [Display(Name = "Nivel aceite:")]
        public string NivelAceite { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado sílica:")]
        public string EstadoSilica { get; set; }

        [StringLength(20)]
        [Display(Name = "Termómetro:")]
        public string EstadoTermometro { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado pintura:")]
        public string EstadoPintura { get; set; }

        [StringLength(20)]
        [Display(Name = "Estado ventilación:")]
        public string EstadoVentilacion { get; set; }

        [StringLength(20)]
        [Display(Name = "Conexiones:")]
        public string EstadoConexiones { get; set; }

        [StringLength(20)]
        [Display(Name = "Aterramiento Tanque:")]
        public string AterramientoTanque { get; set; }

        [StringLength(20)]
        [Display(Name = "Aterramiento Neutro:")]
        public string AterramientoNeutro { get; set; }

        [StringLength(100)]
        public string InstUtilizadoResistOhm { get; set; }

        [StringLength(100)]
        public string InstUtiliRelacTransf { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        [Display(Name = "Prueba 1:")]
        public short? RigidezDielectricaPrueba1 { get; set; }

        [Display(Name = "Prueba 2:")]
        public short? RigidezDielectricaPrueba2 { get; set; }
        [Display(Name = "Prueba 3:")]
        public short? RigidezDielectricaPrueba3 { get; set; }
        [Display(Name = "Prueba 4:")]
        public short? RigidezDielectricaPrueba4 { get; set; }
        [Display(Name = "Prueba 5:")]
        public short? RigidezDielectricaPrueba5 { get; set; }
        [Display(Name = "Promedio:")]
        public double? PromedioRigidezDielectrica { get; set; }

        public double? TempAmbRigidezDielectrica { get; set; }

        public double? TempTransfRigidezDielectrica { get; set; }

        public double? HumRelatRigidezDielectrica { get; set; }

        [StringLength(50)]
        [Display(Name = "Norma utilizada:")]
        public string NormaRigidezDielectrica { get; set; }

        [Display(Name = "Instrumento utilizado:")]
        public short? InstUtilRigidezDielectrica { get; set; }

        [Display(Name = "Prueba 5:")]
        public short? RigidezDielectricaPrueba6 { get; set; }

        public double? ResistAislBATR60 { get; set; }

        public double? ValorEsperadoRelacTransf { get; set; }

        [StringLength(10)]
        public string EstadoPrimVsSecTierra { get; set; }

        [StringLength(10)]
        public string EstadoSecVsPrimTierra { get; set; }

        [StringLength(10)]
        public string EstadoPrimSecVsTierra { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sub_MttoUPRelacTransformacion> Sub_MttoUPRelacTransformacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sub_MttoUPResistOhmica> Sub_MttoUPResistOhmica { get; set; }

        [NotMapped]
        public string MensajeExistenciaMtto { get; set; }
    }
}