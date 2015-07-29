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
        public FFRKViewActiveBattle()
        {
            InitializeComponent();
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
                //FFRKProxy.Instance.OnItemCacheRefreshed += FFRKProxy_OnItemCacheRefreshed;

                //PopulateActiveDungeonListView(FFRKProxy.Instance.GameState.ActiveDungeon);
                //PopulateActiveBattleListView(FFRKProxy.Instance.GameState.ActiveBattle);
                PopulateEnemyInfoListView(FFRKProxy.Instance.GameState.ActiveBattle);
                PopulateDropInfoListView(FFRKProxy.Instance.GameState.ActiveBattle);

                //BeginPopulateAllDropsListView(FFRKProxy.Instance.GameState.ActiveDungeon);
            }
            else
            {
                PopulateEnemyInfoListView(null);
                PopulateDropInfoListView(null);
                //PopulateActiveBattleListView(null);
                //PopulateActiveDungeonListView(null);
            }
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
                            string[] row =
                                {
                                enemy.EnemyName,
                                enemy.EnemyMaxHp.ToString("N0"),
                                //string.Join(", ",enemy.EnemyElemWeakness.ToArray()),
                                //"test2",
                                //"test3"
                                string.Join(", ",enemy.EnemyStatusImmunity.ToArray()),
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
                //PopulateDroInfoListView(null);
                //UpdateAllDropsForLastBattle(battle);
            }));
        }

        void FFRKProxy_OnLeaveDungeon()
        {
            this.BeginInvoke((Action)(() =>
            {
                PopulateEnemyInfoListView(null);
                PopulateDropInfoListView(null);
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
                PopulateDropInfoListView(null);
                PopulateEnemyInfoListView(null);
            }));
        }

        void FFRKProxy_OnBattleEngaged(EventBattleInitiated battle)
        {
            this.BeginInvoke((Action)(() =>
            {
                //PopulateActiveBattleListView(battle); 
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
