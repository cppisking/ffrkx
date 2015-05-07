
from ffrkx.proxy.proxy import FFRKXProxy
import ffrkx.proxy.response

FFRKXProxy.register_handler("/get_battle_init_data", response.handle_get_battle_init_data)
FFRKXProxy.register_handler("/dff/party/list", response.handle_party_list)
FFRKXProxy.register_handler("/dff/world/dungeons", response.handle_dungeon_list)
FFRKXProxy.register_handler("/dff/world/battles", response.handle_battle_list)
FFRKXProxy.register_handler("/coliseum/6/get_data", response.handle_enter_survival_event)
FFRKXProxy.register_handler("/dff/battle/win", response.handle_win_battle)
FFRKXProxy.register_handler("/win_battle", response.handle_win_battle)
FFRKXProxy.register_handler("/dff/battle/escape", response.handle_escape_battle)
FFRKXProxy.register_handler("/escape_battle", response.handle_escape_battle)
