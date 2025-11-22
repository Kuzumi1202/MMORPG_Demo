using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models
{
    internal class Item
    {
        private TCharacterItem dbItem;

        public int ItemID;
        public int Count;

        public Item(TCharacterItem item)
        {
            this.dbItem = item;

            this.ItemID = (short)item.ItemID;
            this.Count = (short)item.ItemCount;
        }

        public void Add(int Count)
        {
            this.Count += Count;
            dbItem.ItemCount = this.Count;
        }

        public void Remove(int Count)
        {
            this.Count -= Count;
            dbItem.ItemCount -= this.Count;
        }

        public bool Use(int count = 1)
        {
            return false;
        }

        public override string ToString()
        {
            return string.Format("ID:{0},Count:{1}", this.ItemID, this.Count);
        }
    }
}