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
    public partial class MissingItemsPanel : UserControl, FFRKDataBoundPanel
    {
        public MissingItemsPanel()
        {
            InitializeComponent();
        }

        private void MissingItemsPanel_Load(object sender, EventArgs e)
        {
            this.missing_itemsTableAdapter.Connection.ConnectionString = FFRKProxy.Instance.Database.ConnectionString;
            Reload();
        }

        public void Reload()
        {
            try
            {
                this.missing_itemsTableAdapter.Fill(this.missingItemsDataSet.missing_items);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public void Commit()
        {
            try
            {
                this.missing_itemsTableAdapter.Update(this.missingItemsDataSet.missing_items);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
