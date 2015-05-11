using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffrk_winproxy.GameData
{
    class DataGachaProbabilitiesByRarity
    {
        [JsonProperty("1")]
        public float OneStar;

        [JsonProperty("2")]
        public float TwoStar;

        [JsonProperty("3")]
        public float ThreeStar;

        [JsonProperty("4")]
        public float FourStar;

        [JsonProperty("5")]
        public float FiveStar;
    }
}
