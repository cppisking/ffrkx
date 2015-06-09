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

        public static SchemaConstants.Rarity Evolve(SchemaConstants.Rarity rarity, int num_times)
        {
            System.Diagnostics.Debug.Assert(num_times <= 2);
            int new_rarity = (int)rarity + num_times;
            return (SchemaConstants.Rarity)new_rarity;
        }

        public static SchemaConstants.Rarity Evolve(SchemaConstants.Rarity rarity, SchemaConstants.EvolutionLevel evo)
        {
            return Evolve(rarity, (int)evo);
        }

        public static SchemaConstants.Rarity EvolveAsMuchAsPossible(SchemaConstants.Rarity base_rarity, SchemaConstants.Rarity cur_rarity, int max_times)
        {
            int times_already_upgraded = (int)cur_rarity - (int)base_rarity;
            int new_total_upgrades = Math.Min(times_already_upgraded + max_times, 2);
            return Evolve(base_rarity, new_total_upgrades);
        }

        public static short ComputeStatForLevel(SchemaConstants.Rarity base_rarity, short? base_stat, short? max_stat, byte target_level)
        {
            short bv = base_stat.GetValueOrDefault(0);
            short mv = max_stat.GetValueOrDefault(0);

            byte fully_upgraded_max_level = MaxLevel(Evolve(base_rarity, SchemaConstants.EvolutionLevel.PlusPlus));
            return ComputeStatForLevel2(bv, 1, mv, fully_upgraded_max_level, target_level);
        }

        public static short ComputeStatForLevel2(short vstat1, byte vlevel1, short vstat2, byte vlevel2, byte target_level)
        {
            double level_up_factor = ((double)vstat2 - (double)vstat1) / (double)(vlevel2 - vlevel1);
            return (short)Math.Ceiling((double)vstat1 + (double)(target_level - 1) * level_up_factor);
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
