﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

using Fiddler;

using ffrk_winproxy.GameData;

namespace ffrk_winproxy.Database
{
    static class FFRKMySqlInstance
    {
        static string mConnStr = "server=localhost;user id=ffrkserver;persistsecurityinfo=True;database=ffrktest;password=xHBLO0hZEajI4DVDHDTP";
        static MySqlConnection mConnection = null;

        static bool Connect()
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

        public static void RefreshDungeonCache()
        {
            if (!Connect())
                return;

            string Stmt = "SELECT * FROM dungeons";
            using (MySqlCommand command = new MySqlCommand(Stmt, mConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    FFRKCacheDungeons.mDungeonCache.Clear();
                    while (reader.Read())
                    {
                        Type t = reader.GetFieldType("difficulty");
                        FFRKCacheDungeons.Key key = new FFRKCacheDungeons.Key();
                        FFRKCacheDungeons.Data data = new FFRKCacheDungeons.Data();
                        key.DungeonId = reader.GetFieldValue<uint>(reader.GetOrdinal("id"));
                        data.Difficulty = reader.GetFieldValue<ushort>(reader.GetOrdinal("difficulty"));
                        data.Name = reader.GetFieldValue<string>(reader.GetOrdinal("name"));
                        data.Type = reader.GetFieldValue<byte>(reader.GetOrdinal("type"));
                        data.WorldId = reader.GetFieldValue<uint>(reader.GetOrdinal("world"));
                        FFRKCacheDungeons.mDungeonCache.Add(key, data);
                    }
                }
            }
        }

        public static void RefreshDungeonDropsCache()
        {
            if (!Connect())
                return;

            string Stmt = "SELECT * FROM dungeon_drops";
            using (MySqlCommand command = new MySqlCommand(Stmt, mConnection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    FFRKCacheDungeonDrops.mDropCache.Clear();
                    while (reader.NextResult())
                    {
                        FFRKCacheDungeonDrops.Key key = new FFRKCacheDungeonDrops.Key();
                        FFRKCacheDungeonDrops.Data data = new FFRKCacheDungeonDrops.Data();
                        key.BattleId = reader.GetFieldValue<uint>(reader.GetOrdinal("battleid"));
                        key.ItemId = reader.GetFieldValue<uint>(reader.GetOrdinal("itemid"));
                        key.EnemyId = reader.GetFieldValue<uint>(reader.GetOrdinal("enemyid"));
                        data.BattleName = reader.GetFieldValue<string>(reader.GetOrdinal("battle_name"));
                        data.EnemyName = reader.GetFieldValue<string>(reader.GetOrdinal("enemy_name"));
                        data.ItemName = reader.GetFieldValue<string>(reader.GetOrdinal("item_name"));
                        data.NumDrops = reader.GetFieldValue<uint>(reader.GetOrdinal("drop_count"));
                        FFRKCacheDungeonDrops.mDropCache.Add(key, data);
                    }
                }
            }
        }

        static public void RecordBattleEncounter(EventBattleInitiated encounter)
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
                    CallProcRecordBattleEncounter(transaction, encounter.Battle.BattleId);
                    List<DropEvent> drops = encounter.Battle.Drops.ToList();
                    foreach (DropEvent drop in drops)
                    {
                        if (drop.ItemType == DataEnemyDropItem.DropItemType.Gold)
                            continue;

                        try
                        {
                            CallProcRecordDropEvent(transaction, encounter.Battle, drop);
                        }
                        catch (MySqlException)
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
                    } 
                    else
                    {
                        FiddlerApplication.Log.LogFormat("WARNING: Committed drop information for battle #{0}.  {1}/{2} items failed.",
                            encounter.Battle.BattleId, failedDropCount, drops.Count, failedDropList.ToString());
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    FiddlerApplication.Log.LogFormat("An error occurred recording the battle encounter.  {0}", e.Message);
                    transaction.Rollback();
                }
            }
        }

        static public void RecordDungeonList(EventListDungeons dungeons)
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
                }
                catch(Exception ex)
                {
                    FiddlerApplication.Log.LogFormat("An error occurred recording the dungeons for world '{0}'.  {1}", dungeons.World.Name, ex.Message);
                    transaction.Rollback();
                }
            }
        }

        static public void RecordBattleList(EventListBattles battles)
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
        }

        static MySqlTransaction BeginTransaction()
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

        static int CallProcInsertBattleEntry(MySqlTransaction transaction, DataBattle battle)
        {
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

        static int CallProcInsertWorldEntry(MySqlTransaction transaction, DataWorld world)
        {
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

        static int CallProcInsertDungeonEntry(MySqlTransaction transaction, DataWorld world, DataDungeon dungeon)
        {
            using (MySqlCommand command = new MySqlCommand("insert_dungeon_entry", mConnection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@did", dungeon.Id);
                command.Parameters.AddWithValue("@world_id", world.Id);
                command.Parameters.AddWithValue("@dname", dungeon.Name);
                command.Parameters.AddWithValue("@dtype", dungeon.Type+1);
                command.Parameters.AddWithValue("@ddiff", dungeon.Difficulty);
                command.Parameters.AddWithValue("@dsyn", 0);
                return command.ExecuteNonQuery();
            }
        }

        static int CallProcRecordBattleEncounter(MySqlTransaction transaction, uint battle_id)
        {
            using (MySqlCommand command = new MySqlCommand("record_battle_encounter", mConnection, transaction))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@battle_id", battle_id);
                return command.ExecuteNonQuery();
            }
        }

        static int CallProcRecordDropEvent(MySqlTransaction transaction, DataActiveBattle battle, DropEvent drop)
        {
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
