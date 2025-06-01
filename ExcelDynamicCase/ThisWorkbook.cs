using Microsoft.Office.Interop.Excel;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelDynamicCase
{
    public partial class ThisWorkbook
    {
        public static Stopwatch LevelStopwatch { get; set; } = Stopwatch.StartNew();


        private void ThisWorkbook_Startup(object sender, System.EventArgs e)
        {
            LevelManagement.UpdateLevelInfo(Globals.L1_ChooseAStarter);
            LevelManagement.InitialiseLevels();

            this.SheetChange += ThisWorkbook_SheetChange;
            this.NewSheet += ThisWorkbook_NewSheet;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            
        }

        private void ThisWorkbook_NewSheet(object sh)
        {
            try
            {
                if (sh is Worksheet ws)
                {
                    ws.Delete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void HookSheetChangeEvent()
        {
            this.SheetChange -= ThisWorkbook_SheetChange;
            this.SheetChange += ThisWorkbook_SheetChange;
        }

        public void UnHookSheetChangeEvent()
        {
            this.SheetChange -= ThisWorkbook_SheetChange;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("An unhandled error occurred, oops.");
            sb.AppendLine(e.ExceptionObject.ToString());

            MessageBox.Show(sb.ToString());
        }

        private void ThisWorkbook_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void ThisWorkbook_SheetChange(object sheet, Excel.Range target)
        {
            SheetChangeValidator.DeleteAllNames();
            SheetChangeValidator.ValidateChanges(sheet, target);

            if (Globals.L2_Battle.TimeDidElapse)
            {
                Globals.L2_Battle.YouLose();
            }
        }

        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
        }

        #endregion

    }
}
