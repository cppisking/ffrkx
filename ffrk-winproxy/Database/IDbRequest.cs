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
        void Execute(MySqlConnection connection);
        void Respond();
    }

    interface ITransactedDbRequest
    {
        void Execute(MySqlConnection connection, MySqlTransaction transaction);
        void Respond();
    }
}
