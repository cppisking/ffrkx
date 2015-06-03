using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
    public enum FieldWidthStyle
    {
        Percent,
        Absolute,
        AutoSize,
        Fill
    }

    interface IListViewField
    {
        string DisplayName { get; }
        int Compare(object x, object y);
        string Format(object value);
        string AltFormat(object value);

        int InitialWidth { get; }
        FieldWidthStyle InitialWidthStyle { get; }
    }
}
