using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFRKInspector.Database;
using FFRKInspector.Proxy;

namespace FFRKInspector.UI
{
    internal partial class FFRKTabInspector : UserControl
    {
        private bool mDeveloperTabsEnabled;
        private TabPage[] mDeveloperTabs;

        public FFRKTabInspector()
        {
            InitializeComponent();

            mDeveloperTabs = new TabPage[] { tabPageEditEquipment };
            DeveloperTabsEnabled = false;
        }

        private void FFRKTabInspectorView_Load(object sender, EventArgs e)
        {
            if (FFRKProxy.Instance != null)
            {
                FFRKProxy.Instance.Database.OnConnectionStateChanged += Database_OnConnectionStateChanged;
            }
        }

        private void InternalEnableDeveloperTabs()
        {
            foreach (TabPage page in mDeveloperTabs)
            {
                int current_index = tabControlFFRKInspector.Controls.IndexOf(page);
                if (current_index == -1 && mDeveloperTabsEnabled)
                    tabControlFFRKInspector.Controls.Add(page);
                else if (current_index >= 0 && !mDeveloperTabsEnabled)
                    tabControlFFRKInspector.Controls.RemoveAt(current_index);
            }
        }

        public bool DeveloperTabsEnabled
        {
            get { return mDeveloperTabsEnabled; }
            set
            {
                mDeveloperTabsEnabled = value;
                // BeginInvoke doesn't work if the handle isn't created, but it's a requirement if the
                // handle is created.
                if (IsHandleCreated)
                    BeginInvoke((Action)(() => InternalEnableDeveloperTabs()));
                else
                    InternalEnableDeveloperTabs();
            }
        }

        void FFRKTabInspector_HandleDestroyed(object sender, EventArgs e)
        {
        }

        void Database_OnConnectionStateChanged(FFRKMySqlInstance.ConnectionState NewState)
        {
            if (IsHandleCreated)
                BeginInvoke((Action)(() => { toolStripStatusLabelConnection.Text = NewState.ToString(); }));
        }
    }
}
