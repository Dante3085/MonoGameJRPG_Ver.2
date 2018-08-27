﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG.TwoDGameEngine.Input;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents
{
    /// <summary>
    /// MenuElements can be added to a Menu.
    /// </summary>
    public abstract class MenuElement
    {
        #region MemberVariables

        private int _x;
        private int _y;
        private string _name;
        private List<MenuElement> _elements;

        #endregion
        #region Properties

        public string Name => _name;
        public List<MenuElement> Elements => _elements;

        #endregion
        #region AbstractProperties

        public abstract int Width { get; }
        public abstract int Height { get; }

        public abstract int X { get; set; }
        public abstract int Y { get; set; }

        public abstract Rectangle Rectangle { get; }

        #endregion
        #region Constructors

        public MenuElement(string name)
        {
            _name = name;
        }

        #endregion
        #region ImplementedMethods

        public MenuElement ElementByName(string name)
        {
            foreach (MenuElement m in _elements)
                if (m.Name.Equals(name))
                    return m;
            Console.WriteLine("@ElementByName(" + name + "): Element specified by name does not exist! Returning null.");
            return null;
        }

        #endregion
        #region AbstractMethods

        /// <summary>
        /// Updates the MenuElement.
        /// </summary>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Executes MenuElement's functionality.
        /// </summary>
        public abstract void ExecuteFunctionality();

        /// <summary>
        /// Changes MenuElements functionality to given functionality.
        /// </summary>
        /// <param name="functionality"></param>
        public abstract void ChangeFunctionality(Action functionality);

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
