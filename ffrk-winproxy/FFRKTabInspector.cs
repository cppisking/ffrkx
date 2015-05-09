using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ffrk_winproxy
{
    internal partial class FFRKTabInspector : UserControl
    {
        FFRKProxy mProxy;

        public FFRKTabInspector(FFRKProxy proxy)
        {
            InitializeComponent();
            mProxy = proxy;
        }

        private void FFRKTabInspectorView_Load(object sender, EventArgs e)
        {
        }

        internal FFRKProxy Proxy { get { return mProxy; } }
    }
}
