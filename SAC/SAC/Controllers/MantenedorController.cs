using SAC.Helpers;
using SAC.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace SAC.Controllers
{
    public class MantenedorController : Controller
    {


        // GET: Mantenedor
        public ActionResult Empresas()
        {
            if (SessionHandler.Logged)
            {
                var modelMantenedor = new Models.MantenedorModel();
                ViewBag.PageTitle = "Mantenedor";
                ViewBag.UsuarioNombre = SessionHandler.Usuario;
                ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                var lista = modelMantenedor.obtenerEmpresas();
                string html = "";
                foreach (var empresa in lista)
                {
                    html += "<tr>";
                    html += "<td>" + empresa.rut + "</td>";
                    html += "<td>" + empresa.razonSocial + "</td>";
                    html += "<td>" + empresa.giro + "</td>";
                    html += "<td><img src='" + empresa.logo + "' class='logoimg'></td>";
                    html += "<td><button class='btn btn-warning' onclick='obtenerEmpresa(" + empresa.empresaId + ")' >Editar</button>";
                    html += "<button class='btn btn-success' onclick='eliminarEmpresa(" + empresa.empresaId + ", \"" + empresa.razonSocial + "\")' >Eliminar</button></td>";
                    html += "</tr>";

                }

                ViewBag.Table = html;

                return View();
            }
            else
            {
                return Redirect("~/Login/Index");
            }

        }

        [HttpPost]
        public ActionResult ingresarEmpresa(string rut, string razonSocial, string giro, string logo)
        {
            var modelMantenedor = new Models.MantenedorModel();
            if (modelMantenedor.ExisteEmpresa(razonSocial))
            {
                return Json(new { response = "error", message = "la Empresa " + razonSocial + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }
            if (modelMantenedor.ExisteEmpresa("", rut))
            {
                return Json(new { response = "error", message = "la Empresa con RUT " + rut + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }

            if (modelMantenedor.IngresarEmpresas(rut, razonSocial, giro, logo, SessionHandler.UsuarioId))
            {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar la empresa..." }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult obtenerEmpresa(int id)
        {
            var modelMantenedor = new Models.MantenedorModel();

            Models.DTO.Empresa empresa = modelMantenedor.obtenerEmpresaPorId(id);

            return Json(new { empresaId = empresa.empresaId, empresaRut = empresa.rut, empresaRazonSocial = empresa.razonSocial, empresaGiro = empresa.giro, empresaLogo = empresa.logo }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult editarEmpresa(int empresaId, string rut, string razonSocial, string giro, string logo)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.EditarEmpresas(empresaId, rut, razonSocial, giro, logo, SessionHandler.UsuarioId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar la empresa..." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult eliminarEmpresa(int empresaId)
        {
            var modelMantenedor = new Models.MantenedorModel();

            if (modelMantenedor.EmpresaEsEliminable(empresaId))
            {
                if (modelMantenedor.EliminarEmpresas(empresaId))
                {

                    return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar la empresa..." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { response = "error", message = "No se puede Borrar la Empresa porque tiene datos asociados..." }, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult Rubros()
        {
            if (SessionHandler.Logged)
            {
                var modelMantenedor = new Models.MantenedorModel();
                ViewBag.PageTitle = "Mantenedor";
                ViewBag.UsuarioNombre = SessionHandler.Usuario;
                ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                var lista = modelMantenedor.obtenerRubros();
                string html = "";
                foreach (var rubro in lista)
                {
                    html += "<tr>";
                    html += "<td>" + rubro.rubroNombre + "</td>";
                    html += "<td><div style='display: inline-block;white-space: normal;'>" + rubro.rubroDescripcion + "</div></td>";
                    html += "<td><button class='btn btn-warning' onclick='obtenerRubro(" + rubro.rubroId + ")' >Editar</button>";
                    html += "<button class='btn btn-success' onclick='eliminarRubro(" + rubro.rubroId + " , \"" + rubro.rubroNombre + "\")'>Eliminar</button></td>";
                    html += "</tr>";

                }

                ViewBag.Table = html;

                return View();
            }
            else
            {
                return Redirect("~/Login/Index");
            }

        }


        [HttpPost]
        public ActionResult ingresarRubro(string rubroNombre, string rubroDescripcion)
        {
            var modelMantenedor = new Models.MantenedorModel();

            if (modelMantenedor.ExisteRubro(rubroNombre))
            {
                return Json(new { response = "error", message = "El rubro  " + rubroNombre + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }

            if (modelMantenedor.IngresarRubros(rubroNombre, rubroDescripcion, SessionHandler.UsuarioId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar la empresa..." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult obtenerRubro(int id)
        {
            var modelMantenedor = new Models.MantenedorModel();

            Models.DTO.Rubro rubro = modelMantenedor.ObtenerRubroPorId(id);

            return Json(new { rubroId = rubro.rubroId, rubroNombre = rubro.rubroNombre, rubroDescripcion = rubro.rubroDescripcion }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult editarRubro(int rubroId, string rubroNombre, string rubroDescripcion)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.EditarRubros(rubroId, rubroNombre, rubroDescripcion, SessionHandler.UsuarioId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar el rubro..." }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        public ActionResult eliminarRubro(int rubroId)
        {
            var modelMantenedor = new Models.MantenedorModel();

            if (modelMantenedor.RubroEsEliminable(rubroId))
            {
                if (modelMantenedor.EliminarRubro(rubroId))
                {

                    return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar El rubro..." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { response = "error", message = "No se puede Borrar el Rubro porque tiene datos asociados..." }, JsonRequestBehavior.AllowGet);
            }


        }


        public ActionResult Grupos()
        {
            if (SessionHandler.Logged)
            {
                var modelMantenedor = new Models.MantenedorModel();
                ViewBag.PageTitle = "Mantenedor";
                ViewBag.UsuarioNombre = SessionHandler.Usuario;
                ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                var lista = modelMantenedor.obtenerGrupos();
                string html = "";
                foreach (var grupo in lista)
                {
                    html += "<tr>";
                    html += "<td>" + grupo.grupoNombre + "</td>";
                    html += "<td>" + grupo.grupoDescripcion + "</td>";
                    html += "<td><button class='btn btn-warning' onclick='obtenerGrupo(" + grupo.grupoId + ")' >Editar</button>";
                    html += "<button class='btn btn-success' onclick='eliminarGrupo(" + grupo.grupoId + " , \"" + grupo.grupoNombre + "\")'>Eliminar</button></td>";
                    html += "</tr>";

                }

                ViewBag.Table = html;

                return View();
            }
            else
            {
                return Redirect("~/Login/Index");
            }

        }

        [HttpPost]
        public ActionResult ingresarGrupo(string grupoNombre, string grupoDescripcion)
        {
            var modelMantenedor = new Models.MantenedorModel();

            if (modelMantenedor.ExisteGrupo(grupoNombre))
            {
                return Json(new { response = "error", message = "El grupo  " + grupoNombre + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }

            if (modelMantenedor.IngresarGrupos(grupoNombre, grupoDescripcion, SessionHandler.UsuarioId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar el grupo..." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult obtenerGrupo(int id)
        {
            var modelMantenedor = new Models.MantenedorModel();

            Models.DTO.Grupo grupo = modelMantenedor.ObtenerGrupoPorId(id);

            return Json(new { grupoId = grupo.grupoId, grupoNombre = grupo.grupoNombre, grupoDescripcion = grupo.grupoDescripcion }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult editarGrupo(int grupoId, string grupoNombre, string grupoDescripcion)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.EditarGrupos(grupoId, grupoNombre, grupoDescripcion, SessionHandler.UsuarioId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar el grupo..." }, JsonRequestBehavior.AllowGet);
            }

        }



        [HttpPost]
        public ActionResult eliminarGrupo(int grupoId)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.GrupoEsEliminable(grupoId))
            {
                if (modelMantenedor.EliminarGrupo(grupoId))
                {

                    return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar El grupo..." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { response = "error", message = "No se puede Borrar el Grupo porque tiene datos asociados..." }, JsonRequestBehavior.AllowGet);
            }




        }

        public ActionResult Cuentas()
        {
            if (SessionHandler.Logged)
            {
                var modelMantenedor = new Models.MantenedorModel();
                ViewBag.PageTitle = "Mantenedor de Cuentas";
                ViewBag.UsuarioNombre = SessionHandler.Usuario;
                ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                var empresas = modelMantenedor.obtenerEmpresas();
                var ehtml = "";
                foreach (var empresa in empresas)
                {
                    ehtml += "<option value='" + empresa.empresaId + "'>" + empresa.razonSocial + "</option>";
                }

                ViewBag.EmpresaSelect = ehtml;

                var rubros = modelMantenedor.obtenerRubros();
                var rhtml = "";
                foreach (var rubro in rubros)
                {
                    rhtml += "<option value='" + rubro.rubroId + "'>" + rubro.rubroNombre + "</option>";
                }

                ViewBag.RubroSelect = rhtml;

                var grupos = modelMantenedor.obtenerGrupos();
                var ghtml = "";
                foreach (var grupo in grupos)
                {
                    ghtml += "<option value='" + grupo.grupoId + "'>" + grupo.grupoNombre + "</option>";
                }

                ViewBag.GrupoSelect = ghtml;



                var lista = modelMantenedor.obtenerCuentas();
                string html = "";
                foreach (var cuenta in lista)
                {
                    html += "<tr>";
                    html += "<td>" + cuenta.numero + "</td>";
                    html += "<td>" + cuenta.nombre + "</td>";
                    html += "<td>" + cuenta.empresa.razonSocial + "</td>";
                    html += "<td>" + cuenta.rubro.rubroNombre + "</td>";
                    html += "<td>" + cuenta.grupo.grupoNombre + "</td>";
                    html += "<td>" + cuenta.descripcion + "</td>";
                    html += "<td>";
                    html += string.Format("   <button class='btn btn-warning' onclick='location=\"CuentasAsociadas/{0}-{1}\"' >Ver</button>", cuenta.id, cuenta.numero);
                    html += "   <button class='btn btn-warning' onclick='obtenerCuenta(" + cuenta.id + ")' >Editar</button>";
                    html += "   <button class='btn btn-success' onclick='eliminarCuenta(" + cuenta.id + " , \"" + cuenta.nombre + "\")'>Eliminar</button>";
                    html += "</td>";
                    html += "</tr>";

                }

                ViewBag.Table = html;

                return View();
            }
            else
            {
                return Redirect("~/Login/Index");
            }

        }


        public ActionResult CuentasAsociadas()
        {
            if (SessionHandler.Logged)
            {

                if (RouteData.Values["id"] != null)
                {
                    string id = RouteData.Values["id"].ToString();
                    if (id.Contains("-"))
                    {
                        try
                        {
                            var modelMantenedor = new Models.MantenedorModel();
                            var modelCuentaActiva = new Models.CuentaActivaModel();

                            string[] sh = id.Split('-');
                            int cuentaId = int.Parse(sh[0]);
                            string numero = sh[1];
                            ViewBag.PageTitle = "Cuentas Activas Asociadas a " + numero;
                            ViewBag.UsuarioNombre = SessionHandler.Usuario;
                            ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                            Models.DTO.Cuenta cuenta = modelMantenedor.ObtenerCuentaPorId(cuentaId);

                            ViewBag.CuentaNombre = numero + " - " + cuenta.nombre;

                            var lista = modelCuentaActiva.ObtenerCuentasActivasAsociadasPorCuenta(cuentaId);

                            string html = "";
                            foreach (var c in lista)
                            {
                                var archivos = modelCuentaActiva.ObtenerNumeroDeArchivosPorCuentaActiva(c.cuentaActivaId, c.cuentaActivaFecha.Year, c.cuentaActivaFecha.Month);
                                html += "<tr>";
                                html += "<td>" + c.cuentaActivaFecha.Year + "</td>";
                                html += "<td>" + c.cuentaActivaFecha.Month + "</td>";
                                html += "<td>" + c.empresa.razonSocial + "</td>";
                                html += "<td>" + c.analista.nombre + "</td>";
                                html += "<td>" + c.validador.nombre + "</td>";
                                html += "<td>" + c.certificador.nombre + "</td>";
                                html += "<td>" + archivos + "</td>";
                                html += "<td>";
                                var botones = "";//<a href='#' id='btnEditarCA_{0}' class='btn btn-info' onclick='EditarCuenta({0})'>Editar</a>";
                                if (archivos == 0)
                                {
                                    botones += "   <a href='#' class='btn btn-info' onclick='EliminarCuenta({0}, \"{1}\", {2}, {3})'>Eliminar</a>";
                                }
                                else
                                {
                                    botones += "   <a href='#' class='btn btn-info disabled' >Eliminar</a>";
                                }
                                html += string.Format(botones, c.cuentaActivaId, numero, c.cuentaActivaFecha.Year, c.cuentaActivaFecha.Month);
                                html += "</td>";
                                html += "</tr>";

                            }

                            ViewBag.Table = html;

                            return View();
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }

            }

            return Redirect("~/Login/Index");

        }


        [HttpPost]
        public ActionResult ingresarCuenta(string nroCuenta, string nombreCuenta, int empresaCuenta, int empresaRubro, int empresaGrupo, string descripcionCuenta)
        {
            var modelMantenedor = new Models.MantenedorModel();

            if (modelMantenedor.ExisteCuenta("", nombreCuenta, empresaCuenta))
            {
                return Json(new { response = "error", message = "La cuenta  " + nombreCuenta + " ya Existe." }, JsonRequestBehavior.AllowGet);

            }

            if (modelMantenedor.ExisteCuenta(nroCuenta, "", empresaCuenta))
            {
                return Json(new { response = "error", message = "La cuenta  " + nroCuenta + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }



            if (modelMantenedor.IngresarCuenta(nroCuenta, nombreCuenta, empresaCuenta, empresaRubro, empresaGrupo, descripcionCuenta, SessionHandler.UsuarioId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar la cuenta..." }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        public ActionResult obtenerCuenta(int id)
        {
            var modelMantenedor = new Models.MantenedorModel();

            Models.DTO.Cuenta cuenta = modelMantenedor.ObtenerCuentaPorId(id);

            return Json(new { cuentaId = cuenta.id, cuentaNumero = cuenta.numero, cuentaNombre = cuenta.nombre, cuentaEmpresa = cuenta.empresa.empresaId, cuentaRubro = cuenta.rubro.rubroId, cuentaGrupo = cuenta.grupo.grupoId, cuentaDescripcion = cuenta.descripcion }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult editarCuenta(int cuentaId, string nroCuenta, string nombreCuenta, int empresaCuenta, int empresaRubro, int empresaGrupo, string descripcionCuenta)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.EditarCuentas(cuentaId, nroCuenta, nombreCuenta, empresaCuenta, empresaRubro, empresaGrupo, descripcionCuenta, SessionHandler.UsuarioId))
            {

                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar la cuenta..." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult eliminarCuenta(int cuentaId)
        {
            var modelMantenedor = new Models.MantenedorModel();

            if (modelMantenedor.CuentaEsEliminable(cuentaId))
            {
                if (modelMantenedor.EliminarCuenta(cuentaId))
                {

                    return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar La Cuenta..." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { response = "error", message = "No se puede Eliminar la cuenta porque existen Cuentas Activas Asociadas..." }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Usuarios()
        {
            if (SessionHandler.Logged)
            {
                var modelMantenedor = new Models.MantenedorModel();
                ViewBag.PageTitle = "Mantenedor";
                ViewBag.UsuarioNombre = SessionHandler.Usuario;
                ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);


                var perfiles = modelMantenedor.obtenerPerfiles();
                var phtml = "";
                foreach (var item in perfiles)
                {
                    phtml += "<option value='" + item.Id + "'>" + item.Nombre + "</option>";
                }

                ViewBag.PerfilSelect = phtml;

                var empresas = modelMantenedor.obtenerEmpresas();
                var ehtml = "";
                foreach (var empresa in empresas)
                {
                    ehtml += "<option value='" + empresa.empresaId + "'>" + empresa.razonSocial + "</option>";
                }

                ViewBag.EmpresaSelect = ehtml;

                var lista = modelMantenedor.obtenerUsuarios();
                string html = "";
                foreach (var item in lista)
                {
                    html += "<tr>";
                    html += "<td>" + item.rut + "</td>";
                    html += "<td>" + item.nombre + "</td>";
                    html += "<td>" + item.empresa.razonSocial + "</td>";
                    html += "<td>" + item.email + "</td>";
                    html += "<td>" + item.perfil.Nombre + "</td>";
                    html += "<td><button class='btn btn-warning' onclick='obtenerUsuario(" + item.usuarioId + ")' >Editar</button>";
                    html += "<button class='btn btn-success' onclick='eliminarUsuario(" + item.usuarioId + ", \"" + item.nombre + "\")' >Eliminar</button>";
                    html += "<button class='btn btn-warning' onclick='editarClave(" + item.usuarioId + ", \"" + item.nombre + "\")' >Editar Clave</button></td>";
                    html += "</tr>";
                }


                ViewBag.Table = html;


                return View();
            }
            else
            {
                return Redirect("~/Login/Index");
            }

        }

        [HttpPost]
        public ActionResult ingresarUsuario(string usuarioNombre, int empresaUsuario, string usuarioEmail, int perfilUsuario, string usuarioRut)
        {
            var modelMantenedor = new Models.MantenedorModel();


            if (modelMantenedor.ExisteEmailUsuario(usuarioEmail))
            {
                return Json(new { response = "error", message = "El  E-Mail  " + usuarioEmail + " ya Existe." }, JsonRequestBehavior.AllowGet);
            }

            string username = usuarioEmail;
            var rnd = new Random(DateTime.Now.Millisecond);
            int ticks = rnd.Next(111111111, 999999999);

            string pwd = ticks.ToString();

            if (modelMantenedor.IngresarUsuarios(usuarioNombre, empresaUsuario, usuarioEmail, perfilUsuario, username, pwd, SessionHandler.UsuarioId, usuarioRut))
            {


                string subject = "Credenciales para accedo al sistema SAC";
                string body = "Su clave de acceso es: " + pwd;
                MailHelper.mail(usuarioNombre, usuarioEmail, subject, body);
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar el usuario..." }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        public ActionResult obtenerUsuario(int id)
        {
            var modelMantenedor = new Models.MantenedorModel();

            Models.DTO.Usuario usuario = modelMantenedor.ObtenerUsuarioPorId(id);

            return Json(new { usuarioId = usuario.usuarioId, usuarioNombre = usuario.nombre, usuarioEmpresaId = usuario.empresa.empresaId, usuarioEmail = usuario.email, usuarioPerfil = usuario.perfil.Id, usuarioRut = usuario.rut }, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public ActionResult editarUsuario(int usuarioId, int empresaUsuario, string usuarioNombre, string usuarioEmail, int perfilUsuario, string usuarioRut)
        {

            var modelMantenedor = new Models.MantenedorModel();

            if (modelMantenedor.EditarUsuario(usuarioId, empresaUsuario, usuarioNombre, usuarioEmail, perfilUsuario, SessionHandler.UsuarioId, usuarioRut))
            {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar el usuario..." }, JsonRequestBehavior.AllowGet);
            }



        }


        [HttpPost]
        public ActionResult eliminarUsuario(int usuarioId)
        {
            var modelMantenedor = new Models.MantenedorModel();

            if (modelMantenedor.UsuarioEsEliminable(usuarioId))
            {
                if (modelMantenedor.EliminarUsuario(usuarioId))
                {

                    return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar El usuario..." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { response = "error", message = "No se puede Eliminar el Usuario porque esta asociado a una cuenta activa..." }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult editarClave(int usuarioId)
        {
            var modelMantenedor = new Models.MantenedorModel();


            var rnd = new Random(DateTime.Now.Millisecond);
            int ticks = rnd.Next(111111111, 999999999);

            string pwd = ticks.ToString();

            Models.DTO.Usuario usuario = modelMantenedor.ObtenerUsuarioPorId(usuarioId);

            //  if (modelMantenedor.IngresarUsuarios(usuarioNombre, empresaUsuario, usuarioEmail, perfilUsuario, username, pwd, SessionHandler.UsuarioId))
            if (modelMantenedor.EditarClave(usuarioId, pwd, SessionHandler.UsuarioId))
            {


                string subject = "Credenciales para accedo al sistema SAC";
                string body = "Su clave de acceso es: " + pwd;
                MailHelper.mail(usuario.nombre, usuario.email, subject, body);
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar la Clave del usuario..." }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        public ActionResult procesarExcel(string archivo)
        {
            var modelExcel = new Models.ExcelModel();
            var modelMantenedor = new Models.MantenedorModel();
            var selectEmpresas = "";
            var empresas = modelMantenedor.obtenerEmpresas();
            var selectRubros = "";
            var rubros = modelMantenedor.obtenerRubros();
            var selectGrupos = "";
            var grupos = modelMantenedor.obtenerGrupos();

            foreach (Empresa fila in empresas)
            {
                selectEmpresas += "<option value='" + fila.empresaId + "'>" + fila.razonSocial + "</option>";
            }
            foreach (Rubro fila in rubros)
            {
                selectRubros += "<option value='" + fila.rubroId + "'>" + fila.rubroNombre + "</option>";
            }
            foreach (Grupo fila in grupos)
            {
                selectGrupos += "<option value='" + fila.grupoId + "'>" + fila.grupoNombre + "</option>";
            }

            List<Cuenta> cuentas;
            cuentas = modelExcel.ObtenerCuentas(HostingEnvironment.ApplicationPhysicalPath + "Archivos\\Temporal\\" + archivo);
            string html = "";
            int contador = 0;
            bool errores = false;
            foreach (Cuenta cuenta in cuentas)
            {
                contador++;
                Cuenta c = modelMantenedor.IngresarActualizarCuenta(cuenta, SessionHandler.UsuarioId);

                if (c.id == -1)
                {
                    html += "<tr class='danger' id='" + contador + "'>";
                    errores = true;
                }
                else
                {
                    html += "<tr>";
                }



                html += "<td>" + contador + "</td>";
                html += "<td>" + c.numero + "</td>";
                html += "<td>" + c.nombre + "</td>";

                html += "<td>";
                if (c.empresa.empresaId == -1)
                {
                    html += "<select name='empresas' class='form-control' >";
                    html += "<option value=''>" + c.empresa.razonSocial + " (Valor Erroneo) </option>" + selectEmpresas;
                    html += "</select>";
                }
                else
                {
                    html += c.empresa.razonSocial;
                }
                html += "</td>";


                html += "<td>";
                if (c.rubro.rubroId == -1)
                {
                    html += "<select name='rubros' class='form-control' >";
                    html += "<option value=''>" + c.rubro.rubroNombre + " (Valor Erroneo) </option>" + selectRubros;
                    html += "</select>";
                }
                else
                {
                    html += c.rubro.rubroNombre;
                }
                html += "</td>";

                html += "<td>";
                if (c.grupo.grupoId == -1)
                {
                    html += "<select name='grupos' class='form-control' >";
                    html += "<option value=''>" + c.grupo.grupoNombre + " (Valor Erroneo) </option>" + selectGrupos;
                    html += "</select>";
                }
                else
                {
                    html += c.grupo.grupoNombre;
                }
                html += "</td>";


                html += "<td>" + c.descripcion + "</td>";
                html += "<td>";
                if (c.id == -1)
                {

                    html += "FALLIDO <i class='fa fa-times-circle danger'></i>";
                }
                else
                {
                    if (c.insertUpdate)
                    {
                        html += "INSERTADA  <i class='fa fa-plus-circle' style='color:#4f4' ></i>";

                    }
                    else
                    {

                        html += "ACTUALIZADA <i class='fa fa-check-circle' style='color:#4f4' ></i>";

                    }
                }
                html += "</td>";
                html += "</tr>";


            }




            return Json(new { response = "success", tabla = html, errores = errores }, JsonRequestBehavior.AllowGet);


        }



        [HttpPost]
        public ActionResult reprocesarExcel(string[][] data)
        {
            var modelExcel = new Models.ExcelModel();
            var modelMantenedor = new Models.MantenedorModel();
            //fila,  cuenta,nombre,  empresa,  rubro,  grupo,    descripcion
            List<FilaJson> lista = new List<FilaJson>();
            foreach (string[] fila in data)
            {
                string n = fila[0];
                string cuenta = fila[1];
                string nombre = fila[2];
                string empresa = fila[3];
                string rubro = fila[4];
                string grupo = fila[5];
                string descripcion = fila[6];
                Cuenta c = new Cuenta();
                c.numero = cuenta;
                c.nombre = nombre;
                c.empresa = new Empresa();
                c.empresa.razonSocial = empresa;
                c.rubro = new Rubro();
                c.rubro.rubroNombre = rubro;
                c.grupo = new Grupo();
                c.grupo.grupoNombre = grupo;
                c.descripcion = descripcion;
                var r = modelMantenedor.IngresarActualizarCuenta(c, SessionHandler.UsuarioId);
                lista.Add(new FilaJson { fila = n, resultado = r.insertUpdate.ToString() });
            }


            return Json(new { response = "success", resultados = Json(lista) }, JsonRequestBehavior.AllowGet);



        }


        class FilaJson
        {

            public string fila { get; set; }
            public string resultado { get; set; }


        }




    }
}