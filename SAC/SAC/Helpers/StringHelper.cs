using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Helpers
{
    public class StringHelper
    {
        public static string DosNumeros(object o)
        {
            string s = o.ToString();
            if (s.Length < 2)
            {
                s = "0" + s;
            }
            return s;
        }

        public static int ContarCaracteres(object string1, char caracter)
        {
            string s = string1.ToString();
            s = s.Replace(caracter.ToString(), "");

            return string1.ToString().Length - s.Length;
        }

        public static int ContarCaracteres(object string1, string string2)
        {
            string s = string1.ToString();
            s = s.Replace(string2, "");

            return (string1.ToString().Length - s.Length) / string2.Length;
        }
        public static string extension(string archivo)
        {
            if (archivo.Contains("."))
            {
                string s = archivo.Substring(archivo.LastIndexOf(".") + 1);
                return s;
            }
            else
            {
                return archivo;
            }

        }
        public static string fechaDMA(DateTime fecha)
        {
            return DosNumeros(fecha.Day) + "-" + DosNumeros(fecha.Month) + "-" + fecha.Year;

        }

        public static string StripTags(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static string HashUnico(string prepend = "", string append = "")
        {
            DateTime now = DateTime.Now;
            return prepend + Math.Abs(now.Year * 365 * 24 * 60 * 60 + now.Month * 30 * 24 * 60 * 60 + now.Day * 24 * 60 * 60 + now.Hour * 60 * 60 + now.Minute * 60 + now.Second) + append;
        }

    }
}