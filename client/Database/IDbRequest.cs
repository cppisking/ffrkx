using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    interface IDbRequest
    {
        bool RequiresTransaction { get; }
        void Execute(MySqlConnection connection, MySqlTransaction transaction);
        void Respond();
    }
}
