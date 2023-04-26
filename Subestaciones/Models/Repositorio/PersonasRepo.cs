using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Subestaciones.Models.Repositorio
{
    public class PersonasRepo
    {
        DBContext db = new DBContext();
        public bool LoguearPersona(Adm_PersonalExtendido p, string ip)
        {
            SeguridadRepo sr = new SeguridadRepo();
            try
            {
                if (sr.Validar_Credenciales(p.nombreUsuario, p.contrasenna))
                {
                    var logged = db.Logueados;
                    var l = new Logueados() { Maquina = ip + "(" + p.nombreUsuario + ")", Modulo = 20, Usuario = p.nombreUsuario };
                    if (!db.Logueados.Any(c => c.Modulo == 20 && c.Maquina == l.Maquina && c.Usuario == l.Usuario))
                    {
                        logged.Add(l);
                        db.SaveChanges();
                        FormsAuthentication.SetAuthCookie(GetNombrePersonaPorUsuario(p.nombreUsuario), false);
                        return true;
                    }
                    if (db.Logueados.Any(c => c.Modulo == 20 && c.Maquina == l.Maquina && c.Usuario == l.Usuario))
                    {
                        FormsAuthentication.SetAuthCookie(GetNombrePersonaPorUsuario(p.nombreUsuario), false);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DesloguearPersona(string ip)
        {
            try
            {
                Logueados l = db.Logueados.Find(20, ip);
                db.Logueados.Remove(l);
                db.SaveChanges();
                FormsAuthentication.SignOut();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetNombrePersonaPorUsuario(string user)
        {
            try
            {
                return db.Adm_PersonalExtendido.Where(x => x.nombreUsuario == user).Select(y => y.Personal).FirstOrDefault().Nombre;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}