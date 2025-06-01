using Interop = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;

namespace ExcelDynamicCase
{
    public static class LevelManagement
    {
        public static ILevel CurrentLevel = null;

        public static void UpdateLevelInfo(ILevel level)
        {
            CurrentLevel = level;

            Globals.Information.UpdateLevelInfo(level);
        }

        public static void NextLevel(WorksheetBase worksheetFrom, WorksheetBase worksheetTo, ILevel levelTo)
        {
            Globals.ThisWorkbook.UnHookSheetChangeEvent();

            worksheetFrom.Visible = Interop.XlSheetVisibility.xlSheetVeryHidden;

            worksheetTo.Unprotect(Storage.PASSWORD);
            levelTo.RunSetup();
            worksheetTo.Protect(Storage.PASSWORD);

            UpdateLevelInfo(levelTo);

            worksheetTo.Visible = Interop.XlSheetVisibility.xlSheetVisible;
            worksheetTo.Activate();


            Globals.Workings.Cells.Clear();
            Globals.ThisWorkbook.HookSheetChangeEvent();
        }

        public static void InitialiseLevels()
        {
            ThisWorkbook wb = Globals.ThisWorkbook;

            foreach (Interop.Worksheet ws in wb.Worksheets)
            {
                if (ws.Name == Globals.Information.Name)
                {
                    ws.Protect(Storage.PASSWORD);
                }
                else if (ws.Name == Globals.Workings.Name)
                {
                    continue;
                }
                else if (ws.Name == Globals.L1_ChooseAStarter.Name)
                {
                    ws.Protect(Storage.PASSWORD);
                }
                else
                {
                    ws.Visible = Interop.XlSheetVisibility.xlSheetVeryHidden;
                    ws.Protect(Storage.PASSWORD);
                }
            }

            Globals.L1_ChooseAStarter.Visible = Interop.XlSheetVisibility.xlSheetVisible;
            Globals.L1_ChooseAStarter.Activate();
        }
    }
}
