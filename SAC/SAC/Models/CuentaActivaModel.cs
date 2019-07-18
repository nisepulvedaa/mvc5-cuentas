using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAC.Models.Resources;
using System.Linq.Expressions;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace SAC.Models
{
    public class CuentaActivaModel
    {

        private readonly Models.Resources.SACModel db = new Models.Resources.SACModel();

        public List<Int32> obtenerAñosDistintos()
        {
            List<Int32> lista = new List<Int32>();
            try
            {
                var años = db.ObtenerAñosDistintos();

                foreach (var año in años)
                {
                    lista.Add(año.GetValueOrDefault());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.CuentaActivaModel (obtenerAño): " + ex.Message);
                lista = new List<Int32>();
            }
            return lista;
        }

        public List<DTO.CuentaActiva> obtenerCuentasActivas()
        {
            List<DTO.CuentaActiva> lista = new List<DTO.CuentaActiva>();
            try
            {
                var cuentasActivas = db.CuentaActiva.ToList();
                foreach (var cuenta in cuentasActivas)
                {
                    lista.Add(DTOBuilder.CuentaActiva(cuenta));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.CuentaActivaModel (obtenerCuentasActivas): " + ex.Message);
                lista = new List<DTO.CuentaActiva>();
            }
            return lista;
        }



        public DTO.CuentaActiva obtenerCuentaActivaPorId(int cuentaActivaId, int mes, int año)
        {
            try
            {
                Resources.CuentaActiva cuenta = db.CuentaActiva.Where(ca =>
                    ca.CuentaActivaId == cuentaActivaId &&
                    DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Year == año &&
                    DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Month == mes
                ).First();

                return DTOBuilder.CuentaActiva(cuenta);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.CuentaActivaModel (obtenerCuentaActivaPorId): " + ex.Message);
                return new DTO.CuentaActiva();
            }
        }





        public List<DTO.CuentaActiva> obtenerCuentasActivasPorFecha(int año, int mes)
        {
            List<DTO.CuentaActiva> lista = new List<DTO.CuentaActiva>();
            try
            {
                var cuentasActivas = db.CuentaActiva.Where(ca => DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Year == año && DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Month == mes);

                foreach (var cuenta in cuentasActivas)
                {

                    //var c = new DTO.CuentaActiva();
                    //c.cuentaActivaId = cuenta.CuentaActivaId;
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

                    lista.Add(DTOBuilder.CuentaActiva(cuenta));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (obtenerCuentasActivasPorFecha): " + ex.Message);
                lista = new List<DTO.CuentaActiva>();
            }
            return lista;
        }

        public List<DTO.Cuenta> obtenerCuentasNoActivas(int año, int mes, int empresaId, int grupoId, int rubroId)
        {
            List<DTO.Cuenta> lista = new List<DTO.Cuenta>();
            try
            {
                var donde = new WhereBuilder<Cuenta>();

                if (empresaId != -1)
                {
                    donde.SetAnd(c => c.Empresa.EmpresaId == empresaId);
                }
                if (rubroId != -1)
                {
                    donde.SetAnd(c => c.Rubro.RubroId == rubroId);
                }
                if (grupoId != -1)
                {
                    donde.SetAnd(c => c.Grupo.GrupoId == grupoId);
                }

                var cas = from ca in db.CuentaActiva
                          where
                          DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Year == año &&
                          DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Month == mes
                          select ca.CuentaActivaId;
                var cuentas = db.Cuenta.Where(c => !cas.Contains(c.CuentaId));
                cuentas = cuentas.Where(donde.GetWhere());

                //foreach (var i in cas)
                //{
                //    System.Diagnostics.Debug.WriteLine(">>>>>>" + i);
                //}

                foreach (var cuenta in cuentas)
                {
                    //var c = new DTO.Cuenta();
                    //c.id = cuenta.CuentaId;
                    //c.numero = cuenta.CuentaNumero;
                    //c.nombre = cuenta.CuentaNombre;
                    //c.empresa = new DTO.Empresa();
                    //c.empresa.razonSocial = cuenta.Empresa.EmpresaRazonSocial;
                    //c.empresa.empresaId = cuenta.Empresa.EmpresaId;
                    //c.rubro = new DTO.Rubro();
                    //c.rubro.rubroNombre = cuenta.Rubro.RubroNombre;
                    //c.rubro.rubroId = cuenta.Rubro.RubroId;
                    //c.grupo = new DTO.Grupo();
                    //c.grupo.grupoNombre = cuenta.Grupo.GrupoNombre;
                    //c.grupo.grupoId = cuenta.Grupo.GrupoId;
                    //c.descripcion = cuenta.CuentaDescripcion;
                    //lista.Add(c);
                    lista.Add(DTOBuilder.Cuenta(cuenta));
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (obtenerCuentasNoActivas): " + ex.Message);
                lista = new List<DTO.Cuenta>();
            }
            return lista;
        }

        public bool AsignarCuentaActiva(int cuentaId, int analista, int validador, int certificador, int diasPlazo, int total, DateTime fecha)
        {
            try
            {
                db.AsignarCuentaActiva(cuentaId, fecha, total, analista, validador, certificador, diasPlazo);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.CuentaModel (AsignarCuentaActiva): " + ex.Message);
                return false;
            }

        }

        public bool IngresarCuentaActiva(int cuentaId, int analista, int validador, int certificador, int diasPlazo, int usuarioId, int total, DateTime fecha)
        {
            try
            {
                db.IngresarCuentaActiva(cuentaId, fecha, total, analista, validador, certificador, diasPlazo, usuarioId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.CuentaModel (IngresarCuentaActiva): " + ex.Message);
                System.Diagnostics.Debug.WriteLine("SAC.Models.CuentaModel (IngresarCuentaActiva): " + ex.InnerException);
                return false;
            }

        }

        public bool EliminarCuentaActiva(int cuentaActivaId, DateTime fecha)
        {
            try
            {
                db.EliminarCuentaActiva(cuentaActivaId, fecha);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.CuentaModel (EliminarCuentaActiva): " + ex.Message);
                System.Diagnostics.Debug.WriteLine("SAC.Models.CuentaModel (EliminarCuentaActiva): " + ex.InnerException);
                return false;
            }

        }

        public List<DTO.CuentaActiva> ObtenerCuentasActivasAsociadasPorCuenta(int cuentaId)
        {
            List<DTO.CuentaActiva> cuentasActivas;

            try
            {
                cuentasActivas = new List<DTO.CuentaActiva>();
                var cuentasActivasColeccion = db.CuentaActiva.Where(ca => ca.Cuenta.CuentaId == cuentaId);
                foreach (var cuentaActivaR in cuentasActivasColeccion)
                {
                    cuentasActivas.Add(DTOBuilder.CuentaActiva(cuentaActivaR));
                }
            }
            catch (Exception e)
            {

                cuentasActivas = new List<DTO.CuentaActiva>();
            }
            return cuentasActivas;
        }

        public int ObtenerNumeroDeArchivosPorCuentaActiva(int cuentaActivaId, int year, int month)
        {
            try
            {
                Resources.CuentaActiva cuenta = db.CuentaActiva.Where(ca =>
                    ca.CuentaActivaId == cuentaActivaId &&
                    ca.CuentaActivaFecha.Year == year &&
                    ca.CuentaActivaFecha.Month == month
                ).FirstOrDefault();
                int count = cuenta.Archivo.Count;
                return count;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public DTO.CuentaActiva IngresarActualizarAsignacionCuenta(DTO.CuentaActiva cuenta, int usuarioId)
        {
            bool valid = true;
            Resources.Empresa empresa = db.Empresa.FirstOrDefault(e => e.EmpresaRazonSocial == cuenta.empresa.razonSocial);
            if (empresa != null)
            {
                cuenta.empresa.empresaId = empresa.EmpresaId;

                var cuentas = db.Cuenta.FirstOrDefault(c => c.CuentaNumero == cuenta.numero && c.Empresa.EmpresaId == empresa.EmpresaId);
                if (cuentas != null)
                {
                    cuenta.id = cuentas.CuentaId;
                }
                else
                {
                    valid = false;
                }
            }
            else
            {
                valid = false;
            }
            Resources.Usuario analista = db.Usuario.FirstOrDefault(u => u.UsuarioNombre == cuenta.analista.nombre);
            if (analista != null)
            {
                cuenta.analista.usuarioId = analista.UsuarioId;
            }
            else
            {
                valid = false;
            }
            Resources.Usuario validador = db.Usuario.FirstOrDefault(u => u.UsuarioNombre == cuenta.validador.nombre);
            if (validador != null)
            {
                cuenta.validador.usuarioId = validador.UsuarioId;
            }
            else
            {
                valid = false;
            }
            Resources.Usuario certificador = db.Usuario.FirstOrDefault(u => u.UsuarioNombre == cuenta.certificador.nombre);
            if (certificador != null)
            {
                cuenta.certificador.usuarioId = certificador.UsuarioId;
            }
            else
            {
                valid = false;
            }

            if (valid)
            {
                Resources.CuentaActiva cuentaDB = db.CuentaActiva.FirstOrDefault(c =>
                    c.CuentaActivaId == cuenta.id
                    && c.Cuenta.Empresa.EmpresaRazonSocial == c.Cuenta.Empresa.EmpresaRazonSocial
                    && DbFunctions.TruncateTime(c.CuentaActivaFecha).Value.Year == cuenta.cuentaActivaFecha.Year
                    && DbFunctions.TruncateTime(c.CuentaActivaFecha).Value.Month == cuenta.cuentaActivaFecha.Month
                );
                if (cuentaDB == null)
                {
                    DateTime fecha = new DateTime(cuenta.cuentaActivaFecha.Year, cuenta.cuentaActivaFecha.Month, 1);
                    cuenta.cuentaActivaId = (int)db.IngresarCuentaActiva(cuenta.id, fecha, cuenta.cuentaActivaTotal, cuenta.analista.usuarioId, cuenta.validador.usuarioId, cuenta.certificador.usuarioId, cuenta.cuentaActivaDiasPlazo, usuarioId);
                    cuenta.insertUpdate = true;
                }
                else
                {
                    DateTime fecha = new DateTime(cuenta.cuentaActivaFecha.Year, cuenta.cuentaActivaFecha.Month, 1);
                    db.AsignarCuentaActiva(cuenta.id, fecha, cuenta.cuentaActivaTotal, cuenta.analista.usuarioId, cuenta.validador.usuarioId, cuenta.certificador.usuarioId, cuenta.cuentaActivaDiasPlazo);
                    cuenta.cuentaActivaId = cuentaDB.Cuenta.CuentaId;
                    cuenta.insertUpdate = false;
                }
            }
            return cuenta;
        }

    }



}





