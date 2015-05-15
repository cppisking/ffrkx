using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
    class ListViewColumnSorter : IComparer
    {
        private class SortParameters
        {
            public IComparer Comparer;
            public Utility.IConverter<string, string> Converter;
        }

        private int mColumn;
        private SortOrder mOrder;
        private Dictionary<int, SortParameters> mSorters;

        private class TypedStringComparer<T> : IComparer
        {
            public int Compare(object x, object y)
            {
                T tx = (T)Convert.ChangeType(x, typeof(T));
                T ty = (T)Convert.ChangeType(y, typeof(T));
                return Comparer<T>.Default.Compare(tx, ty);
            }
        }

        public ListViewColumnSorter()
        {
            mColumn = 0;
            mOrder = SortOrder.None;
            mSorters = new Dictionary<int, SortParameters>();
        }

        public void AddSorter<T>(int Index)
        {
            mSorters[Index] = new SortParameters { Comparer = new TypedStringComparer<T>(), Converter = null };
        }

        public void AddSorter<T>(int Index, Utility.IConverter<string, string> Converter)
        {
            mSorters[Index] = new SortParameters { Comparer = new TypedStringComparer<T>(), Converter = Converter };
        }

        public int Compare(object x, object y)
        {
            if (mOrder == SortOrder.None)
                return 0;

            ListViewItem lvix = (ListViewItem)x;
            ListViewItem lviy = (ListViewItem)y;

            string sx = lvix.SubItems[mColumn].Text;
            string sy = lviy.SubItems[mColumn].Text;
            SortParameters sort_params = null;
            int compare_result = 0;
            if (!mSorters.TryGetValue(mColumn, out sort_params))
                compare_result = CaseInsensitiveComparer.Default.Compare(sx, sy);
            else
            {
                if (sort_params.Converter != null)
                {
                    sx = sort_params.Converter.Convert(sx);
                    sy = sort_params.Converter.Convert(sy);
                }

                compare_result = sort_params.Comparer.Compare(sx, sy);
            }

            return (mOrder == SortOrder.Ascending) ? compare_result : (-compare_result);
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
    }
}
