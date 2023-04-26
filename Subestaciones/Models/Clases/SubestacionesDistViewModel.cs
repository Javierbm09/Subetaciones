using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Clases
{
    public class SubestacionesDistViewModel : IValidatableObject
    {
        public Subestaciones DSubestaciones { get; set; }
        public List<Sub_LineasSubestacion> Circuitos_Alta { get; set; }
        public List<Circuitos_Baja> DCircuitos_Baja { get; set; }
        public List<Bloque> Bloques_Transformacion { get; set; }
        public List<TransformadorSubtransmision> Transformadores { get; set; }
        public List<Emplazamiento_Sigere> CentralesElectricas { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {// validando que se repitan el seccionalizador para el mismo circuito en la subestación
            if ((Circuitos_Alta != null) && (Circuitos_Alta.Count > 1))
            {
                var circuitosAltaAgrupados = (from c in Circuitos_Alta
                                              group c by new { c.Circuito } into Cgroup
                                              select Cgroup
                                 ).ToList();
                var circuitosAltaMal = (from cag in circuitosAltaAgrupados
                                        where cag.Count() > 1
                                        select new { cag.Key.Circuito }).ToList();
                foreach (var item in circuitosAltaMal)
                {
                    var indice = Circuitos_Alta.FindLastIndex(c => c.Circuito == item.Circuito );
                    yield return new ValidationResult("El circuito ya existe en la subestación.", new string[] { String.Format("Circuitos_Alta[{0}].Circuito", indice) });
                }
            }

            
        }
    }
}