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
using FFRKInspector.UI.DatabaseUI;

namespace FFRKInspector.UI
{
    internal partial class FFRKTabInspector : UserControl
    {
        private TabPage[] mDeveloperTabs;

        public enum InspectorPage
        {
            CurrentDungeon,
            CurrentBattle,
            ItemSearch,
            Inventory,
            Database,
            Gacha,
            About,
            Debugging
        }

        public FFRKTabInspector()
        {
            InitializeComponent();
            tabPageAbout.Tag = InspectorPage.About;
            tabPageDungeon.Tag = InspectorPage.CurrentBattle;
            tabPageDebug.Tag = InspectorPage.Debugging;
            tabPageEditEquipment.Tag = InspectorPage.Database;
            tabPageGacha.Tag = InspectorPage.Gacha;
            tabPageInventory.Tag = InspectorPage.Inventory;
            tabPageSearch.Tag = InspectorPage.ItemSearch;
        }

        private void FFRKTabInspectorView_Load(object sender, EventArgs e)
        {
            tabControlFFRKInspector.SelectedIndexChanged += tabControlFFRKInspector_SelectedIndexChanged;
            if (FFRKProxy.Instance != null)
            {
                FFRKProxy.Instance.Database.OnConnectionStateChanged += Database_OnConnectionStateChanged;
            }
        }

        void tabControlFFRKInspector_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        void FFRKTabInspector_HandleDestroyed(object sender, EventArgs e)
        {
        }

        void Database_OnConnectionStateChanged(FFRKMySqlInstance.ConnectionState NewState)
        {
            if (IsHandleCreated)
                BeginInvoke((Action)(() => { toolStripStatusLabelConnection.Text = NewState.ToString(); }));
        }

        public InspectorPage SelectedPage
        {
            get
            {
                if (tabControlFFRKInspector.SelectedTab == null)
                    return InspectorPage.CurrentDungeon;
                return (InspectorPage)tabControlFFRKInspector.SelectedTab.Tag;
            }
            set
            {
                TabPage page = tabControlFFRKInspector.TabPages.Cast<TabPage>().First(x => (InspectorPage)x.Tag == value);
                tabControlFFRKInspector.SelectTab(page);
            }
        }

        public FFRKViewDatabase DatabaseTab
        {
            get
            {
                return (FFRKViewDatabase)tabPageEditEquipment.Controls[0];
            }
        }

        private void tabPageInventory_Click(object sender, EventArgs e)
        {

        }
    }
}
