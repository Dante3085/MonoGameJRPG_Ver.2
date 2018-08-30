using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.Layouts
{
    /// <summary>
    /// Describes essential parts of a layout. <para></para>
    /// At it's most essential a layout has to provide a Method that orders it's elements in a specific way (vetically, horizontally, etc.)
    /// </summary>
    public abstract class Layout : MenuElement
    {
        #region MemberVariables

        protected List<MenuElement> _elements = new List<MenuElement>();

        #endregion
        #region Properties

        public List<MenuElement> Elements => _elements;

        #endregion
        #region Constructors

        protected Layout(string name, int x = 0, int y = 0, Action functionality = null, params MenuElement[] elements) : base(name, x, y, functionality)
        {
            _x = x;
            _y = y;
            _elements.AddRange(elements);
        }

        #endregion
        #region Methods

        public abstract int Offset { get; set; }
        public abstract void OrderElements();

        /// <summary>
        /// Updates and orders Layout Elements.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (MenuElement m in _elements)
                m.Update(gameTime);
            OrderElements();
        }

        public MenuElement ElementByName(string name)
        {
            foreach (MenuElement m in _elements)
                if (m.Name.Equals(name))
                    return m;
            throw new ElementNotFoundException("@ElementByName(" + name + "): Element specified by name does not exist! Returning null.");
        }

        #endregion
    }
}
