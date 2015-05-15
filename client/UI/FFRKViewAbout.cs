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
    public partial class FFRKViewAbout : UserControl
    {
        public FFRKViewAbout()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:cppisking@gmail.com");
        }

        private void buttonTestSettings_Click(object sender, EventArgs e)
        {
            try
            {
                FFRKProxy.Instance.Database.TestConnect(textBoxHost.Text, textBoxUser.Text, textBoxPassword.Text, textBoxSchema.Text);
                labelConnectionResult.Text = "Connection successful!  Save the settings, and close and restart fiddler for the settings to take effect.";
            }
            catch(Exception ex)
            {
                labelConnectionResult.Text = String.Format("Unable to connect.  {0}", ex.Message);
            }
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DatabaseHost = textBoxHost.Text;
            Properties.Settings.Default.DatabasePassword = textBoxPassword.Text;
            Properties.Settings.Default.DatabaseSchema = textBoxSchema.Text;
            Properties.Settings.Default.DatabaseUser = textBoxUser.Text;
            Properties.Settings.Default.Save();

            labelConnectionResult.Text = "Settings saved.  Close and restart fiddler for the settings to take effect.";
        }

        private void FFRKViewAbout_Load(object sender, EventArgs e)
        {
            textBoxHost.Text = Properties.Settings.Default.DatabaseHost;
            textBoxPassword.Text = Properties.Settings.Default.DatabasePassword;
            textBoxSchema.Text = Properties.Settings.Default.DatabaseSchema;
            textBoxUser.Text = Properties.Settings.Default.DatabaseUser;
        }
    }
}
