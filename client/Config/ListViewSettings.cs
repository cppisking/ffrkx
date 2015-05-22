using FFRKInspector.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKInspector.Config
{
    public class ListViewColumnSettings
    {
        public ListViewColumnSettings()
        {

        }

        [JsonProperty("width_style")]
        public FieldWidthStyle WidthStyle;
        [JsonProperty("width")]
        public int Width;
        [JsonProperty("visible")]
        public bool Visible;
    }

    public class ListViewSettings
    {
        public ListViewSettings()
        {
            Columns = new Dictionary<string, ListViewColumnSettings>();
        }

        [JsonProperty("columns")]
        public Dictionary<string, ListViewColumnSettings> Columns;

        public ListViewColumnSettings GetColumnSettings(
            ColumnHeader Header, FieldWidthStyle DefaultStyle, int DefaultWidth)
        {
            ListViewColumnSettings settings;
            if (Columns.TryGetValue(Header.Name, out settings))
                return settings;

            settings = new ListViewColumnSettings();
            settings.WidthStyle = DefaultStyle;
            settings.Width = DefaultWidth;
            settings.Visible = true;
            Columns.Add(Header.Name, settings);
            return settings;
        }
    }

}
