using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataEnemyDropItem
    {
        public enum DropItemType
        {
            Gold = 11,
            Equipment = 41,
            Orb = 51,
            Magicite = 61
        };

        [JsonProperty("rarity")]
        public uint Rarity;

        [JsonProperty("item_id")]
        public uint Id;

        [JsonProperty("type")]
        public DropItemType Type;

        [JsonProperty("amount")]
        public uint GoldAmount;

        [JsonProperty("num")]
        public uint NumberOfItems;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
