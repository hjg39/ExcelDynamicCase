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

        private void Sheet4_Startup(object sender, System.EventArgs e)
        {
        }

        private void Sheet4_Shutdown(object sender, System.EventArgs e)
        {
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
