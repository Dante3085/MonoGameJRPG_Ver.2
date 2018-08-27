using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents
{
    /// <summary>
    /// A Menu contains several MenuElements. A Menu itself is a MenuElement, so Menus can be nested.
    /// // TODO: I am not shure about the additional utility this class will or can provide.
    /// </summary>
    public class Menu : MenuElement
    {
        #region MemberVariables

        private List<MenuElement> _menuElements;
        private int _x;
        private int _y;
        private Rectangle _rec;

        #endregion
        #region Properties

        public override int Width => CalcWidth();
        public override int Height => CalcHeight();
        public override int X
        {
            get => _x;
            set => _x = value;
        }
        public override int Y
        {
            get => _y;
            set => _y = value;
        }

        public override Rectangle Rectangle => _rec;

        #endregion
        #region Methods

        public Menu(string name, List<MenuElement> menuElements, int x = 0, int y = 0) : base(name)
        {
            _menuElements = menuElements;
            _x = x;
            _y = y;
            _rec = new Rectangle(x, y, Width, Height);
        }

        public MenuElement ElementByName(string name)
        {
            foreach (MenuElement m in _menuElements)
                if (m.Name.Equals(name))
                    return m;
            Console.WriteLine("@ElementByName(" + name + "): Element with given name does not exist! Returning null.");
            return null;
        }

        public List<MenuElement> Elements()
        {
            return _menuElements;
        }

        public int CalcWidth()
        {
            int width = 0;
            foreach (MenuElement m in _menuElements)
                width += m.Width;
            return width;
        }

        public int CalcHeight()
        {
            int height = 0;
            foreach (MenuElement m in _menuElements)
                height += m.Height;
            return height;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (MenuElement m in _menuElements)
                m.Update(gameTime);
        }

        public override void ExecuteFunctionality()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public override void ChangeFunctionality(Action functionality)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuElement m in _menuElements)
                m.Draw(spriteBatch);
        }

        public override void MouseHoverReaction()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        #endregion
    }
}
