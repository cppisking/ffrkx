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
    class HandleListBattles : IResponseHandler
    {
        public bool CanHandle(string RequestPath)
        {
            return RequestPath.Equals("/dff/world/battles");
        }

        public void Handle(string RequestPath, string ResponseJson)
        {
            EventListBattles result = JsonConvert.DeserializeObject<EventListBattles>(ResponseJson);
            FFRKProxy.Instance.GameState.ActiveDungeon = result;

            FFRKProxy.Instance.Database.BeginExecuteRequest(new DbOpRecordBattleList(result));
            FFRKProxy.Instance.RaiseListBattles(result);
        }
    }
}
