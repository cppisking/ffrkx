﻿using FFRKInspector.Proxy;
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
            Histogram = new Utility.Histogram(6);
        }

        public uint ItemId;
        public uint BattleId;
        public uint DungeonId;
        public uint WorldId;

        public string ItemName;
        public string BattleName;
        public string DungeonName;
        public string WorldName;

        public SchemaConstants.DungeonType DungeonType;
        public SchemaConstants.ItemType Type;
        public SchemaConstants.Rarity Rarity;

        public RealmSynergy.SynergyValue Synergy;

        public uint TotalDrops;
        public uint TimesRun;
        public uint TimesRunWithHistogram;
        public ushort BattleStamina;

        public float DropsPerRunF; //This is only used in the filter to select the drops/run column.

        public Utility.Histogram Histogram;

        public double DropsAverage
        {
            get { return (double)TotalDrops/(double)TimesRun; }
        }

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
