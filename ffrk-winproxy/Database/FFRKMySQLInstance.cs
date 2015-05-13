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

namespace FFRKInspector.Database
{
    class FFRKMySqlInstance
    {
        BackgroundWorker mDatabaseThread = null;
        CancellationToken mCancellationToken = CancellationToken.None;
        BlockingCollection<IDbRequest> mDatabaseQueue = null;
        static string mConnStr = null;
        static MySqlConnection mConnection = null;

        public delegate void DungeonDropsAvailableDelegate(List<BasicItemDropStats> items);

        static FFRKMySqlInstance()
        {
            mConnStr = "server=localhost;user id=ffrkserver;persistsecurityinfo=True;database=ffrktest;password=xHBLO0hZEajI4DVDHDTP;check parameters=false";
            mConnection = null;
        }

        public FFRKMySqlInstance()
        {
            mDatabaseThread = new BackgroundWorker();
            mDatabaseThread.DoWork += mDatabaseThread_DoWork;
            mDatabaseThread.RunWorkerAsync();
            mDatabaseQueue = new BlockingCollection<IDbRequest>();
            mCancellationToken = new CancellationToken();
        }

        void ProcessDbRequestOnThisThread(IDbRequest Request)
        {
            try
            {
                FiddlerApplication.Log.LogFormat("Database exceuting operation {0}", Request.GetType().Name);
                Connect();

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
                            FiddlerApplication.Log.LogFormat("An error occurred executing the operation in a transaction.  Rolling back.  {0}", ex.Message);
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
                FiddlerApplication.Log.LogFormat("An error occurred executing request {0}.  Message = {1}.\n{2}", Request.GetType().Name, ex.Message, ex.StackTrace);
                System.Diagnostics.Debugger.Break();
            }
        }

        void mDatabaseThread_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!mCancellationToken.IsCancellationRequested)
            {
                IDbRequest request = null;
                try
                {
                    FiddlerApplication.Log.LogString("Database thread waiting for request");
                    request = mDatabaseQueue.Take(mCancellationToken);

                    ProcessDbRequestOnThisThread(request);
                }
                catch (OperationCanceledException)
                {

                }
            }
        }

        void Connect()
        {
            if (mConnection == null)
            {
                // We've never connected before
                mConnection = new MySqlConnection(mConnStr);
            }
            else
            {
                switch (mConnection.State)
                {
                    case System.Data.ConnectionState.Broken:
                        FiddlerApplication.Log.LogString("Database connection broken.  Attempting to re-open.");
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
                FiddlerApplication.Log.LogFormat("An error occurred initiating request {0}.  Message = {1}.\n{2}", Request.GetType().Name, ex.Message, ex.StackTrace);
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}
