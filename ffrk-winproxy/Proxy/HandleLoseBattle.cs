using FFRKInspector.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Proxy
{
    class HandleLoseBattle : IResponseHandler
    {
        public bool CanHandle(string RequestPath)
        {
            return RequestPath.Equals("/dff/battle/lose")       // Lose from a world map battle
                || RequestPath.Equals("/dff/battle/escape")     // Escape from a world map battle
                || RequestPath.StartsWith("/dff/world/fail");   // Fail a survival event
        }

        public void Handle(string RequestPath, string ResponseJson)
        {
            GameState state = FFRKProxy.Instance.GameState;
            // Win or lose, finishing a battle means it's safe to record that encounter and its drops
            // since it won't be possible for the user to have the same drop set if they continue.
            if (state.ActiveBattle != null)
            {
                EventBattleInitiated original_battle = state.ActiveBattle;
                state.ActiveBattle = null;

                FFRKProxy.Instance.Database.BeginRecordBattleEncounter(original_battle);
                FFRKProxy.Instance.RaiseBattleLost(original_battle);
            }
        }
    }
}
