using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataEnemyParam
    {
        [JsonProperty("disp_name")]
        public string Name;

        [JsonProperty("id")]
        public uint Id;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
