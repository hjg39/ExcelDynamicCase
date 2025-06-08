using Assets.ExcelDomain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Creator_Kit___RPG.Persistence
{
    public static class SaveManager
    {
        public const string NOFUNCTIONAVAILABLE = "NOTHING";

        private static string SaveFilePath => Path.Combine(Application.persistentDataPath, "SaveData.json");

        public static bool SaveGame(SaveData data)
        {
            try
            {
                // Ensure directory exists
                string directory = Path.GetDirectoryName(SaveFilePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                // Serialize and write
                string json = JsonUtility.ToJson(data, prettyPrint: true);
                File.WriteAllText(SaveFilePath, json);
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to save game: {e.Message}");
                return false;
            }
        }

        public static void SaveTag(string tag, int number)
        {
            LoadGameData(out SaveData saveData);
            saveData.Tags[tag] = number;
            SaveGame(saveData);
        }

        public static void SaveUnlockedFunction(string functionName)
        {
            LoadGameData(out SaveData saveData);
            if (saveData.UnlockedFunctions.Contains(functionName))
            {
                return;
            }

            saveData.UnlockedFunctions.Add(functionName);
            SaveGame(saveData);
        }

        public static void SaveUnlockedFunctions(string[] functions)
        {
            if (!functions.Any())
            {
                return;
            }

            LoadGameData(out SaveData saveData);

            List<string> unlockedFunctions = saveData.UnlockedFunctions;
            unlockedFunctions.AddRange(functions);
            unlockedFunctions = unlockedFunctions.Distinct().OrderBy(x => x).ToList();
            unlockedFunctions.Sort();

            saveData.UnlockedFunctions = unlockedFunctions;

            SaveGame(saveData);
        }

        public static string[] UnlockRandomFunctions(QuestionRewardClassification questionRewardClassification)
        {
            string[] candidateUnlocks = QuestionsDatabase.FunctionRewardsByClassification[questionRewardClassification];

            List<string> selectedFunctions = new();

            for (int i = 0; i < 3; i++)
            {
                selectedFunctions.Add(candidateUnlocks[UnityEngine.Random.Range((int)0, candidateUnlocks.Length)]);
            }

            selectedFunctions = selectedFunctions.Distinct().ToList();

            string[] validFunctionsForUnlock = GetFunctionsToUnlock(questionRewardClassification);

            string[] winners = selectedFunctions.Where(x => validFunctionsForUnlock.Contains(x)).ToArray();

            SaveUnlockedFunctions(winners);

            return winners;
        }

        public static string[] GetFunctionsToUnlock(QuestionRewardClassification questionRewardClassification)
        {
            LoadGameData(out SaveData data);

            string[] functionRewards = QuestionsDatabase.FunctionRewardsByClassification[questionRewardClassification];

            string[] unlockableRewards = functionRewards.Where(x => !data.UnlockedFunctions.Contains(x)).ToArray();

            if (unlockableRewards.Any())
            {
                return unlockableRewards;
            }

            return new string[1] { NOFUNCTIONAVAILABLE };
        }

        public static void LoadGameData(out SaveData data)
        {
            data = new SaveData();

            if (!File.Exists(SaveFilePath))
            {
                string directory = Path.GetDirectoryName(SaveFilePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                SaveData startingSaveData = new()
                {
                    Tags = new(),
                    UnlockedFunctions = new() { }
                };

                string json = JsonUtility.ToJson(startingSaveData, prettyPrint: true);
                File.WriteAllText(SaveFilePath, json);
            }

            try
            {
                string json = File.ReadAllText(SaveFilePath);
                data = JsonUtility.FromJson<SaveData>(json);
                data.UnlockedFunctions ??= new();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load game: {e.Message}");
            }
        }
    }
}
