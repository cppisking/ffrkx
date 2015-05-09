using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ffrk_winproxy.GameData
{
    class DataWorld
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("series_id")]
        public uint SeriesId;

        [JsonProperty("Id")]
        public uint Id;

        [JsonProperty("type")]
        public uint Type;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
