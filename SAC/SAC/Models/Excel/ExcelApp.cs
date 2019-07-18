using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Runtime.InteropServices;
using SQL = System.Data;
using Microsoft.Office.Interop.Excel;

namespace SAC.Models.Excel
{
    public class ExcelApp
    {
        private List<ExcelWorkBook> books;
        private Application ExcelApplication;
        private _Workbook WorkBook;
        private _Worksheet ActiveSheet;
        private List<string> cols;

        public ExcelApp() {
            this.ExcelApplication = new Application();
            this.ExcelApplication.Visible = false;

            this.WorkBook = (_Workbook)(this.ExcelApplication.Workbooks.Add(Missing.Value));
            this.ActiveSheet = (_Worksheet)this.ExcelApplication.ActiveSheet;

            cols = new List<string>();
            string[] s = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,AA".Split(',');
            foreach (var l in s)
            {
                cols.Add(l);
            }
        }

        public void Save(string path) {
            this.ExcelApplication.Visible = false;
            this.ExcelApplication.UserControl = false;
            
            this.WorkBook.SaveAs(path, AccessMode: XlSaveAsAccessMode.xlShared);
        }
        
        public void ActiveSheetSetName(string name) {
            this.ActiveSheet.Name = name;
        }

        public void ActiveSheetSetColumnName(string range, string name) {
            this.ActiveSheet.get_Range(range).Value2 = name;
            this.ActiveSheet.get_Range(range).Font.Bold = true;
            this.ActiveSheet.get_Range(range).VerticalAlignment = XlVAlign.xlVAlignCenter;
        }

        public void ActiveSheetLineWriter(string letter, int number, List<string> values) {
            var first = true;
            foreach (var val in values)
            {
                if (!first) {
                    letter = nextCol(letter);
                    
                }
                this.ActiveSheet.get_Range(letter + number).Value2 = val;

                first = false;
            }
        }

        private string nextCol(string col) {
            int i = cols.IndexOf(col);
            return cols[i + 1];
        }
    }


}