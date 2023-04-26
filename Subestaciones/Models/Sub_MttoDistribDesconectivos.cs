namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sub_MttoDistribDesconectivos
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(7)]
        public string CodigoSub { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Fecha { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(7)]
        public string CodigoDesc { get; set; }

        [StringLength(20)]
        [Display(Name ="Limpieza tanque")]
        public string EstLimpiezaTanque { get; set; }

        [StringLength(20)]
        [Display(Name = "Limpieza gabinete")]
        public string EstLimpiezaGabinete { get; set; }

        [StringLength(20)]
        [Display(Name ="Pintura")]
        public string EstPintura { get; set; }

        [StringLength(20)]
        [Display(Name ="Aterramiento tanque")]
        public string EstAterramTanque { get; set; }

        [StringLength(20)]
        [Display(Name = "Aterramiento gabinete")]
        public string EstAterramGabinete { get; set; }

        [StringLength(20)]
        [Display(Name ="Presión SF6")]
        public string PresionSF6 { get; set; }

        [Display(Name ="Nro operaciones")]
        public short? NroOperaciones { get; set; }
        
        [Display(Name = "% desgaste fase A")]
        public double? PorcDesgasteFaseA { get; set; }

        [Display(Name = "% desgaste fase B")]
        public double? PorcDesgasteFaseB { get; set; }

        [Display(Name = "% desgaste fase C")]
        public double? PorcDesgasteFaseC { get; set; }

        [StringLength(20)]
        [Display(Name = "Pruebas funcionales")]
        public string PruebasFuncionales { get; set; }

        [StringLength(20)]
        [Display(Name = "Rótulos")]
        public string EstRotulos { get; set; }

        public double? ResistContactoFaseA { get; set; }

        public double? ResistContactoFaseB { get; set; }

        public double? ResistContactoFaseC { get; set; }

        public double? ResistAislamFaseAET { get; set; }

        public double? ResistAislamFaseBET { get; set; }

        public double? ResistAislamFaseCET { get; set; }

        public double? ResistAislamFaseAST { get; set; }

        public double? ResistAislamFaseBST { get; set; }

        public double? ResistAislamFaseCST { get; set; }

        public double? ResistAislamFaseAEST { get; set; }

        public double? ResistAislamFaseBEST { get; set; }

        public double? ResistAislamFaseCEST { get; set; }

        [StringLength(500)]
        public string Ovservaciones { get; set; }

        [StringLength(20)]
        [Display(Name = "Limpieza aislamientos")]
        public string LimpiezaAislamiento { get; set; }

        [StringLength(20)]
        [Display(Name = "Limpieza contactos")]
        public string LimpiezaContactos { get; set; }

        [StringLength(20)]
        public string Pintura { get; set; }

        [StringLength(20)]
        [Display(Name = "Mtto al mando")]
        public string MttoMando { get; set; }

        [StringLength(20)]
        public string Aterramiento { get; set; }

        [StringLength(20)]
        [Display(Name = "Verificación de capacidad")]
        public string VerificacionCapacidad { get; set; }
    }
}
