﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Subestaciones.Models;
using Subestaciones.Models.Repositorio;

namespace Subestaciones.Controllers
{
    public class PersonalController : Controller
    {
        PersonasRepo repo = new PersonasRepo();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Adm_PersonalExtendido persona)
        {
            if (repo.LoguearPersona(persona, Request.UserHostName))
                return Redirect("~/Home");
            else
            {
                ModelState.AddModelError("nombreUsuario", "Las credenciales no son válidas.");
                return View(persona);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Personal/Login");
        }

        //private DBContext db = new DBContext();
        //Seguridad seguridad = new Seguridad();
        //PersonasRepo repo = new PersonasRepo();

        //// GET: Login
        //public ActionResult Login()
        //{
        //    //var logueados = Logueados();
        //    //ViewBag.Nombre = new SelectList(logueados, "Nombre", "Nombre");
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Login(Adm_PersonalExtendido persona)
        //{
        //    if (repo.LoguearPersona(persona, Request.UserHostName))
        //        return Redirect("~/Portada/Index");
        //    else
        //    {
        //        ModelState.AddModelError("nombreUsuario", "Las credenciales no son válidas.");
        //        return View(persona);
        //    }
        //}


        ////[HttpPost]
        ////public ActionResult Login(Personal persona)
        ////{
        ////    if (seguridad.Validar_Credenciales(persona.Nombre, persona.Password))
        ////    {
        ////        var logged = db.Logueados;
        ////        Logueados l = new Logueados();

        ////        string IP = Request.UserHostName;
        ////        IPAddress myIP = IPAddress.Parse(IP);
        ////        IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
        ////        List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();

        ////        l.Maquina = compName.First();
        ////        l.Modulo = 29;
        ////        l.Usuario = persona.Nombre;
        ////        if (!db.Logueados.Any(c => c.Modulo == 29 && c.Maquina == l.Maquina && c.Usuario == l.Usuario))
        ////        {
        ////            logged.Add(l);
        ////            db.SaveChanges();
        ////        }

        ////        FormsAuthentication.SetAuthCookie(persona.Nombre, false);
        ////        return Redirect("~/Home");
        ////    }
        ////    else
        ////        ModelState.AddModelError("Nombre", "Las credenciales no son válidas.");

        ////    var logueados = Logueados();
        ////    ViewBag.Nombre = new SelectList(logueados, "Nombre", "Nombre");
        ////    return View(persona);
        ////}

        //public ActionResult Logout()
        //{
        //    //string IP = Request.UserHostName;
        //    //IPAddress myIP = IPAddress.Parse(IP);
        //    //IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
        //    //List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();

        //    //string Maquina = compName.First();

        //    //Logueados l = db.Logueados.Find(29, Maquina);

        //    //db.Logueados.Remove(l);
        //    //db.SaveChanges();

        //    //FormsAuthentication.SignOut();
        //    //return Redirect("~/Personal/Login");
        //    repo.DesloguearPersona(Request.UserHostName);
        //    return Redirect("~/Autenticacion/Login");

        //}

        //public List<Personal> Logueados()
        //{
        //    try
        //    {
        //        string IP = Request.UserHostName;

        //        IPAddress myIP = IPAddress.Parse(IP);
        //        IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
        //        List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();

        //        string Maquina = compName.First();

        //        List<Personal> personas = new List<Personal>();
        //        using (SqlConnection conexion = new SqlConnection(WebConfigurationManager.ConnectionStrings["SubestacionesConnection"].ToString()))
        //        {
        //            conexion.Open();
        //            SqlCommand command = conexion.CreateCommand();
        //            command.CommandText = "EXEC PersonalLoguear 29,@Maquina";
        //            command.Parameters.Add(new SqlParameter("Maquina", Maquina));
        //            var dr = command.ExecuteReader();

        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    Personal p = db.Personal.Find(int.Parse(dr["id_Persona"].ToString()));
        //                    personas.Add(p);
        //                }
        //                return personas;
        //            }
        //            else
        //                return new List<Personal>();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.error = e.Message;
        //        return new List<Personal>();
        //    }
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult ConfigurarConexion(string server, string database, string username, string userpass)
        //{
        //    string ConnStr = "data source=" + server + ";initial catalog=" + database + ";User ID=" + username + ";Password=" + userpass + ";MultipleActiveResultSets=True;App=EntityFramework";
        //    try
        //    {
        //        using (SqlConnection conexion = new SqlConnection(ConnStr))
        //        {
        //            conexion.Open();
        //            var lista = new SqlCommand(String.Format(@"SELECT * FROM Personal"), conexion).ExecuteReader();
        //        }
        //        Configuration Config = WebConfigurationManager.OpenWebConfiguration("~");
        //        ConnectionStringsSection conSetting = (ConnectionStringsSection)Config.GetSection("connectionStrings");
        //        conSetting.ConnectionStrings["SubestacionesConnection"].ConnectionString = ConnStr;
        //        Config.Save();
        //    }
        //    catch (Exception)
        //    {
        //        ViewBag.Error = "Los datos de conexión no son válidos";
        //        return Redirect("~/Personal/Login#signup");
        //    }
        //    return Redirect("~/Personal/Login");
        //}
    }
}
