using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FFRKInspector.GameData
{
    class DataUserDungeon
    {
        [JsonProperty("captures")]
        public List<DataDungeonCaptures> Captures;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
