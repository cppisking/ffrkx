import json
import shlex
import os
import socket
import heapq
from collections import OrderedDict, defaultdict

from ffrkx.proto import messages_pb2
from ffrkx.util import log
from libmproxy.protocol.http import decoded
from tabulate import tabulate

from ffrkx.data import Equipment, ITEMS, BATTLES, DUNGEONS, slicedict, best_equipment

active_battle = None

def get_display_name(enemy):
    for child in enemy["children"]:
        for param in child["params"]:
            return param.get("disp_name", "Unknown Enemy")

def get_drops(enemy):
    for child in enemy["children"]:
        for drop in child["drop_item_list"]:
            yield drop

def handle_win_battle(proxy, path, data):
    proxy.send_battle_encounter(active_battle)

def handle_escape_battle(proxy, path, data):
    proxy.send_battle_encounter(active_battle)

def record_drop_for_active_battle(item_id):
    global active_battle
    if active_battle == None:
        return
    drop_event = next((x for x in active_battle.drop_list if x.item_id == item_id), None)
    if drop_event == None:
        drop_event = active_battle.drop_list.add()
        drop_event.count = 1
        drop_event.item_id = item_id
    else:
        ++drop_event.count
    pass

def handle_get_battle_init_data(proxy, path, data):
    global active_battle
    battle_data = data["battle"]
    battle_id = battle_data["battle_id"]

    log.log_message("Entering Battle #{0}".format(battle_id))
    all_rounds_data = battle_data['rounds']
    tbl = [["rnd", "enemy", "drop"]]
    active_battle = messages_pb2.BattleEncounterMsg()
    active_battle.battle_id = int(battle_id)

    for round_data in all_rounds_data:
        round = round_data.get("round", "???")
        for round_drop in round_data["drop_item_list"]:
            tbl.append([round, "<round drop>", round_drop["type"]])
        for enemy in round_data["enemy"]:
            had_drop = False
            enemyname = get_display_name(enemy)
            for drop in get_drops(enemy):
                if "item_id" in drop:
                    item_id = int(drop["item_id"])
                    kind = "orb id#" if drop["type"] == 51 else "equipment id#"
                    record_drop_for_active_battle(item_id)
                    item = ITEMS.get(item_id, kind + str(item_id))
                    itemname = "{0}* {1}".format(drop.get("rarity", "1"), item)
                else:
                    itemname = "{0} gold".format(drop.get("amount", 0))
                had_drop = True
                tbl.append([round, enemyname, itemname])
            if not had_drop:
                tbl.append([round, enemyname, "nothing"])
    print tabulate(tbl, headers="firstrow")
    print ""
    log.log_message("Created drop information for battle id %s" % battle_id)

def handle_party_list(proxy, path, data):
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
        log.log_message("Best equipment for FF{0}:".format((series - 100001) / 1000))

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
        tabulate(tbl, headers="firstrow")
        print ""

def handle_dungeon_list(proxy, path, data):
    tbl = []
    world_data = data["world"]
    world_id = world_data["id"]
    world_name = world_data["name"]
    "Dungeon List for {0} (id={1})".format(world_name, world_id)
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

def handle_battle_list(proxy, path, data):
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

def handle_enter_survival_event(proxy, path, data):
    # XXX: This maybe works for all survival events...
    enemy = data.get("enemy", dict(name="???", memory_factor="0"))
    name = enemy.get("name", "???")
    factor = float(enemy.get("memory_factor", "0"))
    log.log_message("Your next opponent is {0} (x{1:.1f})".format(name, factor))
