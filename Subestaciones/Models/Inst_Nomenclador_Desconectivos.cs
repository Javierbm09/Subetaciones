namespace Subestaciones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Inst_Nomenclador_Desconectivos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inst_Nomenclador_Desconectivos()
        {
            Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion = new HashSet<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Nomenclador { get; set; }

        public int Descripcion { get; set; }

        public int Id_Administrativa { get; set; }

        public int NumAccion { get; set; }

        public int id_EAdministrativa_Prov { get; set; }

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

        public double? PesoGas { get; set; }

        public double? PesoInterruptor { get; set; }

        public double? PesoGabinete { get; set; }

        public double? PesoTotal { get; set; }

        public int? Tanque { get; set; }

        public virtual Inst_Nomenclador_Desconectivo_MedidoExtincionArco Inst_Nomenclador_Desconectivo_MedidoExtincionArco { get; set; }

        public virtual Inst_Nomenclador_Desconectivo_PresionGas Inst_Nomenclador_Desconectivo_PresionGas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion> Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_Aislamiento Inst_Nomenclador_Desconectivos_Aislamiento { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_ApertCable Inst_Nomenclador_Desconectivos_ApertCable { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_BIL Inst_Nomenclador_Desconectivos_BIL { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_CorrienteNominal Inst_Nomenclador_Desconectivos_CorrienteNominal { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_Cortocircuito Inst_Nomenclador_Desconectivos_Cortocircuito { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_Modelo Inst_Nomenclador_Desconectivos_Modelo { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_SecuenciaOperacion Inst_Nomenclador_Desconectivos_SecuenciaOperacion { get; set; }

        public virtual Inst_Nomenclador_Desconectivos_TensionNominal Inst_Nomenclador_Desconectivos_TensionNominal { get; set; }
    }
}
