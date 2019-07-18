using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAC.Models.Resources;

namespace SAC.Models
{
    public class ExcelModel
    {
        private Models.Resources.ExcelConnector excel;

        public List<DTO.Cuenta> ObtenerCuentas(string archivo) {
            List<DTO.Cuenta> cuentas = new List<DTO.Cuenta>();
            try
            {
                excel = new ExcelConnector(archivo);

                List<List<string>> lista = excel.getSheet("CUENTAS");

                foreach (List<string> fila in lista)
                {
                    //System.Diagnostics.Debug.WriteLine(string.Format("> {0}, {1}, {2}, {3}", fila[0], fila[1], fila[7], fila[8]));
                    DTO.Cuenta c = new DTO.Cuenta();
                    c.id = -1;
                    c.numero = fila[0];
                    c.nombre = fila[1];
                    c.empresa = new DTO.Empresa();
                    c.empresa.empresaId = -1;
                    c.empresa.razonSocial = fila[2];
                    c.rubro = new DTO.Rubro();
                    c.rubro.rubroId = -1;
                    c.rubro.rubroNombre = fila[3];
                    c.grupo = new DTO.Grupo();
                    c.grupo.grupoId = -1;
                    c.grupo.grupoNombre = fila[4];
                    c.descripcion = fila[5];
                    cuentas.Add(c);
                }
                return cuentas;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPCION (excel, ObtenerCuentas): " + ex.Message);
                return new List<DTO.Cuenta>();
            }
            
            
        }

        public List<DTO.CuentaActiva> ObtenerCuentasActivasDesdeArchivo(string archivo)
        {
            List<DTO.CuentaActiva> cuentas = new List<DTO.CuentaActiva>();
            try
            {
                excel = new ExcelConnector(archivo);

                List<List<string>> lista = excel.getSheet("CUENTAS");

                foreach (List<string> fila in lista)
                {
                    //System.Diagnostics.Debug.WriteLine(string.Format("> {0}, {1}, {2}, {3}", fila[0], fila[1], fila[7], fila[8]));
                    DTO.CuentaActiva c = new DTO.CuentaActiva();
                    c.id = -1;
                    c.cuentaActivaId = -1;
                    c.empresa = new DTO.Empresa();
                    c.empresa.empresaId = -1;
                    c.empresa.razonSocial = fila[0];
                    c.numero = fila[1];
                    c.cuentaActivaTotal =  int.Parse(fila[2]);
                    c.analista = new DTO.Usuario();
                    c.analista.usuarioId = -1;
                    c.analista.nombre = fila[3];
                    c.validador  = new DTO.Usuario();
                    c.validador.usuarioId = -1;
                    c.validador.nombre = fila[4];
                    c.certificador  = new DTO.Usuario();
                    c.certificador.nombre = fila[5];
                    c.certificador.usuarioId = -1;
                    c.cuentaActivaDiasPlazo = int.Parse(fila[6]);
                    c.cuentaActivaFecha = Convert.ToDateTime(fila[7]);
                    //System.Diagnostics.Debug.WriteLine("--------" + fila[7]);
                    cuentas.Add(c);
                }
                return cuentas;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPCION (excel, ObtenerCuentasAsignadas): " + ex.Message);
                return new List<DTO.CuentaActiva>();
            }


        }


    }
}