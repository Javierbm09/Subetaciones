namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_Mantenimientos
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string Nombre { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(7)]
        public string Codigo { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime Fecha { get; set; }

        public short? RealizadoPor { get; set; }

        public short ea_transformador { get; set; }

        public int id_Transformador { get; set; }

        public short? EA { get; set; }

        public int? NumAccion { get; set; }

        public double? PorcientoImpedancia { get; set; }

        public short? VoltajePrimario { get; set; }

        public short? VoltajeSecundario { get; set; }

        public double? TemperaturaMax { get; set; }

        public double? CapacidadVentilador { get; set; }

        [StringLength(6)]
        public string NivelAceite { get; set; }

        [StringLength(6)]
        public string IndiceAcidez { get; set; }

        public double? AguaDestilacion { get; set; }

        public double? ImpuresasMecanicas { get; set; }

        public double? PuntoInflamacion { get; set; }

        public double? Viscosidad { get; set; }

        public double? PerdidasDielectricasAT { get; set; }

        public double? PerdidasDielectricasBT { get; set; }

        public double? PerdidasDielectricasAB { get; set; }

        public double? ResistenciaAislamientoAB15 { get; set; }

        public double? ResistenciaAislamientoAB60 { get; set; }

        public double? ResistenciaAislamientoABK { get; set; }

        public double? ResistenciaOhm1A { get; set; }

        public double? ResistenciaOhm1B { get; set; }

        public double? ResistenciaOhm1C { get; set; }

        public double? ResistenciaOhm2A { get; set; }

        public double? ResistenciaOhm2B { get; set; }

        public double? ResistenciaOhm2C { get; set; }

        public double? ResistenciaOhm3A { get; set; }

        public double? ResistenciaOhm3B { get; set; }

        public double? ResistenciaOhm3C { get; set; }

        public double? ResistenciaOhm4A { get; set; }

        public double? ResistenciaOhm4B { get; set; }

        public double? ResistenciaOhm4C { get; set; }

        public double? ResistenciaOhm5A { get; set; }

        public double? ResistenciaOhm5B { get; set; }

        public double? ResistenciaOhm5C { get; set; }

        public double? ResistenciaAislamientoBAT15 { get; set; }

        public double? ResistenciaAislamientoBAT60 { get; set; }

        public double? ResistenciaAislamientoBATK { get; set; }

        public double? ResistenciaAislamientoABT15 { get; set; }

        public double? ResistenciaAislamientoABT60 { get; set; }

        public double? ResistenciaAislamientoABTK { get; set; }

        public double? ResistenciaAislamientoABcf { get; set; }

        public double? ResistenciaAislamientoBATcf { get; set; }

        public double? ResistenciaAislamientoABTcf { get; set; }

        public double? ResistenciaAislamientoTaABTe15 { get; set; }

        public double? ResistenciaAislamientoTaABTe60 { get; set; }

        public double? ResistenciaAislamientoTaABTeK { get; set; }

        public double? ResistenciaAislamientoTaABTecf { get; set; }

        public double? ResistenciaOhmDevPrim6A { get; set; }

        public double? ResistenciaOhmDevPrim6B { get; set; }

        public double? ResistenciaOhmDevPrim6C { get; set; }

        public double? ResistenciaOhmDevPrim7A { get; set; }

        public double? ResistenciaOhmDevPrim7B { get; set; }

        public double? ResistenciaOhmDevPrim7C { get; set; }

        public double? ResistenciaOhmDevPrim8A { get; set; }

        public double? ResistenciaOhmDevPrim8B { get; set; }

        public double? ResistenciaOhmDevPrim8C { get; set; }

        public double? ResistenciaOhmDevPrim9A { get; set; }

        public double? ResistenciaOhmDevPrim9B { get; set; }

        public double? ResistenciaOhmDevPrim9C { get; set; }

        public double? ResistenciaOhmDevPrim10A { get; set; }

        public double? ResistenciaOhmDevPrim10C { get; set; }

        public double? ResistenciaOhmDevPrim10B { get; set; }

        public double? ResistenciaOhmDevPrim11A { get; set; }

        public double? ResistenciaOhmDevPrim11B { get; set; }

        public double? ResistenciaOhmDevPrim11C { get; set; }

        public double? ResistenciaOhmDevPrim12A { get; set; }

        public double? ResistenciaOhmDevPrim12B { get; set; }

        public double? ResistenciaOhmDevPrim12C { get; set; }

        public double? ResistenciaOhmDevPrim13A { get; set; }

        public double? ResistenciaOhmDevPrim13B { get; set; }

        public double? ResistenciaOhmDevPrim13C { get; set; }

        public double? ResistenciaOhmDevPrim14A { get; set; }

        public double? ResistenciaOhmDevPrim14B { get; set; }

        public double? ResistenciaOhmDevPrim14C { get; set; }

        public double? ResistenciaOhmDevPrim15A { get; set; }

        public double? ResistenciaOhmDevPrim15B { get; set; }

        public double? ResistenciaOhmDevPrim15C { get; set; }

        public double? ResistenciaOhmDevPrim16A { get; set; }

        public double? ResistenciaOhmDevPrim16B { get; set; }

        public double? ResistenciaOhmDevPrim16C { get; set; }

        public double? ResistenciaOhmDevPrim17A { get; set; }

        public double? ResistenciaOhmDevPrim17B { get; set; }

        public double? ResistenciaOhmDevPrim17C { get; set; }

        public double? ResistenciaOhmDevPrim18A { get; set; }

        public double? ResistenciaOhmDevPrim18B { get; set; }

        public double? ResistenciaOhmDevPrim18C { get; set; }

        public double? ResistenciaOhmDevPrim19A { get; set; }

        public double? ResistenciaOhmDevPrim19B { get; set; }

        public double? ResistenciaOhmDevPrim19C { get; set; }

        public double? ResistenciaOhmDevPrim20A { get; set; }

        public double? ResistenciaOhmDevPrim20B { get; set; }

        public double? ResistenciaOhmDevPrim20C { get; set; }

        public double? ResistenciaOhmDevSecA { get; set; }

        public double? ResistenciaOhmDevSecB { get; set; }

        public double? ResistenciaOhmDevSecC { get; set; }

        public double? ResistenciaOhmDevTerciarioA { get; set; }

        public double? ResistenciaOhmDevTerciarioB { get; set; }

        public double? ResistenciaOhmDevTerciarioC { get; set; }

        public double? RigidezDielectrica { get; set; }
    }
}
