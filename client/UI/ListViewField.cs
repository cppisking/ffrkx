using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
    abstract class ListViewField<T> : IListViewField
    {
        private string mDisplayName;
        private FieldWidthStyle mWidthStyle;
        private int mInitialWidth;

        protected ListViewField(string DisplayName)
        {
            mDisplayName = DisplayName;
            mWidthStyle = FieldWidthStyle.AutoSize;
            mInitialWidth = 0;
        }

        protected ListViewField(string DisplayName, FieldWidthStyle WidthStyle, int InitialWidth)
        {
            mDisplayName = DisplayName;
            mWidthStyle = WidthStyle;
            mInitialWidth = InitialWidth;
        }

        public string DisplayName
        {
            get { return mDisplayName; }
        }

        public int Compare(object x, object y)
        {
            return CompareValues((T)x, (T)y);
        }

        public string Format(object value)
        {
            return FormatValue((T)value);
        }

        public string AltFormat(object value)
        {
            return AltFormatValue((T)value);
        }

        protected abstract int CompareValues(T x, T y);
        protected abstract string FormatValue(T value);

        protected virtual string AltFormatValue(T value)
        {
            return FormatValue(value);
        }

        public int InitialWidth
        {
            get { return mInitialWidth; }
        }

        public FieldWidthStyle InitialWidthStyle
        {
            get { return mWidthStyle; }
        }
    }
}
