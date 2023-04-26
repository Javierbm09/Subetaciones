using System.ComponentModel.DataAnnotations;

namespace Subestaciones.Models.Clases
{
    public class Sub_MttoTransformadoresUsoPlantaViewModel
    {

        public Capacidades capacidades { get; set; }

        public Transformadores transformadores { get; set; }

        public BancosTransformadores bancosTransformadores { get; set; }

        public Fabricantes fabricantes { get; set; }

        public SubestacionesTrans subestacionesTrans { get; set; }
        
        public Sub_MttoTransUsoP sub_MttoTransUsoP { get; set; }
        
        public Sub_NomInstrumentoMedicion sub_NomInstrumentoMedicion { get; set; }

        public VoltajesSistemas voltajesSistemas { get; set; }

        public GruposVoltaje gruposVoltaje { get; set; }

        public Sub_MttoUPResistOhmica sub_MttoUPResistOhmica { get; set; }

        public Sub_MttoUPRelacTransformacion sub_MttoUPRelacT { get; set; }

        public TipoMantenimiento tipoMantenimiento { get; set; }

        public DatosTransformadorUsoPlantaViewModel dTUsoP { get; set; }

    }
}