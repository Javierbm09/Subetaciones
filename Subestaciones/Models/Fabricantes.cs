namespace Subestaciones.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Fabricantes
    {
        [Key]
        public int Id_Fabricante { get; set; }

        [StringLength(20)]
        public string Nombre { get; set; }

        [StringLength(20)]
        [Display(Name = "Fabricante")]
        public string Pais { get; set; }

        [StringLength(56)]
        public string Direccion { get; set; }

        [StringLength(20)]
        public string EMail { get; set; }

        [StringLength(20)]
        public string Web { get; set; }

        public short? EstructuraAdministrativa { get; set; }

        public int? NumAccion { get; set; }

        public bool FTransformadores { get; set; }

        public bool FPararrayos { get; set; }

        public bool FPortafusibles { get; set; }

        public bool FFusibles { get; set; }

        public bool FDesconectivos { get; set; }

        public bool FCapacitores { get; set; }

        public bool FAutomatica { get; set; }

        public bool FConductores { get; set; }

        public bool FLuminarias { get; set; }

        public bool FLamparas { get; set; }

        public bool FAccesorios { get; set; }

        public bool FAisladores { get; set; }

        public bool FEquipoMedida { get; set; }

        public bool FEquipodeAlumbrado { get; set; }

        public bool FFotocelda { get; set; }

        public bool FRelevador { get; set; }

        public bool? FTransformadoresSub { get; set; }

        public bool? FCargadoresSub { get; set; }

        public bool? FBateriasSub { get; set; }
    }
}
