using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    public interface ISelectParam
    {
        string WhereClause { get; }
        bool HasValue { get; }
        void Bind(MySqlCommand Command);
    }
}
