using Interop = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;
using ExcelUnityPipeline;
using System;
using System.Threading.Tasks;
using ExcelDynamicCase.Domain;
using ExcelDynamicCase.Domain.CaseQuestions;

namespace ExcelDynamicCase
{
    public static class LevelManagement
    {
        public static CaseQuestionEnum CaseQuestionCode { get; set; }

        public static CaseQuestion GetCaseQuestion(CaseQuestionEnum questionCode)
            => CaseQuestionRepo.CaseQuestions[questionCode];

        public static void StartCaseQuestion()
        {
            CaseQuestion caseQuestion = GetCaseQuestion(CaseQuestionCode);
            StartBattle(caseQuestion);
        }

        public static void StartBattle(CaseQuestion caseQuestion)
        {
            Globals.ThisWorkbook.UnHookSheetChangeEvent();

            DisableUnityIsActiveSheet();
            EnableWorkingsSheet();
            EnableBattleSheet(caseQuestion);

            Globals.ThisWorkbook.HookSheetChangeEvent();
        }

        private static void DisableUnityIsActiveSheet()
        {
            Globals.UnityIsActive.Visible = Interop.XlSheetVisibility.xlSheetVeryHidden;
        }

        private static void EnableWorkingsSheet()
        {
            Globals.Workings.Cells.Clear();
            Globals.Workings.Visible = Interop.XlSheetVisibility.xlSheetVisible;
        }

        public static void EnableBattleSheet(CaseQuestion caseQuestion)
        {
            Globals.Battle.Unprotect(Storage.PASSWORD);
            Globals.Battle.RunSetup(caseQuestion);
            Globals.Battle.Protect(Storage.PASSWORD);

            Globals.Battle.Visible = Interop.XlSheetVisibility.xlSheetVisible;
            Globals.Battle.Activate();
        }

        public static void ReturnToUnity(BattleResult battleResult)
        {
            Task.Run(async () => await PipelineToUnity.PipelineToUnity.SendOverworldStateAsync(battleResult)).RunSynchronously();
        }

        public static void InitialiseLevels()
        {
            Globals.ThisWorkbook.UnHookSheetChangeEvent();

            ThisWorkbook wb = Globals.ThisWorkbook;

            foreach (Interop.Worksheet ws in wb.Worksheets)
            {
                if (ws.Name == Globals.UnityIsActive.Name)
                {
                    ws.Protect(Storage.PASSWORD);
                    Globals.UnityIsActive.Activate();
                }
                else
                {
                    ws.Visible = Interop.XlSheetVisibility.xlSheetVeryHidden;
                    ws.Protect(Storage.PASSWORD);
                }
            }

            Globals.ThisWorkbook.HookSheetChangeEvent();
        }
    }
}
