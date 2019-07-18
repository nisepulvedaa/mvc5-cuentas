using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAC.Helpers;
using System.Web.Hosting;
using SAC.Models.Excel;

namespace SAC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            SessionHandler.Logged = false;
            SessionHandler.Usuario = "";
            SessionHandler.UsuarioId = -1;
            SessionHandler.Mail = "";
            SessionHandler.Perfil = -1;
            SessionHandler.EmpresaId = -1;

            //var excel = new Models.Resources.ExcelConnector("D:\\test.xlsx");
            //excel.test();
            //System.Diagnostics.Debug.WriteLine(ConfigHelper.getString("MailHost"));
            //MailHelper.mail("cgg", "cgajardo@brains.cl", "asd", "f<b>g</b>h");
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {

            var modelLogin = new Models.LoginModel();
            var modelMantenedores = new Models.MantenedorModel();

            var usuarioId = modelLogin.Login(user, pass);
            

            if (usuarioId >= 0)
            {
                Models.DTO.Usuario usuario = modelMantenedores.ObtenerUsuarioPorId(usuarioId);
                SessionHandler.Logged = true;
                SessionHandler.Usuario = usuario.nombre;
                SessionHandler.UsuarioId = usuarioId;
                SessionHandler.Mail = usuario.email;
                SessionHandler.Perfil = usuario.perfil.Id ;
                SessionHandler.EmpresaId = usuario.empresa.empresaId ;
                SessionHandler.pwdEstado = usuario.pwdEstado;
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                switch (usuarioId) {
                    case -1:
                        return Json(new { response = "error", message = "Nombre de usuario o password incorrecto" }, JsonRequestBehavior.AllowGet);
                    case -2:
                        return Json(new { response = "error", message = "No se ha podido establecer una conexion con el servidor" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { response = "error", message = "Error Desconocido" }, JsonRequestBehavior.AllowGet);
            }

            
        }
    }
}