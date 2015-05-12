using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ffrk_winproxy.GameData;

namespace ffrk_winproxy
{
    internal partial class FFRKViewActiveDungeon : UserControl
    {
        public FFRKViewActiveDungeon()
        {
            InitializeComponent();
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
            }
            else
            {
                PopulateActiveBattleListView(null);
                PopulateActiveDungeonListView(null);
            }
        }

        void ClearActiveBattleListView()
        {
        }

        void FFRKProxy_OnWinBattle(EventBattleInitiated battle)
        {
            this.BeginInvoke((Action)(() => { PopulateActiveBattleListView(null); }));
        }

        void FFRKProxy_OnFailBattle(EventBattleInitiated battle)
        {
            this.BeginInvoke((Action)(() => { PopulateActiveBattleListView(null); }));
        }

        void FFRKProxy_OnLeaveDungeon()
        {
            this.BeginInvoke((Action)(() => { PopulateActiveDungeonListView(null); }));
        }

        void FFRKProxy_OnListBattles(EventListBattles battles)
        {
            this.BeginInvoke((Action)(() => { PopulateActiveDungeonListView(battles); }));
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
            ClearActiveBattleListView();
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
    }
}
