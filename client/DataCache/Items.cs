using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.DataCache.Items
{
    public struct Key
    {
        public uint ItemId;
    }

    public class Data
    {
        public string Name;
        public byte Rarity;
        public byte Type;
        public byte Subtype;
        public uint? Series;
        public GameData.EquipStats BaseStats;
        public GameData.EquipStats MaxStats;

        public bool AreStatsValid
        {
            get
            {
                if (BaseStats == null || MaxStats == null)
                    return false;
                if (!BaseStats.IsValid || !MaxStats.IsValid)
                    return false;
                return true;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
