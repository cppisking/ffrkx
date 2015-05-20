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
using FFRKInspector.DataCache;

namespace FFRKInspector.UI
{
    internal partial class FFRKViewActiveDungeon : UserControl
    {
        private List<BasicItemDropStats> mCachedItemStats;
        private VirtualListViewFieldManager<BasicItemDropStats> mFieldManager;

        public FFRKViewActiveDungeon()
        {
            InitializeComponent();
            mCachedItemStats = new List<BasicItemDropStats>();
            mFieldManager = new VirtualListViewFieldManager<BasicItemDropStats>();
        }

        private void FFRKViewCurrentBattle_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            CenterControl(listViewActiveBattle, labelActiveBattleNotice);
            CenterControl(listViewActiveBattle, labelNoDrops);

            mFieldManager.AddColumn(columnHeaderAllName,
                                    (x, y) => x.ItemName.CompareTo(y.ItemName),
                                    x => x.ItemName);
            mFieldManager.AddColumn(columnHeaderAllBattle,
                                    (x,y) => x.BattleName.CompareTo(y.BattleName),
                                    x => x.BattleName);
            mFieldManager.AddColumn(columnHeaderAllTimes,
                                    (x, y) => x.TimesRun.CompareTo(y.TimesRun),
                                    x => x.TimesRun.ToString());
            mFieldManager.AddColumn(columnHeaderAllTotalDrops,
                                    (x, y) => x.TotalDrops.CompareTo(y.TotalDrops),
                                    x => x.TotalDrops.ToString());
            mFieldManager.AddColumn(columnHeaderAllDropsPerRun, 
                                    (x, y) => x.DropsPerRun.CompareTo(y.DropsPerRun),
                                    x => x.DropsPerRun.ToString("F"));
            mFieldManager.AddColumn(columnHeaderAllStam,
                                    (x, y) => x.StaminaPerDrop.CompareTo(y.StaminaPerDrop),
                                    x => x.StaminaPerDrop.ToString("F"));
            mFieldManager.AddColumn(columnHeaderAllReachStamina,
                                    (x, y) => x.StaminaToReachBattle.CompareTo(y.StaminaToReachBattle),
                                    x => x.StaminaToReachBattle.ToString());
            mFieldManager.AddColumn(columnHeaderAllRepeatable,
                                    (x, y) => x.IsBattleRepeatable.CompareTo(y.IsBattleRepeatable),
                                    x => x.IsBattleRepeatable.ToString());

            if (FFRKProxy.Instance != null)
            {
                FFRKProxy.Instance.OnBattleEngaged += FFRKProxy_OnBattleEngaged;
                FFRKProxy.Instance.OnListBattles += FFRKProxy_OnListBattles;
                FFRKProxy.Instance.OnListDungeons += FFRKProxy_OnListDungeons;
                FFRKProxy.Instance.OnLeaveDungeon += FFRKProxy_OnLeaveDungeon;
                FFRKProxy.Instance.OnCompleteBattle += FFRKProxy_OnCompleteBattle;
                FFRKProxy.Instance.OnItemCacheRefreshed += FFRKProxy_OnItemCacheRefreshed;

                PopulateActiveDungeonListView(FFRKProxy.Instance.GameState.ActiveDungeon);
                PopulateActiveBattleListView(FFRKProxy.Instance.GameState.ActiveBattle);
                listViewAllDrops.VirtualListSize = 0;

                BeginPopulateAllDropsListView(FFRKProxy.Instance.GameState.ActiveDungeon);
            }
            else
            {
                PopulateActiveBattleListView(null);
                PopulateActiveDungeonListView(null);
                listViewAllDrops.VirtualListSize = 0;
            }
        }

        void FFRKProxy_OnItemCacheRefreshed()
        {
            // We don't need to do a full refresh here, just make sure the list invalidates so that it asks
            // again for all of the virtual items.
            listViewAllDrops.Invalidate();
        }

        void BeginPopulateAllDropsListView(EventListBattles dungeon)
        {
            if (dungeon != null)
            {
                DbOpFilterDrops op = new DbOpFilterDrops(FFRKProxy.Instance.Database);
                op.Dungeons.AddValue(dungeon.DungeonSession.DungeonId);
                op.OnRequestComplete += RequestDungeonDrops_OnRequestComplete;
                FFRKProxy.Instance.Database.BeginExecuteRequest(op);
            }
        }

        void RequestDungeonDrops_OnRequestComplete(List<BasicItemDropStats> items)
        {
            this.BeginInvoke((Action)(() =>
            {
                mCachedItemStats = items;
                listViewAllDrops.VirtualListSize = mCachedItemStats.Count;
                listViewAllDrops.Invalidate();
            }));
        }

        void UpdateAllDropsForLastBattle(EventBattleInitiated battle)
        {
            if (battle == null)
            {
                listViewAllDrops.VirtualListSize = 0;
                mCachedItemStats.Clear();
            }
            else
            {
                foreach (BasicItemDropStats stats in mCachedItemStats)
                {
                    // Update the times_run field of every item that matches the last battle.  If we don't do
                    // this here in a separate loop, it will only happen for items that actually dropped in
                    // the following loop.
                    if (stats.BattleId == battle.Battle.BattleId)
                    {
                        stats.TimesRun++;
                        stats.TimesRunWithHistogram++;
                    }
                }

                lock(FFRKProxy.Instance.Cache.SyncRoot)
                {
                    foreach (DropEvent drop in battle.Battle.Drops)
                    {
                        if (drop.ItemType == DataEnemyDropItem.DropItemType.Gold)
                            continue;

                        BasicItemDropStats match = mCachedItemStats.Find(x => (x.BattleId == battle.Battle.BattleId)
                                                                           && (x.ItemId == drop.ItemId));
                        EventListBattles this_battle_list = FFRKProxy.Instance.GameState.ActiveDungeon;
                        if (match == null)
                        {
                            if (this_battle_list != null)
                            {
                                DataBattle this_battle = this_battle_list.Battles.Find(x => x.Id == battle.Battle.BattleId);
                                uint times_run = 1;
                                uint histo_times_run = 1;
                                string item_name = drop.ItemId.ToString();
                                DataCache.Items.Data item_data;
                                DataCache.Battles.Data battle_data;
                                lock (FFRKProxy.Instance.Cache.SyncRoot)
                                {
                                    if (FFRKProxy.Instance.Cache.Items.TryGetValue(new DataCache.Items.Key { ItemId = drop.ItemId }, out item_data))
                                        item_name = item_data.Name;
                                    if (FFRKProxy.Instance.Cache.Battles.TryGetValue(new DataCache.Battles.Key { BattleId = battle.Battle.BattleId }, out battle_data))
                                    {
                                        // Get the times_run from the cache, and add 1 to it.
                                        times_run = battle_data.Samples;
                                        histo_times_run = battle_data.HistoSamples;
                                    }
                                }

                                mCachedItemStats.Add(
                                    new BasicItemDropStats
                                    {
                                        BattleId = battle.Battle.BattleId,
                                        BattleName = this_battle.Name,
                                        BattleStamina = this_battle.Stamina,
                                        ItemId = drop.ItemId,
                                        ItemName = item_name,
                                        TimesRun = times_run,
                                        TotalDrops = 1,
                                    });
                                listViewAllDrops.VirtualListSize = mCachedItemStats.Count;
                            }
                        }
                        else
                            ++match.TotalDrops;
                    }
                    listViewAllDrops.Invalidate();
                }
            }
        }

        void FFRKProxy_OnCompleteBattle(EventBattleInitiated battle)
        {
            this.BeginInvoke((Action)(() => 
            { 
                PopulateActiveBattleListView(null);
                UpdateAllDropsForLastBattle(battle);
            }));
        }

        void FFRKProxy_OnLeaveDungeon()
        {
            this.BeginInvoke((Action)(() => 
            { 
                PopulateActiveDungeonListView(null);
                UpdateAllDropsForLastBattle(null);
            }));
        }

        void FFRKProxy_OnListBattles(EventListBattles battles)
        {
            BeginPopulateAllDropsListView(battles);
            this.BeginInvoke((Action)(() => 
                { 
                    PopulateActiveDungeonListView(battles);
                }));
        }

        void FFRKProxy_OnListDungeons(EventListDungeons dungeons)
        {
            this.BeginInvoke((Action)(() => { PopulateActiveBattleListView(null); }));
        }

        void FFRKProxy_OnBattleEngaged(EventBattleInitiated battle)
        {
            this.BeginInvoke((Action)(() => { PopulateActiveBattleListView(battle); }));
        }

        private void PopulateActiveDungeonListView(EventListBattles dungeon)
        {
            listViewActiveDungeon.Items.Clear();
            PopulateActiveBattleListView(null);
            if (dungeon == null)
                groupBoxDungeon.Text = "(No Active Dungeon)";
            else
            {
                groupBoxDungeon.Text = dungeon.DungeonSession.Name;

                foreach (DataBattle battle in dungeon.Battles)
                {
                    string[] row =
                        {
                            (battle.HasBoss) ? "BOSS" : "",
                            battle.Name,
                            battle.RoundNumber.ToString(),
                            battle.Stamina.ToString()
                        };

                    listViewActiveDungeon.Items.Add(new ListViewItem(row));
                }
            }
        }

        private void PopulateActiveBattleListView(EventBattleInitiated battle)
        {
            listViewActiveBattle.Items.Clear();
            if (battle == null)
            {
                labelActiveBattleNotice.Visible = true;
                labelNoDrops.Visible = false;
            }
            else
            {
                listViewActiveBattle.View = View.Details;
                List<DropEvent> drops = battle.Battle.Drops.ToList();
                labelActiveBattleNotice.Visible = false;
                if (drops.Count == 0)
                    labelNoDrops.Visible = true;
                else
                {
                    lock(FFRKProxy.Instance.Cache.SyncRoot)
                    {
                        foreach (DropEvent drop in battle.Battle.Drops)
                        {
                            string Item;
                            DataCache.Items.Key ItemKey = new DataCache.Items.Key { ItemId = drop.ItemId };
                            DataCache.Items.Data ItemData = null;
                            if (drop.ItemType == DataEnemyDropItem.DropItemType.Gold)
                                Item = String.Format("{0} gold", drop.GoldAmount);
                            else if (FFRKProxy.Instance.Cache.Items.TryGetValue(ItemKey, out ItemData))
                                Item = ItemData.Name;
                            else
                                Item = drop.ItemId.ToString();
                            string[] row = 
                            {
                                Item,
                                drop.Rarity.ToString(),
                                drop.Round.ToString(),
                                drop.EnemyName,
                                "",
                                ""
                            };
                            listViewActiveBattle.Items.Add(new ListViewItem(row));
                        }
                    }
                }
            }
        }

        private void CenterControl(Control parent, Control child)
        {
            int innerw = child.Width;
            int innerh = child.Height;
            int outerw = parent.Width;
            int outerh = parent.Height;
            child.Left = parent.Location.X + (outerw - innerw) / 2;
            child.Top = parent.Location.Y + (outerh - innerh) / 2;
        }

        private void FFRKViewCurrentBattle_SizeChanged(object sender, EventArgs e)
        {
            CenterControl(listViewActiveBattle, labelActiveBattleNotice);
            CenterControl(listViewActiveBattle, labelNoDrops);
        }

        private void listViewAllDrops_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex >= mCachedItemStats.Count)
                throw new IndexOutOfRangeException();

            BasicItemDropStats item = mCachedItemStats[e.ItemIndex];
            List<string> fields = new List<string>();
            foreach (ColumnHeader column in listViewAllDrops.Columns)
            {
                string value = mFieldManager.GetFieldValue(column, item);
                fields.Add(value);
            }
            e.Item = new ListViewItem(fields.ToArray());
        }

        private void listViewAllDrops_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listViewAllDrops.Columns[e.Column] == mFieldManager.SortColumn)
            {
                if (mFieldManager.Order == SortOrder.Ascending)
                    mFieldManager.Order = SortOrder.Descending;
                else if (mFieldManager.Order == SortOrder.Descending)
                    mFieldManager.Order = SortOrder.Ascending;
            }
            else
            {
                mFieldManager.SortColumn = listViewAllDrops.Columns[e.Column];
                mFieldManager.Order = SortOrder.Ascending;
            }
            mCachedItemStats.Sort(mFieldManager.ComparisonFunction);
            listViewAllDrops.Invalidate();
        }
    }
}
