using Assets.Creator_Kit___RPG.Scripts.Gameplay;
using RPGM.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Creator_Kit___RPG.Persistence
{
    public static class TaggingManager
    {
        public static void ProcessTag(string tag, bool isFreshLoad)
        {
            if (!isFreshLoad)
            {
                // Save that we picked the starter
                SaveManager.SaveTag(tag);
            }

            switch (tag)
            {
                case "PickedStarter":
                    SetConversationNumber("AndrewGrigolyunovich", 1);
                    SetConversationNumber("AndrewNgai", 1);
                    SetConversationNumber("ElliottPaterson", 1);
                    SetConversationNumber("BenniWeber", 1);
                    SetConversationNumber("JaqKennedy", 1);
                    SetConversationNumber("HarryWatson", 1);

                    SetConversationNumber("ExcelWizard", 1);
                    SetConversationNumber("HarrySeiders", 1);
                    SetConversationNumber("TheHumbleMVP", 1);
                    SetConversationNumber("JulienLacaze", 1);

                    SetConversationNumber("LiannaGerrish", 1);
                    SetConversationNumber("ExcelMan", 1);
                    SetConversationNumber("LorenzoFoti", 1);
                    break;
                case "UpdateGilesConversation":
                    SetConversationNumber("TheHumbleMVP", 2);
                    break;
                case "UpdateHarrySeidersConversation":
                    SetConversationNumber("HarrySeiders", 2);
                    break;
                case "UnlockAdvanced":
                    SetConversationNumber("ExcelWizard", 2);
                    SetConversationNumber("HarrySeiders", 3);
                    SetConversationNumber("TheHumbleMVP", 3);
                    SetConversationNumber("LiannaGerrish", 2);
                    SetConversationNumber("ExcelMan", 2);
                    SetConversationNumber("LorenzoFoti", 2);
                    SetConversationNumber("AndrewGrigolyunovich", 2);
                    SetConversationNumber("JulienLacaze", 2);
                    break;
                case "UnlockExpert":
                    GameObject.FindObjectOfType<Roadblock>()?.gameObject?.SetActive(false);
                    SetConversationNumber("JulienLacaze", 3);
                    SetConversationNumber("AndrewGrigolyunovich", 3);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static void SetConversationNumber(string npcName, int conversationNumber)
        {
            GameObject obj = GameObject.Find(npcName);
            NPCController npcController = obj.GetComponent<NPCController>();
            npcController.conversationNumber = Math.Max(npcController.conversationNumber, conversationNumber);
        }
    }
}
