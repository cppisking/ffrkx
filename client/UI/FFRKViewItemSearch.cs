using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFRKInspector.GameData;
using FFRKInspector.Proxy;
using FFRKInspector.Database;
using FFRKInspector.UI.ListViewFields;
using FFRKInspector.GameData.AppInit;

namespace FFRKInspector.UI
{
    internal partial class FFRKViewItemSearch : UserControl
    {
        private class WorldListItem
        {
            private DataCache.Worlds.Key mKey;
            private DataCache.Worlds.Data mData;

            public WorldListItem(DataCache.Worlds.Key key, DataCache.Worlds.Data data)
            {
                mKey = key;
                mData = data;
            }

            public override string ToString() 
            {
                return mData.Name;
            }

            public uint WorldId { get { return mKey.WorldId; } }
        }

        private class DungeonListItem
        {
            private DataCache.Dungeons.Key mKey;
            private DataCache.Dungeons.Data mData;

            public DungeonListItem(DataCache.Dungeons.Key key, DataCache.Dungeons.Data data)
            {
                mKey = key;
                mData = data;
            }

            public override string ToString() 
            {
                string result = mData.Name;
                if (mData.Type == SchemaConstants.DungeonType.Elite)
                    result += " (Elite)";
                return result;
            }

            public uint DungeonId { get { return mKey.DungeonId; } }
        }

        private class BattleListItem
        {
            private DataCache.Battles.Key mKey;
            private DataCache.Battles.Data mData;

            public BattleListItem(DataCache.Battles.Key key, DataCache.Battles.Data data)
            {
                mKey = key;
                mData = data;
            }

            public override string ToString() { return mData.Name; }

            public uint BattleId { get { return mKey.BattleId; } }
        }

        private delegate string GetListViewField(BasicItemDropStats item);

        private ListListViewBinding<BasicItemDropStats> mBinding;
        private List<BasicItemDropStats> mUnfilteredResults;
        private ItemStaminaPerDropField mStaminaPerDropField;
        private bool mParametersShowing;

        public FFRKViewItemSearch()
        {
            InitializeComponent();
            mBinding = new ListListViewBinding<BasicItemDropStats>();
            mUnfilteredResults = new List<BasicItemDropStats>();
            mParametersShowing = true;

            listViewResults.LoadSettings();

            mStaminaPerDropField = new ItemStaminaPerDropField("Stamina/Drop", checkBoxUseStamToReach.Checked);
            listViewResults.AddField(new ItemNameField("Item", FieldWidthStyle.Percent, 15));
            listViewResults.AddField(new ItemWorldField("World", FieldWidthStyle.Percent, 10));
            listViewResults.AddField(new ItemDungeonField("Dungeon", FieldWidthStyle.Percent, 15));
            listViewResults.AddField(new ItemBattleField("Battle", FieldWidthStyle.Percent, 15));
            listViewResults.AddField(new ItemRarityField("Rarity"));
            listViewResults.AddField(new ItemSynergyField("Synergy"));
            listViewResults.AddField(new ItemDropsPerRunField("Drops/Run"));
            listViewResults.AddField(mStaminaPerDropField);
            listViewResults.AddField(new ItemTotalDropsField("Total Drops"));
            listViewResults.AddField(new ItemTimesRunField("Times Run"));
            listViewResults.AddField(new ItemBattleStaminaField("Stamina"));
            listViewResults.AddField(new ItemStaminaToReachField("Stamina to Reach"));
            listViewResults.AddField(new ItemRepeatableField("Is Repeatable"));

            listViewResults.DataBinding = mBinding;
        }

        private void FFRKViewItemSearch_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            listBoxRealmSynergy.Items.Clear();
            listBoxWorld.Items.Clear();
            listBoxDungeon.Items.Clear();
            listBoxBattle.Items.Clear();
            listBoxRarity.Items.Clear();

            listBoxRarity.Items.AddRange(
                Enum.GetValues(typeof(SchemaConstants.Rarity))
                    .Cast<object>()
                    .ToArray());

            listBoxRealmSynergy.Items.AddRange(RealmSynergy.Values.ToArray());

            var worlds = FFRKProxy.Instance.Cache.Worlds.ToList();
            worlds.Sort((x, y) => { return x.Value.Name.CompareTo(y.Value.Name); });
            listBoxWorld.Items.AddRange(worlds.Select(x => new WorldListItem(x.Key, x.Value)).ToArray());
            DisableDungeonList();
            DisableBattlesList();
        }

        private void listBoxWorld_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxWorld.SelectedItem == null)
                return;
            DisableBattlesList();

            WorldListItem item = (WorldListItem)listBoxWorld.SelectedItem;
            listBoxDungeon.Items.Clear();
            listBoxDungeon.Enabled = true;
            var dungeons = FFRKProxy.Instance.Cache.Dungeons.Where((x, y) => x.Value.WorldId == item.WorldId).ToList();
            dungeons.Sort((x, y) => { return x.Key.DungeonId.CompareTo(y.Key.DungeonId); });
            listBoxDungeon.Items.AddRange(dungeons.Select(x => new DungeonListItem(x.Key, x.Value)).ToArray());
        }

        private void listBoxDungeon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDungeon.SelectedItem == null)
                return;
            DungeonListItem item = (DungeonListItem)listBoxDungeon.SelectedItem;
            listBoxBattle.Items.Clear();
            listBoxBattle.Enabled = true;
            var battles = FFRKProxy.Instance.Cache.Battles.Where((x, y) => x.Value.DungeonId == item.DungeonId).ToList();
            battles.Sort((x, y) => { return x.Key.BattleId.CompareTo(y.Key.BattleId); });
            listBoxBattle.Items.AddRange(battles.Select(x => new BattleListItem(x.Key, x.Value)).ToArray());
        }

        private void listBoxBattle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxWorld_SelectionCleared(object sender, EventArgs e)
        {
            DisableDungeonList();
        }

        private void listBoxDungeon_SelectionCleared(object sender, EventArgs e)
        {
            DisableBattlesList();
        }

        private void DisableDungeonList()
        {
            listBoxDungeon.Items.Clear();
            listBoxDungeon.Items.Add("(Choose a world to populate this list)");
            listBoxDungeon.Enabled = false;
            DisableBattlesList();
        }

        private void DisableBattlesList()
        {
            listBoxBattle.Items.Clear();
            listBoxBattle.Items.Add("(Choose a dungeon to populate this list)");
            listBoxBattle.Enabled = false;
        }

        private void DoSearch()
        {
            DbOpFilterDrops request = new DbOpFilterDrops(FFRKProxy.Instance.Database);
            request.Name.Value = textBoxNameFilter.Text;
            foreach (RealmSynergy.SynergyValue value in listBoxRealmSynergy.SelectedItems)
                request.Synergies.AddValue(value);
            if (listBoxBattle.Enabled)
            {
                foreach (BattleListItem battle in listBoxBattle.SelectedItems)
                    request.Battles.AddValue(battle.BattleId);
            }
            if (listBoxDungeon.Enabled)
            {
                foreach (DungeonListItem dungeon in listBoxDungeon.SelectedItems)
                    request.Dungeons.AddValue(dungeon.DungeonId);
            }
            foreach (SchemaConstants.Rarity rarity in listBoxRarity.SelectedItems)
                request.Rarities.AddValue(rarity);

            request.OnRequestComplete += DbOpFilterDrops_OnRequestComplete;
            FFRKProxy.Instance.Database.BeginExecuteRequest(request);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        void DbOpFilterDrops_OnRequestComplete(List<BasicItemDropStats> items)
        {
            BeginInvoke((Action)(() =>
            {
                mUnfilteredResults = items;

                InplaceFilterDrops();
            }));
        }

        private void buttonResetAll_Click(object sender, EventArgs e)
        {
            listBoxRealmSynergy.SelectedItems.Clear();
            listBoxWorld.SelectedItems.Clear();
            listBoxDungeon.SelectedItems.Clear();
            listBoxBattle.SelectedItems.Clear();
            listBoxRarity.SelectedItems.Clear();
            textBoxNameFilter.Clear();
            radioButtonMinSamples.Checked = false;
            checkBoxRepeatable.Checked = false;

            mBinding.Collection.Clear();
            listViewResults.VirtualListSize = 0;
        }

        private void buttonHideParameters_Click(object sender, EventArgs e)
        {
            int offset = groupBoxItemAndLocation.Height + 6;

            if (mParametersShowing)
            {
                buttonHideParameters.Text = "Show Parameters ↓";
                mParametersShowing = false;
                offset = -offset;
            }
            else
            {
                buttonHideParameters.Text = "Hide Parameters ↑";
                mParametersShowing = true;
            }
            groupBoxItemAndLocation.Visible = mParametersShowing;

            buttonHideParameters.Top += offset;
            groupBoxSampleSize.Top += offset;
            groupBoxAdditional.Top += offset;

            int lv_top = listViewResults.Top + offset;
            int lv_bottom = listViewResults.Bottom;
            int lv_height = lv_bottom - lv_top;
            listViewResults.SetBounds(0, lv_top, 0, lv_height, BoundsSpecified.Y | BoundsSpecified.Height);
        }

        private void InplaceFilterDrops()
        {
            IEnumerable<BasicItemDropStats> filtered_items = mUnfilteredResults;

            if (radioButtonMinSamples.Checked && numericUpDownMinBattles.Value > 0)
                filtered_items = filtered_items.Where(x => x.TimesRun >= numericUpDownMinBattles.Value);
            else if (radioButtonHelp.Checked && numericUpDownLowSampleThreshold.Value > 0)
                filtered_items = filtered_items.Where(x => x.TimesRun <= numericUpDownLowSampleThreshold.Value);

            if (checkBoxRepeatable.Checked)
                filtered_items = filtered_items.Where(x => x.IsBattleRepeatable);

            if (checkBoxNoInactive.Checked)
            {
                AppInitData data = FFRKProxy.Instance.GameState.AppInitData;
                if (data != null)
                {
                    filtered_items = filtered_items.Where(x =>
                    {
                        return data.Worlds.Exists(y => y.Id == x.WorldId);
                    });
                }
            }

            mBinding.Collection = filtered_items.ToList();
            listViewResults.VirtualListSize = mBinding.Collection.Count;
            listViewResults.Invalidate();
        }

        private void radioButtonMinSamples_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownLowSampleThreshold.Enabled = radioButtonHelp.Checked;
            numericUpDownMinBattles.Enabled = radioButtonMinSamples.Checked;

            InplaceFilterDrops();
        }

        private void radioButtonHelp_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownLowSampleThreshold.Enabled = radioButtonHelp.Checked;
            numericUpDownMinBattles.Enabled = radioButtonMinSamples.Checked;

            InplaceFilterDrops();
        }

        private void radioButtonAllSamples_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownLowSampleThreshold.Enabled = radioButtonHelp.Checked;
            numericUpDownMinBattles.Enabled = radioButtonMinSamples.Checked;

            InplaceFilterDrops();
        }

        private void numericUpDownLowSampleThreshold_ValueChanged(object sender, EventArgs e)
        {
            InplaceFilterDrops();
        }

        private void numericUpDownMinBattles_ValueChanged(object sender, EventArgs e)
        {
             InplaceFilterDrops();
        }

        private void checkBoxRepeatable_CheckedChanged(object sender, EventArgs e)
        {
            InplaceFilterDrops();
        }

        private void checkBoxUseStamToReach_CheckedChanged(object sender, EventArgs e)
        {
            mStaminaPerDropField.UseStaminaToReachForNonRepeatable = checkBoxUseStamToReach.Checked;
            listViewResults.Invalidate();
        }

        private void checkBoxNoInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxNoInactive.Checked)
            {
                AppInitData data = FFRKProxy.Instance.GameState.AppInitData;
                if (data == null)
                {
                    MessageBox.Show("This feature requires FFRK Inspector to have been running when you " +
                                    "first launched FFRK.  Please close and restart FFRK and try again.");
                    checkBoxNoInactive.Checked = false;
                    return;
                }
            }
            InplaceFilterDrops();
        }
    }
}
