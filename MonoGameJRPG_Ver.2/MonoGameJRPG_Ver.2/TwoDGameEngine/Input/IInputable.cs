using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJRPG.TwoDGameEngine
{
    /// <summary>
    /// All types that can receive and handle input.
    /// </summary>
    public interface IInputable
    {
        void HandleKeyboardInput();
        void HandleGamePadInput();
    }
}
