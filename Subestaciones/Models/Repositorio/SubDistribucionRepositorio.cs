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
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Subestaciones.Models.Repositorio
{
    public class SubDistribucionRepositorio
    {
        private DBContext db;
        public SubDistribucionRepositorio(DBContext db)
        {
            this.db = db;
        }
        public async Task<SubestacionesDistribucion> FindAsync(string codigo)
        {
            var lista = await ObtenerListadoSubD();
            return lista.Find(c => c.Codigo == codigo);
        }

        public async Task<List<SubestacionesDistribucion>> ObtenerListadoSubD()
        {
            return await (from subD in db.Subestaciones
                          join EO in db.Nomenclador_EstadoOperativo on subD.EstadoOperativo equals EO.Id_EstadoOperativo into SubEO
                          from EstadoOperativo in SubEO.DefaultIfEmpty()
                          join volt in db.VoltajesSistemas on subD.VoltajeNominal equals volt.Id_VoltajeSistema into voltaje
                          from subvolt in voltaje.DefaultIfEmpty()
                          join esquema in db.EsquemasAlta on subD.TipoSubestacion equals esquema.Id_EsquemaAlta into EsquemaxAlta
                          from EsquemasAlta in EsquemaxAlta.DefaultIfEmpty()
                              //join bloques in db.Bloque on subD.Codigo equals bloques.Codigo into BloqueD
                              //from bloqueTransf in BloqueD.DefaultIfEmpty()
                          select new SubestacionesDistribucion
                          {
                              Codigo = subD.Codigo,
                              CodigoAntiguo = subD.CodigoAntiguo,
                              TipoSubestacion = subD.TipoSub,
                              NumeroSalidas = subD.NumeroSalidas,
                              NombreSubestacion = subD.NombreSubestacion,
                              TipoTercero = subD.Tipo_Terceros,
                              EsquemaAlta = EsquemasAlta.EsquemaPorAlta,
                              EstadoOperativo = EstadoOperativo.EstadoOperativo,
                              VoltajeNominal = subvolt.Voltaje

                          }).ToListAsync();
        }



        public SelectList Seccionalizadores(string CodCircuito)
        {
            var Listado = (from desc in db.InstalacionDesconectivos
                           where desc.CircuitoA == CodCircuito || desc.UbicadaEn == CodCircuito
                           orderby desc.Codigo
                           select new SelectListItem { Value = desc.Codigo, Text = desc.Codigo }).ToList();
            return new SelectList(Listado, "Value", "Text");

        }

        public SelectList ObtenerTiposSubestacion()
        {
            return new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "" },
                new SelectListItem { Value= "Transformación", Text= "Transformación" },
                new SelectListItem{ Value="Chucheo", Text="Chucheo"},
                 new SelectListItem{ Value="Múltiples Bloques", Text="Múltiples Bloques"}
            }, "Value", "Text");
        }

        public SelectList ObtenerTipoTerceros()
        {
            return new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "" },
                new SelectListItem { Value= "true", Text= "Si" },
                new SelectListItem{ Value= "false", Text="No"},
                   }, "Value", "Text");
        }
        public List<SelectListItem> ObtenerPriorizados()
        {
            var listado = new List<SelectListItem>();
            listado.Add(new SelectListItem { Value = "", Text = "" });
            listado.Add(new SelectListItem { Value = "true", Text = "Si" });
            listado.Add(new SelectListItem { Value = "false", Text = "No" });
            return listado;
        }

        public async Task<List<Emplazamiento_Sigere>> ObtenerListaCentralesE(string sub)
        {
            List<Emplazamiento_Sigere> centralesElectricas = new List<Emplazamiento_Sigere>();

            if (!String.IsNullOrEmpty(sub))
            {
                //string consulta = "select Subestacion, Circuito, Seccionalizador from Sub_LineasSubestacion where Subestacion=@sub";

                string consulta = "select * from Emplazamiento_Sigere where CentroTransformacion=@sub";


                centralesElectricas = await db.Database.SqlQuery<Emplazamiento_Sigere>(consulta, new SqlParameter("sub", sub)).ToListAsync();
            }

            return centralesElectricas;

        }

        public async Task<List<Circuitos_Baja>> ObtenerListaCircuitosBaja(string sub)
        {
            List<Circuitos_Baja> circuitosB = new List<Circuitos_Baja>();

            if (!String.IsNullOrEmpty(sub))
            {
                //string consulta = "select Subestacion, Circuito, Seccionalizador from Sub_LineasSubestacion where Subestacion=@sub";

                string consulta = "SELECT Concat (CP.CodigoCircuito, ' ', CP.NombreAntiguo) as CodigoCircuito, CP.DesconectivoPrincipal DesconectivoPrincipal FROM CircuitosPrimarios CP WHERE CP.SubAlimentadora = @sub union SELECT concat (CP.CodigoCircuito ,' ', CP.NombreCircuito) as CodigoCircuito, CP.DesconectivoSalida DesconectivoPrincipal FROM dbo.CircuitosSubtransmision CP WHERE CP.SubestacionTransmision = @sub";
                circuitosB = await db.Database.SqlQuery<Circuitos_Baja>(consulta, new SqlParameter("sub", sub)).ToListAsync();
            }

            return circuitosB;

        }

        public async Task<List<Sub_LineasSubestacion>> ObtenerListaCircuitosAlta(string sub)
        {
            List<Sub_LineasSubestacion> circuitosAlta = new List<Sub_LineasSubestacion>();

            if (!String.IsNullOrEmpty(sub))
            {
                
                string consulta = "select Subestacion, CodigoCircuito +', ' + NombreCircuito as Circuito, Seccionalizador from Sub_LineasSubestacion inner join CircuitosSubtransmision on Circuito = CodigoCircuito where Subestacion=@sub union select Subestacion, CodigoCircuito + ', ' + NombreAntiguo as Circuito, Seccionalizador from Sub_LineasSubestacion inner join CircuitosPrimarios on Circuito = CodigoCircuito where Subestacion=@sub";

                circuitosAlta = await db.Database.SqlQuery<Sub_LineasSubestacion>(consulta, new SqlParameter("sub", sub)).ToListAsync();
            }

            return circuitosAlta;

        }

        public async Task<List<BloqueSub>> ObtenerListaBloquesTransformacion(string sub)
        {
            List<BloqueSub> bloques = new List<BloqueSub>();

            if (!String.IsNullOrEmpty(sub))
            {
                string consulta = "select Bloque.Codigo, Bloque.Id_bloque id_bloque, tipobloque, Bloque.EsquemaPorBaja idEsquemaPorBaja, EsquemasBaja.EsquemaPorBaja, bloque.VoltajeSecundario idVoltajeSecundario, Bloque.VoltajeTerciario idVoltajeTerciario, V.Voltaje VoltajeSecundario, VS.Voltaje VoltajeTerciario, TipoSalida, Priorizado, SalidaExclusivaSub.Sector idSector, Sectores.Sector, Cliente from Bloque left join VoltajesSistemas V on Bloque.VoltajeSecundario = V.Id_VoltajeSistema left join VoltajesSistemas VS on Bloque.VoltajeTerciario = VS.Id_VoltajeSistema left join EsquemasBaja on Bloque.EsquemaPorBaja=EsquemasBaja.Id_EsquemaPorBaja left join SalidaExclusivaSub on Bloque.Id_bloque = SalidaExclusivaSub.id_bloque and Bloque.Codigo = SalidaExclusivaSub.Codigo left join Sectores on SalidaExclusivaSub.Sector = Sectores.Id_Sector where Bloque.Codigo=@sub";
                bloques = await db.Database.SqlQuery<BloqueSub>(consulta, new SqlParameter("sub", sub)).ToListAsync();
            }

            return bloques;

        }

        public async Task InsertaCircuitoAlta(string sub, string circ, string secc)
        {
            if (!await ValidarSiExisteCircuito(sub, circ))
            {
                try
                {
                    Sub_LineasSubestacion circuitosAlta = new Sub_LineasSubestacion();
                    circuitosAlta.Subestacion = sub;
                    circuitosAlta.Circuito = circ;
                    circuitosAlta.Seccionalizador = secc;

                    db.Entry(circuitosAlta).State = EntityState.Added;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw new HttpException((int)HttpStatusCode.BadRequest, "Ocurrió un error al insertar el circuito.");
                }
            }
            else
            {
                throw new HttpException(httpCode: (int)HttpStatusCode.Conflict, message: "Lo sentimos no se puede insertar el circuito, ya existe en la subestación.");

            }
        }

        public void ActualizarCircuito(string sub, string circuitoAnterior, string circuito, string secc)
        {
            Sub_LineasSubestacion circuitoParaActualizar = db.Sub_LineasSubestacion.Find(sub, circuitoAnterior);
            if (circuitoParaActualizar != null)
            {
                circuitoParaActualizar.Subestacion = sub;
                circuitoParaActualizar.Circuito = circuito;
                circuitoParaActualizar.Seccionalizador = secc;

                db.Entry(circuitoParaActualizar).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException();
            }

        }


        public void EliminarCircuito(string codSub, string codCircuito)
        {
            Sub_LineasSubestacion circuito = db.Sub_LineasSubestacion.Find(codSub, codCircuito);
            if (circuito != null)
            {
                try
                {
                    db.Sub_LineasSubestacion.Remove(circuito);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
            else
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Lo sentimos no se puede eliminar, el circuito no existe.");
            }
        }

        public void ActualizarBloque(BloqueSub bloque)
        {
            Bloque bloqueActualizar = db.Bloque.Find(bloque.Codigo, bloque.Id_bloque);
            if (bloqueActualizar != null)
            {
                bloqueActualizar.Codigo = bloque.Codigo;
                bloqueActualizar.Id_bloque = bloque.Id_bloque;
                bloqueActualizar.tipobloque = bloque.tipobloque;
                bloqueActualizar.TipoSalida = bloque.TipoSalida;
                bloqueActualizar.EsquemaPorBaja = bloque.idEsquemaPorBaja;
                bloqueActualizar.VoltajeSecundario = bloque.idVoltajeSecundario;
                bloqueActualizar.VoltajeTerciario = bloque.idVoltajeTerciario;
                bloqueActualizar.sector = bloque.Sector;
                bloqueActualizar.Cliente = bloque.Cliente;
                bloqueActualizar.Priorizado = bloque.Priorizado;

                db.Entry(bloqueActualizar).State = EntityState.Modified;

                if ((bloque.TipoSalida != "") && (bloque.TipoSalida == "Exclusiva"))
                {
                    SalidaExclusivaSub salidaActualizar = db.SalidaExclusivaSub.Find(bloque.Codigo, bloque.Id_bloque);
                    if (salidaActualizar != null)
                    {
                        salidaActualizar.Id_EAdministrativa = salidaActualizar.Id_EAdministrativa;
                        salidaActualizar.NumAccion = salidaActualizar.NumAccion;
                        salidaActualizar.Codigo = bloque.Codigo;
                        salidaActualizar.id_bloque = (short)bloque.Id_bloque;
                        salidaActualizar.Sector = bloque.idSector;
                        salidaActualizar.Cliente = bloque.Cliente;
                    };
                    db.Entry(salidaActualizar).State = EntityState.Modified;
                }
                else
                    if ((bloque.TipoSalida != "") && (bloque.TipoSalida != "Exclusiva"))
                {
                    SalidaExclusivaSub salidaEliminar = db.SalidaExclusivaSub.Find(bloque.Codigo, bloque.Id_bloque);
                    if (salidaEliminar != null)
                    {
                        db.SalidaExclusivaSub.Remove(salidaEliminar);

                    }
                }

                    db.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException();
            }

        }


        public void EliminarBloque(string codSub, int idBloque)
        {
            Bloque bloqueElimnar = db.Bloque.Find(codSub, idBloque);
            if (bloqueElimnar != null)
            {
                SalidaExclusivaSub salidaEliminar = db.SalidaExclusivaSub.Find(codSub, idBloque);
                if (salidaEliminar != null)
                {
                    db.SalidaExclusivaSub.Remove(salidaEliminar);

                }
                try
                {
                    db.Bloque.Remove(bloqueElimnar);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
            else
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Lo sentimos no se puede eliminar, el bloque no existe.");
            }


        }

        public void InsertaBloque(string sub, string tipoBloque, string tipoSalida, short esquema, bool priorizado, short? VS, short? VT, string sector, string Cliente, int EA, int numA)
        {
            var id_bloque = db.Database.SqlQuery<int>(@"declare @numBloque int
                                                                            Select @numBloque = Max(Id_bloque) + 1
                                                                            From Bloque
                                                                            Where Codigo = {0}
                                                                            if @numBloque is null
                                                                            set @numBloque = 1
                                                                            Select @numBloque as idBloque", sub).First();
            try
            {
                Bloque bloque = new Bloque
                {
                    Id_bloque = id_bloque,
                    tipobloque = tipoBloque,
                    TipoSalida = tipoSalida,
                    EsquemaPorBaja = esquema,
                    Codigo = sub,
                    Priorizado = priorizado,
                    VoltajeSecundario = VS,
                    VoltajeTerciario = VT
                };
                db.Entry(bloque).State = EntityState.Added;

                if ((tipoSalida != "") && (tipoSalida == "Exclusiva"))
                {
                    SalidaExclusivaSub salidaExclusivaSub = new SalidaExclusivaSub
                    {
                        Id_EAdministrativa = EA,
                        NumAccion = numA,
                        Codigo = sub,
                        id_bloque = (short)id_bloque,
                        Sector = sector,
                        Cliente = Cliente
                    };
                    db.SalidaExclusivaSub.Add(salidaExclusivaSub);
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);

                throw new HttpException((int)HttpStatusCode.BadRequest, "Ocurrió un error al insertar el bloque.");
            }

        }

        public async Task<bool> ValidarSiExisteCircuito(string sub, string circuito)
        {
        return await db.Sub_LineasSubestacion.AnyAsync(c => c.Subestacion == sub && c.Circuito == circuito);
        }

}
}