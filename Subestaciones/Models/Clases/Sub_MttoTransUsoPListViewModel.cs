using System;

namespace Subestaciones.Models.Clases
{
    public class Sub_MttoTransUsoPListViewModel
    {
        public int Id_Eadministrativa { get; set; }

        public short Id_EATransformador { get; set; }

        public int Id_Transformador { get; set; }

        public string CodigoSub { get; set; }

        public DateTime? fechaMtto { get; set; }

        public short TipoMantenimiento { get; set; }

        public string subestacion { get; set; }

        public string RealizadoPor { get; set; }

        public string TipoMtto { get; set; }

        public string Mantenido { get; set; }

        public string NombreSubestacion { get; set; }
    }
}