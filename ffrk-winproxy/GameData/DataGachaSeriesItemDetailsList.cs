using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataGachaSeriesItemDetailsList
    {
        public List<KeyValuePair<DataGachaSeriesEntryPoint, DataGachaSeriesItemDetails>> Gachas;

        public DataGachaSeriesItemDetailsList()
        {
            Gachas = new List<KeyValuePair<DataGachaSeriesEntryPoint, DataGachaSeriesItemDetails>>();
        }
    }
}
