using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
    class VirtualListViewFieldManager<T>
    {
        public delegate int ColumnComparisonDelegate(T x, T y);

        private ColumnHeader mColumn;
        private SortOrder mOrder;
        private Dictionary<ColumnHeader, Comparison<T>> mComparers;
        private Dictionary<ColumnHeader, Converter<T, string>> mConverters;

        public VirtualListViewFieldManager()
        {
            mOrder = SortOrder.None;
            mComparers = new Dictionary<ColumnHeader, Comparison<T>>();
            mConverters = new Dictionary<ColumnHeader, Converter<T, string>>();
        }

        public void AddColumn(ColumnHeader Column, Comparison<T> Comparer, Converter<T, string> Converter)
        {
            mComparers[Column] = Comparer;
            mConverters[Column] = Converter;
        }

        public ColumnHeader SortColumn
        {
            get { return mColumn; }
            set { mColumn = value; }
        }

        public SortOrder Order
        {
            get { return mOrder; }
            set { mOrder = value; }
        }

        public Comparison<T> ComparisonFunction
        {
            get
            {
                if (mOrder == SortOrder.None)
                    return ((x, y) => 0);
                Comparison<T> comparer;
                if (!mComparers.TryGetValue(mColumn, out comparer))
                    return ((x, y) => 0);
                if (mOrder == SortOrder.Ascending)
                    return comparer;
                return ((x, y) => -comparer(x, y));
            }
        }

        public string GetFieldValue(ColumnHeader Header, T Item)
        {
            return mConverters[Header](Item);
        }
    }
}
