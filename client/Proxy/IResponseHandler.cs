using Fiddler;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Proxy
{
    interface IResponseHandler
    {
        bool CanHandle(Session Session);
        string GetResponseBody(Session Session);
        JObject CreateJsonObject(Session session);
        void Handle(Session Session);
    }
}
