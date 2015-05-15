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
        public FFRKTabInspector()
        {
            InitializeComponent();
        }

        private void FFRKTabInspectorView_Load(object sender, EventArgs e)
        {
            if (FFRKProxy.Instance != null)
            {
                FFRKProxy.Instance.Database.OnConnectionStateChanged += Database_OnConnectionStateChanged;
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
