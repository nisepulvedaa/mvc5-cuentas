using SAC.Helpers;
using SAC.Models.Excel;
using SAC.Models.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace SAC.Models
{
    public class ReporteModel
    {
        private readonly Models.Resources.SACModel db = new Models.Resources.SACModel();

        public List<int> ObtenerAñosCuentasActivas()
        {
            List<int> lista = new List<int>();

            var años = db.CuentaActiva.GroupBy(ca => ca.CuentaActivaFecha.Year);
            foreach (var año in años)
            {
                lista.Add(año.First().CuentaActivaFecha.Year);
            }
            return lista;
        }

        public List<DTO.CuentaActiva> ObtenerCuentasActivasPorFiltro(int[] años, int[] meses, int[] empresas, int[] rubros, int[] grupos, int[] cuentas)
        {
            List<DTO.CuentaActiva> lista = new List<DTO.CuentaActiva>();

            WhereBuilder<Resources.CuentaActiva> donde = new WhereBuilder<Resources.CuentaActiva>();

            if (años.Count() > 0)
            {
                donde.SetAnd(ca => años.Contains(DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Year));
            }
            if (meses.Count() > 0)
            {
                donde.SetAnd(ca => meses.Contains(DbFunctions.TruncateTime(ca.CuentaActivaFecha).Value.Month));
            }
            if (empresas.Count() > 0)
            {
                donde.SetAnd(ca => empresas.Contains(ca.Cuenta.Empresa.EmpresaId));
            }
            if (rubros.Count() > 0)
            {
                donde.SetAnd(ca => rubros.Contains(ca.Cuenta.Rubro.RubroId));
            }
            if (grupos.Count() > 0)
            {
                donde.SetAnd(ca => grupos.Contains(ca.Cuenta.Grupo.GrupoId));
            }
            if (cuentas.Count() > 0)
            {
                donde.SetAnd(ca => cuentas.Contains(ca.Cuenta.CuentaId));
            }

            var cuentasActivas = db.CuentaActiva.Where(donde.GetWhere());
            foreach (var ca in cuentasActivas)
            {
                lista.Add(DTOBuilder.CuentaActiva(ca));
            }

            return lista;
        }

        public string GenerarExcel(List<List<String>> datos)
        {
            string file = StringHelper.HashUnico("excel_", ".xlsx");
            string nombreArchivo = HostingEnvironment.ApplicationPhysicalPath + "Archivos\\Temporal\\" + file;

            var excel = new ExcelApp();
            excel.ActiveSheetSetColumnName("A1", "Empresa");
            excel.ActiveSheetSetColumnName("B1", "Cuenta");
            excel.ActiveSheetSetColumnName("C1", "Rubro");
            excel.ActiveSheetSetColumnName("D1", "Grupo");
            excel.ActiveSheetSetColumnName("E1", "Fecha");
            excel.ActiveSheetSetColumnName("F1", "Dias Plazo");
            excel.ActiveSheetSetColumnName("G1", "Estado");
            excel.ActiveSheetSetColumnName("H1", "Analista");
            excel.ActiveSheetSetColumnName("I1", "Validador");
            excel.ActiveSheetSetColumnName("J1", "Certificador");
            excel.ActiveSheetSetColumnName("K1", "Avance");


            int i = 1;
            foreach (var fila in datos)
            {
                i++;
                excel.ActiveSheetLineWriter("A", i, fila);
            }
            excel.Save(nombreArchivo);

            return nombreArchivo;
        }

        public void GenerarReporte1(string archivo, List<List<String>> datos)
        {
            ExcelConnector excel = new ExcelConnector(archivo);
            excel.WriteToSheet("CUENTAS", datos);
        }
    }
}