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
    class HandleListDungeons : SimpleResponseHandler
    {
        public override bool CanHandle(Session Session)
        {
            string RequestPath = Session.oRequest.headers.RequestPath;
            return RequestPath.StartsWith("/dff/world/dungeons");
        }

        public override void Handle(Session Session)
        {
            string ResponseJson = Session.GetResponseBodyAsString();
            EventListDungeons result = JsonConvert.DeserializeObject<EventListDungeons>(ResponseJson);
            
            lock (FFRKProxy.Instance.Cache.SyncRoot)
            {
                foreach (DataDungeon dungeon in result.Dungeons)
                {
                    DataCache.Dungeons.Key key = new DataCache.Dungeons.Key { DungeonId = dungeon.Id };
                    DataCache.Dungeons.Data data = null;
                    if (!FFRKProxy.Instance.Cache.Dungeons.TryGetValue(key, out data))
                    {
                        data = new DataCache.Dungeons.Data
                        {
                            Difficulty = dungeon.Difficulty,
                            Name = dungeon.Name,
                            Series = dungeon.SeriesId,
                            Type = dungeon.Type,
                            WorldId = dungeon.WorldId
                        };
                        FFRKProxy.Instance.Cache.Dungeons.Update(key, data);
                    }
                }
            }

            FFRKProxy.Instance.Database.BeginExecuteRequest(new DbOpRecordDungeonList(result));
            FFRKProxy.Instance.RaiseListDungeons(result);
        }
    }
}
