using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataEnemyChild
    {
        [JsonProperty("enemy_id")]
        public uint EnemyId;

        [JsonProperty("params")]
        public List<DataEnemyParam> Params;

        [JsonProperty("drop_item_list")]
        public List<DataEnemyDropItem> DropItems;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
