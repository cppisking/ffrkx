using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
    class VirtualListViewColumnSorter<T>
    {
        public delegate int ColumnComparisonDelegate(T x, T y);

        private int mColumn;
        private SortOrder mOrder;
        private Dictionary<int, Comparison<T>> mComparers;

        public VirtualListViewColumnSorter()
        {
            mColumn = 0;
            mOrder = SortOrder.None;
            mComparers = new Dictionary<int, Comparison<T>>();
        }

        public void AddSorter(int Index, Comparison<T> Comparer)
        {
            mComparers[Index] = Comparer;
        }

        public int SortColumn
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
                return comparer;
            }
        }
    }
}
