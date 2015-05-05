import json
import shlex
import os
import socket
import heapq
from collections import OrderedDict, defaultdict

from libmproxy.protocol.http import decoded
from tabulate import tabulate

from csv_data import Equipment, ITEMS, BATTLES, DUNGEONS, slicedict, best_equipment
from dispatcher import Dispatcher
import options

pending_drop_info = None

def get_display_name(enemy):
    for child in enemy["children"]:
        for param in child["params"]:
            return param.get("disp_name", "Unknown Enemy")

def get_drops(enemy):
    for child in enemy["children"]:
        for drop in child["drop_item_list"]:
            yield drop

def commit_pending_drop_info():
    global pending_drop_info
    if pending_drop_info == None:
        print "There is no pending drop info, exiting..."
        return

    import database
    battle = pending_drop_info.get('battle_id', None)
    if battle is not None:
        try:
            database.trans_begin()
            database.record_battle_encounter(battle)

            items = pending_drop_info['drops']
            for item in items:
                count = items[item]
                database.record_drop_event(battle, item, count)
            database.trans_commit()
            print "Successfully committed drop information for %s items" % len(items)
        except Exception as e:
            print "An error occurred committing the drop information.  Rolling back"
            database.trans_rollback()

    print "The drop information was reset to None"
    pending_drop_info = None

def handle_update_user_session(data):
    json_dump(data)

def handle_battle_win(data):
    commit_pending_drop_info()

def handle_battle_escape(data):
    commit_pending_drop_info()

def add_pending_drop_data(item_id, enemy_id):
    global pending_drop_info
    drop_data = pending_drop_info['drops']
    result = drop_data.get(item_id, 0)
    drop_data[item_id] = result+1
    pass

def handle_get_battle_init_data(data):
    global pending_drop_info
    battle_data = data["battle"]
    battle_id = battle_data["battle_id"]

    battle_name = BATTLES.get(battle_id, "battle #" + battle_id)
    print "Entering {0}".format(battle_name)
    all_rounds_data = battle_data['rounds']
    tbl = [["rnd", "enemy", "drop"]]
    pending_drop_info = {'battle_id': battle_id, 'drops': {}}
    for round_data in all_rounds_data:
        round = round_data.get("round", "???")
        for round_drop in round_data["drop_item_list"]:
            tbl.append([round, "<round drop>", round_drop["type"]])
        for enemy in round_data["enemy"]:
            had_drop = False
            enemyname = get_display_name(enemy)
            for drop in get_drops(enemy):
                if "item_id" in drop:
                    kind = "orb id#" if drop["type"] == 51 else "equipment id#"
                    add_pending_drop_data(drop["item_id"], enemy["id"])
                    item = ITEMS.get(drop["item_id"], kind + drop["item_id"])
                    itemname = "{0}* {1}".format(drop.get("rarity", "1"), item)
                else:
                    itemname = "{0} gold".format(drop.get("amount", 0))
                had_drop = True
                tbl.append([round, enemyname, itemname])
            if not had_drop:
                tbl.append([round, enemyname, "nothing"])
    print tabulate(tbl, headers="firstrow")
    print ""
    print "Created drop information %s for battle id %s" % (pending_drop_info, battle_id)

def handle_party_list(data):
    wanted = "name series_id acc atk def eva matk mdef mnd series_acc series_atk series_def series_eva series_matk series_mdef series_mnd"
    topn = OrderedDict()
    topn["atk"] = 5
    topn["matk"] = 2
    topn["mnd"] = 2
    topn["def"] = 5
    find_series = [101001, 102001, 104001, 105001, 106001, 107001, 110001]
    equips = defaultdict(list)
    for item in data["equipments"]:
        kind = item.get("equipment_type", 1)
        heapq.heappush(equips[kind], Equipment(slicedict(item, wanted)))

    for series in find_series:
        print "Best equipment for FF{0}:".format((series - 100001) / 1000)

        # Need to use lists for column ordering
        tbl = ["stat n weapon stat n armor stat n accessory".split()]
        tbldata = [[],[],[],[]]
        for itemtype in range(1, 4): ## 1, 2, 3
            for stat, count in topn.iteritems():
                for equip in best_equipment(series, equips[itemtype], stat, count):
                    name = equip["name"].replace(u"\uff0b", "+")
                    tbldata[itemtype].append([stat, equip[stat], name])

        # Transpose data
        for idx in range(0, len(tbldata[1])):
            tbl.append(tbldata[1][idx] + tbldata[2][idx] + tbldata[3][idx])
        print tabulate(tbl, headers="firstrow")
        print ""

def handle_dungeon_list(data):
    tbl = []
    world_data = data["world"]
    world_id = world_data["id"]
    world_name = world_data["name"]
    print "Dungeon List for {0} (id={1})".format(world_name, world_id)
    dungeons = data["dungeons"]
    for dungeon in dungeons:
        name = dungeon["name"]
        id = dungeon["id"]
        difficulty = dungeon["challenge_level"]
        type = "ELITE" if dungeon["type"] == 2 else "NORMAL"
        tbl.append([name, id, difficulty, type])
    tbl = sorted(tbl, key=lambda row : int(row[1]))
    tbl.insert(0, ["Name", "ID", "Difficulty", "Type"])
    print tabulate(tbl, headers="firstrow")

def handle_battle_list(data):
    tbl = [["Id", "Dungeon", "Name", "Stamina"]]
    dungeon_data = data["dungeon_session"]
    dungeon_id = dungeon_data["dungeon_id"]
    dungeon_name = dungeon_data["name"]
    dungeon_type = int(dungeon_data["type"])
    world_id = dungeon_data["world_id"]
    print "Entering dungeon {0} ({1})".format(dungeon_name, "Elite" if dungeon_type==2 else "Normal")
    battles = data["battles"]
    for battle in battles:
        tbl.append([battle["id"], dungeon_id, battle["name"], battle["stamina"]])
    print tabulate(tbl, headers="firstrow")

def handle_survival_event(data):
    # XXX: This maybe works for all survival events...
    enemy = data.get("enemy", dict(name="???", memory_factor="0"))
    name = enemy.get("name", "???")
    factor = float(enemy.get("memory_factor", "0"))
    print "Your next opponent is {0} (x{1:.1f})".format(name, factor)

def start(context, argv):

    global dp
    dp = Dispatcher('ffrk.denagames.com')
    [dp.register(path, function) for path, function in handlers]
    [dp.ignore(path, regex) for path, regex in ignored_requests]

handlers = [
    ('/get_battle_init_data' , handle_get_battle_init_data),
    ('/dff/party/list', handle_party_list),
    ('/dff/world/dungeons', handle_dungeon_list),
    ('/dff/world/battles', handle_battle_list),
    ('/dff/event/coliseum/6/get_data', handle_survival_event),
    ('/dff/battle/win', handle_battle_win),
    ('/dff/battle/escape', handle_battle_escape),
    ('/win_battle', handle_battle_win),         # Challenge event wins
    ('/escape_battle', handle_battle_escape),   # Challenge event escapes
]

ignored_requests = [
    ('/dff/', True),
    ('/dff/splash', False),
    ('/dff/?timestamp', False),
]

def response(context, flow):
    global dp
    dp.handle(flow)
