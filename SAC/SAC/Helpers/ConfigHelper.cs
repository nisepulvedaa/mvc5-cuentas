using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SAC.Helpers
{
    public class ConfigHelper
    {
        public static string getString(string key) {
            return ConfigurationManager.AppSettings[key].ToString();
        }
        public static int getInt(string key)
        {
            return int.Parse(ConfigurationManager.AppSettings[key].ToString());
        }
        public static bool getBool(string key)
        {
            return Convert.ToBoolean(ConfigurationManager.AppSettings[key].ToString());
        }
    }
}