using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.Database
{
    class DbOpVerifySchema : IDbRequest
    {
        private class VersionPoint
        {
            public VersionPoint(uint Version, bool Breaking)
            {
                mVersion = Version;
                mBreaking = Breaking;
            }
            private uint mVersion;
            private bool mBreaking;

            public uint Version { get { return mVersion; } }
            public bool Breaking { get { return mBreaking; } }
        }

        public enum VerificationResult
        {
            OK,
            DatabaseTooOld,
            DatabaseTooNew
        }

        public DbOpVerifySchema(uint ClientSchemaVersion)
        {
            mClientSchemaVersion = ClientSchemaVersion;
        }

        public delegate void VerifySchemaResultDelegate(VerificationResult result);
        public event VerifySchemaResultDelegate OnVerificationCompleted;
        private VerificationResult mResult;
        private uint mClientSchemaVersion;

        public bool RequiresTransaction
        {
            get { return false; }
        }

        public void Execute(MySqlConnection connection, MySqlTransaction transaction)
        {
            List<VersionPoint> versions = new List<VersionPoint>();

            SelectBuilder builder = new SelectBuilder();
            builder.Table = "schema_version";
            using (MySqlCommand command = new MySqlCommand(builder.ToString(), connection, transaction))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        uint version = (uint)reader["version"];
                        bool breaking = false;
                        if (reader.ColumnExists("breaking"))
                            breaking = (bool)reader["breaking"];
                        versions.Add(new VersionPoint(version, breaking));
                    }
                }
            }
            versions.Sort((x, y) => x.Version.CompareTo(y.Version));
            mResult = DoVerify(versions);
        }

        public VerificationResult Result { get { return mResult; } }

        private VerificationResult DoVerify(List<VersionPoint> versions)
        {
            bool minimum_check_ok = false;
            foreach (var version in versions)
            {
                if (version.Version < mClientSchemaVersion)
                    continue;
                minimum_check_ok = true;
                if (version.Version > mClientSchemaVersion && version.Breaking)
                    return VerificationResult.DatabaseTooNew;
            }

            if (!minimum_check_ok)
                return VerificationResult.DatabaseTooOld;
            return VerificationResult.OK;
        }

        public void Respond()
        {
            if (OnVerificationCompleted != null)
                OnVerificationCompleted(mResult);
        }
    }
}
