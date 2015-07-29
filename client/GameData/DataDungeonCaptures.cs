using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FFRKInspector.GameData
{
    class DataDungeonCaptures
    {
        [JsonProperty("sp_scores")]
        public List<DataDungeonSpScore> SpScore;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
