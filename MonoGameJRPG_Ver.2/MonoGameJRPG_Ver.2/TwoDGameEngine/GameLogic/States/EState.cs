using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.States
{
    public enum EState
    {
        MainMenuState,
        FirstMapState,
        InventoryState,

        #region Combat
        BattleTick,
        BattleExecute
        #endregion
    }
}
