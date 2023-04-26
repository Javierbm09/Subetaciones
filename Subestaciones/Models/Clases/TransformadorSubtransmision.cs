using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class TransformadorSubtransmision
    {
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_EAdministrativa { get; set; }


        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Transformador { get; set; }

        public int NumAccion { get; set; }

        [StringLength(7)]
        public string Subestacion { get; set; }

        [Display(Name = "Ubicación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Codigo { get; set; }


        [StringLength(10)]
        [Display(Name ="Nro. Empresa")]
        public string Numemp { get; set; }

        [Display(Name = "Capacidad")]
        public double? capacidad { get; set; }

        //[StringLength(15)]
        //public string SimboloTaps { get; set; }

        [Display(Name = "Tensión primaria")]
        public short? voltajePrimario { get; set; }

        [StringLength(3)]
        [Display(Name = "Fase")]
        public string Fase { get; set; }

        [StringLength(15)]
        [Display(Name = "Nro. Serie")]
        public string NoSerie { get; set; }

        
        //public bool NecesidadEmitida { get; set; }

        //[StringLength(11)]
        //public string CE { get; set; }

        //[StringLength(12)]
        //public string PosicionBanco { get; set; }

        [StringLength(1)]
        [Display(Name = "Estado Operativo")]
        public string EstadoOperativo { get; set; }

        //public short? TapEncontrado { get; set; }

        //public short? TapDejado { get; set; }

        [Display(Name = "Nro. Fase")]
        public short? NumFase { get; set; }


        //public int? UltAccionVer { get; set; }

        //[StringLength(10)]
        //public string EstadoHermeticidad { get; set; }

        //[StringLength(15)]
        //public string EstadoPinturaTanque { get; set; }

        //[StringLength(10)]
        //public string EstadoPinturaRotulos { get; set; }

        //[StringLength(6)]
        //public string AcidezAceite { get; set; }

        //[StringLength(6)]
        //public string NivelAceite { get; set; }

        //[StringLength(6)]
        //public string ColoracionAceite { get; set; }

        //public short? PerteneceA { get; set; }

        [Display(Name = "Tensión secundaria")]
        public double? TensionSecundaria { get; set; }

        [Display(Name = "Tensión Primaria")]
        public double? TensionPrimaria { get; set; }

        public short? Id_Bloque { get; set; }


        [Display(Name = "Tipo de bloque")]
        public string Bloque { get; set; }

        [Display(Name = "Esquema por baja")]
        public string esquemaBloque { get; set; }

        [Display(Name = "Sector cliente")]
        public string sectorClienteBloque { get; set; }

        [Display(Name = "Cliente")]
        public string clienteBloque { get; set; }

        [Display(Name = "Tipo de salida")]
        public string tipoSalidaBloque { get; set; }

        [Display(Name = "Tensión terciario")]
        public double? tensionTerciarioBloque { get; set; }

        [Display(Name = "Tension salida")]
        public double? tensionSalidaBloque { get; set; }

        //[MaxLength(1)]
        //public byte[] TabRegulable { get; set; }

        //[MaxLength(1)]
        //public byte[] TabPrimarioSecundario { get; set; }

        //public short? CantidadRegulacion { get; set; }

        //public short? CuentaOperaciones { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public DateTime? TabFecha { get; set; }

        [Display(Name = "Año Fabricación")]
        public short? AnnoFabricacion { get; set; }

        [Display(Name = "Fabricante")]
        public string Fabricante { get; set; }


        //public short? VoltajeSecundario { get; set; }

        [Display(Name = "Porciento Impedancia")]
        public double? PorcientoImpedancia { get; set; }

        [StringLength(30)]
        [Display(Name = "Grupo Conexión")]
        public string GrupoConexion { get; set; }

        [Display(Name = "Peso Total")]
        public double? PesoTotal { get; set; }

        //public short? CapacidadVentilador { get; set; }

        [StringLength(5)]
        [Display(Name ="Nombre"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Nombre { get; set; }

        [Display(Name = "Corriente Primaria")]
        public double? CorrientePrimaria { get; set; }

        [Display(Name = "Frecuencia")]
        public double? Frecuencia { get; set; }

        [Display(Name = "Enfriamiento")]
        public short? Enfriamiento { get; set; }

        [Display(Name = "Pérdidas en Vacío")]
        public double? PerdidasVacio { get; set; }

        [Display(Name = "Pérdidas Bajo Carga")]
        public double? PerdidasBajoCarga { get; set; }

        public double? NivelRuido { get; set; }

        [Display(Name = "Máx Temperatura")]
        public double? MaxTemperatura { get; set; }

        [Display(Name = "Tensión Impulso")]
        public double? TensionImpulso { get; set; }

        [Display(Name = "Peso Aceite")]
        public double? PesoAceite { get; set; }

        [Display(Name = "Peso Núcleo")]
        public double? PesoNucleo { get; set; }

        public double? NivelRadioInterf { get; set; }

        [Display(Name = "Corriente Secundaria")]
        public double? CorrienteSecundaria { get; set; }

        [StringLength(50)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Display(Name = "Cant. Ventiladores")]
        public short? CantVentiladores { get; set; }

        [Display(Name = "Cant. Radiadores")]
        public short? CantRadiadores { get; set; }

        [StringLength(700)]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Display(Name = "Peso Transporte")]
        public double? PesoTansporte { get; set; }

        [StringLength(20)]
        [Display(Name = "Regulación voltaje")]
        public string TipoRegVoltaje { get; set; }

        [Display(Name = "Total Taps")]
        public short? NroPosiciones { get; set; }

        [StringLength(50)]
        [Display(Name = "Tipo Caja Mando")]
        public string TipoCajaMando { get; set; }

        [Display(Name = "Tubo Expulsor")]
        public bool? TuboExplosor { get; set; }

        [Display(Name = "Válvula Sobrepresión")]
        public bool? ValvulaSobrePresion { get; set; }

        [Display(Name = "Tensión Terciaria")]
        public double? TensionTerciario { get; set; }

        [Display(Name = "Posición Trabajo")]
        public short? PosicionTrabajo { get; set; }

        [StringLength(30)]
        [Display(Name = "Nro Inventario")]
        public string NumeroInventario { get; set; }

        [Display(Name = "Fecha Instalado")]
        public DateTime? FechaDeInstalado { get; set; }
    }
}