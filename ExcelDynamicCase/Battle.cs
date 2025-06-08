using ExcelDynamicCase.Domain.CaseQuestions;
using ExcelDynamicCase.Questions;
using ExcelUnityPipeline;
using System;
using System.Diagnostics;
using System.Windows.Forms;

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

        public void RunSetup(CaseQuestion caseQuestion)
        {



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
