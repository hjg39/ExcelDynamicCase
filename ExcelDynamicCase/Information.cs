using Microsoft.VisualStudio.Tools.Applications.Runtime;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace ExcelDynamicCase
{
    public partial class Information
    {
        private void Sheet2_Startup(object sender, System.EventArgs e)
        {
        }

        private void Sheet2_Shutdown(object sender, System.EventArgs e)
        {
        }

        public void UpdateLevelInfo(ILevel level)
        {
            this.Unprotect(Storage.PASSWORD);

            ((Excel.Range)this.Cells[3, 2]).Value2 = level.LevelName;
            ((Excel.Range)this.Cells[3, 4]).Value2 = DateTime.Now.AddMinutes(level.BaseDeadline).ToOADate();

            int startingRow = 6;

            foreach (string formula in Storage.AllowedFormulae)
            {
                this.Cells[startingRow++, 2].Value = formula;
            }

            startingRow = 6;

            foreach (string formula in Storage.GetAllLockedFunctions())
            {
                this.Cells[startingRow++, 3].Value = formula;
            }

            this.Protect(Storage.PASSWORD);
        }

        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Sheet2_Startup);
            this.Shutdown += new System.EventHandler(Sheet2_Shutdown);
        }

        #endregion

    }
}
