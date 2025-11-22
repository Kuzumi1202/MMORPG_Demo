using SkillBridge.Message;
using System.Collections.Generic;

namespace Common.Data
{
    public enum ItemFunction
    {
        RecoverHP,
        RecoverMP,
        AddBuff,
        AddExp,
        AddMoney,
        AddItem,
        AddSkillPoint,
    }

    public class ItemDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType type { get; set; }
        public string Category { get; set; }
        public bool CanUse { get; set; }
        public int Price { get; set; }
        public int SellPrice { get; set; }
        public int StackLimit { get; set; }
        public string Icon { get; set; }
        public ItemFunction Function { get; set; }
        public int Parm { get; set; }
        public List<int> Parms { get; set; }
    }
}