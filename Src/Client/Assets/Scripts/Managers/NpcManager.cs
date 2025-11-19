using Common.Data;
using System;
using System.Collections.Generic;

namespace Managers
{
    internal class NpcManager : Singleton<NpcManager>
    {
        public delegate bool NpcActionalHandler(NpcDefine npc);

        private Dictionary<NpcFunction, NpcActionalHandler> eventMap = new Dictionary<NpcFunction, NpcActionalHandler>();

        public void RegisterNpcEvent(NpcFunction function, NpcActionalHandler npcAction)
        {
            if (!eventMap.ContainsKey(function))
            {
                eventMap[function] = npcAction;
            }
            else
            {
                eventMap[function] += npcAction;
            }
        }

        public NpcDefine GetNpcDefine(int npcID)
        {
            NpcDefine npcDefine = null;
            DataManager.Instance.Npcs.TryGetValue(npcID, out npcDefine);
            return npcDefine;
        }

        public bool Interactive(int npcID)
        {
            if (DataManager.Instance.Npcs.ContainsKey(npcID))
            {
                var npc = DataManager.Instance.Npcs[npcID];
                return Interactive(npc);
            }
            return false;
        }

        public bool Interactive(NpcDefine npc)
        {
            if (npc.Type == NpcType.Task)
            {
                return DoTaskInterative(npc);
            }
            else if (npc.Type == NpcType.Functional)
            {
                return DoFunctionInteractive(npc);
            }
            return false;
        }

        private bool DoTaskInterative(NpcDefine npc)
        {
            MessageBox.Show("点击了对话NPC" + npc.Name);
            return true;
        }

        private bool DoFunctionInteractive(NpcDefine npc)
        {
            if (npc.Type != NpcType.Functional)
                return false;
            if(!eventMap.ContainsKey(npc.Function))
                return false;
            return eventMap[npc.Function](npc);
        }
    }
}