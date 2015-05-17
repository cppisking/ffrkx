using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData.Party
{
    class DataEquipmentInformation
    {
        [JsonProperty("id")]
        public uint InstanceId;

        [JsonProperty("equipment_id")]
        public uint EquipmentId;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("base_rarity")]
        public SchemaConstants.Rarity BaseRarity;

        [JsonProperty("series_id")]
        public uint SeriesId;

        [JsonProperty("category_id")]
        public SchemaConstants.EquipmentCategory Category;
    }
}
