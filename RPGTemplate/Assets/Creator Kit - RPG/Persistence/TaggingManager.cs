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

                    GameObject obj = GameObject.Find("AndrewG");
                    NPCController npcController = obj.GetComponent<NPCController>();
                    npcController.conversationNumber = 1;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
