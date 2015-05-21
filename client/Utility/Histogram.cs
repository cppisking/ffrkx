using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Utility
{
    class Histogram
    {
        List<uint> mHistogram;
        int mMinBuckets;

        public Histogram(int MinBuckets)
        {
            mMinBuckets = MinBuckets;
            mHistogram = Enumerable.Repeat<uint>(0, (int)mMinBuckets).ToList();
        }

        // The histogram will never contain fewer than this many buckets, even if we
        // try to trim them off.
        public int MinBucketCount
        {
            get
            {
                return mMinBuckets;
            }
            set
            {
                if (mMinBuckets == value)
                    return;
                mMinBuckets = value;
                if (mHistogram.Count < mMinBuckets)
                    AddBuckets(mMinBuckets - mHistogram.Count);
            }
        }

        public int BucketCount
        {
            get { return mHistogram.Count; }
            set 
            {
                if (mHistogram.Count == value)
                    return;
                if (mHistogram.Count > value)
                {
                    RemoveBucketsStartingAt(value);
                    return;
                }

                AddBuckets(value - mHistogram.Count);
            }
        }

        public uint this[int bucket]
        {
            get { return mHistogram[bucket]; }
            set
            {
                EnsureBucket(bucket);
                mHistogram[bucket] = value;
            }
        }

        public void TrimBack()
        {
            int last_non_zero = mHistogram.FindLastIndex(x => x != 0);
            if (last_non_zero > 0)
                RemoveBucketsStartingAt(last_non_zero);
        }

        private void EnsureBucket(int bucket)
        {
            if (mHistogram.Count <= bucket)
                AddBuckets(bucket - mHistogram.Count + 1);
        }

        private void AddBuckets(int count)
        {
            mHistogram.AddRange(Enumerable.Repeat<uint>(0, count));
        }

        private void RemoveBucketsStartingAt(int index)
        {
            mHistogram.RemoveRange(index, mHistogram.Count - index);
        }
    }
}
