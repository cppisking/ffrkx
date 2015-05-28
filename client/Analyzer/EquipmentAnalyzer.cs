using FFRKInspector.GameData;
using FFRKInspector.GameData.Party;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Analyzer
{
    class EquipmentAnalyzer
    {
        public class Result
        {
            public double Score;
            public bool IsValid;
        }

        private class AnalysisItem
        {
            public DataEquipmentInformation Item;
            public List<DataBuddyInformation> WhoCanUse;
            public EquipStats SynergizedStats;
            public EquipStats NonSynergizedStats;
            public Result Result;
        }

        private AnalyzerSettings mSettings;
        private List<AnalysisItem> mItems;

        public EquipmentAnalyzer(AnalyzerSettings Settings)
        {
            mSettings = Settings;
        }

        public EquipmentAnalyzer()
        {
            mSettings = AnalyzerSettings.DefaultSettings;
        }
    }
}
