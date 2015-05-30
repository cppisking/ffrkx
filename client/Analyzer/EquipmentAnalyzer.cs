using FFRKInspector.GameData;
using FFRKInspector.GameData.Party;
using FFRKInspector.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Analyzer
{
    class EquipmentAnalyzer
    {
        private static readonly int kDefaultN = 3;

        public class Result
        {
            public double Score;
            public bool IsValid;
        }

        private struct SynergyDefStatCombo
        {
            public RealmSynergy.Value Synergy;
            public AnalyzerSettings.DefensiveStat Stat;
        }

        private struct SynergyOffStatCombo
        {
            public RealmSynergy.Value Synergy;
            public AnalyzerSettings.OffensiveStat Stat;
        }

        private class AnalysisBuddy
        {
            public DataBuddyInformation Buddy;
            public List<AnalysisItem> UsableItems = new List<AnalysisItem>();
            public AnalyzerSettings.PartyMemberSettings Settings;

            public List<AnalysisItem>[,] TopNOffense;
            public List<AnalysisItem>[,] TopNDefense;
        }

        private class AnalysisItem
        {
            private List<AnalysisBuddy> mUsers = new List<AnalysisBuddy>();

            public DataEquipmentInformation Item;
            public EquipStats SynergizedStats = new EquipStats();
            public EquipStats NonSynergizedStats = new EquipStats();
            public Result Result;
            public EquipStats BaseStats;
            public EquipStats MaxStats;
            public bool Ignore = false;

            public List<AnalysisBuddy> Users
            {
                get { return mUsers; }
                set { mUsers = value; }
            }

            public List<AnalysisBuddy> EnabledUsers
            {
                get { return mUsers.Where(x => x.Settings.Score).ToList(); }
            }

            public EquipStats GetEffectiveStats(RealmSynergy.SynergyValue synergy)
            {
                return (Item.SeriesId == synergy.GameSeries) ? SynergizedStats : NonSynergizedStats;
            }
        }

        private AnalyzerSettings mSettings;
        private List<AnalysisItem> mItems = new List<AnalysisItem>();
        private List<AnalysisBuddy> mBuddies = new List<AnalysisBuddy>();
        private Dictionary<uint, Result> mResults = new Dictionary<uint, Result>();
        private int mTopN = kDefaultN;

        public EquipmentAnalyzer(AnalyzerSettings Settings)
        {
            mSettings = Settings;
        }

        public EquipmentAnalyzer()
        {
            mSettings = AnalyzerSettings.DefaultSettings;
        }

        public int ItemRankThreshold
        {
            get { return mTopN; }
            set { mTopN = value; }
        }

        public double GetScore(uint EquipInstanceId)
        {
            Result result = null;
            if (!mResults.TryGetValue(EquipInstanceId, out result))
                return double.NaN;
            if (!result.IsValid)
                return double.NaN;
            return result.Score;
        }

        public DataEquipmentInformation[] Items
        {
            set
            {
                mResults.Clear();
                mItems.Clear();
                foreach (DataEquipmentInformation equip in value)
                {
                    // Lookup the item in the cache.  This gives us base stats and max stats, so we can
                    // compute effective stats.  If we don't find it, or the stats are invalid, don't score
                    // this item.
                    DataCache.Items.Key item_key = new DataCache.Items.Key { ItemId = equip.EquipmentId };
                    DataCache.Items.Data item_data = null;
                    AnalysisItem item = new AnalysisItem();
                    item.Item = equip;
                    item.Result = new Result();
                    item.Result.IsValid = true;
                    mResults[item.Item.InstanceId] = item.Result;

                    // We also don't know the formula for synergizing accessories right now, so ignore those
                    // for the time being.
                    if (!FFRKProxy.Instance.Cache.Items.TryGetValue(item_key, out item_data) 
                        || !item_data.AreStatsValid
                        || equip.Category == SchemaConstants.EquipmentCategory.Accessory)
                    {
                        item.Ignore = true;
                        item.Result.IsValid = false;
                    }
                    else
                    {
                        item.BaseStats = item_data.BaseStats;
                        item.MaxStats = item_data.MaxStats;
                    }

                    mItems.Add(item);
                }
                UpdateWhoCanUse();
            }
        }

        public DataBuddyInformation[] Buddies
        {
            set
            {
                mBuddies.Clear();
                foreach (DataBuddyInformation buddy in value)
                {
                    AnalysisBuddy instance = new AnalysisBuddy();
                    instance.Buddy = buddy;
                    instance.Settings = mSettings[buddy.BuddyId];
                    mBuddies.Add(instance);
                }
                UpdateWhoCanUse();
            }
        }

        private void UpdateWhoCanUse()
        {
            foreach (AnalysisBuddy buddy in mBuddies)
                buddy.UsableItems.Clear();
            foreach (AnalysisItem item in mItems)
                item.Users.Clear();

            if (mItems.Count == 0 || mBuddies.Count == 0)
                return;

            // For each item, build the list of potential users and vice versa.
            foreach (AnalysisBuddy buddy in mBuddies)
            {
                DataCache.Characters.Key buddy_key = new DataCache.Characters.Key { Id = buddy.Buddy.BuddyId };
                DataCache.Characters.Data buddy_data = null;
                if (!FFRKProxy.Instance.Cache.Characters.TryGetValue(buddy_key, out buddy_data))
                    continue;
                foreach (AnalysisItem item in mItems)
                {
                    if (!buddy_data.UsableEquips.Contains(item.Item.Category))
                        continue;
                    item.Users.Add(buddy);
                    buddy.UsableItems.Add(item);
                }
            }
        }

        public void Run()
        {
            // First run through and compute the effective stats of each item based
            // on the value of the item level consideration setting.
            DebugParallelForEach(mItems.Where(x => !x.Ignore),
                item =>
                {
                    byte effective_level = item.Item.Level;
                    switch (mSettings.LevelConsideration)
                    {
                        case AnalyzerSettings.ItemLevelConsideration.Current:
                            effective_level = item.Item.Level;
                            break;
                        case AnalyzerSettings.ItemLevelConsideration.CurrentRankMaxLevel:
                            effective_level = item.Item.LevelMax;
                            break;
                        case AnalyzerSettings.ItemLevelConsideration.FullyMaxed:
                            effective_level = StatCalculator.MaxLevel(
                                StatCalculator.Evolve(item.Item.BaseRarity, SchemaConstants.EvolutionLevel.PlusPlus));
                            break;
                    }

                    item.NonSynergizedStats = StatCalculator.ComputeStatsForLevel(item.Item.BaseRarity, item.BaseStats, item.MaxStats, effective_level);
                    effective_level = StatCalculator.EffectiveLevelWithSynergy(effective_level);
                    item.SynergizedStats = StatCalculator.ComputeStatsForLevel(item.Item.BaseRarity, item.BaseStats, item.MaxStats, effective_level);
                });

            RealmSynergy.SynergyValue[] synergy_values = RealmSynergy.Values.ToArray();
            AnalyzerSettings.DefensiveStat[] defensive_stats = Enum.GetValues(typeof(AnalyzerSettings.DefensiveStat)).Cast<AnalyzerSettings.DefensiveStat>().ToArray();
            AnalyzerSettings.OffensiveStat[] offensive_stats = Enum.GetValues(typeof(AnalyzerSettings.OffensiveStat)).Cast<AnalyzerSettings.OffensiveStat>().ToArray();

            List<AnalysisItem>[,] best_defensive_items = new List<AnalysisItem>[synergy_values.Length,defensive_stats.Length];
            List<AnalysisItem>[,] best_offensive_items = new List<AnalysisItem>[synergy_values.Length,offensive_stats.Length];

            // Sort the item list MxN different ways, once for each combination of (realm,stat)
            DebugParallelForEach(CartesianProduct(synergy_values, defensive_stats),
                x =>
                {
                    RealmSynergy.SynergyValue synergy = x.Key;
                    AnalyzerSettings.DefensiveStat stat = x.Value;
                    List<AnalysisItem> sorted_items = new List<AnalysisItem>(mItems.Where(y => !y.Ignore));
                    sorted_items.Sort((a,b) =>
                    {
                        EquipStats a_stats = a.GetEffectiveStats(synergy);
                        EquipStats b_stats = b.GetEffectiveStats(synergy);
                        short a_value = ChooseDefensiveStat(a_stats, stat);
                        short b_value = ChooseDefensiveStat(b_stats, stat);
                        return -a_value.CompareTo(b_value);
                    });
                    best_defensive_items[(int)synergy.Realm + 1,(int)stat] = sorted_items;
                });

            DebugParallelForEach(CartesianProduct(synergy_values, offensive_stats),
                x =>
                {
                    RealmSynergy.SynergyValue synergy = x.Key;
                    AnalyzerSettings.OffensiveStat stat = x.Value;
                    List<AnalysisItem> sorted_items = new List<AnalysisItem>(mItems.Where(y => !y.Ignore));
                    sorted_items.Sort((a, b) =>
                    {
                        EquipStats a_stats = a.GetEffectiveStats(synergy);
                        EquipStats b_stats = b.GetEffectiveStats(synergy);
                        short a_value = ChooseOffensiveStat(a_stats, stat);
                        short b_value = ChooseOffensiveStat(b_stats, stat);
                        return -a_value.CompareTo(b_value);
                    });
                    best_offensive_items[(int)synergy.Realm + 1, (int)stat] = sorted_items;
                });

            // Compute the top N items for each character.
            DebugParallelForEach(mBuddies.Where(x => x.Settings.Score),
                buddy =>
                {
                    buddy.TopNDefense = new List<AnalysisItem>[synergy_values.Length, defensive_stats.Length];
                    buddy.TopNOffense = new List<AnalysisItem>[synergy_values.Length, offensive_stats.Length];
                    for (int x=0; x < best_defensive_items.GetLength(0); ++x)
                    {
                        for (int y=0; y < best_defensive_items.GetLength(1); ++y)
                        {
                            List<AnalysisItem> best_items = best_defensive_items[x, y];
                            buddy.TopNDefense[x,y] = best_items.Where(item => item.EnabledUsers.Contains(buddy)).Take(mTopN).ToList();
                        }
                    }
                    for (int x = 0; x < best_offensive_items.GetLength(0); ++x)
                    {
                        for (int y = 0; y < best_offensive_items.GetLength(1); ++y)
                        {
                            List<AnalysisItem> best_items = best_offensive_items[x, y];
                            buddy.TopNOffense[x, y] = best_items.Where(item => item.EnabledUsers.Contains(buddy)).Take(mTopN).ToList();
                        }
                    }
                });

            // Finally, go through each item and assign it a score based on how many times it appears in someone's Top N list.
            DebugParallelForEach(mItems.Where(x => !x.Ignore),
                item =>
                {
                    bool is_weapon = false;
                    switch (item.Item.Type)
                    {
                        case SchemaConstants.ItemType.Weapon:
                            is_weapon = true;
                            break;
                        case SchemaConstants.ItemType.Armor:
                            is_weapon = false;
                            break;
                        default:
                            return;
                    }

                    // In the worst case the item appears 0 times in the top N for any character and any realm.  In the best
                    // case it appears somewhere in the top N for every character who can use it, and in every single realm.
                    // Furthermore, we weight appearances by their rank, so the max *score* is that value times the best
                    // possible rank.
                    int usable_by = item.EnabledUsers.Count;
                    int max_denormalized_score = mTopN * usable_by * synergy_values.Length;
                    int denormalized_score = 0;
                    foreach (AnalysisBuddy buddy in mBuddies.Where(b => b.Settings.Score))
                    {
                        AnalyzerSettings.PartyMemberSettings buddy_settings = mSettings[buddy.Buddy.BuddyId];
                        List<AnalysisItem>[,] rank_array = is_weapon ? buddy.TopNOffense : buddy.TopNDefense;
                        int stat_index = is_weapon ? (int)buddy_settings.OffensiveStat : (int)buddy_settings.DefensiveStat;

                        for (int realm = 0; realm < rank_array.GetLength(0); ++realm)
                        {
                            List<AnalysisItem> top_n_for_stat = rank_array[realm, stat_index];
                            int index = top_n_for_stat.IndexOf(item);
                            if (index == -1)
                                continue;
                            denormalized_score += top_n_for_stat.Count - index;
                        }
                    }
                    System.Diagnostics.Debug.Assert(denormalized_score <= max_denormalized_score);
                    item.Result.IsValid = true;
                    item.Result.Score = (double)denormalized_score / (double)max_denormalized_score;
                    item.Result.Score *= 100.0;
                });
        }

        private void DebugParallelForEach<T>(IEnumerable<T> Source, Action<T> Body)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                foreach (T Item in Source)
                    Body(Item);
            }
            else
            {
                Parallel.ForEach(Source, Body);
            }
        }

        private IEnumerable<KeyValuePair<T,U>> CartesianProduct<T,U>(IEnumerable<T> First, IEnumerable<U> Second)
        {
            foreach (T t in First)
            {
                foreach (U u in Second)
                    yield return new KeyValuePair<T, U>(t, u);
            }
        }

        private short ChooseOffensiveStat(EquipStats stats, AnalyzerSettings.OffensiveStat stat)
        {
            switch (stat)
            {
                case AnalyzerSettings.OffensiveStat.ATK:
                    return stats.Atk.GetValueOrDefault(0);
                case AnalyzerSettings.OffensiveStat.MAG:
                    return stats.Mag.GetValueOrDefault(0);
                case AnalyzerSettings.OffensiveStat.MND:
                    return stats.Mnd.GetValueOrDefault(0);
                default:
                    return stats.Atk.GetValueOrDefault(0);
            }
        }

        private short ChooseDefensiveStat(EquipStats stats, AnalyzerSettings.DefensiveStat stat)
        {
            switch (stat)
            {
                case AnalyzerSettings.DefensiveStat.DEF:
                    return stats.Def.GetValueOrDefault(0);
                case AnalyzerSettings.DefensiveStat.RES:
                    return stats.Res.GetValueOrDefault(0);
                default:
                    return stats.Def.GetValueOrDefault(0);
            }
        }
    }
}
