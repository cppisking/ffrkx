using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    static class StatCalculator
    {
        public static ushort MaxLevel(SchemaConstants.Rarity rarity)
        {
            if (rarity == SchemaConstants.Rarity.One)
                return 3;
            return (ushort)(5 * ((int)rarity - 1));
        }

        public static SchemaConstants.Rarity Evolve(SchemaConstants.Rarity rarity, SchemaConstants.EvolutionLevel evo)
        {
            return (SchemaConstants.Rarity)((int)rarity + (int)evo);
        }

        public static short ComputeStatForLevel(SchemaConstants.Rarity base_rarity, short? base_stat, short? max_stat, ushort target_level)
        {
            short bv = base_stat.GetValueOrDefault(0);
            short mv = max_stat.GetValueOrDefault(0);

            ushort fully_upgraded_max_level = MaxLevel(Evolve(base_rarity, SchemaConstants.EvolutionLevel.PlusPlus));
            double level_up_factor = ((double)mv - (double)bv) / (double)(fully_upgraded_max_level - 1);
            return (short)Math.Ceiling((double)bv + (double)(target_level - 1) * level_up_factor);
        }

        public static ushort EffectiveLevelWithSynergy(ushort current_level)
        {
            if (current_level < 5)
                return (ushort)(current_level + 15);
            ushort bonus_levels = (ushort)(20 + 10 * (current_level / 5 - 1));
            return (ushort)(current_level + bonus_levels);
        }
    }
}
