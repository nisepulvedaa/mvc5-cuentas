using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Helpers
{
    public abstract class MenuHelper
    {
        private static string controller;
        private static string action;

        public static string menuPorPerfil(int perfilId)
        {
            string html = "";
            controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();

            html += MenuItem("Dashboard/Index", "Dashboard", "fa fa-cog", "Dashboard");
            switch (perfilId)
            {
                case 1:
                    html += MenuItem("Cuentas/Asignacion", "Asignación de Cuenta", "fa fa-cog", "Cuentas/Asignacion");
                    html += MenuItem("Reportes/Index", "Reportería", "fa fa-cog", "Reportes/Index");
                    html += MenuItemSubmenu("Mantenedor", "Mantenedores", "fa fa-database",
                        SubMenuItem("Mantenedor/Empresas", "Empresas", "fa fa-cog", "Mantenedor/Empresas")
                        + SubMenuItem("Mantenedor/Rubros", "Rubros", "fa fa-cog", "Mantenedor/Rubros")
                        + SubMenuItem("Mantenedor/Grupos", "Grupos", "fa fa-cog", "Mantenedor/Grupos")
                        + SubMenuItem("Mantenedor/Cuentas", "Cuentas", "fa fa-cog", "Mantenedor/Cuentas")
                        + SubMenuItem("Mantenedor/Usuarios", "Usuarios", "fa fa-user", "Mantenedor/Usuarios")
                
                        );
                    break;
                case 2:
                    html += MenuItem("Reportes/Index", "Reportería", "fa fa-cog", "Reportes/Index");
                    break;
                case 3:
                    html += MenuItem("Analisis/Index", "Análisis", "fa fa-cog", "Analisis/Index");
                    break;
                case 4:
                    html += MenuItem("Validacion/Index", "Validación", "fa fa-cog", "Validacion/Index");
                    break;
                case 5:
                    html += MenuItem("Certificacion/Index", "Certificación", "fa fa-cog", "Certificacion/Index");
                    break;
            }
            
            return html;
        }



        private static string MenuItemSubmenu(string contexto, string titulo, string icono, string submenu)
        {
            string html = "";
            if (contexto == controller)
            {
                html += "<li class=\"start active open\">";
            }
            else
            {
                html += "<li>";
            }
            html += "<a href=\"javascript:;\" ><i class=\"{0}\"></i><span class=\"title\"> {1}</span>";
            html += "<span class=\"selected\"></span><span class=\"arrow open\"></span></a>";

            if (submenu.Length > 0)
            {
                html += "<ul class=\"sub-menu\">" + submenu + "</ul>";
            }

            html += "</li>";

            return String.Format(html, icono, titulo);
        }

        private static string SubMenuItem(string contexto, string titulo, string icono, string link)
        {
            link = setLinkAuthority(link);
            string html = "";
            if (contexto == controller + "/" + action)
            {
                html += "<li class=\"active\">";
            }
            else
            {
                html += "<li>";
            }
            html += "<a href=\"{0}\"><i class=\"{1}\"></i> {2}</a></li>";
            return String.Format(html, link, icono, titulo);
        }

        private static string SubMenuItemTooltip(string contexto, string titulo, string icono, string link, string tooltip)
        {
            link = setLinkAuthority(link);
            string html = "";
            if (contexto == controller + "/" + action)
            {
                html += "<li class=\"active tooltips\" data-original-title=\"{0}\" data-html=\"true\" data-placement=\"right\" data-container=\"body\">";
            }
            else
            {
                html += "<li class=\"tooltips\" data-original-title=\"{0}\" data-html=\"true\" data-placement=\"right\" data-container=\"body\">";
            }
            html += "<a href=\"{1}\"><i class=\"{2}\"></i> {3}</a></li>";

            html = String.Format(html, tooltip, link, icono, titulo);

            return html;
        }

        private static string MenuItem(string contexto, string titulo, string icono, string link)
        {
            link = setLinkAuthority(link);
            //System.Diagnostics.Debug.WriteLine("[" + contexto + "|" + controller + "/" + action + "]");
            string html = "";
            if (contexto == controller + "/" + action)
            {
                html += "<li class=\"active\">";
            }
            else
            {
                html += "<li>";
            }
            html += "<a href=\"{0}\"><i class=\"{1}\"></i><span class=\"title\"> {2}</span></a></li>";
            html = String.Format(html, link, icono, titulo);
            return html;
        }

        private static string MenuItemTooltip(string contexto, string titulo, string icono, string link, string tooltip)
        {
            link = setLinkAuthority(link);
            string html = "";
            if (contexto == controller + "/" + action)
            {
                html += "<li class=\"active tooltips\" data-original-title=\"{0}\" data-html=\"true\" data-placement=\"right\" data-container=\"body\">";
            }
            else
            {
                html += "<li class=\"tooltips\" data-original-title=\"{0}\" data-html=\"true\" data-placement=\"right\" data-container=\"body\">";
            }
            html += "<a href=\"{1}\"><i class=\"{2}\"></i><span class=\"title\"> {3}</span></a></li>";

            html = String.Format(html, tooltip, link, icono, titulo);

            return html;
        }

        private static string MenuHeader(string titulo)
        {
            return String.Format("<li class=\"heading\"><h3 class=\"uppercase\">{0}</h3></li>", titulo);
        }

        private static string setLinkAuthority(string link)
        {
            return "http://" + HttpContext.Current.Request.Url.Authority + "/" + link;
        }

    }
}