using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData.AppInit
{
    class AppInitData
    {
        [JsonProperty("events")]
        public List<EventData> Events;

        [JsonProperty("worlds")]
        public List<DataWorld> Worlds;

        [JsonProperty("user")]
        public DataUser User;
    }
}
