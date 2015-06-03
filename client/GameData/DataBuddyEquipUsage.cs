using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataBuddyEquipUsage
    {
        [JsonProperty("category_id")]
        public SchemaConstants.EquipmentCategory Category;

        [JsonProperty("equipment_type")]
        public SchemaConstants.ItemType Type;

        [JsonProperty("factor")]
        public ushort Factor;
    }
}
