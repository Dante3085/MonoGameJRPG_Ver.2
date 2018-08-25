using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG_Ver._2.Characters;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.States
{
    public abstract class State
    {
        protected SpriteBatch _spriteBatch;
        protected Texture2D _background;
        protected Rectangle _backgroundRectangle;
        protected List<Character> _characters;
        protected List<AnimatedSprite> _characterSprites = new List<AnimatedSprite>();

        public State(SpriteBatch spriteBatch, Texture2D background, int backgroundWidth, int backgroundHeight, List<Character> characters = null)
        {
            _spriteBatch = spriteBatch;
            _background = background;
            _backgroundRectangle = new Rectangle(0, 0, backgroundWidth, backgroundHeight);
            _characters = characters;

            // SCHLECHTE LÖSUNG: Habe den Eindruck, dass der Zugriff auf die AnimatedSprites
            // der Characters besser gelöst werden könnte.
            if (_characters != null)
                foreach (Character c in _characters)
                    _characterSprites.Add(c.AnimatedSprite);
        }

        public abstract void Update(GameTime gameTime);
        public virtual void Render()
        {
            _spriteBatch.Draw(_background, _backgroundRectangle, Color.White);
        }
        public abstract void OnEnter();
        public abstract void OnExit();
    }
}
