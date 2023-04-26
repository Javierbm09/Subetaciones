using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Subestaciones.Models
{
    public class DBContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public DBContext() : base("name=SubestacionesConnection")
        {
        }

        public DbSet<Personal> Personal { get; set; }

        public DbSet<Logueados> Logueados { get; set; }
        
        public DbSet<Cargadores> Cargadores { get; set; }

        public DbSet<NomencladorBaterias> NomencladorBaterias { get; set; }
    }
}
