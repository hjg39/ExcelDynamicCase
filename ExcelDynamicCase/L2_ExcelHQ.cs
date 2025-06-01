using ExcelDynamicCase.Domain;
using ExcelDynamicCase.Questions;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace ExcelDynamicCase
{
    public partial class L2_ExcelHQ : ILevel
    {
        public string LevelName => "Excel HQ";

        public int BaseDeadline => 10000;

        private void Sheet6_Startup(object sender, System.EventArgs e)
        {
            this.Change += L2_ExcelHQ_Change;
        }

        private void L2_ExcelHQ_Change(Excel.Range target)
        {
            if (target.Row != 10 || target.Column != 2)
            {
                return;
            }

            dynamic value = target.Value;

            if (!(value is double d))
            {
                return;
            }

            int questionNumber;

            switch (d)
            {
                case 1:
                    L2_Battle.Player = "Elliott Patterson";
                    L2_Battle.ContestedFunction = Storage.GetRandomCurrentlyLockedFunction(FunctionClass.Lookup);
                    break;
                case 2:
                    L2_Battle.Player = "Ha Dang";
                    L2_Battle.ContestedFunction = Storage.GetRandomCurrentlyLockedFunction(FunctionClass.Aggregator);
                    break;
                case 3:
                    L2_Battle.Player = "Julien Lacaze";
                    L2_Battle.ContestedFunction = Storage.GetRandomCurrentlyLockedFunction(FunctionClass.Manipulation);
                    break;
                default:
                    return;
            }

            questionNumber = AllowedQuestions.GetRandomNotYetCompletedQuestion();
            L2_Battle.QuestionNumber = questionNumber;
            L2_Battle.NumberOfMinutes = AllowedQuestions.GetNumberOfMinutes(questionNumber);
            LevelManagement.NextLevel(this, Globals.L2_Battle, Globals.L2_Battle);
        }

        private void Sheet6_Shutdown(object sender, System.EventArgs e)
        {
        }

        public void RunSetup()
        {
        }


        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Sheet6_Startup);
            this.Shutdown += new System.EventHandler(Sheet6_Shutdown);
        }

        #endregion
    }
}
