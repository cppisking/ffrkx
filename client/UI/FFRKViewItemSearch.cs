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

        private List<BasicItemDropStats> mResultSet;
        private VirtualListViewFieldManager<BasicItemDropStats> mFieldManager;

        public FFRKViewItemSearch()
        {
            InitializeComponent();
        }

        private void FFRKViewItemSearch_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            mResultSet = new List<BasicItemDropStats>();
            mFieldManager = new VirtualListViewFieldManager<BasicItemDropStats>();

            mFieldManager.AddColumn(columnHeaderName,
                                    (x, y) => x.ItemName.CompareTo(y.ItemName), 
                                    x => x.ItemName);
            mFieldManager.AddColumn(columnHeaderDungeon,
                                    (x, y) => x.DungeonName.CompareTo(y.DungeonName),
                                    x => x.EffectiveDungeonName);
            mFieldManager.AddColumn(columnHeaderBattle,
                                    (x, y) => x.BattleName.CompareTo(y.BattleName), 
                                    x => x.BattleName);
            mFieldManager.AddColumn(columnHeaderRarity, 
                                    (x, y) => x.Rarity.CompareTo(y.Rarity), 
                                    x => ((byte)x.Rarity).ToString());
            mFieldManager.AddColumn(columnHeaderSynergy, 
                                    (x, y) => {
                                        if (x.Synergy == y.Synergy) return 0;
                                        if (x.Synergy == null) return -1;
                                        if (y.Synergy == null) return 1;
                                        return x.Synergy.Realm.CompareTo(y.Synergy.Realm);
                                    }, 
                                    x => (x.Synergy == null) ? String.Empty : x.Synergy.Text);
            mFieldManager.AddColumn(columnHeaderDropsPerRun, 
                                    (x, y) => x.DropsPerRun.CompareTo(y.DropsPerRun), 
                                    x => x.DropsPerRun.ToString("F"));
            mFieldManager.AddColumn(columnHeaderStamDrop,
                                    (x, y) => x.StaminaPerDrop.CompareTo(y.StaminaPerDrop),
                                    x => x.StaminaPerDrop.ToString("F"));
            mFieldManager.AddColumn(columnHeaderNumDrops, 
                                    (x, y) => x.TotalDrops.CompareTo(y.TotalDrops),
                                    x => x.TotalDrops.ToString());
            mFieldManager.AddColumn(columnHeaderTimesRun, 
                                    (x, y) => x.TimesRun.CompareTo(y.TimesRun),
                                    x => x.TimesRun.ToString());
            mFieldManager.AddColumn(columnHeaderStamToReach,
                                    (x, y) => x.StaminaToReachBattle.CompareTo(y.StaminaToReachBattle),
                                    x => x.StaminaToReachBattle.ToString());
            mFieldManager.AddColumn(columnHeaderRepeatable,
                                    (x, y) => x.IsBattleRepeatable.CompareTo(y.IsBattleRepeatable),
                                    x => x.IsBattleRepeatable.ToString());

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

        private void buttonSearch_Click(object sender, EventArgs e)
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

        void DbOpFilterDrops_OnRequestComplete(List<BasicItemDropStats> items)
        {
            BeginInvoke((Action)(() =>
            {
                mResultSet = items;
                listViewResults.VirtualListSize = mResultSet.Count;
                listViewResults.Invalidate();
            }));
        }

        private void listViewResults_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex >= mResultSet.Count)
                throw new IndexOutOfRangeException();

            BasicItemDropStats item = mResultSet[e.ItemIndex];
            List<string> fields = new List<string>();
            foreach (ColumnHeader column in listViewResults.Columns)
            {
                string value = mFieldManager.GetFieldValue(column, item);
                fields.Add(value);
            }
            e.Item = new ListViewItem(fields.ToArray());
        }

        private void listViewResults_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listViewResults.Columns[e.Column] == mFieldManager.SortColumn)
            {
                if (mFieldManager.Order == SortOrder.Ascending)
                    mFieldManager.Order = SortOrder.Descending;
                else if (mFieldManager.Order == SortOrder.Descending)
                    mFieldManager.Order = SortOrder.Ascending;
            }
            else
            {
                mFieldManager.SortColumn = listViewResults.Columns[e.Column];
                mFieldManager.Order = SortOrder.Ascending;
            }
            mResultSet.Sort(mFieldManager.ComparisonFunction);
            listViewResults.Invalidate();
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

            mResultSet.Clear();
            listViewResults.VirtualListSize = 0;
        }
    }
}
