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
            _ = LoadGameData(out SaveData saveData);
            saveData.Tags[tag] = number;
            SaveGame(saveData);
        }

        public static void SaveUnlockedFunction(string functionName)
        {
            _ = LoadGameData(out SaveData saveData);
            if (saveData.UnlockedFunctions.Contains(functionName))
            {
                return;
            }

            saveData.UnlockedFunctions.Add(functionName);
            SaveGame(saveData);
        }

        public static bool LoadGameData(out SaveData data)
        {
            data = null;

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
                return true;

            }

            try
            {
                string json = File.ReadAllText(SaveFilePath);
                data = JsonUtility.FromJson<SaveData>(json);
                data.UnlockedFunctions ??= new();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load game: {e.Message}");
                return false;
            }
        }
    }
}
