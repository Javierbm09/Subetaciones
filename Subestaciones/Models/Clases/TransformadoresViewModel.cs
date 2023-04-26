using System.ComponentModel.DataAnnotations;

namespace Subestaciones.Models.Clases
{
    public class TransformadoresViewModel
    {
        [StringLength(7)]
        public string Codigo { get; set; }

        [StringLength(10)]
        [Display(Name = "Nro Empresa")]
        public string Numemp { get; set; }
    }
}