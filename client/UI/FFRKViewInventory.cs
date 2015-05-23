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

        public FFRKViewInventory()
        {
            InitializeComponent();

            mBuddyList = new ListListViewBinding<DataBuddyInformation>();

            listViewEx1.LoadSettings();

            listViewEx1.AddField(new CharacterNameField("Name"));
            listViewEx1.AddField(new CharacterLevelField("Level"));
            listViewEx1.AddField(new CharacterLevelMaxField("Max Level"));

            listViewEx1.DataBinding = mBuddyList;
        }

        private void FFRKViewInventory_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (FFRKProxy.Instance != null)
            {
                FFRKProxy.Instance.OnPartyList += FFRKProxy_OnPartyList;

                DataBuddyInformation[] buddies = FFRKProxy.Instance.GameState.PartyDetails.Buddies;
                if (buddies != null)
                {
                    mBuddyList.Collection = buddies.ToList();
                    listViewEx1.VirtualListSize = buddies.Length;
                }
            }

        }

        void FFRKProxy_OnPartyList(DataPartyDetails party)
        {
            mBuddyList.Collection = party.Buddies.ToList();
            listViewEx1.VirtualListSize = party.Buddies.Length;
        }

        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void dataGridView2_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView2.RowCount > 5)
            //    dataGridView2.AllowUserToAddRows = false;
        }

        private void dataGridView2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            //if (dataGridView2.RowCount < 5)
            //    dataGridView2.AllowUserToAddRows = false;
        }
    }
}
