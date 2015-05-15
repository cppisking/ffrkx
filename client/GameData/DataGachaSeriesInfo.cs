using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataGachaSeriesInfo
    {
        [JsonProperty("series_id")]
        public uint SeriesId;

        [JsonProperty("series_name")]
        public string SeriesName;

        [JsonProperty("box_list")]
        public List<DataGachaSeriesBox> Boxes;
    }
}
