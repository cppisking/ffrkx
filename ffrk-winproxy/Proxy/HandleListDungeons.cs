using FFRKInspector.Database;
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
    class HandleListDungeons : IResponseHandler
    {
        public bool CanHandle(string RequestPath)
        {
            return RequestPath.StartsWith("/dff/world/dungeons");
        }

        public void Handle(string RequestPath, string ResponseJson)
        {
            EventListDungeons result = JsonConvert.DeserializeObject<EventListDungeons>(ResponseJson);
            FFRKProxy.Instance.Database.BeginExecuteRequest(new DbOpRecordDungeonList(result));
            FFRKProxy.Instance.RaiseListDungeons(result);
        }
    }
}
