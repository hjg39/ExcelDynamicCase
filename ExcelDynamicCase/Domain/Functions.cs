using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelDynamicCase.Domain
{
    public static class Functions
    {
        static Functions()
        {
            AllFunctions.AddRange(AggregatorFunctions);
            AllFunctions.AddRange(ManipulationFunctions);
            AllFunctions.AddRange(LookupFunctions);
        }

        public static readonly List<string> AllFunctions = new List<string>();

        public static readonly List<string> StarterFunctions = new List<string>() { "XLOOKUP", "VLOOKUP", "INDEX" };

        public static readonly List<string> AggregatorFunctions = new List<string>()
        {
            "MAX",
            "MAXA",
            "MINA",
            "MIN",
            "AND",
            "OR",
            "XOR",
            "CONCAT",
            "CONCATENATE",
            "GROUPBY",
            "AGGREGATE",
            "SUBTOTAL",
            "REDUCE",
            "SCAN",
            "MONTH",
            "IFS",
            "TOCOL",
            "TOROW",
            "SUM",
            "PRODUCT",
            "UNIQUE",
            "BASE",
            "CEILING.MATH",
            "FLOOR.MATH",
            "COMBINA",
            "FACT",
            "LCM",
            "GCD",
            "ROWS",
            "COLS",
            "SUMIF",
            "SUMIFS",
            "COUNTIF",
            "COUNTIFS",
            "AVERAGE",
            "AVERAGEIFS",
            "MEDIAN",
            "MODE.SNGL",
            "MODE.MULTI",
            "SEQUENCE"
        };

        public static readonly List<string> LookupFunctions = new List<string>()
        {
            "HLOOKUP",
            "LOOKUP",
            "CHOOSE",
            "CHOOSECOLS",
            "CHOOSEROWS",
            "TRANSPOSE",
            "INDIRECT",
            "VLOOKUP",
            "XLOOKUP",
            "INDEX",
            "LAMBDA",
            "LET",
            "EXACT",
            "LOG10",
            "RANDARRAY",
            "DAY",
            "IFERROR",
            "SORT",
            "SORTBY",
            "SWITCH",
            "MAKEARRAY",
            "LARGE",
            "SMALL",
            "COMPLEX",
            "IMAGINARY",
            "IMREAL",
            "IMSUM",
            "IMPRODUCT",
            "IMSUB",
            "IMDIV",
            "OFFSET"
        };

        public static readonly List<string> ManipulationFunctions = new List<string>()
        {
            "MATCH",
            "XMATCH",
            "FORMULATEXT",
            "TAKE",
            "DROP",
            "VSTACK",
            "HSTACK",
            "WRAPCOLS",
            "WRAPROWS",
            "NOT",
            "MOD",
            "CHAR",
            "CODE",
            "FILTER",
            "DATE",
            "YEAR",
            "IF",
            "MAP",
            "UPPER",
            "LOWER",
            "SUBSTITUTE",
            "LEFT",
            "RIGHT",
            "MID",
            "EXPAND",
            "N",
            "NA",
            "MMULT",
            "MUNIT"
        };
    }
}
