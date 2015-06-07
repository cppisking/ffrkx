using FFRKInspector.GameData.AppInit;
using Fiddler;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Proxy
{
    class HandleAppInitData : SimpleResponseHandler
    {
        public override bool CanHandle(Session Session)
        {
            string RequestPath = Session.oRequest.headers.RequestPath;
            return RequestPath.Equals("/dff") || RequestPath.Equals("/dff/");
        }

        public override void Handle(Session Session)
        {
            AppInitData result = JsonConvert.DeserializeObject<AppInitData>(GetResponseBody(Session));
            FFRKProxy.Instance.GameState.AppInitData = result;
        }

        public override string GetResponseBody(Session Session)
        {
            string ResponseBody = Session.GetResponseBodyAsString();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(ResponseBody);
            HtmlNode node = doc.DocumentNode.SelectSingleNode(".//script[@data-app-init-data]");
            if (node == null)
                return null;
            return node.InnerHtml;
        }
    }
}
