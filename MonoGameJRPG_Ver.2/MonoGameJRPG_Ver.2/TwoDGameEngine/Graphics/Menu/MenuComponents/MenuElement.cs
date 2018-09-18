using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.Scenes;
using IDrawable = MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.IDrawable;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents
{
    /// <summary>
    /// MenuElements can be added to a Menu.
    /// </summary>
    public abstract class MenuElement : GameObject, IEntity, GameLogic.States.IEntity, IDrawable
    {
        #region MemberVariables

        protected string _name;
        protected int _x;
        protected int _y;
        protected Action _functionality;
        protected bool _cursorOnIt = false;

        #endregion
        #region Properties

        public string Name => _name;

        public int X
        {
            get => _x;
            set => _x = value;
        }

        public int Y
        {
            get => _y;
            set => _y = value;
        }

        public bool CursorOnIt
        {
            get => _cursorOnIt;
            set => _cursorOnIt = value;
        }

        public abstract int Width { get; }
        public abstract int Height { get; }

        public abstract Rectangle Rectangle { get; }

        #endregion
        #region Methods

        protected MenuElement(string name, int x = 0, int y = 0, Action functionality = null)
        {
            _name = name;
            _x = x;
            _y = y;
            _functionality = functionality;

            if (_functionality == null)
                _functionality = () => { Game1.gameConsole.Log(Name + " has no functionality set!"); };
        }

        /// <summary>
        /// Updates the MenuElement.
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            MouseHoverReaction();
            CursorReaction(gameTime);
        }

        /// <summary>
        /// Draws the MenuElement on screen with use of SpriteBatch.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Override this method to describe behaviour of this MenuElement on MouseHover.
        /// </summary>
        public abstract void MouseHoverReaction();

        /// <summary>
        /// Override this method to describe behaviour of this MenuElement when a Cursor is on it.
        /// </summary>
        public abstract void CursorReaction(GameTime gameTime);

        /// <summary>
        /// Executes MenuElement's functionality.
        /// </summary>
        public virtual void ExecuteFunctionality()
        {
            if (_functionality != null)
            {
                _functionality();
                // oder _functionality.Invoke();
            }
            else
                Game1.gameConsole.Log(_name + "'s doesn't have any functionality!");
        }

        /// <summary>
        /// Changes MenuElements functionality to given functionality.
        /// </summary>
        /// <param name="functionality"></param>
        public void ChangeFunctionality(Action functionality)
        {
            _functionality = functionality;
        }

        /// <summary>
        /// Gets whether or not Mouse is hovering over this MenuElement-
        /// </summary>
        /// <returns></returns>
        public virtual bool IsMouseHover()
        {
            return InputManager.IsMouseHoverRectangle(Rectangle);
        }

        /// <summary>
        /// Gets whether Mouse is hovering over MenuElement and at the same time LeftMouseButton has been clicked.
        /// </summary>
        /// <returns></returns>
        public virtual bool OnLeftMouseClick()
        {
            return IsMouseHover() && InputManager.OnLeftMouseClick();
        }

        #endregion
    }
}
