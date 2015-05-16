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

        private List<BasicItemDropStats> mResultSet;
        private VirtualListViewColumnSorter<BasicItemDropStats> mSorter;

        public FFRKViewItemSearch()
        {
            InitializeComponent();
        }

        private void FFRKViewItemSearch_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            mResultSet = new List<BasicItemDropStats>();
            mSorter = new VirtualListViewColumnSorter<BasicItemDropStats>();

            mSorter.AddSorter(0, (x, y) => x.ItemName.CompareTo(y.ItemName));
            mSorter.AddSorter(1, (x, y) => x.DungeonName.CompareTo(y.DungeonName));
            mSorter.AddSorter(2, (x, y) => x.BattleName.CompareTo(y.BattleName));
            mSorter.AddSorter(3, (x, y) => x.Type.ToString().CompareTo(y.Type.ToString()));
            mSorter.AddSorter(4, (x, y) => x.Rarity.CompareTo(y.Rarity));
            mSorter.AddSorter(5, (x, y) => x.Synergy.Realm.CompareTo(y.Synergy.Realm));
            mSorter.AddSorter(6, (x, y) => x.DropRate.CompareTo(y.DropRate));
            mSorter.AddSorter(7, (x, y) => x.StaminaPerDrop.CompareTo(y.StaminaPerDrop));
            mSorter.AddSorter(8, (x, y) => x.TotalDrops.CompareTo(y.TotalDrops));
            mSorter.AddSorter(9, (x, y) => x.TimesRun.CompareTo(y.TimesRun));

            listBoxItemType.Items.Clear();
            listBoxRealmSynergy.Items.Clear();
            listBoxEquippable.Items.Clear();
            listBoxWorld.Items.Clear();
            listBoxDungeon.Items.Clear();
            listBoxBattle.Items.Clear();

            listBoxItemType.Items.AddRange(
                Enum.GetValues(typeof(SchemaConstants.EquipmentCategory))
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
            DbOpFilterDrops request = new DbOpFilterDrops(FFRKProxy.Instance.Database, SearchParams);
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

        private DbOpFilterDrops.SearchParameters SearchParams
        {
            get
            {
                return new DbOpFilterDrops.SearchParameters { Name = textBoxNameFilter.Text };
            }
        }

        private void listViewResults_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex >= mResultSet.Count)
                throw new IndexOutOfRangeException();

            BasicItemDropStats item = mResultSet[e.ItemIndex];
            float drop_rate = 100.0f * (float)item.TotalDrops / (float)item.TimesRun;
            float stam_per_drop = (float)(item.TimesRun * item.BattleStamina) / (float)item.TotalDrops;
            string[] rows = new string[]
            {
                item.ItemName,
                item.EffectiveDungeonName,
                item.BattleName,
                item.Type.ToString(),
                ((byte)item.Rarity).ToString(),
                (item.Synergy == null) ? String.Empty : item.Synergy.Text,
                drop_rate.ToString("F") + "%",
                stam_per_drop.ToString("F"),
                item.TotalDrops.ToString(),
                item.TimesRun.ToString()
            };
            e.Item = new ListViewItem(rows);
        }

        private void listViewResults_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == mSorter.SortColumn)
            {
                if (mSorter.Order == SortOrder.Ascending)
                    mSorter.Order = SortOrder.Descending;
                else if (mSorter.Order == SortOrder.Descending)
                    mSorter.Order = SortOrder.Ascending;
            }
            else
            {
                mSorter.SortColumn = e.Column;
                mSorter.Order = SortOrder.Ascending;
            }
            mResultSet.Sort(mSorter.ComparisonFunction);
            listViewResults.Invalidate();
        }
    }
}
