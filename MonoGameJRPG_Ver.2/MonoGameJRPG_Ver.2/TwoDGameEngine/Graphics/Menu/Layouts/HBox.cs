using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents.Layouts
{
    /// <summary>
    /// Layout that orders MenuElements horizontally.
    /// </summary>
    public class HBox : Layout
    {
        #region MemberVariables

        private int _x;
        private int _y;
        private int _horizontalOffset;
        private List<MenuElement> _elements = new List<MenuElement>();
        private Action _functionality;
        private Rectangle _rec;

        #endregion
        #region Properties

        public override int Width => CalcWidth();
        public override int Height => HeightTallestElement();
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
            get => _horizontalOffset;
            set => _horizontalOffset = value;
        }

        public override Rectangle Rectangle { get; }

        #endregion

        public HBox(int x = 0, int y = 0, int horizontalOffset = 0, Action functionality = null, params MenuElement[] elements)
        {
            _x = x;
            _y = y;
            _horizontalOffset = horizontalOffset;
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
        /// Returns height of the tallest element in the HBox.
        /// </summary>
        /// <returns></returns>
        private int HeightTallestElement()
        {
            if (_elements.Count == 0)
                return -1;

            int height = _elements[0].Height;

            foreach (MenuElement m in _elements)
                if (m.Height > height)
                    height = m.Height;
            return height;
        }

        /// <summary>
        /// Returns width of the HBox. (See: OneNote "HBox Width Berechnung")
        /// </summary>
        /// <returns></returns>
        private int CalcWidth()
        {
            // HBox with no elements has a width of 0.
            if (_elements.Count == 0)
                return 0;

            int width = 0;

            // Sum of all elements' width values.
            foreach (MenuElement m in _elements)
                width += m.Width;

            // Plus (number of elements - 1) times horizontal offset.
            width += (_elements.Count - 1) * _horizontalOffset;

            return width;
        }

        /// <summary>
        /// Positions elements horizontally.
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

            // Position every element (except first) at VBox.Y and VBox.X + previousElement.X + horizontalOffset.
            // Makes it so that elements aren't stacked on top of each other and variable spacing is possible.
            for (int i = 1; i < _elements.Count; i++)
            {
                _elements[i].Y = this._y;
                _elements[i].X = _elements[i - 1].X + _elements[i - 1].Width + _horizontalOffset;
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuElement m in _elements)
                m.Draw(spriteBatch);
        }

        public override void MouseHoverReaction()
        {
            throw new NotImplementedException();
        }
    }
}
