using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
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


        public IEnumerable<BasicEnemyInfo> Enemies
        {
            get
            {
                HashSet<BasicEnemyInfo> infos = new HashSet<BasicEnemyInfo>();
                foreach (DataBattleRound Round in Rounds)
                {
                    foreach (DataEnemy enemy in Round.Enemies)
                    {
                        foreach (DataEnemyChild child in enemy.Children)
                        {
                            // Not sure what the deal is with child and params, or why an enemy can theoretically
                            // have more than one name.  I assume it has something to do with enemies that have
                            // different body parts, but haven't figured it out yet.
                            if (child.Params.Count == 0)
                                continue;
                            infos.Add(new BasicEnemyInfo { EnemyId = child.EnemyId, EnemyName = child.Params[0].Name });
                            break;
                        }
                    }
                }
                return infos;
            }
        }
    }
}
