using CSCifrado;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Repositorio
{
    public class SeguridadRepo
    {
        DBContext db = new DBContext();

        public SeguridadRepo(DBContext db)
        {
            this.db = db;
        }

        public SeguridadRepo()
        {
        }

        public bool Validar_Credenciales(string user, string password)
        {
            try
            {
                string passDescifrado = CipherUtility.DesCifrarAES(
                    db.Adm_PersonalExtendido.SingleOrDefault(c => c.nombreUsuario == user).contrasenna,
                    "F735F6DDDBB1F0784403180DBF74FF3697F030D564B23AC18DAF0D4CB77AFF1C715CB1F11274B8824FFDD69C5D71C3E23B1238757FE2F52C1814642856B933C7");
                return passDescifrado == password ? true : false;
            }
            catch (Exception e)
            {
                 throw ;
            }
            //return false;
        }

        public int GetNumAccion(string tipoSubAccion, string tipoAccion, int? amod)
        {
            var usuario = HttpContext.Current.User?.Identity?.Name ?? null;
            var usuario_logueado = db.Adm_PersonalExtendido.FirstOrDefault(c => c.Id_Persona == db.Personal.FirstOrDefault(x => c.nombreUsuario == System.Web.HttpContext.Current.User.Identity.Name).Id_Persona);
            short id_usuario = (short)db.Personal.FirstOrDefault(c => c.Nombre == System.Web.HttpContext.Current.User.Identity.Name).Id_Persona;
            short EAdmin = (short)db.Personal.FirstOrDefault(c => c.Nombre == System.Web.HttpContext.Current.User.Identity.Name).id_EAdministrativa;
            short EAdmin_Prov = (short)db.Personal.FirstOrDefault(c => c.Nombre == System.Web.HttpContext.Current.User.Identity.Name).id_EAdministrativa;
            short EA = (short)db.Personal.FirstOrDefault(c => c.Id_Persona == id_usuario).id_EAdministrativa;
            string nombre_usuario = HttpContext.Current.User.Identity.Name;

            try
            {
                DBContext db = new DBContext();
                return db.Database.SqlQuery<int>("GetNumAccion @EAdmin,@tipoSubAccion,@tipoAccion,@usuario,@EA,@amod,@numAccion OUTPUT SELECT @numAccion",
                        new SqlParameter("EAdmin", EAdmin),
                        new SqlParameter("tipoSubAccion", tipoSubAccion),
                        new SqlParameter("tipoAccion", tipoAccion),
                        new SqlParameter("usuario", id_usuario),
                        new SqlParameter("EA", EA),
                        new SqlParameter("amod", amod),
                        new SqlParameter("numAccion", amod)
                    ).FirstOrDefault();
            }
            catch (Exception)
            {
                return new int();
            }

        }
        public List<Personal> Logueados(string Maquina)
        {
            try
            {
                return db.Database.SqlQuery<Personal>("EXEC PersonalLoguear 20,@Maquina",
                    new SqlParameter("Maquina", Maquina)
                    ).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}