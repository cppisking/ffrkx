using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
    class DeselectableListBox : ListBox
    {
        private ContextMenuStrip mContextMenu;

        [Browsable(true)]
        [Category("Behavior")]
        public event EventHandler SelectionCleared;

        public DeselectableListBox()
        {
            mContextMenu = new ContextMenuStrip();
            mContextMenu.Items.Add("Clear selected items");
            mContextMenu.ItemClicked += ContextMenuStrip_ItemClicked;

            this.ContextMenuStrip = mContextMenu;
        }

        void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.SelectedItems.Clear();
            if (SelectionCleared != null)
                SelectionCleared(mContextMenu, null);
        }
    }
}
