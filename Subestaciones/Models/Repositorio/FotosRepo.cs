using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Subestaciones.Models.Repositorio
{
    public class FotosRepo
    {

        private DBContext db;
        public FotosRepo(DBContext db)
        {
            this.db = db;
        }

        public void UploadImageInDataBase(HttpPostedFileBase file, Fotos contentFoto)
        {
            contentFoto.Id_EAdministrativa = (short)db.Personal.FirstOrDefault(c => c.Nombre == System.Web.HttpContext.Current.User.Identity.Name).id_EAdministrativa;
            contentFoto.NumAccion = new SeguridadRepo(db).GetNumAccion("I", "IRF", 0);
            contentFoto.Version = (short)db.Database.SqlQuery<int>("SELECT CASE WHEN MAX(version) IS NULL THEN 1  ELSE MAX(version) + 1 END AS 'Version' FROM Fotos WHERE instalacion = @p0", contentFoto.Instalacion).First<int>();
            if (file.FileName.ToLower().EndsWith(".jpeg") || file.FileName.ToLower().EndsWith(".jpg"))
                contentFoto.tipo = 0;
            else if (file.FileName.ToLower().EndsWith(".bmp"))
                contentFoto.tipo = 1;
            else if (file.FileName.ToLower().EndsWith(".png"))
                contentFoto.tipo = 2;
            else if (file.FileName.ToLower().EndsWith(".ico"))
                contentFoto.tipo = 3;
            else if (file.FileName.ToLower().EndsWith(".emf") || file.FileName.ToLower().EndsWith(".wmf"))
                contentFoto.tipo = 4;
            contentFoto.Foto = ConvertToBytes(file);
            var Content = new Fotos
            {
                Id_EAdministrativa = contentFoto.Id_EAdministrativa,
                NumAccion = contentFoto.NumAccion,
                Instalacion = contentFoto.Instalacion,
                Version = contentFoto.Version,
                Nombre = contentFoto.Nombre,
                Foto = contentFoto.Foto,
                tipo = contentFoto.tipo
            };
            db.Fotos.Add(Content);
        }
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public async Task CrearAsync(Fotos fotos, HttpPostedFileBase file)
        {
            UploadImageInDataBase(file, fotos);
            await db.SaveChangesAsync();
        }

        public async Task EditarAsync(Fotos fotos, HttpPostedFileBase file)
        {
            if (file.FileName != "")
            {
                fotos.Foto = ConvertToBytes(file);
            }
            db.Entry(fotos).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

    }
}