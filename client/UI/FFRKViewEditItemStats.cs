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
        List<UserControl> mPanels;

        UserControl mSelectedPanel;

        public FFRKViewEditItemStats()
        {
            InitializeComponent();
            mPanels = new List<UserControl>();

#if ALLOW_ITEM_EDITING
            EditExistingItemsPanel edit_panel = new EditExistingItemsPanel();
            edit_panel.Dock = DockStyle.Fill;
            edit_panel.Location = new Point(0, 0);
            this.groupBox1.Controls.Add(edit_panel);
            mPanels.Add(edit_panel);
            this.comboBox1.Items.Add("Edit existing items");
#endif

            MissingItemsPanel missing_panel = new MissingItemsPanel();
            missing_panel.Dock = DockStyle.Fill;
            missing_panel.Location = new Point(0, 0);
            this.groupBox1.Controls.Add(missing_panel);
            mPanels.Add(missing_panel);
            this.comboBox1.Items.Add("Add missing items");

            comboBox1.SelectedIndex = 0;
        }

        private void FFRKViewEditItemStats_Load(object sender, EventArgs e)
        {
        }

        private void buttonCommit_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                FFRKDataBoundPanel panel = mPanels[comboBox1.SelectedIndex] as FFRKDataBoundPanel;
                if (panel != null)
                    panel.Commit();
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                FFRKDataBoundPanel panel = mPanels[comboBox1.SelectedIndex] as FFRKDataBoundPanel;
                if (panel != null)
                    panel.Reload();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mSelectedPanel != null)
                mSelectedPanel.Visible = false;
            mSelectedPanel = mPanels[comboBox1.SelectedIndex];
            mSelectedPanel.Visible = true;
        }
    }
}
