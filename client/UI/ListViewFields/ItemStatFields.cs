using FFRKInspector.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.UI.ListViewFields
{
    class ItemNameField : TrivialField<BasicItemDropStats, string>
    {
        public ItemNameField(string DisplayName, FieldWidthStyle WidthStyle, int Width)
            : base(DisplayName, x => x.ItemName, WidthStyle, Width)
        {
        }
    }

    class ItemDungeonField : TrivialField<BasicItemDropStats, string>
    {
        public ItemDungeonField(string DisplayName, FieldWidthStyle WidthStyle, int Width)
            : base(DisplayName, x => x.EffectiveDungeonName, WidthStyle, Width)
        {
        }
    }

    class ItemBattleField : TrivialField<BasicItemDropStats, string>
    {
        public ItemBattleField(string DisplayName, FieldWidthStyle WidthStyle, int Width)
            : base(DisplayName, x => x.BattleName, WidthStyle, Width)
        {
        }
    }

    class ItemWorldField : TrivialField<BasicItemDropStats, string>
    {
        public ItemWorldField(string DisplayName, FieldWidthStyle WidthStyle, int Width)
            : base(DisplayName, x => x.WorldName, WidthStyle, Width)
        {
        }
    }

    class ItemTotalDropsField : TrivialField<BasicItemDropStats, uint>
    {
        public ItemTotalDropsField(string DisplayName) : base(DisplayName, x => x.TotalDrops) { }
    }

    class ItemTimesRunField : TrivialField<BasicItemDropStats, uint>
    {
        public ItemTimesRunField(string DisplayName) : base(DisplayName, x => x.TimesRun) { }
    }

    class ItemBattleStaminaField : TrivialField<BasicItemDropStats, uint>
    {
        public ItemBattleStaminaField(string DisplayName) : base(DisplayName, x => x.BattleStamina) { }
    }

    class ItemStaminaToReachField : TrivialField<BasicItemDropStats, uint>
    {
        public ItemStaminaToReachField(string DisplayName) : base(DisplayName, x => x.StaminaToReachBattle) { }
    }

    class ItemRepeatableField : TrivialField<BasicItemDropStats, bool>
    {
        public ItemRepeatableField(string DisplayName) : base(DisplayName, x => x.IsBattleRepeatable) { }
    }

    class ItemRarityField : ListViewField<BasicItemDropStats>
    {
        public ItemRarityField(string DisplayName) : base(DisplayName, FieldWidthStyle.Absolute, 65) { }

        protected override int CompareValues(BasicItemDropStats x, BasicItemDropStats y)
        {
            return x.Rarity.CompareTo(y.Rarity);
        }

        protected override string FormatValue(BasicItemDropStats value)
        {
            return new String('★', (int)value.Rarity);
        }
    }

    class ItemSynergyField : ListViewField<BasicItemDropStats>
    {
        public ItemSynergyField(string DisplayName) : base(DisplayName) { }

        protected override int CompareValues(BasicItemDropStats x, BasicItemDropStats y)
        {
            if (x.Synergy == y.Synergy) return 0;
            if (x.Synergy == null) return -1;
            if (y.Synergy == null) return 1;
            return x.Synergy.Realm.CompareTo(y.Synergy.Realm);
        }

        protected override string FormatValue(BasicItemDropStats value)
        {
            if (value.Synergy == null) return string.Empty;
            return value.Synergy.Text;
        }
    }

    class ItemDropsPerRunField : ListViewField<BasicItemDropStats>
    {
        public ItemDropsPerRunField(string DisplayName) : base(DisplayName) { }

        protected override int CompareValues(BasicItemDropStats x, BasicItemDropStats y)
        {
            return x.DropsPerRun.CompareTo(y.DropsPerRun);
        }

        protected override string FormatValue(BasicItemDropStats value)
        {
            return value.DropsPerRun.ToString("F");
        }
    }

    class ItemStaminaPerDropField : ListViewField<BasicItemDropStats>
    {
        public ItemStaminaPerDropField(string DisplayName, bool useStaminaToReachForNonRep) : base(DisplayName)
        {
            this.mUseStaminaToReachForNonRepetable = useStaminaToReachForNonRep;
        }

        private bool mUseStaminaToReachForNonRepetable;

        protected override int CompareValues(BasicItemDropStats x, BasicItemDropStats y)
        {
            return GetValue(x).CompareTo(GetValue(y));
        }

        protected override string FormatValue(BasicItemDropStats value)
        {
            return GetValue(value).ToString("F");
        }

        public float GetValue(BasicItemDropStats stats)
        {
            if (!mUseStaminaToReachForNonRepetable)
                return stats.StaminaPerDrop;
            return (float)stats.StaminaToReachBattle + stats.StaminaPerDrop;
        }

        public bool UseStaminaToReachForNonRepeatable
        {
            get
            {
                return mUseStaminaToReachForNonRepetable;
            }
            set
            {
                mUseStaminaToReachForNonRepetable = value;
            }
        }
    }
}
