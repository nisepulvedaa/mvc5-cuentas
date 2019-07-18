using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class DTOBuilder
    {
        public static DTO.Archivo Archivo(Resources.Archivo recArchivo)
        {
            DTO.Archivo archivo = new DTO.Archivo();

            archivo.Id = recArchivo.ArchivoId;
            archivo.Version = recArchivo.ArchivoVersion;
            archivo.Nombre = recArchivo.ArchivoNombre;
            archivo.Fecha = recArchivo.ArchivoFecha.GetValueOrDefault();
            archivo.Data = new byte[0];
            archivo.Monto = recArchivo.ArchivoMonto.GetValueOrDefault();
            archivo.Comentario = recArchivo.ArchivoComentario;
            archivo.Extension = recArchivo.ArchivoExtension;
            archivo.Estado = (int)recArchivo.ArchivoEstado;
            archivo.UsuarioCreacion = recArchivo.UsuarioCreacion.GetValueOrDefault();
            archivo.CuentaActivaId = recArchivo.CuentaActivaId.GetValueOrDefault();
            archivo.CuentaActivaFecha = recArchivo.CuentaActivaFecha.GetValueOrDefault();

            return archivo;
        }

        public static DTO.Log Log(Resources.Log recLog)
        {
            DTO.Log log = new DTO.Log();

            log.Id = recLog.LogId;
            log.Descripcion = recLog.LogDescripcion;
            log.EstadoId = recLog.LogEstadoId.GetValueOrDefault();
            log.Fecha = recLog.LogFecha.GetValueOrDefault();
            log.CuentaActivaId = recLog.CuentaActivaId.GetValueOrDefault();
            log.CuentaActivaFecha = recLog.CuentaActivaFecha.GetValueOrDefault();

            return log;
        }

        public static DTO.Empresa Empresa(Resources.Empresa recEmpresa)
        {
            DTO.Empresa empresa = new DTO.Empresa();

            empresa.empresaId = recEmpresa.EmpresaId;
            empresa.rut = recEmpresa.EmpresaRut;
            empresa.razonSocial = recEmpresa.EmpresaRazonSocial;
            empresa.giro = recEmpresa.EmpresaGiro;
            empresa.logo = recEmpresa.EmpresaLogo;

            return empresa;
        }

        public static DTO.Usuario Usuario(Resources.Usuario recUsuario)
        {
            DTO.Usuario usuario = new DTO.Usuario();

            usuario.usuarioId = recUsuario.UsuarioId;
            usuario.rut = recUsuario.UsuarioRut;
            usuario.nombre = recUsuario.UsuarioNombre;
            usuario.email = recUsuario.UsuarioEmail;
            usuario.perfilId = recUsuario.PerfilId.GetValueOrDefault();
            usuario.empresa = DTOBuilder.Empresa(recUsuario.Empresa);
            usuario.perfil = DTOBuilder.Perfil(recUsuario.Perfil);
            usuario.pwdEstado = (recUsuario.pwdEstado == 1);

            return usuario;
        }

        public static DTO.Rubro Rubro(Resources.Rubro recRubro)
        {
            var rubro = new DTO.Rubro();

            rubro.rubroId = recRubro.RubroId;
            rubro.rubroNombre = recRubro.RubroNombre;
            rubro.rubroDescripcion = recRubro.RubroDescripcion;

            return rubro;
        }

        public static DTO.Grupo Grupo(Resources.Grupo recGrupo)
        {
            var grupo = new DTO.Grupo();

            grupo.grupoId = recGrupo.GrupoId;
            grupo.grupoNombre = recGrupo.GrupoNombre;
            grupo.grupoDescripcion = recGrupo.GrupoDescripcion;

            return grupo;
        }

        public static DTO.Cuenta Cuenta(Resources.Cuenta recCuenta)
        {
            var cuenta = new DTO.Cuenta();

            cuenta.id = recCuenta.CuentaId;
            cuenta.numero = recCuenta.CuentaNumero;
            cuenta.nombre = recCuenta.CuentaNombre;
            cuenta.descripcion = recCuenta.CuentaDescripcion;

            cuenta.empresa = DTOBuilder.Empresa(recCuenta.Empresa);
            cuenta.rubro = DTOBuilder.Rubro(recCuenta.Rubro);
            cuenta.grupo = DTOBuilder.Grupo(recCuenta.Grupo);

            return cuenta;
        }

        public static DTO.CuentaActiva CuentaActiva(Resources.CuentaActiva recCuentaActiva)
        {
            var cuentaActiva = new DTO.CuentaActiva();

            cuentaActiva.id = recCuentaActiva.CuentaActivaId;
            cuentaActiva.numero = recCuentaActiva.Cuenta.CuentaNumero;
            cuentaActiva.nombre = recCuentaActiva.Cuenta.CuentaNombre;
            cuentaActiva.descripcion = recCuentaActiva.Cuenta.CuentaDescripcion;

            cuentaActiva.cuentaActivaId = recCuentaActiva.CuentaActivaId;
            cuentaActiva.cuentaActivaFecha = recCuentaActiva.CuentaActivaFecha.Date;
            cuentaActiva.cuentaActivaDiasPlazo = recCuentaActiva.CuentaActivaDiasPlazo.GetValueOrDefault();
            cuentaActiva.cuentaActivaTotal = recCuentaActiva.CuentaActivaTotal.GetValueOrDefault();
            cuentaActiva.cuentaActivaEstado = recCuentaActiva.CuentaActivaEstado.GetValueOrDefault();

            cuentaActiva.analista = DTOBuilder.Usuario(recCuentaActiva.Usuario);
            cuentaActiva.validador = DTOBuilder.Usuario(recCuentaActiva.Usuario2);
            cuentaActiva.certificador = DTOBuilder.Usuario(recCuentaActiva.Usuario1);
            cuentaActiva.empresa = DTOBuilder.Empresa(recCuentaActiva.Cuenta.Empresa);

            cuentaActiva.rubro = DTOBuilder.Rubro(recCuentaActiva.Cuenta.Rubro);
            cuentaActiva.grupo = DTOBuilder.Grupo(recCuentaActiva.Cuenta.Grupo);

            return cuentaActiva;
        }

        public static DTO.Perfil Perfil(Resources.Perfil recPerfil) {
            var perfil = new DTO.Perfil();

            perfil.Id = recPerfil.PerfilId;
            perfil.Nombre = recPerfil.PerfilNombre;

            return perfil;
        }

    }
}