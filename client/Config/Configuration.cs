using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Config
{
    public class AppSettings
    {
        public AppSettings()
        {
            mListViewSettings = new Dictionary<string, ListViewSettings>();
        }

        [JsonProperty("list_views")]
        private Dictionary<string, ListViewSettings> mListViewSettings;

        [JsonIgnore]
        public Dictionary<string, ListViewSettings> ListViews
        {
            get { return mListViewSettings; }
        }
    }
}
