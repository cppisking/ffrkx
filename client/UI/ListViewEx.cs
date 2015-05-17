using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
    class ListViewEx : ListView
    {
        private Rectangle mHeaderRect;
        private ColumnHeader[] mConstantHeaderList;

        private delegate bool EnumWinCallback(IntPtr Hwnd, IntPtr LParam);
        private ContextMenuStrip mHeaderContextMenuStrip;

        private class ToolStripItemTag
        {
            public int LastIndex;
            public int LastWidth;
            public ColumnHeader Header;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern int EnumChildWindows(IntPtr HwndParent, EnumWinCallback Callback, IntPtr LParam);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr Hwnd, out RECT Rect);

        public ListViewEx()
        {
            this.HandleCreated += ListViewEx_HandleCreated;
        }

        void ListViewEx_HandleCreated(object sender, EventArgs e)
        {
            this.ContextMenuStripChanged += ListViewEx_ContextMenuStripChanged;
            mHeaderContextMenuStrip = new ContextMenuStrip();

            // This is a list of column headers when the control was created.  We use this to 
            // determine where to insert a column into the column list after it is shown again
            // after being hidden, since all the DisplayIndexes could have changed.
            mConstantHeaderList = new ColumnHeader[Columns.Count];

            foreach (ColumnHeader header in Columns)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(header.Text);
                mConstantHeaderList[header.DisplayIndex] = header;
                item.Checked = true;
                item.CheckOnClick = true;
                item.Tag = new ToolStripItemTag { Header = header, LastIndex = header.DisplayIndex };
                item.CheckStateChanged += item_CheckStateChanged;
                mHeaderContextMenuStrip.Items.Add(item);
            }
            mHeaderContextMenuStrip.Opening += mHeaderContextMenuStrip_Opening;

            // If the user has not yet set a context menu strip, set ours.
            if (ContextMenuStrip == null)
                ContextMenuStrip = mHeaderContextMenuStrip;
        }

        void item_CheckStateChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            ToolStripItemTag tag = (ToolStripItemTag)item.Tag;
            if (item.CheckState == CheckState.Checked)
            {
                int new_index = FindIndexForColumn(tag.Header);
                Columns.Insert(new_index, tag.Header);
                tag.Header.DisplayIndex = new_index;
                tag.Header.Width = tag.LastWidth;
            }
            else
            {
                tag.LastWidth = tag.Header.Width;
                Columns.Remove(tag.Header);
            }
        }

        private int FindIndexForColumn(ColumnHeader Header)
        {
            int this_index = Array.IndexOf(mConstantHeaderList, Header);
            if (this_index <= 0)
                return 0;

            // Work backwards in the constant header list looking for an item that is
            // still in the current list.
            for (int i=this_index; i >= 0; --i)
            {
                ColumnHeader next = mConstantHeaderList[i];
                int found_index = Columns.IndexOf(next);
                if (found_index >= 0)
                    return found_index + 1;
            }
            return 0;
        }

        void ListViewEx_ContextMenuStripChanged(object sender, EventArgs e)
        {
            if (ContextMenuStrip == mHeaderContextMenuStrip)
                return;

            if (ContextMenuStrip != null)
                ContextMenuStrip.Opening += CustomContextMenuStrip_Opening;
            else
                ContextMenuStrip = mHeaderContextMenuStrip;
        }

        void mHeaderContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Our context menu was shown.  If there was no user defined context menu, then this may
            // have occurred outside the header rect.  So cancel if that's the case.
            EnumChildWindows(Handle, new EnumWinCallback(EnumWindowCallback), IntPtr.Zero);

            // Don't display the header context menu strip if the click didn't occur in the header area.
            if (!mHeaderRect.Contains(MousePosition))
                e.Cancel = true;
        }

        void CustomContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Right click occurred while a user-defined context menu is set.  If it's inside the
            // header rect, show our context menu instead.  Otherwise show the user's.
            EnumChildWindows(Handle, new EnumWinCallback(EnumWindowCallback), IntPtr.Zero);

            if (!mHeaderRect.Contains(MousePosition))
            {
                // Display the regular context menu strip.
                return;
            }

            e.Cancel = true;
            mHeaderContextMenuStrip.Show(MousePosition);
        }

        private bool EnumWindowCallback(IntPtr Hwnd, IntPtr LParam)
        {
            RECT rect;
            if (!GetWindowRect(Hwnd, out rect))
                mHeaderRect = Rectangle.Empty;
            else
            {
                mHeaderRect = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left + 1, rect.Bottom - rect.Top + 1);
            }
            return false;
        }

        public ColumnHeader[] OrderedColumnHeaders
        {
            get
            {
                ColumnHeader[] array = new ColumnHeader[Columns.Count];
                foreach (ColumnHeader header in Columns)
                    array[header.DisplayIndex] = header;
                return array;
            }
        }
    }
}
