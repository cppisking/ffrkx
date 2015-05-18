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
    }
}
