using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameJRPG.TwoDGameEngine.Input;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites.Combos
{
    /// <summary>
    /// Represents a Combo.
    /// </summary>
    public class Combo
    {
        #region MemberVariables

        private ComboNode _root;
        private ComboNode _current;
        private AnimatedSprite _sprite;

        #endregion
        #region Properties

        public ComboNode Root => _root;

        public ComboNode Current
        {
            get => _current;
            set => _current = value;
        }

        public AnimatedSprite Sprite => _sprite;

        #endregion
        #region Methods

        public Combo(ComboNode root, AnimatedSprite sprite)
        {
            _root = root;
            _current = _root;
            _sprite = sprite;
        }

        public void Update(GameTime gameTime)
        {
            _current.Update(gameTime);
        }

        /// <summary>
        /// Resets the Combo to root ComboNode.
        /// </summary>
        public void Reset()
        {
            _current = _root;
        }

        public void Add(ComboNode comboNode)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
