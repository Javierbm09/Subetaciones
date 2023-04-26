using Subestaciones.Models.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Subestaciones.Models.Repositorio
{
    public class TermografiasRepo
    {
        private DBContext db;
        public TermografiasRepo(DBContext db)
        {
            this.db = db;
        }
        public async Task<List<Termografias>> ObtenerTermografias()
        {
            //var parametrocodigo = new SqlParameter("@Subestacion", codigo);
            //var parametrotipo = new SqlParameter("@tipo", tipo);
            return await db.Database.SqlQuery<Termografias>(@"SELECT Sub_Termografias.Subestacion sub,(select NombreSubestacion from Subestaciones where Codigo = Sub_Termografias.Subestacion union select NombreSubestacion from SubestacionesTransmision where Codigo = Sub_Termografias.Subestacion) subNomb,
            Sub_Termografias.NumAccion numA, CAST(Sub_Termografias.Id_EAdministrativa AS INT) idEA, Sub_Termografias.fecha Fecha, CAST(Sub_Termografias.TempMedio AS INT) TempAmbiente, CAST(Sub_Termografias.transferencia AS INT) TransfLinea, Personal.Nombre EjecutadoPor, Sub_Termografias.DescripcionEquipo Designacion, 
            CASE  Sub_Termografias.Ejecutado 
            WHEN 0 THEN 'No'
            WHEN 1 THEN 'Si' END Ejecutado, (select count(*) from  sub_PuntoTermografia where (Id_EAdministrativa=Sub_Termografias.Id_EAdministrativa)
           and  (NumAccion=Sub_Termografias.NumAccion) )as puntos
            FROM Sub_Termografias left JOIN Personal on Sub_Termografias.EjecutadaPor = Personal.Id_Persona").ToListAsync();

        }

        public List<Elementos> elementos(string inst)
        {
            //var parametrocodigo = new SqlParameter("@Subestacion", codigo);
            var parametroinst = new SqlParameter("@instalacion", inst);
            return db.Database.SqlQuery<Elementos>(@"SELECT E.Id_Elemento, E.Elemento, E.Tipo
            FROM Elementos E, TipoInstalacionElemento TIE
            WHERE E.Id_Elemento = TIE.Id_Elemento and TIE.Id_TipoInstalacion =@instalacion 
            ORDER BY E.Elemento", parametroinst).ToList();

        }

        public List<Elementos> elementos()
        {
            //var parametrocodigo = new SqlParameter("@Subestacion", codigo);
            //var parametroinst = new SqlParameter("@instalacion", inst);
            return db.Database.SqlQuery<Elementos>(@"SELECT E.Id_Elemento, E.Elemento, E.Tipo
            FROM Elementos E, TipoInstalacionElemento TIE
            WHERE E.Id_Elemento = TIE.Id_Elemento 
            ORDER BY E.Elemento").ToList();

        }

        public SelectList ObtenerFaseTermografia()
        {
            return new SelectList(new List<SelectListItem>
            {
                //new SelectListItem { Value = "", Text = "" },
                new SelectListItem { Value= "A", Text= "A" },
                new SelectListItem { Value="B", Text="B"},
                new SelectListItem { Value="C", Text="C"},
                 new SelectListItem{ Value="Neutro", Text="Neutro"}
            }, "Value", "Text");
        }
    }
}