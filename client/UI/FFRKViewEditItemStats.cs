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
    public partial class FFRKViewEditItemStats : UserControl
    {
        public FFRKViewEditItemStats()
        {
            InitializeComponent();
        }

        private void FFRKViewEditItemStats_Load(object sender, EventArgs e)
        {
            this.equipment_statsTableAdapter.Connection.ConnectionString = FFRKProxy.Instance.Database.ConnectionString;
            Reload();
        }

        private void buttonCommit_Click(object sender, EventArgs e)
        {
            try
            {
                this.equipment_statsTableAdapter.Update(this.ffrktestDataSet.equipment_stats);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            try
            {
                this.equipment_statsTableAdapter.Fill(this.ffrktestDataSet.equipment_stats);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
