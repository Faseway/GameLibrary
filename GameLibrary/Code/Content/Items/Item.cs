using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Content.Items
{
    public class Item
    {
        // Properties
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Code 
        {
            get { return string.Concat(ItemType, ArmType, Number.ToString("00")); }
        }
        public string Context { get; set; }

        public char ItemType { get; set; }
        public char ArmType { get; set; }
        public int Number { get; set; }

        public ItemTradeFactor TradeFactor { get; private set; }
        public ItemApplyFactor ApplyFactor { get; private set; }

        // Constructor
        public Item()
        {
            ItemType = 'A';
            ArmType = 'A';
            Name = string.Concat(ItemType, ArmType, "_UNNAMED");

            TradeFactor = new ItemTradeFactor();
            ApplyFactor = new ItemApplyFactor();
        }

        public class ItemApplyFactor
        {
            int UsedType { get; set; }
            int UsedOption { get; set; }
            int AddPoint { get; set; }
            int DurationTime { get; set; }
        }

        public class ItemTradeFactor
        {
            bool Buyable { get; set; }
            int CostType { get; set; }
            int CostOptionCnt { get; set; }
            int[] BuyCost { get; set; }
            int[] AddDinar { get; set; }
            int ReqBP { get; set; }
            int ReqLvl { get; set; }
            int RandRange { get; set; }
        }
    }
}
