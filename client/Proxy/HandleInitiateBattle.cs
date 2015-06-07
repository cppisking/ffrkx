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
    class HandleInitiateBattle : SimpleResponseHandler
    {
        public override bool CanHandle(Session Session)
        {
            string RequestPath = Session.oRequest.headers.RequestPath;
            return RequestPath.EndsWith("get_battle_init_data");
        }

        public override void Handle(Session Session)
        {
            string ResponseJson = Session.GetResponseBodyAsString();

            EventBattleInitiated result = JsonConvert.DeserializeObject<EventBattleInitiated>(ResponseJson);
            FFRKProxy.Instance.GameState.ActiveBattle = result;
            FFRKProxy.Instance.RaiseBattleInitiated(result);
        }
    }
}
