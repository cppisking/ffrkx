using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    public static class DataRecordExtensions
    {
        public static bool ColumnExists(this IDataRecord Record, string Column)
        {
            for (int i=0; i < Record.FieldCount; ++i)
            {
                if (Record.GetName(i).Equals(Column, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public static T? GetValueOrNull<T>(this IDataRecord Record, string Column) where T : struct
        {
            int ordinal = Record.GetOrdinal(Column);
            if (ordinal == -1)
                return null;

            if (Record.IsDBNull(ordinal))
                return null;
            return (T)Record[ordinal];
        }
    }
}
