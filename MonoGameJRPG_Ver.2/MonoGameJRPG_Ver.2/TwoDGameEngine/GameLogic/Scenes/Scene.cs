using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG_Ver._2.Characters;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.Scenes
{
    public class Scene
    {
        protected Texture2D _background;
        protected Rectangle _backgroundRectangle;
        private List<IEntity> _entities = new List<IEntity>();

        public Scene(Texture2D background, params IEntity[] entities)
        {
            _background = background;
            _backgroundRectangle = new Rectangle(0, 0, Game1.screenWidth, Game1.screenHeight);
            _entities.AddRange(entities);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (IEntity e in _entities)
                e.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, _backgroundRectangle, Color.White);
            foreach (IEntity e in _entities)
                e.Draw(spriteBatch);
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }
    }
}
