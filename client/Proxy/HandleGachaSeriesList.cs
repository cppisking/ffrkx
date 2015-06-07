using FFRKInspector.GameData;
using Fiddler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Proxy
{
    class HandleGachaSeriesList : SimpleResponseHandler
    {
        public override bool CanHandle(Session Session)
        {
            string RequestPath = Session.oRequest.headers.RequestPath;
            return RequestPath.StartsWith("/dff/gacha/show");
        }

        public override void Handle(Session Session)
        {
            string ResponseJson = Session.GetResponseBodyAsString();
            DataGachaSeriesList result = JsonConvert.DeserializeObject<DataGachaSeriesList>(ResponseJson);
            FFRKProxy.Instance.GameState.GachaSeries = result;
        }
    }
}
