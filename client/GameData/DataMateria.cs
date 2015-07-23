using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataMateria
    {
        [JsonProperty("name")]
        public string Name;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;

    }
}
