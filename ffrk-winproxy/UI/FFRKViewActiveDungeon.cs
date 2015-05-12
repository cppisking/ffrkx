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
    internal partial class FFRKViewActiveDungeon : UserControl
    {
        private List<BasicItemDropStats> mCachedItemStats;

        public FFRKViewActiveDungeon()
        {
            InitializeComponent();
            mCachedItemStats = new List<BasicItemDropStats>();
        }

        private void FFRKViewCurrentBattle_Load(object sender, EventArgs e)
        {
            CenterControl(listViewActiveBattle, labelActiveBattleNotice);
            CenterControl(listViewActiveBattle, labelNoDrops);

            if (FFRKProxy.Instance != null)
            {
                FFRKProxy.Instance.OnBattleEngaged += FFRKProxy_OnBattleEngaged;
                FFRKProxy.Instance.OnListBattles += FFRKProxy_OnListBattles;
                FFRKProxy.Instance.OnListDungeons += FFRKProxy_OnListDungeons;
                FFRKProxy.Instance.OnLeaveDungeon += FFRKProxy_OnLeaveDungeon;
                FFRKProxy.Instance.OnWinBattle += FFRKProxy_OnWinBattle;
                FFRKProxy.Instance.OnFailBattle += FFRKProxy_OnFailBattle;

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

        void BeginPopulateAllDropsListView(EventListBattles dungeon)
        {
            if (dungeon != null)
            {
                DbOpRequestDropsForDungeon op = new DbOpRequestDropsForDungeon(FFRKProxy.Instance.Database, dungeon.DungeonSession.DungeonId);
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
                foreach (DropEvent drop in battle.Battle.Drops)
                {
                    BasicItemDropStats match = mCachedItemStats.Find(x => (x.BattleId == battle.Battle.BattleId)
                                                                       && (x.EnemyId == drop.EnemyId) 
                                                                       && (x.ItemId == drop.ItemId));
                    EventListBattles this_battle_list = FFRKProxy.Instance.GameState.ActiveDungeon;
                    if (match == null)
                    {
                        if (this_battle_list != null)
                        {
                            DataBattle this_battle = this_battle_list.Battles.Find(x => x.Id == battle.Battle.BattleId);
                            mCachedItemStats.Add(
                                new BasicItemDropStats
                                {
                                    // TODO(cpp): TimesRun and ItemName are wrong, need to load from DB.
                                    BattleId = battle.Battle.BattleId,
                                    BattleName = this_battle.Name,
                                    BattleStamina = this_battle.Stamina,
                                    EnemyId = drop.EnemyId,
                                    EnemyName = drop.EnemyName,
                                    ItemId = drop.ItemId,
                                    ItemName = drop.ItemId.ToString(),
                                    TimesRun = 1,
                                    TotalDrops = 1
                                });
                            listViewAllDrops.VirtualListSize = mCachedItemStats.Count;
                        }
                    }
                    else
                    {
                        ++match.TotalDrops;
                        listViewAllDrops.Invalidate();
                    }
                }
            }
        }

        void FFRKProxy_OnWinBattle(EventBattleInitiated battle)
        {
            this.BeginInvoke((Action)(() => 
            { 
                PopulateActiveBattleListView(null);
                UpdateAllDropsForLastBattle(battle);
            }));
        }

        void FFRKProxy_OnFailBattle(EventBattleInitiated battle)
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
                    foreach (DropEvent drop in battle.Battle.Drops)
                    {
                        string Item;
                        if (drop.ItemType == DataEnemyDropItem.DropItemType.Gold)
                            Item = String.Format("{0} gold", drop.GoldAmount);
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
            {
                e.Item = new ListViewItem();
                return;
            }

            BasicItemDropStats item = mCachedItemStats[e.ItemIndex];
            float drop_rate = 100.0f * (float)item.TotalDrops / (float)item.TimesRun;
            float stam_per_drop = (float)(item.TimesRun * item.BattleStamina) / (float)item.TotalDrops;
            string[] rows = new string[]
            {
                item.ItemName,
                item.BattleName,
                item.EnemyName,
                drop_rate.ToString("F") + "%",
                stam_per_drop.ToString("F")
            };
            e.Item = new ListViewItem(rows);
        }
    }
}
