using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class MantenedorModel
    {
        private readonly Models.Resources.SACModel db = new Models.Resources.SACModel();

        public bool IngresarEmpresas(string rut, string razonSocial, string giro, string logo, int usuarioId)
        {
            try
            {
                db.IngresarEmpresa(rut, razonSocial, giro, logo, usuarioId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (IngresarEmpresas): " + ex.Message);
                return false;
            }
        }

        public bool ExisteEmpresa(string razonSocial, string rut = "")
        {
            try
            {
                if (razonSocial != "")
                {
                    var empresa = db.Empresa.Where(e => e.EmpresaRazonSocial == razonSocial);
                    if (empresa.Count() > 0)
                    {
                        return true;
                    }
                }
                if (rut != "")
                {
                    var empresa = db.Empresa.Where(e => e.EmpresaRut == rut);
                    if (empresa.Count() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (EditarEmpresas): " + ex.Message);
                return false;
            }
        }
        public bool EditarEmpresas(int empresaId, string rut, string razonSocial, string giro, string logo, int usuarioId)
        {
            try
            {
                db.EditarEmpresa(empresaId, rut, razonSocial, giro, logo, usuarioId);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (EditarEmpresas): " + ex.Message);
                return false;
            }
        }

        public bool EliminarEmpresas(int empresaId)
        {
            try
            {
                db.EliminarEmpresa(empresaId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (EliminarEmpresas): " + ex.Message);
                return false;
            }
        }

        public bool EmpresaEsEliminable(int empresaId)
        {

            try
            {

                Resources.Empresa empresa = db.Empresa.Where(e => e.EmpresaId == empresaId).FirstOrDefault();
                if (empresa.Cuenta.Count() > 0 && empresa.Usuario.Count() > 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (EmpresaEsEliminable): " + ex.Message);
                return false;
            }
        }



        public List<DTO.Empresa> obtenerEmpresas()
        {
            List<DTO.Empresa> lista = new List<DTO.Empresa>();
            try
            {

                var empresas = db.Empresa.ToList();
                foreach (var empresa in empresas)
                {
                    //var e = new DTO.Empresa();
                    //e.empresaId = empresa.EmpresaId;
                    //e.rut = empresa.EmpresaRut;
                    //e.razonSocial = empresa.EmpresaRazonSocial;
                    //e.giro = empresa.EmpresaGiro;
                    //e.logo = empresa.EmpresaLogo;

                    //lista.Add(e);
                    lista.Add(DTOBuilder.Empresa(empresa));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (IngresarEmpresas): " + ex.Message);
                lista = new List<DTO.Empresa>();
            }
            return lista;
        }


        public DTO.Empresa obtenerEmpresaPorId(int id)
        {

            DTO.Empresa respuesta = new DTO.Empresa();

            try
            {
                Resources.Empresa empresa = db.Empresa.FirstOrDefault(e => e.EmpresaId == id);
                return DTOBuilder.Empresa(empresa);
                //respuesta.empresaId = empresa.EmpresaId;
                //respuesta.rut = empresa.EmpresaRut;
                //respuesta.razonSocial = empresa.EmpresaRazonSocial;
                //respuesta.giro = empresa.EmpresaGiro;
                //respuesta.logo = empresa.EmpresaLogo;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (obtenerEmpresaPorId): " + ex.Message);
                respuesta = new DTO.Empresa();
            }
            return respuesta;
        }





        public DTO.Usuario ObtenerUsuarioPorId(int id)
        {
            //DTO.Usuario respuesta = new DTO.Usuario();

            //Resources.Usuario usuario = db.Usuario.FirstOrDefault(u => u.UsuarioId == id);
            //respuesta.nombre = usuario.UsuarioNombre;
            //respuesta.email = usuario.UsuarioEmail;
            //respuesta.perfil = new DTO.Perfil();
            //respuesta.perfil.Id = usuario.Perfil.PerfilId;
            //respuesta.perfil.Nombre = usuario.Perfil.PerfilNombre;
            //respuesta.empresa = new DTO.Empresa();

            //respuesta.empresa.empresaId = usuario.Empresa.EmpresaId;
            //respuesta.empresa.razonSocial = usuario.Empresa.EmpresaRazonSocial;



            //respuesta.pwdEstado = (int)usuario.pwdEstado == 1;

            //return respuesta;
            Resources.Usuario usuario = db.Usuario.FirstOrDefault(u => u.UsuarioId == id);
            return DTOBuilder.Usuario(usuario);
        }

        public DTO.Usuario ObtenerUsuarioPorEmail(string usuarioEmail)
        {
            //DTO.Usuario respuesta = new DTO.Usuario();

            //Resources.Usuario usuario = db.Usuario.FirstOrDefault(u => u.UsuarioId == id);
            //respuesta.nombre = usuario.UsuarioNombre;
            //respuesta.email = usuario.UsuarioEmail;
            //respuesta.perfil = new DTO.Perfil();
            //respuesta.perfil.Id = usuario.Perfil.PerfilId;
            //respuesta.perfil.Nombre = usuario.Perfil.PerfilNombre;
            //respuesta.empresa = new DTO.Empresa();

            //respuesta.empresa.empresaId = usuario.Empresa.EmpresaId;
            //respuesta.empresa.razonSocial = usuario.Empresa.EmpresaRazonSocial;



            //respuesta.pwdEstado = (int)usuario.pwdEstado == 1;

            //return respuesta;
            Resources.Usuario usuario = db.Usuario.FirstOrDefault(u => u.UsuarioEmail == usuarioEmail);
            return DTOBuilder.Usuario(usuario);
        }

        public bool ExisteEmailUsuario(string usuarioEmail)
        {
            try
            {
                if (usuarioEmail != "")
                {
                    var usuario = db.Usuario.Where(u => u.UsuarioEmail == usuarioEmail);
                    if (usuario.Count() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (ExisteEmailUsuario): " + ex.Message);
                return false;
            }
        }


        public bool IngresarRubros(string rubroNombre, string rubroDescripcion, int usuarioId)
        {
            try
            {

                db.IngresarRubros(rubroNombre, rubroDescripcion, usuarioId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (IngresarRubros): " + ex.Message);
                return false;
            }
        }


        public List<DTO.Rubro> obtenerRubros()
        {
            List<DTO.Rubro> lista = new List<DTO.Rubro>();
            try
            {

                var rubros = db.Rubro.ToList();
                foreach (var rubro in rubros)
                {
                    //var r = new DTO.Rubro();
                    //r.rubroId = rubro.RubroId;
                    //r.rubroNombre = rubro.RubroNombre;
                    //r.rubroDescripcion = rubro.RubroDescripcion;
                    //lista.Add(r);
                    lista.Add(DTOBuilder.Rubro(rubro));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (obtenerRubros): " + ex.Message);
                lista = new List<DTO.Rubro>();
            }
            return lista;
        }



        public DTO.Rubro ObtenerRubroPorId(int id)
        {
            DTO.Rubro respuesta = new DTO.Rubro();

            Resources.Rubro rubro = db.Rubro.FirstOrDefault(r => r.RubroId == id);
            return DTOBuilder.Rubro(rubro);
            //respuesta.rubroId = (int)rubro.RubroId;
            //respuesta.rubroNombre = rubro.RubroNombre;
            //respuesta.rubroDescripcion = rubro.RubroDescripcion;
            //return respuesta;
        }

        public bool EditarRubros(int rubroId, string rubroNombre, string rubroDescripcion, int usuarioId)
        {
            try
            {
                db.EditarRubro(rubroId, rubroNombre, rubroDescripcion, usuarioId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (EditarRubros): " + ex.Message);
                return false;
            }
        }

        public bool RubroEsEliminable(int rubroId)
        {

            try
            {

                Resources.Rubro rubro = db.Rubro.Where(r => r.RubroId == rubroId).FirstOrDefault();
                if (rubro.Cuenta.Count() > 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (RubroEsEliminable): " + ex.Message);
                return false;
            }
        }


        public bool EliminarRubro(int rubroId)
        {
            try
            {
                db.EliminarRubro(rubroId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (EliminarRubro): " + ex.Message);
                return false;
            }
        }

        public bool ExisteRubro(string rubroNombre)
        {
            try
            {
                if (rubroNombre != "")
                {
                    var rubro = db.Rubro.Where(r => r.RubroNombre == rubroNombre);
                    if (rubro.Count() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (ExisteRubro): " + ex.Message);
                return false;
            }
        }
        public bool ExisteGrupo(string grupoNombre)
        {
            try
            {
                if (grupoNombre != "")
                {
                    var grupo = db.Grupo.Where(g => g.GrupoNombre == grupoNombre);
                    if (grupo.Count() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (ExisteGrupo): " + ex.Message);
                return false;
            }
        }




        public bool IngresarGrupos(string grupoNombre, string grupoDescripcion, int usuarioId)
        {
            try
            {
                db.IngresarGrupos(grupoNombre, grupoDescripcion, usuarioId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (IngresarGrupos): " + ex.Message);
                return false;
            }
        }

        public List<DTO.Grupo> obtenerGrupos()
        {
            List<DTO.Grupo> lista = new List<DTO.Grupo>();
            try
            {

                var grupos = db.Grupo.ToList();
                foreach (var grupo in grupos)
                {
                    //var g = new DTO.Grupo();
                    //g.grupoId = grupo.GrupoId;
                    //g.grupoNombre = grupo.GrupoNombre;
                    //g.grupoDescripcion = grupo.GrupoDescripcion;
                    //lista.Add(g);
                    lista.Add(DTOBuilder.Grupo(grupo));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (obtenerGrupos): " + ex.Message);
                lista = new List<DTO.Grupo>();
            }
            return lista;
        }

        public DTO.Grupo ObtenerGrupoPorId(int id)
        {
            //DTO.Grupo respuesta = new DTO.Grupo();
            
            Resources.Grupo grupo = db.Grupo.FirstOrDefault(g => g.GrupoId == id);
            return DTOBuilder.Grupo(grupo);
        //    respuesta.grupoId = (int)grupo.GrupoId;
        //    respuesta.grupoNombre = grupo.GrupoNombre;
        //    respuesta.grupoDescripcion = grupo.GrupoDescripcion;
        //    return respuesta;
        }

        public bool EditarGrupos(int grupoId, string grupoNombre, string grupoDescripcion, int usuarioId)
        {
            try
            {
                db.EditarGrupo(grupoId, grupoNombre, grupoDescripcion, usuarioId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (EditarGrupos): " + ex.Message);
                return false;
            }
        }

        public bool GrupoEsEliminable(int grupoId)
        {

            try
            {
                Resources.Grupo grupo = db.Grupo.Where(g => g.GrupoId == grupoId).FirstOrDefault();
                if (grupo.Cuenta.Count > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (GrupoEsEliminable): " + ex.Message);
                return false;
            }
        }


        public bool EliminarGrupo(int grupoId)
        {
            try
            {
                db.EliminarGrupo(grupoId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (EliminarGrupo): " + ex.Message);
                return false;
            }
        }


        public bool ExisteCuenta(string cuentaNumero, string CuentaNombre, int cuentaEmpresa)
        {
            try
            {
                if (cuentaNumero != "")
                {
                    var cuenta = db.Cuenta.Where(c => c.CuentaNumero == cuentaNumero && c.CuentaEmpresa == cuentaEmpresa);
                    if (cuenta.Count() > 0)
                    {
                        return true;
                    }
                }
                else
                {

                    var cuenta = db.Cuenta.Where(c => c.CuentaNombre == CuentaNombre && c.CuentaEmpresa == cuentaEmpresa);
                    if (cuenta.Count() > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (ExisteCuenta): " + ex.Message);
                return false;
            }
        }

        public bool IngresarCuenta(string cuentaNumero, string CuentaNombre, int cuentaEmpresa, int cuentaRubro, int cuentaGrupo, string cuentaDescripcion, int usuarioId)
        {
            try
            {
                db.IngresarCuenta(cuentaNumero, CuentaNombre, cuentaEmpresa, cuentaRubro, cuentaGrupo, cuentaDescripcion, usuarioId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (IngresarCuenta): " + ex.Message);
                return false;
            }
        }

        public List<DTO.Cuenta> obtenerCuentas()
        {
            List<DTO.Cuenta> lista = new List<DTO.Cuenta>();
            try
            {
                var cuentas = db.Cuenta.ToList();
                foreach (var cuenta in cuentas)
                {
                    //var c = new DTO.Cuenta();
                    //c.id = cuenta.CuentaId;
                    //c.numero = cuenta.CuentaNumero;
                    //c.nombre = cuenta.CuentaNombre;
                    //c.empresa = new DTO.Empresa();
                    //c.empresa.razonSocial = cuenta.Empresa.EmpresaRazonSocial;
                    //c.rubro = new DTO.Rubro();
                    //c.rubro.rubroNombre = cuenta.Rubro.RubroNombre;
                    //c.grupo = new DTO.Grupo();
                    //c.grupo.grupoNombre = cuenta.Grupo.GrupoNombre;
                    //c.descripcion = cuenta.CuentaDescripcion;
                    //lista.Add(c);
                    lista.Add(DTOBuilder.Cuenta(cuenta));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (obtenerCuentas): " + ex.Message);
                lista = new List<DTO.Cuenta>();
            }
            return lista;
        }


        public List<DTO.Usuario> obtenerUsuarios()
        {
            List<DTO.Usuario> lista = new List<DTO.Usuario>();
            try
            {
                var usuarios = db.Usuario.ToList();
                foreach (var usuario in usuarios)
                {
                    //var u = new DTO.Usuario();
                    //u.usuarioId = usuario.UsuarioId;
                    //u.rut = usuario.UsuarioRut;
                    //u.nombre = usuario.UsuarioNombre;
                    //u.email = usuario.UsuarioEmail;
                    //u.empresa = new DTO.Empresa();
                    //u.empresa.empresaId = usuario.Empresa.EmpresaId;
                    //u.empresa.razonSocial = usuario.Empresa.EmpresaRazonSocial;
                    ////u.empresaId = usuario.EmpresaId.GetValueOrDefault();
                    //u.perfil = new DTO.Perfil();
                    //u.perfil.Id = usuario.Perfil.PerfilId;
                    //u.perfil.Nombre = usuario.Perfil.PerfilNombre;
                    ////u.perfilId = usuario.PerfilId.GetValueOrDefault();
                    //u.pwdEstado = (usuario.pwdEstado == 1);

                    //lista.Add(u);
                    lista.Add(DTOBuilder.Usuario(usuario));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (obtenerCuentas): " + ex.Message);
                lista = new List<DTO.Usuario>();
            }
            return lista;
        }



        public DTO.Cuenta ObtenerCuentaPorId(int id)
        {
            DTO.Cuenta respuesta = new DTO.Cuenta();

            Resources.Cuenta cuenta = db.Cuenta.FirstOrDefault(c => c.CuentaId == id);
            return DTOBuilder.Cuenta(cuenta);
            //respuesta.id = (int)cuenta.CuentaId;
            //respuesta.numero = cuenta.CuentaNumero;
            //respuesta.nombre = cuenta.CuentaNombre;
            //respuesta.empresa = new DTO.Empresa();
            //respuesta.empresa.empresaId = cuenta.Empresa.EmpresaId;
            //respuesta.empresa.razonSocial = cuenta.Empresa.EmpresaRazonSocial;
            //respuesta.rubro = new DTO.Rubro();
            //respuesta.rubro.rubroId = cuenta.Rubro.RubroId;
            //respuesta.rubro.rubroNombre = cuenta.Rubro.RubroNombre;
            //respuesta.grupo = new DTO.Grupo();
            //respuesta.grupo.grupoId = cuenta.Grupo.GrupoId;
            //respuesta.grupo.grupoNombre = cuenta.Grupo.GrupoNombre;
            //respuesta.descripcion = cuenta.CuentaDescripcion;
            //return respuesta;
        }

        public bool CuentaEsEliminable(int cuentaId)
        {
            try
            {
                Resources.Cuenta cuenta = db.Cuenta.Where(c => c.CuentaId == cuentaId).FirstOrDefault();
                if (cuenta.CuentaActiva.Count > 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (CuentaEsEliminable): " + ex.Message);
                return false;
            }
        }

        public List<DTO.Cuenta> ObtenerCuentasPorEmpresa(int empresaId)
        {
            List<DTO.Cuenta> respuesta = new List<DTO.Cuenta>();
            var cuentas = db.Cuenta.Where(c => c.Empresa.EmpresaId == empresaId);

            foreach (Resources.Cuenta cuenta in cuentas)
            {
                //DTO.Cuenta c = new DTO.Cuenta();
                //c.id = (int)cuenta.CuentaId;
                //c.numero = cuenta.CuentaNumero;
                //c.nombre = cuenta.CuentaNombre;
                //c.empresa = new DTO.Empresa();
                //c.empresa.empresaId = cuenta.Empresa.EmpresaId;
                //c.empresa.razonSocial = cuenta.Empresa.EmpresaRazonSocial;
                //c.rubro = new DTO.Rubro();
                //c.rubro.rubroId = cuenta.Rubro.RubroId;
                //c.rubro.rubroNombre = cuenta.Rubro.RubroNombre;
                //c.grupo = new DTO.Grupo();
                //c.grupo.grupoId = cuenta.Grupo.GrupoId;
                //c.grupo.grupoNombre = cuenta.Grupo.GrupoNombre;
                //c.descripcion = cuenta.CuentaDescripcion;
                //respuesta.Add(c);
                respuesta.Add(DTOBuilder.Cuenta(cuenta));
            }
            return respuesta;

        }



        public bool EditarCuentas(int cuentaId, string cuentaNumero, string CuentaNombre, int cuentaEmpresa, int cuentaRubro, int cuentaGrupo, string cuentaDescripcion, int usuarioId)
        {
            try
            {
                db.EditarCuenta(cuentaId, cuentaNumero, CuentaNombre, cuentaEmpresa, cuentaRubro, cuentaGrupo, cuentaDescripcion, usuarioId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (EditarCuentas): " + ex.Message);
                return false;
            }
        }

        public bool EliminarCuenta(int cuentaId)
        {
            try
            {
                db.EliminarCuenta(cuentaId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (EliminarCuenta): " + ex.Message);
                return false;
            }
        }

        public bool IngresarUsuarios(string usuarioNombre, int empresaId, string usuarioEmail, int perfilId, string username, string pwd, int usuarioId, string usuarioRut)
        {
            try
            {
                db.IngresarUsuario(usuarioNombre, empresaId, usuarioEmail, perfilId, username, pwd, usuarioId, usuarioRut);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (IngresarUsuarios): " + ex.Message);
                return false;
            }
        }

        public DTO.Cuenta IngresarActualizarCuenta(DTO.Cuenta cuenta, int usuarioId)
        {
            bool valid = true;
            Resources.Empresa empresa = db.Empresa.FirstOrDefault(e => e.EmpresaRazonSocial == cuenta.empresa.razonSocial);
            if (empresa != null)
            {
                cuenta.empresa.empresaId = empresa.EmpresaId;
            }
            else
            {
                valid = false;
            }
            Resources.Rubro rubro = db.Rubro.FirstOrDefault(r => r.RubroNombre == cuenta.rubro.rubroNombre);
            if (rubro != null)
            {
                cuenta.rubro.rubroId = rubro.RubroId;
            }
            else
            {
                valid = false;
            }
            Resources.Grupo grupo = db.Grupo.FirstOrDefault(g => g.GrupoNombre == cuenta.grupo.grupoNombre);
            if (grupo != null)
            {
                cuenta.grupo.grupoId = grupo.GrupoId;
            }
            else
            {
                valid = false;
            }




            if (valid)
            {
                Resources.Cuenta cuentaDB = db.Cuenta.FirstOrDefault(c => c.CuentaNumero == cuenta.numero && c.Empresa.EmpresaRazonSocial == cuenta.empresa.razonSocial);
                if (cuentaDB == null)
                {
                    cuenta.id = (int)db.IngresarCuenta(cuenta.numero, cuenta.nombre, cuenta.empresa.empresaId, cuenta.rubro.rubroId, cuenta.grupo.grupoId, cuenta.descripcion, usuarioId).FirstOrDefault();
                    cuenta.insertUpdate = true;
                }
                else
                {
                    db.EditarCuenta(cuentaDB.CuentaId, cuenta.numero, cuenta.nombre, empresa.EmpresaId, rubro.RubroId, grupo.GrupoId, cuenta.descripcion, usuarioId);
                    cuenta.id = cuentaDB.CuentaId;
                    cuenta.insertUpdate = false;
                }
            }


            /*
            Resources.Cuenta cuentaDB = db.Cuenta.FirstOrDefault(c => c.CuentaNumero == cuenta.numero && c.Empresa.EmpresaRazonSocial == cuenta.empresa.razonSocial);
            if (cuentaDB == null) {
                return cuenta;
            }
            */
            return cuenta;
        }

        public List<DTO.Perfil> obtenerPerfiles()
        {
            List<DTO.Perfil> lista = new List<DTO.Perfil>();
            try
            {
                var perfiles = db.Perfil.ToList();
                foreach (var item in perfiles)
                {
                    //var p = new DTO.Perfil();
                    //p.Id = item.PerfilId;
                    //p.Nombre = item.PerfilNombre;
                    //lista.Add(p);
                    lista.Add(DTOBuilder.Perfil(item));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (obtenerPerfiles): " + ex.Message);
                lista = new List<DTO.Perfil>();
            }
            return lista;
        }



        public bool EditarUsuario(int usuarioId, int empresaUsuario, string usuarioNombre, string usuarioEmail, int perfilUsuario, int usuario, string usuarioRut)
        {
            try
            {
                db.EditarUsuario(usuarioId, empresaUsuario, usuarioNombre, usuarioEmail, perfilUsuario, usuario, usuarioRut);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (EditarUsuario): " + ex.Message);
                return false;
            }
        }



        public bool UsuarioEsEliminable(int usuarioId)
        {
            try
            {
                Resources.Usuario usuario = db.Usuario.Where(u => u.UsuarioId == usuarioId).FirstOrDefault();
                if (usuario.CuentaActiva.Count > 0 || usuario.CuentaActiva1.Count > 0 || usuario.CuentaActiva2.Count > 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (UsuarioEsEliminable): " + ex.Message);
                return false;
            }
        }

        public bool EliminarUsuario(int usuarioId)
        {
            try
            {
                db.EliminarUsuario(usuarioId);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (EliminarUsuario): " + ex.Message);
                return false;
            }
        }



        public bool EditarClave(int usuarioId, string pwd, int usuario)
        {
            try
            {
                db.EditarClaveUsuario(usuarioId, pwd, usuario);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.MantenedorModel (EditarUsuario): " + ex.Message);
                return false;
            }
        }

    }
}