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
    public abstract class SimpleResponseHandler : IResponseHandler
    {
        public abstract bool CanHandle(Session Session);
        public abstract void Handle(Session Session);

        public virtual string GetResponseBody(Session session)
        {
            return session.GetResponseBodyAsString();
        }

        public JObject CreateJsonObject(Session Session)
        {
            return JsonConvert.DeserializeObject<JObject>(GetResponseBody(Session));
        }

    }
}
