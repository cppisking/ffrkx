using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffrk_winproxy.DataCache
{
    class FFRKDataCacheTable<Key,Value>
    {
        private Dictionary<Key, Value> mCache = null;

        public FFRKDataCacheTable()
        {
            mCache = new Dictionary<Key,Value>();
        }

        public void Update(Key k, Value v)
        {
            if (!mCache.ContainsKey(k))
                mCache.Add(k, v);
        }

        public void Clear()
        {
            mCache.Clear();
        }

        public bool TryGetValue(Key key, out Value value)
        {
            return mCache.TryGetValue(key, out value);
        }
    }
}
