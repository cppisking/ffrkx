using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.DataCache
{
    class FFRKDataCache
    {
        public delegate void CacheRefreshedDelegate();
        private FFRKDataCacheTable<Dungeons.Key, Dungeons.Data> mDungeons;
        private FFRKDataCacheTable<Worlds.Key, Worlds.Data> mWorlds;
        private FFRKDataCacheTable<Battles.Key, Battles.Data> mBattles;
        private FFRKDataCacheTable<Items.Key, Items.Data> mItems;

        public FFRKDataCache()
        {
            mDungeons = new FFRKDataCacheTable<Dungeons.Key, Dungeons.Data>();
            mWorlds = new FFRKDataCacheTable<Worlds.Key, Worlds.Data>();
            mBattles = new FFRKDataCacheTable<Battles.Key, Battles.Data>();
            mItems = new FFRKDataCacheTable<Items.Key, Items.Data>();
        }

        public FFRKDataCacheTable<Dungeons.Key, Dungeons.Data> Dungeons { get { return mDungeons; } }
        public FFRKDataCacheTable<Worlds.Key, Worlds.Data> Worlds { get { return mWorlds; } }
        public FFRKDataCacheTable<Battles.Key, Battles.Data> Battles { get { return mBattles; } }
        public FFRKDataCacheTable<Items.Key, Items.Data> Items { get { return mItems; } }
    }
}
