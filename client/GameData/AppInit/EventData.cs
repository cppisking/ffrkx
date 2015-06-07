using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData.AppInit
{
    class EventData
    {
        [JsonProperty("world_id")]
        public uint WorldId;

        [JsonProperty("type")]
        public SchemaConstants.EventType type;
    }
}
