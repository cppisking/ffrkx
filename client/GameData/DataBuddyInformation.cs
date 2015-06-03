using FFRKInspector.GameData.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataBuddyInformation
    {
        [JsonProperty("row")]
        public SchemaConstants.PartyFormation Formation;

        [JsonProperty("id")]
        public uint Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("ability_1_id")]
        public uint Ability1;

        [JsonProperty("ability_2_id")]
        public uint Ability2;

        [JsonProperty("level")]
        public byte Level;

        [JsonProperty("level_max")]
        public byte LevelMax;

        [JsonProperty("soul_strike_id")]
        public uint SoulStrike;

        [JsonProperty("job_name")]
        public string Job;

        [JsonProperty("weapon_id")]
        public uint WeaponId;

        [JsonProperty("armor_id")]
        public uint ArmorId;

        [JsonProperty("accessory_id")]
        public uint AccessoryId;

        [JsonProperty("buddy_id")]
        public uint BuddyId;

        [JsonProperty("series_id")]
        public uint SeriesId;

        [JsonProperty("equipment_category")]
        [JsonConverter(typeof(EquipUsageListConverter))]
        public List<DataBuddyEquipUsage> EquipUsage;

        public IEnumerable<SchemaConstants.EquipmentCategory> UsableEquipCategories
        {
            get {  return EquipUsage.Select(x => x.Category); }
        }
    }
}
