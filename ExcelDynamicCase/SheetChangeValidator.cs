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
                try
                {
                    name?.Delete();
                }
                catch (System.Exception)
                {
                }
            }
        }

        public static void ValidateChanges(object sheet, Range target)
        {
            bool clearRange = false;

            if (target is null)
            {
                return;
            }

            if (!(sheet is Worksheet))
            {
                return;
            }

            if (target.Formula is object[,] formulae)
            {
                foreach (object formula in formulae)
                {
                    if (formula is string formulaString)
                    {
                        clearRange |= !ValidateFormula(formulaString);
                    }
                }
            }

            if (target.Formula is string s)
            {
                clearRange |= !ValidateFormula(s);
            }

            if (target.FormulaArray is object[,] formulae2)
            {
                foreach (object formula in formulae2)
                {
                    if (formula is string formulaString)
                    {
                        clearRange |= !ValidateFormula(formulaString);
                    }
                }
            }

            if (target.Formula is string s2)
            {
                clearRange |= !ValidateFormula(s2);
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

                if ((Storage.AllowedFormulae ?? new List<string>()).Contains(functionPart))
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
