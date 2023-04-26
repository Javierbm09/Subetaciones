using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class ModelosDesconectivos
    {
        public int Id_Nomenclador { get; set; }

        public int id_EAdministrativa_Prov { get; set; }

        public int Id_Administrativa { get; set; }

        public int NumAccion { get; set; }

        public int? Id_ModeloDesconectivo { get; set; }

        public int? Id_Fabricante { get; set; }

        public int? Id_Tension { get; set; }

        public int? Id_Corriente { get; set; }

        public int? Id_Cortocircuito { get; set; }

        public int? Id_ApertCable { get; set; }

        public int? Id_Bil { get; set; }

        public int? Id_SecuenciaOperacion { get; set; }

        public int? Id_MedioExtinsion { get; set; }

        public int? Id_Aislamiento { get; set; }

        public int? Id_PresionGas { get; set; }

        [Display(Name = "Modelo")]
        public string modelo { get; set; }

        [Display(Name = "Aislamiento")]
        public string Aislamiento { get; set; }

        [Display(Name = "Fabricante")]
        public string Fabricante { get; set; }

        [Display(Name = "BIL")]
        public string Bil { get; set; }

        [Display(Name = "Tanque")]
        public int? Tanque { get; set; }

        [Display(Name = "Secuencia operación")]
        public string SecOpe { get; set; }

        [Display(Name = "Extensión del arco")]
        public string ExtArco { get; set; }

        [Display(Name = "Tensión nominal(kV)")]
        public string tension { get; set; }

        [Display(Name = "Corriente nominal(kA)")]
        public string corrienteN { get; set; }


        [Display(Name = "Corriente cortocircuito(kA)")]
        public string corrienteCorto { get; set; }

        [Display(Name = "Corriente apertura cable(kA)")]
        public string corrienteA { get; set; }

        [Display(Name = "Presión gas(Kpa)")]
        public string presion { get; set; }

        [Display(Name = "Peso gas(kg)")]
        public double? pesoGas { get; set; }

        [Display(Name = "Peso interruptor")]
        public double? pesoInt { get; set; }

        [Display(Name = "Peso gabinete")]
        public double? pesoGab { get; set; }

        [Display(Name = "Peso total")]
        public double? pesoTotal { get; set; }

        public int tipoDesc { get; set; }


    }
}