using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Helpers
{
    public class ListaOrdenablePorFecha
    {
        private class ListaOrdenablePorFechaItem
        {
            public DateTime Fecha;
            public object Objeto;

            public ListaOrdenablePorFechaItem(DateTime fecha, object objeto) {
                this.Fecha = fecha;
                this.Objeto = objeto;
            }
        }

        private List<ListaOrdenablePorFechaItem> lista;

        public ListaOrdenablePorFecha() {
            lista = new List<ListaOrdenablePorFechaItem>();
        }

        public void Add(DateTime fecha, object objeto) {
            lista.Add(new ListaOrdenablePorFechaItem(fecha, objeto));
        }

        public List<object> Ascendente()
        {
            List<object> retorno = new List<object>();
            lista = lista.OrderBy(lo => lo.Fecha).ToList();
            foreach (var litem in lista)
            {
                retorno.Add(litem.Objeto);
            }
            return retorno;
        }

        public List<object> Descendente()
        {
            List<object> retorno = new List<object>();
            lista = lista.OrderByDescending(lo => lo.Fecha).ToList();
            foreach (var litem in lista)
            {
                retorno.Add(litem.Objeto);
            }
            return retorno;
        }


    }


}