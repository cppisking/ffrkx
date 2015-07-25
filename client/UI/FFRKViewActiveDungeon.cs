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
using FFRKInspector.UI.ListViewFields;
using System.Globalization;

namespace FFRKInspector.UI
{
    internal partial class FFRKViewActiveDungeon : UserControl
    {
        private List<BasicItemDropStats> mAllItems;
        private ListListViewBinding<BasicItemDropStats> mFilteredItems;

        public FFRKViewActiveDungeon()
        {
            InitializeComponent();
            mAllItems = new List<BasicItemDropStats>();
            mFilteredItems = new ListListViewBinding<BasicItemDropStats>();

            listViewAllDrops.LoadSettings();

            listViewAllDrops.AddField(new ItemNameField("Item", FieldWidthStyle.Percent, 24));
            listViewAllDrops.AddField(new ItemBattleField("Battle", FieldWidthStyle.Percent, 24));
            listViewAllDrops.AddField(new ItemTimesRunField("Times Run"));
            listViewAllDrops.AddField(new ItemTotalDropsField("Total Drops"));
            listViewAllDrops.AddField(new ItemDropsPerRunField("Drops/Run"));
            listViewAllDrops.AddField(new ItemStaminaPerDropField("Stamina/Drop", false));
            listViewAllDrops.AddField(new ItemStaminaToReachField("Stamina to Reach"));
            listViewAllDrops.AddField(new ItemRepeatableField("Is Repeatable"));

            listViewAllDrops.DataBinding = mFilteredItems;
        }

        private void FFRKViewCurrentBattle_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            CenterControl(listViewActiveBattle, labelActiveBattleNotice);
            CenterControl(listViewActiveBattle, labelNoDrops);


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
                mAllItems = items;
                RebuildFilteredDropListAndInvalidate();
            }));
        }

        void UpdateAllDropsForLastBattle(EventBattleInitiated battle)
        {
            if (battle == null)
            {
                listViewAllDrops.VirtualListSize = 0;
                mAllItems.Clear();
                mFilteredItems.Collection.Clear();
                return;
            }

            foreach (BasicItemDropStats stats in mAllItems.Where(x => x.BattleId == battle.Battle.BattleId))
            {
                // Update the times_run field of every item that matches the last battle.  If we don't do
                // this here in a separate loop, it will only happen for items that actually dropped in
                // the following loop.
                stats.TimesRun++;
                stats.TimesRunWithHistogram++;
            }

            lock(FFRKProxy.Instance.Cache.SyncRoot)
            {
                foreach (DropEvent drop in battle.Battle.Drops)
                {
                    if (drop.ItemType == DataEnemyDropItem.DropItemType.Gold)
                        continue;

                    if (drop.ItemType == DataEnemyDropItem.DropItemType.Materia)
                        continue;

                    if (drop.ItemType == DataEnemyDropItem.DropItemType.Potion)
                        continue;

                    BasicItemDropStats match = mAllItems.Find(x => (x.BattleId == battle.Battle.BattleId)
                                                                && (x.ItemId == drop.ItemId));
                    if (match != null)
                    {
                        ++match.TotalDrops;
                        continue;
                    }

                    EventListBattles this_battle_list = FFRKProxy.Instance.GameState.ActiveDungeon;
                    if (this_battle_list == null)
                        continue;

                    DataBattle this_battle = this_battle_list.Battles.Find(x => x.Id == battle.Battle.BattleId);
                    if (this_battle == null)
                        continue;

                    uint times_run = 1;
                    uint histo_times_run = 1;
                    string item_name = drop.ItemId.ToString();
                    DataCache.Items.Data item_data;
                    DataCache.Battles.Data battle_data;

                    if (FFRKProxy.Instance.Cache.Items.TryGetValue(new DataCache.Items.Key { ItemId = drop.ItemId }, out item_data))
                        item_name = item_data.Name;
                    if (FFRKProxy.Instance.Cache.Battles.TryGetValue(new DataCache.Battles.Key { BattleId = battle.Battle.BattleId }, out battle_data))
                    {
                        // Get the times_run from the cache, and add 1 to it.
                        times_run = battle_data.Samples;
                        histo_times_run = battle_data.HistoSamples;
                    }

                    mAllItems.Add(
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
                }
            }
            RebuildFilteredDropListAndInvalidate();
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
                    BeginPopulateAllDropsListView(battles);
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
                return;
            }

            listViewActiveBattle.View = View.Details;
            List<DropEvent> drops = battle.Battle.Drops.ToList();
            labelActiveBattleNotice.Visible = false;
            if (drops.Count == 0)
            {
                labelNoDrops.Visible = true;
                return;
            }

            lock(FFRKProxy.Instance.Cache.SyncRoot)
            {
                foreach (DropEvent drop in battle.Battle.Drops)
                {
                    string Item;
                    DataCache.Items.Key ItemKey = new DataCache.Items.Key { ItemId = drop.ItemId };
                    DataCache.Items.Data ItemData = null;
                    if (drop.ItemType == DataEnemyDropItem.DropItemType.Gold)
                        Item = String.Format("{0} gold", drop.GoldAmount);
                    else if (drop.ItemType == DataEnemyDropItem.DropItemType.Materia)
                        Item = drop.MateriaName;
                    else if (drop.ItemType == DataEnemyDropItem.DropItemType.Potion)
                        Item = drop.PotionName;
                    else if (FFRKProxy.Instance.Cache.Items.TryGetValue(ItemKey, out ItemData))
                        Item = ItemData.Name;
                    else
                        Item = drop.ItemId.ToString();

                    if (drop.NumberOfItems > 1)
                        Item += String.Format(" x{0}", drop.NumberOfItems);
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

        private void checkBoxRepeatable_CheckedChanged(object sender, EventArgs e)
        {
            RebuildFilteredDropListAndInvalidate();
        }

        private void checkBoxFilterSamples_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = checkBoxFilterSamples.Checked;
            RebuildFilteredDropListAndInvalidate();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            RebuildFilteredDropListAndInvalidate();
        }

        private void textBoxNameFilter_TextChanged(object sender, EventArgs e)
        {
            RebuildFilteredDropListAndInvalidate();
        }

        private void RebuildFilteredDropListAndInvalidate()
        {
            mFilteredItems.Collection = mAllItems.Where(x =>
            {
                if (checkBoxFilterSamples.Checked && x.TimesRun < numericUpDown1.Value)
                    return false;
                if (checkBoxRepeatable.Checked && !x.IsBattleRepeatable)
                    return false;
                if (textBoxNameFilter.Text.Length > 0)
                {
                    int match_index = CultureInfo.CurrentCulture.CompareInfo.IndexOf(x.ItemName, textBoxNameFilter.Text, CompareOptions.IgnoreCase);
                    if (match_index == -1)
                        return false;
                }
                return true;
            }).ToList();
            listViewAllDrops.VirtualListSize = mFilteredItems.Collection.Count;
            listViewAllDrops.Invalidate();
        }
    }
}
