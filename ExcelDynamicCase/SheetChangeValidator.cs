using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ExcelDynamicCase
{
    public static class SheetChangeValidator
    {
        public static Regex ExcelFunctionRegex = new Regex(@"\b([A-Za-z_][A-Za-z0-9_.]*)\s*\(", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        public static void DeleteAllNames()
        {
            foreach (Name name in Globals.ThisWorkbook.Names)
            {
                name?.Delete();
            }
        }

        public static void ValidateChanges(object sheet, Range target)
        {
            bool clearRange = false;

            if (target is null)
            {
                return;
            }

            if (!(sheet is Worksheet ws))
            {
                return;
            }

            if (ws.Name == Globals.Information.Name)
            {
                return;
            }

            if (target.Formula is string[,] formulae)
            {
                foreach (string formula in formulae)
                {
                    clearRange |= !ValidateFormula(formula);
                }
            }

            if (target.Formula is string s)
            {
                clearRange |= !ValidateFormula(s);
            }

            if (clearRange)
            {
                target.Formula = null;
            }
        }

        public static bool ValidateFormula(string formula)
        {
            if (formula is null)
            {
                return true;
            }

            MatchCollection matches = ExcelFunctionRegex.Matches(formula);

            foreach (Match match in matches)
            {
                string functionPart = match.Value.Replace("(", "").Trim();

                if (Storage.AllowedFormulae.Contains(functionPart))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
