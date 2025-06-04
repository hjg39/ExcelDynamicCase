using ExcelDynamicCase.Questions;
using ExcelUnityPipeline;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ExcelDynamicCase
{
    public partial class L2_Battle : ILevel
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

        public void RunSetup()
        {
            EulerProblemParser problemParser = new EulerProblemParser();
            string problemText = problemParser.GetProblemText(QuestionNumber);

            this.Cells[4, 2].Value = $"{Player} challenges you to a battle for the {ContestedFunction} function on problem {QuestionNumber}!";

            this.Cells[6, 2].Value = $"Here is the question, you have {NumberOfMinutes} minutes - which you can follow on the 'Information' tab.";
            this.Cells[7, 2].Value = $"Note the answer must be pasted as a value.  If you want to give up, write -1.";

            this.Cells[9, 2].Value = problemText;

            this.Cells[2, 3].Value = null;

            stopwatch.Restart();
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
