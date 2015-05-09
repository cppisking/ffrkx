using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ffrk_winproxy.GameData
{
    class EventListBattles
    {
        [JsonProperty("dungeon_session")]
        public DataDungeonSession DungeonSession;

        [JsonProperty("battles")]
        public List<DataBattle> Battles;

        [JsonProperty("user")]
        public DataUser User;

        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}
