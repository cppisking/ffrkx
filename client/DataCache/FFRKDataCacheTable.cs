using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.DataCache
{
    class FFRKDataCacheTable<Key,Value> : IEnumerable<KeyValuePair<Key,Value>>
    {
        private Dictionary<Key, Value> mCache = null;

        public FFRKDataCacheTable()
        {
            mCache = new Dictionary<Key,Value>();
        }

        public bool Contains(Key k)
        {
            return mCache.ContainsKey(k);
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

        public IEnumerable<Key> Keys
        {
            get { return mCache.Keys; }
        }

        public IEnumerable<Value> Values
        {
            get { return mCache.Values; }
        }

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            return mCache.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return mCache.GetEnumerator();
        }
    }
}
