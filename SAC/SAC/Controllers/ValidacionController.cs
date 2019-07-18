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
    public class ValidacionController :  EtapasController
    {
        // GET: Validacion
        public ActionResult Index()
        {
            if (SessionHandler.Logged && SessionHandler.Perfil == 4)
            {
                var modelEtapas = new Models.EtapasModel();
                ViewBag.PageTitle = "Validacion";
                ViewBag.UsuarioNombre = SessionHandler.Usuario;
                ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                var usuarioId = SessionHandler.UsuarioId;
                //var lista = modelEtapas.obtenerCuentasActivasEnEstadoDos(usuarioId);
                //var listaFinalizadas = modelEtapas.obtenerCuentasActivasEnEstadoDosFinalizadas(usuarioId);
                var perfilId = SessionHandler.Perfil;
                var lista = modelEtapas.obtenerCuentasActivasPorUsuario(usuarioId, perfilId);
                var listaFinalizadas = modelEtapas.obtenerCuentasActivasFueraDeEtapaPorUsuario(usuarioId, perfilId);

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
                foreach (var cuentaActivaFn in listaFinalizadas)
                {

                    htmlFn += "<tr>";
                    htmlFn += "<td>" + cuentaActivaFn.empresa.razonSocial + "</td>";
                    htmlFn += "<td>" + cuentaActivaFn.numero + "</td>";
                    htmlFn += "<td>" + cuentaActivaFn.cuentaActivaFecha + "</td>";
                    htmlFn += "<td>" + cuentaActivaFn.cuentaActivaDiasPlazo + "</td>";
                    htmlFn += "<td>" + cuentaActivaFn.cuentaActivaTotal + "</td>";
                    htmlFn += "<td></td>";
                    string linkFn = "<a class='btn btn-warning' href='Ver/{0}-{1}-{2}-{3}' >Ver</a>";


                    linkFn = string.Format(linkFn,
                        cuentaActivaFn.numero,
                        cuentaActivaFn.id,
                        StringHelper.DosNumeros(cuentaActivaFn.cuentaActivaFecha.Month),
                        cuentaActivaFn.cuentaActivaFecha.Year);
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
            if (SessionHandler.Logged && SessionHandler.Perfil == 4)
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
                        botones = "<a class='btn btn-info blue'  onclick='agregarArchivo()'>Agregar Archivo</a> <a class='btn btn-info blue' onclick='prepararEnviar()'>Enviar a   Certificación</a> <a class='btn btn-info blue' onclick='prepararRetroceder()'> Retroceder a Analisis </a>";

                        if (cuenta.cuentaActivaEstado == 2 || cuenta.cuentaActivaEstado == 4)
                        {
                            ViewBag.botones = botones;
                        }
                        ViewBag.Analista = cuenta.analista.nombre;
                        ViewBag.Validador = cuenta.validador.nombre;
                        ViewBag.Certificador = cuenta.certificador.nombre;
                        ViewBag.AnalistaComentarioFecha = "";
                        ViewBag.ValidadorComentarioFecha = "";
                        ViewBag.CertificadorComentarioFecha = "";
                        ViewBag.AnalistaComentario = "";
                        ViewBag.ValidadorComentario = "";
                        ViewBag.CertificadorComentario = "";

                        ViewBag.Archivos = this.ListaArchivos(cuentaId, mes, año, 2, 4);
                        ViewBag.CuentaId = cuentaId;
                        ViewBag.CuentaFecha = cuenta.cuentaActivaFecha;

                        ViewBag.LineaDeTiempo = this.TimeLine(cuentaId, mes, año);
                        ViewBag.Logs = this.ListaLogs(cuentaId, mes, año);
                        ViewBag.LogStatus = modelEtapas.ObtenerCuentaActivaEstadoLog(cuentaId, mes, año);

                        return View();
                    }
                }


                return Redirect("~/Validacion/Index");




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

            if (modelEtapas.EliminarArchivo(archivoId, version ))
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
        public ActionResult EnviarACertificacion(string comentario, int cuentaId, string cuentaFecha)
        {
            EtapasModel modelEtapas = new EtapasModel();
            CuentaActivaModel modelCuentas = new CuentaActivaModel();
            DateTime fecha = Convert.ToDateTime(cuentaFecha);
            if (modelEtapas.CambiarEstadoCuenta(comentario, 3, cuentaId,fecha))
            {
                var cuenta = modelCuentas.obtenerCuentaActivaPorId(cuentaId, fecha.Month, fecha.Year);
                MailHelper.mail(
                    cuenta.certificador.nombre,
                    cuenta.certificador.email,
                    string.Format("Cuenta {0} ha sido enviada para Certificar", cuenta.numero),
                    string.Format("El Usuario {0} ha enviado la cuenta {1} para ser certificada ({2})", cuenta.validador.nombre, cuenta.numero, StringHelper.fechaDMA(DateTime.Today))
                    );
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar Enviar la cuenta a validación..." }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult EnviarAnalisis(string comentario, int cuentaId, string cuentaFecha)
        {
            EtapasModel modelEtapas = new EtapasModel();
            CuentaActivaModel modelCuentas = new CuentaActivaModel();
            DateTime fecha = Convert.ToDateTime(cuentaFecha);
            if (modelEtapas.CambiarEstadoCuenta(comentario, 1, cuentaId, fecha))
            {
                var cuenta = modelCuentas.obtenerCuentaActivaPorId(cuentaId, fecha.Month, fecha.Year);
                MailHelper.mail(
                    cuenta.analista.nombre,
                    cuenta.analista.email,
                    string.Format("Cuenta {0} ha sido rechazada por el Validador", cuenta.numero),
                    string.Format("El Usuario {0} ha rechazado la cuenta {1} ({2})", cuenta.validador.nombre, cuenta.numero, StringHelper.fechaDMA(DateTime.Today))
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