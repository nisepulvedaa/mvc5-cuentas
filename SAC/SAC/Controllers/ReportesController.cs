using SAC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAC.Models;
using System.Web.Hosting;

namespace SAC.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index()
        {
            if (SessionHandler.Logged && (SessionHandler.Perfil == 1 || SessionHandler.Perfil == 2))
            {
                var modelEtapas = new Models.EtapasModel();
                var modelMantenedor = new Models.MantenedorModel();
                var modelCuentaActiva = new Models.CuentaActivaModel();
                var modelReporte = new ReporteModel();
                ViewBag.PageTitle = "Reportería";
                ViewBag.UsuarioNombre = SessionHandler.Usuario;
                ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);



                var html = "";
                var usuarios = modelMantenedor.obtenerUsuarios();
                foreach (var usuario in usuarios)
                {
                    html += string.Format("<option value='{0}'>{1}</option>", usuario.usuarioId, usuario.nombre);
                }
                ViewBag.UsuariosSelect = html;

                var htmle = "";
                var empresas = modelMantenedor.obtenerEmpresas();
                foreach (var empresa in empresas)
                {
                    htmle += string.Format("<option value='{0}'>{1}</option>", empresa.empresaId, empresa.razonSocial);
                }
                ViewBag.EmpresasSelect = htmle;


                var rubros = modelMantenedor.obtenerRubros();
                var rhtml = "";
                foreach (var rubro in rubros)
                {
                    rhtml += "<option value='" + rubro.rubroId + "'>" + rubro.rubroNombre + "</option>";
                }

                ViewBag.RubrosSelect = rhtml;

                var grupos = modelMantenedor.obtenerGrupos();
                var ghtml = "";
                foreach (var grupo in grupos)
                {
                    ghtml += "<option value='" + grupo.grupoId + "'>" + grupo.grupoNombre + "</option>";
                }

                ViewBag.GruposSelect = ghtml;


                var cuentas = modelMantenedor.obtenerCuentas();
                var chtml = "";
                foreach (var cuenta in cuentas)
                {
                    chtml += string.Format("<option value='{0}'>{1}</option>", cuenta.id, cuenta.numero);
                }

                ViewBag.CuentasSelect = chtml;

                var años = modelReporte.ObtenerAñosCuentasActivas();
                var ahtml = "";
                foreach (var año in años)
                {
                    ahtml += string.Format("<option value='{0}'>{0}</option>", año);
                }

                ViewBag.AñosSelect = ahtml;


                return View();
            }
            else
            {
                return Redirect("~/Login/Index");
            }
        }


        [HttpGet]
        public ActionResult Filtrar(string annos, string meses, string empresas, string rubros, string grupos, string cuentas)
        {
            var modelReporte = new ReporteModel();
            var modelEtapas = new EtapasModel();
            var lista = new List<List<string>>();

            var cuentasActivas = modelReporte.ObtenerCuentasActivasPorFiltro(
                validarArreglo(annos),
                validarArreglo(meses),
                validarArreglo(empresas),
                validarArreglo(rubros),
                validarArreglo(grupos),
                validarArreglo(cuentas)
                );

            foreach (var cuenta in cuentasActivas)
            {
                var fila = new List<string>();
                int estado = modelEtapas.ObtenerCuentaActivaEstadoLog(cuenta.cuentaActivaId, cuenta.cuentaActivaFecha.Month, cuenta.cuentaActivaFecha.Year);
                string estadoStr = "";
                string descripcion = "";
                switch (estado)
                {
                    case 0:
                        estadoStr = "<b>0%</b>";
                        descripcion = "En Borrador";
                        break;
                    case 1:
                        estadoStr = "<b style='color: #ff0000'>33%</b>";
                        descripcion = "Rechazado desde Validaci&oacute;n";
                        break;
                    case 2:
                        estadoStr = "<b style='color: #ff6600;'>66%</b>";
                        descripcion = "Enviado a Validaci&oacute;n";
                        break;
                    case 3:
                        estadoStr = "<b style='color: #ff6600;'>66%</b>";
                        descripcion = "Enviado a Certificaci&oacute;n";
                        break;
                    case 4:
                        estadoStr = "<b style='color: #ff6600;'>66%</b>";
                        descripcion = "Rechazado desde Certificaci&oacute;n";
                        break;
                    case 5:
                        estadoStr = "<b style='color: #66ff33;'>100%</b>";
                        descripcion = "Certificado";
                        break;
                }
                fila.Add(cuenta.empresa.razonSocial);
                fila.Add(cuenta.numero);
                fila.Add(cuenta.rubro.rubroNombre);
                fila.Add(cuenta.grupo.grupoNombre);
                fila.Add(StringHelper.fechaDMA(cuenta.cuentaActivaFecha));
                fila.Add(cuenta.cuentaActivaDiasPlazo.ToString());
                fila.Add(descripcion);
                fila.Add(cuenta.analista.nombre);
                fila.Add(cuenta.validador.nombre);
                fila.Add(cuenta.certificador.nombre);
                fila.Add(estadoStr);

                lista.Add(fila);
            }

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);

            //return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    switch (usuarioId)
            //    {
            //        case -1:
            //            return Json(new { response = "error", message = "Nombre de Usuario o password Incorrecto" }, JsonRequestBehavior.AllowGet);
            //        case -2:
            //            return Json(new { response = "error", message = "No se ha podido establecer una conexion con el Servidor" }, JsonRequestBehavior.AllowGet);
            //    }
            //    return Json(new { response = "error", message = "Error Desconocido" }, JsonRequestBehavior.AllowGet);
            //}
        }


        /*
         * 
         * var excel = new ExcelApp();
            excel.ActiveSheetSetColumnName("A1", "Titulos");

            List<string> lista = new List<string>();
            lista.Add("a");
            lista.Add("b");
            lista.Add("c");
            excel.ActiveSheetLineWriter("A", 2, lista);

            excel.Save("D:\\" + StringHelper.HashUnico("Test_", ".xlsx"));
         * */

        [HttpGet]
        public ActionResult Descargar(string annos, string meses, string empresas, string rubros, string grupos, string cuentas)
        {
            var modelReporte = new ReporteModel();
            var modelEtapas = new EtapasModel();
            var lista = new List<List<string>>();

            var cuentasActivas = modelReporte.ObtenerCuentasActivasPorFiltro(
                validarArreglo(annos),
                validarArreglo(meses),
                validarArreglo(empresas),
                validarArreglo(rubros),
                validarArreglo(grupos),
                validarArreglo(cuentas)
                );

            foreach (var cuenta in cuentasActivas)
            {
                var fila = new List<string>();
                
                int estado = modelEtapas.ObtenerCuentaActivaEstadoLog(cuenta.cuentaActivaId, cuenta.cuentaActivaFecha.Month, cuenta.cuentaActivaFecha.Year);
                string estadoStr = "";
                string descripcion = "";
                switch (estado)
                {
                    case 0:
                        estadoStr = "0%";
                        descripcion = "En Borrador";
                        break;
                    case 1:
                        estadoStr = "33%";
                        descripcion = "Rechazado desde Validación";
                        break;
                    case 2:
                        estadoStr = "66%";
                        descripcion = "Enviado a Validación";
                        break;
                    case 3:
                        estadoStr = "66%";
                        descripcion = "Enviado a Certificación";
                        break;
                    case 4:
                        estadoStr = "66%";
                        descripcion = "Rechazado desde Certificación";
                        break;
                    case 5:
                        estadoStr = "100%";
                        descripcion = "Certificado";
                        break;
                }
                fila.Add(cuenta.empresa.razonSocial);
                fila.Add(cuenta.numero);
                fila.Add(cuenta.rubro.rubroNombre);
                fila.Add(cuenta.grupo.grupoNombre);
                fila.Add(StringHelper.fechaDMA(cuenta.cuentaActivaFecha));
                fila.Add(cuenta.cuentaActivaDiasPlazo.ToString());
                fila.Add(descripcion);
                fila.Add(cuenta.analista.nombre);
                fila.Add(cuenta.validador.nombre);
                fila.Add(cuenta.certificador.nombre);
                fila.Add(estadoStr);

                lista.Add(fila);
            }

            //string archivo = modelReporte.GenerarExcel(lista);
            string path = HostingEnvironment.ApplicationPhysicalPath + "Archivos\\";
            string file = StringHelper.HashUnico("Excel_", ".xlsx");
            System.IO.File.Copy(path + "Plantillas\\Plantilla_Reporte1.xlsx", path + "Temporal\\" + file, true);
            modelReporte.GenerarReporte1(path + "Temporal\\" + file, lista);

            byte[] contenido = GetFile(path + "Temporal\\" + file);;
            
            return File(contenido, "application/force-download", "CuentasActivas-" + DateTime.Now.ToShortDateString() + ".xlsx");
            //}
            //else
            //{
            //    switch (usuarioId)
            //    {
            //        case -1:
            //            return Json(new { response = "error", message = "Nombre de Usuario o password Incorrecto" }, JsonRequestBehavior.AllowGet);
            //        case -2:
            //            return Json(new { response = "error", message = "No se ha podido establecer una conexion con el Servidor" }, JsonRequestBehavior.AllowGet);
            //    }
            //    return Json(new { response = "error", message = "Error Desconocido" }, JsonRequestBehavior.AllowGet);
            //}
        }

        private byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        private int[] validarArreglo(string arr)
        {
            if (arr.Equals("-1"))
            {
                return new int[0];
            }
            else
            {
                if (arr.Contains(","))
                {
                    
                    string[] s = arr.Split(',');
                    int[] n = new int[s.Count()];
                    for (int i = 0; i < s.Count(); i++)
                    {
                        n[i] = int.Parse(s[i]);
                    }
                    return n;
                }
                else
                {
                    int[] n = new int[1];
                    n[0] = int.Parse(arr);
                    return n;
                }
            }
        }

    }
}