import mysql.connector

from ffrkx.proto import messages_pb2

class Database:
    def __init__(self, user, password, host, database):
        try:
            self.DBCONN = mysql.connector.connect(user=user, password=password,
                                                  host=host, database=database)
        except mysql.connector.Error as err:
            print "Unable to connect to MYSQL.  Error = %s" % err.message
            self.DBCONN = None
        pass


    def trans_begin(self):
        if not self.DBCONN:
            return
        self.DBCONN.start_transaction()

    def trans_rollback(self):
        if not self.DBCONN:
            return
        try:
            self.DBCONN.rollback()
        except mysql.connector.Error as err:
            print "An error occurred rolling back the data: %s" % err.message

    def trans_commit(self):
        if not self.DBCONN:
            return
        try:
            self.DBCONN.commit()
        except mysql.connector.Error as err:
            print "An error occurred commiting the data: %s" % err.message
    def fetch_all_battles(self):
        if not self.DBCONN:
            return []
        try:
            cursor = self.DBCONN.cursor()
            cursor.execute("SELECT id, name FROM battles")
            return [x for x in cursor]
        except mysql.connector.Error as err:
            return []
        finally:
            cursor.close()

    def fetch_all_items(self):
        if not self.DBCONN:
            return []
        try:
            cursor = self.DBCONN.cursor()
            cursor.execute("SELECT id, name FROM items")
            return [x for x in cursor]

        except mysql.connector.Error as err:
            return []
        finally:
            cursor.close()

    def record_battle_encounter(self, message):
        if not self.DBCONN:
            return
        try:
            print "Recording battle encounter for battle {0} ({1} items)".format(message.battle_id, len(message.drop_list))
            self.trans_begin()
            cursor = self.DBCONN.cursor()
            cursor.callproc("record_battle_encounter", (message.battle_id,))
            for drop_event in message.drop_list:
                cursor.callproc("record_drop_event", (message.battle_id, drop_event.item_id, drop_event.count))
            self.trans_commit()
            print "Successfully commited battle encounter"
        except mysql.connector.Error as err:
            self.trans_rollback()
            print "An error occurred committing the battle encounter: %s" % err.message
            raise
        finally:
            cursor.close()

    def record_dungeon_entry(self, message):
        pass

    def handle_message(self, message):
        if message.HasField("battle_encounter"):
            self.record_battle_encounter(message.battle_encounter)
        if message.HasField("dungeon_entry"):
            self.record_dungeon_entry(message.dungeon_entry)
        pass