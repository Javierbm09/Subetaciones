using Subestaciones.Models.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Subestaciones.Models.Repositorio
{
    public class TransfTransRepositorio
    {
        private DBContext db;
        public TransfTransRepositorio(DBContext db)
        {
            this.db = db;
        }
        public TransformadorTransmision Find(int EA, int id_transformador, string ubicado)
        {
            var lista = ObtenerListadoTransformador(ubicado);
            return lista.Find(c => c.Id_EAdministrativa == EA && c.Id_Transformador == id_transformador);
        }

        public List<TransformadorTransmision> ObtenerListadoTransformador(string ubicado)
        {
            if (ubicado == "TT") {
                return (from transformador in db.TransformadoresTransmision

                        join subT in db.SubestacionesTransmision on transformador.Codigo equals subT.Codigo

                        join voltPrimaria in db.VoltajesSistemas on transformador.Id_VoltajePrim equals voltPrimaria.Id_VoltajeSistema into voltajePrimario
                        from tensionPrimaria in voltajePrimario.DefaultIfEmpty()

                        join fabricante in db.Fabricantes on transformador.idFabricante equals fabricante.Id_Fabricante into Fab
                        from Fabricantes in Fab.DefaultIfEmpty()


                        join capacidad in db.Capacidades on transformador.Id_Capacidad equals capacidad.Id_Capacidad into capacidades
                        from capacidadTransformador in capacidades.DefaultIfEmpty()

                        select new TransformadorTransmision
                        {
                            Id_EAdministrativa = transformador.Id_EAdministrativa,
                            Id_Transformador = transformador.Id_Transformador,
                            NumAccion = transformador.NumAccion,
                            Subestacion = subT.Codigo + ", " + subT.NombreSubestacion,
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
                            Id_VoltajePrim = tensionPrimaria.Voltaje,
                            Id_Voltaje_Secun = tensionPrimaria.Voltaje,
                            VoltajeTerciario = tensionPrimaria.Voltaje,
                            VoltajeImpulso = transformador.VoltajeImpulso,
                            PesoAceite = transformador.PesoAceite,
                            PesoTotal = transformador.PesoTotal,
                            PesoNucleo = transformador.PesoNucleo,
                            PesoTansporte = transformador.PesoTansporte,
                            NoSerie = transformador.NoSerie,
                            idFabricante = Fabricantes.Nombre + ", " + Fabricantes.Pais,
                            Capacidad = capacidadTransformador.Capacidad,
                            PerdidasVacio = transformador.PerdidasVacio,
                            CantRadiadores = transformador.CantRadiadores,
                            CantVentiladores = transformador.CantVentiladores,
                            TipoRegVoltaje = transformador.TipoRegVoltaje,
                            PosicionTrabajo = transformador.PosicionTrabajo,
                            TipoCajaMando = transformador.TipoCajaMando,
                            TuboExplosor = transformador.TuboExplosor,
                            ValvulaSobrePresion = transformador.ValvulaSobrePresion,
                            NumFase = transformador.NumFase,
                            PorcientoZccPS = transformador.PorcientoZccPS,
                            PorcientoZccST = transformador.PorcientoZccST,
                            PorcientoZccPT = transformador.PorcientoZccPT,
                            Observaciones = transformador.Observaciones,
                            TieneTermosifones = transformador.TieneTermosifones, 
                            PorcientoImpedancia = transformador.PorcientoImpedancia

                        }).ToList(); } else             {
                return (from transformador in db.TransformadoresTransmision

                        join almacenes in db.EstructurasAdministrativas on transformador.Codigo equals almacenes.Dir_Calle
                        join voltPrimaria in db.VoltajesSistemas on transformador.Id_VoltajePrim equals voltPrimaria.Id_VoltajeSistema into voltajePrimario
                        from tensionPrimaria in voltajePrimario.DefaultIfEmpty()

                        join fabricante in db.Fabricantes on transformador.idFabricante equals fabricante.Id_Fabricante into Fab
                        from Fabricantes in Fab.DefaultIfEmpty()


                        join capacidad in db.Capacidades on transformador.Id_Capacidad equals capacidad.Id_Capacidad into capacidades
                        from capacidadTransformador in capacidades.DefaultIfEmpty()

                        select new TransformadorTransmision
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
                            Id_VoltajePrim = tensionPrimaria.Voltaje,
                            Id_Voltaje_Secun = tensionPrimaria.Voltaje,
                            VoltajeTerciario = tensionPrimaria.Voltaje,
                            VoltajeImpulso = transformador.VoltajeImpulso,
                            PesoAceite = transformador.PesoAceite,
                            PesoTotal = transformador.PesoTotal,
                            PesoNucleo = transformador.PesoNucleo,
                            PesoTansporte = transformador.PesoTansporte,
                            NoSerie = transformador.NoSerie,
                            idFabricante = Fabricantes.Nombre + ", " + Fabricantes.Pais,
                            Capacidad = capacidadTransformador.Capacidad,
                            PerdidasVacio = transformador.PerdidasVacio,
                            CantRadiadores = transformador.CantRadiadores,
                            CantVentiladores = transformador.CantVentiladores,
                            TipoRegVoltaje = transformador.TipoRegVoltaje,
                            PosicionTrabajo = transformador.PosicionTrabajo,
                            TipoCajaMando = transformador.TipoCajaMando,
                            TuboExplosor = transformador.TuboExplosor,
                            ValvulaSobrePresion = transformador.ValvulaSobrePresion,
                            NumFase = transformador.NumFase,
                            Observaciones = transformador.Observaciones


                        }).ToList(); }
        }

        public List<TransformadorTransmision> ObtenerListadoTransformadorSub(string sub)
        {
          
                return (from transformador in db.TransformadoresTransmision

                        join subT in db.SubestacionesTransmision on transformador.Codigo equals subT.Codigo

                        join voltPrimaria in db.VoltajesSistemas on transformador.Id_VoltajePrim equals voltPrimaria.Id_VoltajeSistema into voltajePrimario
                        from tensionPrimaria in voltajePrimario.DefaultIfEmpty()

                        join fabricante in db.Fabricantes on transformador.idFabricante equals fabricante.Id_Fabricante into Fab
                        from Fabricantes in Fab.DefaultIfEmpty()


                        join capacidad in db.Capacidades on transformador.Id_Capacidad equals capacidad.Id_Capacidad into capacidades
                        from capacidadTransformador in capacidades.DefaultIfEmpty()
                        where transformador.Codigo == sub
                        select new TransformadorTransmision
                        {
                            Id_EAdministrativa = transformador.Id_EAdministrativa,
                            Id_Transformador = transformador.Id_Transformador,
                            NumAccion = transformador.NumAccion,
                            Subestacion = subT.Codigo + ", " + subT.NombreSubestacion,
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
                            Id_VoltajePrim = tensionPrimaria.Voltaje,
                            Id_Voltaje_Secun = tensionPrimaria.Voltaje,
                            VoltajeTerciario = tensionPrimaria.Voltaje,
                            VoltajeImpulso = transformador.VoltajeImpulso,
                            PesoAceite = transformador.PesoAceite,
                            PesoTotal = transformador.PesoTotal,
                            PesoNucleo = transformador.PesoNucleo,
                            PesoTansporte = transformador.PesoTansporte,
                            NoSerie = transformador.NoSerie,
                            idFabricante = Fabricantes.Nombre + ", " + Fabricantes.Pais,
                            Capacidad = capacidadTransformador.Capacidad,
                            PerdidasVacio = transformador.PerdidasVacio,
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
                    join sector in db.SalidaExclusivaSub on new { id = (short)bloques.Id_bloque, bloques.Codigo } equals new { id = sector.id_bloque, sector.Codigo } into sectores
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

        public TransformadorTransmision EditarTransformador(int EA, int id_transformador, string ubicado)
        {
            if (ubicado == "TT")
            {
                return (from transformador in db.TransformadoresTransmision

                        join subT in db.SubestacionesTransmision on transformador.Codigo equals subT.Codigo

                        join voltPrimaria in db.VoltajesSistemas on transformador.Id_VoltajePrim equals voltPrimaria.Id_VoltajeSistema into voltajePrimario
                        from tensionPrimaria in voltajePrimario.DefaultIfEmpty()

                        join voltSec in db.VoltajesSistemas on transformador.Id_Voltaje_Secun equals voltSec.Id_VoltajeSistema into voltajeSecundario
                        from tensionSecundaria in voltajeSecundario.DefaultIfEmpty()

                        join voltTerc in db.VoltajesSistemas on transformador.VoltajeTerciario equals voltTerc.Id_VoltajeSistema into VT
                        from tensionTerc in VT.DefaultIfEmpty()

                        join fabricante in db.Fabricantes on transformador.idFabricante equals fabricante.Id_Fabricante into Fab
                        from Fabricantes in Fab.DefaultIfEmpty()

                        join capacidad in db.Capacidades on transformador.Id_Capacidad equals capacidad.Id_Capacidad into capacidades
                        from capacidadTransformador in capacidades.DefaultIfEmpty()

                        join bloqTransf in db.Bloque on transformador.Id_Bloque equals (short)bloqTransf.Id_bloque into bloq
                        from bloquesTransformacion in bloq.DefaultIfEmpty()

                        join esquemaBloq in db.EsquemasBaja on bloquesTransformacion.EsquemaPorBaja equals esquemaBloq.Id_EsquemaPorBaja into esquemaBloques
                        from esquema in esquemaBloques.DefaultIfEmpty()

                        join sectorC in db.SalidaExclusivaSub on bloquesTransformacion.Id_bloque equals sectorC.id_bloque into sectores
                        from sectorClientes in sectores.DefaultIfEmpty()

                        join nombSector in db.Sectores on sectorClientes.Sector equals nombSector.Id_Sector into nombreSectores
                        from sectoresBloques in nombreSectores.DefaultIfEmpty()

                        join tensionTerciarioBloque in db.VoltajesSistemas on bloquesTransformacion.VoltajeTerciario equals tensionTerciarioBloque.Id_VoltajeSistema into bloqueTP
                        from bloqueTensionTerciario in bloqueTP.DefaultIfEmpty()

                        join tensionSalida in db.VoltajesSistemas on bloquesTransformacion.VoltajeSecundario equals tensionSalida.Id_VoltajeSistema into bloqueTS
                        from bloqueTensionSalida in bloqueTS.DefaultIfEmpty()
                        where (transformador.Id_EAdministrativa == EA && transformador.Id_Transformador == id_transformador) 

                        select new TransformadorTransmision
                        {
                            Id_EAdministrativa = transformador.Id_EAdministrativa,
                            Id_Transformador = transformador.Id_Transformador,
                            NumAccion = transformador.NumAccion,
                            Codigo = transformador.Codigo,
                            Subestacion = subT.Codigo + ", " + subT.NombreSubestacion,
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
                            Id_VoltajePrim = tensionPrimaria.Voltaje,
                            Id_Voltaje_Secun = tensionSecundaria.Voltaje,
                            VoltajeTerciario = tensionTerc.Voltaje,
                            VoltajeImpulso = transformador.VoltajeImpulso,
                            PesoAceite = transformador.PesoAceite,
                            PesoTotal = transformador.PesoTotal,
                            PesoNucleo = transformador.PesoNucleo,
                            PesoTansporte = transformador.PesoTansporte,
                            NoSerie = transformador.NoSerie,
                            idFabricante = Fabricantes.Nombre + ", " + Fabricantes.Pais,
                            Capacidad = capacidadTransformador.Capacidad,
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
                            esquema = esquema.EsquemaPorBaja,
                            sectorCliente = sectoresBloques.Sector,
                            cliente = sectorClientes.Cliente,
                            tensionTerciarioBloque = bloqueTensionTerciario.Voltaje,
                            tensionSalida = bloqueTensionSalida.Voltaje,
                            tipoSalida = bloquesTransformacion.TipoSalida,
                            BushingPrimFaseATipo = transformador.BushingPrimFaseATipo,
                            BushingPrimFaseASerie = transformador.BushingPrimFaseASerie,
                            BushingPrimFaseAIn = transformador.BushingPrimFaseAIn,
                            BushingPrimFaseAUn = transformador.BushingPrimFaseAUn,
                            BushingPrimFaseBTipo = transformador.BushingPrimFaseBTipo,
                            BushingPrimFaseBSerie = transformador.BushingPrimFaseBSerie,
                            BushingPrimFaseBIn = transformador.BushingPrimFaseBIn,
                            BushingPrimFaseBUn = transformador.BushingPrimFaseBUn,
                            BushingPrimFaseCTipo = transformador.BushingPrimFaseCTipo,
                            BushingPrimFaseCSerie = transformador.BushingPrimFaseCSerie,
                            BushingPrimFaseCIn = transformador.BushingPrimFaseCIn,
                            BushingPrimFaseCUn = transformador.BushingPrimFaseCUn,
                            BushingPrimFaseNeutroTipo = transformador.BushingPrimFaseNeutroTipo,
                            BushingSecFasesTipo = transformador.BushingSecFasesTipo,
                            BushingSecFasesUn = transformador.BushingSecFasesUn,
                            BushingSecFasesIn = transformador.BushingSecFasesIn,
                            BushingSecNeutroTipo = transformador.BushingSecNeutroTipo,
                            BushingTercFasesTipo = transformador.BushingTercFasesTipo,
                            BushingTercFasesUn = transformador.BushingTercFasesUn,
                            BushingTercFasesIn = transformador.BushingTercFasesIn

                        }).FirstOrDefault();
            }
            else return (from transformador in db.TransformadoresTransmision

                         join almacenes in db.EstructurasAdministrativas on transformador.Codigo equals almacenes.Dir_Calle

                         join voltPrimaria in db.VoltajesSistemas on transformador.Id_VoltajePrim equals voltPrimaria.Id_VoltajeSistema into voltajePrimario
                         from tensionPrimaria in voltajePrimario.DefaultIfEmpty()

                         join voltSec in db.VoltajesSistemas on transformador.Id_Voltaje_Secun equals voltSec.Id_VoltajeSistema into voltajeSecundario
                         from tensionSecundaria in voltajeSecundario.DefaultIfEmpty()

                         join voltTerc in db.VoltajesSistemas on transformador.VoltajeTerciario equals voltTerc.Id_VoltajeSistema into VT
                         from tensionTerc in VT.DefaultIfEmpty()

                         join fabricante in db.Fabricantes on transformador.idFabricante equals fabricante.Id_Fabricante into Fab
                         from Fabricantes in Fab.DefaultIfEmpty()

                         join capacidad in db.Capacidades on transformador.Id_Capacidad equals capacidad.Id_Capacidad into capacidades
                         from capacidadTransformador in capacidades.DefaultIfEmpty()
                         where ((transformador.Codigo != null) && (transformador.Codigo != "") && (almacenes.Dir_Calle != null) && (almacenes.Dir_Calle != ""))
                         where (transformador.Id_EAdministrativa == EA && transformador.Id_Transformador == id_transformador)

                         select new TransformadorTransmision
                         {
                             Id_EAdministrativa = transformador.Id_EAdministrativa,
                             Id_Transformador = transformador.Id_Transformador,
                             NumAccion = transformador.NumAccion,
                             Codigo = transformador.Codigo,
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
                             Id_VoltajePrim = tensionPrimaria.Voltaje,
                             Id_Voltaje_Secun = tensionSecundaria.Voltaje,
                             VoltajeTerciario = tensionTerc.Voltaje,
                             VoltajeImpulso = transformador.VoltajeImpulso,
                             PesoAceite = transformador.PesoAceite,
                             PesoTotal = transformador.PesoTotal,
                             PesoNucleo = transformador.PesoNucleo,
                             PesoTansporte = transformador.PesoTansporte,
                             NoSerie = transformador.NoSerie,
                             idFabricante = Fabricantes.Nombre + ", " + Fabricantes.Pais,
                             Capacidad = capacidadTransformador.Capacidad,
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
                             BushingPrimFaseATipo = transformador.BushingPrimFaseATipo,
                             BushingPrimFaseASerie = transformador.BushingPrimFaseASerie,
                             BushingPrimFaseAIn = transformador.BushingPrimFaseAIn,
                             BushingPrimFaseAUn = transformador.BushingPrimFaseAUn,
                             BushingPrimFaseBTipo = transformador.BushingPrimFaseBTipo,
                             BushingPrimFaseBSerie = transformador.BushingPrimFaseBSerie,
                             BushingPrimFaseBIn = transformador.BushingPrimFaseBIn,
                             BushingPrimFaseBUn = transformador.BushingPrimFaseBUn,
                             BushingPrimFaseCTipo = transformador.BushingPrimFaseCTipo,
                             BushingPrimFaseCSerie = transformador.BushingPrimFaseCSerie,
                             BushingPrimFaseCIn = transformador.BushingPrimFaseCIn,
                             BushingPrimFaseCUn = transformador.BushingPrimFaseCUn,
                             BushingPrimFaseNeutroTipo = transformador.BushingPrimFaseNeutroTipo,
                             BushingSecFasesTipo = transformador.BushingSecFasesTipo,
                             BushingSecFasesUn = transformador.BushingSecFasesUn,
                             BushingSecFasesIn = transformador.BushingSecFasesIn,
                             BushingSecNeutroTipo = transformador.BushingSecNeutroTipo,
                             BushingTercFasesTipo = transformador.BushingTercFasesTipo,
                             BushingTercFasesUn = transformador.BushingTercFasesUn,
                             BushingTercFasesIn = transformador.BushingTercFasesIn

                         }).FirstOrDefault();
        }

        public List<Fabricantes> fab()
        {
            return (from Fabricantes in db.Fabricantes
                    where Fabricantes.FTransformadoresSub == true
                    select Fabricantes).ToList();
        }
    }
}
