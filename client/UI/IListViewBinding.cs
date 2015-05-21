using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.UI
{
    interface IListViewBinding
    {
        void SortData(Comparison<object> Comparer);
        object RetrieveItem(int Index);
    }
}
