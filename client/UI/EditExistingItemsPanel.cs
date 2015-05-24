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
using FFRKInspector.GameData;

namespace FFRKInspector.UI
{
    public partial class EditExistingItemsPanel : UserControl, FFRKDataBoundPanel
    {
        private static readonly int kBaseStatsColumnZero = 5;
        private static readonly int kMaxStatsColumnZero = 12;

        public EditExistingItemsPanel()
        {
            InitializeComponent();

            type.CellTemplate = new EnumDataViewGridCell<SchemaConstants.ItemType>();
            subtype.CellTemplate = new EnumDataViewGridCell<SchemaConstants.EquipmentCategory>();
        }

        private void EditExistingItemsPanel_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (FFRKProxy.Instance != null)
            {
                this.equipment_statsTableAdapter.Connection.ConnectionString = FFRKProxy.Instance.Database.ConnectionString;
#if !ALLOW_ITEM_EDITING
                this.equipment_statsTableAdapter.Adapter.UpdateCommand = null;
#endif
            }
        }

        public void Reload()
        {
            this.equipment_statsTableAdapter.Fill(this.equipmentStatsDataSet.equipment_stats);
        }

        public void Commit()
        {
#if ALLOW_ITEM_EDITING
            this.equipment_statsTableAdapter.Update(this.equipmentStatsDataSet.equipment_stats);
#else
            MessageBox.Show("Editing items is not supported.  Use the missing items panel to submit missing or incorrect items.");
#endif
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            int col = 0;
            int row = 0;
            DataObject clipboard_data;
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        cell.Value = DBNull.Value;
                    e.Handled = true;
                    break;
                case Keys.Left:
                    // When Control is down, move left by section
                    if (!e.Control || dataGridView1.CurrentCell == null)
                        break;

                    col = dataGridView1.CurrentCell.ColumnIndex;
                    row = dataGridView1.CurrentCell.RowIndex;

                    if (col <= kBaseStatsColumnZero)
                        col = 0;
                    else if (col <= kMaxStatsColumnZero)
                        col = kBaseStatsColumnZero;
                    else
                        col = kMaxStatsColumnZero;
                    dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[col];
                    e.Handled = true;
                    break;
                case Keys.Right:
                    // When Control is down, move right by section
                    if (!e.Control || dataGridView1.CurrentCell == null)
                        break;

                    col = dataGridView1.CurrentCell.ColumnIndex;
                    row = dataGridView1.CurrentCell.RowIndex;

                    if (col >= kMaxStatsColumnZero)
                        col = dataGridView1.Columns.Count - 1;
                    else if (col >= kBaseStatsColumnZero)
                        col = kMaxStatsColumnZero;
                    else
                        col = kBaseStatsColumnZero;
                    dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[col];
                    e.Handled = false;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Tab:
                    if (dataGridView1.CurrentCell == null)
                        break;

                    // If shift is down, move to next non-empty cell.
                    // If shift is not down move to previous non-empty cell.
                    DataGridViewCell first = dataGridView1.CurrentCell;
                    if (e.Shift)
                        dataGridView1.CurrentCell = FindPreviousNonEmptyCell(dataGridView1.CurrentCell);
                    else
                        dataGridView1.CurrentCell = FindNextNonEmptyCell(dataGridView1.CurrentCell);
                    e.Handled = true;
                    break;
                case Keys.C:
                    // When Control is down, copy selection to clipboard
                    if (!e.Control)
                        break;
                    DataObject d = dataGridView1.GetClipboardContent();
                    Clipboard.SetDataObject(d);
                    e.Handled = true;
                    break;
                case Keys.X:
                    // When Control is down, cut selection to clipboard
                    if (!e.Control)
                        break;
                    clipboard_data = dataGridView1.GetClipboardContent();
                    Clipboard.SetDataObject(clipboard_data);
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        cell.Value = DBNull.Value;
                    e.Handled = true;
                    break;
                case Keys.V:
                    // When Control si down, paste selection from clipboard
                    if (!e.Control)
                        break;
                    clipboard_data = Clipboard.GetDataObject() as DataObject;
                    PasteAtCurrentLocation(clipboard_data.GetText().Split('\n'));
                    e.Handled = true;
                    break;
            }
            if (e.Handled)
                e.SuppressKeyPress = true;
        }

        private DataGridViewCell FindNextNonEmptyCell(DataGridViewCell cell)
        {
            int row = cell.RowIndex;
            int col = cell.ColumnIndex+1;
            int num_cols = dataGridView1.Columns.Count;
            DataGridViewCell this_cell = null;
            for (int r = row; r < dataGridView1.Rows.Count; r++)
            {
                while (col < dataGridView1.Columns.Count)
                {
                    this_cell = dataGridView1.Rows[r].Cells[col];
                    if (this_cell.Value != DBNull.Value)
                        return this_cell;
                    col++;
                }
                col = 0;
            }
            return this_cell;
        }

        private DataGridViewCell FindPreviousNonEmptyCell(DataGridViewCell cell)
        {
            int row = cell.RowIndex;
            int col = cell.ColumnIndex-1;
            int num_cols = dataGridView1.Columns.Count;
            DataGridViewCell this_cell = null;
            for (int r = row; r >= 0; r--)
            {
                while (col >= 0)
                {
                    this_cell = dataGridView1.Rows[r].Cells[col];
                    if (this_cell.Value != DBNull.Value)
                        return this_cell;
                    col = col - 1;
                }
                col = num_cols - 1;
            }
            return this_cell;
        }

        private DataGridViewCell FindSymmetricCell(DataGridViewCell cell)
        {
            int column = cell.ColumnIndex;
            if (column < kBaseStatsColumnZero)
                return null;
            if (column < kMaxStatsColumnZero)
                return dataGridView1.Rows[cell.RowIndex].Cells[column + kMaxStatsColumnZero - kBaseStatsColumnZero];

            return dataGridView1.Rows[cell.RowIndex].Cells[column - (kMaxStatsColumnZero - kBaseStatsColumnZero)];
        }

        private void PasteAtCurrentLocation(string[] lines)
        {
            int start_row_index = dataGridView1.CurrentCell.RowIndex;
            int start_col_index = dataGridView1.CurrentCell.ColumnIndex;
            foreach (string line in lines)
            {
                string[] cells = line.TrimEnd('\r').Split('\t');
                int current_col = start_col_index;
                DataGridViewRow dest_row = dataGridView1.Rows[start_row_index];
                foreach (string cell_text in cells)
                {
                    if (current_col >= dest_row.Cells.Count)
                        break;

                    DataGridViewCell dest_cell = dest_row.Cells[current_col];
                    if (cell_text == String.Empty)
                        dest_cell.Value = DBNull.Value;
                    else
                        dest_cell.Value = Convert.ChangeType(cell_text, dest_cell.ValueType);
                    current_col++;
                }
                start_row_index++;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }

        private void dataGridView1_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {

        }
    }
}
