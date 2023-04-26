using Subestaciones.Models.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Subestaciones.Models.Repositorio
{
    public class CertificacionSubDistRepo
    {
        private DBContext db;
        public CertificacionSubDistRepo(DBContext db)
        {
            this.db = db;
        }
        public async Task<CertificacionesSubDist> FindAsync(string codigo, DateTime fecha)
        {
            var lista = await ObtenerCertificaciones();
            return lista.Find(c => (c.CodigoSub == codigo) && (c.Fecha == fecha));
        }

        public async Task<List<CertificacionesSubDist>> ObtenerCertificaciones()
        {
            return await db.Database.SqlQuery<CertificacionesSubDist>(@"select Sub_Certificacion.NumAccion NumAccion,
            Sub_Certificacion.Id_EAdministrativa Id_EAdministrativa, 
            Subestaciones.Codigo CodigoSub, 
            Subestaciones.Codigo+', '+ Subestaciones.NombreSubestacion NombreSub,  
            EstructurasAdministrativas.Nombre UEB, 
            Sub_Certificacion.Fecha, 
            Sub_Certificacion.Observaciones
            from  Sub_Certificacion 
            left join Subestaciones on Sub_Certificacion.CodigoSub = Subestaciones.Codigo
            left join EstructurasAdministrativas on Sub_Certificacion.UEB = EstructurasAdministrativas.Id_EAdministrativa ").ToListAsync();
        }

        public List<SelectListItem> cumplimiento()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value="Si", Text="Si" },
                new SelectListItem { Value="No", Text="No"},
                new SelectListItem { Value="No procede", Text="No procede"}};

        }
        public List<SelectListItem> responsable()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value="UEB Redes", Text="UEB Redes" },
                new SelectListItem { Value="Centro Operaciones", Text="Centro Operaciones"}

        };
        }

        public List<AspYSubAspCertificacion> listaAspYSubAsp()
        {

            return db.Database.SqlQuery<AspYSubAspCertificacion>(@"select A.NroAspecto, A.NombreAspecto, S.NroSubAspecto, S.NombreSubAspecto
from Sub_CertificacionSubAspectos S inner join Sub_CertificacionAspectos A 
on S.NroAspecto = A.NroAspecto
order by A.NroAspecto").ToList();
        }


    }
}