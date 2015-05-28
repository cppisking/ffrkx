using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
    class DataGridViewEx : DataGridView
    {
        public DataGridViewEx()
        {
            this.EditingControlShowing += DataGridViewEx_EditingControlShowing;
        }

        void DataGridViewEx_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewCell template = CurrentCell.OwningColumn.CellTemplate;
            if (template == null)
                return;
            IDataGridViewAutoCompleteSource source = template as IDataGridViewAutoCompleteSource;
            if (source == null)
                return;
            TextBox box = e.Control as TextBox;
            if (box != null)
            {
                box.AutoCompleteMode = AutoCompleteMode.Append;
                box.AutoCompleteSource = AutoCompleteSource.CustomSource;
                box.AutoCompleteCustomSource = source.AutoCompleteSource;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            return base.ProcessDialogKey(keyData);
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            // If the edit control is showing, just call OnKeyDown directly, because the base
            // class has weird behavior in this case and we want to handle it.
            switch (e.KeyCode)
            {
                case Keys.Right:
                case Keys.Left:
                    if (IsCurrentCellInEditMode && e.Control)
                    {
                        OnKeyDown(e);
                        return true;
                    }
                    break;
            }

            return base.ProcessDataGridViewKey(e);
        }
    }
}
