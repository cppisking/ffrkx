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

        public FFRKViewItemSearch()
        {
            InitializeComponent();
            mBinding = new ListListViewBinding<BasicItemDropStats>();

            listViewResults.LoadSettings();

            listViewResults.AddField(new ItemNameField("Item", FieldWidthStyle.Percent, 16));
            listViewResults.AddField(new ItemDungeonField("Dungeon", FieldWidthStyle.Percent, 16));
            listViewResults.AddField(new ItemBattleField("Battle", FieldWidthStyle.Percent, 16));
            listViewResults.AddField(new ItemRarityField("Rarity"));
            listViewResults.AddField(new ItemSynergyField("Synergy"));
            listViewResults.AddField(new ItemDropsPerRunField("Drops/Run"));
            listViewResults.AddField(new ItemStaminaPerDropField("Stamina/Drop"));
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

            listBoxItemType.Items.Clear();
            listBoxRealmSynergy.Items.Clear();
            listBoxEquippable.Items.Clear();
            listBoxWorld.Items.Clear();
            listBoxDungeon.Items.Clear();
            listBoxBattle.Items.Clear();
            listBoxRarity.Items.Clear();

            listBoxItemType.Items.AddRange(
                Enum.GetValues(typeof(SchemaConstants.EquipmentCategory))
                    .Cast<object>()
                    .ToArray());

            listBoxRarity.Items.AddRange(
                Enum.GetValues(typeof(SchemaConstants.Rarity))
                    .Cast<object>()
                    .ToArray());

            listBoxRealmSynergy.Items.AddRange(RealmSynergy.Values.ToArray());
            listBoxEquippable.Items.Add("Not implemented");

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

            if (checkBoxFilterSamples.Checked)
                request.MinimumRuns.Value = Convert.ToUInt32(numericUpDown1.Value);
            if (checkBoxRepeatable.Checked)
                request.OnlyRepeatable = true;

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
                mBinding.Collection = items;
                listViewResults.VirtualListSize = mBinding.Collection.Count;
                listViewResults.Invalidate();
            }));
        }

        private void buttonResetAll_Click(object sender, EventArgs e)
        {
            listBoxItemType.SelectedItems.Clear();
            listBoxRealmSynergy.SelectedItems.Clear();
            listBoxEquippable.SelectedItems.Clear();
            listBoxWorld.SelectedItems.Clear();
            listBoxDungeon.SelectedItems.Clear();
            listBoxBattle.SelectedItems.Clear();
            listBoxRarity.SelectedItems.Clear();
            textBoxNameFilter.Clear();
            checkBoxFilterSamples.Checked = false;
            checkBoxRepeatable.Checked = false;

            mBinding.Collection.Clear();
            listViewResults.VirtualListSize = 0;
        }

        private void checkBoxFilterSamples_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = checkBoxFilterSamples.Checked;
        }

        private void FFRKViewItemSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoSearch();
            }
        }
    }
}
