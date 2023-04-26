using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class TransformadorTransmision
    {

        public int Id_EAdministrativa { get; set; }

        public int Id_Transformador { get; set; }

        public int NumAccion { get; set; }

        [StringLength(5)]
        public string Nombre { get; set; }

        [StringLength(7)]
        [Display(Name = "Subestación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Subestacion { get; set; }

        [Display(Name = "Subestación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string Codigo { get; set; }

        [StringLength(30)]
        [Display(Name = "Nro Inventario")]
        public string NumeroInventario { get; set; }

        [StringLength(30)]
        [Display(Name = "Grupo Conexión")]
        public string GrupoConexion { get; set; }

        [Display(Name = "Fecha de Instalado")]
        public DateTime? FechaDeInstalado { get; set; }

        [StringLength(10)]
        [Display(Name = "Nro. Empresa")]
        public string Numemp { get; set; }

        [Display(Name = "Enfriamiento")]
        public short? Enfriamiento { get; set; }

        [Display(Name = "Frecuencia")]
        public double? Frecuencia { get; set; }

        [Display(Name = "Corriente Primaria")]
        public double? CorrientePrimaria { get; set; }

        [Display(Name = "Corriente Secundaria")]
        public double? CorrienteSecundaria { get; set; }

        [Display(Name = "Corriente Terciaria")]
        public double? CorrienteTerciaria { get; set; }

        [StringLength(50)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Display(Name = "Máx Temperatura")]
        public double? MaxTemperatura { get; set; }

        [Display(Name = "Año Fabricación")]
        public short? AnnoFabricacion { get; set; }

        [Display(Name = "Pérdidas Bajo Carga")]
        public double? PerdidasBajoCarga { get; set; }

        [Display(Name = "Nivel Radio Interferencia")]
        public double? NivelRadioInterf { get; set; }

        [Display(Name = "Tensión Impulso")]
        public double? VoltajeImpulso { get; set; }

        [Display(Name = "Tensión Primaria")]
        public double? Id_VoltajePrim { get; set; }

        [Display(Name = "Tensión Secundaria")]
        public double? Id_Voltaje_Secun { get; set; }

        [Display(Name = "Tensión Terciaria")]
        public double? VoltajeTerciario { get; set; }

        [Display(Name = "Peso Total")]
        public double? PesoTotal { get; set; }

        [Display(Name = "Peso Núcleo")]
        public double? PesoNucleo { get; set; }

        [Display(Name = "Peso Aceite")]
        public double? PesoAceite { get; set; }

        [Display(Name = "Peso Transporte")]
        public double? PesoTansporte { get; set; }

        [StringLength(15)]
        [Display(Name = "Número de Serie")]
        public string NoSerie { get; set; }

        [Display(Name = "Fabricante")]
        public string idFabricante { get; set; }

        [Display(Name = "Pérdidas en Vacío")]
        public double? PerdidasVacio { get; set; }

        [Display(Name = "Capacidad")]
        public double? Capacidad { get; set; }

        [Display(Name = "Cant. Radiadores")]
        public short? CantRadiadores { get; set; }

        [Display(Name = "Cant. Ventiladores")]
        public short? CantVentiladores { get; set; }

        [Display(Name = "% Zcc P-S")]
        public double? PorcientoZccPS { get; set; }

        [Display(Name = "% Zcc S-T")]
        public double? PorcientoZccST { get; set; }

        [Display(Name = "% Zcc P-T")]
        public double? PorcientoZccPT { get; set; }

        [StringLength(1)]
        [Display(Name = "Estado Operativo")]
        public string EstadoOperativo { get; set; }

        [StringLength(700)]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Display(Name = "Tubo Explosor")]
        public bool? TuboExplosor { get; set; }

        [Display(Name = "Válvula Sobrepresión")]
        public bool? ValvulaSobrePresion { get; set; }

        [Display(Name = "Nro. Fase")]
        public short? NumFase { get; set; }

        [Display(Name = "Termosifones")]
        public bool? TieneTermosifones { get; set; }

        [Display(Name = "Cant. Termosifones")]
        public short? CantTermosifones { get; set; }

        [Display(Name = "Regulación Voltaje Prim")]
        [StringLength(30)]
        public string TipoRegVoltaje { get; set; }

        [Display(Name = "Nro Posiciones")]
        public short? NroPosiciones { get; set; }

        [Display(Name = "Posición Trabajo")]
        public short? PosicionTrabajo { get; set; }
        [Display(Name = "% Impedancia")]
        public double? PorcientoImpedancia { get; set; }

        [Display(Name = "Tipo Caja Mando")]
        [StringLength(50)]
        public string TipoCajaMando { get; set; }

        [Display(Name = "Nro Posiciones")]
        public short? RegVoltajeSecNroPos { get; set; }

        [Display(Name = "Posición Trabajo")]
        public short? RegVoltajeSecPosTrab { get; set; }

        //bushing
        [StringLength(30)]
        [Display(Name = "Cant Termosifones")]
        public string BushingPrimFaseATipo { get; set; }

        [StringLength(30)]
        public string BushingPrimFaseBTipo { get; set; }

        [StringLength(30)]
        public string BushingPrimFaseCTipo { get; set; }


        [StringLength(30)]
        public string BushingPrimFaseASerie { get; set; }

        [StringLength(30)]
        public string BushingPrimFaseBSerie { get; set; }

        [StringLength(30)]
        public string BushingPrimFaseCSerie { get; set; }

        public double? BushingPrimFaseAUn { get; set; }

        public double? BushingPrimFaseBUn { get; set; }

        public double? BushingPrimFaseCUn { get; set; }

        public double? BushingPrimFaseAIn { get; set; }

        public double? BushingPrimFaseBIn { get; set; }

        public double? BushingPrimFaseCIn { get; set; }

        [StringLength(30)]
        public string BushingPrimFaseNeutroTipo { get; set; }

        [StringLength(30)]
        public string BushingSecFasesTipo { get; set; }

        [StringLength(30)]
        public string BushingSecNeutroTipo { get; set; }

        [StringLength(30)]
        public string BushingTercFasesTipo { get; set; }

        public double? BushingSecFasesUn { get; set; }

        public double? BushingSecFasesIn { get; set; }

        public double? BushingTercFasesUn { get; set; }

        public double? BushingTercFasesIn { get; set; }

        public short? Id_Bloque { get; set; }


        [Display(Name = "Tipo de bloque")]
        public string Bloque { get; set; }

        [Display(Name = "Esquema por baja")]
        public string esquema { get; set; }

        [Display(Name = "Sector cliente")]
        public string sectorCliente { get; set; }

        [Display(Name = "Cliente")]
        public string cliente { get; set; }

        [Display(Name = "Tipo de salida")]
        public string tipoSalida { get; set; }

        [Display(Name = "Tensión terciario")]
        public double? tensionTerciarioBloque { get; set; }

        [Display(Name = "Tension salida")]
        public double? tensionSalida { get; set; }









    }
}