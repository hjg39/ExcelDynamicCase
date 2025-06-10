using ExcelDynamicCase.PipelineToUnity;
using ExcelDynamicCase.Utility;
using Microsoft.Office.Interop.Excel;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelDynamicCase
{
    public partial class ThisWorkbook
    {
        private static Process _unity;

        public static Stopwatch LevelStopwatch { get; set; } = Stopwatch.StartNew();

        public static SynchronizationContext ExcelCtx;

        public static CancellationTokenSource StartUnityCts { get; set; }

        private void ThisWorkbook_Startup(object sender, System.EventArgs e)
        {
            ExcelCtx = WindowsFormsSynchronizationContext.Current
                ?? new WindowsFormsSynchronizationContext();

            LevelManagement.InitialiseLevels();

            HookSheetChangeEvent();
            this.NewSheet += ThisWorkbook_NewSheet;
        
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //Application.Interactive = false;

            try
            {
                StartUnityCts = new CancellationTokenSource();

                Task.Run(async () => await StartUnity(), StartUnityCts.Token);
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
                if (Debugger.IsAttached)
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = @"C:\Users\harry\source\repos\ExcelDynamicCase\ExcelDynamicCase\overworld\Excelopolis.exe",
                        Arguments = "-screen-fullscreen 0 -screen-width 1280 -screen-height 720",
                        UseShellExecute = true,
                        WindowStyle = ProcessWindowStyle.Normal,
                    };
                    _unity = Process.Start(psi);
                    WindowHelpers.ActivateWindow(_unity);
                }
                else
                {
                    string wbFolder = System.IO.Path.GetDirectoryName(Globals.ThisWorkbook.FullName);
                    string exePath = System.IO.Path.Combine(wbFolder, "overworld", "Excelopolis.exe");   // adjust filename
                    string workingDir = System.IO.Path.GetDirectoryName(exePath);

                    var psi = new ProcessStartInfo
                    {
                        FileName = exePath,
                        Arguments = "-screen-fullscreen 0 -screen-width 1280 -screen-height 720",
                        WorkingDirectory = workingDir,
                        UseShellExecute = true,
                        WindowStyle = ProcessWindowStyle.Normal,
                    };

                    Process.Start(psi);
                }
            }

            await PipelineToUnity.PipelineToUnity.InitPipeAsync();
            await PipelineToUnity.PipelineToUnity.SendOverworldStateAsync(null);
            //await PipelineToUnity.PipelineToUnity.SendOverworldStateAsync(new ExcelUnityPipeline.BattleResult()
            //{
            //    BattleResultId = Guid.NewGuid(),
            //    IsSuccess = true,
            //});
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
            Thread.Sleep(1);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("An unhandled error occurred, oops.");
            sb.AppendLine(e.ExceptionObject.ToString());

            MessageBox.Show(sb.ToString());
        }

        private void ThisWorkbook_Shutdown(object sender, EventArgs e)
        {
            PipelineToUnity.PipelineToUnity.ClosePipe();
        }

        private void ThisWorkbook_SheetChange(object sheet, Range target)
        {
            SheetChangeValidator.DeleteAllNames();
            SheetChangeValidator.ValidateChanges(sheet, target);
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
