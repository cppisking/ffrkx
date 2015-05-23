using FFRKInspector.GameData.Party;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFRKInspector.GameData
{
    class GameState
    {
        private EventBattleInitiated mActiveBattle;
        private EventListBattles mActiveDungeon;
        private DataGachaSeriesList mGachas;
        private DataPartyDetails mParty;

        public GameState()
        {
            mActiveBattle = null;
            mActiveDungeon = null;
            mGachas = null;
        }

        public EventBattleInitiated ActiveBattle
        {
            get { return mActiveBattle; }
            set { mActiveBattle = value; }
        }

        public EventListBattles ActiveDungeon
        {
            get { return mActiveDungeon; }
            set { mActiveDungeon = value; }
        }

        public DataGachaSeriesList GachaSeries
        {
            get { return mGachas; }
            set { mGachas = value; }
        }

        public DataPartyDetails PartyDetails
        {
            get { return mParty; }
            set { mParty = value; }
        }
    }
}
