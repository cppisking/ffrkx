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
    public partial class EditExistingItemsPanel : UserControl, FFRKDataBoundPanel
    {
        public EditExistingItemsPanel()
        {
            InitializeComponent();
        }

        private void EditExistingItemsPanel_Load(object sender, EventArgs e)
        {
            this.equipment_statsTableAdapter.Connection.ConnectionString = FFRKProxy.Instance.Database.ConnectionString;
            Reload();
        }

        public void Reload()
        {
            try
            {
                this.equipment_statsTableAdapter.Fill(this.equipmentStatsDataSet.equipment_stats);
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
                this.equipment_statsTableAdapter.Update(this.equipmentStatsDataSet.equipment_stats);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
