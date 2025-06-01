using Microsoft.VisualStudio.Tools.Applications.Runtime;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace ExcelDynamicCase
{
    public partial class L1_ChooseAStarter : ILevel<object>
    {
        public string LevelName { get => "Choose A Starter"; }

        public int BaseDeadline { get => 2000; }

        private void Sheet3_Startup(object sender, System.EventArgs e)
        {
            this.Change += L1_ChooseAStarter_Change;
        }

        private void L1_ChooseAStarter_Change(Excel.Range target)
        {
            if (target.Row != 8 || target.Column != 3)
            {
                return;
            }

            dynamic value = target.Value;

            if (!(value is string stringValue))
            {
                return;
            }

            switch (stringValue)
            {
                case "XLOOKUP":
                case "VLOOKUP":
                case "INDEX":
                    Storage.AllowedFormulae.Add(stringValue);
                    NextLevel(stringValue);
                    break;
                default:
                    return;
            }
        }

        public void RunSetup() { }

        public void NextLevel(object o)
        {
            LevelManagement.NextLevel(this, Globals.B1_RivalBattleOffer, Globals.B1_RivalBattleOffer);
        }

        private void Sheet3_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Sheet3_Startup);
            this.Shutdown += new System.EventHandler(Sheet3_Shutdown);
        }

        #endregion

    }
}
