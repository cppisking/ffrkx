using FFRKInspector.GameData;
using FFRKInspector.GameData.Party;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.UI.ListViewFields
{
    class CharacterNameField : TrivialField<DataBuddyInformation, string>
    {
        public CharacterNameField(string DisplayName)
            : base(DisplayName, x => x.Name, FieldWidthStyle.Absolute, 100)
        {
        }
    }
    class CharacterLevelField : TrivialField<DataBuddyInformation, byte>
    {
        public CharacterLevelField(string DisplayName)
            : base(DisplayName, x => x.Level, FieldWidthStyle.AutoSize, 0)
        {
        }
    }
    class CharacterLevelMaxField : TrivialField<DataBuddyInformation, byte>
    {
        public CharacterLevelMaxField(string DisplayName)
            : base(DisplayName, x => x.LevelMax, FieldWidthStyle.AutoSize, 0)
        {
        }
    }
}
