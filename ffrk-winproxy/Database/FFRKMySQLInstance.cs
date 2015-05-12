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
        class InternalDbRequest
        {
            private IDbRequest mNonTransactedRequest;
            private ITransactedDbRequest mTransactedRequest;

            public InternalDbRequest(IDbRequest Request)
            {
                mNonTransactedRequest = Request;
            }

            public InternalDbRequest(ITransactedDbRequest Request)
            {
                mTransactedRequest = Request;
            }

            public IDbRequest Request { get { return mNonTransactedRequest; } }
            public ITransactedDbRequest TransactedRequest { get { return mTransactedRequest; } }
            public bool IsTransacted { get { return mTransactedRequest != null; } }
        }

        BackgroundWorker mDatabaseThread = null;
        CancellationToken mCancellationToken = CancellationToken.None;
        BlockingCollection<InternalDbRequest> mDatabaseQueue = null;
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
            mDatabaseQueue = new BlockingCollection<InternalDbRequest>();
            mCancellationToken = new CancellationToken();
        }

        void mDatabaseThread_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!mCancellationToken.IsCancellationRequested)
            {
                try
                {
                    FiddlerApplication.Log.LogString("Database thread waiting for request");
                    InternalDbRequest request = mDatabaseQueue.Take(mCancellationToken);
                    FiddlerApplication.Log.LogFormat("Database dequeued operation of type {0}", request.GetType().Name);
                    if (!Connect())
                        throw new InvalidProgramException("Cannot connect to the database");
                    if (request.IsTransacted)
                    {
                        using (MySqlTransaction transaction = mConnection.BeginTransaction())
                        {
                            try
                            {
                                request.TransactedRequest.Execute(mConnection, transaction);
                                transaction.Commit();
                                request.TransactedRequest.Respond();
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
                        request.Request.Execute(mConnection);
                        request.Request.Respond();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debugger.Break();
                }
            }
        }

        public void BeginExecuteRequest(IDbRequest Request)
        {
            try
            {
                InternalDbRequest InternalRequest = new InternalDbRequest(Request);
                mDatabaseQueue.Add(InternalRequest, mCancellationToken);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        public void BeginExecuteRequest(ITransactedDbRequest Request)
        {
            try
            {
                InternalDbRequest InternalRequest = new InternalDbRequest(Request);
                mDatabaseQueue.Add(InternalRequest, mCancellationToken);
            } catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        private bool Connect()
        {
            try
            {
                if (mConnection == null)
                {
                    // We've never connected before
                    mConnection = new MySqlConnection(mConnStr);
                } else
                {
                    switch (mConnection.State)
                    {
                        case System.Data.ConnectionState.Broken:
                            // Need to close the connection and re-open it.
                            mConnection.Close();
                            break;
                        case System.Data.ConnectionState.Closed:
                            // Connection is already closed, only need to re-open.
                            break;
                        default:
                            // The connection is already open
                            return true;
                    }
                }

                mConnection.Open();
                return true;
            } catch (Exception ex)
            {
                FiddlerApplication.Log.LogFormat("An error occurred connecting to the database.  {0}", ex.Message);
                mConnection = null;
                return false;
            }
        }

        MySqlTransaction BeginTransaction()
        {
            try
            {
                return mConnection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            }
            catch (Exception ex)
            {
                FiddlerApplication.Log.LogFormat("Unable to begin a database transaction.  This message will not be recorded.  {0}", ex.Message);
                return null;
            }
        }
    }
}
