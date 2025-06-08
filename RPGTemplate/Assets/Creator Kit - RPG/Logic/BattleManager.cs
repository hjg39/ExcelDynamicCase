using Assets.Creator_Kit___RPG.Persistence;
using Assets.ExcelDomain;
using ExcelUnityPipeline;

namespace Assets.Creator_Kit___RPG.Logic
{
    public static class BattleManager
    {
        public static BattleParameters GetBattleParameters(string character, QuestionRewardClassification questionRewardClassification)
        {
            SaveManager.LoadGameData(out SaveData saveData);

            return new BattleParameters()
            {
                Challenger = character,
                QuestionId = 1,
                AllowedFunctions = saveData.UnlockedFunctions,
            };
        }
    }
}