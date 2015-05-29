using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    static class StatCalculator
    {
        public static byte MaxLevel(SchemaConstants.Rarity rarity)
        {
            if (rarity == SchemaConstants.Rarity.One)
                return 3;
            return (byte)(5 * ((int)rarity - 1));
        }

        public static SchemaConstants.Rarity Evolve(SchemaConstants.Rarity rarity, SchemaConstants.EvolutionLevel evo)
        {
            return (SchemaConstants.Rarity)((int)rarity + (int)evo);
        }

        public static short ComputeStatForLevel(SchemaConstants.Rarity base_rarity, short? base_stat, short? max_stat, byte target_level)
        {
            short bv = base_stat.GetValueOrDefault(0);
            short mv = max_stat.GetValueOrDefault(0);

            ushort fully_upgraded_max_level = MaxLevel(Evolve(base_rarity, SchemaConstants.EvolutionLevel.PlusPlus));
            double level_up_factor = ((double)mv - (double)bv) / (double)(fully_upgraded_max_level - 1);
            return (short)Math.Ceiling((double)bv + (double)(target_level - 1) * level_up_factor);
        }

        public static byte EffectiveLevelWithSynergy(byte current_level)
        {
            if (current_level < 5)
                return (byte)(current_level + 15);
            byte bonus_levels = (byte)(20 + 10 * (current_level / 5 - 1));
            return (byte)(current_level + bonus_levels);
        }

        public static EquipStats ComputeStatsForLevel(SchemaConstants.Rarity base_rarity, EquipStats base_stats, EquipStats max_stats, byte target_level)
        {
            return new EquipStats
            {
                Acc = ComputeStatForLevel(base_rarity, base_stats.Acc, max_stats.Acc, target_level),
                Atk = ComputeStatForLevel(base_rarity, base_stats.Atk, max_stats.Atk, target_level),
                Def = ComputeStatForLevel(base_rarity, base_stats.Def, max_stats.Def, target_level),
                Res = ComputeStatForLevel(base_rarity, base_stats.Res, max_stats.Res, target_level),
                Eva = ComputeStatForLevel(base_rarity, base_stats.Eva, max_stats.Eva, target_level),
                Mag = ComputeStatForLevel(base_rarity, base_stats.Mag, max_stats.Mag, target_level),
                Mnd = ComputeStatForLevel(base_rarity, base_stats.Mnd, max_stats.Mnd, target_level)
            };
        }
    }
}
