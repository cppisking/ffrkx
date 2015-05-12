using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FFRKInspector.GameData
{
    class DataUser
    {
        [JsonProperty("dungeon_id")]
        public uint DungeonId;

        [JsonProperty("user_id")]
        public uint UserId;

        [JsonProperty("stamina")]
        public uint Stamina;

        [JsonProperty("gil")]
        public uint Gil;

        [JsonProperty("stamina_recovery_time_remaining")]
        public uint StaminaRecoveryTimeRemaining;

        [JsonProperty("max_stamina")]
        public uint MaxStamina;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
