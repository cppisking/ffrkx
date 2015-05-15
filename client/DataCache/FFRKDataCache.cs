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
        private object mSyncRoot;

        public FFRKDataCache()
        {
            mDungeons = new FFRKDataCacheTable<Dungeons.Key, Dungeons.Data>();
            mWorlds = new FFRKDataCacheTable<Worlds.Key, Worlds.Data>();
            mBattles = new FFRKDataCacheTable<Battles.Key, Battles.Data>();
            mItems = new FFRKDataCacheTable<Items.Key, Items.Data>();

            mSyncRoot = new object();
        }

        public FFRKDataCacheTable<Dungeons.Key, Dungeons.Data> Dungeons { get { return mDungeons; } set { mDungeons = value; } }
        public FFRKDataCacheTable<Worlds.Key, Worlds.Data> Worlds { get { return mWorlds; } set { mWorlds = value; } }
        public FFRKDataCacheTable<Battles.Key, Battles.Data> Battles { get { return mBattles; } set { mBattles = value; } }
        public FFRKDataCacheTable<Items.Key, Items.Data> Items { get { return mItems; } set { mItems = value; } }

        public object SyncRoot { get { return mSyncRoot; } }
    }
}
