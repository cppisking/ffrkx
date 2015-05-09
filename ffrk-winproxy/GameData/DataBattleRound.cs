using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffrk_winproxy.GameData
{
    class DataBattleRound
    {
        [JsonProperty("enemy")]
        public List<DataEnemy> Enemies;

        [JsonProperty("round")]
        public uint Index;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;

        public IEnumerable<DropEvent> Drops
        {
            get
            {
                foreach (DataEnemy enemy in Enemies)
                    foreach (DropEvent drop in enemy.Drops)
                    {
                        drop.Round = Index;
                        yield return drop;
                    }
            }
        }
    }
}
