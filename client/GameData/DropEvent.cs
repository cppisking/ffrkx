using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DropEvent
    {
        public uint EnemyId;
        public string EnemyName;
        public uint Round;
        public uint ItemId;
        public uint Rarity;
        public uint GoldAmount;
        public uint NumberOfItems;
        public DataEnemyDropItem.DropItemType ItemType;
        public string MateriaName;
        public string PotionName;
    }
}
