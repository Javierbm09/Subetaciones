using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Subestaciones.Models.Clases
{
    public class unionSub
    {
        public string Codigo { get; set; }
        [Display(Name = "Subestación")]
        public string NombreSubestacion { get; set; }
        [StringLength(20)]
        public string Calle { get; set; }
        [StringLength(6)]
        public string Numero { get; set; }
        [StringLength(20)]
        public string Entrecalle1 { get; set; }
        [StringLength(20)]
        public string Entrecalle2 { get; set; }
        [StringLength(25)]
        public string BarrioPueblo { get; set; }
        public short Sucursal { get; set; }
        public short Id_EAdministrativa { get; set; }
        public string TipoBloque { get; set; }
        public string TipoSalida { get; set; }
        public string EsquemaPorBaja { get; set; }
        public string OBE { get; set; }
        public string SucursalNombre { get; set; }


    }
}