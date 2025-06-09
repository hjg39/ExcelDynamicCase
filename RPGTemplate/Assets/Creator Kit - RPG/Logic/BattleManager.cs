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

            timeRemaining = GetTimeAllowedByQuestionNumber(questionId);

            return new BattleParameters()
            {
                Challenger = character,
                QuestionId = questionId,
                AllowedFunctions = saveData.UnlockedFunctions,
            };
        }

        private static int[] GetQuestionsByRewardClassification(QuestionRewardClassification questionRewardClassification)
        => questionRewardClassification switch
        {
            QuestionRewardClassification.BasicAggregates => new int[] { 3, },
            QuestionRewardClassification.AdvancedAggregates => new int[] { 7, },
            QuestionRewardClassification.ExpertAggregates => new int[] { 13, 33 },
            QuestionRewardClassification.DivineAggregates => new int[] { 16, 31, 43, 76 },
            QuestionRewardClassification.AdvancedComplex => new int[] { 8, },
            QuestionRewardClassification.ExpertBases => new int[] { 15, 40 },
            QuestionRewardClassification.BasicLookup => new int[] { 5, },
            QuestionRewardClassification.AdvancedLookup => new int[] { 9 },
            QuestionRewardClassification.BasicMaths => new int[] { 1, 6, },
            QuestionRewardClassification.ExpertMaths => new int[] { 17, },
            QuestionRewardClassification.BasicText => new int[] { 2, 11 },
            QuestionRewardClassification.AdvancedText => new int[] { 12 },
            QuestionRewardClassification.BasicManipulation => new int[] { 4, 30 },
            QuestionRewardClassification.AdvancedManipulation => new int[] { 34, },
            QuestionRewardClassification.AdvancedDates => new int[] { 36, },
            QuestionRewardClassification.ExpertDates => new int[] { 24, },
            _ => throw new NotImplementedException(),
        };

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

            _ => throw new NotImplementedException(),
        };
    }
}