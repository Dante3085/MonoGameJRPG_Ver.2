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

        public Keys Attack { get; set; } // DELTE ME LATER
        #endregion


    }
}
