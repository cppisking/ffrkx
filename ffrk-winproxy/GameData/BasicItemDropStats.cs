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
        public uint EnemyId;
        public string ItemName;
        public string BattleName;
        public string EnemyName;
        public uint TotalDrops;
        public uint TimesRun;
        public ushort BattleStamina;
    }
}
