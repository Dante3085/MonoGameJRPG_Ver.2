using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJRPG.TwoDGameEngine.Input
{
    public class KeyboardInput
    {
        #region WorldMovement

        public Keys Left { get; set; }
        public Keys Up { get; set; }
        public Keys Right { get; set; }
        public Keys Down { get; set; }
        public Keys Run { get; set; }
        public Keys Interact { get; set; }

        #endregion
        #region Combat

        public Keys CursorLeft { get; set; }
        public Keys CursorUp { get; set; }
        public Keys CursorRight { get; set; }
        public Keys CursorDown { get; set; }
        public Keys Confirm { get; set; }
        public Keys Back { get; set; }

        public Keys Combo { get; set; }

        #endregion
        #region Methods

        /// <summary>
        /// Returns KeyboardInput with default Layout.
        /// </summary>
        /// <returns></returns>
        public static KeyboardInput Default()
        {
            return new KeyboardInput()
            {
                Left = Keys.A,
                Up = Keys.W,
                Right = Keys.D,
                Down = Keys.S,
                Run = Keys.LeftShift,
                Interact = Keys.Space,
                Combo = Keys.F,
            };
        }

        /// <summary>
        /// Returns KeyboardInput with each key being set to Keys.None.
        /// </summary>
        /// <returns></returns>
        public static KeyboardInput None()
        {
            return new KeyboardInput()
            {
                Left = Keys.None,
                Up = Keys.None,
                Right = Keys.None,
                Down = Keys.None,
                Run = Keys.None,
                Interact = Keys.None,
                Combo = Keys.None,
            };
        }

        public override string ToString()
        {
            return "Lef\nt: " + Left + ", Up: " + Up + ", Right: " + Right +
                   ", Down: " + Down + ", Interact: " + Interact + ", Combo: "
                   + Combo;
        }

        #endregion
    }
}