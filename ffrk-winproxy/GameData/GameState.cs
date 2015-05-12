using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffrk_winproxy.GameData
{
    class GameState
    {
        private EventBattleInitiated mActiveBattle;
        private EventListBattles mActiveDungeon;

        public GameState()
        {
            mActiveBattle = null;
            mActiveDungeon = null;
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
    }
}
