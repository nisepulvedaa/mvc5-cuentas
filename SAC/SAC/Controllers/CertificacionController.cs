using SAC.Helpers;
using SAC.Models;
using SAC.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace SAC.Controllers
{
    public class CertificacionController : EtapasController
    {
        // GET: Certificacion
        public ActionResult Index()
        {
            if (SessionHandler.Logged && SessionHandler.Perfil == 5)
            {
                var modelEtapas = new Models.EtapasModel();
                ViewBag.PageTitle = "Certificacion";
                ViewBag.UsuarioNombre = SessionHandler.Usuario;
                ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                var usuarioId = SessionHandler.UsuarioId;
                var perfilId = SessionHandler.Perfil;
                var lista = modelEtapas.obtenerCuentasActivasPorUsuario(usuarioId, perfilId);
                var listaFinalizadas = modelEtapas.obtenerCuentasActivasFueraDeEtapaPorUsuario(usuarioId, perfilId);
                //var lista = modelEtapas.obtenerCuentasActivasEnEstadoTres(usuarioId);
                //var listaFinalizadas = modelEtapas.obtenerCuentasActivasEnEstadoTresFinalizadas(usuarioId);

                string html = "";
                foreach (var cuentaActiva in lista)
                {

                    html += "<tr>";
                    html += "<td>" + cuentaActiva.empresa.razonSocial + "</td>";
                    html += "<td>" + cuentaActiva.numero + "</td>";
                    html += "<td>" + cuentaActiva.cuentaActivaFecha + "</td>";
                    html += "<td>" + cuentaActiva.cuentaActivaDiasPlazo + "</td>";
                    html += "<td>" + cuentaActiva.cuentaActivaTotal + "</td>";
                    html += "<td></td>";



                    string link = "<a class='btn btn-warning' href='Ver/{0}-{1}-{2}-{3}' >Ver</a>";


                    link = string.Format(link,
                        cuentaActiva.numero,
                        cuentaActiva.id,
                        StringHelper.DosNumeros(cuentaActiva.cuentaActivaFecha.Month),
                        cuentaActiva.cuentaActivaFecha.Year);
                    html += "<td>" + link + "</td>";

                    html += "</tr>";

                }
                ViewBag.Table = html;

                string htmlFn = "";
                foreach (var cuentaActiva in listaFinalizadas)
                {

                    htmlFn += "<tr>";
                    htmlFn += "<td>" + cuentaActiva.empresa.razonSocial + "</td>";
                    htmlFn += "<td>" + cuentaActiva.numero + "</td>";
                    htmlFn += "<td>" + cuentaActiva.cuentaActivaFecha + "</td>";
                    htmlFn += "<td>" + cuentaActiva.cuentaActivaDiasPlazo + "</td>";
                    htmlFn += "<td>" + cuentaActiva.cuentaActivaTotal + "</td>";
                    htmlFn += "<td></td>";
                    string linkFn = "<a class='btn btn-warning' href='Ver/{0}-{1}-{2}-{3}' >Ver</a>";


                    linkFn = string.Format(linkFn,
                        cuentaActiva.numero,
                        cuentaActiva.id,
                        StringHelper.DosNumeros(cuentaActiva.cuentaActivaFecha.Month),
                        cuentaActiva.cuentaActivaFecha.Year);
                    htmlFn += "<td>" + linkFn + "</td>";

                    htmlFn += "</tr>";

                }
                ViewBag.TableFinalizadas = htmlFn;




                return View();
            }
            else
            {
                return Redirect("~/Login/Index");
            }
        }





        public ActionResult Ver()
        {
            if (SessionHandler.Logged && SessionHandler.Perfil == 5)
            {



                if (RouteData.Values["id"] != null)
                {
                    string id = RouteData.Values["id"].ToString();
                    if (StringHelper.ContarCaracteres(id, '-') == 3)
                    {

                        EtapasModel modelEtapas = new EtapasModel();
                        MantenedorModel modelMantenedor = new MantenedorModel();
                        CuentaActivaModel modelCuentaActiva = new CuentaActivaModel();

                        ViewBag.UsuarioNombre = SessionHandler.Usuario;
                        ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                        string[] campos = id.Split('-');
                        string numero = campos[0];
                        int cuentaId = int.Parse(campos[1]);
                        int mes = int.Parse(campos[2]);
                        int año = int.Parse(campos[3]);

                        ViewBag.PageTitle = "Analisis Cuenta: " + numero;
                        ViewBag.Numero = numero;
                        ViewBag.Mes = StringHelper.DosNumeros(mes);
                        ViewBag.Año = año;

                        CuentaActiva cuenta = modelCuentaActiva.obtenerCuentaActivaPorId(cuentaId, mes, año);
                        //Usuario usuario = modelMantenedor.ObtenerUsuarioPorId(cuenta.id);

                        ViewBag.Empresa = cuenta.empresa.razonSocial;
                        ViewBag.Rubro = cuenta.rubro.rubroNombre;
                        ViewBag.Grupo = cuenta.grupo.grupoNombre;

                        string botones = "";
                        botones = "<a class='btn btn-info blue'  onclick='agregarArchivo()'>Agregar Archivo</a> ";
                        botones += "<a class='btn btn-info blue' onclick='prepararEnviar()'>Finalizar Cuenta</a> ";
                        botones += "<a class='btn btn-info blue' onclick='prepararRetroceder()'> Retroceder a Validaci&oacute;n </a>";

                        if (cuenta.cuentaActivaEstado == 3)
                        {
                            ViewBag.botones = botones;
                        }
                        ViewBag.Analista = cuenta.analista.nombre;
                        ViewBag.Validador = cuenta.validador.nombre;
                        ViewBag.Certificador = cuenta.certificador.nombre;


                        ViewBag.Archivos = this.ListaArchivos(cuentaId, mes, año, 3);
                        ViewBag.CuentaId = cuentaId;
                        ViewBag.CuentaFecha = cuenta.cuentaActivaFecha;

                        ViewBag.LineaDeTiempo = this.TimeLine(cuentaId, mes, año);
                        ViewBag.Logs = this.ListaLogs(cuentaId, mes, año);
                        ViewBag.LogStatus = modelEtapas.ObtenerCuentaActivaEstadoLog(cuentaId, mes, año);

                        return View();
                    }
                }


                return Redirect("~/Certificacion/Index");




            }
            else
            {
                return Redirect("~/Login/Index");
            }
        }


        [HttpPost]
        public ActionResult ingresarArchivo(string nombreFile, string nombreArchivo, int montoArchivo, string comentarioArchivo, int cuentaId, string cuentaFecha)
        {
            var extension = StringHelper.extension(nombreFile);
            byte[] archivo = System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath + "Archivos\\Temporal\\" + nombreFile); ;
            var modelEtapas = new Models.EtapasModel();

            if (modelEtapas.IngresarArchivo(nombreArchivo, archivo, montoArchivo, comentarioArchivo, extension, SessionHandler.UsuarioId, cuentaId, cuentaFecha))
            {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar el archivo ..." }, JsonRequestBehavior.AllowGet);
            }


        }


        [HttpPost]
        public ActionResult obtenerArchivo(int id, int version)
        {
            var modelEtapa = new Models.EtapasModel();


            Models.DTO.Archivo archivo = modelEtapa.obtenerArchivoPorId(id, version);


            return Json(new { archivoId = archivo.Id, archivoVersion = archivo.Version, archivoNombre = archivo.Nombre, archivoMonto = archivo.Monto, archivoComentario = archivo.Comentario }, JsonRequestBehavior.AllowGet);


        }




        [HttpPost]
        public ActionResult editarArchivo(int archivoId, int archivoVersion, string nombreArchivo, int montoArchivo, string comentarioArchivo)
        {

            var modelEtapas = new Models.EtapasModel();

            if (modelEtapas.EditarArchivo(archivoId, archivoVersion, nombreArchivo, montoArchivo, comentarioArchivo, SessionHandler.UsuarioId))
            {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Editar el archivo..." }, JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public ActionResult eliminarArchivo(int archivoId, int version)
        {

            var modelEtapas = new Models.EtapasModel();

            if (modelEtapas.EliminarArchivo(archivoId, version))
            {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar El archivo..." }, JsonRequestBehavior.AllowGet);
            }

        }


        //[HttpPost]
        //public ActionResult ingresarVersion(int archivoId, int archivoVersion, string nombreArchivo, int montoArchivo, string comentarioArchivo, string nombreFile)
        //{

        //    byte[] archivo = System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath + "Archivos\\Temporal\\" + nombreFile); ;
        //    var modelEtapas = new Models.EtapasModel();

        //    if (modelEtapas.IngresarVersion(archivoId, archivoVersion, nombreArchivo, montoArchivo, comentarioArchivo, archivo, SessionHandler.UsuarioId))
        //    {
        //        return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar la Version..." }, JsonRequestBehavior.AllowGet);
        //    }


        //}


        [HttpPost, ValidateInput(false)]
        public ActionResult FinalizarCuenta(string comentario, int cuentaId, string cuentaFecha)
        {
            EtapasModel modelEtapas = new EtapasModel();
            if (modelEtapas.CambiarEstadoCuenta(comentario, 5, cuentaId, Convert.ToDateTime(cuentaFecha)))
            {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar Enviar la cuenta a validación..." }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult EnviarCertificacion(string comentario, int cuentaId, string cuentaFecha)
        {
            EtapasModel modelEtapas = new EtapasModel();
            CuentaActivaModel modelCuentas = new CuentaActivaModel();
            DateTime fecha = Convert.ToDateTime(cuentaFecha);
            if (modelEtapas.CambiarEstadoCuenta(comentario, 4, cuentaId, fecha))
            {
                var cuenta = modelCuentas.obtenerCuentaActivaPorId(cuentaId, fecha.Month, fecha.Year);
                MailHelper.mail(
                    cuenta.validador.nombre,
                    cuenta.validador.email,
                    string.Format("Cuenta {0} ha sido rechazada por el Certificador", cuenta.numero),
                    string.Format("El Usuario {0} ha rechazado la cuenta {1} ({2})", cuenta.certificador.nombre, cuenta.numero, StringHelper.fechaDMA(DateTime.Today))
                    );
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar Enviar la cuenta a validación..." }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}