using Assets.Creator_Kit___RPG.Persistence;
using Assets.ExcelDomain;
using ExcelUnityPipeline;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Creator_Kit___RPG.Logic
{
    public static class BattleManager
    {
        public static int LastQuestionId = 0;

        public static BattleParameters GetBattleParameters(string character, QuestionRewardClassification questionRewardClassification, out float timeRemaining)
        {
            SaveManager.LoadGameData(out SaveData saveData);

            int questionId;

            int[] allPossibleQuestions = GetQuestionsByRewardClassification(questionRewardClassification);

            List<int> possibleQuestionsThatAreNotYetComplete = allPossibleQuestions.Where(x => !saveData.CompletedQuestions.Contains(x)).ToList();

            if (possibleQuestionsThatAreNotYetComplete.Any() && UnityEngine.Random.Range(0f, 1f) < 0.75f)
            {
                // pick not previously completed
                questionId = possibleQuestionsThatAreNotYetComplete[UnityEngine.Random.Range(0, possibleQuestionsThatAreNotYetComplete.Count)];
            }
            else
            {
                questionId = allPossibleQuestions[UnityEngine.Random.Range(0, allPossibleQuestions.Length)];
            }

            LastQuestionId = questionId;
            timeRemaining = GetTimeAllowedByQuestionNumber(questionId);

            return new BattleParameters()
            {
                Challenger = character,
                QuestionId = questionId,
                AllowedFunctions = saveData.UnlockedFunctions,
            };
        }

        public static int[] GetQuestionsByRewardClassification(QuestionRewardClassification questionRewardClassification)
        {
            switch (questionRewardClassification)
            {
                case QuestionRewardClassification.BasicAggregates:
                case QuestionRewardClassification.BasicLookup:
                case QuestionRewardClassification.BasicMaths:
                case QuestionRewardClassification.BasicText:
                case QuestionRewardClassification.BasicManipulation:
                    return new int[] { 3, 20001, 30399, 40003, 40022, 5, 20002, 30379, 40008, 1, 6, 40000, 40009, 2, 11, 40001, 40013, 4, 30, 40002, 40021 };
                case QuestionRewardClassification.AdvancedAggregates:
                case QuestionRewardClassification.AdvancedComplex:
                case QuestionRewardClassification.AdvancedLookup:
                case QuestionRewardClassification.AdvancedText:
                case QuestionRewardClassification.AdvancedManipulation:
                case QuestionRewardClassification.AdvancedDates:
                    return new int[] { 7, 10015, 40011, 8, 30369, 30380, 40016, 9, 40004, 40017, 12, 40005, 40014, 34, 40006, 40018, 36, 40010, 40019, };
                case QuestionRewardClassification.ExpertAggregates:
                case QuestionRewardClassification.ExpertBases:
                case QuestionRewardClassification.ExpertMaths:
                case QuestionRewardClassification.ExpertDates:
                    return new int[] { 13, 33, 40020, 15, 40, 40007, 17, 30398, 40012, 24, 30390, 40015, };
                case QuestionRewardClassification.DivineAggregates:
                    return new int[] { 16, 31, 43, 76, 20000, 30378, 30354 };
                default:
                    throw new NotImplementedException();
            }
        }
        //=> questionRewardClassification switch
        //{
        //    //QuestionRewardClassification.BasicAggregates => new int[] { 3, 20001, 30399, 40003, 40022 },
        //    //QuestionRewardClassification.AdvancedAggregates => new int[] { 7, 10015, 40011, },
        //    //QuestionRewardClassification.ExpertAggregates => new int[] { 13, 33, 30348, },
        //    //QuestionRewardClassification.DivineAggregates => new int[] { 16, 31, 43, 76, 20000, 30378, 30354 },
        //    //QuestionRewardClassification.AdvancedComplex => new int[] { 8, 30369, 40016, },
        //    //QuestionRewardClassification.ExpertBases => new int[] { 15, 40, 40007 },
        //    //QuestionRewardClassification.BasicLookup => new int[] { 5, 20002, 30379, 40008 },
        //    //QuestionRewardClassification.AdvancedLookup => new int[] { 9, 40004, 40017 },
        //    //QuestionRewardClassification.BasicMaths => new int[] { 1, 6, 40000, 40009 },
        //    //QuestionRewardClassification.ExpertMaths => new int[] { 17, 30398, 40012, },
        //    //QuestionRewardClassification.BasicText => new int[] { 2, 11, 40001, 40013, },
        //    //QuestionRewardClassification.AdvancedText => new int[] { 12, 40005, 40014, },
        //    //QuestionRewardClassification.BasicManipulation => new int[] { 4, 30, 40002, 40021 },
        //    //QuestionRewardClassification.AdvancedManipulation => new int[] { 34, 40006, 40018, },
        //    //QuestionRewardClassification.AdvancedDates => new int[] { 36, 40010, 40019, },
        //    //QuestionRewardClassification.ExpertDates => new int[] { 24, 30390, 40015, },
        //    //_ => throw new NotImplementedException(),
        //};

        public static float GetTimeAllowedByQuestionNumber(int i)
        => i switch
        {
            1 => 5f,
            2 => 5f,
            3 => 3f,
            4 => 3f,
            5 => 3f,
            6 => 3f,
            7 => 10f,
            8 => 5f,
            9 => 5f,
            11 => 5f,
            12 => 10f,
            13 => 15f,
            15 => 10f,
            16 => 15f,
            17 => 10f,
            24 => 15f,
            30 => 8f,
            31 => 15f,
            33 => 10f,
            34 => 5f,
            36 => 10f,
            40 => 15f,
            43 => 20f,
            76 => 20f,
            10015 => 10f,
            20000 => 20f,
            20001 => 5f,
            20002 => 8f,
            30354 => 30f,
            30369 => 8f,
            30378 => 20f,
            30379 => 3f,
            30380 => 3f,
            30390 => 20f,
            30398 => 10f,
            30399 => 3f,
            40000 => 3f,
            40001 => 3f,
            40002 => 3f,
            40003 => 3f,
            40004 => 5f,
            40005 => 10f,
            40006 => 12f,
            40007 => 15f,
            40008 => 3f,
            40009 => 3f,
            40010 => 8f,
            40011 => 8f,
            40012 => 12f,
            40013 => 3f,
            40014 => 5f,
            40015 => 15f,
            40016 => 7f,
            40017 => 12f,
            40018 => 8f,
            40019 => 8f,
            40020 => 10f,
            40021 => 3f,
            40022 => 3f,
            _ => throw new NotImplementedException(),
        };
    }
}