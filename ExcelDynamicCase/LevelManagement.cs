using Interop = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;
using ExcelUnityPipeline;
using System;
using System.Threading.Tasks;
using ExcelDynamicCase.Domain;
using ExcelDynamicCase.Domain.CaseQuestions;
using System.Threading;

namespace ExcelDynamicCase
{
    public static class LevelManagement
    {
        public static CaseQuestionEnum CaseQuestionCode { get; set; }
        public static string Challenger { get; set; }

        public static CaseQuestion GetCaseQuestion(CaseQuestionEnum questionCode)
            => CaseQuestionRepo.CaseQuestions[questionCode];

        public static CancellationTokenSource BattleTimerCts { get; set; }

        public static CancellationTokenSource WaitForNextBattleCts { get; set; }


        public static void StartCaseQuestion()
        {
            CancellationTokenSource cts = new CancellationTokenSource();


            CaseQuestion caseQuestion = GetCaseQuestion(CaseQuestionCode);
            Battle.CaseQuestion = caseQuestion;
            StartBattle(caseQuestion, cts);

            Task.Delay(TimeSpan.FromMinutes(caseQuestion.Minutes), cts.Token).ContinueWith(_ => ThisWorkbook.ExcelCtx.Post(__ => StopBattle(
                new BattleResult()
                {
                    BattleResultId = Guid.NewGuid(),
                    IsSuccess = false,
                }), null), cts.Token);
        }

        public static void StopBattle(BattleResult battleResult)
        {
            BattleTimerCts.Cancel();
            Globals.ThisWorkbook.UnHookSheetChangeEvent();

            EnableUnityIsActiveSheet();
            DisableWorkingsSheet();
            DisableBattleSheet();

            Globals.UnityIsActive.Activate();

            Task.Run(async () => await PipelineToUnity.PipelineToUnity.SendOverworldStateAsync(battleResult));
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
