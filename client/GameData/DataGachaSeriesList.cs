using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataGachaSeriesList
    {
        [JsonProperty("series_list")]
        public List<DataGachaSeriesInfo> SeriesList;
    }
}
