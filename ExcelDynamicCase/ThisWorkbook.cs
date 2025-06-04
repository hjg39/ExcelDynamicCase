using ExcelDynamicCase.PipelineToUnity;
using ExcelDynamicCase.Utility;
using Microsoft.Office.Interop.Excel;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelDynamicCase
{
    public partial class ThisWorkbook
    {
        private static Process _unity;

        public static Stopwatch LevelStopwatch { get; set; } = Stopwatch.StartNew();


        private void ThisWorkbook_Startup(object sender, System.EventArgs e)
        {
            LevelManagement.InitialiseLevels();

            this.SheetChange += ThisWorkbook_SheetChange;
            this.NewSheet += ThisWorkbook_NewSheet;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //Application.Interactive = false;

            try
            {
                Task.Run(async () => await StartUnity());
            }
            finally
            {
                //Globals.ThisWorkbook.inter
            }

        }

        private async static Task StartUnity()
        {
            if (_unity is null || _unity.HasExited)
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = @"C:\Users\harry\source\repos\ExcelDynamicCase\ExcelDynamicCase\overworld\My project.exe",
                    Arguments = "-screen-fullscreen 0 -popupwindow",
                    UseShellExecute = true
                };
                _unity = Process.Start(psi);
                WindowHelpers.ActivateWindow(_unity);
            }

            await PipelineToUnity.PipelineToUnity.SendOverworldStateAsync(new ExcelUnityPipeline.BattleResult()
            {
                BattleResultId = Guid.NewGuid(),
                IsSuccess = true,
            });
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
