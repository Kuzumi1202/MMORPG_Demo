using GameServer.Entities;
using SkillBridge.Message;
using System.Collections.Generic;

namespace GameServer.Managers
{
    internal class StatusManager
    {
        private Character Owner;
        private List<NStatus> Status { get; set; }

        public bool HasStatus
        {
            get { return this.Status.Count > 0; }
        }

        public StatusManager(Character owner)
        {
            this.Owner = owner;
            this.Status = new List<NStatus>();
        }

        public void AddStatus(StatusType type, int id, int value, StatusAction action)
        {
            this.Status.Add(new NStatus()
            {
                Type = type,
                Id = id,
                Value = value,
                Action = action
            });
        }

        public void AddGoldChange(int goldData)
        {
            if (goldData > 0)
            {
                this.AddStatus(StatusType.Money, 0, goldData, StatusAction.Add);
            }
            if (goldData < 0)
            {
                this.AddStatus(StatusType.Money, 0, -goldData, StatusAction.Delete);
            }
        }

        public void AddItemChange(int id, int count, StatusAction action)
        {
            this.AddStatus(StatusType.Item, id, count, action);
        }

        public void ApplyResponse(NetMessageResponse message) 
        {
            if(message.statusNotify == null) 
            {
                message.statusNotify = new StatusNotify();
            }
            foreach(var status in this.Status) 
            {
                message.statusNotify.Status.Add(status);
            }
            this.Status.Clear();
        }
    }
}