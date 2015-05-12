using FFRKInspector.GameData;
using Fiddler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Proxy
{
    class HandleGacha : IResponseHandler
    {
        public bool CanHandle(string RequestPath)
        {
            return RequestPath.StartsWith("/dff/gacha/probability");
        }

        public void Handle(string RequestPath, string ResponseJson)
        {
            JObject parsed_object = JsonConvert.DeserializeObject<JObject>(ResponseJson);
            EventViewGacha gacha = new EventViewGacha();
            foreach (var child in parsed_object)
            {
                try
                {
                    uint series_id = uint.Parse(child.Key);
                    if (child.Value.Type == JTokenType.Object)
                    {
                        string serialized = JsonConvert.SerializeObject(child.Value);
                        DataGachaSeries this_series = JsonConvert.DeserializeObject<DataGachaSeries>(serialized);
                        gacha.Gachas.Add(series_id, this_series);
                    }
                }
                catch
                {
                }
            }
            FFRKProxy.Instance.RaiseGachaStats(gacha);
        }
    }
}
