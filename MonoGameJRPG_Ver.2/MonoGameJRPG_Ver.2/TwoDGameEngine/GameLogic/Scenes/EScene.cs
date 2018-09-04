using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.Scenes
{
    public enum EScene
    {
        MainMenuScene,
        FirstLevelScene,
        InventoryScene,

        #region Combat
        BattleTick,
        BattleExecute
        #endregion
    }
}
