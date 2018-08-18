using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites
{
    public enum EAnimation
    {
        #region Movement

        Left,
        Up,
        Right,
        Down,

        #endregion
        #region Idle

        IdleLeft,
        IdleUp,
        IdleRight,
        IdleDown,
        Idle,


        #endregion
        #region MeleeAttacks

        MeleeLeft,
        MeleeUp,
        MeleeRight,
        MeleeDown,

        #endregion
        #region RangedAttacks

        RangedLeft,
        RangedUp,
        RangedRight,
        RangedDown,

        #endregion

    }
}
