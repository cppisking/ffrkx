using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData.Party
{
    class DataPartySlotToBuddyIdRoot
    {
        [JsonProperty("slot_to_buddy_id")]
        public DataPartySlotToBuddyId BuddyMap;
    }
}
