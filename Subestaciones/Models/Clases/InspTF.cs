using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class InspTF
    {

        [Display(Name = "Nombre")]

        public string nombre { get; set; }//del transf

        [Display(Name = "Num empresa")]
        public string Numemp { get; set; }

        [Display(Name = "No serie")]
        public string NoSerie { get; set; }

        public string Fabricante { get; set; }

        public string nombreCelaje { get; set; }

        public string codigoSub { get; set; }

        public DateTime fecha { get; set; }

        [Display(Name = "Tensión Primaria")]
        public double? TensionPrimaria { get; set; }

        [Display(Name = "Tensión Secundaria")]
        public double? TensionSecundaria { get; set; }

        [Display(Name = "Capacidad")]
        public double? capacidad { get; set; }

        [Display(Name = "Porciento Impedancia")]
        public double? PorcientoImpedancia { get; set; }

        [StringLength(30)]
        [Display(Name = "Grupo Conexión")]
        public string GrupoConexion { get; set; }

        [Display(Name = "Nro posiciones")]
        public short? NroPosiciones { get; set; }


        [Display(Name = "Peso Total (Kg)")]
        public double? PesoTotal { get; set; }

        [Display(Name = "Peso Aceite (Kg)")]
        public double? PesoAceite { get; set; }

        public int Id_EAdministrativa { get; set; }

        public int Id_Transformador { get; set; }

        public bool? salidero { get; set; }

        [Display(Name = "Nivel aceite")]
        public string nAceite { get; set; }

        [Display(Name = "Pintura")]

        public string pintura { get; set; }

        [Display(Name = "Aterramientos")]
        public string aterramiento { get; set; }

        [Display(Name = "Bushing")]
        public string bushing { get; set; }

        [Display(Name = "Temperatura real")]
        public int? temperaturaReal { get; set; }

        public string observaciones { get; set; }

    }
}