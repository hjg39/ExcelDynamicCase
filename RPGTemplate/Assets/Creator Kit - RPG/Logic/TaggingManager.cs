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
            switch (tag)
            {
                case "PickedStarter":
                    if (!isFreshLoad)
                    {
                        // Save that we picked the starter
                        SaveManager.SaveTag(tag, 1);
                    }

                    SetConversationNumber("AndrewGrigolyunovich", 1);
                    SetConversationNumber("AndrewNgai", 1);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static void SetConversationNumber(string npcName, int conversationNumber)
        {
            GameObject obj = GameObject.Find(npcName);
            NPCController npcController = obj.GetComponent<NPCController>();
            npcController.conversationNumber = conversationNumber;
        }
    }
}
