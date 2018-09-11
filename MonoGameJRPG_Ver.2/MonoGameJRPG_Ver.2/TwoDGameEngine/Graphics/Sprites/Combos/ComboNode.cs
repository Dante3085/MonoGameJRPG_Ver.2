using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG.TwoDGameEngine.Input;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites.Combos
{
    /// <summary>
    /// Represents a part of a Combo.
    /// </summary>
    public class ComboNode
    {
        #region MemberVariables

        /// <summary>
        /// The Combo that this ComboNode is part of.
        /// </summary>
        private Combo _combo;

        /// <summary>
        /// Animation associated to this part of the Combo.
        /// </summary>
        private EAnimation _animation;

        /// <summary>
        /// Part of the Combo that comes after this.
        /// </summary>
        private Dictionary<Keys, ComboNode> _next;

        /// <summary>
        /// PassedTime since last ComboNode finished.
        /// </summary>
        private int _passedTime;

        private bool _executed;

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

        public Keys Key => _key;
        public EAnimation Animation => _animation;

        public ComboNode(Combo combo, EAnimation animation, Dictionary<Keys, ComboNode> next, Intervall intervall, 
            Keys key, Buttons button)
        {
            _combo = combo;
            _animation = animation;
            _next = next;
            _intervall = intervall;
            _key = key;
            _button = button;
        }

        public void Update(GameTime gameTime)
        {
            // If this ComboNode hasn't been executed, check Input for executing it.
            if (!_executed)
            {
                if (InputManager.IsKeyDown(_key))
                {
                    _combo.Sprite.SetAnimation(_animation);
                    _executed = true;
                }
            }

            // If this ComboNode has been executed, so long as Intervall.End has not been exceeded, check
            // if Input for next ComboNode is given.
            else
            {
                if (_next == null)
                {
                    _combo.Reset();
                    _executed = false;
                }

                _passedTime += gameTime.ElapsedGameTime.Milliseconds;
                if (_passedTime > _intervall.Start && _passedTime < _intervall.End)
                {
                    foreach (Keys k in _next.Keys)
                        if (InputManager.IsKeyDown(k))
                        {
                            _combo.Current = _next[k];
                            _executed = false;
                        }
                }
                else if (_passedTime > _intervall.End)
                {
                    _combo.Reset();
                    _executed = false;
                }
            }
        }
    }
}
