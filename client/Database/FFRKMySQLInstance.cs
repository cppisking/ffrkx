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
            Disconnected
        }

        BackgroundWorker mDatabaseThread = null;
        CancellationToken mCancellationToken = CancellationToken.None;
        BlockingCollection<IDbRequest> mDatabaseQueue = null;
        string mConnStr = null;
        MySqlConnection mConnection = null;
        ConnectionState mConnectionState;

        bool mInsertUnknownDungeons;
        bool mInsertUnknownWorlds;
        bool mInsertUnknownItems;
        bool mInsertUnknownBattles;

        string mDatabaseHost;
        string mDatabaseUser;
        string mDatabasePassword;

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

        public delegate void ConnectionStateChangedDelegate(ConnectionState NewState);
        public event ConnectionStateChangedDelegate OnConnectionStateChanged;

        public FFRKMySqlInstance()
        {
            mDatabaseThread = new BackgroundWorker();
            mDatabaseThread.DoWork += mDatabaseThread_DoWork;
            mDatabaseThread.RunWorkerAsync();
            mDatabaseQueue = new BlockingCollection<IDbRequest>();
            mCancellationToken = new CancellationToken();
            mConnectionState = ConnectionState.Disconnected;
        }

        string BuildConnectString(string Host, string User, string Password, string Schema)
        {
            if (String.IsNullOrEmpty(Host) || String.IsNullOrEmpty(Schema) || String.IsNullOrEmpty(User))
                throw new InvalidProgramException("Database host, user, and schema cannot be empty");

            return String.Format("server={0};user id={1};password={2};database={3};check parameters=false;persistsecurityinfo=True",
                                 Host, User, Password, Schema);
        }

        public void TestConnect(string Host, string User, string Password, string Schema)
        {
            string ConnectString = BuildConnectString(Host, User, Password, Schema);
            MySqlConnection connection = new MySqlConnection(ConnectString);
            connection.Open();
            connection.Close();
        }

        void Connect()
        {
            string Host = Properties.Settings.Default.DatabaseHost;
            string User = Properties.Settings.Default.DatabaseUser;
            string Password = Properties.Settings.Default.DatabasePassword;
            string Schema = Properties.Settings.Default.DatabaseSchema;

            string ConnectString = BuildConnectString(Host, User, Password, Schema);

            MySqlConnection connection = new MySqlConnection(ConnectString);
            connection.StateChange += MySqlConnection_StateChange;
            connection.Open();
            mConnection = connection;
            mConnStr = ConnectString;
        }

        void MySqlConnection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            ConnectionState new_state = ConnectionState.Disconnected;
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
            if (new_state != mConnectionState)
            {
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
            while (!mCancellationToken.IsCancellationRequested)
            {
                IDbRequest request = null;
                try
                {
                    Utility.Log.LogString("Database thread waiting for request");
                    request = mDatabaseQueue.Take(mCancellationToken);

                    ProcessDbRequestOnThisThread(request);
                }
                catch (OperationCanceledException)
                {

                }
            }
        }

        void EnsureConnected()
        {
            if (mConnection == null)
                Connect();

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
            try
            {
                mDatabaseQueue.Add(Request, mCancellationToken);
            }
            catch (Exception ex)
            {
                Utility.Log.LogFormat("An error occurred initiating request {0}.  Message = {1}.\n{2}", Request.GetType().Name, ex.Message, ex.StackTrace);
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}
