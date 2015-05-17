using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData.Converters
{
    class PartySlotListConverter : JsonCreationConverter<uint[]>
    {
        protected override uint[] Create(Type ObjectType, JObject Object)
        {
            uint[] result = new uint[5];
            JObject id_list = (JObject)Object.GetValue("slot_to_buddy_id");
            foreach (var child in id_list)
            {
                uint index = Convert.ToUInt32(child.Key);
                uint id = Convert.ToUInt32(((JValue)child.Value).Value);
                result[index-1] = id;
            }
            return result;
        }
    }
}
