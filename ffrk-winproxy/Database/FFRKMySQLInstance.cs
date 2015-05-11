using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

using Fiddler;

using ffrk_winproxy.GameData;
using System.ComponentModel;
using ffrk_winproxy.DataCache;

namespace ffrk_winproxy.Database
{
    class FFRKMySqlInstance
    {
        BackgroundWorker mDatabaseThread = null;
        static string mConnStr = null;
        static MySqlConnection mConnection = null;
        FFRKDataCache mCache = null;

        enum WorkOperation
        {
            RefreshBattleCache,
            RefreshDungeonDropsCache,
            RefreshDungeonsCache,
            RefreshItemsCache,
            RefreshWorldsCache,
            RecordBattleEncounter,
            RecordDungeonList,
            RecordBattleList
        }

        class BackgroundJob
        {
            public Action Invoke;
            public Action Notify;
        }

        static FFRKMySqlInstance()
        {
            mConnStr = "server=localhost;user id=ffrkserver;persistsecurityinfo=True;database=ffrktest;password=xHBLO0hZEajI4DVDHDTP";
            mConnection = null;
        }

        public FFRKMySqlInstance(FFRKDataCache cache)
        {
            mCache = cache;
            mDatabaseThread = new BackgroundWorker();
            mDatabaseThread.DoWork += mDatabaseThread_DoWork;
            mDatabaseThread.RunWorkerCompleted += mDatabaseThread_RunWorkerCompleted;
        }

        public delegate void DBOperationCompleteDelegate();

        public event DBOperationCompleteDelegate OnBattlesUpdated;
        public event DBOperationCompleteDelegate OnDungeonDropsUpdated;
        public event DBOperationCompleteDelegate OnDungeonsUpdated;
        public event DBOperationCompleteDelegate OnItemsUpdated;
        public event DBOperationCompleteDelegate OnWorldsUpdated;

        void mDatabaseThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundJob job = (BackgroundJob)e.Result;
            if (job.Notify != null)
                job.Notify();
        }

        void mDatabaseThread_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundJob job = (BackgroundJob)e.Argument;
            job.Invoke();
            e.Result = job;
        }

        bool Connect()
        {
            try
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
            }
            catch(Exception ex)
            {
                FiddlerApplication.Log.LogFormat("An error occurred connecting to the database.  {0}", ex.Message);
                mConnection = null;
                return false;
            }
        }

        void BeginInvoke(Action job, Action notify)
        {
            mDatabaseThread.RunWorkerAsync(new BackgroundJob { Invoke = job, Notify = notify });
        }

        void BeginInvoke(Action job)
        {
            mDatabaseThread.RunWorkerAsync(new BackgroundJob { Invoke = job, Notify = null });
        }

        public void BeginRefreshDungeonCache()
        {
            BeginInvoke((Action)(() =>
                {
                    if (!Connect())
                        return;

                    string Stmt = "SELECT * FROM dungeons";
                    using (MySqlCommand command = new MySqlCommand(Stmt, mConnection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            mCache.Dungeons.Clear();
                            while (reader.Read())
                            {
                                Type t = reader.GetFieldType("difficulty");
                                DataCache.Dungeons.Key key = new DataCache.Dungeons.Key();
                                DataCache.Dungeons.Data data = new DataCache.Dungeons.Data();
                                key.DungeonId = reader.GetFieldValue<uint>(reader.GetOrdinal("id"));
                                data.Difficulty = reader.GetFieldValue<ushort>(reader.GetOrdinal("difficulty"));
                                data.Name = reader.GetFieldValue<string>(reader.GetOrdinal("name"));
                                data.Type = reader.GetFieldValue<byte>(reader.GetOrdinal("type"));
                                data.WorldId = reader.GetFieldValue<uint>(reader.GetOrdinal("world"));
                                mCache.Dungeons.Update(key, data);
                            }
                        }
                    }
                }),
                (Action)(() => { if (OnDungeonsUpdated != null) OnDungeonsUpdated(); }));
        }

        public void BeginRefreshDungeonDropsCache()
        {
            BeginInvoke((Action)(() =>
                {
                    if (!Connect())
                        return;

                    string Stmt = "SELECT * FROM dungeon_drops";
                    using (MySqlCommand command = new MySqlCommand(Stmt, mConnection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            mCache.Drops.Clear();
                            while (reader.Read())
                            {
                                DataCache.DungeonDrops.Key key = new DataCache.DungeonDrops.Key();
                                DataCache.DungeonDrops.Data data = new DataCache.DungeonDrops.Data();
                                key.BattleId = reader.GetFieldValue<uint>(reader.GetOrdinal("battleid"));
                                key.ItemId = reader.GetFieldValue<uint>(reader.GetOrdinal("itemid"));
                                key.EnemyId = reader.GetFieldValue<uint>(reader.GetOrdinal("enemyid"));
                                data.BattleName = reader.GetFieldValue<string>(reader.GetOrdinal("battle_name"));
                                data.EnemyName = reader.GetFieldValue<string>(reader.GetOrdinal("enemy_name"));
                                data.ItemName = reader.GetFieldValue<string>(reader.GetOrdinal("item_name"));
                                data.NumDrops = reader.GetFieldValue<uint>(reader.GetOrdinal("drop_count"));
                                mCache.Drops.Update(key, data);
                            }
                        }
                    }
                }),
                (Action)(() => { if (OnDungeonDropsUpdated != null) OnDungeonDropsUpdated(); }));
        }

        public void BeginRefreshWorldsCache()
        {
            BeginInvoke((Action)(() =>
            {
                if (!Connect())
                    return;

                string Stmt = "SELECT * FROM worlds";
                using (MySqlCommand command = new MySqlCommand(Stmt, mConnection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        mCache.Worlds.Clear();
                        while (reader.Read())
                        {
                            DataCache.Worlds.Key key = new DataCache.Worlds.Key();
                            DataCache.Worlds.Data data = new DataCache.Worlds.Data();
                            key.WorldId = reader.GetFieldValue<uint>(reader.GetOrdinal("id"));
                            data.Series = reader.GetFieldValue<ushort>(reader.GetOrdinal("series"));
                            data.Type = reader.GetFieldValue<byte>(reader.GetOrdinal("type"));
                            data.Name = reader.GetFieldValue<string>(reader.GetOrdinal("name"));
                            mCache.Worlds.Update(key, data);
                        }
                    }
                }
            }),
            (Action)(() => { if (OnWorldsUpdated != null) OnWorldsUpdated(); }));
        }

        public void BeginRefreshItemsCache()
        {
            BeginInvoke((Action)(() =>
            {
                if (!Connect())
                    return;

                string Stmt = "SELECT * FROM items";
                using (MySqlCommand command = new MySqlCommand(Stmt, mConnection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        mCache.Items.Clear();
                        while (reader.Read())
                        {
                            DataCache.Items.Key key = new DataCache.Items.Key();
                            DataCache.Items.Data data = new DataCache.Items.Data();
                            key.ItemId = reader.GetFieldValue<uint>(reader.GetOrdinal("id"));
                            data.Name = reader.GetFieldValue<string>(reader.GetOrdinal("name"));
                            data.Rarity = reader.GetFieldValue<byte>(reader.GetOrdinal("rarity"));
                            data.Type = reader.GetFieldValue<byte>(reader.GetOrdinal("type"));
                            data.Subtype = reader.GetFieldValue<byte>(reader.GetOrdinal("subtype"));
                            int realm_ordinal = reader.GetOrdinal("realm");
                            if (reader.IsDBNull(realm_ordinal))
                                data.Realm = null;
                            else
                                data.Realm = reader.GetFieldValue<byte>(realm_ordinal);
                            mCache.Items.Update(key, data);
                        }
                    }
                }
            }),
            (Action)(() => { if (OnItemsUpdated != null) OnItemsUpdated(); }));
        }

        public void BeginRefreshBattlesCache()
        {
            BeginInvoke((Action)(() =>
            {
                if (!Connect())
                    return;

                string Stmt = "SELECT * FROM battles";
                using (MySqlCommand command = new MySqlCommand(Stmt, mConnection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        mCache.Battles.Clear();
                        while (reader.Read())
                        {
                            DataCache.Battles.Key key = new DataCache.Battles.Key();
                            DataCache.Battles.Data data = new DataCache.Battles.Data();
                            key.BattleId = reader.GetFieldValue<uint>(reader.GetOrdinal("id"));
                            data.DungeonId = reader.GetFieldValue<uint>(reader.GetOrdinal("dungeon"));
                            data.Name = reader.GetFieldValue<string>(reader.GetOrdinal("name"));
                            data.Stamina = reader.GetFieldValue<byte>(reader.GetOrdinal("stamina"));
                            data.TimesRun = reader.GetFieldValue<byte>(reader.GetOrdinal("times_run"));
                            mCache.Battles.Update(key, data);
                        }
                    }
                }
            }),
            (Action)(() => { if (OnBattlesUpdated != null) OnBattlesUpdated(); }));
        }

        public void BeginRecordBattleEncounter(EventBattleInitiated encounter)
        {
            BeginInvoke((Action)(() =>
            {
                FiddlerApplication.Log.LogFormat("Received BattleEncounterMsg for battle #{0}", encounter.Battle.BattleId);

                if (!Connect())
                    return;

                MySqlTransaction transaction = BeginTransaction();
                if (transaction == null)
                    return;

                using (transaction)
                {
                    try
                    {
                        int failedDropCount = 0;
                        StringBuilder failedDropList = new StringBuilder();
                        CallProcRecordBattleEncounter(transaction, encounter.Battle);
                        List<DropEvent> drops = encounter.Battle.Drops.ToList();
                        // TODO(cpp): Record enemy (id,name) pairs here.

                        foreach (DropEvent drop in drops)
                        {
                            if (drop.ItemType == DataEnemyDropItem.DropItemType.Gold)
                                continue;

                            try
                            {
                                CallProcRecordDropEvent(transaction, encounter.Battle, drop);
                            } catch (MySqlException)
                            {
                                if (failedDropCount++ > 0)
                                    failedDropList.Append(", ");
                                failedDropList.AppendFormat("[item: {0}, round: {1}, enemy: {2}]", drop.ItemId, drop.Round, drop.EnemyName);
                            }
                        }
                        if (failedDropCount == 0)
                        {
                            FiddlerApplication.Log.LogFormat("Committed drop information for battle #{0}.  0/{1} items failed.",
                                encounter.Battle.BattleId, drops.Count);
                        } else
                        {
                            FiddlerApplication.Log.LogFormat("WARNING: Committed drop information for battle #{0}.  {1}/{2} items failed.",
                                encounter.Battle.BattleId, failedDropCount, drops.Count, failedDropList.ToString());
                        }

                        transaction.Commit();
                    } catch (Exception e)
                    {
                        FiddlerApplication.Log.LogFormat("An error occurred recording the battle encounter.  {0}", e.Message);
                        transaction.Rollback();
                    }
                }
            }));

        }

        public void BeginRecordDungeonList(EventListDungeons dungeons)
        {
            BeginInvoke((Action)(() =>
            {
                FiddlerApplication.Log.LogFormat("Received ListDungeonsMsg for world #{0} ({1} dungeons)",
                    dungeons.World.Id, dungeons.Dungeons.Count);

                if (!Connect())
                    return;

                MySqlTransaction transaction = BeginTransaction();
                if (transaction == null)
                    return;
                using (transaction)
                {
                    try
                    {
                        CallProcInsertWorldEntry(transaction, dungeons.World);

                        foreach (DataDungeon dungeon in dungeons.Dungeons)
                            CallProcInsertDungeonEntry(transaction, dungeons.World, dungeon);
                        transaction.Commit();
                    } catch (Exception ex)
                    {
                        FiddlerApplication.Log.LogFormat("An error occurred recording the dungeons for world '{0}'.  {1}", dungeons.World.Name, ex.Message);
                        transaction.Rollback();
                    }
                }
            }));
        }

        public void BeginRecordBattleList(EventListBattles battles)
        {
            BeginInvoke((Action)(() =>
            {

                FiddlerApplication.Log.LogFormat("Received ListBattlesMsg for dungeon #{0} ({1} battles)",
                    battles.DungeonSession.DungeonId, battles.Battles.Count);

                if (!Connect())
                    return;

                MySqlTransaction transaction = BeginTransaction();
                if (transaction == null)
                    return;
                using (transaction)
                {
                    try
                    {
                        foreach (DataBattle battle in battles.Battles)
                            CallProcInsertBattleEntry(transaction, battle);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        FiddlerApplication.Log.LogFormat("An error occurred recording the battles for dungeon '{0}'.  {1}", battles.DungeonSession.Name, ex.Message);
                        transaction.Rollback();
                    }
                }
            })); ;
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

        int CallProcInsertBattleEntry(MySqlTransaction transaction, DataBattle battle)
        {
            DataCache.Battles.Key k = new DataCache.Battles.Key { BattleId=battle.Id };
            DataCache.Battles.Data d = new DataCache.Battles.Data { DungeonId=battle.DungeonId, Name=battle.Name, Stamina=battle.Stamina, TimesRun=0 };
            mCache.Battles.Update(k, d);

            using (MySqlCommand command = new MySqlCommand("insert_battle_entry", mConnection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@bid", battle.Id);
                command.Parameters.AddWithValue("@did", battle.DungeonId);
                command.Parameters.AddWithValue("@bname", battle.Name);
                command.Parameters.AddWithValue("@bstam", battle.Stamina);
                return command.ExecuteNonQuery();
            }
        }

        int CallProcInsertWorldEntry(MySqlTransaction transaction, DataWorld world)
        {
            DataCache.Worlds.Key k = new DataCache.Worlds.Key { WorldId = world.Id };
            DataCache.Worlds.Data d = new DataCache.Worlds.Data { Name = world.Name, Type = world.Type, Series = world.SeriesId };
            mCache.Worlds.Update(k, d);

            using (MySqlCommand command = new MySqlCommand("insert_world_entry", mConnection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@wid", world.Id);
                command.Parameters.AddWithValue("@series", world.SeriesId);
                command.Parameters.AddWithValue("@type", world.Type);
                command.Parameters.AddWithValue("@name", world.Name);
                return command.ExecuteNonQuery();
            }
        }

        int CallProcInsertDungeonEntry(MySqlTransaction transaction, DataWorld world, DataDungeon dungeon)
        {
            DataCache.Dungeons.Key k = new DataCache.Dungeons.Key { DungeonId = dungeon.Id };
            DataCache.Dungeons.Data d = new DataCache.Dungeons.Data { WorldId = world.Id, Name = dungeon.Name, Type = dungeon.Type, Difficulty = dungeon.Difficulty };
            mCache.Dungeons.Update(k, d);

            using (MySqlCommand command = new MySqlCommand("insert_dungeon_entry", mConnection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@did", dungeon.Id);
                command.Parameters.AddWithValue("@world_id", world.Id);
                command.Parameters.AddWithValue("@dname", dungeon.Name);
                command.Parameters.AddWithValue("@dtype", dungeon.Type);
                command.Parameters.AddWithValue("@ddiff", dungeon.Difficulty);
                command.Parameters.AddWithValue("@dsyn", 0);
                return command.ExecuteNonQuery();
            }
        }

        int CallProcRecordBattleEncounter(MySqlTransaction transaction, DataActiveBattle battle)
        {
            DataCache.Battles.Data battle_data = null;
            if (mCache.Battles.TryGetValue(new DataCache.Battles.Key {BattleId = battle.BattleId}, out battle_data))
                ++battle_data.TimesRun;

            using (MySqlCommand command = new MySqlCommand("record_battle_encounter", mConnection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@battle_id", battle.BattleId);
                return command.ExecuteNonQuery();
            }
        }

        int CallProcRecordDropEvent(MySqlTransaction transaction, DataActiveBattle battle, DropEvent drop)
        {
            // Don't update the items cache here.  A DropEvent doesn't contain enough information to insert
            // an item anyway (in particular because it doesn't tell you the name of an item)

            using (MySqlCommand command = new MySqlCommand("record_drop_event", mConnection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@battle_id", battle.BattleId);
                command.Parameters.AddWithValue("@item_id", drop.ItemId);
                command.Parameters.AddWithValue("@item_count", 1);
                return command.ExecuteNonQuery();
            }
        }
    }
}
