using ExcelDynamicCase.Domain;
using ExcelUnityPipeline;
using System;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ExcelDynamicCase.PipelineToUnity
{
    public static class PipelineToUnity
    {
        private const string PIPE = "BattlePipe";
        private static readonly SemaphoreSlim _sync = new SemaphoreSlim(1, 1);
        private static NamedPipeClientStream _pipe;

        public static async Task InitPipeAsync()
        {
            _pipe = new NamedPipeClientStream(
                        ".", PIPE, PipeDirection.InOut, PipeOptions.Asynchronous);

            await _pipe.ConnectAsync();
            _pipe.ReadMode = PipeTransmissionMode.Message;   // keep Message mode
        }

        public static async Task SendOverworldStateAsync(BattleResult battleResult)
        {
            if (_pipe is null || !_pipe.IsConnected)
                throw new InvalidOperationException("Pipe not initialised");

            await _sync.WaitAsync();         // serialise callers
            try
            {
                if (!(battleResult is null))
                {
                    await PipeHelper.WriteAsync(_pipe, battleResult);
                } 

                BattleParameters battleParameters = await PipeHelper.ReadAsync<BattleParameters>(_pipe);

                LevelManagement.CaseQuestionCode = (CaseQuestionEnum)battleParameters.QuestionId;
                LevelManagement.Challenger = battleParameters.Challenger;
                //Storage.AllowedFunctions = battleParameters.AllowedFunctions;

                ThisWorkbook.ExcelCtx.Post(_ => StartBattleMode(), null);

                // …use parms …
            }
            catch (Exception)
            {
                QuitExcelGracefully();
                ThisWorkbook.ExcelCtx.Post(_ => Globals.ThisWorkbook.Close(false), null);
            }
            finally
            {
                _sync.Release();
            }
        }

        static void QuitExcelGracefully()
        {
            // First make sure the pipe is cleaned up; never throw here.
            try { _pipe?.Dispose(); } catch { /**/ }

            // Everything below MUST run on Excel's main thread.
            void QuitCore()
            {
                var app = Globals.ThisWorkbook.Application;

                try
                {


                    bool justThisWorkbook = app.Workbooks.Count == 1;

                    app.DisplayAlerts = false;   // no “Save changes?” dialogs
                    Globals.ThisWorkbook.Saved = true;   // mark as saved
                    Globals.ThisWorkbook.Close(false);   // close the workbook

                    if (justThisWorkbook)
                    {
                        app.Quit();                          // quit Excel
                    }
                }
                catch (ThreadAbortException)
                {
                    /* swallow – Excel is already on its way out */
                }
                catch (Exception quitEx)
                {
                    Debug.Fail($"Excel refused to quit: {quitEx}");
                    Environment.Exit(0);                 // last-resort hard exit
                }
                finally
                {
                    // Release COM objects so the process can really terminate
                    Marshal.FinalReleaseComObject(app);
                }
            }

            if (SynchronizationContext.Current == ThisWorkbook.ExcelCtx)
                QuitCore();                             // already on Excel thread
            else
                ThisWorkbook.ExcelCtx.Post(_ => QuitCore(), null);
        }

    /// somewhere in your shutdown/cleanup path
    public static void ClosePipe()
    {
        _pipe?.Dispose();
    }

    public static void StartBattleMode()
    {
        LevelManagement.StartCaseQuestion();
    }
    }
}
