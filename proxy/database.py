import mysql.connector

from recordpeeker import DBCONN

def trans_begin():
    if not DBCONN:
        return
    DBCONN.start_transaction()

def trans_rollback():
    if not DBCONN:
        return
    try:
        DBCONN.rollback()
    except mysql.connector.Error as err:
        print "An error occurred rolling back the data: %s" % err.message

def trans_commit():
    if not DBCONN:
        return
    try:
        DBCONN.commit()
    except mysql.connector.Error as err:
        print "An error occurred commiting the data: %s" % err.message

def record_battle_encounter(battle_id):
    if not DBCONN:
        return
    try:
        cursor = DBCONN.cursor()
        values = (int(battle_id),)
        cursor.callproc("record_battle_encounter", values)
    except mysql.connector.Error as err:
        print "An error occurred recording the battle encounter: %s" % err.message
        raise

def record_drop_event(battle_id, item_id, item_count):
    if not DBCONN:
        return
    try:
        cursor = DBCONN.cursor()
        values = (int(battle_id), int(item_id), int(item_count))
        cursor.callproc("record_drop_event", values)
    except mysql.connector.Error as err:
        print "An error occurred recording the drop event: %s" % err.message
        raise
