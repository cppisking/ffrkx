using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.UI.ListViewFields
{
    class TrivialField<T, U> : ListViewField<T> where U : IComparable<U>
    {
        private Converter<T, U> mGetter;
        protected TrivialField(string DisplayName, Converter<T, U> Getter)
            : base(DisplayName)
        {
            mGetter = Getter;
        }

        protected TrivialField(string DisplayName, Converter<T, U> Getter, FieldWidthStyle WidthStyle, int Width)
            : base(DisplayName, WidthStyle, Width)
        {
            mGetter = Getter;
        }

        protected override int CompareValues(T x, T y)
        {
            return mGetter(x).CompareTo(mGetter(y));
        }

        protected override string FormatValue(T value)
        {
            return mGetter(value).ToString();
        }
    }
}
