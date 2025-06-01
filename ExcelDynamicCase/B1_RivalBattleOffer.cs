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
    public partial class B1_RivalBattleOffer : ILevel
    {
        public string LevelName => "Rival Battle Offer";

        public int BaseDeadline => 2000;

        public string TargetFunction = Storage.GetRandomCurrentlyLockedFunction(FunctionClass.Manipulation);

        private void Sheet4_Startup(object sender, System.EventArgs e)
        {
            this.Change += B1_RivalBatterOffer_Change;
        }

        private void B1_RivalBatterOffer_Change(Excel.Range target)
        {
            if (target.Row != 6 || target.Column != 2)
            {
                return;
            }

            dynamic value = target.Value;

            if (!(value is bool b))
            {
                return;
            }

            switch (b)
            {
                case true:
                    L2_Battle.Player = Storage.Rival;
                    L2_Battle.ContestedFunction = TargetFunction;

                    int questionNumber = AllowedQuestions.GetRandomNotYetCompletedQuestion();
                    L2_Battle.QuestionNumber = questionNumber;
                    L2_Battle.NumberOfMinutes = AllowedQuestions.GetNumberOfMinutes(questionNumber);

                    LevelManagement.NextLevel(this, Globals.L2_Battle, Globals.L2_Battle);
                    return;
                case false:
                    LevelManagement.NextLevel(this, Globals.L2_ExcelHQ, Globals.L2_ExcelHQ);
                    return;
            }
        }

        private void Sheet4_Shutdown(object sender, System.EventArgs e)
        {
        }

        public void RunSetup()
        {
            TargetFunction = Storage.GetRandomCurrentlyLockedFunction(FunctionClass.Manipulation);

            this.Unprotect(Storage.PASSWORD);

            ((Excel.Range)this.Cells[2, 2]).Value = $"Your rival, {Storage.Rival}, offers you to battle, contesting for the {TargetFunction} function.";

            this.Unprotect(Storage.PASSWORD);
        }


        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Sheet4_Startup);
            this.Shutdown += new System.EventHandler(Sheet4_Shutdown);
        }

        #endregion

    }
}
