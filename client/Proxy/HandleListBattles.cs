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

            lock (FFRKProxy.Instance.Cache.SyncRoot)
            {
                foreach (DataBattle battle in result.Battles)
                {
                    DataCache.Battles.Key key = new DataCache.Battles.Key { BattleId = battle.Id };
                    DataCache.Battles.Data data = null;
                    if (!FFRKProxy.Instance.Cache.Battles.TryGetValue(key, out data))
                    {
                        data = new DataCache.Battles.Data
                        {
                            DungeonId = battle.DungeonId,
                            HistoSamples = data.HistoSamples,
                            Name = data.Name,
                            Repeatable = data.Repeatable,
                            Samples = data.Samples,
                            Stamina = data.Stamina,
                            StaminaToReach = data.StaminaToReach
                        };
                        FFRKProxy.Instance.Cache.Battles.Update(key, data);
                    }
                }
            }
            FFRKProxy.Instance.Database.BeginExecuteRequest(new DbOpRecordBattleList(result));
            FFRKProxy.Instance.RaiseListBattles(result);
        }
    }
}
