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
    internal partial class FFRKViewActiveBattle : UserControl
    {
        private List<BasicItemDropStats> mAllPrevItems;
        private ListListViewBinding<BasicItemDropStats> mFilteredPrevItems;

        public FFRKViewActiveBattle()
        {
            InitializeComponent();
            mAllPrevItems = new List<BasicItemDropStats>();
            mFilteredPrevItems = new ListListViewBinding<BasicItemDropStats>();

            listViewPrevDrops.LoadSettings();

            listViewPrevDrops.AddField(new ItemNameField("Item", FieldWidthStyle.Percent, 24));
            //listViewPrevDrops.AddField(new ItemBattleField("Battle", FieldWidthStyle.Percent, 24));
            listViewPrevDrops.AddField(new ItemDropsPerRunField("Drops/Run"));
            listViewPrevDrops.AddField(new ItemTimesRunField("Times Run"));
            listViewPrevDrops.AddField(new ItemTotalDropsField("Total Drops"));
            //listViewPrevDrops.AddField(new ItemDropsPerRunField("Drops/Run"));
            //listViewPrevDrops.AddField(new ItemStaminaPerDropField("Stamina/Drop", false));
            //listViewPrevDrops.AddField(new ItemStaminaToReachField("Stamina to Reach"));
            //listViewPrevDrops.AddField(new ItemRepeatableField("Is Repeatable"));
            foreach (ColumnHeader column in listViewPrevDrops.Columns) { column.Width = -2; }
            listViewPrevDrops.DataBinding = mFilteredPrevItems;
        }

        private void FFRKViewCurrentBattle_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (FFRKProxy.Instance != null)
            {
                FFRKProxy.Instance.OnBattleEngaged += FFRKProxy_OnBattleEngaged;
                FFRKProxy.Instance.OnListBattles += FFRKProxy_OnListBattles;
                FFRKProxy.Instance.OnListDungeons += FFRKProxy_OnListDungeons;
                FFRKProxy.Instance.OnLeaveDungeon += FFRKProxy_OnLeaveDungeon;
                FFRKProxy.Instance.OnCompleteBattle += FFRKProxy_OnCompleteBattle;
                FFRKProxy.Instance.OnItemCacheRefreshed += FFRKProxy_OnItemCacheRefreshed;

                //PopulateActiveDungeonListView(FFRKProxy.Instance.GameState.ActiveDungeon);
                //PopulateActiveBattleListView(FFRKProxy.Instance.GameState.ActiveBattle);
                PopulateEnemyInfoListView(FFRKProxy.Instance.GameState.ActiveBattle);
                PopulateDropInfoListView(FFRKProxy.Instance.GameState.ActiveBattle);
                BeginPopulateAllDropsListView(FFRKProxy.Instance.GameState.ActiveBattle);
            }
            else
            {
                PopulateEnemyInfoListView(null);
                PopulateDropInfoListView(null);
                BeginPopulateAllDropsListView(null);
                //PopulateActiveBattleListView(null);
                //PopulateActiveDungeonListView(null);
            }
        }

        void BeginPopulateAllDropsListView(EventBattleInitiated battle)
        {
            if (battle != null)
            {
                DbOpFilterDrops op = new DbOpFilterDrops(FFRKProxy.Instance.Database);
                op.Battles.AddValue(battle.Battle.BattleId);
                op.OnRequestComplete += RequestBattleDrops_OnRequestComplete;
                FFRKProxy.Instance.Database.BeginExecuteRequest(op);
            }
            else
            {
                listViewPrevDrops.VirtualListSize = 0;
                mAllPrevItems.Clear();
                mFilteredPrevItems.Collection.Clear();
            }
        }

        void RequestBattleDrops_OnRequestComplete(List<BasicItemDropStats> items)
        {
            this.BeginInvoke((Action)(() =>
            {
                mAllPrevItems = items;
                RebuildFilteredDropListAndInvalidate();
            }));
        }

        private void RebuildFilteredDropListAndInvalidate()
        {
            List<BasicItemDropStats> result = mAllPrevItems.Select(i => new BasicItemDropStats() {
                ItemName = i.ItemName,
                DropsPerRunF = i.DropsPerRun,
                TimesRun = i.TimesRun,
                TotalDrops = i.TotalDrops
            }).ToList();
            mFilteredPrevItems.Collection = result;
            listViewPrevDrops.VirtualListSize = mFilteredPrevItems.Collection.Count;
            listViewPrevDrops.Invalidate();
            foreach (ColumnHeader column in listViewPrevDrops.Columns) { column.Width = -2; }
        }

        void FFRKProxy_OnItemCacheRefreshed()
        {
            // We don't need to do a full refresh here, just make sure the list invalidates so that it asks
            // again for all of the virtual items.
            listViewPrevDrops.Invalidate();
        }

        private void PopulateEnemyInfoListView(EventBattleInitiated battle)
        {

            listViewEnemyInfo.Items.Clear();
            if (battle == null) { return; }
            else
            {
                listViewEnemyInfo.View = View.Details;

                List<BasicEnemyInfo> enemies = battle.Battle.Enemies.ToList();
                if (enemies.Count == 0) { return; }
                else
                {
                    lock (FFRKProxy.Instance.Cache.SyncRoot)
                    {
                        foreach (BasicEnemyInfo enemy in battle.Battle.Enemies)
                        {
                            //The below is the "full" list of status effects, however I removed Haste, Shell, Protect, Reflect, and a few other
                            //positive buffs, and also the status effects not yet implemented such as Zombie, Toad, Mini.
                            List<string> AllStatusEffects = new List<String>()
                            {
                                "Poison","Silence","Paralyze","Confuse","Slow","Stop",
                                "Blind","Sleep","Petrify","Doom","Instant_KO","Beserk"
                                //,"Haste","Shell","Protect","Regen","Reflect"
                            };
                            //Calculate difference between above set of status effects and the status immunities of enemy.
                            List<string> EnemyStatusWeakness = AllStatusEffects.Except(enemy.EnemyStatusImmunity).ToList();
                            string[] row =
                                {
                                enemy.EnemyName,
                                enemy.EnemyMaxHp.ToString("N0"),
                                //string.Join(", ",enemy.EnemyElemWeakness.ToArray()),
                                //"test2",
                                //"test3"
                                string.Join(", ",EnemyStatusWeakness.ToArray()),
                                string.Join(", ",enemy.EnemyElemWeakness.ToArray()),
                                string.Join(", ",enemy.EnemyElemResist.ToArray()),
                                string.Join(", ",enemy.EnemyElemNull.ToArray()),
                                string.Join(", ",enemy.EnemyElemAbsorb.ToArray())
                            };
                            listViewEnemyInfo.Items.Add(new ListViewItem(row));
                        }
                    }
                    foreach (ColumnHeader column in listViewEnemyInfo.Columns) { column.Width = -2; }
                }
            }
        }


        private void PopulateDropInfoListView(EventBattleInitiated battle)
        {
            listViewDropInfo.Items.Clear();
            if (battle == null) { return; }
            else
            {
                listViewDropInfo.View = View.Details;
                List<DropEvent> drops = battle.Battle.Drops.ToList();
                //labelActiveBattleNotice.Visible = false;
                if (drops.Count == 0) { return; }
                else
                {
                    lock (FFRKProxy.Instance.Cache.SyncRoot)
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
                                drop.EnemyName
                            };
                            listViewDropInfo.Items.Add(new ListViewItem(row));
                        }
                    }
                }
                foreach (ColumnHeader column in listViewDropInfo.Columns) { column.Width = -2; }
            }
        }

        void FFRKProxy_OnCompleteBattle(EventBattleInitiated battle)
        {
            this.BeginInvoke((Action)(() =>
            {
                PopulateEnemyInfoListView(null);
                PopulateDropInfoListView(null);
                BeginPopulateAllDropsListView(null);
                //PopulateDropInfoListView(null);
                //UpdateAllDropsForLastBattle(battle);
            }));
        }

        void FFRKProxy_OnLeaveDungeon()
        {
            this.BeginInvoke((Action)(() =>
            {
                PopulateEnemyInfoListView(null);
                PopulateDropInfoListView(null);
                BeginPopulateAllDropsListView(null);
                //PopulateActiveDungeonListView(null);
                //UpdateAllDropsForLastBattle(null);
            }));
        }

        void FFRKProxy_OnListBattles(EventListBattles battles)
        {
            //BeginPopulateAllDropsListView(battles);
            //this.BeginInvoke((Action)(() =>
            //{
            //    PopulateActiveDungeonListView(battles);
            //    BeginPopulateAllDropsListView(battles);
            //}));
        }

        void FFRKProxy_OnListDungeons(EventListDungeons dungeons)
        {
            this.BeginInvoke((Action)(() =>
            {
                //PopulateActiveBattleListView(null);
                BeginPopulateAllDropsListView(null);
                PopulateDropInfoListView(null);
                PopulateEnemyInfoListView(null);
            }));
        }

        void FFRKProxy_OnBattleEngaged(EventBattleInitiated battle)
        {
            this.BeginInvoke((Action)(() =>
            {
                //PopulateActiveBattleListView(battle); 
                BeginPopulateAllDropsListView(battle);
                PopulateEnemyInfoListView(battle);
                PopulateDropInfoListView(battle);
            }));
        }

        private void FFRKViewActiveDungeon2_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
