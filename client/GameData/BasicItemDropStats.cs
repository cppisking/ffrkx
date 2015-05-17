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
