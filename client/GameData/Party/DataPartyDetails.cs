using FFRKInspector.GameData.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData.Party
{
    class DataPartyDetails
    {
        [JsonProperty("party")]
        public DataPartySlotToBuddyIdRoot PartySlotToBuddyId;

        [JsonProperty("buddies")]
        public DataBuddyInformation[] Buddies;

        [JsonProperty("equipments")]
        public DataEquipmentInformation[] Equipments;
    }
}
