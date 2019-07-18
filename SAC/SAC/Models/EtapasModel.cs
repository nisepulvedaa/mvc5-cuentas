using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class EtapasModel
    {
        private readonly Models.Resources.SACModel db = new Models.Resources.SACModel();

        public List<DTO.CuentaActiva> obtenerCuentasActivasPorUsuario(int usuarioId, int usuarioPerfil)
        {
            List<DTO.CuentaActiva> lista = new List<DTO.CuentaActiva>();
            try
            {
                IQueryable<Resources.CuentaActiva> cuentasActivas;
                switch (usuarioPerfil)
                {
                    case 3:
                        cuentasActivas = db.CuentaActiva.Where(ca => ca.AnalistaId == usuarioId && ca.EstadoCuenta.EstadoId < 2);
                        break;
                    case 4:
                        cuentasActivas = db.CuentaActiva.Where(ca => ca.ValidadorId == usuarioId && (ca.EstadoCuenta.EstadoId == 2 || ca.EstadoCuenta.EstadoId == 4));
                        break;
                    default:
                        cuentasActivas = db.CuentaActiva.Where(ca => ca.CertificadorId == usuarioId && ca.EstadoCuenta.EstadoId == 3);
                        break;
                }
                foreach (var cuenta in cuentasActivas)
                {
                    lista.Add(DTOBuilder.CuentaActiva(cuenta));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (obtenerCuentasActivasPorValidador): " + ex.Message);
                lista = new List<DTO.CuentaActiva>();
            }
            return lista;
        }
        public List<DTO.CuentaActiva> obtenerCuentasActivasFueraDeEtapaPorUsuario(int usuarioId, int usuarioPerfil)
        {
            List<DTO.CuentaActiva> lista = new List<DTO.CuentaActiva>();
            try
            {
                IQueryable<Resources.CuentaActiva> cuentasActivas;
                switch (usuarioPerfil)
                {
                    case 3:
                        cuentasActivas = db.CuentaActiva.Where(ca => ca.AnalistaId == usuarioId && ca.EstadoCuenta.EstadoId >= 2 && ca.EstadoCuenta.EstadoId != 5);
                        break;
                    case 4:
                        cuentasActivas = db.CuentaActiva.Where(ca => ca.ValidadorId == usuarioId && (ca.EstadoCuenta.EstadoId != 2 && ca.EstadoCuenta.EstadoId != 4 && ca.EstadoCuenta.EstadoId != 5));
                        break;
                    default:
                        cuentasActivas = db.CuentaActiva.Where(ca => ca.CertificadorId == usuarioId && ca.EstadoCuenta.EstadoId != 3 && ca.EstadoCuenta.EstadoId != 5);
                        break;
                }
                foreach (var cuenta in cuentasActivas)
                {
                    lista.Add(DTOBuilder.CuentaActiva(cuenta));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (obtenerCuentasActivasPorValidador): " + ex.Message);
                lista = new List<DTO.CuentaActiva>();
            }
            return lista;
        }

        public List<DTO.CuentaActiva> obtenerCuentasActivasPorValidador(int usuarioId)
        {
            List<DTO.CuentaActiva> lista = new List<DTO.CuentaActiva>();
            try
            {
                var cuentasActivas = db.CuentaActiva.Where(ca => ca.AnalistaId == usuarioId && ca.EstadoCuenta.EstadoId < 2);

                foreach (var cuenta in cuentasActivas)
                {
                    //var c = new DTO.CuentaActiva();
                    //c.cuentaActivaId = cuenta.CuentaActivaId;
                    //c.id = cuenta.CuentaActivaId;
                    //c.cuentaActivaFecha = cuenta.CuentaActivaFecha.Date;
                    //c.cuentaActivaDiasPlazo = cuenta.CuentaActivaDiasPlazo.GetValueOrDefault();
                    //c.cuentaActivaTotal = cuenta.CuentaActivaTotal.GetValueOrDefault();
                    //c.analista = new DTO.Usuario();
                    //c.analista.nombre = cuenta.Usuario.UsuarioNombre;
                    //c.analista.usuarioId = cuenta.Usuario.UsuarioId;
                    //c.validador = new DTO.Usuario();
                    //c.validador.nombre = cuenta.Usuario2.UsuarioNombre;
                    //c.validador.usuarioId = cuenta.Usuario2.UsuarioId;
                    //c.certificador = new DTO.Usuario();
                    //c.certificador.nombre = cuenta.Usuario1.UsuarioNombre;
                    //c.certificador.usuarioId = cuenta.Usuario1.UsuarioId;

                    //c.nombre = cuenta.Cuenta.CuentaNombre;
                    //c.numero = cuenta.Cuenta.CuentaNumero;

                    //c.empresa = new DTO.Empresa();
                    //c.empresa.empresaId = cuenta.Cuenta.Empresa.EmpresaId;
                    //c.empresa.razonSocial = cuenta.Cuenta.Empresa.EmpresaRazonSocial;

                    //lista.Add(c);
                    lista.Add(DTOBuilder.CuentaActiva(cuenta));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (obtenerCuentasActivasPorValidador): " + ex.Message);
                lista = new List<DTO.CuentaActiva>();
            }
            return lista;
        }

        public List<DTO.CuentaActiva> obtenerCuentasActivasPorValidadorFinalizadas(int usuarioId)
        {
            List<DTO.CuentaActiva> lista = new List<DTO.CuentaActiva>();
            try
            {
                var cuentasActivas = db.CuentaActiva.Where(ca => ca.AnalistaId == usuarioId && ca.EstadoCuenta.EstadoId >= 2);

                foreach (var cuenta in cuentasActivas)
                {
                    //var c = new DTO.CuentaActiva();
                    //c.cuentaActivaId = cuenta.CuentaActivaId;
                    //c.id = cuenta.CuentaActivaId;
                    //c.cuentaActivaFecha = cuenta.CuentaActivaFecha.Date;
                    //c.cuentaActivaDiasPlazo = cuenta.CuentaActivaDiasPlazo.GetValueOrDefault();
                    //c.cuentaActivaTotal = cuenta.CuentaActivaTotal.GetValueOrDefault();
                    //c.analista = new DTO.Usuario();
                    //c.analista.nombre = cuenta.Usuario.UsuarioNombre;
                    //c.analista.usuarioId = cuenta.Usuario.UsuarioId;
                    //c.validador = new DTO.Usuario();
                    //c.validador.nombre = cuenta.Usuario2.UsuarioNombre;
                    //c.validador.usuarioId = cuenta.Usuario2.UsuarioId;
                    //c.certificador = new DTO.Usuario();
                    //c.certificador.nombre = cuenta.Usuario1.UsuarioNombre;
                    //c.certificador.usuarioId = cuenta.Usuario1.UsuarioId;

                    //c.nombre = cuenta.Cuenta.CuentaNombre;
                    //c.numero = cuenta.Cuenta.CuentaNumero;

                    //c.empresa = new DTO.Empresa();
                    //c.empresa.empresaId = cuenta.Cuenta.Empresa.EmpresaId;
                    //c.empresa.razonSocial = cuenta.Cuenta.Empresa.EmpresaRazonSocial;

                    //lista.Add(c);
                    lista.Add(DTOBuilder.CuentaActiva(cuenta));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (obtenerCuentasActivasPorValidadorFinalizadas): " + ex.Message);
                lista = new List<DTO.CuentaActiva>();
            }
            return lista;
        }


        public List<DTO.Archivo> obtenerArchivosPorCuentaActiva(int cuentaActivaId, int mes, int año)
        {
            List<DTO.Archivo> lista = new List<DTO.Archivo>();

            var Archivos = db.Archivo.Where(a =>
            a.CuentaActivaId == cuentaActivaId
            && DbFunctions.TruncateTime(a.CuentaActivaFecha).Value.Year == año
            && DbFunctions.TruncateTime(a.CuentaActivaFecha).Value.Month == mes).OrderByDescending(a => a.ArchivoId).ThenByDescending(a => a.ArchivoVersion);

            int ultimaId = -1;
            foreach (var Archivo in Archivos)
            {
                if (Archivo.ArchivoEstado == 0)
                {
                    if (Archivo.ArchivoId != ultimaId)
                    {
                        lista.Add(DTOBuilder.Archivo(Archivo));
                        ultimaId = Archivo.ArchivoId;
                    }
                }
            }

            return lista;
        }


        public List<DTO.Archivo> obtenerArchivosVersionPorCuentaActiva(int cuentaActivaId, int mes, int año)
        {
            List<DTO.Archivo> lista = new List<DTO.Archivo>();

            var Archivos = db.Archivo.Where(a =>
            a.CuentaActivaId == cuentaActivaId
            && DbFunctions.TruncateTime(a.CuentaActivaFecha).Value.Year == año
            && DbFunctions.TruncateTime(a.CuentaActivaFecha).Value.Month == mes).OrderByDescending(a => a.ArchivoId).ThenByDescending(a => a.ArchivoVersion);

            foreach (var Archivo in Archivos)
            {
                lista.Add(DTOBuilder.Archivo(Archivo));
            }

            return lista;
        }





        public List<DTO.Log> obtenerLogsPorCuentaActiva(int cuentaActivaId, int mes, int año)
        {
            List<DTO.Log> lista = new List<DTO.Log>();

            var Logs = db.Log.Where(l =>
            l.CuentaActivaId == cuentaActivaId
            && DbFunctions.TruncateTime(l.CuentaActivaFecha).Value.Year == año
            && DbFunctions.TruncateTime(l.CuentaActivaFecha).Value.Month == mes).OrderByDescending(l => l.CuentaActivaFecha);

            foreach (var Log in Logs)
            {
                lista.Add(DTOBuilder.Log(Log));
            }

            return lista;
        }

        public DTO.Archivo obtenerArchivoPorId(int id, int version)
        {
            DTO.Archivo respuesta = new DTO.Archivo();
            try
            {
                Resources.Archivo archivo = db.Archivo.FirstOrDefault(a => a.ArchivoId == id && a.ArchivoVersion == version);

                return DTOBuilder.Archivo(archivo);

                //respuesta.Id = archivo.ArchivoId;
                //respuesta.Version = archivo.ArchivoVersion;
                //respuesta.Nombre = archivo.ArchivoNombre;
                //respuesta.Data = archivo.ArchivoData;
                //respuesta.Monto = (double)archivo.ArchivoMonto;
                //respuesta.Comentario = archivo.ArchivoComentario;
                //respuesta.Extension = archivo.ArchivoExtension;
                //respuesta.UsuarioCreacion = (int)archivo.UsuarioCreacion;
                //respuesta.CuentaActivaId = (int)archivo.CuentaActivaId;
                //respuesta.CuentaActivaFecha = (DateTime)archivo.CuentaActivaFecha;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (obtenerArchivoPorId): " + ex.Message);
                respuesta = new DTO.Archivo();

            }
            return respuesta;

        }





        public bool IngresarArchivo(string archivoNombre, byte[] archivoData, int archivoMonto, string archivoComentario, string archivoExtension, int usuarioId, int cuentaActivaId, string cuentaActivaFecha)
        {
            try
            {
                db.IngresarArchivo(archivoNombre, archivoData, archivoMonto, archivoComentario, archivoExtension, usuarioId, cuentaActivaId, Convert.ToDateTime(cuentaActivaFecha));

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (IngresarEmpresas): " + ex.Message);
                return false;
            }
        }


        public bool EditarArchivo(int archivoId, int archivoVersion, string nombreArchivo, int montoArchivo, string comentarioArchivo, int usuarioId)
        {
            try
            {

                db.EditarArchivo(archivoId, archivoVersion, nombreArchivo, montoArchivo, comentarioArchivo, usuarioId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (EditarArchivo): " + ex.Message);
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (EditarArchivo): " + ex.InnerException.Message);
                return false;
            }
        }

        public bool EliminarArchivo(int archivoId, int version)
        {
            try
            {
                db.EliminarArchivo(archivoId, version);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (EliminarArchivo): " + ex.Message);
                return false;
            }
        }


        public bool IngresarVersion(int archivoId, int archivoVersion, string nombreArchivo, int montoArchivo, string comentarioArchivo, byte[] archivoData, int usuarioId, string extension, int cuentaActivaId, DateTime cuentaActivaFecha)
        {
            try
            {
                db.IngresarVersion(archivoId, archivoVersion, nombreArchivo, montoArchivo, comentarioArchivo, archivoData, usuarioId, extension, cuentaActivaId, cuentaActivaFecha);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (IngresarVersion): " + ex.Message);
                System.Diagnostics.Debug.WriteLine("                                        : " + ex.InnerException);
                return false;
            }
        }

        public bool CambiarEstadoCuenta(string comentario, int estadoId, int cuentaActivaId, DateTime cuentaActivaFecha)
        {
            try
            {
                db.CambiarEstadoCuenta(comentario, estadoId, cuentaActivaId, cuentaActivaFecha);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (CambiarEstadoCuenta): " + ex.Message);
                return false;
            }
        }

        public byte[] obtenerContenidoArchivo(int archivoId, int version)
        {
            try
            {
                var archivo = (from a in db.Archivo
                               where a.ArchivoId == archivoId && a.ArchivoVersion == version
                               select a.ArchivoData).First();
                return archivo;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (obtenerArchivo): " + ex.Message);
                return null;
            }
        }

        public int ObtenerCuentaActivaEstadoLog(int cuentaActivaId, int mes, int año)
        {
            var logs = db.Log.Where(l => l.CuentaActivaId == cuentaActivaId
                && DbFunctions.TruncateTime(l.LogFecha).Value.Year == año
                && DbFunctions.TruncateTime(l.LogFecha).Value.Month == mes
            ).OrderByDescending(l => l.LogFecha);

            if (logs.Count() > 0)
            {
                return logs.First().LogEstadoId.GetValueOrDefault();
            }

            return 0;
        }

        public int ObtenerCuentaActivaEstadoParaEstado0(int cuentaActivaId, int mes, int año)
        {
            var archivos = db.Archivo.Where(a => a.CuentaActivaId == cuentaActivaId
                && DbFunctions.TruncateTime(a.CuentaActivaFecha).Value.Year == año
                && DbFunctions.TruncateTime(a.CuentaActivaFecha).Value.Month == mes
            );
            if (archivos.Count() > 0)
            {
                return 0;
            }
            return -1;
        }

        public List<DTO.CuentaActiva> obtenerCuentasActivasEnEstadoDos(int usuarioId)
        {
            List<DTO.CuentaActiva> lista = new List<DTO.CuentaActiva>();
            try
            {

                var cuentasActivas = db.CuentaActiva.Where(ca => ca.ValidadorId == usuarioId && ca.CuentaActivaEstado <= 2);



                foreach (var cuenta in cuentasActivas)
                {
                    //var c = new DTO.CuentaActiva();
                    //c.cuentaActivaId = cuenta.CuentaActivaId;
                    //c.id = cuenta.CuentaActivaId;
                    //c.cuentaActivaFecha = cuenta.CuentaActivaFecha.Date;
                    //c.cuentaActivaDiasPlazo = cuenta.CuentaActivaDiasPlazo.GetValueOrDefault();
                    //c.cuentaActivaTotal = cuenta.CuentaActivaTotal.GetValueOrDefault();
                    //c.analista = new DTO.Usuario();
                    //c.analista.nombre = cuenta.Usuario.UsuarioNombre;
                    //c.analista.usuarioId = cuenta.Usuario.UsuarioId;
                    //c.validador = new DTO.Usuario();
                    //c.validador.nombre = cuenta.Usuario2.UsuarioNombre;
                    //c.validador.usuarioId = cuenta.Usuario2.UsuarioId;
                    //c.certificador = new DTO.Usuario();
                    //c.certificador.nombre = cuenta.Usuario1.UsuarioNombre;
                    //c.certificador.usuarioId = cuenta.Usuario1.UsuarioId;

                    //c.nombre = cuenta.Cuenta.CuentaNombre;
                    //c.numero = cuenta.Cuenta.CuentaNumero;

                    //c.empresa = new DTO.Empresa();
                    //c.empresa.empresaId = cuenta.Cuenta.Empresa.EmpresaId;
                    //c.empresa.razonSocial = cuenta.Cuenta.Empresa.EmpresaRazonSocial;
                    //c.cuentaActivaEstado = (int)cuenta.CuentaActivaEstado;
                    //lista.Add(c);
                    lista.Add(DTOBuilder.CuentaActiva(cuenta));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (obtenerCuentasActivasEnEstadoDos): " + ex.Message);
                lista = new List<DTO.CuentaActiva>();
            }
            return lista;
        }

        public List<DTO.CuentaActiva> obtenerCuentasActivasEnEstadoDosFinalizadas(int usuarioId)
        {
            List<DTO.CuentaActiva> lista = new List<DTO.CuentaActiva>();
            try
            {
                var cuentasActivas = db.CuentaActiva.Where(ca => ca.ValidadorId == usuarioId && ca.EstadoCuenta.EstadoId == 3);
                //var cuentasActivas = db.CuentaActiva.Where(ca => DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Year == año && DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Month == mes);
                //var cuentasActivas = from c in db.Cuenta where !(from ca in db.CuentaActiva where ca.CuentaActivaDiasPlazo > 26 select ca.CuentaActivaId).Contains(c.CuentaId) select c;
                foreach (var cuenta in cuentasActivas)
                {
                    //var c = new DTO.CuentaActiva();
                    //c.cuentaActivaId = cuenta.CuentaActivaId;
                    //c.id = cuenta.CuentaActivaId;
                    //c.cuentaActivaFecha = cuenta.CuentaActivaFecha.Date;
                    //c.cuentaActivaDiasPlazo = cuenta.CuentaActivaDiasPlazo.GetValueOrDefault();
                    //c.cuentaActivaTotal = cuenta.CuentaActivaTotal.GetValueOrDefault();
                    //c.analista = new DTO.Usuario();
                    //c.analista.nombre = cuenta.Usuario.UsuarioNombre;
                    //c.analista.usuarioId = cuenta.Usuario.UsuarioId;
                    //c.validador = new DTO.Usuario();
                    //c.validador.nombre = cuenta.Usuario2.UsuarioNombre;
                    //c.validador.usuarioId = cuenta.Usuario2.UsuarioId;
                    //c.certificador = new DTO.Usuario();
                    //c.certificador.nombre = cuenta.Usuario1.UsuarioNombre;
                    //c.certificador.usuarioId = cuenta.Usuario1.UsuarioId;

                    //c.nombre = cuenta.Cuenta.CuentaNombre;
                    //c.numero = cuenta.Cuenta.CuentaNumero;

                    //c.empresa = new DTO.Empresa();
                    //c.empresa.empresaId = cuenta.Cuenta.Empresa.EmpresaId;
                    //c.empresa.razonSocial = cuenta.Cuenta.Empresa.EmpresaRazonSocial;

                    //lista.Add(c);
                    lista.Add(DTOBuilder.CuentaActiva(cuenta));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (obtenerCuentasActivasEnEstadoDosFinalizadas): " + ex.Message);
                lista = new List<DTO.CuentaActiva>();
            }
            return lista;
        }

        public List<DTO.CuentaActiva> obtenerCuentasActivasEnEstadoTres(int usuarioId)
        {
            List<DTO.CuentaActiva> lista = new List<DTO.CuentaActiva>();
            try
            {

                var cuentasActivas = db.CuentaActiva.Where(ca => ca.CertificadorId == usuarioId && ca.CuentaActivaEstado < 5);



                foreach (var cuenta in cuentasActivas)
                {
                    //var c = new DTO.CuentaActiva();
                    //c.cuentaActivaId = cuenta.CuentaActivaId;
                    //c.id = cuenta.CuentaActivaId;
                    //c.cuentaActivaFecha = cuenta.CuentaActivaFecha.Date;
                    //c.cuentaActivaDiasPlazo = cuenta.CuentaActivaDiasPlazo.GetValueOrDefault();
                    //c.cuentaActivaTotal = cuenta.CuentaActivaTotal.GetValueOrDefault();
                    //c.analista = new DTO.Usuario();
                    //c.analista.nombre = cuenta.Usuario.UsuarioNombre;
                    //c.analista.usuarioId = cuenta.Usuario.UsuarioId;
                    //c.validador = new DTO.Usuario();
                    //c.validador.nombre = cuenta.Usuario2.UsuarioNombre;
                    //c.validador.usuarioId = cuenta.Usuario2.UsuarioId;
                    //c.certificador = new DTO.Usuario();
                    //c.certificador.nombre = cuenta.Usuario1.UsuarioNombre;
                    //c.certificador.usuarioId = cuenta.Usuario1.UsuarioId;

                    //c.nombre = cuenta.Cuenta.CuentaNombre;
                    //c.numero = cuenta.Cuenta.CuentaNumero;

                    //c.empresa = new DTO.Empresa();
                    //c.empresa.empresaId = cuenta.Cuenta.Empresa.EmpresaId;
                    //c.empresa.razonSocial = cuenta.Cuenta.Empresa.EmpresaRazonSocial;
                    //c.cuentaActivaEstado = (int)cuenta.CuentaActivaEstado;
                    //lista.Add(c);
                    lista.Add(DTOBuilder.CuentaActiva(cuenta));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (obtenerCuentasActivasEnEstadoDos): " + ex.Message);
                lista = new List<DTO.CuentaActiva>();
            }
            return lista;
        }


        public List<DTO.CuentaActiva> obtenerCuentasActivasEnEstadoTresFinalizadas(int usuarioId)
        {
            List<DTO.CuentaActiva> lista = new List<DTO.CuentaActiva>();
            try
            {
                var cuentasActivas = db.CuentaActiva.Where(ca => ca.CertificadorId == usuarioId && ca.EstadoCuenta.EstadoId == 5);
                //var cuentasActivas = db.CuentaActiva.Where(ca => DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Year == año && DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Month == mes);
                //var cuentasActivas = from c in db.Cuenta where !(from ca in db.CuentaActiva where ca.CuentaActivaDiasPlazo > 26 select ca.CuentaActivaId).Contains(c.CuentaId) select c;
                foreach (var cuenta in cuentasActivas)
                {
                    //var c = new DTO.CuentaActiva();
                    //c.cuentaActivaId = cuenta.CuentaActivaId;
                    //c.id = cuenta.CuentaActivaId;
                    //c.cuentaActivaFecha = cuenta.CuentaActivaFecha.Date;
                    //c.cuentaActivaDiasPlazo = cuenta.CuentaActivaDiasPlazo.GetValueOrDefault();
                    //c.cuentaActivaTotal = cuenta.CuentaActivaTotal.GetValueOrDefault();
                    //c.analista = new DTO.Usuario();
                    //c.analista.nombre = cuenta.Usuario.UsuarioNombre;
                    //c.analista.usuarioId = cuenta.Usuario.UsuarioId;
                    //c.validador = new DTO.Usuario();
                    //c.validador.nombre = cuenta.Usuario2.UsuarioNombre;
                    //c.validador.usuarioId = cuenta.Usuario2.UsuarioId;
                    //c.certificador = new DTO.Usuario();
                    //c.certificador.nombre = cuenta.Usuario1.UsuarioNombre;
                    //c.certificador.usuarioId = cuenta.Usuario1.UsuarioId;

                    //c.nombre = cuenta.Cuenta.CuentaNombre;
                    //c.numero = cuenta.Cuenta.CuentaNumero;

                    //c.empresa = new DTO.Empresa();
                    //c.empresa.empresaId = cuenta.Cuenta.Empresa.EmpresaId;
                    //c.empresa.razonSocial = cuenta.Cuenta.Empresa.EmpresaRazonSocial;

                    //lista.Add(c);
                    lista.Add(DTOBuilder.CuentaActiva(cuenta));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (obtenerCuentasActivasEnEstadoDosFinalizadas): " + ex.Message);
                lista = new List<DTO.CuentaActiva>();
            }
            return lista;
        }



        public List<DTO.CuentaActiva> obtenerCuentasActivas(int mes, int año, int analista = -1, int validador = -1, int certificador = -1)
        {
            List<DTO.CuentaActiva> lista = new List<DTO.CuentaActiva>();
            try
            {
                WhereBuilder<Resources.CuentaActiva> donde = new WhereBuilder<Resources.CuentaActiva>();

                donde.SetWhere(ca =>
                    DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Year == año &&
                    DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Month == mes
                    );
                if (analista != -1)
                {
                    donde.And(ca => ca.Usuario.UsuarioId == analista);
                }
                if (validador != -1)
                {
                    donde.And(ca => ca.Usuario2.UsuarioId == validador);
                }
                if (certificador != -1)
                {
                    donde.And(ca => ca.Usuario1.UsuarioId == certificador);
                }

                var cuentasActivas = db.CuentaActiva.Where(donde.GetWhere()).OrderBy(ca => ca.CuentaActivaFecha);

                foreach (var cuenta in cuentasActivas)
                {
                    lista.Add(DTOBuilder.CuentaActiva(cuenta));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.EtapasModel (obtenerCuentasActivas): " + ex.Message);
                lista = new List<DTO.CuentaActiva>();
            }
            return lista;
        }


    }

}