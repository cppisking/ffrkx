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
using MySql.Data.MySqlClient;

namespace FFRKInspector.UI.DatabaseUI
{
    public partial class FFRKViewDatabase : UserControl
    {
        List<UserControl> mPanels;

        UserControl mSelectedPanel;

        public enum DatabaseModeEnum
        {
            EquipmentAndStats,
            MissingItems
        }

        public FFRKViewDatabase()
        {
            InitializeComponent();
            mPanels = new List<UserControl>();

            ItemStatsPanel edit_panel = new ItemStatsPanel();
            edit_panel.Dock = DockStyle.Fill;
            edit_panel.Location = new Point(0, 0);
            this.groupBox1.Controls.Add(edit_panel);
            mPanels.Add(edit_panel);
            this.comboBox1.Items.Add("View equipment and stats");

            MissingItemsPanel missing_panel = new MissingItemsPanel();
            missing_panel.Dock = DockStyle.Fill;
            missing_panel.Location = new Point(0, 0);
            this.groupBox1.Controls.Add(missing_panel);
            mPanels.Add(missing_panel);
            this.comboBox1.Items.Add("Add missing items or submit fixes for incorrect items");

            comboBox1.SelectedIndex = 0;
        }

        private string sDatabaseLoadError = "Unable to load items from the database.  Check that you are " +
                                            "using the latest version of FFRKInspector.  Functionality on this page " +
                                            "will be disabled.";
        private string sPrivilegeError = "You do not have the appropriate privileges to submit changes to " +
                                         "the requested fields.  Please submit missing and incorrect item information " +
                                         "through the missing items panel";

        private void FFRKViewEditItemStats_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            try
            {
                foreach (FFRKDataBoundPanel panel in mPanels)
                {
                    panel.InitializeConnection();
                    panel.Reload();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(sDatabaseLoadError);
                groupBox1.Controls.Clear();
                mPanels.Clear();
                mSelectedPanel = null;
            }

        }

        private void buttonCommit_Click(object sender, EventArgs e)
        {
            if (mSelectedPanel == null)
                return;
            try
            {
                ((FFRKDataBoundPanel)mSelectedPanel).Commit();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 1142:
                        // User does not have privileges to edit the requested table.
                        MessageBox.Show(sPrivilegeError);
                        break;
                    default:
                        throw;
                }
            }
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            if (mSelectedPanel == null)
                return;
            ((FFRKDataBoundPanel)mSelectedPanel).Reload();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mSelectedPanel = (comboBox1.SelectedIndex < mPanels.Count) ? mPanels[comboBox1.SelectedIndex] : null;
            foreach (UserControl panel in mPanels)
            {
                panel.Visible = (panel == mSelectedPanel);
            }
        }

        public DatabaseModeEnum DatabaseMode
        {
            get
            {
                return (DatabaseModeEnum)comboBox1.SelectedIndex;
            }
            set
            {
                comboBox1.SelectedIndex = (int)value;
            }
        }
    }
}
