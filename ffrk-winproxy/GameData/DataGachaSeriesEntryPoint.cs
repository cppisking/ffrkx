using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataGachaSeriesEntryPoint
    {
        public enum PayId
        {
            Gems = 0,
            Mythril = 91000000
        }

        [JsonProperty("entry_point_id")]
        public uint EntryPointId;

        [JsonProperty("pay_cost")]
        public uint PayCost;

        [JsonProperty("pay_id")]
        public PayId CurrencyType;
    }
}
