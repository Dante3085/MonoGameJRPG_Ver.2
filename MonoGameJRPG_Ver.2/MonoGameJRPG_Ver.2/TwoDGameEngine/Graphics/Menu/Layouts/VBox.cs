using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.Layouts
{
    /// <summary>
    /// Layout that orders MenuElements vertically.
    /// </summary>
    public class VBox : Layout
    {
        #region MemberVariables

        private int _x;
        private int _y;
        private int _verticalOffset;
        private List<MenuElement> _elements = new List<MenuElement>();
        private Action _functionality;
        private Rectangle _rec;

        #endregion

        #region Properties

        public override int Width => WidthWidestElement();
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
        public override int Offset
        {
            get => _verticalOffset;
            set => _verticalOffset = value;
        }

        public override Rectangle Rectangle => _rec;

        #endregion

        public VBox(int x = 0, int y = 0, int verticalOffset = 0, Action functionality = null, params MenuElement[] elements)
        {
            _x = x;
            _y = y;
            _verticalOffset = verticalOffset;
            _functionality = functionality;

            if (elements != null)
            {
                _elements.AddRange(elements);
                OrderElements();
            }
            else
                _elements = new List<MenuElement>();

            _rec = new Rectangle(x, y, Width, Height);
        }

        /// <summary>
        /// Returns height of the VBox.
        /// </summary>
        /// <returns></returns>
        private int CalcHeight()
        {
            // VBox with no elements has a height of 0.
            if (_elements.Count == 0)
                return 0;

            int height = 0;


            // Sum of all elements' height values.
            foreach (MenuElement m in _elements)
                height += m.Height;

            // Plus (number of elements - 1) times vertical offset.
            height += (_elements.Count - 1) * _verticalOffset;

            return height;
        }

        /// <summary>
        /// Returns width of the widest element in VBox.
        /// </summary>
        /// <returns></returns>
        private int WidthWidestElement()
        {
            if (_elements.Count == 0)
                return -1;

            int width = _elements[0].Width;

            foreach (MenuElement m in _elements)
                if (m.Width > width)
                    width = m.Width;
            return width;
        }

        /// <summary>
        /// Orders elements vertically.
        /// Position of element is dependant on position of element in element list.
        /// </summary>
        public override void OrderElements()
        {
            if (_elements.Count == 0)
                return;

            // Position first element at upper left corner of VBox.
            _elements[0].X = this._x;
            _elements[0].Y = this._y;

            if (_elements.Count == 1)
                return;

            // Position every element (except first) at VBox.X and VBox.Y + previousElement.Y + verticalOffset.
            // Makes it so that elements aren't stacked on top of each other and variables spacing is possible.
            for (int i = 1; i < _elements.Count; i++)
            {
                _elements[i].X = this._x;
                _elements[i].Y = _elements[i - 1].Y + _elements[i - 1].Height + _verticalOffset;
            }
        }

        public override List<MenuElement> Elements()
        {
            return _elements;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (MenuElement m in _elements)
                m.Update(gameTime);
            OrderElements();

            // Update rec.
            _rec.X = _x;
            _rec.Y = _y;
            _rec.Width = Width;
            _rec.Height = Height;
        }

        public override void ExecuteFunctionality()
        {
            _functionality();
        }

        public override void ChangeFunctionality(Action functionality)
        {
            _functionality = functionality;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            foreach (MenuElement m in _elements)
                m.Render(spriteBatch);
        }

        public override void MouseHoverReaction()
        {
            // TODO
        }
    }
}
