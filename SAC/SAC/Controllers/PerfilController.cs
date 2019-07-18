using SAC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAC.Controllers
{
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Index()
        {

            if (SessionHandler.Logged)
            {
                return View();
            }
            else
            {
                return Redirect("~/Login/Index");
            }
        }

        public ActionResult Recuperar()
        {
            return View();
        }


        public ActionResult Recuperar2(string validation)
        {
            var modelMantenedor = new Models.MantenedorModel();
            try
            {
                System.Diagnostics.Debug.WriteLine(validation);
                string val = EncryptHelper.Decrypt(validation);
                if (val.Contains("¬"))
                {
                    string[] data = val.Split('¬');
                    if (data[0].Equals("email"))
                    {

                        string usuarioEmail = data[1];
                        Models.DTO.Usuario usuario = modelMantenedor.ObtenerUsuarioPorEmail(usuarioEmail);

                        string usuarioNombre = "";
                        int usuarioId = 0;

                        usuarioNombre = usuario.nombre;
                        usuarioId = usuario.usuarioId;

                        string username = usuarioEmail;
                        var rnd = new Random(DateTime.Now.Millisecond);
                        int ticks = rnd.Next(111111111, 999999999);

                        string pwd = ticks.ToString();


                        if (modelMantenedor.EditarClave(usuarioId, pwd, usuarioId))
                        {
                            string subject = "Credenciales para accedo al sistema SAC";
                            string body = "Su clave de acceso es: " + pwd;

                            //string enc = EncryptHelper.Encrypt("email¬" + usuarioEmail);

                            //body += "<br><a href='http://localhost:21698/Perfil/Recuperar2?validation=" + enc + "'>http://localhost:21698/Perfil/Recuperar</a>";

                            MailHelper.mail(usuarioNombre, usuarioEmail, subject, body);
                            //return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            //return Json(new { response = "error", message = "Ocurrio un Error al tratar de ingresar el usuario..." }, JsonRequestBehavior.AllowGet);
                        }
                    }

                }



            }
            catch (Exception)
            {


            }


            return View();
        }


        public ActionResult RecuperarPassword(string usuarioEmail)
        {
            var modelMantenedor = new Models.MantenedorModel();

            Models.DTO.Usuario usuario = modelMantenedor.ObtenerUsuarioPorEmail(usuarioEmail);


            // string  usuarioNombre = "";
            // int usuarioId = 0;


            string usuarioNombre = usuario.nombre;
            int usuarioId = usuario.usuarioId;


            // string username = usuarioEmail;
            // var rnd = new Random(DateTime.Now.Millisecond);
            // int ticks = rnd.Next(111111111, 999999999);

            // string pwd = ticks.ToString();


            //if (modelMantenedor.EditarClave(usuarioId, pwd, usuarioId))
            if (usuario.usuarioId > 0)
            {
                string subject = "Confirmacion de Reseteo de Password, Sistema SAC";
                string body = "Por Favor siga el siguiente link para resetear su password."; //"Su clave de acceso es: " + pwd;

                string enc = HttpUtility.UrlEncode(EncryptHelper.Encrypt("email¬" + usuarioEmail));
                string baseURL = ConfigHelper.getString("baseURL");
                System.Diagnostics.Debug.WriteLine(enc);
                //System.Diagnostics.Debug.WriteLine(EncryptHelper.Decrypt(enc));

                body += "<br><br><a href='" + baseURL + "Perfil/Recuperar2?validation=" + enc + "' target='_blank'>";
                body += baseURL + "Perfil/Recuperar2?validation=" + enc + "</a>";
                body += "<br><br>No responda este e-mail, si usted no ha solicitado un cambio de clave, porfavor ignore este e-mail";

                MailHelper.mail(usuarioNombre, usuarioEmail, subject, body);

            }

            return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult CambiarPassword(string pwd)
        {
            var modelLogin = new Models.LoginModel();


            if (modelLogin.CambiarPassword(SessionHandler.UsuarioId, pwd))
            {
                SessionHandler.pwdEstado = true;
                return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { response = "error", message = "ha ocurrido un error al actualizar el password" }, JsonRequestBehavior.AllowGet);
            }

        }

    }


}