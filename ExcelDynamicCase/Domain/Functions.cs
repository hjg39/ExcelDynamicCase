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
            "SUBTOTAL"
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
            "INDEX"
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
            "MOD"
        };
    }
}
