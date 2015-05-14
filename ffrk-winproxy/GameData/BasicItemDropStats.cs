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
        public string ItemName;
        public string BattleName;
        public uint TotalDrops;
        public uint TimesRun;
        public ushort BattleStamina;

        public float DropRate { get { return (float)TotalDrops / (float)TimesRun; } }
        public float StaminaPerDrop { get { return ((float)BattleStamina * (float)TimesRun) / (float)TotalDrops; } }
    }
}
