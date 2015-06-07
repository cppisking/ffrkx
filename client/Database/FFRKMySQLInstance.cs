using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

using Fiddler;

using FFRKInspector.GameData;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using FFRKInspector.Proxy;

namespace FFRKInspector.Database
{
    class FFRKMySqlInstance
    {
        public enum ConnectionState
        {
            Connecting,
            Connected,
            Disabled,
            Disconnected
        }

        public enum ConnectResult
        {
            Success,
            SchemaTooOld,
            SchemaTooNew,
            InvalidConnection
        }

        BackgroundWorker mDatabaseThread = null;
        CancellationTokenSource mCancellationTokenSource = null;
        BlockingCollection<IDbRequest> mDatabaseQueue = null;
        string mConnStr = null;
        MySqlConnection mConnection = null;
        ConnectionState mConnectionState;

        bool mInsertUnknownDungeons;
        bool mInsertUnknownWorlds;
        bool mInsertUnknownItems;
        bool mInsertUnknownBattles;
        bool mDatabaseDisabled;

        string mDatabaseHost;

        public string DatabaseHost
        {
            get { return mDatabaseHost; }
        }

        public string ConnectionString
        {
            get { return mConnStr; }
        }

        public bool InsertUnknownDungeons
        { 
            get { return mInsertUnknownDungeons; }
            set { mInsertUnknownDungeons = value; }
        }

        public bool InsertUnknownWorlds
        {
            get { return mInsertUnknownWorlds; }
            set { mInsertUnknownWorlds = value; }
        }

        public bool InsertUnknownItems
        {
            get { return mInsertUnknownItems; }
            set { mInsertUnknownItems = value; }
        }

        public bool InsertUnknownBattles
        {
            get { return mInsertUnknownBattles; }
            set { mInsertUnknownBattles = value; }
        }

        public bool IsDatabaseDisabled
        {
            get { return mDatabaseDisabled; }
        }

        public MySqlConnection Connection
        {
            get { return mConnection; }
        }

        public delegate void ConnectionStateChangedDelegate(ConnectionState NewState);
        public delegate void ConnectionInitializedDelegate(ConnectResult ConnectResult);

        public event ConnectionStateChangedDelegate OnConnectionStateChanged;
        public event ConnectionInitializedDelegate OnConnectionInitialized;
        public event ConnectionInitializedDelegate OnSchemaError;

        public FFRKMySqlInstance()
        {
            mDatabaseThread = new BackgroundWorker();
            mDatabaseThread.DoWork += mDatabaseThread_DoWork;
            mDatabaseThread.RunWorkerAsync();
            mDatabaseQueue = new BlockingCollection<IDbRequest>();
            mCancellationTokenSource = new CancellationTokenSource();
            mConnectionState = ConnectionState.Disconnected;
        }

        public void Shutdown()
        {
            mCancellationTokenSource.Cancel();
        }

        string BuildConnectString(string Host, string User, string Password, string Schema)
        {
            if (String.IsNullOrEmpty(Host) || String.IsNullOrEmpty(Schema) || String.IsNullOrEmpty(User))
                throw new InvalidProgramException("Database host, user, and schema cannot be empty");

            return String.Format("server={0};user id={1};password={2};database={3};check parameters=false;persistsecurityinfo=True",
                                 Host, User, Password, Schema);
        }

        public ConnectResult TestConnect(string Host, string User, string Password, string Schema, uint MinimumRequiredSchema)
        {
            string ConnectString = BuildConnectString(Host, User, Password, Schema);
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(ConnectString);
                connection.Open();

                DbOpVerifySchema schema_request = new DbOpVerifySchema(MinimumRequiredSchema);
                schema_request.Execute(connection, null);
                DbOpVerifySchema.VerificationResult result = schema_request.Result;
                return TranslateSchemaVerificationResult(result);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public void InitializeConnection(uint MinimumRequiredSchema)
        {
            string Host = Properties.Settings.Default.DatabaseHost;
            string User = Properties.Settings.Default.DatabaseUser;
            string Password = Properties.Settings.Default.DatabasePassword;
            string Schema = Properties.Settings.Default.DatabaseSchema;

            string ConnectString = BuildConnectString(Host, User, Password, Schema);

            try
            {
                MySqlConnection connection = new MySqlConnection(ConnectString);
                connection.StateChange += MySqlConnection_StateChange;
                connection.Open();
                mDatabaseHost = Host;
                mConnection = connection;
                mConnStr = ConnectString;

                DbOpVerifySchema schema_request = new DbOpVerifySchema(MinimumRequiredSchema);
                schema_request.OnVerificationCompleted += DbOpGetSchemaInfo_OnVerificationCompleted;
                BeginExecuteRequest(schema_request);
            }
            catch (MySqlException)
            {
                if (OnConnectionInitialized != null)
                    OnConnectionInitialized(ConnectResult.InvalidConnection);
            }
        }

        private ConnectResult TranslateSchemaVerificationResult(DbOpVerifySchema.VerificationResult Result)
        {
            switch (Result)
            {
                case DbOpVerifySchema.VerificationResult.DatabaseTooOld:
                    return ConnectResult.SchemaTooOld;
                case DbOpVerifySchema.VerificationResult.DatabaseTooNew:
                    return ConnectResult.SchemaTooNew;
                default:
                    return ConnectResult.Success;
            }
        }

        void DbOpGetSchemaInfo_OnVerificationCompleted(DbOpVerifySchema.VerificationResult VerificationResult)
        {
            ConnectResult result = TranslateSchemaVerificationResult(VerificationResult);
            if (result != ConnectResult.Success)
            {
                mDatabaseDisabled = true;
                mConnectionState = ConnectionState.Disabled;
                if (OnConnectionStateChanged != null)
                    OnConnectionStateChanged(mConnectionState);
            }

            if (OnConnectionInitialized != null)
                OnConnectionInitialized(result);
        }

        void MySqlConnection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            ConnectionState new_state = ConnectionState.Disconnected;
            if (mDatabaseDisabled)
                new_state = ConnectionState.Disabled;
            else
            {
                switch (e.CurrentState)
                {
                    case System.Data.ConnectionState.Broken:
                    case System.Data.ConnectionState.Closed:
                        new_state = ConnectionState.Disconnected;
                        break;
                    case System.Data.ConnectionState.Connecting:
                        new_state = ConnectionState.Connecting;
                        break;
                    default:
                        new_state = ConnectionState.Connected;
                        break;
                }
            }

            if (new_state != mConnectionState)
            {
                Utility.Log.LogFormat("Database connection state changed.  Old = {0}, new = {1}", mConnectionState, new_state);
                mConnectionState = new_state;
                if (OnConnectionStateChanged != null)
                    OnConnectionStateChanged(new_state);
            }
        }

        void ProcessDbRequestOnThisThread(IDbRequest Request)
        {
            try
            {
                Utility.Log.LogFormat("Database exceuting operation {0}", Request.GetType().Name);
                EnsureConnected();

                if (Request.RequiresTransaction)
                {
                    using (MySqlTransaction transaction = mConnection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
                    {
                        try
                        {
                            Request.Execute(mConnection, transaction);
                            transaction.Commit();
                            Request.Respond();
                        }
                        catch (Exception ex)
                        {
                            Utility.Log.LogFormat("An error occurred executing the operation in a transaction.  Rolling back.  {0}", ex.Message);
                            Utility.Log.LogFormat(ex.StackTrace);
                            transaction.Rollback();
                        }
                    }
                }
                else
                {
                    Request.Execute(mConnection, null);
                    Request.Respond();
                }
            }
            catch (Exception ex)
            {
                Utility.Log.LogFormat("An error occurred executing request {0}.  Message = {1}.\n{2}", Request.GetType().Name, ex.Message, ex.StackTrace);
            }
        }

        void mDatabaseThread_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!mCancellationTokenSource.IsCancellationRequested)
            {
                IDbRequest request = null;
                try
                {
                    Utility.Log.LogString("Database thread waiting for request");
                    request = mDatabaseQueue.Take(mCancellationTokenSource.Token);

                    Utility.Log.LogFormat("Database thread dequeued request of type {0}", request.GetType().Name);
                    DbOpVerifySchema schema_request = new DbOpVerifySchema(FFRKProxy.Instance.MinimumRequiredSchema);
                    ProcessDbRequestOnThisThread(schema_request);

                    if (schema_request.Result != DbOpVerifySchema.VerificationResult.OK)
                    {
                        Utility.Log.LogString("Schema verification failed.  Disabling database connectivity.");
                        mConnectionState = ConnectionState.Disabled;
                        if (OnSchemaError != null)
                            OnSchemaError(TranslateSchemaVerificationResult(schema_request.Result));

                        if (OnConnectionStateChanged != null)
                            OnConnectionStateChanged(mConnectionState);
                        Shutdown();
                    }
                    else
                        ProcessDbRequestOnThisThread(request);
                }
                catch (OperationCanceledException)
                {
                    Utility.Log.LogString("Database worker thread shutting down because cancellation was requested.");
                }
                catch(Exception ex)
                {
                    Utility.Log.LogFormat("Database worker thread encountered an unknown exception.  {0}\n{1}", ex.Message, ex.StackTrace);
                }
            }
            Utility.Log.LogString("Database worker thread exiting.");
        }

        void EnsureConnected()
        {
            switch (mConnection.State)
            {
                case System.Data.ConnectionState.Broken:
                    Utility.Log.LogString("Database connection broken.  Attempting to re-open.");
                    // Need to close the connection and re-open it.
                    mConnection.Close();
                    break;
                case System.Data.ConnectionState.Closed:
                    // Connection is already closed, only need to re-open.
                    break;
                default:
                    // The connection is already open
                    return;
            }

            mConnection.Open();
        }

        public void BeginExecuteRequest(IDbRequest Request)
        {
            if (mDatabaseDisabled)
            {
                Utility.Log.LogFormat("Ignoring request {0} because database connectivity is disabled.", Request.GetType().Name);
                return;
            }

            try
            {
                mDatabaseQueue.Add(Request, mCancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                Utility.Log.LogFormat("An error occurred initiating request {0}.  Message = {1}.\n{2}", Request.GetType().Name, ex.Message, ex.StackTrace);
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}
