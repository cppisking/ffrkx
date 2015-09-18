using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    static class RealmSynergy
    {
        public enum Value
        {
            None = -1,
            Core = 0,
            FF1 = 1,
            FF2 = 2,
            FF3 = 3,
            FF4 = 4,
            FF5 = 5,
            FF6 = 6,
            FF7 = 7,
            FF8 = 8,
            FF9 = 9,
            FF10 = 10,
            FF11 = 11,
            FF12 = 12,
            FF13 = 13,
            FF14 = 14,
        }

        public class SynergyValue
        {
            private string mText;
            private uint mGameSeries;
            private Value mRealmValue;

            public SynergyValue(string Text, uint Series, Value Realm)
            {
                mText = Text;
                mGameSeries = Series;
                mRealmValue = Realm;
            }

            public string Text { get { return mText; } }
            public uint GameSeries { get { return mGameSeries; } }
            public Value Realm { get { return mRealmValue; } }

            public override string ToString()
            {
                return mText;
            }
        }

        private static Dictionary<string, SynergyValue> mTextLookup;
        private static Dictionary<uint, SynergyValue> mSeriesLookup;
        private static Dictionary<Value, SynergyValue> mRealmLookup;

        static RealmSynergy()
        {
            mTextLookup = new Dictionary<string, SynergyValue>(StringComparer.CurrentCultureIgnoreCase);
            mSeriesLookup = new Dictionary<uint, SynergyValue>();
            mRealmLookup = new Dictionary<Value, SynergyValue>();
            // These values used here for the `series` field are sent to us by the server.
            // The server sends another magic value 300001 for the daily dungeon world,
            // and a third value 1 which means "no synergy", but these don't seem to be
            // used for anything import to us, so I don't include them here.
            SynergyValue[] values = { 
                new SynergyValue("None", 1, Value.None),
                new SynergyValue("Core", 200001, Value.Core),
                new SynergyValue("I", 101001, Value.FF1),
                new SynergyValue("II", 102001, Value.FF2),
                new SynergyValue("III", 103001, Value.FF3),
                new SynergyValue("IV", 104001, Value.FF4),
                new SynergyValue("V", 105001, Value.FF5),
                new SynergyValue("VI", 106001, Value.FF6),
                new SynergyValue("VII", 107001, Value.FF7),
                new SynergyValue("VIII", 108001, Value.FF8),
                new SynergyValue("IX", 109001, Value.FF9),
                new SynergyValue("X", 110001, Value.FF10),
                new SynergyValue("XI", 111001, Value.FF11),
                new SynergyValue("XII", 112001, Value.FF12),
                new SynergyValue("XIII", 113001, Value.FF13),
                new SynergyValue("XIV", 114001, Value.FF14),
            };

            foreach (SynergyValue value in values)
            {
                mTextLookup.Add(value.Text, value);
                mSeriesLookup.Add(value.GameSeries, value);
                mRealmLookup.Add(value.Realm, value);
            }
        }

        public static SynergyValue FromName(string Name)
        {
            SynergyValue result;
            if (mTextLookup.TryGetValue(Name, out result))
                return result;
            Value result2;
            if (Enum.TryParse<Value>(Name, true, out result2))
                return FromRealm(result2);
            throw new KeyNotFoundException();
        }

        public static SynergyValue FromSeries(uint Series)
        {
            return mSeriesLookup[Series];
        }

        public static SynergyValue FromRealm(Value Realm)
        {
            return mRealmLookup[Realm];
        }

        public static IEnumerable<SynergyValue> Values
        {
            get { return mRealmLookup.Values; }
        }
    }
}
