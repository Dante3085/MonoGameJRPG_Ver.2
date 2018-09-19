using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Utils;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites.Combos
{
    /// <summary>
    /// Represents a part of a Combo.
    /// </summary>
    public class ComboNode
    {
        #region MemberVariables

        /// <summary>
        /// Animation associated to this part of the Combo.
        /// </summary>
        private EAnimation _animation;

        /// <summary>
        /// Part of the Combo that comes after this.
        /// </summary>
        private Dictionary<Keys, ComboNode> _next;

        /// <summary>
        /// When this ComboNode has finished, the Intervall describes the TimeFrame in which the corresponding
        /// Key / Button of the next ComboNode has to be pressed for it to execute.
        /// </summary>
        private Intervall _intervall;

        /// <summary>
        /// Key to execute this ComboNode.
        /// </summary>
        private Keys _key;

        /// <summary>
        /// Button to execute this ComboNode.
        /// </summary>
        private Buttons _button;

        #endregion

        /// <summary>
        /// Describes a time intervall in which the Key / Button has to be pressed to continue the Combo.
        /// </summary>
        public struct Intervall
        {
            private int _start;
            private int _end;

            public int Start => _start;
            public int End => _end;

            public Intervall(int start, int end)
            {
                _start = start;
                _end = end;
            }
        }

        #region Properties

        public EAnimation Animation => _animation;
        public Dictionary<Keys, ComboNode> Next => _next;
        public Intervall TimeIntervall => _intervall;
        public Keys Key => _key;
        public Buttons Button => _button;

        #endregion

        public ComboNode(EAnimation animation, Dictionary<Keys, ComboNode> next, Intervall intervall, 
            Keys key, Buttons button)
        {
            _animation = animation;
            _next = next;
            _intervall = intervall;
            _key = key;
            _button = button;
        }
    }
}
