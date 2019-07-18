using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC
{
    public abstract class SessionHandler
    {
        
        public static bool Logged
        {
            get
            {
                if (HttpContext.Current.Session["logged"] != null)
                {
                    return (bool)HttpContext.Current.Session["logged"];
                }
                return false;
            }
            set
            {
                HttpContext.Current.Session["logged"] = value;
            }
        } 
        public static string Usuario
        {
            get
            {
                return (string)HttpContext.Current.Session["usuario"];
            }
            set
            {
                HttpContext.Current.Session["usuario"] = value;
            }
        }
        public static int UsuarioId
        {
            get
            {
                return (int)HttpContext.Current.Session["usuarioid"];
            }
            set
            {
                HttpContext.Current.Session["usuarioid"] = value;
            }
        }
        public static string Mail
        {
            get
            {
                return (string)HttpContext.Current.Session["mail"];
            }
            set
            {
                HttpContext.Current.Session["mail"] = value;
            }
        }
        public static int Perfil
        {
            get
            {
                return (int)HttpContext.Current.Session["perfil"];
            }
            set
            {
                HttpContext.Current.Session["perfil"] = value;
            }
        }
        public static int EmpresaId
        {
            get
            {
                return (int)HttpContext.Current.Session["empresaid"];
            }
            set
            {
                HttpContext.Current.Session["empresaid"] = value;
            }
        }

        public static bool pwdEstado
        {
            get
            {
                return (bool)HttpContext.Current.Session["pwdestado"];
            }
            set
            {
                HttpContext.Current.Session["pwdestado"] = value;
            }
        }
    }
}