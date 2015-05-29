using FFRKInspector.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Analyzer
{
    class AnalyzerSettings
    {
        public enum ItemLevelConsideration
        {
            Current,
            CurrentRankMaxLevel,
            FullyMaxed
        }

        public enum OffensiveStat
        {
            ATK,
            MAG,
            MND
        }

        public enum DefensiveStat
        {
            DEF,
            RES
        }

        public class PartyMemberSettings
        {
            public bool Score;
            public OffensiveStat OffensiveStat;
            public DefensiveStat DefensiveStat;

            public PartyMemberSettings Clone()
            {
                return (PartyMemberSettings)MemberwiseClone();
            }
        }

        private ItemLevelConsideration mLevelConsideration;
        private Dictionary<uint, PartyMemberSettings> mConfiguration;
        private static AnalyzerSettings mDefaultSettings;
        private static PartyMemberSettings mDefaultMemberSettings;

        private AnalyzerSettings Clone()
        {
            AnalyzerSettings result = (AnalyzerSettings)MemberwiseClone();
            result.mConfiguration = new Dictionary<uint, PartyMemberSettings>(result.mConfiguration);
            return result;
        }

        public static AnalyzerSettings DefaultSettings
        {
            get { return mDefaultSettings.Clone(); }
        }

        static AnalyzerSettings()
        {
            mDefaultSettings = new AnalyzerSettings();
            mDefaultSettings.mLevelConsideration = ItemLevelConsideration.Current;
            mDefaultMemberSettings = new PartyMemberSettings
            {
                Score = true,
                DefensiveStat = DefensiveStat.DEF,
                OffensiveStat = OffensiveStat.ATK
            
            };
            AddDefault("Tyro",          false, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Warrior",       false, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Knight",        false, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Red Mage",      false, OffensiveStat.MAG, DefensiveStat.RES);
            AddDefault("Black Mage",    false, OffensiveStat.MAG, DefensiveStat.RES);
            AddDefault("White Mage",    false, OffensiveStat.MND, DefensiveStat.RES);
            AddDefault("Summoner",      false, OffensiveStat.MAG, DefensiveStat.RES);
            AddDefault("Ranger",        false, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Bard",          false, OffensiveStat.MND, DefensiveStat.RES);
            AddDefault("Warrior of Light", false, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Gordon",        true, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Josef",         true, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Luneth",        true, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Cecil (DRK)",   true, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Cecil (PLD)",   true, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Kain",          true, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Rydia",         true, OffensiveStat.MAG, DefensiveStat.RES);
            AddDefault("Lenna",         true, OffensiveStat.MND, DefensiveStat.RES);
            AddDefault("Terra",         true, OffensiveStat.MAG, DefensiveStat.RES);
            AddDefault("Celes",         true, OffensiveStat.MAG, DefensiveStat.RES);
            AddDefault("Cyan",          true, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Cloud",         true, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Tifa",          true, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Aerith",        true, OffensiveStat.MND, DefensiveStat.RES);
            AddDefault("Sephiroth",     true, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Rinoa",         true, OffensiveStat.MAG, DefensiveStat.RES);
            AddDefault("Tidus",         true, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Wakka",         true, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Snow",          true, OffensiveStat.ATK, DefensiveStat.DEF);
            AddDefault("Vanille",       true, OffensiveStat.MND, DefensiveStat.RES);
        }

        private static void AddDefault(string Name, bool Score, OffensiveStat Off, DefensiveStat Def)
        {
            DataCache.Characters.Key key = FFRKProxy.Instance.Cache.Characters.First(
                x => x.Value.Name.ToString().Equals(Name, StringComparison.CurrentCultureIgnoreCase)).Key;
            mDefaultSettings.mConfiguration.Add(
                key.Id,
                new PartyMemberSettings
                {
                    Score = Score,
                    OffensiveStat = Off,
                    DefensiveStat = Def
                });
        }

        public AnalyzerSettings()
        {
            mConfiguration = new Dictionary<uint, PartyMemberSettings>();
        }

        public PartyMemberSettings this[uint Id]
        {
            get
            {
                // If we have a local configuration, try to get the name from there first.
                PartyMemberSettings result = null;
                if (mConfiguration.TryGetValue(Id, out result))
                    return result;

                // If we couldn't find it and this is already the default configuration, don't ask
                // ourselves to do the lookup again which would create an infinite recursion.  Just
                // return something sane.
                if (this == mDefaultSettings)
                    return mDefaultMemberSettings.Clone();

                // Otherwise ask the default configuration.
                return mDefaultSettings[Id];
            }
        }

        public ItemLevelConsideration LevelConsideration
        {
            get { return mLevelConsideration; }
            set { mLevelConsideration = value; }
        }
    }
}
