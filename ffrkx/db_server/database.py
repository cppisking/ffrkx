import mysql.connector

from ffrkx.proto import messages_pb2
from ffrkx.util import log
from ffrkx.db_server.resources import Cursor, Transaction

class Database:
    def __init__(self, user, password, host, database):
        try:
            self.DBCONN = mysql.connector.connect(user=user, password=password,
                                                  host=host, database=database)
            log.log_message("Successfully connected to MySQL instance `{0}`".format(database))
        except err:
            log.log_exception("Unable to connect to MySQL")
            self.DBCONN = None
        pass

    def fetch_all_battles(self):
        if not self.DBCONN:
            return []

        cursor = self.DBCONN.cursor()
        with Cursor(cursor):
            cursor.execute("SELECT id, name FROM battles")
            return [x for x in cursor]

    def fetch_all_items(self):
        if not self.DBCONN:
            return []
        cursor = self.DBCONN.cursor()
        with Cursor(cursor):
            cursor.execute("SELECT id, name FROM items")
            return [x for x in cursor]

    def record_battle_encounter(self, message):
        if not self.DBCONN:
            return

        with Transaction(self.DBCONN):
            log.log_message("Recording battle encounter for battle {0} ({1} items)".format(message.battle_id, len(message.drop_list)))
            cursor = self.DBCONN.cursor()
            with Cursor(cursor):
                cursor.callproc("record_battle_encounter", (message.battle_id,))
                for drop_event in message.drop_list:
                    cursor.callproc("record_drop_event", (message.battle_id, drop_event.item_id, drop_event.count))
                log.log_message("Successfully commited battle encounter")

    def record_list_dungeons(self, message):
        if not self.DBCONN:
            return
        failures = 0
        with Transaction(self.DBCONN):
            for dungeon in message.dungeon_list:
                try:
                    cursor = self.DBCONN.cursor();
                    with Cursor(cursor):
                        cursor.callproc("insert_dungeon_entry", (dungeon.id, message.world_id, dungeon.name, dungeon.type, dungeon.difficulty, message.synergy))
                except:
                    log.log_exception("An error occurred inserting record for dungeon {0}".format(dungeon.id))
                    failures = failures + 1
        log.log_message("Committed {0} dungeon entries ({1} failed)".format(len(message.dungeon_list), failures))

    def record_list_battles(self, message):
        if not self.DBCONN:
            return
        failures = 0
        with Transaction(self.DBCONN):
            for battle in message.battle_list:
                try:
                    cursor = self.DBCONN.cursor();
                    with Cursor(cursor):
                        cursor.callproc("insert_battle_entry", (battle.id, message.dungeon_id, battle.name, battle.stamina))
                except:
                    failures = failures + 1
                    log.log_exception("An error occurred inserting record for battle {0}".format(battle.id))
        log.log_message("Committed {0} battle entries ({1} failed)".format(len(message.battle_list), failures))

    def handle_message(self, message):
        if message.HasField("battle_encounter"):
            self.record_battle_encounter(message.battle_encounter)
        if message.HasField("list_dungeons"):
            self.record_list_dungeons(message.list_dungeons)
        if message.HasField("list_battles"):
            self.record_list_battles(message.list_battles)
        pass