import random

from ffrkx.db_server.resources import Transaction
from ffrkx.proto import messages_pb2

class DBTestServer:
    def __init__(self, args):
        self.args = args
        db = database.Database(args.user, args.password, args.host, args.database)

    def on_battle_encounter(self, battle_id, drop_list):
        for drop in drop_list:
            drop_event = message.drop_list.add()
            drop_event.item_id = drop[0]
            drop_event.count = drop[1]
        self.db.handle_message(message)

    def run(self):

        # For some reason running a pure SELECT begins a transaction, so let's just begin
        # our own so that it ends after we finish fetching.
        with Transaction(self.db.DBCONN):
            battles = self.db.fetch_all_battles()
            items = self.db.fetch_all_items()

        # Add between 1 and 20 random battle encounters
        for i in xrange(1,20):
            message = messages_pb2.FFRKProxyMessage()
            battle_message = message.battle_encounter
            battle_message.battle_id = random.choice(battles)[0]

            distinct_item_count = random.randint(1, 3)
            for i in xrange(0, distinct_item_count):
                drop_event = battle_message.drop_list.add()
                drop_event.item_id = random.choice(items)[0]
                drop_event.count = random.randint(1,3)

            self.db.handle_message(message)