using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffrk_winproxy.DataCache
{
    class FFRKDataCache
    {
        public delegate void CacheRefreshedDelegate();
        private FFRKDataCacheTable<DungeonDrops.Key, DungeonDrops.Data> mDrops;
        private FFRKDataCacheTable<Dungeons.Key, Dungeons.Data> mDungeons;
        private FFRKDataCacheTable<Worlds.Key, Worlds.Data> mWorlds;
        private FFRKDataCacheTable<Battles.Key, Battles.Data> mBattles;
        private FFRKDataCacheTable<Items.Key, Items.Data> mItems;

        public FFRKDataCache()
        {
            mDrops = new FFRKDataCacheTable<DungeonDrops.Key, DungeonDrops.Data>();
            mDungeons = new FFRKDataCacheTable<Dungeons.Key, Dungeons.Data>();
            mWorlds = new FFRKDataCacheTable<Worlds.Key, Worlds.Data>();
            mBattles = new FFRKDataCacheTable<Battles.Key, Battles.Data>();
            mItems = new FFRKDataCacheTable<Items.Key, Items.Data>();
        }

        public FFRKDataCacheTable<DungeonDrops.Key, DungeonDrops.Data> Drops { get { return mDrops; } }
        public FFRKDataCacheTable<Dungeons.Key, Dungeons.Data> Dungeons { get { return mDungeons; } }
        public FFRKDataCacheTable<Worlds.Key, Worlds.Data> Worlds { get { return mWorlds; } }
        public FFRKDataCacheTable<Battles.Key, Battles.Data> Battles { get { return mBattles; } }
        public FFRKDataCacheTable<Items.Key, Items.Data> Items { get { return mItems; } }
    }
}
