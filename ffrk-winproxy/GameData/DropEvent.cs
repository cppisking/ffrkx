using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffrk_winproxy.GameData
{
    class DropEvent
    {
        public uint EnemyId;
        public string EnemyName;
        public uint Round;
        public uint ItemId;
        public uint Rarity;
        public uint GoldAmount;
        public DataEnemyDropItem.DropItemType ItemType;
    }
}
