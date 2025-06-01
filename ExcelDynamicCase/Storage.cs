using ExcelDynamicCase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelDynamicCase
{
    public static class Storage
    {
        public static Random Random = new Random();

        public const string PASSWORD = "fdfbgiskfhdspaoojoFODBSVFIUS";

        public static List<string> AllowedFormulae = new List<string>();

        public static List<int> AlreadyCompletedQuestions = new List<int>();

        public static string Rival = Domain.Rival.PossibleRivals[Random.Next(3)];

        public static List<string> GetAllLockedFunctions()
            => Functions.AllFunctions.Where(x => !AllowedFormulae.Contains(x)).ToList();

        public static List<string> GetFunctions(FunctionClass functionClass)
        {
            switch (functionClass)
            {
                case FunctionClass.Starter:
                    return Functions.StarterFunctions;
                case FunctionClass.Aggregator:
                    return Functions.AggregatorFunctions;
                case FunctionClass.Lookup:
                    return Functions.LookupFunctions;
                case FunctionClass.Manipulation:
                    return Functions.ManipulationFunctions;
                default:
                    throw new NotImplementedException();
            }
        }

        public static string GetRandomCurrentlyLockedFunction(FunctionClass functionClass)
        {
            List<string> remainingManipluationFunctions = GetFunctions(functionClass).Where(x => !AllowedFormulae.Contains(x)).ToList();

            if (!remainingManipluationFunctions.Any())
            {
                return null;
            }

            return remainingManipluationFunctions[Random.Next(remainingManipluationFunctions.Count)];
        }
    }
}