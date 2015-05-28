using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFRKInspector.Proxy;
using FFRKInspector.GameData;

namespace FFRKInspector.UI
{
    public partial class EquipUsagePanel : UserControl, FFRKDataBoundPanel
    {
        private class CharacterInfo
        {
            public string Name;
            public uint Id;
            public override string ToString()
            {
                return Name;
            }
        }

        private static class CharacterLookupCache
        {
            public static Dictionary<uint, string> mCharacterLookup = new Dictionary<uint,string>();
            public static Dictionary<string, uint> mCharacterReverseLookup = new Dictionary<string,uint>(StringComparer.CurrentCultureIgnoreCase);
        }

        public class CharacterNameCellTemplate : DataGridViewTextBoxCell, IAutoCompleteSource
        {
            public CharacterNameCellTemplate()
            {
            }

            public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.ComponentModel.TypeConverter valueTypeConverter)
            {
                if (formattedValue == null || formattedValue == DBNull.Value)
                    return DBNull.Value;
                string s = (string)formattedValue;
                if (s == string.Empty) return DBNull.Value;
                uint id;
                if (CharacterLookupCache.mCharacterReverseLookup.TryGetValue(s, out id))
                    return id;
                else if (uint.TryParse(s, out id))
                    return id;
                throw new FormatException();
            }

            protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
            {
                if (value == DBNull.Value || value == null)
                    return "";

                uint uvalue = (uint)value;
                string name;
                if (CharacterLookupCache.mCharacterLookup.TryGetValue(uvalue, out name))
                    return name;
                throw new FormatException();
            }

            public AutoCompleteStringCollection AutoCompleteSource
            {
                get
                {
                    AutoCompleteStringCollection complete = new AutoCompleteStringCollection();
                    complete.AddRange(CharacterLookupCache.mCharacterReverseLookup.Keys.ToArray());

                    return complete; 
                }
            }
        }

        public EquipUsagePanel()
        {
            InitializeComponent();
            dgcCharacterId.CellTemplate = new CharacterNameCellTemplate();
            dgcEquipCategory.CellTemplate = new EnumDataViewGridCell<SchemaConstants.EquipmentCategory>();
        }

        private void EquipUsagePanel_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (FFRKProxy.Instance != null)
            {
                this.equip_usageTableAdapter.Connection = FFRKProxy.Instance.Database.Connection;
                this.charactersTableAdapter1.Connection = FFRKProxy.Instance.Database.Connection;
            }
        }

        public void Reload()
        {
            this.equip_usageTableAdapter.Fill(this.equipUsageDataSet.equip_usage);
            this.charactersTableAdapter1.Fill(this.equipUsageDataSet.characters);
            foreach (var x in equipUsageDataSet.characters)
            {
                CharacterLookupCache.mCharacterLookup.Add(x.id, x.name);
                CharacterLookupCache.mCharacterReverseLookup.Add(x.name, x.id);
            }
        }

        public void Commit()
        {
            int result = this.equip_usageTableAdapter.Update(this.equipUsageDataSet.equip_usage);
            if (result > 0)
                MessageBox.Show(String.Format("{0} items successfully updated.", result));
        }
    }
}
