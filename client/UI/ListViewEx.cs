using FFRKInspector.Config;
using FFRKInspector.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private struct ListViewFieldAdapter
        {
            public IListViewField Field;
            public ColumnHeader Column;
        }

        private Rectangle mHeaderRect;
        private List<ListViewFieldAdapter> mFields;

        private delegate bool EnumWinCallback(IntPtr Hwnd, IntPtr LParam);
        private ContextMenuStrip mHeaderContextMenuStrip;

        private SortOrder mCurrentSortOrder;
        private ColumnHeader mCurrentSortColumn;
        private IListViewBinding mBinding;
        private Config.ListViewSettings mSettings;
        private string mSettingsKey;

        public ListViewEx()
        {
            this.mCurrentSortOrder = SortOrder.None;
            this.mCurrentSortColumn = null;
            this.mFields = new List<ListViewFieldAdapter>();

            this.HandleCreated += ListViewEx_HandleCreated;
            this.RetrieveVirtualItem += ListViewEx_RetrieveVirtualItem;
            this.ColumnClick += ListViewEx_ColumnClick;
            this.ColumnWidthChanged += ListViewEx_ColumnWidthChanged;
        }

        public IListViewBinding DataBinding
        {
            get { return mBinding; }
            set { mBinding = value; }
        }

        [Browsable(true)]
        [Category("Behavior")]
        public string SettingsKey
        {
            get { return mSettingsKey; }
            set { mSettingsKey = value; }
        }

        public void LoadSettings()
        {
            if (mSettingsKey == null)
                return;

            if (!FFRKProxy.Instance.AppSettings.ListViews.TryGetValue(mSettingsKey, out mSettings))
            {
                mSettings = new Config.ListViewSettings();
                FFRKProxy.Instance.AppSettings.ListViews.Add(mSettingsKey, mSettings);
            }
        }

        public void AddField(IListViewField Field)
        {
            ListViewFieldAdapter Adapter = new ListViewFieldAdapter();
            Adapter.Field = Field;
            Adapter.Column = new ColumnHeader();
            Adapter.Column.Text = Field.DisplayName;
            Adapter.Column.Name = Field.GetType().Name;

            int field_width = Field.InitialWidth;
            FieldWidthStyle width_style = Field.InitialWidthStyle;
            ListViewColumnSettings field_settings = null;
            if (mSettings != null)
            {
                field_settings = mSettings.GetColumnSettings(
                    Adapter.Column, Field.InitialWidthStyle, Field.InitialWidth);
                field_width = field_settings.Width;
                width_style = field_settings.WidthStyle;
            }

            Adapter.Column.Width = ComputeColumnWidth(width_style, field_width);

            mFields.Add(Adapter);
            if (field_settings == null || field_settings.Visible)
            {
                Adapter.Column.DisplayIndex = mFields.Count+1;
                Columns.Add(Adapter.Column);
            }
        }

        void ListViewEx_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            ColumnHeader changed_column = Columns[e.ColumnIndex];
            if (mSettings != null)
            {
                var settings = mSettings.Columns[changed_column.Name];
                settings.WidthStyle = FieldWidthStyle.Absolute;
                settings.Width = changed_column.Width;
            }
        }

        void ListViewEx_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ColumnHeader clicked_column = Columns[e.Column];
            if (clicked_column == mCurrentSortColumn)
            {
                if (mCurrentSortOrder == SortOrder.Ascending)
                    mCurrentSortOrder = SortOrder.Descending;
                else if (mCurrentSortOrder == SortOrder.Descending)
                    mCurrentSortOrder = SortOrder.Ascending;
            }
            else
            {
                mCurrentSortColumn = clicked_column;
                mCurrentSortOrder = SortOrder.Ascending;
            }
            
            ListViewFieldAdapter Field = mFields.Find(x => x.Column == clicked_column);
            switch (mCurrentSortOrder)
            {
                case SortOrder.Ascending:
                    mBinding.SortData(Field.Field.Compare);
                    Invalidate();
                    break;
                case SortOrder.Descending:
                    mBinding.SortData((x,y) => -Field.Field.Compare(x,y));
                    Invalidate();
                    break;
            }
        }

        private int ComputeColumnWidth(FieldWidthStyle Style, int Magnitude)
        {
            switch (Style)
            {
                case FieldWidthStyle.Absolute:
                    return Magnitude;
                case FieldWidthStyle.AutoSize:
                    return -2;
                case FieldWidthStyle.Percent:
                    float pct = (float)Magnitude / 100.0f;
                    return (int)(this.Width * pct);
                default:
                    return Magnitude;
            }
        }

        void ListViewEx_HandleCreated(object sender, EventArgs e)
        {
            this.ContextMenuStripChanged += ListViewEx_ContextMenuStripChanged;
            mHeaderContextMenuStrip = new ContextMenuStrip();

            foreach (ColumnHeader header in mFields.Select(x => x.Column))
            {
                ToolStripMenuItem item = new ToolStripMenuItem(header.Text);
                ListViewColumnSettings column_settings = 
                    (mSettings == null) ? null : mSettings.Columns[header.Name];
                item.Checked = (column_settings == null) || column_settings.Visible;

                item.CheckOnClick = true;
                item.Tag = new ToolStripItemTag
                {
                    Header = header,
                    LastIndex = header.DisplayIndex,
                    LastWidth = (mSettings == null) 
                              ? header.Width 
                              : ComputeColumnWidth(column_settings.WidthStyle, column_settings.Width)
                };
                item.CheckStateChanged += item_CheckStateChanged;
                mHeaderContextMenuStrip.Items.Add(item);
            }
            mHeaderContextMenuStrip.Opening += mHeaderContextMenuStrip_Opening;

            // If the user has not yet set a context menu strip, set ours.
            if (ContextMenuStrip == null)
                ContextMenuStrip = mHeaderContextMenuStrip;
        }

        void ListViewEx_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            object item = mBinding.RetrieveItem(e.ItemIndex);
            List<string> fields = new List<string>();
            foreach (ListViewFieldAdapter field in mFields)
            {
                if (field.Column.DisplayIndex == -1)
                    continue;

                string formatted_value = field.Field.Format(item);
                fields.Add(formatted_value);
            }
            e.Item = new ListViewItem(fields.ToArray());
        }

        void item_CheckStateChanged(object sender, EventArgs e)
        {
            // A column was chosen to be either hidden or displayed.
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            ToolStripItemTag tag = (ToolStripItemTag)item.Tag;
            ColumnHeader column_to_change = tag.Header;

            // Find the corresponding field so we can update its visibility state.
            ListViewFieldAdapter Field = mFields.Find(x => x.Column == column_to_change);

            if (item.CheckState == CheckState.Checked)
            {
                int new_index = DetermineIndexForInsertingColumn(tag.Header);
                Columns.Insert(new_index, tag.Header);
                tag.Header.DisplayIndex = new_index;
                tag.Header.Width = tag.LastWidth;
                if (mSettings != null)
                    mSettings.Columns[column_to_change.Name].Visible = true;
            }
            else
            {
                tag.LastWidth = tag.Header.Width;
                Columns.Remove(tag.Header);
                if (mSettings != null)
                    mSettings.Columns[column_to_change.Name].Visible = false;
            }
        }

        private int DetermineIndexForInsertingColumn(ColumnHeader Header)
        {
            // We want to find where this column should go in the internal list view's
            // column header collection.  We want to put it after the last currently
            // displayed item which comes before this item in the master field collection.
            return mFields.TakeWhile(x => x.Column != Header)
                          .Count(x => x.Column.DisplayIndex != -1);
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
    }
}
