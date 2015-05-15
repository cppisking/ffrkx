using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FFRKInspector.GameData
{
    class EventListDungeons
    {
        [JsonProperty("dungeons")]
        public List<DataDungeon> Dungeons;

        [JsonProperty("user")]
        public DataUser User;

        [JsonProperty("world")]
        public DataWorld World;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
