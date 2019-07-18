using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace SAC.Models.Resources
{
    public class ExcelConnector
    {
        private string fileName = "";

        public ExcelConnector(string fileName)
        {
            this.fileName = fileName;
        }

        private string getConnectionString()
        {
            string connString = "";
            if (fileName.Length > 0)
            {
                string ext = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (ext.ToLower().Equals("xls"))
                {
                    connString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties=Excel 8.0; ", this.fileName);
                }
                if (ext.ToLower().Equals("xlsx"))
                {
                    connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 12.0;", this.fileName);
                }

            }
            return connString;
        }

        public List<List<String>> getSheet(string sheetName)
        {
            List<List<String>> l = new List<List<string>>();

            var adapter = new OleDbDataAdapter("SELECT * FROM [" + sheetName + "$]", getConnectionString());
            var ds = new DataSet();

            adapter.Fill(ds, "tempTable");

            DataTable data = ds.Tables["tempTable"];

            foreach (DataRow row in data.Rows)
            {
                List<string> wrow = new List<string>();
                foreach (var item in row.ItemArray)
                {
                    wrow.Add(item.ToString());
                }
                l.Add(wrow);
            }

            return l;
        }

        public void test() {
            System.Diagnostics.Debug.WriteLine(getConnectionString());
            var conn = new OleDbConnection(getConnectionString());
            conn.Open();
            var cmd = new OleDbCommand("INSERT INTO [HOJA$] VALUES('1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11')", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void WriteToSheet(string sheetName, List<List<string>> data) {
            var conn = new OleDbConnection(getConnectionString());
            conn.Open();
            var cmd = new OleDbCommand();
            cmd.Connection = conn;

            foreach (var fila in data)
            {
                string values = "'" + String.Join("','", fila) + "'";
                string command = string.Format("INSERT INTO [{0}$] VALUES({1})", sheetName, values);
                cmd.CommandText = command;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
}