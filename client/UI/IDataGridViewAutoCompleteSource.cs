using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
    interface IDataGridViewAutoCompleteSource
    {
        AutoCompleteStringCollection AutoCompleteSource { get; }
    }
}
