using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class MoverTransf
    {
        //datos del transformador
        public int idTransformador { get; set; }

        [Display(Name = "Transformador")]
        public string NombreTransformador { get; set; }

        public int idEA { get; set; }

        //datos del almacen
        [Display(Name = "Código almacén")]
        public string codAlmacen { get; set; }

        [Display(Name = "Almacén")]
        public string almacen { get; set; }

        //datos de la subestacion
        [Display(Name = "Código subestación")]
        public string codigo { get; set; }

        [Display(Name = "Subestación")]
        public string nombresubestacion { get; set; }

        public string Sucursal { get; set; }

        public string OBE { get; set; }

        public string Numemp { get; set; }

        public double? Capacidad { get; set; }

        public double? Voltaje { get; set; }


        public double? Volt_Secund { get; set; }

        public double? Volt_Terc { get; set; }

        public string Fabricante { get; set; }

        [Display(Name = "Código subestación")]
        public string codSub { get; set; }

        [Display(Name = "Tipo bloque")]
        public string tipoBloque { get; set; }

        [Display(Name = "Tipo salida")]
        public string tipoSalida { get; set; }


        [Display(Name = "Esquema por baja")]
        public string esquemaBaja { get; set; }

      

    }
}