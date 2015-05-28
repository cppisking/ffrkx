using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFRKInspector.Proxy;
using FFRKInspector.GameData.Party;
using FFRKInspector.UI.ListViewFields;
using FFRKInspector.GameData;
using FFRKInspector.DataCache;
using FFRKInspector.Analyzer;

namespace FFRKInspector.UI
{
    public partial class FFRKViewInventory : UserControl
    {
        private ListListViewBinding<DataBuddyInformation> mBuddyList;
        private AnalyzerSettings mAnalyzerSettings;

        private class GridEquipStats
        {
            public EquipStats Stats = new EquipStats();
            public ushort Level;
            public ushort MaxLevel;
        }

        private enum ViewModeComboIndex
        {
            AvailableItems = 0,
            BestItems = 1
        }

        private enum UpgradeModeComboIndex
        {
            CurrentUpgradeCurrentLevel,
            CurrentUpgradeMaxLevel,
            MaxUpgradeMaxLevel
        }

        private class SynergyFormatter
        {
            private RealmSynergy.SynergyValue mSynergy;
            public SynergyFormatter(RealmSynergy.SynergyValue Synergy)
            {
                mSynergy = Synergy;
            }

            public override string ToString()
            {
                if (mSynergy.Realm == RealmSynergy.Value.None)
                    return "No";
                else
                    return mSynergy.Realm.ToString();
            }
        }

        public FFRKViewInventory()
        {
            InitializeComponent();

            mBuddyList = new ListListViewBinding<DataBuddyInformation>();

            foreach (RealmSynergy.SynergyValue synergy in RealmSynergy.Values)
                comboBoxSynergy.Items.Add(new SynergyFormatter(synergy));
            comboBoxViewMode.SelectedIndex = 0;
            comboBoxUpgradeMode.SelectedIndex = 0;
            comboBoxSynergy.SelectedIndex = 0;
        }

        private void FFRKViewInventory_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (FFRKProxy.Instance != null)
            {
                FFRKProxy.Instance.OnPartyList += FFRKProxy_OnPartyList;

                DataPartyDetails party = FFRKProxy.Instance.GameState.PartyDetails;
                if (party != null)
                {
                    DataBuddyInformation[] buddies = party.Buddies;
                    UpdatePartyGrid(buddies.ToList());
                    UpdateEquipmentGrid(party.Equipments);
                }
            }
        }

        private class SynergyColumnValue : IComparable
        {
            private RealmSynergy.SynergyValue mValue;
            public SynergyColumnValue(RealmSynergy.SynergyValue Value)
            {
                mValue = Value;
            }
        
            public int CompareTo(object obj)
            {
                return mValue.GameSeries.CompareTo(((SynergyColumnValue)obj).mValue.GameSeries);
            }

            public override string ToString()
            {
                return mValue.Text;
            }

            public override bool Equals(object obj)
            {
                if (obj == null || obj == DBNull.Value)
                    return false;

                SynergyColumnValue other = (SynergyColumnValue)obj;
                return (mValue == other.mValue);
            }
        }

        private class LevelColumnValue : IComparable
        {
            private int mCurrentLevel;
            private int mMaxLevel;

            public LevelColumnValue(int Current, int Max)
            {
                mCurrentLevel = Current;
                mMaxLevel = Max;
            }
        
            public int CompareTo(object obj)
            {
                LevelColumnValue other = (LevelColumnValue)obj;
                int current_result = mCurrentLevel.CompareTo(other.mCurrentLevel);
                if (current_result != 0)
                    return current_result;
                return mMaxLevel.CompareTo(other.mMaxLevel);
            }

            public override string ToString()
            {
                return String.Format("{0}/{1}", mCurrentLevel, mMaxLevel);
            }

            public override bool Equals(object obj)
            {
                if (obj == null || obj == DBNull.Value)
                    return false;

                LevelColumnValue other = (LevelColumnValue)obj;
                return (mCurrentLevel == other.mCurrentLevel) && (mMaxLevel == other.mMaxLevel);
            }
        }

        private class RarityColumnValue : IComparable
        {
            private int mBaseRarity;
            private int mUpgrades;

            public RarityColumnValue(int BaseRarity, int Upgrades)
            {
                mBaseRarity = BaseRarity;
                mUpgrades = Upgrades;
            }
        
            public int CompareTo(object obj)
            {
                RarityColumnValue other = (RarityColumnValue)obj;
                int current_result = mBaseRarity.CompareTo(other.mBaseRarity);
                if (current_result != 0)
                    return current_result;
                return mUpgrades.CompareTo(other.mUpgrades);
            }

            public override string ToString()
            {
                string result = mBaseRarity.ToString() + new string('+', mUpgrades);
                return result;
            }

            public override bool Equals(object obj)
            {
                if (obj == null || obj == DBNull.Value)
                    return false;

                RarityColumnValue other = (RarityColumnValue)obj;
                return (mBaseRarity == other.mBaseRarity) && (mUpgrades == other.mUpgrades);
            }
        }

        void FFRKProxy_OnPartyList(DataPartyDetails party)
        {
            BeginInvoke((Action)(() =>
            {
                UpdatePartyGrid(party.Buddies.ToList());
                UpdateEquipmentGrid(party.Equipments);
            }));
        }

        private void UpdatePartyGrid(List<DataBuddyInformation> buddies)
        {
            mBuddyList.Collection = buddies;

            dataGridViewBuddies.Rows.Clear();
            dataGridViewBuddies.Rows.Add(buddies.Count);
            int cur_row = 0;
            AnalyzerSettings settings = mAnalyzerSettings;
            if (settings == null)
                settings = AnalyzerSettings.DefaultSettings;
            foreach (DataBuddyInformation info in buddies)
            {
                DataGridViewRow row = dataGridViewBuddies.Rows[cur_row];
                row.Tag = info;
                row.Cells[dgcCharacterName.Name].Value = info.Name;
                row.Cells[dgcCharacterLevel.Name].Value = info.Level;
                row.Cells[dgcCharacterMaxLevel.Name].Value = info.LevelMax;
                AnalyzerSettings.PartyMemberSettings member_settings = settings[info.Name];

                ((DataGridViewCheckBoxCell)row.Cells[dgcCharacterOptimize.Name]).Value = member_settings.Score;
                ((DataGridViewComboBoxCell)row.Cells[dgcCharacterOffensiveStat.Name]).Value = member_settings.OffensiveStat.ToString();
                ((DataGridViewComboBoxCell)row.Cells[dgcCharacterDefensiveStat.Name]).Value = member_settings.DefensiveStat.ToString();
                ++cur_row;
            }
        }

        void UpdateEquipmentGrid(DataEquipmentInformation[] EquipList)
        {
            foreach (DataEquipmentInformation equip in EquipList)
            {
                int row_index = dataGridViewEquipment.Rows.Add();
                DataGridViewRow row = dataGridViewEquipment.Rows[row_index];
                row.Tag = equip;
                row.Cells[dgcItem.Name].Value = equip.Name;
                row.Cells[dgcCategory.Name].Value = equip.Category;
                row.Cells[dgcType.Name].Value = equip.Type;
                row.Cells[dgcRarity.Name].Value = new RarityColumnValue((int)equip.BaseRarity, (int)equip.EvolutionNumber);
                row.Cells[dgcSynergy.Name].Value = new SynergyColumnValue(RealmSynergy.FromSeries(equip.SeriesId));
                row.Cells[dgcLevel.Name].Value = new LevelColumnValue(equip.Level, equip.LevelMax);

                GridEquipStats stats = ComputeDisplayStats(equip);
                SetStatsForRow(row, equip, stats);
            }
        }

        private void SetStatForCell<T>(DataGridViewRow row, DataGridViewColumn col, T actual_value, T display_value)
        {
            if (display_value.Equals(default(T)) || display_value == null)
                row.Cells[col.Name].Value = null;
            else
            {
                row.Cells[col.Name].Value = display_value;
                DataGridViewCellStyle current_style = row.Cells[col.Name].Style;
                if (!actual_value.Equals(display_value))
                    current_style.ForeColor = Color.Yellow;
                else
                    current_style.ForeColor = Color.Black;
            }
        }

        private void SetStatsForRow(DataGridViewRow row, DataEquipmentInformation actual_stats, GridEquipStats display_stats)
        {
            SetStatForCell(row, dgcLevel,
                                new LevelColumnValue(actual_stats.Level, actual_stats.LevelMax),
                                new LevelColumnValue(display_stats.Level, display_stats.MaxLevel));
            SetStatForCell(row, dgcATK, actual_stats.Atk, display_stats.Stats.Atk);
            SetStatForCell(row, dgcMAG, actual_stats.Mag, display_stats.Stats.Mag);
            SetStatForCell(row, dgcMND, actual_stats.Mnd, display_stats.Stats.Mnd);
            SetStatForCell(row, dgcDEF, actual_stats.Def, display_stats.Stats.Def);
            SetStatForCell(row, dgcRES, actual_stats.Res, display_stats.Stats.Res);
        }

        private GridEquipStats ComputeDisplayStats(DataEquipmentInformation equip)
        {
            UpgradeModeComboIndex upgrade_type = (UpgradeModeComboIndex)comboBoxUpgradeMode.SelectedIndex;
            RealmSynergy.SynergyValue synergy = RealmSynergy.Values.ElementAt(comboBoxSynergy.SelectedIndex);
            DataCache.Items.Key cache_key = new DataCache.Items.Key { ItemId = equip.EquipmentId };
            DataCache.Items.Data cache_value;
            bool in_cache = FFRKProxy.Instance.Cache.Items.TryGetValue(cache_key, out cache_value);

            GridEquipStats result = new GridEquipStats();
            if (upgrade_type == UpgradeModeComboIndex.CurrentUpgradeCurrentLevel || !in_cache)
            {
                result.Stats.Atk = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesAtk : equip.Atk;
                result.Stats.Mag = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesMag : equip.Mag;
                result.Stats.Acc = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesAcc : equip.Acc;
                result.Stats.Def = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesDef : equip.Def;
                result.Stats.Res = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesRes : equip.Res;
                result.Stats.Eva = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesEva : equip.Eva;
                result.Stats.Mnd = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesMnd : equip.Mnd;
                result.Level = equip.Level;
                result.MaxLevel = equip.LevelMax;
                if (equip.SeriesId == synergy.GameSeries)
                    result.Level = StatCalculator.EffectiveLevelWithSynergy(result.Level);
            }
            else
            {
                
                if (upgrade_type == UpgradeModeComboIndex.CurrentUpgradeMaxLevel)
                    result.MaxLevel = StatCalculator.MaxLevel(equip.Rarity);
                else
                    result.MaxLevel = StatCalculator.MaxLevel(StatCalculator.Evolve(equip.BaseRarity, SchemaConstants.EvolutionLevel.PlusPlus));
                result.Level = result.MaxLevel;
                if (equip.SeriesId == synergy.GameSeries)
                    result.Level = StatCalculator.EffectiveLevelWithSynergy(result.Level);

                result.Stats.Atk = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Atk, cache_value.MaxStats.Atk, result.Level);
                result.Stats.Mag = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Mag, cache_value.MaxStats.Mag, result.Level);
                result.Stats.Acc = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Acc, cache_value.MaxStats.Acc, result.Level);
                result.Stats.Def = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Def, cache_value.MaxStats.Def, result.Level);
                result.Stats.Res = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Res, cache_value.MaxStats.Res, result.Level);
                result.Stats.Eva = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Eva, cache_value.MaxStats.Eva, result.Level);
                result.Stats.Mnd = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Mnd, cache_value.MaxStats.Mnd, result.Level);
            }

            return result;
        }

        private void RecomputeAllItemStats()
        {
            foreach (DataGridViewRow row in dataGridViewEquipment.Rows)
            {
                DataEquipmentInformation equipment = row.Tag as DataEquipmentInformation;
                if (equipment == null)
                    continue;
                GridEquipStats stats = ComputeDisplayStats(equipment);
                SetStatsForRow(row, equipment, stats);
            }
            if (dataGridViewEquipment.SortOrder != SortOrder.None)
            {
                ListSortDirection dir = (dataGridViewEquipment.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
                dataGridViewEquipment.Sort(dataGridViewEquipment.SortedColumn, dir);
            }
        }

        private void comboBoxViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecomputeAllItemStats();
        }

        private void comboBoxUpgradeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecomputeAllItemStats();
        }

        private void comboBoxSynergy_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecomputeAllItemStats();
        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
        }
    }
}
