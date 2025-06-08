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
            QuestionRewardClassification.ExpertAggregates => new int[] { },
            QuestionRewardClassification.DivineAggregates => new int[] { },
            QuestionRewardClassification.AdvancedComplex => new int[] { 8, },
            QuestionRewardClassification.ExpertBases => new int[] { },
            QuestionRewardClassification.BasicLookup => new int[] { 5, },
            QuestionRewardClassification.AdvancedLookup => new int[] { 9 },
            QuestionRewardClassification.BasicMaths => new int[] { 1, 6, },
            QuestionRewardClassification.ExpertMaths => new int[] { },
            QuestionRewardClassification.BasicText => new int[] { 2, 11 },
            QuestionRewardClassification.AdvancedText => new int[] { },
            QuestionRewardClassification.BasicManipulation => new int[] { 4 },
            QuestionRewardClassification.AdvancedManipulation => new int[] { },
            QuestionRewardClassification.AdvancedDates => new int[] { },
            QuestionRewardClassification.ExpertDates => new int[] { },
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
            _ => throw new NotImplementedException(),
        };
    }
}