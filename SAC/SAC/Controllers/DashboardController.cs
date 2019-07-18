using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAC.Helpers;
using SAC.Models;
using SAC.Models.DTO;

namespace SAC.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (SessionHandler.Logged)
            {
                if (SessionHandler.pwdEstado)
                {
                    ViewBag.PageTitle = "Dashboard";
                    ViewBag.UsuarioNombre = SessionHandler.Usuario;
                    ViewBag.Menu = MenuHelper.menuPorPerfil(SessionHandler.Perfil);

                    EtapasModel modelEtapas = new EtapasModel();

                    List<CuentaActiva> cuentas = new List<CuentaActiva>();

                    if (SessionHandler.Perfil >= 3)
                    {
                        switch (SessionHandler.Perfil)
                        {
                            case 3:
                                cuentas = modelEtapas.obtenerCuentasActivas(DateTime.Today.Month, DateTime.Today.Year, SessionHandler.UsuarioId);
                                break;
                            case 4:
                                cuentas = modelEtapas.obtenerCuentasActivas(DateTime.Today.Month, DateTime.Today.Year, -1, SessionHandler.UsuarioId);
                                break;
                            case 5:
                                cuentas = modelEtapas.obtenerCuentasActivas(DateTime.Today.Month, DateTime.Today.Year, -1, -1, SessionHandler.UsuarioId);
                                break;
                        }
                    }
                    else
                    {
                        cuentas = modelEtapas.obtenerCuentasActivas(DateTime.Today.Month, DateTime.Today.Year);
                    }

                    string html = "";

                    foreach (CuentaActiva cuenta in cuentas)
                    {

                        string tr = "<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td></tr>";
                        int estado = modelEtapas.ObtenerCuentaActivaEstadoLog(cuenta.cuentaActivaId, cuenta.cuentaActivaFecha.Month, cuenta.cuentaActivaFecha.Year);
                        string estadoStr = "";
                        string descripcion = "";
                        if (estado == 0) {
                            estado = modelEtapas.ObtenerCuentaActivaEstadoParaEstado0(cuenta.cuentaActivaId, cuenta.cuentaActivaFecha.Month, cuenta.cuentaActivaFecha.Year);
                        }
                        switch (estado)
                        {
                            case -1:
                                estadoStr = "<b>0%</b>";
                                descripcion = "Asignado (Sin Movimientos)";
                                break;
                            case 0:
                                estadoStr = "<b style='color: #993333'>25%</b>";
                                descripcion = "En Borrador";
                                break;
                            case 1:
                                estadoStr = "<b style='color: #993333'>25%</b>";
                                descripcion = "Rechazado desde Validaci&oacute;n";
                                break;
                            case 2:
                                estadoStr = "<b style='color: #ff0000;'>50%</b>";
                                descripcion = "Enviado a Validaci&oacute;n";
                                break;
                            case 3:
                                estadoStr = "<b style='color: #ff9933;'>75%</b>";
                                descripcion = "Enviado a Certificaci&oacute;n";
                                break;
                            case 4:
                                estadoStr = "<b style='color: #ff0000;'>50%</b>";
                                descripcion = "Rechazado desde Certificaci&oacute;n";
                                break;
                            case 5:
                                estadoStr = "<b style='color: #66ff33;'>100%</b>";
                                descripcion = "Certificado";
                                break;
                        }
                        html += string.Format(tr,
                            cuenta.empresa.razonSocial,
                            cuenta.numero,
                            StringHelper.fechaDMA(cuenta.cuentaActivaFecha),
                            cuenta.cuentaActivaDiasPlazo,
                            descripcion,
                            cuenta.analista.nombre,
                            cuenta.validador.nombre,
                            cuenta.certificador.nombre,
                            estadoStr
                            );
                    }


                    ViewBag.Table = html;

                    return View();
                }
                else
                {
                    return Redirect("~/Perfil/Index");
                }

            }
            else
            {
                return Redirect("~/Login/Index");
            }
        }
    }
}