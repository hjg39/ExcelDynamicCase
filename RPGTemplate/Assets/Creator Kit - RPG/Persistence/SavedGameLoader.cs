using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Creator_Kit___RPG.Persistence
{
    public class SavedGameLoader : MonoBehaviour
    {
        void Start()
        {
            SaveManager.LoadGameData(out SaveData saveData);

            foreach (string tag in saveData.Tags)
            {
                TaggingManager.ProcessTag(tag, true);
            }
        }
    }
}
