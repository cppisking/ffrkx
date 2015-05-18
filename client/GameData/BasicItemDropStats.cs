using FFRKInspector.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class BasicItemDropStats
    {
        public BasicItemDropStats()
        {

        }

        public uint ItemId;
        public uint BattleId;
        public uint DungeonId;

        public string ItemName;
        public string BattleName;
        public string DungeonName;

        public SchemaConstants.DungeonType DungeonType;
        public SchemaConstants.ItemType Type;
        public SchemaConstants.Rarity Rarity;

        public RealmSynergy.SynergyValue Synergy;

        public uint TotalDrops;
        public uint TimesRun;
        public ushort BattleStamina;

        public ushort StaminaToReachBattle
        {
            get
            {
                DataCache.Battles.Key k = new DataCache.Battles.Key { BattleId = BattleId };
                DataCache.Battles.Data d;
                if (!FFRKProxy.Instance.Cache.Battles.TryGetValue(k, out d))
                    return 0;
                return d.StaminaToReach;
            }
        }

        public bool IsBattleRepeatable
        {
            get
            {
                DataCache.Battles.Key k = new DataCache.Battles.Key { BattleId = BattleId };
                DataCache.Battles.Data d;
                if (!FFRKProxy.Instance.Cache.Battles.TryGetValue(k, out d))
                    return true;
                return d.Repeatable;
            }
        }

        public string EffectiveDungeonName
        {
            get
            {
                if (DungeonType == SchemaConstants.DungeonType.Elite)
                    return DungeonName + " (Elite)";
                return DungeonName;
            }
        }

        public float DropsPerRun { get { return (float)TotalDrops / (float)TimesRun; } }
        public float StaminaPerDrop { get { return ((float)BattleStamina * (float)TimesRun) / (float)TotalDrops; } }
    }
}
