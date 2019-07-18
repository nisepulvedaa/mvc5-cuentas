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
    public class CuentasController : Controller
    {
        // GET: Cuentas
        public ActionResult Asignacion(int año = 0, int mes = 0)
        {
            if (SessionHandler.Logged)
            {
                System.Diagnostics.Debug.WriteLine("=============" + año + " - " + mes);
                if (año == 0)
                {
                    año = DateTime.Now.Year;
                    mes = DateTime.Now.Month;
                }

                var modelCuenta = new Models.CuentaActivaModel();
                var modelMantenedor = new Models.MantenedorModel();
                ViewBag.PageTitle = "Mantenedor";
                ViewBag.UsuarioNombre = SessionHandler.Usuario;
                ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                var años = modelCuenta.obtenerAñosDistintos();

                string html = "";
                foreach (var a in años)
                {
                    html += "<option value='" + a + "'>" + a + "</option>";
                }

                html += "<option value='" + DateTime.Now.Year.ToString() + "' selected >" + DateTime.Now.Year.ToString() + "</option>";
                ViewBag.AñoSelect = html;
                ViewBag.MesSelect = DateTime.Now.Month.ToString();

                html = "";
                //var cuentas = modelCuenta.obtenerCuentasActivasPorFecha(DateTime.Now.Year, DateTime.Now.Month);
                var cuentas = modelCuenta.obtenerCuentasActivasPorFecha(año, mes);
                foreach (var cuenta in cuentas)
                {
                    var htmltr = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>";
                    var analista = "<div id=\"tcuentas_an_{0}\" uid=\"{1}\">{2}</div>";
                    var validador = "<div id=\"tcuentas_va_{0}\" uid=\"{1}\">{2}</div>";
                    var certificador = "<div id=\"tcuentas_ce_{0}\" uid=\"{1}\">{2}</div>";
                    var total = "<div id=\"tcuentas_total_{0}\">{1}</div>";
                    var dias = "<div id=\"tdias_{0}\">{1}</div>";
                    var botones = "<a href='#' id='btnEditarCA_{0}' class='btn btn-info' onclick='EditarCuenta({0})'>Editar</a>";
                    botones += "   <a href='#' id='btnEliminarCA_{0}'class='btn btn-info' onclick='EliminarCuenta({0}, \"{1}\")'>Eliminar</a>";
                    htmltr = string.Format(htmltr,
                        cuenta.empresa.razonSocial,
                        cuenta.numero,
                        string.Format(total, cuenta.cuentaActivaId, cuenta.cuentaActivaTotal),
                        string.Format(analista, cuenta.cuentaActivaId, cuenta.analista.usuarioId, cuenta.analista.nombre),
                        string.Format(validador, cuenta.cuentaActivaId, cuenta.validador.usuarioId, cuenta.validador.nombre),
                        string.Format(certificador, cuenta.cuentaActivaId, cuenta.certificador.usuarioId, cuenta.certificador.nombre),
                        string.Format(dias, cuenta.cuentaActivaId, cuenta.cuentaActivaDiasPlazo),
                        string.Format(botones, cuenta.cuentaActivaId, cuenta.numero)
                        );
                    html += htmltr;
                }
                ViewBag.TablaCuentas = html;

                html = "";
                var usuarios = modelMantenedor.obtenerUsuarios();
                foreach (var usuario in usuarios)
                {
                    html += string.Format("<option value='{0}'>{1}</option>", usuario.usuarioId, usuario.nombre);
                }
                ViewBag.UsuariosSelect = html;

                html = "";
                var empresas = modelMantenedor.obtenerEmpresas();
                foreach (var empresa in empresas)
                {
                    html += string.Format("<option value='{0}'>{1}</option>", empresa.empresaId, empresa.razonSocial);
                }
                ViewBag.EmpresasSelect = html;



                html = "";
                //var cuentasnoactivas = modelCuenta.obtenerCuentasNoActivas(DateTime.Now.Year, DateTime.Now.Month, -1, -1, -1);
                var cuentasnoactivas = modelCuenta.obtenerCuentasNoActivas(año, mes, -1, -1, -1);
                foreach (var cuenta in cuentasnoactivas)
                {
                    var str = "<div class='col-sm-6' id='cuentaHolder_{4}'><table class='cuentasTable'><tr>";
                    str += "<td><input type='checkbox' class='chkcuenta md-check' cid='{4}' eid='{5}' rid='{6}' gid='{7}'></td>";
                    str += "<td><div>{3} ({8})</div><div class='smtext'>Empresa: {0} Rubro: {1} Grupo: {2} </div></td>";
                    //str += "</tr>";
                    //str += "<td></td>";
                    //str += "<td>{3}</td>";
                    str += "</tr></table></div>";
                    html += string.Format(str,
                        cuenta.empresa.razonSocial,
                        cuenta.rubro.rubroNombre,
                        cuenta.grupo.grupoNombre,
                        cuenta.nombre,
                        cuenta.id,
                        cuenta.empresa.empresaId,
                        cuenta.rubro.rubroId,
                        cuenta.grupo.grupoId,
                        cuenta.numero
                        );
                }
                ViewBag.cuentasNoActivas = html;



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


                return View();

            }
            else
            {
                return Redirect("~/Login/Index");
            }
        }

        public ActionResult AsignacionTable(int año, int mes)
        {
            var modelCuenta = new Models.CuentaActivaModel();
            var modelMantenedor = new Models.MantenedorModel();
            var lista = new List<List<string>>();

            var cuentas = modelCuenta.obtenerCuentasActivasPorFecha(año, mes);
            foreach (var cuenta in cuentas)
            {
                var fila = new List<string>();

                var htmltr = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>";
                var analista = "<div id=\"tcuentas_an_{0}\" uid=\"{1}\">{2}</div>";
                var validador = "<div id=\"tcuentas_va_{0}\" uid=\"{1}\">{2}</div>";
                var certificador = "<div id=\"tcuentas_ce_{0}\" uid=\"{1}\">{2}</div>";
                var total = "<div id=\"tcuentas_total_{0}\">{1}</div>";
                var dias = "<div id=\"tdias_{0}\">{1}</div>";
                var botones = "<a href='#' id='btnEditarCA_{0}' class='btn btn-info' onclick='EditarCuenta({0})'>Editar</a>";
                botones += "   <a href='#' id='btnEliminarCA_{0}'class='btn btn-info' onclick='EliminarCuenta({0}, \"{1}\")'>Eliminar</a>";

                fila.Add(cuenta.empresa.razonSocial);
                fila.Add(cuenta.numero);
                fila.Add(string.Format(total, cuenta.cuentaActivaId, cuenta.cuentaActivaTotal));
                fila.Add(string.Format(analista, cuenta.cuentaActivaId, cuenta.analista.usuarioId, cuenta.analista.nombre));
                fila.Add(string.Format(validador, cuenta.cuentaActivaId, cuenta.validador.usuarioId, cuenta.validador.nombre));
                fila.Add(string.Format(certificador, cuenta.cuentaActivaId, cuenta.certificador.usuarioId, cuenta.certificador.nombre));
                fila.Add(string.Format(dias, cuenta.cuentaActivaId, cuenta.cuentaActivaDiasPlazo));
                fila.Add(string.Format(botones, cuenta.cuentaActivaId, cuenta.numero));
                lista.Add(fila);
            }

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RecargarCuentasNoActivas(int año, int mes)
        {
            var modelCuenta = new Models.CuentaActivaModel();
            var cuentasnoactivas = modelCuenta.obtenerCuentasNoActivas(año, mes, -1, -1, -1);
            var html = "";
            foreach (var cuenta in cuentasnoactivas)
            {
                var str = "<div class='col-sm-6' id='cuentaHolder_{4}'><table class='cuentasTable'><tr>";
                str += "<td><input type='checkbox' class='chkcuenta md-check' cid='{4}' eid='{5}' rid='{6}' gid='{7}'></td>";
                str += "<td><div>{3} ({8})</div><div class='smtext'>Empresa: {0} Rubro: {1} Grupo: {2} </div></td>";
                str += "</tr></table></div>";
                html += string.Format(str,
                    cuenta.empresa.razonSocial,
                    cuenta.rubro.rubroNombre,
                    cuenta.grupo.grupoNombre,
                    cuenta.nombre,
                    cuenta.id,
                    cuenta.empresa.empresaId,
                    cuenta.rubro.rubroId,
                    cuenta.grupo.grupoId,
                    cuenta.numero
                    );
            }
            return Json(new { data = html }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditarCuentaActiva(int cuentaId, int analista, int validador, int certificador, int diasPlazo, int total, int yyyy, int mm)
        {

            var modelCuenta = new Models.CuentaActivaModel();
            if (modelCuenta.AsignarCuentaActiva(cuentaId, analista, validador, certificador, diasPlazo, total, new DateTime(yyyy, mm, 1)))
            {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de actualizar la informacion..." }, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult AgregarCuentasActivas(int[] cuentasId, int analista, int validador, int certificador, int diasPlazo, int total, int yyyy, int mm)
        {
            var sinErrores = true;
            var modelCuenta = new Models.CuentaActivaModel();
            //System.Diagnostics.Debug.WriteLine("begin=============");
            foreach (var cuenta in cuentasId)
            {
                if (!modelCuenta.IngresarCuentaActiva(cuenta, analista, validador, certificador, diasPlazo, SessionHandler.UsuarioId, total, new DateTime(yyyy, mm, 1)))
                {
                    sinErrores = false;
                }
            }
            if (sinErrores)
            {
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "Ocurrio un Error al tratar de insertar la informacion..." }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Eliminar(int cuentaId, int yyyy, int mm)
        {
            var modelCuenta = new CuentaActivaModel();
            if (modelCuenta.ObtenerNumeroDeArchivosPorCuentaActiva(cuentaId, yyyy, mm) == 0)
            {
                if (modelCuenta.EliminarCuentaActiva(cuentaId, new DateTime(yyyy, mm, 1)))
                {
                    return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { response = "error", message = "Ocurrio un Error al tratar de Eliminar la Cuenta Activa..." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { response = "error", message = "No se puede eliminar la Cuenta Activa, Porque tiene Archivos Asociados..." }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        public ActionResult procesarExcel(string archivo)
        {
            var modelExcel = new Models.ExcelModel();
            var modelMantenedor = new Models.MantenedorModel();
            var modelCuenta = new Models.CuentaActivaModel();
            var selectEmpresas = "";
            var empresas = modelMantenedor.obtenerEmpresas();
            var selectUsuarios = "";
            var usuarios = modelMantenedor.obtenerUsuarios();

            var cuentas = modelMantenedor.obtenerCuentas();
            var selectCuentas = "";

            foreach (Cuenta fila in cuentas)
            {
                selectCuentas += "<option value='" + fila.id + "'>" + fila.numero + "</option>";
            }
            foreach (Empresa fila in empresas)
            {
                selectEmpresas += "<option value='" + fila.empresaId + "'>" + fila.razonSocial + "</option>";
            }
            foreach (Usuario fila in usuarios)
            {
                selectUsuarios += "<option value='" + fila.usuarioId + "'>" + fila.nombre + "</option>";
            }
            List<CuentaActiva> cuentasActivas;
            cuentasActivas = modelExcel.ObtenerCuentasActivasDesdeArchivo(HostingEnvironment.ApplicationPhysicalPath + "Archivos\\Temporal\\" + archivo);
            string html = "";
            int contador = 0;
            bool errores = false;
            foreach (CuentaActiva cuenta in cuentasActivas)
            {
                contador++;
                CuentaActiva c = modelCuenta.IngresarActualizarAsignacionCuenta(cuenta, SessionHandler.UsuarioId);

                if (c.cuentaActivaId == -1)
                {
                    html += "<tr class='danger' id='" + contador + "'>";
                    errores = true;
                }
                else
                {
                    html += "<tr>";
                }
                //html += "<td>" + contador + "</td>";
                html += "<td>";
                var htmlcuenta = "";
                if (c.empresa.empresaId == -1)
                {
                    html += "<select id='ReprocesarEmpresa' name='empresas' class='form-control' onchange='CargarCuentasPorEmpresa()'>";
                    html += "<option value=''>" + c.empresa.razonSocial + " (Valor Erroneo) </option>" + selectEmpresas;
                    html += "</select>";
                    htmlcuenta += "<select id='ReprocesarCuenta' name='cuentas' class='form-control' >";
                    htmlcuenta += "<option value=''> Seleccione... </option>";
                    htmlcuenta += "</select>";
                }
                else
                {
                    html += c.empresa.razonSocial;
                    if (c.id != -1)
                    {
                        htmlcuenta = c.numero;
                    }
                    else
                    {
                        var cuentasEmpresa = modelMantenedor.ObtenerCuentasPorEmpresa(c.empresa.empresaId);
                        htmlcuenta += "<select name='cuentas' class='form-control' >";
                        htmlcuenta += "<option value=''>" + c.numero + " (Valor Erroneo) </option>";
                        foreach (Cuenta fila in cuentasEmpresa)
                        {
                            htmlcuenta += "<option value='" + fila.id + "'>" + fila.numero + "</option>";
                        }
                        htmlcuenta += "</select>";
                    }
                }
                html += "</td>";
                html += "<td>" + htmlcuenta + "</td>";
                html += "<td>" + c.cuentaActivaTotal + "</td>";
                html += "<td>";
                if (c.analista.usuarioId == -1)
                {
                    html += "<select name='analista' class='form-control' >";
                    html += "<option value=''>" + c.analista.nombre + " (Valor Erroneo) </option>" + selectUsuarios;
                    html += "</select>";
                }
                else
                {
                    html += c.analista.nombre;
                }
                html += "</td>";
                html += "<td>";
                if (c.validador.usuarioId == -1)
                {
                    html += "<select name='validador' class='form-control' >";
                    html += "<option value=''>" + c.validador.nombre + " (Valor Erroneo) </option>" + selectUsuarios;
                    html += "</select>";
                }
                else
                {
                    html += c.validador.nombre;
                }
                html += "</td>";
                html += "<td>";
                if (c.certificador.usuarioId == -1)
                {
                    html += "<select name='certificador' class='form-control' >";
                    html += "<option value=''>" + c.certificador.nombre + " (Valor Erroneo) </option>" + selectUsuarios;
                    html += "</select>";
                }
                else
                {
                    html += c.certificador.nombre;
                }
                html += "</td>";
                html += "<td>" + c.cuentaActivaDiasPlazo + "</td>";

                html += "<td>" + StringHelper.DosNumeros(c.cuentaActivaFecha.Month) + "/" + c.cuentaActivaFecha.Year + "</td>";

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
            var modelCuentas = new Models.CuentaActivaModel();

            List<FilaJson> lista = new List<FilaJson>();
            foreach (string[] fila in data)
            {

                string empresa = fila[0];
                string nroCuenta = fila[1];
                string total = fila[2];
                string analista = fila[3];
                string validador = fila[4];
                string certificador = fila[5];
                var dias = fila[6];
                string[] fecha = fila[7].Split('/');
                int mm = int.Parse(fecha[0]);
                int yyyy = int.Parse(fecha[1]);
                string id = fila[8];

                CuentaActiva ca = new CuentaActiva();
                ca.numero = nroCuenta;
                ca.cuentaActivaTotal = int.Parse(total);
                ca.cuentaActivaDiasPlazo = int.Parse(dias);
                ca.cuentaActivaFecha = new DateTime(yyyy, mm, 1);

                ca.empresa = new Empresa();
                ca.empresa.razonSocial = empresa;

                ca.analista = new Usuario();
                ca.analista.nombre = analista;

                ca.validador = new Usuario();
                ca.validador.nombre = validador;

                ca.certificador = new Usuario();
                ca.certificador.nombre = certificador;

                var r = modelCuentas.IngresarActualizarAsignacionCuenta(ca, SessionHandler.UsuarioId);
                lista.Add(new FilaJson { fila = id, resultado = r.insertUpdate.ToString() });
            }


            return Json(new { response = "success", resultados = Json(lista) }, JsonRequestBehavior.AllowGet);



        }


        [HttpPost]
        public ActionResult ObtenerCuentasPorEmpresa(int empresaId)
        {

            var modelMantenedor = new Models.MantenedorModel();
            var cuentas = modelMantenedor.ObtenerCuentasPorEmpresa(empresaId);
            var html = "";

            foreach (var cuenta in cuentas)
            {
                html += string.Format("<option value='{0}'>{1}</option>", cuenta.id, cuenta.numero);
            }

            return Json(new { response = "success", resultados = html }, JsonRequestBehavior.AllowGet);

        }


        class FilaJson
        {
            public string fila { get; set; }
            public string resultado { get; set; }
        }
        class nameValue
        {
            public string name { get; set; }
            public string value { get; set; }
        }





    }
}