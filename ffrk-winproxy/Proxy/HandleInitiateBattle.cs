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
    class HandleInitiateBattle : IResponseHandler
    {
        public bool CanHandle(string RequestPath)
        {
            return RequestPath.EndsWith("get_battle_init_data");
        }

        public void Handle(string RequestPath, string ResponseJson)
        {
            EventBattleInitiated result = JsonConvert.DeserializeObject<EventBattleInitiated>(ResponseJson);
            FFRKProxy.Instance.GameState.ActiveBattle = result;
            FFRKProxy.Instance.RaiseBattleInitiated(result);
        }
    }
}
