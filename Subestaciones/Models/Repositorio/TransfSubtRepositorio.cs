using Subestaciones.Models.Clases;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Subestaciones.Models.Repositorio
{
    public class TransfSubtRepositorio
    {
        private DBContext db;
        public TransfSubtRepositorio(DBContext db)
        {
            this.db = db;
        }
        public TransformadorSubtransmision Find(int EA, int id_transformador, string ubicado)
        {
            var lista = ObtenerListadoTransformador(ubicado);
            return lista.Find(c => c.Id_EAdministrativa == EA && c.Id_Transformador == id_transformador);
        }

        public List<TransformadorSubtransmision> ObtenerListadoTransformador(string ubicado)
        {
            if (ubicado == "TS")
            {
                return (from transformador in db.TransformadoresSubtransmision

                        join subD in db.Subestaciones on transformador.Codigo equals subD.Codigo
                        // join enfriamiento in db.Sub_NomEnfriamiento on transformador.TipoEnfriamiento equals enfriamiento.Codigo

                        join voltPrimaria in db.VoltajesSistemas on transformador.Id_VoltajePrim equals voltPrimaria.Id_VoltajeSistema into voltajePrimario
                        from tensionPrimaria in voltajePrimario.DefaultIfEmpty()

                        join fabricante in db.Fabricantes on transformador.idFabricante equals fabricante.Id_Fabricante into Fab
                        from Fabricantes in Fab.DefaultIfEmpty()

                            //join grupoC in db.Sub_grupoconexion on transformador.GrupoConexion equals grupoC.tipo into grupo
                            //from GrupoConexion in grupo.DefaultIfEmpty()

                        join capacidad in db.Capacidades on transformador.Id_Capacidad equals capacidad.Id_Capacidad into capacidades
                        from capacidadTransformador in capacidades.DefaultIfEmpty()

                        select new TransformadorSubtransmision
                        {
                            Id_EAdministrativa = transformador.Id_EAdministrativa,
                            Id_Transformador = transformador.Id_Transformador,
                            NumAccion = transformador.NumAccion,
                            Subestacion = subD.Codigo + ", " + subD.NombreSubestacion,
                            Nombre = transformador.Nombre,
                            NumeroInventario = transformador.NumeroInventario,
                            GrupoConexion = transformador.GrupoConexion,
                            FechaDeInstalado = transformador.FechaDeInstalado,
                            Numemp = transformador.Numemp,
                            Enfriamiento = transformador.TipoEnfriamiento,
                            Frecuencia = transformador.FrecuenciaN,
                            CorrientePrimaria = transformador.CorrienteAlta,
                            CorrienteSecundaria = transformador.CorrienteBaja,
                            Tipo = transformador.Tipo,
                            MaxTemperatura = transformador.MaxTemperatura,
                            AnnoFabricacion = transformador.AnnoFabricacion,
                            PerdidasBajoCarga = transformador.PerdidasBajoCarga,
                            TensionPrimaria = tensionPrimaria.Voltaje,
                            TensionSecundaria = tensionPrimaria.Voltaje,
                            TensionTerciario = tensionPrimaria.Voltaje,
                            TensionImpulso = transformador.VoltajeImpulso,
                            PesoAceite = transformador.PesoAceite,
                            PesoTotal = transformador.PesoTotal,
                            PesoNucleo = transformador.PesoNucleo,
                            PesoTansporte = transformador.PesoTansporte,
                            NoSerie = transformador.NoSerie,
                            Fabricante = Fabricantes.Nombre + ", " + Fabricantes.Pais,
                            capacidad = capacidadTransformador.Capacidad,
                            PerdidasVacio = transformador.PerdidasVacio,
                            PorcientoImpedancia = transformador.PorcientoImpedancia,
                            CantRadiadores = transformador.CantRadiadores,
                            CantVentiladores = transformador.CantVentiladores,
                            TipoRegVoltaje = transformador.TipoRegVoltaje,
                            PosicionTrabajo = transformador.PosicionTrabajo,
                            TipoCajaMando = transformador.TipoCajaMando,
                            TuboExplosor = transformador.TuboExplosor,
                            ValvulaSobrePresion = transformador.ValvulaSobrePresion,
                            NumFase = transformador.NumFase,
                            Observaciones = transformador.Observaciones,
                            NroPosiciones = transformador.NroPosiciones

                        }).ToList();
            }
            else return (from transformador in db.TransformadoresSubtransmision

                         join almacenes in db.EstructurasAdministrativas on transformador.Codigo equals almacenes.Dir_Calle

                         join voltPrimaria in db.VoltajesSistemas on transformador.Id_VoltajePrim equals voltPrimaria.Id_VoltajeSistema into voltajePrimario
                         from tensionPrimaria in voltajePrimario.DefaultIfEmpty()

                         join fabricante in db.Fabricantes on transformador.idFabricante equals fabricante.Id_Fabricante into Fab
                         from Fabricantes in Fab.DefaultIfEmpty()

                         join capacidad in db.Capacidades on transformador.Id_Capacidad equals capacidad.Id_Capacidad into capacidades
                         from capacidadTransformador in capacidades.DefaultIfEmpty()
                         where ((transformador.Codigo != null) && (transformador.Codigo != "") && (almacenes.Dir_Calle != null) && (almacenes.Dir_Calle != ""))
                         select new TransformadorSubtransmision
                         {
                             Id_EAdministrativa = transformador.Id_EAdministrativa,
                             Id_Transformador = transformador.Id_Transformador,
                             NumAccion = transformador.NumAccion,
                             Subestacion = almacenes.Nombre,
                             Nombre = transformador.Nombre,
                             NumeroInventario = transformador.NumeroInventario,
                             GrupoConexion = transformador.GrupoConexion,
                             FechaDeInstalado = transformador.FechaDeInstalado,
                             Numemp = transformador.Numemp,
                             Enfriamiento = transformador.TipoEnfriamiento,
                             Frecuencia = transformador.FrecuenciaN,
                             CorrientePrimaria = transformador.CorrienteAlta,
                             CorrienteSecundaria = transformador.CorrienteBaja,
                             Tipo = transformador.Tipo,
                             MaxTemperatura = transformador.MaxTemperatura,
                             AnnoFabricacion = transformador.AnnoFabricacion,
                             PerdidasBajoCarga = transformador.PerdidasBajoCarga,
                             TensionPrimaria = tensionPrimaria.Voltaje,
                             TensionSecundaria = tensionPrimaria.Voltaje,
                             TensionTerciario = tensionPrimaria.Voltaje,
                             TensionImpulso = transformador.VoltajeImpulso,
                             PesoAceite = transformador.PesoAceite,
                             PesoTotal = transformador.PesoTotal,
                             PesoNucleo = transformador.PesoNucleo,
                             PesoTansporte = transformador.PesoTansporte,
                             NoSerie = transformador.NoSerie,
                             Fabricante = Fabricantes.Nombre + ", " + Fabricantes.Pais,
                             capacidad = capacidadTransformador.Capacidad,
                             PerdidasVacio = transformador.PerdidasVacio,
                             PorcientoImpedancia = transformador.PorcientoImpedancia,
                             CantRadiadores = transformador.CantRadiadores,
                             CantVentiladores = transformador.CantVentiladores,
                             TipoRegVoltaje = transformador.TipoRegVoltaje,
                             PosicionTrabajo = transformador.PosicionTrabajo,
                             TipoCajaMando = transformador.TipoCajaMando,
                             TuboExplosor = transformador.TuboExplosor,
                             ValvulaSobrePresion = transformador.ValvulaSobrePresion,
                             NumFase = transformador.NumFase,
                             Observaciones = transformador.Observaciones,
                             NroPosiciones = transformador.NroPosiciones

                         }).ToList();
        }


        public TransformadorSubtransmision EditarTransformador(int EA, int id_transformador, string ubicado)
        {
            if (ubicado == "TS")
            {
                return (from transformador in db.TransformadoresSubtransmision

                        join subD in db.Subestaciones on transformador.Codigo equals subD.Codigo

                        join voltPrimaria in db.VoltajesSistemas on transformador.Id_VoltajePrim equals voltPrimaria.Id_VoltajeSistema into voltajePrimario
                        from tensionPrimaria in voltajePrimario.DefaultIfEmpty()

                        join fabricante in db.Fabricantes on transformador.idFabricante equals fabricante.Id_Fabricante into Fab
                        from Fabricantes in Fab.DefaultIfEmpty()

                        join capacidad in db.Capacidades on transformador.Id_Capacidad equals capacidad.Id_Capacidad into capacidades
                        from capacidadTransformador in capacidades.DefaultIfEmpty()

                        join bloqTransf in db.Bloque on transformador.Id_Bloque equals (short)bloqTransf.Id_bloque into bloq
                        from bloquesTransformacion in bloq.DefaultIfEmpty()
                        where bloquesTransformacion.Codigo == transformador.Codigo

                        join esquemaBloq in db.EsquemasBaja on bloquesTransformacion.EsquemaPorBaja equals esquemaBloq.Id_EsquemaPorBaja into esquemaBloques
                        from esquema in esquemaBloques.DefaultIfEmpty()

                        join sectorC in db.SalidaExclusivaSub on bloquesTransformacion.Id_bloque equals sectorC.id_bloque into sectores
                        from sectorClientes in sectores.DefaultIfEmpty()

                        join nombSector in db.Sectores on sectorClientes.Sector equals nombSector.Id_Sector into nombreSectores
                        from sectoresBloques in nombreSectores.DefaultIfEmpty()

                        join tensionTerciario in db.VoltajesSistemas on bloquesTransformacion.VoltajeTerciario equals tensionTerciario.Id_VoltajeSistema into bloqueTP
                        from bloqueTensionTerciario in bloqueTP.DefaultIfEmpty()

                        join tensionSalida in db.VoltajesSistemas on bloquesTransformacion.VoltajeSecundario equals tensionSalida.Id_VoltajeSistema into bloqueTS
                        from bloqueTensionSalida in bloqueTS.DefaultIfEmpty()
                        where (transformador.Id_EAdministrativa == EA && transformador.Id_Transformador == id_transformador)
                        select new TransformadorSubtransmision
                        {
                            Id_EAdministrativa = transformador.Id_EAdministrativa,
                            Id_Transformador = transformador.Id_Transformador,
                            NumAccion = transformador.NumAccion,
                            Codigo = transformador.Codigo,
                            Subestacion = subD.Codigo + ", " + subD.NombreSubestacion,
                            Nombre = transformador.Nombre,
                            NumeroInventario = transformador.NumeroInventario,
                            GrupoConexion = transformador.GrupoConexion,
                            FechaDeInstalado = transformador.FechaDeInstalado,
                            Numemp = transformador.Numemp,
                            Enfriamiento = transformador.TipoEnfriamiento,
                            Frecuencia = transformador.FrecuenciaN,
                            CorrientePrimaria = transformador.CorrienteAlta,
                            CorrienteSecundaria = transformador.CorrienteBaja,
                            Tipo = transformador.Tipo,
                            MaxTemperatura = transformador.MaxTemperatura,
                            AnnoFabricacion = transformador.AnnoFabricacion,
                            PerdidasBajoCarga = transformador.PerdidasBajoCarga,
                            TensionPrimaria = tensionPrimaria.Voltaje,
                            TensionSecundaria = tensionPrimaria.Voltaje,
                            TensionTerciario = tensionPrimaria.Voltaje,
                            TensionImpulso = transformador.VoltajeImpulso,
                            PesoAceite = transformador.PesoAceite,
                            PesoTotal = transformador.PesoTotal,
                            PesoNucleo = transformador.PesoNucleo,
                            PesoTansporte = transformador.PesoTansporte,
                            NoSerie = transformador.NoSerie,
                            Fabricante = Fabricantes.Nombre + ", " + Fabricantes.Pais,
                            capacidad = capacidadTransformador.Capacidad,
                            PerdidasVacio = transformador.PerdidasVacio,
                            PorcientoImpedancia = transformador.PorcientoImpedancia,
                            CantRadiadores = transformador.CantRadiadores,
                            CantVentiladores = transformador.CantVentiladores,
                            TipoRegVoltaje = transformador.TipoRegVoltaje,
                            PosicionTrabajo = transformador.PosicionTrabajo,
                            TipoCajaMando = transformador.TipoCajaMando,
                            TuboExplosor = transformador.TuboExplosor,
                            ValvulaSobrePresion = transformador.ValvulaSobrePresion,
                            NumFase = transformador.NumFase,
                            Observaciones = transformador.Observaciones,
                            NroPosiciones = transformador.NroPosiciones,
                            Id_Bloque = transformador.Id_Bloque,
                            Bloque = bloquesTransformacion.tipobloque,
                            esquemaBloque = esquema.EsquemaPorBaja,
                            sectorClienteBloque = sectoresBloques.Sector,
                            clienteBloque = sectorClientes.Cliente,
                            tensionTerciarioBloque = bloqueTensionTerciario.Voltaje,
                            tensionSalidaBloque = bloqueTensionSalida.Voltaje,
                            tipoSalidaBloque = bloquesTransformacion.TipoSalida

                        }).FirstOrDefault();
            }
            else return (from transformador in db.TransformadoresSubtransmision

                         join almacenes in db.EstructurasAdministrativas on transformador.Codigo equals almacenes.Dir_Calle

                         join voltPrimaria in db.VoltajesSistemas on transformador.Id_VoltajePrim equals voltPrimaria.Id_VoltajeSistema into voltajePrimario
                         from tensionPrimaria in voltajePrimario.DefaultIfEmpty()

                         join fabricante in db.Fabricantes on transformador.idFabricante equals fabricante.Id_Fabricante into Fab
                         from Fabricantes in Fab.DefaultIfEmpty()

                         join capacidad in db.Capacidades on transformador.Id_Capacidad equals capacidad.Id_Capacidad into capacidades
                         from capacidadTransformador in capacidades.DefaultIfEmpty()
                         where ((transformador.Codigo != null) && (transformador.Codigo != "") && (almacenes.Dir_Calle != null) && (almacenes.Dir_Calle != ""))
                         where (transformador.Id_EAdministrativa == EA && transformador.Id_Transformador == id_transformador)

                         select new TransformadorSubtransmision
                         {
                             Id_EAdministrativa = transformador.Id_EAdministrativa,
                             Id_Transformador = transformador.Id_Transformador,
                             NumAccion = transformador.NumAccion,
                             Subestacion = almacenes.Nombre,
                             Codigo = transformador.Codigo,
                             Nombre = transformador.Nombre,
                             NumeroInventario = transformador.NumeroInventario,
                             GrupoConexion = transformador.GrupoConexion,
                             FechaDeInstalado = transformador.FechaDeInstalado,
                             Numemp = transformador.Numemp,
                             Enfriamiento = transformador.TipoEnfriamiento,
                             Frecuencia = transformador.FrecuenciaN,
                             CorrientePrimaria = transformador.CorrienteAlta,
                             CorrienteSecundaria = transformador.CorrienteBaja,
                             Tipo = transformador.Tipo,
                             MaxTemperatura = transformador.MaxTemperatura,
                             AnnoFabricacion = transformador.AnnoFabricacion,
                             PerdidasBajoCarga = transformador.PerdidasBajoCarga,
                             TensionPrimaria = tensionPrimaria.Voltaje,
                             TensionSecundaria = tensionPrimaria.Voltaje,
                             TensionTerciario = tensionPrimaria.Voltaje,
                             TensionImpulso = transformador.VoltajeImpulso,
                             PesoAceite = transformador.PesoAceite,
                             PesoTotal = transformador.PesoTotal,
                             PesoNucleo = transformador.PesoNucleo,
                             PesoTansporte = transformador.PesoTansporte,
                             NoSerie = transformador.NoSerie,
                             Fabricante = Fabricantes.Nombre + ", " + Fabricantes.Pais,
                             capacidad = capacidadTransformador.Capacidad,
                             PerdidasVacio = transformador.PerdidasVacio,
                             PorcientoImpedancia = transformador.PorcientoImpedancia,
                             CantRadiadores = transformador.CantRadiadores,
                             CantVentiladores = transformador.CantVentiladores,
                             TipoRegVoltaje = transformador.TipoRegVoltaje,
                             PosicionTrabajo = transformador.PosicionTrabajo,
                             TipoCajaMando = transformador.TipoCajaMando,
                             TuboExplosor = transformador.TuboExplosor,
                             ValvulaSobrePresion = transformador.ValvulaSobrePresion,
                             NumFase = transformador.NumFase,
                             Observaciones = transformador.Observaciones,
                             NroPosiciones = transformador.NroPosiciones


                         }).FirstOrDefault();
        }

        public List<TransformadorSubtransmision> ObtenerListadoTransformadorEnSub(string sub)
        {

            return (from transformador in db.TransformadoresSubtransmision

                    join subD in db.Subestaciones on transformador.Codigo equals subD.Codigo
                    // join enfriamiento in db.Sub_NomEnfriamiento on transformador.TipoEnfriamiento equals enfriamiento.Codigo

                    join voltPrimaria in db.VoltajesSistemas on transformador.Id_VoltajePrim equals voltPrimaria.Id_VoltajeSistema into voltajePrimario
                    from tensionPrimaria in voltajePrimario.DefaultIfEmpty()

                    join fabricante in db.Fabricantes on transformador.idFabricante equals fabricante.Id_Fabricante into Fab
                    from Fabricantes in Fab.DefaultIfEmpty()

                        //join grupoC in db.Sub_grupoconexion on transformador.GrupoConexion equals grupoC.tipo into grupo
                        //from GrupoConexion in grupo.DefaultIfEmpty()

                    join capacidad in db.Capacidades on transformador.Id_Capacidad equals capacidad.Id_Capacidad into capacidades
                    from capacidadTransformador in capacidades.DefaultIfEmpty()
                    where transformador.Codigo == sub

                    select new TransformadorSubtransmision
                    {
                        Id_EAdministrativa = transformador.Id_EAdministrativa,
                        Id_Transformador = transformador.Id_Transformador,
                        NumAccion = transformador.NumAccion,
                        Subestacion = subD.Codigo + ", " + subD.NombreSubestacion,
                        Nombre = transformador.Nombre,
                        NumeroInventario = transformador.NumeroInventario,
                        GrupoConexion = transformador.GrupoConexion,
                        FechaDeInstalado = transformador.FechaDeInstalado,
                        Numemp = transformador.Numemp,
                        Enfriamiento = transformador.TipoEnfriamiento,
                        Frecuencia = transformador.FrecuenciaN,
                        CorrientePrimaria = transformador.CorrienteAlta,
                        CorrienteSecundaria = transformador.CorrienteBaja,
                        Tipo = transformador.Tipo,
                        MaxTemperatura = transformador.MaxTemperatura,
                        AnnoFabricacion = transformador.AnnoFabricacion,
                        PerdidasBajoCarga = transformador.PerdidasBajoCarga,
                        TensionPrimaria = tensionPrimaria.Voltaje,
                        TensionSecundaria = tensionPrimaria.Voltaje,
                        TensionTerciario = tensionPrimaria.Voltaje,
                        TensionImpulso = transformador.VoltajeImpulso,
                        PesoAceite = transformador.PesoAceite,
                        PesoTotal = transformador.PesoTotal,
                        PesoNucleo = transformador.PesoNucleo,
                        PesoTansporte = transformador.PesoTansporte,
                        NoSerie = transformador.NoSerie,
                        Fabricante = Fabricantes.Nombre + ", " + Fabricantes.Pais,
                        capacidad = capacidadTransformador.Capacidad,
                        PerdidasVacio = transformador.PerdidasVacio,
                        PorcientoImpedancia = transformador.PorcientoImpedancia,
                        CantRadiadores = transformador.CantRadiadores,
                        CantVentiladores = transformador.CantVentiladores,
                        TipoRegVoltaje = transformador.TipoRegVoltaje,
                        PosicionTrabajo = transformador.PosicionTrabajo,
                        TipoCajaMando = transformador.TipoCajaMando,
                        TuboExplosor = transformador.TuboExplosor,
                        ValvulaSobrePresion = transformador.ValvulaSobrePresion,
                        NumFase = transformador.NumFase,
                        Observaciones = transformador.Observaciones

                    }).ToList();
        }

        public List<TransformadorSubtransmision> ObtenerListadoTransformadorAlmacen()
        {
            return (from transformador in db.TransformadoresSubtransmision

                    join almacenes in db.EstructurasAdministrativas on transformador.Codigo equals almacenes.Dir_Calle                    // join enfriamiento in db.Sub_NomEnfriamiento on transformador.TipoEnfriamiento equals enfriamiento.Codigo

                    join voltPrimaria in db.VoltajesSistemas on transformador.Id_VoltajePrim equals voltPrimaria.Id_VoltajeSistema into voltajePrimario
                    from tensionPrimaria in voltajePrimario.DefaultIfEmpty()

                    join fabricante in db.Fabricantes on transformador.idFabricante equals fabricante.Id_Fabricante into Fab
                    from Fabricantes in Fab.DefaultIfEmpty()

                        //join grupoC in db.Sub_grupoconexion on transformador.GrupoConexion equals grupoC.tipo into grupo
                        //from GrupoConexion in grupo.DefaultIfEmpty()

                    join capacidad in db.Capacidades on transformador.Id_Capacidad equals capacidad.Id_Capacidad into capacidades
                    from capacidadTransformador in capacidades.DefaultIfEmpty()

                    select new TransformadorSubtransmision
                    {
                        Id_EAdministrativa = transformador.Id_EAdministrativa,
                        Id_Transformador = transformador.Id_Transformador,
                        NumAccion = transformador.NumAccion,
                        Subestacion = almacenes.Nombre,
                        Nombre = transformador.Nombre,
                        NumeroInventario = transformador.NumeroInventario,
                        GrupoConexion = transformador.GrupoConexion,
                        FechaDeInstalado = transformador.FechaDeInstalado,
                        Numemp = transformador.Numemp,
                        Enfriamiento = transformador.TipoEnfriamiento,
                        Frecuencia = transformador.FrecuenciaN,
                        CorrientePrimaria = transformador.CorrienteAlta,
                        CorrienteSecundaria = transformador.CorrienteBaja,
                        Tipo = transformador.Tipo,
                        MaxTemperatura = transformador.MaxTemperatura,
                        AnnoFabricacion = transformador.AnnoFabricacion,
                        PerdidasBajoCarga = transformador.PerdidasBajoCarga,
                        TensionPrimaria = tensionPrimaria.Voltaje,
                        TensionSecundaria = tensionPrimaria.Voltaje,
                        TensionTerciario = tensionPrimaria.Voltaje,
                        TensionImpulso = transformador.VoltajeImpulso,
                        PesoAceite = transformador.PesoAceite,
                        PesoTotal = transformador.PesoTotal,
                        PesoNucleo = transformador.PesoNucleo,
                        PesoTansporte = transformador.PesoTansporte,
                        NoSerie = transformador.NoSerie,
                        Fabricante = Fabricantes.Nombre + ", " + Fabricantes.Pais,
                        capacidad = capacidadTransformador.Capacidad,
                        PerdidasVacio = transformador.PerdidasVacio,
                        PorcientoImpedancia = transformador.PorcientoImpedancia,
                        CantRadiadores = transformador.CantRadiadores,
                        CantVentiladores = transformador.CantVentiladores,
                        TipoRegVoltaje = transformador.TipoRegVoltaje,
                        PosicionTrabajo = transformador.PosicionTrabajo,
                        TipoCajaMando = transformador.TipoCajaMando,
                        TuboExplosor = transformador.TuboExplosor,
                        ValvulaSobrePresion = transformador.ValvulaSobrePresion,
                        NumFase = transformador.NumFase,
                        Observaciones = transformador.Observaciones

                    }).ToList();
        }


        public List<BloqueTransformacion> Bloques(string codsub)
        {
            return (from bloques in db.Bloque
                    join esquemas in db.EsquemasBaja on bloques.EsquemaPorBaja equals esquemas.Id_EsquemaPorBaja into Esq
                    from EsquemasXBaja in Esq.DefaultIfEmpty()
                    join voltajeTerciario in db.VoltajesSistemas on bloques.VoltajeTerciario equals voltajeTerciario.Id_VoltajeSistema into tensionT
                    from TensionTerciario in tensionT.DefaultIfEmpty()
                    join voltajeSecundario in db.VoltajesSistemas on bloques.VoltajeSecundario equals voltajeSecundario.Id_VoltajeSistema into tensionS
                    from TensionSecundario in tensionS.DefaultIfEmpty()
                    join sector in db.SalidaExclusivaSub on new { id = (short)bloques.Id_bloque, bloques.Codigo } equals new { id = (short)sector.id_bloque, sector.Codigo } into sectores
                    from SectoresCliente in sectores.DefaultIfEmpty()
                    join tipoSector in db.Sectores on SectoresCliente.Sector equals tipoSector.Id_Sector into cliente
                    from ClienteSector in cliente.DefaultIfEmpty()
                    where bloques.Codigo.Equals(codsub)
                    select new BloqueTransformacion
                    {
                        Id_bloque = bloques.Id_bloque,
                        tipobloque = bloques.tipobloque,
                        EsquemaPorBaja = EsquemasXBaja.EsquemaPorBaja,
                        VoltajeSalida = TensionSecundario.Voltaje,
                        VoltajeTerciario = TensionTerciario.Voltaje,
                        TipoSalida = bloques.TipoSalida,
                        Sector = ClienteSector.Sector,
                        Cliente = SectoresCliente.Cliente

                    }).ToList();
        }

        public List<Fabricantes> fab()
        {
            return (from Fabricantes in db.Fabricantes
                    where Fabricantes.FTransformadoresSub == true
                    select Fabricantes).ToList();
        }

        public List<Sub_NomEnfriamiento> Enfriamiento()
        {
            return (from Enf in db.Sub_NomEnfriamiento
                    select Enf).ToList();
        }

        public List<VoltajesSistemas> voltajePrimario()
        {
            return (from tension in db.VoltajesSistemas
                    where tension.TransfSubPrimario == true
                    select tension).ToList();
        }

        public List<VoltajesSistemas> voltajeSecundario()
        {
            return (from tension in db.VoltajesSistemas
                    where tension.TransfSubSecundario == true
                    select tension).ToList();
        }

        public List<VoltajesSistemas> voltajeTerciario()
        {
            return (from tension in db.VoltajesSistemas
                    where tension.TransfSubTerciario == true
                    select tension).ToList();
        }

        public SelectList EstadoO()
        {
            List<Fase> Listado = new List<Fase>();
            var F = new Fase { Id_Fase = "S", NombreFase = "En Servicio" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "D", NombreFase = "Desconectado" };
            Listado.Add(F);

            var Fase = (from Lista in Listado
                        select new SelectListItem { Value = Lista.Id_Fase, Text = Lista.NombreFase }).ToList();
            return new SelectList(Fase, "Value", "Text");

        }

        public SelectList TuboE()
        {
            List<Fase> Listado = new List<Fase>();
            var F = new Fase { Id_Fase = "true", NombreFase = "Si" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "false", NombreFase = "No" };
            Listado.Add(F);

            var Fase = (from Lista in Listado
                        select new SelectListItem { Value = Lista.Id_Fase, Text = Lista.NombreFase }).ToList();
            return new SelectList(Fase, "Value", "Text");

        }

        public SelectList numFase()
        {
            List<Fase> Listado = new List<Fase>();
            var F = new Fase { Id_Fase = "1", NombreFase = "1" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "2", NombreFase = "2" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "3", NombreFase = "3" };
            Listado.Add(F);
            var Fase = (from Lista in Listado
                        select new SelectListItem { Value = Lista.Id_Fase, Text = Lista.NombreFase }).ToList();
            return new SelectList(Fase, "Value", "Text");

        }

        public SelectList Valvula()
        {
            List<Fase> Listado = new List<Fase>();
            var F = new Fase { Id_Fase = "true", NombreFase = "Si" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "false", NombreFase = "No" };
            Listado.Add(F);

            var Fase = (from Lista in Listado
                        select new SelectListItem { Value = Lista.Id_Fase, Text = Lista.NombreFase }).ToList();
            return new SelectList(Fase, "Value", "Text");

        }

        public SelectList Termosifones()
        {
            List<Fase> Listado = new List<Fase>();
            var F = new Fase { Id_Fase = "true", NombreFase = "Si" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "false", NombreFase = "No" };
            Listado.Add(F);

            var Fase = (from Lista in Listado
                        select new SelectListItem { Value = Lista.Id_Fase, Text = Lista.NombreFase }).ToList();
            return new SelectList(Fase, "Value", "Text");

        }

        public SelectList RegVolt()
        {
            List<Fase> Listado = new List<Fase>();
            var F = new Fase { Id_Fase = "Manual", NombreFase = "Manual" };
            Listado.Add(F);
            F = new Fase { Id_Fase = "Bajo Carga", NombreFase = "Bajo Carga" };
            Listado.Add(F);

            var Fase = (from Lista in Listado
                        select new SelectListItem { Value = Lista.Id_Fase, Text = Lista.NombreFase }).ToList();
            return new SelectList(Fase, "Value", "Text");

        }

        public List<Sub_grupoconexion> GrupoConexion()
        {
            return (from grupos in db.Sub_grupoconexion
                    join sub_grupos in db.Sub_TipoTransf_Grupoconexion on grupos.id_tipo equals sub_grupos.id_grupoconexion into GC
                    from GrupoC in GC.DefaultIfEmpty()
                    where GrupoC.id_transformador == 2
                    select grupos).ToList();
        }


        public List<SubTermometrosTransfSubTransmision> ObtenerListaTermometros(int transf)
        {
            var parametroTransf = new SqlParameter("@t", transf);
            return db.Database.SqlQuery<SubTermometrosTransfSubTransmision>(@"Select * from SubTermometrosTransfSubTransmision where Id_Transformador=@t", parametroTransf).ToList();

        }


        public void InsertarTermometro(int EA, int numA, int transf, string termometro, string tipo, double rango)
        {
            try
            {
                SubTermometrosTransfSubTransmision termometroInsertar = new SubTermometrosTransfSubTransmision();
                termometroInsertar.Id_EAdministrativa = EA;
                termometroInsertar.NumAccion = numA;
                termometroInsertar.Id_Transformador = transf;
                termometroInsertar.Numero = termometro;
                termometroInsertar.Tipo = tipo;
                termometroInsertar.Rango = rango;
                db.Entry(termometroInsertar).State = EntityState.Added;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Ocurrió un error al insertar el termométro.");
            }

        }

        public void ActualizarTermo(int EA, int numA, int transf, int idt, string termometro, string tipo, double rango)
        {
            SubTermometrosTransfSubTransmision termometroEditar = db.SubTermometrosTransfSubTransmision.Find(EA, transf, idt, numA);
            if (termometroEditar != null)
            {
                termometroEditar.Id_EAdministrativa = EA;
                termometroEditar.Id_Transformador = transf;
                termometroEditar.Id_Termometro = idt;
                termometroEditar.NumAccion = numA;
                termometroEditar.Numero = termometro;
                termometroEditar.Tipo = tipo;
                termometroEditar.Rango = rango;


                db.Entry(termometroEditar).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException();
            }

        }

        [HttpPost]
        public void EliminarTermometro(int EA, int id_t, short? id, int NumAccion)
        {

            SubTermometrosTransfSubTransmision termo = db.SubTermometrosTransfSubTransmision.Find(EA, id_t, id, NumAccion);

            if (termo != null)
            {
                try
                {
                    db.SubTermometrosTransfSubTransmision.Remove(termo);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
            else
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Lo sentimos no se puede eliminar, el termómetro no existe.");
            }

        }

    }
}