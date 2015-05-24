using FFRKInspector.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
    public class EnumDataViewGridCell<T> : DataGridViewTextBoxCell where T : struct
    {
        public EnumDataViewGridCell()
        {

        }

        public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.ComponentModel.TypeConverter valueTypeConverter)
        {
            if (formattedValue == null || formattedValue == DBNull.Value)
                return DBNull.Value;
            string s = (string)formattedValue;
            if (s == string.Empty) return DBNull.Value;
            return Enum.Parse(typeof(T), s);
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (value == DBNull.Value || value == null)
                return "";

            string value_str = value.ToString();
            T result;
            if (Enum.TryParse<T>(value_str, out result))
                return result.ToString();
            return value_str;
        }
    }
}
