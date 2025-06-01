using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelDynamicCase
{
    public static class LevelManagement
    {
        public static ILevel CurrentLevel = null;

        public static void UpdateLevel(ILevel level)
        {
            CurrentLevel = level;

            Globals.Information.UpdateLevelInfo(level);
        }
    }
}
