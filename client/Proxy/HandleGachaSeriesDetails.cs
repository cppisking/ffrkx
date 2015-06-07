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
    class HandleGachaSeriesDetails : SimpleResponseHandler
    {
        public override bool CanHandle(Session Session)
        {
            string RequestPath = Session.oRequest.headers.RequestPath;
            return RequestPath.StartsWith("/dff/gacha/probability");
        }

        public override void Handle(Session Session)
        {
            string ResponseJson = Session.GetResponseBodyAsString();
            string RequestPath = Session.oRequest.headers.RequestPath;

            JObject parsed_object = JsonConvert.DeserializeObject<JObject>(ResponseJson);
            int parameter_start_index = RequestPath.IndexOf('?');
            if (parameter_start_index == -1)
            {
                Utility.Log.LogFormat("Unrecognized gacha series details request path {0}.  Expected ?series_id=<n>", RequestPath);
                return;
            }
            string param_string = RequestPath.Substring(parameter_start_index+1);
            Match series_match = Regex.Match(param_string, "series_id=([0-9]+)");
            if (!series_match.Success)
            {
                Utility.Log.LogFormat("Unrecognized gacha series details request path {0}.  Expected ?series_id=<n>", RequestPath);
                return;
            }

            uint series_id;
            if (!uint.TryParse(series_match.Groups[1].Value, out series_id))
            {
                Utility.Log.LogFormat("Unrecognized gacha series details request path {0}.  series_id does not appear to be an integer.", RequestPath);
                return;
            }

            DataGachaSeriesItemsForEntryPoints gacha = new DataGachaSeriesItemsForEntryPoints();
            foreach (var child in parsed_object)
            {
                try
                {
                    uint entry_point_id = uint.Parse(child.Key);
                    if (child.Value.Type != JTokenType.Object)
                        continue;

                    string serialized = JsonConvert.SerializeObject(child.Value);

                    var entry = new DataGachaSeriesItemsForEntryPoints.ItemsForEntryPoint();
                    entry.ItemDetails = JsonConvert.DeserializeObject<DataGachaSeriesItemDetails>(serialized);
                    // Add the entry immediately.  We may not end up finding DataGachaSeriesEntryPoint for this
                    // entry point (for example if the user was already viewing the gacha banners when he loaded
                    // FFRK Inspector.  But we still save the entry point ID in the UI, and it's mostly just for
                    // show, so the UI can fallback in that case.
                    gacha.Gachas.Add(entry_point_id, entry);

                    // Find the entry point details for this entry point.
                    List<DataGachaSeriesInfo> SeriesList = FFRKProxy.Instance.GameState.GachaSeries.SeriesList;
                    if (SeriesList == null)
                        continue;

                    DataGachaSeriesInfo series = SeriesList.Find(x => x.SeriesId == series_id);
                    if (series == null)
                        continue;

                    entry.EntryPoint = FindEntryPointForSeries(series, entry_point_id);
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
