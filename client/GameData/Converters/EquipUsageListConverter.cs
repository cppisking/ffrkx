using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FFRKInspector.GameData.Converters
{
    class EquipUsageListConverter : CustomCreationConverter<List<DataBuddyEquipUsage>>
    {
        public override List<DataBuddyEquipUsage> Create(Type objectType)
        {
            return new List<DataBuddyEquipUsage>();
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);

            List<DataBuddyEquipUsage> target = Create(objectType);

            IList<JToken> results = obj.Children().ToList();

            foreach (JToken result in results)
            {
                DataBuddyEquipUsage usage = JsonConvert.DeserializeObject<DataBuddyEquipUsage>(result.First.ToString());
                target.Add(usage);
            }

            return target;
        }
    }
}
