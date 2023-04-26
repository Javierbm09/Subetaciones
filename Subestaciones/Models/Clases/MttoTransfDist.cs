using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class MttoTransfDist
    {
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_EAdministrativa { get; set; }


        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Transformador { get; set; }

        public int NumAccion { get; set; }

        [StringLength(7)]
        [Display(Name = "Ubicación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Subestacion { get; set; }

        [StringLength(10)]
        [Display(Name = "Nro. Empresa")]
        public string Numemp { get; set; }

        [Display(Name = "Capacidad")]
        public double? capacidad { get; set; }

        

        [Display(Name = "Tensión primaria")]
        public short? voltajePrimario { get; set; }

        [StringLength(3)]
        [Display(Name = "Fase")]
        public string Fase { get; set; }

        [StringLength(15)]
        [Display(Name = "Nro. Serie")]
        public string NoSerie { get; set; }

        

        [StringLength(1)]
        [Display(Name = "Estado Operativo")]
        public string EstadoOperativo { get; set; }

       
        [Display(Name = "Nro. Fase")]
        public short? NumFase { get; set; }


        [Display(Name = "Tensión secundaria")]
        public double? TensionSecundaria { get; set; }

        [Display(Name = "Tensión Primaria")]
        public double? TensionPrimaria { get; set; }

       

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
        [Display(Name = "Nombre"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
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

        [Display(Name = "Fecha mtto")]
        public DateTime? fecha { get; set; }
    }
}