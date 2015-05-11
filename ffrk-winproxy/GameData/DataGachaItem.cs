using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffrk_winproxy.GameData
{
    class DataGachaItem
    {
        [JsonProperty("category_name")]
        public string Category;

        [JsonProperty("category_id")]
        public ushort CategoryId;

        [JsonProperty("id")]
        public uint Id;

        [JsonProperty("probability")]
        public float Probability;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("has_soul_strike")]
        public bool HasSoulStrike;

        [JsonProperty("description")]
        public string AddedEffect;

        [JsonProperty("rarity")]
        public byte Rarity;
    }
}
