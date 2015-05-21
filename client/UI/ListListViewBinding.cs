using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.UI
{
    class ListListViewBinding<T> : IListViewBinding
    {
        private List<T> mCollection;

        public ListListViewBinding()
        {
            mCollection = new List<T>();
        }

        public List<T> Collection
        {
            get { return mCollection; }
            set { mCollection = value; }
        }

        public void SortData(Comparison<object> Comparer)
        {
            mCollection.Sort((x, y) => Comparer(x, y));
        }

        public object RetrieveItem(int Index)
        {
            return mCollection[Index];
        }
    }
}
