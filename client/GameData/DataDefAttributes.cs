using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataDefAttributes
    {
        [JsonProperty("attribute_id")]
        public uint Id;

        [JsonProperty("factor")]
        public uint Factor;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
