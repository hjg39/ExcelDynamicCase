using ExcelDynamicCase.Domain.CaseQuestions;
using ExcelUnityPipeline;
using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelDynamicCase
{
    public partial class Battle
    {
        public static CaseQuestion CaseQuestion { get; set; }

        private void Sheet5_Startup(object sender, System.EventArgs e)
        {
            this.Change += Battle_Change;
        }

        private void Sheet5_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void Battle_Change(Excel.Range target)
        {
            if (target.Row != 2 || target.Column != 5)
            {
                return;
            }

            dynamic value = target.Value;

            if (!(value is object o))
            {
                return;
            }

            string answer = o.ToString();

            if (answer == CaseQuestion.Answer)
            {
                BattleResult winResult = new BattleResult()
                {
                    BattleResultId = Guid.NewGuid(),
                    IsSuccess = true,
                };

                LevelManagement.StopBattle(winResult);
            }
            else if (answer == (-1).ToString())
            {
                BattleResult lossResult = new BattleResult()
                {
                    BattleResultId = Guid.NewGuid(),
                    IsSuccess = false,
                };

                LevelManagement.StopBattle(lossResult);
            }

            return;
        }

        public void RunSetup(CaseQuestion caseQuestion, string challenger)
        {
            ((Excel.Range)this.Cells[3, 15]).Formula = caseQuestion.QuestionLink is null ? "None" : string.Format("=HYPERLINK(\"{0}\",\"{0}\")", caseQuestion.QuestionLink);
            ((Excel.Range)this.Cells[6, 15]).Value = caseQuestion.Id.ToString();

            ((Excel.Range)this.Cells[2, 5]).Value = null;
            ((Excel.Range)this.Cells[4, 5]).Formula = caseQuestion.ExampleAnswer;
            ((Excel.Range)this.Cells[6, 2]).Value = $"{challenger} has challenged you to a battle";
            ((Excel.Range)this.Cells[8, 2]).Value = $"You have {caseQuestion.Minutes} minutes (which you can follow in the overworld window).";

            ((Excel.Range)this.Cells[17, 2]).Value = caseQuestion.QuestionText;

            Excel.Range data = null;

            try { data = Globals.ThisWorkbook.Application.Intersect(this.UsedRange, this.Range[this.Cells[22, 1], this.Cells[1000, 1000]]); } catch (Exception) { }

            data?.Clear();

            if (caseQuestion.Data is null)
            {
                return;
            }

            int startingRow = 22;

            foreach (KeyValuePair<string, object[,]> item in caseQuestion.Data)
            {
                ((Excel.Range)this.Cells[startingRow, 2]).Value = item.Key;
                startingRow += 2;
                Excel.Range r = ((Excel.Range)this.Cells[startingRow + 2, 2]).Resize[item.Value.GetLength(0), item.Value.GetLength(1)];
                r.Formula = item.Value;

                if (caseQuestion.Colours is Dictionary<string, int[,]> colourDict && colourDict.TryGetValue(item.Key, out int[,] colours))
                {
                    r.Interior.Color = colours;
                }

                startingRow += item.Value.GetLength(0) + 1;
            }
        }

        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Sheet5_Startup);
            this.Shutdown += new System.EventHandler(Sheet5_Shutdown);
        }

        #endregion

    }
}
