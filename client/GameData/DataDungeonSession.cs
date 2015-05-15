using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FFRKInspector.GameData
{
    class DataDungeonSession
    {
        [JsonProperty("world_id")]
        public uint WorldId;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("series_id")]
        public uint SeriesId;

        [JsonProperty("battle_id")]
        public uint BattleId;

        [JsonProperty("dungeon_id")]
        public uint DungeonId;

        [JsonProperty("challenge_level")]
        public uint Difficulty;

        [JsonProperty("type")]
        public uint Type;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
