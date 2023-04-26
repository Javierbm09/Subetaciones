using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class DefectoViewModel

    {
        
        public short Id_EAdministrativa { get; set; }

        public int? Id_Defecto { get; set; }
        public int NumAccion { get; set; }

        [StringLength(1)]
        public string TipoInstalacion { get; set; }

        [StringLength(7)]
        public string Instalacion { get; set; }

        [StringLength(7)]
        public string Seccion { get; set; }

        public string Elemento { get; set; }

        public string Material { get; set; }

        [StringLength(50)]
        public string Dimension { get; set; }

        [StringLength(100)]
        public string Id_LugarHabitado { get; set; }

        [StringLength(200)]
        public string Id_Calle { get; set; }

        [StringLength(200)]
        public string Id_Entrecalle1 { get; set; }

        [StringLength(200)]
        public string Id_Entrecalle2 { get; set; }

        public string Defecto { get; set; }

        public string prioridad { get; set; }

        public DateTime? Fecha { get; set; }

        public short? EstructuraAsignada { get; set; }

        public bool Resuelto { get; set; }

        public bool Cancelado { get; set; }

        public short? ResueltoPor { get; set; }

        public DateTime? FechaFin { get; set; }

        [StringLength(1000)]
        public string Observaciones { get; set; }

        public string Estado { get; set; }

        public int? Queja { get; set; }

        public short? Area { get; set; }

        public short? InformadoPor { get; set; }

        public DateTime? FechaIP { get; set; }

        public short? IntroducidoPor { get; set; }

        public DateTime? FechaIntroducidoP { get; set; }

        public short? InformadoA { get; set; }

        public DateTime? FechaIA { get; set; }

        public short? Bateria { get; set; }

        public short? Grupo { get; set; }

        public bool Informado { get; set; }

        public Guid? Id_Seccion { get; set; }

        public short? CerradoPor { get; set; }

        [StringLength(5)]
        public string Emplazamiento { get; set; }

        [StringLength(5)]
        public string Codigo { get; set; }

       
        public int id_EAdministrativa_Prov { get; set; }

        public bool? planteamiento { get; set; }

        public short? categoria { get; set; }
    }
}