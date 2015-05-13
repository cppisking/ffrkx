using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataGachaSeriesBox
    {
        [JsonProperty("box_id")]
        public uint BoxId;

        [JsonProperty("entry_point_list")]
        public List<DataGachaSeriesEntryPoint> EntryPoints;
    }
}
