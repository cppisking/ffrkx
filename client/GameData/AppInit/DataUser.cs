using FFRKInspector.GameData.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData.AppInit
{
    class DataUser
    {
        [JsonProperty("last_logined_at")]
        public ulong LastLoginTime;

        [JsonProperty("start_time_of_today")]
        [JsonConverter(typeof(EpochToDateTime))]
        public DateTime StartTimeOfToday;
    }
}
