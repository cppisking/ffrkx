using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataGachaSeriesItemsForEntryPoints
    {
        public class ItemsForEntryPoint
        {
            public DataGachaSeriesEntryPoint EntryPoint;
            public DataGachaSeriesItemDetails ItemDetails;
        }

        public Dictionary<uint, ItemsForEntryPoint> Gachas;

        public DataGachaSeriesItemsForEntryPoints()
        {
            Gachas = new Dictionary<uint, ItemsForEntryPoint>();
        }
    }
}
