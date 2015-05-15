using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using FFRKInspector.GameData.Converters;
using Newtonsoft.Json.Linq;

namespace FFRKInspector.GameData
{
    class DataBattle
    {
        [JsonProperty("round_num")]
        public uint RoundNumber;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("has_boss", ItemConverterType = typeof(IntToBool))]
        public bool HasBoss;

        [JsonProperty("stamina")]
        public ushort Stamina;

        [JsonProperty("is_unlocked")]
        public bool IsUnlocked;

        [JsonProperty("dungeon_id")]
        public uint DungeonId;

        [JsonProperty("id")]
        public uint Id;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
