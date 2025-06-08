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
        public static BattleParameters GetBattleParameters(string character, QuestionRewardClassification questionRewardClassification)
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
            QuestionRewardClassification.EasyAggregates => new int[] { },
            QuestionRewardClassification.AdvancedAggregates => new int[] { },
            QuestionRewardClassification.ExpertAggregates => new int[] { },
            QuestionRewardClassification.DivineAggregates => new int[] { },
            QuestionRewardClassification.AdvancedComplex => new int[] { },
            QuestionRewardClassification.ExpertBases => new int[] { },
            QuestionRewardClassification.EasyLookup => new int[] { },
            QuestionRewardClassification.AdvancedLookup => new int[] { },
            QuestionRewardClassification.EasyMaths => new int[] { 1 },
            QuestionRewardClassification.ExpertMaths => new int[] { },
            QuestionRewardClassification.EasyText => new int[] { },
            QuestionRewardClassification.AdvancedText => new int[] { },
            QuestionRewardClassification.BasicManipulation => new int[] { },
            QuestionRewardClassification.AdvancedManipulation => new int[] { },
            QuestionRewardClassification.AdvancedDates => new int[] { },
            QuestionRewardClassification.ExpertDates => new int[] { },
            _ => throw new NotImplementedException(),
        };
    }
}