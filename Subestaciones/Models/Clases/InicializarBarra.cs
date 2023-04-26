using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Subestaciones.Models;
using Subestaciones.Models.Clases;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace Subestaciones.Models.Clases
{
    public class InicializarBarra
    {
        private DBContext db;
        public InicializarBarra(DBContext db)
        {
            this.db = db;
        }

        public List<unionSub> subs()
        {
            var sub1 = db.Subestaciones.ToList();
            var sub2 = db.SubestacionesTransmision.ToList();
            List<unionSub> us = new List<unionSub>();
            foreach (var item in sub1)
            {
                us.Add(new unionSub
                {
                    Codigo = item.Codigo,
                    NombreSubestacion = item.NombreSubestacion
                });
            }
            foreach (var item in sub2)
            {
                us.Add(new unionSub
                {
                    Codigo = item.Codigo,
                    NombreSubestacion = item.NombreSubestacion
                });
            }
            return us.ToList();
            
        }
        //public List<Conductor> conductores ()
        //{
             
        //}
    }
}