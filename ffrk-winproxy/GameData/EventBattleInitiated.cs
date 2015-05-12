using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    internal class EventBattleInitiated
    {
        [JsonProperty("battle")]
        public DataActiveBattle Battle;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
