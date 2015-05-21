using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataEnemy
    {
        [JsonProperty("enemy_id")]
        public uint Id;

        [JsonProperty("children")]
        public List<DataEnemyChild> Children;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;

        public IEnumerable<DropEvent> Drops
        {
            get
            {
                foreach (DataEnemyChild child in Children)
                    foreach (DataEnemyDropItem drop_item in child.DropItems)
                    {
                        DropEvent drop = new DropEvent();
                        drop.ItemId = drop_item.Id;
                        drop.Rarity = drop_item.Rarity;
                        drop.ItemType = drop_item.Type;
                        drop.GoldAmount = drop_item.GoldAmount;
                        drop.NumberOfItems = drop_item.NumberOfItems;

                        drop.EnemyId = child.EnemyId;
                        if (child.Params.Count > 0)
                            drop.EnemyName = child.Params[0].Name;
                        yield return drop;
                    }
            }
        }
    }
}
