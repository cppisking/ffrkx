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

namespace FFRKInspector.UI
{
    public partial class FFRKViewInventory : UserControl
    {
        private ListListViewBinding<DataBuddyInformation> mBuddyList;

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

            listViewEx1.LoadSettings();

            listViewEx1.AddField(new CharacterNameField("Name"));
            listViewEx1.AddField(new CharacterLevelField("Level"));
            listViewEx1.AddField(new CharacterLevelMaxField("Max Level"));

            listViewEx1.DataBinding = mBuddyList;
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
                    mBuddyList.Collection = buddies.ToList();
                    listViewEx1.VirtualListSize = buddies.Length;
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
        }

        void FFRKProxy_OnPartyList(DataPartyDetails party)
        {
            BeginInvoke((Action)(() =>
            {
                mBuddyList.Collection = party.Buddies.ToList();
                listViewEx1.VirtualListSize = party.Buddies.Length;

                foreach (DataEquipmentInformation equip in party.Equipments)
                {
                    int row_index = dataGridView1.Rows.Add();
                    DataGridViewRow row = dataGridView1.Rows[row_index];
                    row.Tag = equip;
                    row.Cells[dgcItem.Name].Value = equip.Name;
                    row.Cells[dgcCategory.Name].Value = equip.Category;
                    row.Cells[dgcType.Name].Value = equip.Type;
                    row.Cells[dgcRarity.Name].Value = new RarityColumnValue((int)equip.BaseRarity, (int)equip.EvolutionNumber);
                    row.Cells[dgcSynergy.Name].Value = new SynergyColumnValue(RealmSynergy.FromSeries(equip.SeriesId));
                    row.Cells[dgcLevel.Name].Value = new LevelColumnValue(equip.Level, equip.LevelMax);

                    EquipStats stats = ComputeDisplayStats(equip);
                    SetStatsForRow(row, equip, stats);
                }
            }));
        }

        private void SetStatForCell(DataGridViewRow row, DataGridViewColumn col, short? actual_value, short? display_value)
        {
            if (display_value == 0 || display_value == null)
                row.Cells[col.Name].Value = null;
            else
            {
                row.Cells[col.Name].Value = display_value;
                if (actual_value.Value != display_value.Value)
                    row.Cells[col.Name].Style.ForeColor = Color.Yellow;
                else
                    row.Cells[col.Name].Style.ForeColor = Color.Black;
            }
        }

        private void SetStatsForRow(DataGridViewRow row, DataEquipmentInformation actual_stats, EquipStats display_stats)
        {
            SetStatForCell(row, dgcATK, actual_stats.Atk, display_stats.Atk);
            SetStatForCell(row, dgcMAG, actual_stats.Mag, display_stats.Mag);
            SetStatForCell(row, dgcMND, actual_stats.Mnd, display_stats.Mnd);
            SetStatForCell(row, dgcDEF, actual_stats.Def, display_stats.Def);
            SetStatForCell(row, dgcRES, actual_stats.Res, display_stats.Res);
        }

        private EquipStats ComputeDisplayStats(DataEquipmentInformation equip)
        {
            UpgradeModeComboIndex upgrade_type = (UpgradeModeComboIndex)comboBoxUpgradeMode.SelectedIndex;
            RealmSynergy.SynergyValue synergy = RealmSynergy.Values.ElementAt(comboBoxSynergy.SelectedIndex);
            DataCache.Items.Key cache_key = new DataCache.Items.Key { ItemId = equip.EquipmentId };
            DataCache.Items.Data cache_value;
            bool in_cache = FFRKProxy.Instance.Cache.Items.TryGetValue(cache_key, out cache_value);

            EquipStats result = new EquipStats();
            if (upgrade_type == UpgradeModeComboIndex.CurrentUpgradeCurrentLevel || !in_cache)
            {
                result.Atk = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesAtk : equip.Atk;
                result.Mag = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesMag : equip.Mag;
                result.Acc = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesAcc : equip.Acc;
                result.Def = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesDef : equip.Def;
                result.Res = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesRes : equip.Res;
                result.Eva = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesEva : equip.Eva;
                result.Mnd = (equip.SeriesId == synergy.GameSeries) ? equip.SeriesMnd : equip.Mnd;
            }
            else
            {
                ushort effective_level;
                if (upgrade_type == UpgradeModeComboIndex.CurrentUpgradeMaxLevel)
                    effective_level = StatCalculator.MaxLevel(equip.Rarity);
                else
                    effective_level = StatCalculator.MaxLevel(StatCalculator.Evolve(equip.BaseRarity, SchemaConstants.EvolutionLevel.PlusPlus));

                if (equip.SeriesId == synergy.GameSeries)
                    effective_level = StatCalculator.EffectiveLevelWithSynergy(effective_level);

                result.Atk = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Atk, cache_value.MaxStats.Atk, effective_level);
                result.Mag = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Mag, cache_value.MaxStats.Mag, effective_level);
                result.Acc = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Acc, cache_value.MaxStats.Acc, effective_level);
                result.Def = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Def, cache_value.MaxStats.Def, effective_level);
                result.Res = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Res, cache_value.MaxStats.Res, effective_level);
                result.Eva = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Eva, cache_value.MaxStats.Eva, effective_level);
                result.Mnd = StatCalculator.ComputeStatForLevel(equip.BaseRarity, cache_value.BaseStats.Mnd, cache_value.MaxStats.Mnd, effective_level);
            }

            return result;
        }

        private void RecomputeAllItemStats()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataEquipmentInformation equipment = row.Tag as DataEquipmentInformation;
                if (equipment == null)
                    continue;
                EquipStats stats = ComputeDisplayStats(equipment);
                SetStatsForRow(row, equipment, stats);
            }
            if (dataGridView1.SortOrder != SortOrder.None)
            {
                ListSortDirection dir = (dataGridView1.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
                dataGridView1.Sort(dataGridView1.SortedColumn, dir);
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
            //if (e.Column == dgcSynergy)
            //{
            //    RealmSynergy.SynergyValue sv1 = RealmSynergy.FromName((string)e.CellValue1);
            //    RealmSynergy.SynergyValue sv2 = RealmSynergy.FromName((string)e.CellValue2);
            //    e.SortResult = sv1.GameSeries.CompareTo(sv2.GameSeries);
            //    e.Handled = true;
            //}
            //else if (e.Column == dgcLevel)
            //{

            //}
        }
    }
}
