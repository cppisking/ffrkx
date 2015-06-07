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
    class HandleListBattles : SimpleResponseHandler
    {
        public override bool CanHandle(Session Session)
        {
            string RequestPath = Session.oRequest.headers.RequestPath;
            return RequestPath.Equals("/dff/world/battles");
        }

        public override void Handle(Session Session)
        {
            string ResponseJson = Session.GetResponseBodyAsString();

            EventListBattles result = JsonConvert.DeserializeObject<EventListBattles>(ResponseJson);
            FFRKProxy.Instance.GameState.ActiveDungeon = result;

            lock (FFRKProxy.Instance.Cache.SyncRoot)
            {
                result.Battles.Sort((x,y) => x.Id.CompareTo(y.Id));
                ushort stam_to_reach = 0;
                for (int i=0; i < result.Battles.Count; ++i)
                {
                    DataBattle battle = result.Battles[i];
                    DataCache.Battles.Key key = new DataCache.Battles.Key { BattleId = battle.Id };
                    DataCache.Battles.Data data = null;
                    if (!FFRKProxy.Instance.Cache.Battles.TryGetValue(key, out data))
                    {
                        data = new DataCache.Battles.Data
                        {
                            DungeonId = battle.DungeonId,
                            HistoSamples = 1,
                            Name = battle.Name,
                            Repeatable = (i < result.Battles.Count-1),
                            Samples = 1,
                            Stamina = battle.Stamina,
                            StaminaToReach = stam_to_reach
                        };

                        FFRKProxy.Instance.Cache.Battles.Update(key, data);
                    }

                    stam_to_reach += battle.Stamina;
                }
            }
            FFRKProxy.Instance.Database.BeginExecuteRequest(new DbOpRecordBattleList(result));
            FFRKProxy.Instance.RaiseListBattles(result);
        }
    }
}
