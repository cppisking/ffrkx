using FFRKInspector.Database;
using FFRKInspector.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Proxy
{
    class HandleCompleteBattle : IResponseHandler
    {
        public bool CanHandle(string RequestPath)
        {
            return RequestPath.Equals("/dff/battle/win")        // Win a world battle
                || RequestPath.EndsWith("/win_battle")          // Win an event battle.
                || RequestPath.Equals("/dff/battle/lose")       // Lose from a world map battle
                || RequestPath.Equals("/dff/battle/escape")     // Escape from a world map battle
                || RequestPath.StartsWith("/dff/world/fail")    // Fail a survival event
                || RequestPath.Equals("/dff/battle/quit");      // User pushed Cancel after restarting from S/L

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

                lock (FFRKProxy.Instance.Cache.SyncRoot)
                {
                    DataCache.Battles.Key key = new DataCache.Battles.Key { BattleId = original_battle.Battle.BattleId };
                    DataCache.Battles.Data data = null;
                    if (FFRKProxy.Instance.Cache.Battles.TryGetValue(key, out data))
                    {
                        data.Samples++;
                        data.StdevSamples++;
                    }
                }

                DbOpRecordBattleEncounter op = new DbOpRecordBattleEncounter(original_battle);
                FFRKProxy.Instance.Database.BeginExecuteRequest(op);
                FFRKProxy.Instance.RaiseBattleComplete(original_battle);
            }
        }
    }
}
