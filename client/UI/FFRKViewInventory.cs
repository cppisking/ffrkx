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
using System.Diagnostics;

namespace FFRKInspector.UI
{
    public partial class FFRKViewInventory : UserControl
    {
        private ListListViewBinding<DataBuddyInformation> mBuddyList;

        private DataBuddyInformation[] mBuddies;
        private DataEquipmentInformation[] mEquipments;

        private AnalyzerSettings mAnalyzerSettings;
        private EquipmentAnalyzer mAnalyzer;

        private class GridEquipStats
        {
            public EquipStats Stats = new EquipStats();
            public byte Level;
            public byte MaxLevel;
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
            comboBoxUpgradeMode.SelectedIndex = (int)UpgradeModeComboIndex.CurrentUpgradeCurrentLevel;
            comboBoxSynergy.SelectedIndex = 0;
            comboBoxScoreSelection.SelectedIndex = (int)UpgradeModeComboIndex.MaxUpgradeMaxLevel;
            dgcCharacterDefensiveStat.CellTemplate = new EnumDataViewGridComboBoxCell<AnalyzerSettings.DefensiveStat>();
            dgcCharacterOffensiveStat.CellTemplate = new EnumDataViewGridComboBoxCell<AnalyzerSettings.OffensiveStat>();
        }

        private void FFRKViewInventory_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (FFRKProxy.Instance != null)
            {
                mAnalyzerSettings = AnalyzerSettings.DefaultSettings;
                mAnalyzerSettings.LevelConsideration = TranslateLevelConsideration((UpgradeModeComboIndex)comboBoxScoreSelection.SelectedIndex);
                mAnalyzer = new EquipmentAnalyzer(mAnalyzerSettings);

                FFRKProxy.Instance.OnPartyList += FFRKProxy_OnPartyList;

                DataPartyDetails party = FFRKProxy.Instance.GameState.PartyDetails;
                if (party != null)
                {
                    // Run the initial analysis before updating the grids, so we can pass the
                    // scores straight through.  We'll run the analysis again every time they
                    // change one of the settings, so that the scores update on the fly.
                    mAnalyzer.Items = party.Equipments;
                    mAnalyzer.Buddies = party.Buddies;
                    mAnalyzer.Run();
                    UpdatePartyGrid(party.Buddies.ToList());
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

            public override int GetHashCode()
            {
                return mValue.GetHashCode();
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

            public override int GetHashCode()
            {
                return mCurrentLevel + 100 * mMaxLevel;
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

            public override int GetHashCode()
            {
                return 10 * mBaseRarity + mUpgrades;
            }
        }

        private class ScoreColumnValue : IComparable
        {
            private double mScore;
            public ScoreColumnValue(double Score)
            {
                mScore = Score;
            }
        
            public int CompareTo(object obj)
            {
                ScoreColumnValue other = (ScoreColumnValue)obj;
                return mScore.CompareTo(other.mScore);
            }

            public override string ToString()
            {
                if (double.IsNaN(mScore))
                    return "N/A";
                return mScore.ToString("F");
            }

            public override bool Equals(object obj)
            {
                if (obj == null || obj == DBNull.Value)
                    return false;

                ScoreColumnValue other = (ScoreColumnValue)obj;
                return mScore == other.mScore;
            }

            public override int GetHashCode()
            {
                return mScore.GetHashCode();
            }

        }

        void FFRKProxy_OnPartyList(DataPartyDetails party)
        {
            BeginInvoke((Action)(() =>
            {
                mAnalyzer.Items = party.Equipments;
                mAnalyzer.Buddies = party.Buddies;
                mAnalyzer.Run();
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
            foreach (DataBuddyInformation info in buddies)
            {
                DataGridViewRow row = dataGridViewBuddies.Rows[cur_row];
                row.Tag = info;
                row.Cells[dgcCharacterName.Name].Value = info.Name;
                row.Cells[dgcCharacterLevel.Name].Value = info.Level;
                row.Cells[dgcCharacterMaxLevel.Name].Value = info.LevelMax;

                AnalyzerSettings.PartyMemberSettings member_settings = mAnalyzerSettings[info.Name];
                row.Cells[dgcCharacterOffensiveStat.Name].Value = member_settings.OffensiveStat;
                row.Cells[dgcCharacterDefensiveStat.Name].Value = member_settings.DefensiveStat;
                row.Cells[dgcCharacterOptimize.Name].Value = member_settings.Score;
                ++cur_row;
            }
        }

        void UpdateEquipmentGrid(DataEquipmentInformation[] EquipList)
        {
            dataGridViewEquipment.Rows.Clear();
            foreach (DataEquipmentInformation equip in EquipList)
            {
                int row_index = dataGridViewEquipment.Rows.Add();
                DataGridViewRow row = dataGridViewEquipment.Rows[row_index];
                row.Tag = equip;
                row.Cells[dgcItemID.Name].Value = equip.EquipmentId;
                row.Cells[dgcItem.Name].Value = equip.Name;
                row.Cells[dgcCategory.Name].Value = equip.Category;
                row.Cells[dgcType.Name].Value = equip.Type;
                row.Cells[dgcRarity.Name].Value = new RarityColumnValue((int)equip.BaseRarity, (int)equip.EvolutionNumber);
                row.Cells[dgcSynergy.Name].Value = new SynergyColumnValue(RealmSynergy.FromSeries(equip.SeriesId));
                row.Cells[dgcLevel.Name].Value = new LevelColumnValue(equip.Level, equip.LevelMax);
                row.Cells[dgcScore.Name].Value = new ScoreColumnValue(mAnalyzer.GetScore(equip.InstanceId));

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
                {
                    current_style.ForeColor = Color.White;
                    if (current_style.Font != null)
                        current_style.Font = new Font(current_style.Font, FontStyle.Bold);
                }
                else
                {
                    current_style.ForeColor = Color.Black;
                    if (current_style.Font != null)
                        current_style.Font = new Font(current_style.Font, FontStyle.Regular);
                }
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

        private void RecomputeAllScores()
        {
            if (mAnalyzer == null)
                return;

            mAnalyzerSettings.LevelConsideration = TranslateLevelConsideration((UpgradeModeComboIndex)comboBoxScoreSelection.SelectedIndex);
            mAnalyzer.Run();
            foreach (DataGridViewRow row in dataGridViewEquipment.Rows)
            {
                DataEquipmentInformation equip = (DataEquipmentInformation)row.Tag;
                row.Cells[dgcScore.Name].Value = new ScoreColumnValue(mAnalyzer.GetScore(equip.InstanceId));
            }
            dataGridViewEquipment.InvalidateColumn(dgcScore.Index);

            if (dataGridViewEquipment.SortedColumn == dgcScore)
                ResortByCurrentlySortedColumn();
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
            ResortByCurrentlySortedColumn();
        }

        private void ResortByCurrentlySortedColumn()
        {
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

        private bool SetValueIfDirty<T>(object source, ref T dest)
        {
            T new_value = (T)source;
            bool is_dirty = !EqualityComparer<T>.Default.Equals(dest, new_value);
            if (is_dirty)
                dest = (T)new_value;
            return is_dirty;
        }

        private AnalyzerSettings.ItemLevelConsideration TranslateLevelConsideration(UpgradeModeComboIndex mode)
        {
            switch (mode)
            {
                case UpgradeModeComboIndex.CurrentUpgradeCurrentLevel:
                    return AnalyzerSettings.ItemLevelConsideration.Current;
                case UpgradeModeComboIndex.CurrentUpgradeMaxLevel:
                    return AnalyzerSettings.ItemLevelConsideration.CurrentRankMaxLevel;
                default:
                    return AnalyzerSettings.ItemLevelConsideration.FullyMaxed;
            }
        }

        private void dataGridViewBuddies_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (mAnalyzerSettings == null)
                return;

            DataGridViewColumn col = dataGridViewBuddies.Columns[e.ColumnIndex];
            DataGridViewRow row = dataGridViewBuddies.Rows[e.RowIndex];
            DataBuddyInformation buddy = (DataBuddyInformation)row.Tag;
            AnalyzerSettings.PartyMemberSettings member_settings = mAnalyzerSettings[buddy.Name];
            if (member_settings == null)
                return;
            if (dataGridViewBuddies.CurrentCell == null)
                return;
            bool was_dirty = false;
            if (col == dgcCharacterOffensiveStat)
            {
                was_dirty = SetValueIfDirty(row.Cells[e.ColumnIndex].Value, ref member_settings.OffensiveStat);
            }
            else if (col == dgcCharacterDefensiveStat)
            {
                was_dirty = SetValueIfDirty(row.Cells[e.ColumnIndex].Value, ref member_settings.DefensiveStat);
            }
            else if (col == dgcCharacterOptimize)
            {
                was_dirty = SetValueIfDirty(row.Cells[e.ColumnIndex].Value, ref member_settings.Score);
            }

            if (was_dirty)
                RecomputeAllScores();
        }

        private void comboBoxScoreSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecomputeAllScores();
        }

        private void linkLabelMissing_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Control parent = Parent;
            while (!(parent is FFRKTabInspector))
                parent = parent.Parent;
            FFRKTabInspector tab_control = (FFRKTabInspector)parent;
            tab_control.DatabaseTab.DatabaseMode = DatabaseUI.FFRKViewDatabase.DatabaseModeEnum.MissingItems;
            tab_control.SelectedPage = FFRKTabInspector.InspectorPage.Database;
        }

        private void linkLabelAlgo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://ffrki.wordpress.com/2015/05/30/about-the-inventory-analysis-algorithm/");
        }
    }
}
