using ExcelDynamicCase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelDynamicCase
{
    public static class Storage
    {
        public static Random Random = new Random();

        public static List<string> AllowedFormulae { get; set; }

        public const string PASSWORD = "fdfbgiskfhdspaoojoFODBSVFIUS";

        public static List<int> AlreadyCompletedQuestions = new List<int>();
    }
}