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

namespace FFRKInspector.UI
{
    public partial class FFRKViewInventory : UserControl
    {
        public FFRKViewInventory()
        {
            InitializeComponent();
        }

        private void FFRKViewInventory_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (FFRKProxy.Instance != null)
                FFRKProxy.Instance.OnPartyList += FFRKProxy_OnPartyList;
            dataGridView1.Rows.Add(1, 2, 3, 4);
            dataGridView1.Rows.Add(1, 2, 3, 4);
            dataGridView1.Rows.Add(1, 2, 3, 4);
            dataGridView1.Rows.Add(1, 2, 3, 4);
            dataGridView1.Rows.Add(1, 2, 3, 4);
            dataGridView1.Rows.Add(1, 2, 3, 4);
            dataGridView1.Rows.Add(1, 2, 3, 4);
            dataGridView1.Rows.Add(1, 2, 3, 4);
        }

        void FFRKProxy_OnPartyList(GameData.Party.DataPartyDetails party)
        {
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
