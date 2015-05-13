using FFRKInspector.GameData;
using Fiddler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FFRKInspector.Proxy
{
    class HandleGachaSeriesDetails : IResponseHandler
    {
        public bool CanHandle(string RequestPath)
        {
            return RequestPath.StartsWith("/dff/gacha/probability");
        }

        public void Handle(string RequestPath, string ResponseJson)
        {
            JObject parsed_object = JsonConvert.DeserializeObject<JObject>(ResponseJson);
            DataGachaSeriesItemDetailsList gacha = new DataGachaSeriesItemDetailsList();
            int parameter_start_index = RequestPath.IndexOf('?');
            if (parameter_start_index == -1)
            {
                FiddlerApplication.Log.LogFormat("Unrecognized gacha series details request path {0}.  Expected ?series_id=<n>", RequestPath);
                return;
            }
            string param_string = RequestPath.Substring(parameter_start_index+1);
            Match series_match = Regex.Match(param_string, "series_id=([0-9]+)");
            if (!series_match.Success)
            {
                FiddlerApplication.Log.LogFormat("Unrecognized gacha series details request path {0}.  Expected ?series_id=<n>", RequestPath);
                return;
            }

            uint series_id;
            if (!uint.TryParse(series_match.Groups[1].Value, out series_id))
            {
                FiddlerApplication.Log.LogFormat("Unrecognized gacha series details request path {0}.  series_id does not appear to be an integer.", RequestPath);
                return;
            }

            foreach (var child in parsed_object)
            {
                try
                {
                    uint entry_point_id = uint.Parse(child.Key);
                    if (child.Value.Type != JTokenType.Object)
                        continue;

                    string serialized = JsonConvert.SerializeObject(child.Value);
                    DataGachaSeriesItemDetails this_series = JsonConvert.DeserializeObject<DataGachaSeriesItemDetails>(serialized);

                    // Find the entry point details for this entry point.
                    List<DataGachaSeriesInfo> SeriesList = FFRKProxy.Instance.GameState.GachaSeries.SeriesList;
                    if (SeriesList == null)
                        continue;

                    DataGachaSeriesInfo series = SeriesList.Find(x => x.SeriesId == series_id);
                    if (series == null)
                        continue;

                    DataGachaSeriesEntryPoint entry_point = FindEntryPointForSeries(series, entry_point_id);
                    gacha.Gachas.Add(new KeyValuePair<DataGachaSeriesEntryPoint, DataGachaSeriesItemDetails>(entry_point, this_series));
                }
                catch
                {
                }
            }
            FFRKProxy.Instance.RaiseGachaStats(gacha);
        }

        DataGachaSeriesEntryPoint FindEntryPointForSeries(DataGachaSeriesInfo series, uint entry_point_id)
        {
            foreach (DataGachaSeriesBox box in series.Boxes)
            {
                foreach (DataGachaSeriesEntryPoint entry in box.EntryPoints)
                {
                    if (entry.EntryPointId == entry_point_id)
                        return entry;
                }
            }
            return null;
        }
    }
}
