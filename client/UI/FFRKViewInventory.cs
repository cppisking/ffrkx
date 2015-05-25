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
                    row.Cells[dgcRarity.Name].Value = (int)equip.Rarity;
                    row.Cells[dgcSynergy.Name].Value = RealmSynergy.FromSeries(equip.SeriesId).Text;
                    row.Cells[dgcLevel.Name].Value = String.Format("{0}/{1}", equip.Level, equip.LevelMax);

                    EquipStats stats = ComputeDisplayStats(equip);
                    SetStatsForRow(row, stats);
                }
            }));
        }

        private void SetStatForCell(DataGridViewRow row, DataGridViewColumn col, short? value)
        {
            if (value == 0 || value == null)
                row.Cells[col.Name].Value = null;
            else
                row.Cells[col.Name].Value = value;
        }

        private void SetStatsForRow(DataGridViewRow row, EquipStats stats)
        {
            SetStatForCell(row, dgcATK, stats.Atk);
            SetStatForCell(row, dgcMAG, stats.Mag);
            SetStatForCell(row, dgcMND, stats.Mnd);
            SetStatForCell(row, dgcDEF, stats.Def);
            SetStatForCell(row, dgcRES, stats.Res);
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
                SetStatsForRow(row, stats);
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
    }
}
