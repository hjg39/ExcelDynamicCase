using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ExcelDomain
{
    public static class QuestionsDatabase
    {
        public static Dictionary<QuestionRewardClassification, int[]> CaseNumbersByRewardClassification =
        new()
        {
            {  QuestionRewardClassification.EasyMaths, new int[] { 1 } }
        };

        public static Dictionary<QuestionRewardClassification, string[]> FunctionRewardsByClassification = new()
        {
            { QuestionRewardClassification.None, new string[] { }  },
            { QuestionRewardClassification.EasyAggregates, new string[] { "MAXA", "SMALL", "MINIFS", "MODE.SNGL", "MEDIAN", "MODE.MULT", "COUNTIF", "AVERAGE", "AVERAGEA", "SUMPRODUCT", "SUM", "PRODUCT", "COUNT", "ROWS", "COLUMNS", "AND" }  },
            { QuestionRewardClassification.AdvancedAggregates, new string[] { "LEN", "MIN", "MAX", "MINA", "RANK", "COUNTA", "COUNTBLANK", "COUNTIF", "COUNTIFS", "LARGE", "SUMIF", "MAXIFS", "MINIFS", "SUMIFS", "HSTACK", "OR" }  },
            { QuestionRewardClassification.ExpertAggregates, new string[] { "VSTACK", "TOCOL", "TOROW", "WRAPCOLS", "WRAPROWS", "REDUCE", "FREQUENCY", "SORT", "UNIQUE", "QUARTILE.INC", "AVERAGEIF", "AVERAGEIFS", "GEOMEAN", "HARMEAN", "LARGE", "XOR" }  },
            { QuestionRewardClassification.DivineAggregates, new string[] { "SUBTOTAL", "AGGREGATE", "VARA", "SORTBY", "GROUPBY", "PIVOTBY", "COMBIN", "QUARTILE.EXC", "MULTINOMIAL", "CORREL", "LINEST", "LOGEST", "INTERCEPT", "SUMSQ", "GCD", "LCM" }  },
            { QuestionRewardClassification.AdvancedComplex, new string[] { "COMPLEX", "IMREAL", "IMAGINARY", "IMSUM", "IMSUB", "IMPRODUCT", "IMDIV", "IMLN", "IMLOG2", "IMLOG10", "IMPOWER", "IMSQRT", "IMSIN", "IMCOS", "IMTAN", "IMEXP" }  },
            { QuestionRewardClassification.ExpertBases, new string[] { "BITAND", "BITOR", "BITXOR", "BITLSHIFT", "BITRSHIFT", "FIXED", "OCT2BIN", "OCT2DEC", "OCT2HEX", "DEC2BIN", "DEC2HEX", "DEC2OCT", "HEX2DEC", "HEX2OCT", "HEX2BIN", "BASE" }  },
            { QuestionRewardClassification.EasyLookup, new string[] { "HLOOKUP", "ISERROR", "LOOKUP", "ISREF", "FIND", "SEARCH", "ROW", "COLUMN", "ADDRESS", "INFO", "CELL", "INDIRECT", "XMATCH", "SHEET", "ISNUMBER", "MATCH" }  },
            { QuestionRewardClassification.AdvancedLookup, new string[] { "ISTEXT", "EXACT", "REGEXEXTRACT", "ISLOGICAL", "OFFSET", "GETPIVOTDATA", "ROUND", "ROUNDUP", "ROUNDDOWN", "INDEX", "VLOOKUP", "XLOOKUP", "ISNA", "NA", "SWITCH", "CHOOSE", "ERROR.TYPE" }  },
            { QuestionRewardClassification.EasyMaths, new string[] { "ABS", "MOD", "SQRT", "ODD", "EVEN", "INT", "ISODD", "ISEVEN", "FLOOR.MATH", "ROMAN", "ARABIC", "CEILING.MATH", "DELTA", "SIGN", "ATAN", "PI", "SEQUENCE"  }  },
            { QuestionRewardClassification.ExpertMaths, new string[] { "MUNIT", "MMULT", "MINVERSE", "MDETERM", "FACT", "FACTDOUBLE", "MROUND", "PERCENTOF", "EXP", "VALUE", "NUMBERVALUE", "N", "LOG", "LOG10", "LN", "PHI" }  },
            { QuestionRewardClassification.EasyText, new string[] { "RIGHT", "CLEAN", "LEFT", "CHAR", "CODE", "UNICHAR", "UNICODE", "REPT", "PROPER", "T", "TRIM", "VALUETOTEXT", "REPLACE", "TEXTAFTER", "ARRAYTOTEXT", "TEXT" }  },
            { QuestionRewardClassification.AdvancedText, new string[] { "SUBSTITUTE", "REGEXREPLACE", "REGEXTEXT", "MID", "UPPER", "DOLLAR", "FORMULATEXT", "ISNONTEXT", "DETECTLANGUAGE", "TRANSLATE", "CONCAT", "TEXTBEFORE", "TEXTJOIN", "TEXTSPLIT" }  },
            { QuestionRewardClassification.BasicManipulation, new string[] { "IFS", "EXPAND", "TRANSPOSE", "TOROW", "AREAS", "GESTEP", "TYPE", "ISOMITTED", "ISFORMULA", "ISLOGICAL", "IMAGE", "HYPERLINK", "NOT"    }  },
            { QuestionRewardClassification.AdvancedManipulation, new string[] { "SIGN", "LET", "LAMBDA", "IF", "OFFSET", "IFNA", "BYCOL", "BYROW", "MAKEARRAY", "MAP", "REDUCE", "SCAN", "TAKE", "DROP", "TOCOL"  }  },
            { QuestionRewardClassification.AdvancedDates, new string[] { "DATE", "DATEVALUE", "DAY", "DAYS", "HOUR", "MINUTE", "MONTH", "NOW", "SECOND", "TIME", "TIMEVALUE", "YEAR", "YEARFRAC"  }  },
            { QuestionRewardClassification.ExpertDates, new string[] { "DAYS360", "EDATE", "EOMONTH", "ISOWEEKNUM", "NETWORKDAYS", "NETWORKDAYS.INTL", "TODAY", "WEEKDAY", "WEEKNUM", "WORKDAY", "WORKDAY.INTL", }  },

        };
    }
}
