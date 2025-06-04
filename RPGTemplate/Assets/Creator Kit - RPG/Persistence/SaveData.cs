using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Creator_Kit___RPG.Persistence
{
    [Serializable]
    public class SaveData
    {
        public bool ReceivedStarter;

        public List<string> UnlockedFunctions = new();
    }
}
