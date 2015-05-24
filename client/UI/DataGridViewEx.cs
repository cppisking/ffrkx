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
