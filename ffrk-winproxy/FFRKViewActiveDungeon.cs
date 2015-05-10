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
            if (FFRKProxy.Instance != null)
            {
                FFRKProxy.Instance.OnBattleEngaged += FFRKProxy_OnBattleEngaged;
                FFRKProxy.Instance.OnListBattles += FFRKProxy_OnListBattles;
                FFRKProxy.Instance.OnListDungeons += FFRKProxy_OnListDungeons;
            }

            ClearActiveBattleListView();
        }

        void ClearActiveBattleListView()
        {
            labelActiveBattleNotice.Visible = true;
        }

        void FFRKProxy_OnListBattles(EventListBattles battles)
        {
            this.BeginInvoke((Action)(() =>
                {
                    listViewActiveDungeon.Items.Clear();
                    groupBoxDungeon.Text = battles.DungeonSession.Name;
                    ClearActiveBattleListView();

                    foreach (DataBattle battle in battles.Battles)
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
                }));
        }

        void FFRKProxy_OnListDungeons(EventListDungeons dungeons)
        {
            this.BeginInvoke((Action)(() =>
                {
                    ClearActiveBattleListView();
                }));
        }

        void FFRKProxy_OnBattleEngaged(EventBattleInitiated battle)
        {
            this.BeginInvoke((Action)(() =>
                {
                    listViewActiveBattle.Items.Clear();
                    listViewActiveBattle.View = View.Details;
                    labelActiveBattleNotice.Visible = false;
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
                }));
        }

        private void FFRKViewCurrentBattle_SizeChanged(object sender, EventArgs e)
        {
            int labelWidth = labelActiveBattleNotice.Width;
            int labelHeight = labelActiveBattleNotice.Height;
            int listViewWidth = listViewActiveBattle.Width;
            int listViewHeight = listViewActiveBattle.Height;
            labelActiveBattleNotice.Left = listViewActiveBattle.Location.X + (listViewWidth - labelWidth) / 2;
            labelActiveBattleNotice.Top = listViewActiveBattle.Location.Y + (listViewHeight - labelHeight) / 2;
        }
    }
}
