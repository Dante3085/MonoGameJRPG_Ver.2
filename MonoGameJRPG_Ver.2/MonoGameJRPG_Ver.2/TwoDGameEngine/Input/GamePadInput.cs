using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJRPG.TwoDGameEngine.Input
{
    /// <summary>
    /// Masks GamePadInput for this game.
    /// </summary>
    public class GamePadInput
    {
        #region WorldMovement
        public Buttons Left { get; set; }
        public Buttons Up { get; set; }
        public Buttons Right { get; set; }
        public Buttons Down { get; set; }
        public Buttons Run { get; set; }
        public Buttons Interact { get; set; }
        #endregion
        #region Combat
        public Buttons CursorLeft { get; set; }
        public Buttons CursorUp { get; set; }
        public Buttons CursorRight { get; set; }
        public Buttons CursorDown { get; set; }
        public Buttons Confirm { get; set; }
        public Buttons Back { get; set; }

        public Buttons Combo { get; set; } 
        #endregion
        #region Methods

        /// <summary>
        /// Returns GamePadInput with default Layout.
        /// </summary>
        /// <returns></returns>
        public static GamePadInput Default()
        {
            return new GamePadInput()
            {
                Left = Buttons.LeftThumbstickLeft,
                Up = Buttons.LeftThumbstickUp,
                Right = Buttons.LeftThumbstickRight,
                Down = Buttons.LeftThumbstickDown,
                Run = Buttons.RightShoulder,
                Interact = Buttons.A,
                Combo = Buttons.X,
            };
        }

        public static GamePadInput None()
        {
            return new GamePadInput()
            {
                Left = 0,
                Up = 0,
                Right = 0,
                Down = 0,
                Run = 0,
                Interact = 0,
                Combo = 0,
            };
        }

        #endregion
    }
}
