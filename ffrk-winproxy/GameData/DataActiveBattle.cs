using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffrk_winproxy.GameData
{
    class DataActiveBattle
    {
        [JsonProperty("rounds")]
        public List<DataBattleRound> Rounds;

        [JsonProperty("battle_id")]
        public uint BattleId;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;

        public IEnumerable<DropEvent> Drops
        {
            get
            {
                foreach (DataBattleRound Round in Rounds)
                    foreach (DropEvent drop in Round.Drops)
                        yield return drop;
            }
        }
    }
}
