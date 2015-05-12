using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.DataCache.DungeonDrops
{
    public class Key
    {
        public uint ItemId;
        public uint BattleId;
        public uint EnemyId;

        public override int GetHashCode()
        {
            return ItemId.GetHashCode() ^ BattleId.GetHashCode() ^ EnemyId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Key other = obj as Key;
            if (other == null)
                return false;
            return (ItemId == other.ItemId) && (BattleId == other.BattleId) && (EnemyId == other.EnemyId);
        }
    }

    public class Data
    {
        public string ItemName;
        public string BattleName;
        public string EnemyName;
        public uint NumDrops;
    }
}
