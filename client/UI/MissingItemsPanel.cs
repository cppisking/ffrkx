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
using FFRKInspector.GameData;

namespace FFRKInspector.UI
{
    public partial class MissingItemsPanel : UserControl, FFRKDataBoundPanel
    {
        public MissingItemsPanel()
        {
            InitializeComponent();

            type.CellTemplate = new EnumDataViewGridCell<SchemaConstants.ItemType>();
            subtype.CellTemplate = new EnumDataViewGridCell<SchemaConstants.EquipmentCategory>();
            series.CellTemplate = new SeriesDataGridViewCell();
        }

        private void MissingItemsPanel_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (FFRKProxy.Instance != null)
                this.missing_itemsTableAdapter.Connection.ConnectionString = FFRKProxy.Instance.Database.ConnectionString;
        }

        public void Reload()
        {
            this.missing_itemsTableAdapter.Fill(this.missingItemsDataSet.missing_items);
        }

        public void Commit()
        {
            int result = this.missing_itemsTableAdapter.Update(this.missingItemsDataSet.missing_items);
            if (result == 0)
                MessageBox.Show("There are no changes to commit");
            else
                MessageBox.Show(String.Format("Updated {0} entries.", result));
        }
    }
}
