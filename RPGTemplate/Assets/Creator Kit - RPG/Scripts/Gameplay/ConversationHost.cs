using RPGM.Gameplay;
using UnityEngine;

namespace Assets.Creator_Kit___RPG.Scripts.Gameplay
{
    public static class ConversationHost
    {
        private static ConversationScript _instance;

        public static ConversationScript Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("ConversationScriptHost");
                    Object.DontDestroyOnLoad(go);
                    _instance = go.AddComponent<ConversationScript>();
                }
                return _instance;
            }
        }
    }
}
