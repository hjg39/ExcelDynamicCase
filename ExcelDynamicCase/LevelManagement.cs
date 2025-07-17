using Interop = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;
using ExcelUnityPipeline;
using System;
using System.Threading.Tasks;
using ExcelDynamicCase.Domain;
using ExcelDynamicCase.Domain.CaseQuestions;
using System.Threading;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using LanaBananaDivineQuestionModifierProject;

namespace ExcelDynamicCase
{
    public static class LevelManagement
    {
        private static readonly Random _random = new Random();

        public static readonly object _lockObject = new object();

        public static CaseQuestionEnum CaseQuestionCode { get; set; }
        public static string Challenger { get; set; }

        public static CaseQuestion GetCaseQuestion(CaseQuestionEnum questionCode)
            => CaseQuestionRepo.CaseQuestions[questionCode];

        private static LanaOverlay LanaOverlay { get; set; }
        private static bool IsOverlayOpenAndNotClosed { get; set; }


        public static CancellationTokenSource BattleTimerCts { get; set; }

        public static CancellationTokenSource WaitForNextBattleCts { get; set; }

        public static void StartCaseQuestion()
        {
            CancellationTokenSource cts = new CancellationTokenSource();


            CaseQuestion caseQuestion = GetCaseQuestion(CaseQuestionCode);

            if (caseQuestion.AllowedFunctions.Any())
            {
                Storage.AllowedFunctions = caseQuestion.AllowedFunctions.ToList();
            }

            Storage.AllowArithmetic = caseQuestion.AllowArithmetic;

            Battle.CaseQuestion = caseQuestion;
            StartBattle(caseQuestion, cts);

            if (caseQuestion.ReflectionModifier)
            {
                Task.Delay(TimeSpan.FromMinutes(3), cts.Token).ContinueWith(_ => ThisWorkbook.ExcelCtx.Post(__ => ApplyReflections(), cts.Token));
            }

            if (caseQuestion.WhirlpoolBananaModifier && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (System.Windows.Application.Current == null)
                    _ = new System.Windows.Application();

                LanaOverlay = new LanaOverlay();
                LanaOverlay.Closed += LanaOverlay_Closed;
                LanaOverlay.Show();
                IsOverlayOpenAndNotClosed = true;
            }

            Task.Delay(TimeSpan.FromMinutes(caseQuestion.Minutes), cts.Token).ContinueWith(_ => ThisWorkbook.ExcelCtx.Post(__ => StopBattle(
                new BattleResult()
                {
                    BattleResultId = Guid.NewGuid(),
                    IsSuccess = false,
                }), null), cts.Token);
        }

        private static void LanaOverlay_Closed(object sender, EventArgs e)
        {
            if (IsOverlayOpenAndNotClosed)
            {
                StopBattle(new BattleResult()
                {
                    BattleResultId = Guid.NewGuid(),
                    IsSuccess = false,
                    IsPure = false,
                });
            }
        }

        private static void ApplyReflections(int attemptCount = 0)
        {
            if (attemptCount == 0)
            {
                lock (_lockObject)
                {
                    ApplyReflectionsLogic(attemptCount);
                }
            }
            else
            {
                ApplyReflectionsLogic(attemptCount);
            }
        }

        private static void ApplyReflectionsLogic(int attemptCount)
        {
            bool rareEffectApplied = false;

            try
            {


                //Globals.ThisWorkbook.UnHookSheetChangeEvent();

                Globals.Workings.DisplayRightToLeft = _random.NextDouble() < 0.5;

                Globals.Workings.DisplayPageBreaks = _random.NextDouble() < 0.05;

                Range r = null;

                try
                {
                    r = Globals.Workings.UsedRange;
                }
                catch (Exception)
                {
                }

                if (!(r is null) && r.Cells.Count > 1)
                {
                    if (_random.NextDouble() < 0.1)
                    {
                        r.Interior.Color = 16776156;
                        rareEffectApplied = true;
                    }

                    if (_random.NextDouble() < 0.1)
                    {
                        r.Font.Color = 16316664;
                        rareEffectApplied = true;
                    }

                    if (_random.NextDouble() < 0.1)
                    {
                        r.Font.Italic = true;
                    }

                    if (_random.NextDouble() < 0.2)
                    {
                        r.ClearFormats();
                    }

                    // if (_random.NextDouble() < 0.4)
                    // {
                    //     r.HorizontalAlignment = _random.NextDouble() < 0.5 ? HorizontalAlignment.Left : HorizontalAlignment.Right;
                    // }
                    if (_random.NextDouble() < 0.05)
                    {
                        r.Orientation = _random.NextDouble() < 0.5 ? 90 : -90;
                        rareEffectApplied = true;
                    }
                    if (_random.NextDouble() < 0.1)
                    {
                        if (Globals.Workings.Application.ActiveCell.Worksheet.Name == "Workings")
                        {
                            Globals.Workings.Application.ActiveWindow.FreezePanes = true;
                        }
                    }
                }


                Task.Delay(TimeSpan.FromMinutes(1)).ContinueWith(_ => ThisWorkbook.ExcelCtx.Post(__ => ApplyReflections(), null));
            }
            catch (Exception)
            {
                if (attemptCount < 60)
                {
                    if (attemptCount == 40)
                    {
                        MessageBox.Show("Unable to trigger reflections events for 40 attempts, will crash if reaches 60.");
                    }

                    if (!rareEffectApplied)
                    {
                        Task.Delay(3000).ContinueWith(_ => ThisWorkbook.ExcelCtx.Post(__ => ApplyReflections(attemptCount + 1), null));
                    }
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                //Globals.ThisWorkbook.HookSheetChangeEvent();
            }
        }

        public static void CloseOverlay()
        {
            IsOverlayOpenAndNotClosed = false;

            if (LanaOverlay == null) return;      // nothing to close

            // always hop onto the window’s UI thread
            LanaOverlay.Dispatcher.Invoke(() => LanaOverlay.Close());
            LanaOverlay = null;
        }

        public static void StopBattle(BattleResult battleResult, int attemptCount = 0)
        {
            if (attemptCount == 0)
            {
                lock (_lockObject)
                {
                    StopBattleLogic(battleResult, attemptCount);
                }
            }
            else
            {
                StopBattleLogic(battleResult, attemptCount);
            }
        }

        private static void StopBattleLogic(BattleResult battleResult, int attemptCount = 0)
        {
            try
            {
                CloseOverlay();

                BattleTimerCts.Cancel();
                Globals.ThisWorkbook.UnHookSheetChangeEvent();

                EnableUnityIsActiveSheet();
                DisableWorkingsSheet();
                DisableBattleSheet();

                Globals.UnityIsActive.Activate();

                Task.Run(async () => await PipelineToUnity.PipelineToUnity.SendOverworldStateAsync(battleResult));
            }
            catch (Exception)
            {
                if (attemptCount < 20)
                {
                    Task.Delay(3000).ContinueWith(_ => ThisWorkbook.ExcelCtx.Post(__ => StopBattle(battleResult, attemptCount + 1), null));
                }
                else
                {
                    throw;
                }
            }
        }

        public static void StartBattle(CaseQuestion caseQuestion, CancellationTokenSource cts)
        {
            LevelManagement.BattleTimerCts = cts;
            Globals.ThisWorkbook.UnHookSheetChangeEvent();

            EnableWorkingsSheet();
            EnableBattleSheet(caseQuestion);
            DisableUnityIsActiveSheet();
            
            Globals.ThisWorkbook.HookSheetChangeEvent();
        }

        private static void DisableUnityIsActiveSheet()
        {
            Globals.UnityIsActive.Unprotect(Storage.PASSWORD);
            Globals.UnityIsActive.Visible = Interop.XlSheetVisibility.xlSheetVeryHidden;
            Globals.UnityIsActive.Protect(Storage.PASSWORD);
        }

        private static void EnableUnityIsActiveSheet()
        {
            Globals.UnityIsActive.Unprotect(Storage.PASSWORD);
            Globals.UnityIsActive.Visible = Interop.XlSheetVisibility.xlSheetVisible;
            Globals.UnityIsActive.Protect(Storage.PASSWORD);
        }

        private static void EnableWorkingsSheet()
        {
            Globals.Workings.Unprotect(Storage.PASSWORD);
            Globals.Workings.Cells.Clear();
            Globals.Workings.Visible = Interop.XlSheetVisibility.xlSheetVisible;
        }

        private static void DisableWorkingsSheet()
        {
            Globals.Workings.Visible = Interop.XlSheetVisibility.xlSheetVeryHidden;
            Globals.Workings.Protect(Storage.PASSWORD);
        }

        public static void EnableBattleSheet(CaseQuestion caseQuestion)
        {
            Globals.Battle.Unprotect(Storage.PASSWORD);
            Globals.Battle.RunSetup(caseQuestion, Challenger);
            Globals.Battle.Visible = Interop.XlSheetVisibility.xlSheetVisible;
            Globals.Battle.Protect(Storage.PASSWORD);

            Globals.Battle.Activate();
        }

        public static void DisableBattleSheet()
        {
            Globals.Battle.Unprotect(Storage.PASSWORD);
            Globals.Battle.Visible = Interop.XlSheetVisibility.xlSheetVeryHidden;
            Globals.Battle.Protect(Storage.PASSWORD);
        }

        public static void InitialiseLevels()
        {
            Globals.ThisWorkbook.UnHookSheetChangeEvent();

            ThisWorkbook wb = Globals.ThisWorkbook;

            foreach (Interop.Worksheet ws in wb.Worksheets)
            {
                if (ws.Name == Globals.UnityIsActive.Name)
                {
                    if (ws.Visible != Interop.XlSheetVisibility.xlSheetVisible)
                    {
                        if (ws.ProtectContents)
                        {
                            ws.Unprotect(Storage.PASSWORD);
                        }

                        ws.Visible = Interop.XlSheetVisibility.xlSheetVisible;
                    }

                    if (!ws.ProtectContents)
                    {
                        ws.Protect(Storage.PASSWORD);
                    }

                    Globals.UnityIsActive.Activate();
                }
                else
                {
                    try
                    {
                        ws.Visible = Interop.XlSheetVisibility.xlSheetVeryHidden;
                    }
                    catch (Exception)
                    {
                        // Just get the game set up with everything in the right place, this can happen in a bad crash out if the main sheet is hidden and is unavoidable
                    }

                    try
                    {
                        if (!ws.ProtectContents)
                        {
                            ws.Protect(Storage.PASSWORD);
                        }
                    }
                    catch (Exception)
                    {
                    }

                }
            }

            Globals.ThisWorkbook.HookSheetChangeEvent();
        }
    }
}
