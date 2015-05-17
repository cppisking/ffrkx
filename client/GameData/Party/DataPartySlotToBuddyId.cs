using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData.Party
{
    class DataPartySlotToBuddyId
    {
        [JsonProperty("1")]
        public uint First;

        [JsonProperty("2")]
        public uint Second;

        [JsonProperty("3")]
        public uint Third;

        [JsonProperty("4")]
        public uint Fourth;

        [JsonProperty("5")]
        public uint Fifth;
    }
}
