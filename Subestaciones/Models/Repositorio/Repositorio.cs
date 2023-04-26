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
    public class Repositorio
    {
        private DBContext db;
        public Repositorio(DBContext db)
        {
            this.db = db;
        }

        // Para obtener el listado del código + nombre de las subestaciones de transmisión y distribución
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
                    NombreSubestacion = item.NombreSubestacion,
                    Calle = item.Calle,
                    Numero = item.Numero,
                    Entrecalle1 = item.Entrecalle1,
                    Entrecalle2 = item.Entrecalle2,
                    BarrioPueblo = item.BarrioPueblo,
                    Sucursal = item.Sucursal,
                    Id_EAdministrativa = item.Id_EAdministrativa

                });
            }
            foreach (var item in sub2)
            {
                us.Add(new unionSub
                {
                    Codigo = item.Codigo,
                    NombreSubestacion = item.NombreSubestacion,
                    Calle = item.Calle,
                    Numero = item.Numero,
                    Entrecalle1 = item.Entrecalle1,
                    Entrecalle2 = item.Entrecalle2,
                    BarrioPueblo = item.BarrioPueblo,
                    Sucursal = item.Sucursal,
                    Id_EAdministrativa = item.Id_EAdministrativa

                });
            }
            return us.ToList();

        }

        public unionSub ObtenerUnionSub(string codigo)
        {
            try
            {
                return subs().FirstOrDefault(c => c.Codigo == codigo);
            }
            catch (Exception)
            {
                return null;
            }
        }

        // para obterner listado de circuitos por alta
        public SelectList CircuitosXAlta()
        {
            var circuitoSubt = db.CircuitosSubtransmision.ToList();
            var circuitoPrim = db.CircuitosPrimarios.ToList();
            var Listado = (from Csubst in circuitoSubt
                           select new SelectListItem
                           {
                               Value = Csubst.CodigoCircuito,
                               Text = string.Format("{0}, {1}", Csubst.CodigoCircuito, Csubst.NombreCircuito)
                           }).Union(from cCprim in circuitoPrim
                                    select new SelectListItem
                                    {
                                        Value = cCprim.CodigoCircuito,
                                        Text = string.Format("{0}, {1}", cCprim.CodigoCircuito, cCprim.NombreAntiguo)
                                    }).ToList();
            return new SelectList(Listado, "Value", "Text");
        }
        public List<Subestaciones> subD()
        {
            return (from SubestacionesD in db.Subestaciones
                    select SubestacionesD).ToList();
        }

        public List<SubestacionesTransmision> subT()
        {
            return (from SubestacionesT in db.SubestacionesTransmision
                    select SubestacionesT).ToList();
        }

        public SelectList listaSubestaciones()
        {
            var subD = db.Subestaciones.ToList();
            var subT = db.SubestacionesTransmision.ToList();
            var Listado = (from subsD in subD
                           select new SelectListItem
                           {
                               Value = subsD.Codigo,
                               Text = string.Format("{0}, {1}", subsD.Codigo, subsD.NombreSubestacion)
                           }).Union(from subsT in subT
                                    select new SelectListItem
                                    {
                                        Value = subsT.Codigo,
                                        Text = string.Format("{0}, {1}", subsT.Codigo, subsT.NombreSubestacion
                                        )
                                    }).ToList();
            return new SelectList(Listado, "Value", "Text");
        }

        public async Task<List<CorrientesNominalesPararrayos>> corriente()
        {
            return await (from CorrNomPararrayo in db.CorrientesNominalesPararrayos
                          select CorrNomPararrayo).ToListAsync();
        }

        public async Task<IEnumerable<VoltajesSistemas>> voltaje()
        {
            return await (from tension in db.VoltajesSistemas
                          select tension).ToListAsync();
        }

        public async Task<IEnumerable<NomencladorTension>> voltajeRedCD()
        {
            return await (from tensionRedCD in db.NomencladorTension
                          select tensionRedCD).ToListAsync();
        }

        public async Task<IEnumerable<VoltajesNominalesPararrayos>> voltajePararrayos()
        {
            return await (from voltPara in db.VoltajesNominalesPararrayos
                          select voltPara).ToListAsync();
        }

        public List<EstructurasAdministrativas> almacenes()
        {
            return (from almacen in db.EstructurasAdministrativas
                    where almacen.Tipo == 6
                    select almacen).ToList();
        }

        public List<Capacidades> capacidades()
        {
            return (from capacidad in db.Capacidades
                    select capacidad).ToList();
        }

        public SelectList ClaseBateria()
        {
            List<TipoControlBC> Listado = new List<TipoControlBC>();
            var tipo = new TipoControlBC { tipo = "Plomo - Ácido" };
            Listado.Add(tipo);
            tipo = new TipoControlBC { tipo = "Alcalina" };
            Listado.Add(tipo);

            var clase = (from Lista in Listado
                         select new SelectListItem { Value = Lista.tipo, Text = Lista.tipo }).ToList();
            return new SelectList(clase, "Value", "Text");

        }

        public SelectList TipoSub()
        {
            List<Fase> Listado = new List<Fase>();
            var F = new Fase { Id_Fase = "E", NombreFase = "Subestación de Distribución" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "R", NombreFase = "Subestación de Transmisión" };
            Listado.Add(F);



            var Fase = (from Lista in Listado
                        select new SelectListItem { Value = Lista.Id_Fase, Text = Lista.NombreFase }).ToList();
            return new SelectList(Fase, "Value", "Text");

        }

        //para el número de acción 
        public int GetNumAccion(string tipoSubAccion, string tipoAccion, int? amod)
        {

            var usuario = System.Web.HttpContext.Current.User?.Identity?.Name ?? null;
            string nombre_usuario = System.Web.HttpContext.Current.User.Identity.Name;
            var usuario_logueado = db.Personal.FirstOrDefault(c => c.Nombre == nombre_usuario);
            int id_usuario = short.Parse(usuario_logueado.Id_Persona.ToString());
            int? EAdmin = usuario_logueado.id_EAdministrativa;
            int? EA = usuario_logueado.id_EA_Persona;

            try
            {
                using (SqlConnection conexion = new SqlConnection(WebConfigurationManager.ConnectionStrings["SubestacionesConnection"].ToString()))
                {
                    conexion.Open();
                    SqlCommand command = conexion.CreateCommand();
                    command.CommandText = "EXEC GetNumAccion @EAdmin,@tipoSubAccion,@tipoAccion,@usuario,@EA,@amod,@numAccion OUTPUT SELECT @numAccion";
                    command.Parameters.Add(new SqlParameter("EAdmin", EAdmin));
                    command.Parameters.Add(new SqlParameter("tipoSubAccion", tipoSubAccion));
                    command.Parameters.Add(new SqlParameter("tipoAccion", tipoAccion));
                    command.Parameters.Add(new SqlParameter("usuario", id_usuario));
                    command.Parameters.Add(new SqlParameter("EA", EA));
                    command.Parameters.Add(new SqlParameter("amod", amod));
                    command.Parameters.Add(new SqlParameter("numAccion", amod));

                    var dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        return int.Parse(dr.GetValue(0).ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                return new int();
            }

        }

        public int? GetId_EAdministrativaUsuario()
        {
            var EAdminS = (from Conf in db.Configuracion
                           where Conf.Id_Dato == 1
                           select Conf.Dato).FirstOrDefault();
            int? Id_EAdministrativa = null;
            if (!String.IsNullOrEmpty(EAdminS))
            {
                try
                {
                    Id_EAdministrativa = int.Parse(EAdminS);
                }
                catch (Exception)
                {

                }

            }

            return Id_EAdministrativa;
        }

        public int? GetId_EAdministrativaProv()
        {
            var EAdminS = (from Conf in db.Configuracion
                           where Conf.Id_Dato == 1014
                           select Conf.Dato).FirstOrDefault();
            int? Id_EAdministrativaProv = null;
            if (!String.IsNullOrEmpty(EAdminS))
            {
                try
                {
                    Id_EAdministrativaProv = int.Parse(EAdminS);
                }
                catch (Exception)
                {

                }

            }

            return Id_EAdministrativaProv;
        }

        public List<SelectListItem> ObtenerTiposBloque()
        {
            return new List<SelectListItem>
            {
                //new SelectListItem { Value = "", Text = "" },
                new SelectListItem { Value= "Transformación Salida", Text= "Transformación Salida" },
                new SelectListItem{ Value="Chucheo Salida", Text="Chucheo Salida"}
            };
        }

       
        public List<SelectListItem> ObtenerTiposSalida()
        {
            return new List<SelectListItem> {
                //new SelectListItem { Value = "", Text = "" },
                new SelectListItem { Value= "Exclusiva", Text= "Exclusiva" },
                new SelectListItem{ Value="Distribución", Text="Distribución"}};

        }

        public List<SelectListItem> ObtenerEsquemaBaja()
        {
            return  (from EsquemasB in db.EsquemasBaja
                                       select new SelectListItem 
                                       {
                                           Value = EsquemasB.Id_EsquemaPorBaja.ToString(),
                                           Text = EsquemasB.EsquemaPorBaja +", " + EsquemasB.CodigoMostrado

                                       }).ToList();
        }

        public SelectList ObtenerEsquemaAlta()
        {
            var ListadoEsquemasAlta = (from EsquemasA in db.EsquemasAlta
                                       select new SelectListItem
                                       {
                                           Value = EsquemasA.Id_EsquemaAlta.ToString(),
                                           Text = EsquemasA.EsquemaPorAlta + ", " + EsquemasA.CodigoMostrado
                                       }).ToList();
            return new SelectList(ListadoEsquemasAlta, "Value", "Text");
        }

        public List<SelectListItem> ObtenerSectores()
        {
            var listado = (from sectores in db.Sectores
                           select new SelectListItem
                           {
                               Value = sectores.Id_Sector,
                               Text = sectores.Sector

                           }).ToList();
            listado.Insert(0,new SelectListItem { Value = "", Text = "" });
            return listado;
        }

        public List<SelectListItem> ObtenerVoltajes()
        {
            return (from voltajes in db.VoltajesSistemas
                                   select new SelectListItem
                                   {
                                       Value = voltajes.Id_VoltajeSistema.ToString(),
                                       Text = voltajes.Voltaje.ToString() + " Kv"

                                   }).ToList();
        }

        public SelectList ObtenerEstadosOperativos()
        {
            var ListadoEstados = (from estados in db.Nomenclador_EstadoOperativo
                                   select new SelectListItem
                                   {
                                       Value = estados.Id_EstadoOperativo,
                                       Text = estados.EstadoOperativo

                                   }).ToList();
            return new SelectList(ListadoEstados, "Value", "Text");
        }

        public SelectList ObtenerSucursales()
        {
            var ListadoVoltajes = (from EA in db.EstructurasAdministrativas
                                   join EA1 in db.EstructurasAdministrativas on EA.Subordinada equals EA1.Id_EAdministrativa
                                   join EA2 in db.EstructurasAdministrativas on EA1.Subordinada equals EA2.Id_EAdministrativa
                                   where EA.Tipo == 4
                                   select new SelectListItem
                                   {
                                       Value = EA.Id_EAdministrativa.ToString(),
                                       Text = EA.Nombre + ", " + EA1.Nombre +", " +  EA2.Nombre 

                                   }).ToList();
            return new SelectList(ListadoVoltajes, "Value", "Text");
        }

       

        public SelectList RealizadoPor()
        {
            var EAdminS = GetId_EAdministrativaProv();
            var ListaRealizado = (from persona in db.Personal
                                  where persona.id_EAdministrativa_Prov== EAdminS
                                  select new SelectListItem
                                  {
                                      Value = persona.Id_Persona.ToString(),
                                      Text = persona.Nombre

                                  }).ToList();
                 return new SelectList(ListaRealizado, "Value", "Text");
        }

    }




}