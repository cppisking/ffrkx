using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class DataGachaSeriesItemDetails
    {
        [JsonProperty("prob_by_rarity")]
        public DataGachaSeriesProbabilitiesByRarity ProbabilityByRarity;

        // The JSON for this property is in both the 'equipments' array as well as
        // the 'prob_by_item' array.  They each contain one different field, but otherwise
        // appear to be identical.  The missing field from the 'equipments' table doesn't
        // appear useful, so we use the 'prob_by_item' array and ignore the other.
        [JsonProperty("prob_by_item")]
        public List<DataGachaItem> Items;
    }
}
