using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models
{
    public class LoginModel
    {
        private readonly Models.Resources.SACModel db = new Models.Resources.SACModel();

        public int Login(string usuario, string password) {
            int result = -1;
            try
            {
                result = ((int)db.SACLogin(usuario, password).FirstOrDefault());
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine("SAC.Models.LoginModel (Login): " + ex.Message);
                result = -2;
            }
            
            return result;
        }

        public bool CambiarPassword(int usuarioId, string password)
        {
            try
            {
                db.ActualizarPassword(usuarioId, password);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SAC.Models.LoginModel (CambiarPassword): " + ex.Message);
                return false;
            }
        }

    }
}