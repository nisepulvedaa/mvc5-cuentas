using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SAC.Helpers
{
    public class MailHelper
    {
        public static bool LoadFromWebConfig = true;
        public static string Host; // = "smtp.gmail.com";
        public static string Usuario;// = "pruebasti@brains.cl";
        public static string Password;// = "testtinew885";
        public static int Port;// = 25;
        public static bool Html;// = true;
        public static string NombreEnvio;// = "brains";
        public static string MailEnvio;// = "noreply@brains.cl";
    //<add key = "MailHost" value="smtp.gmail.com"/>
    //<add key = "MailUsuario" value="pruebasti@brains.cl"/>
    //<add key = "MailPassword" value="testtinew885"/>
    //<add key = "MailPort" value="25"/>
    //<add key = "MailHtml" value="true"/>
    //<add key = "MailNombreEnvio" value="brains"/>
    //<add key = "MailMailEnvio" value="noreply@brains.cl"/>

        public static void mail(string nombre, string email, string subject, string body)
        {
            if (MailHelper.LoadFromWebConfig)
            {
                MailHelper.Host = ConfigHelper.getString("MailHost");
                MailHelper.Usuario = ConfigHelper.getString("MailUsuario");
                MailHelper.Password = ConfigHelper.getString("MailPassword");
                MailHelper.Port = ConfigHelper.getInt("MailPort");
                MailHelper.Html = ConfigHelper.getBool("MailHtml");
                MailHelper.NombreEnvio = ConfigHelper.getString("MailNombreEnvio");
                MailHelper.MailEnvio = ConfigHelper.getString("MailMailEnvio");
            }
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(MailHelper.MailEnvio, MailHelper.NombreEnvio);
            mail.Sender = new MailAddress(MailHelper.MailEnvio, MailHelper.NombreEnvio);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = MailHelper.Html;

            mail.To.Add(new MailAddress(email, nombre));

            var smtp = getSMTP();

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                ex.HelpLink = "Error al enviar el mensaje";
                throw;
            }
        }



        private static SmtpClient getSMTP()
        {
            var smtp = new SmtpClient { Host = MailHelper.Host, EnableSsl = true };
            var networkCred = new NetworkCredential { UserName = MailHelper.Usuario, Password = MailHelper.Password };
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = networkCred;
            smtp.Port = MailHelper.Port;
            return smtp;
        }

    }
}