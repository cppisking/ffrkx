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
using FFRKInspector.Database;
using Fiddler;

namespace FFRKInspector.UI
{
    public partial class FFRKViewAbout : UserControl
    {
        static readonly string mConnectionSuccessMsg = 
            "Connection successful!  Save the settings, and close and restart fiddler for the settings to take effect.";
        static readonly string mDatabaseTooOldMsg = 
            "The database is for an older version of FFRK Inspector.  Database connectivity will be unavailable with these settings.";
        static readonly string mDatabaseTooNewMsg =
            "The database is for a newer version of FFRK Inspector.  You will need to update to a later version to connect to this database.";
        static readonly string mInvalidConnectionMsg =
            "Unable to establish a connection with the specified parameters.  Check that they are correct and try again.";

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
            FFRKMySqlInstance.ConnectResult result = FFRKMySqlInstance.ConnectResult.InvalidConnection;
            result = FFRKProxy.Instance.Database.TestConnect(
                textBoxHost.Text, textBoxUser.Text, textBoxPassword.Text, textBoxSchema.Text,
                FFRKProxy.Instance.MinimumRequiredSchema);
            switch (result)
            {
                case FFRKMySqlInstance.ConnectResult.Success:
                    MessageBox.Show(mConnectionSuccessMsg, "Success");
                    break;
                case FFRKMySqlInstance.ConnectResult.InvalidConnection:
                    MessageBox.Show(mInvalidConnectionMsg, "Invalid connection parameters");
                    break;
                case FFRKMySqlInstance.ConnectResult.SchemaTooNew:
                    MessageBox.Show(mDatabaseTooNewMsg, "Database too new");
                    break;
                case FFRKMySqlInstance.ConnectResult.SchemaTooOld:
                    MessageBox.Show(mDatabaseTooOldMsg, "Database too old");
                    break;
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
