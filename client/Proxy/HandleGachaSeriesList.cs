using FFRKInspector.GameData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Proxy
{
    class HandleGachaSeriesList : IResponseHandler
    {
        public bool CanHandle(string RequestPath)
        {
            return RequestPath.StartsWith("/dff/gacha/show");
        }

        public void Handle(string RequestPath, string ResponseJson)
        {
            DataGachaSeriesList result = JsonConvert.DeserializeObject<DataGachaSeriesList>(ResponseJson);
            FFRKProxy.Instance.GameState.GachaSeries = result;
        }
    }
}
