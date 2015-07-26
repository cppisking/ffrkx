using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataPotion
    {
        [JsonProperty("type")]
        public uint Type;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;

    }
}
