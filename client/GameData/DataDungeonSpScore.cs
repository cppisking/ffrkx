using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace FFRKInspector.GameData
{
    class DataDungeonSpScore
    {
        [JsonProperty("title")]
        public string Title;

        [JsonProperty("battle_id")]
        public uint BattleID;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
