using ExcelDynamicCase.Domain.CaseQuestions;
using ExcelDynamicCase.Questions;
using ExcelUnityPipeline;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelDynamicCase
{
    public partial class Battle
    {
        public static string Player { get; set; }

        public static string ContestedFunction { get; set; }

        public static int NumberOfMinutes { get; set; }

        public static int QuestionNumber { get; set; }

        public string LevelName => "Battle";

        public int BaseDeadline => NumberOfMinutes;

        private static readonly Stopwatch stopwatch = new Stopwatch();

        public bool TimeDidElapse = stopwatch.Elapsed > new TimeSpan(0, NumberOfMinutes, 0);

        private void Sheet5_Startup(object sender, System.EventArgs e)
        {
            this.Change += L2_Battle_Change;
        }

        private void L2_Battle_Change(Microsoft.Office.Interop.Excel.Range target)
        {
            if (TimeDidElapse)
            {
                YouLose();
                return;
            }

            if (target.Row != 2 || target.Column != 3)
            {
                return;
            }

            dynamic value = target.Value;

            if (!(value is object o))
            {
                return;
            }

            string answer = o.ToString();

            if (answer == QuestionSolutions.GetSolution(QuestionNumber))
            {
                BattleResult winResult = new BattleResult()
                {
                    BattleResultId = Guid.NewGuid(),
                    IsSuccess = true,
                };

                LevelManagement.ReturnToUnity(winResult);
            }
            else if (answer == (-1).ToString())
            {
                BattleResult lossResult = new BattleResult()
                {
                    BattleResultId = Guid.NewGuid(),
                    IsSuccess = false,
                };

                LevelManagement.ReturnToUnity(lossResult);
            }

            stopwatch.Stop();

            return;
        }

        public void YouLose()
        {
            MessageBox.Show("You were defeated :(");

            BattleResult loseResult = new BattleResult()
            {
                BattleResultId = Guid.NewGuid(),
                IsSuccess = false,
            };

            LevelManagement.ReturnToUnity(loseResult);
            stopwatch.Stop();
        }

        private void Sheet5_Shutdown(object sender, System.EventArgs e)
        {
            stopwatch.Stop();
        }

        public void RunSetup(CaseQuestion caseQuestion, string challenger)
        {
            ((Excel.Range)this.Cells[3, 15]).Formula = caseQuestion.QuestionLink is null ? "None" : string.Format("=HYPERLINK({0},{0})", caseQuestion.QuestionLink);
            ((Excel.Range)this.Cells[6, 15]).Value = caseQuestion.Id.ToString();

            ((Excel.Range)this.Cells[4, 5]).Value = caseQuestion.ExampleAnswer;
            ((Excel.Range)this.Cells[6, 2]).Value = $"{challenger} has challenged you to a battle";
            ((Excel.Range)this.Cells[8, 2]).Value = $"You have {caseQuestion.Minutes} minutes (which you can follow in the overworld window).";

            ((Excel.Range)this.Cells[15, 2]).Value = caseQuestion.QuestionText;

            Excel.Range data = null;

            try { data = Globals.ThisWorkbook.Application.Intersect(this.UsedRange, this.Range[this.Cells[20, 1], this.Cells[1000, 1000]]); } catch (Exception) { }

            data?.Clear();

            if (caseQuestion.Data is null)
            {
                return;
            }

            int startingRow = 20;

            foreach (KeyValuePair<string, object[,]> item in caseQuestion.Data)
            {
                ((Excel.Range)this.Cells[startingRow, 2]).Value = item.Key;
                startingRow += 2;
                Excel.Range r = ((Excel.Range)this.Cells[startingRow + 2, 2]).Resize[item.Value.GetLength(0), item.Value.GetLength(1)];
                r.Value = caseQuestion.Data;

                if (caseQuestion.Colours.TryGetValue(item.Key, out int[,] colours))
                {
                    r.Interior.Color = colours;
                }
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
