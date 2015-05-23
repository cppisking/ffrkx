using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFRKInspector.Proxy;
using FFRKInspector.GameData.Party;
using FFRKInspector.UI.ListViewFields;
using FFRKInspector.GameData;

namespace FFRKInspector.UI
{
    public partial class FFRKViewInventory : UserControl
    {
        private ListListViewBinding<DataBuddyInformation> mBuddyList;

        private class SynergyFormatter
        {
            private RealmSynergy.SynergyValue mSynergy;
            public SynergyFormatter(RealmSynergy.SynergyValue Synergy)
            {
                mSynergy = Synergy;
            }

            public override string ToString()
            {
                if (mSynergy.Realm == RealmSynergy.Value.None)
                    return "no";
                else
                    return mSynergy.Realm.ToString();
            }
        }

        public FFRKViewInventory()
        {
            InitializeComponent();

            mBuddyList = new ListListViewBinding<DataBuddyInformation>();

            listViewEx1.LoadSettings();

            listViewEx1.AddField(new CharacterNameField("Name"));
            listViewEx1.AddField(new CharacterLevelField("Level"));
            listViewEx1.AddField(new CharacterLevelMaxField("Max Level"));

            listViewEx1.DataBinding = mBuddyList;
            foreach (RealmSynergy.SynergyValue synergy in RealmSynergy.Values)
                comboBoxSynergy.Items.Add(new SynergyFormatter(synergy));
        }

        private void FFRKViewInventory_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (FFRKProxy.Instance != null)
            {
                FFRKProxy.Instance.OnPartyList += FFRKProxy_OnPartyList;

                DataPartyDetails party = FFRKProxy.Instance.GameState.PartyDetails;
                if (party != null)
                {
                    DataBuddyInformation[] buddies = party.Buddies;
                    mBuddyList.Collection = buddies.ToList();
                    listViewEx1.VirtualListSize = buddies.Length;
                }
            }

        }

        void FFRKProxy_OnPartyList(DataPartyDetails party)
        {
            BeginInvoke((Action)(() =>
            {
                mBuddyList.Collection = party.Buddies.ToList();
                listViewEx1.VirtualListSize = party.Buddies.Length;

                foreach (DataEquipmentInformation equip in party.Equipments)
                {
                    int row_index = dataGridView1.Rows.Add();
                    DataGridViewRow row = dataGridView1.Rows[row_index];
                    row.Cells[dgcItem.Name].Value = equip.Name;
                    row.Cells[dgcCategory.Name].Value = equip.Category;
                    row.Cells[dgcType.Name].Value = equip.Type;
                    row.Cells[dgcRarity.Name].Value = (int)equip.Rarity;
                    row.Cells[dgcSynergy.Name].Value = RealmSynergy.FromSeries(equip.SeriesId).Text;
                    row.Cells[dgcLevel.Name].Value = String.Format("{0}/{1}", equip.Level, equip.LevelMax);
                }
            }));
        }
    }
}
