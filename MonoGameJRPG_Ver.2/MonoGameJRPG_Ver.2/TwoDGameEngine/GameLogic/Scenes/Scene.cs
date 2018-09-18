using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.Characters;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.Scenes
{
    public class Scene
    {
        #region MemberVariables

        private string _name;
        protected Texture2D _background;
        protected Rectangle _backgroundRectangle;
        private List<IEntity> _entities = new List<IEntity>();
        private CollisionManager _collisionManager;
        private Action _keyboardHandler;
        private Action _gamePadHandler;

        #endregion

        #region Properties

        public string Name => _name;

        #endregion

        #region Methods

        public Scene(string name, Texture2D background, Action keyboardHandler = null, Action gamePadHandler = null, 
            CollisionManager collisionManager = null, params IEntity[] entities)
        {
            _name = name;
            _background = background;
            _keyboardHandler = keyboardHandler;
            _gamePadHandler = gamePadHandler;
            _collisionManager = collisionManager;
            _backgroundRectangle = new Rectangle(0, 0, Game1.screenWidth, Game1.screenHeight);
            _entities.AddRange(entities);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (InputManager.GamePadConnected())
                HandleGamePadInput();
            else
                HandleKeyboardInput();

            foreach (IEntity e in _entities)
                e.Update(gameTime);

            if (_collisionManager != null)
                _collisionManager.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, _backgroundRectangle, Color.White);
            foreach (IEntity e in _entities)
                e.Draw(spriteBatch);
        }

        #region HandleInput

        public void HandleKeyboardInput()
        {
            if (_keyboardHandler != null)
                _keyboardHandler();
        }

        public void HandleGamePadInput()
        {
            if (_gamePadHandler != null)
                _gamePadHandler();
        }

        #endregion

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }

        #endregion
    }
}
