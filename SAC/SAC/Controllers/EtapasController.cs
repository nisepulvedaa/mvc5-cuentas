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
    public abstract class EtapasController : Controller
    {
        protected string TimeLine(int cuentaActivaId, int mes, int año)
        {
            EtapasModel modelEtapas = new EtapasModel();
            CuentaActivaModel modelCuentaActiva = new CuentaActivaModel();
            MantenedorModel modelMantenedor = new MantenedorModel();
            
            ListaOrdenablePorFecha listaOrden = new ListaOrdenablePorFecha();

            CuentaActiva cuenta = modelCuentaActiva.obtenerCuentaActivaPorId(cuentaActivaId, mes, año);
            List<Log> Logs = modelEtapas.obtenerLogsPorCuentaActiva(cuenta.cuentaActivaId, mes, año);
            List<Archivo> archivos = modelEtapas.obtenerArchivosVersionPorCuentaActiva(cuenta.cuentaActivaId, mes, año);

            foreach (var archivo in archivos)
            {
                listaOrden.Add(archivo.Fecha, archivo);
            }
            foreach (var log in Logs)
            {
                listaOrden.Add(log.Fecha, log);
            }
            string html = "";
            string patron = "<li class='timeline-{3}'>";
            patron += "<div class='timeline-icon'><i class='fa {4}'></i></div>";
            patron += "<div class='timeline-body'><div class='timeline-text'>";
            patron += "<div> {0} ({1}) </div>";
            patron += "<div> {2} </div>";
            patron += "</div></div></li>";

            foreach (var objeto in listaOrden.Descendente())
            {
                if (objeto is Archivo)
                {
                    Archivo a = (Archivo)objeto;
                    Usuario u = modelMantenedor.ObtenerUsuarioPorId(a.UsuarioCreacion);
                    if (a.Version == 1)
                    {
                        html += string.Format(patron,
                            a.Nombre + " (ver." + a.Version + ")",
                            StringHelper.fechaDMA(a.Fecha),
                            "Archivo Agregado por " + u.nombre,
                            "yellow",
                            "fa-plus"
                            );
                    }
                    else {
                        html += string.Format(patron,
                            a.Nombre + " (ver." + a.Version + ")",
                            StringHelper.fechaDMA(a.Fecha),
                            "Archivo Actualizado por " + u.nombre,
                            "purple",
                            "fa-files-o"
                            );
                    }
                }
                if (objeto is Log)
                {
                    Log l = (Log)objeto;
                    //Usuario u = modelMantenedor.ObtenerUsuarioPorId();
                    string titulo = "";
                    string color = "";
                    string icono = "";
                    string texto = "";
                    string descripcion = StringHelper.StripTags(l.Descripcion.Replace("<br>", " "));
                    if (descripcion.Length > 100) {
                        descripcion = descripcion.Substring(0, 97) + "...";
                    }
                    switch (l.EstadoId)
                    {
                        case 1:
                            titulo = "Cuenta Rechazada desde Validaci&oacute;n";
                            color = "red";
                            icono = "fa-close";
                            texto = "Validador: " + cuenta.validador.nombre;
                            break;
                        case 2:
                            titulo = "Cuenta Enviada a Validaci&oacute;n";
                            color = "green";
                            icono = "fa-send";
                            texto = "Analista: " + cuenta.analista.nombre;
                            break;
                        case 4:
                            titulo = "Cuenta Enviada a Validaci&oacute;n";
                            color = "green";
                            icono = "fa-send";
                            texto = "Certificador: " + cuenta.certificador.nombre;
                            break;
                        case 3:
                            titulo = "Cuenta Enviada a Certificación";
                            color = "green";
                            icono = "fa-send";
                            texto = "Validador: " + cuenta.validador.nombre;
                            break;
                        case 5:
                            titulo = "Cuenta Finalizada";
                            color = "green";
                            icono = "fa-send";
                            texto = "Certificador: " + cuenta.certificador .nombre;
                            break;

                    }
                    html += string.Format(patron,
                        titulo,
                        StringHelper.fechaDMA(l.Fecha),
                        texto + "<br>Comentario: " + descripcion,
                        color,
                        icono
                        );
                }
            }

            html += string.Format(patron,
                cuenta.nombre + " - " + cuenta.numero,
                StringHelper.fechaDMA(cuenta.cuentaActivaFecha),
                "Cuenta Asignada a " + cuenta.analista.nombre,
                "blue timeline-noline",
                "fa-check-square-o"
                );

            return html;
        }


        protected string ListaArchivos(int cuentaActivaId, int mes, int año, int etapa, int etapa2  = -1) {
            EtapasModel modelEtapas = new EtapasModel();
            CuentaActivaModel modelCuentaActiva = new CuentaActivaModel();
            MantenedorModel modelMantenedor = new MantenedorModel();

            List<Archivo> archivos = modelEtapas.obtenerArchivosPorCuentaActiva(cuentaActivaId, mes, año);
            CuentaActiva cuenta = modelCuentaActiva.obtenerCuentaActivaPorId(cuentaActivaId, mes, año);

            string html = "";

            foreach (var archivo in archivos)
            {
                if (archivo.Estado != 1 )
                {
                    Usuario usuarioArchivo = modelMantenedor.ObtenerUsuarioPorId(archivo.UsuarioCreacion);
                    string img = string.Format("<img src='/Content/Free-file-icons/512px/{0}.png' onclick='descargar({1}, {2})'>", archivo.Extension, archivo.Id, archivo.Version);
                    var btnEditar = "";
                    //var botones = "";
                    //botones = ' <a class="btn btn-info blue " onclick="agregarArchivo()">Agregar Archivo</a>< a class="btn btn-info blue " onclick="prepararEnviar()">Enviar a Validaci&oacute;n</a>';

                    if (cuenta.cuentaActivaEstado == etapa || cuenta.cuentaActivaEstado == etapa2 )
                    {
                        btnEditar += "<span class='fa fa-edit spanSize'  onclick='obtenerArchivo({3}, {5});'></span> ";
                        btnEditar += "<span class='fa fa-remove spanSizeR'  onclick='eliminarArchivo({3}, {5},  \"{4}\");'></span> ";
                        btnEditar += "<span class='fa fa-files-o spanSizeC' onclick='copiarArchivo({3}, {5})'></span>";
                    }
                    string file = string.Format("{0} " + btnEditar + " <br>{1} <span class='archivoThumbSm'>(v{2})</span>", img, archivo.Nombre, archivo.Version, archivo.Id, archivo.Nombre, archivo.Version);
                    string data = string.Format("{0}<br><span class='archivoThumbSm'>Subido por {2} el {1}</span>", archivo.Comentario, StringHelper.fechaDMA(archivo.Fecha), usuarioArchivo.nombre);
                    html += "<div class='col-sm-4 archivoThumb'><div class='archivoThumbBorder'>";
                    html += "     <div class='archivoThumbFile'>" + file + "</div>";
                    html += "     <div class='archivoThumbData'>" + data + "</div>";
                    html += "</div></div>";
                }

            }

            
            return html;


        }



        protected string ListaLogs(int cuentaActivaId, int mes, int año)
        {
            EtapasModel modelEtapas = new EtapasModel();
            CuentaActivaModel modelCuentaActiva = new CuentaActivaModel();
            MantenedorModel modelMantenedor = new MantenedorModel();

            List<Log> logs = modelEtapas.obtenerLogsPorCuentaActiva(cuentaActivaId, mes, año);
            
            CuentaActiva cuenta = modelCuentaActiva.obtenerCuentaActivaPorId(cuentaActivaId, mes, año);

            string html = "";

            if (logs.Count == 0)
            {
                html = "<div class='row'><div class='col-sm-12'><div class='sac_borderdiv' style='text-align: center;'><span class='title'>Cuenta en Borrador</span></div></div></div>";
            }
            else {
                html = "<div class='row'><div class='col-sm-1 loglistexpandholder' style='height: 100%;'>";
                if (logs.Count > 1) {
                    html += "<i class='fa fa-plus fa-3x loglistexpand' onclick='ExpandirLogs()'></i>";
                }
                html += "</div>";
                html += "<div class='col-sm-11'><div class='sac_borderdiv'>";

                html += generarListaLogItem(logs[logs.Count - 1], cuenta);

                html += "</div></div></div><div id='loglist'>";

                for (int i = logs.Count - 2; i >= 0; i--) {
                    html += "<div class='row'><div class='col-sm-1'></div><div class='col-sm-11'><div class='sac_borderdiv'>";
                    html += generarListaLogItem(logs[i], cuenta) + "</div></div></div>";
                }

                html += "</div>";
            }
            return html;
        }

        private string generarListaLogItem(Log log, CuentaActiva cuenta) {
            string html = "";

            string tipo = "";
            string usuario = "";
            string titulo = "";
            string fecha = StringHelper.fechaDMA(log.Fecha);
            string comentario = log.Descripcion;
            switch (log.EstadoId)
            {
                case 1:
                    titulo = "Cuenta Rechazada desde Validaci&oacute;n";
                    tipo = "Validador";
                    usuario = cuenta.validador.nombre;
                    break;
                case 2:
                    titulo = "Cuenta Enviada a Validaci&oacute;n";
                    tipo = "Analista";
                    usuario = cuenta.analista.nombre;
                    break;
                case 3:
                    titulo = "Cuenta Enviada a Certificaci&oacute;n";
                    tipo = "Validador";
                    usuario = cuenta.validador.nombre;
                    break;
                case 4:
                    titulo = "Cuenta Rechazada desde Certificaci&oacute;n";
                    tipo = "Certificador";
                    usuario = cuenta.certificador.nombre;
                    break;
                case 5:
                    titulo = "Cuenta Certificada";
                    tipo = "Certificador";
                    usuario = cuenta.certificador.nombre;
                    break;
            }

            html = string.Format("<span class='title'>{4}</span><br>{0}: {1} ({2})<br>{3}", tipo, usuario, fecha, comentario, titulo);

            return html;
        }


        [HttpPost]
        public ActionResult ingresarVersion(int archivoId, int archivoVersion, string nombreArchivo, int montoArchivo, string comentarioArchivo, string nombreFile, int cuentaId, string cuentaFecha)
        {

            byte[] archivo = System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath + "Archivos\\Temporal\\" + nombreFile); ;

            var modelEtapas = new Models.EtapasModel();
            var modelCuentaActiva = new Models.CuentaActivaModel();
            var fecha = Convert.ToDateTime(cuentaFecha);
            var extension = StringHelper.extension(nombreFile);

            if (modelEtapas.IngresarVersion(archivoId, archivoVersion, nombreArchivo, montoArchivo, comentarioArchivo, archivo, SessionHandler.UsuarioId, extension, cuentaId, fecha))
            {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar el archivo..." }, JsonRequestBehavior.AllowGet);
            }


        }


    }
}